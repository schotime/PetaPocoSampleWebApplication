using System;
using System.IO;
using System.Web.Mvc;
using PetaPoco;

namespace PetaPocoWebApplication.Infrastructure
{
    public class PetaPocoContextAttribute : FilterUsingAttribute
    {
        public PetaPocoContextAttribute()
            : base(typeof(PetaPocoFilter))
        {
        }

        public class PetaPocoFilter : IActionFilter, IExceptionFilter
        {
            private readonly IDatabase _database;

            private bool wasBound;
            private bool wasUnBound;

            public PetaPocoFilter(IDatabase database)
            {
                _database = database;
            }

            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
                _database.BeginTransaction();
                wasBound = true;
            }

            public void OnActionExecuted(ActionExecutedContext filterContext)
            {
                if (!wasUnBound)
                    EndSession(filterContext.Exception != null, filterContext.Controller.ViewData.ModelState.IsValid);
            }

            public void OnException(ExceptionContext filterContext)
            {
                File.AppendAllText(filterContext.HttpContext.Server.MapPath("~/log.txt"), DateTime.Now + " - EXCEPTION: " + filterContext.Exception + Environment.NewLine);
                if (!wasUnBound)
                    EndSession(true, true);
            }

            private void EndSession(bool isException, bool modelstateValid)
            {
                if (wasBound)
                {
                    if (isException || !modelstateValid)
                        _database.AbortTransaction();
                    else
                        _database.CompleteTransaction();

                    wasUnBound = false;
                }
            }
        }
    }



}