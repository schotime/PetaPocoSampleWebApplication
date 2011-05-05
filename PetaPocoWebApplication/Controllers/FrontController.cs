using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaPocoWebApplication.Infrastructure;
using StructureMap;

namespace PetaPocoWebApplication.Controllers
{
    public class FrontController : PetaController
    {
        public ActionResult Execute()
        {
            var controller = ControllerContext.RouteData.GetRequiredString("controller");
            var action = ControllerContext.RouteData.GetRequiredString("action");

            var o = ObjectFactory.GetNamedInstance<IQueryHandler>(controller.ToLowerInvariant() + action.ToLowerInvariant() + "handler");
            var ts = o.GetType().BaseType.GetGenericArguments();

            object viewmodel = null, inputmodel = null;
            if (ts.Length == 1)
            {
                viewmodel = o.Handle();
            }
            else if (ts.Length == 2)
            {
                inputmodel = Activator.CreateInstance(ts[1]);
                UpdateModel(inputmodel);
                viewmodel = o.Handle(inputmodel);
            }

            return View(viewmodel);
        }
    }



    public class FrontExecuteHandler : QueryHandler<TestIndexViewModel, object>
    {
        public override object Handle(TestIndexViewModel inputmodel)
        {
            return new TestIndexViewModel();
        }
    }

    public abstract class QueryHandler<T> : IQueryHandler<T>
    {
        public abstract T Handle();
        public object Handle(object inputmodel)
        {
            return null;
        }

        object IQueryHandler.Handle()
        {
            return Handle();
        }
    }

    public abstract class QueryHandler<T, U> : IQueryHandler<T, U>
    {
        public abstract U Handle(T inputmodel);
        public object Handle()
        {
            return null;
        }

        public object Handle(object inputmodel)
        {
            return Handle((T) inputmodel);
        }
    }

    public class TestIndexViewModel
    {
        
    }

}