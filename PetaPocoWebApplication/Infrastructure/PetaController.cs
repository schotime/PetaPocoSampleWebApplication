using System.Web.Mvc;

namespace PetaPocoWebApplication.Infrastructure
{
    [HandleError]
    [PetaPocoContext]
    [PetaTuning]
    public class PetaController : Controller
    {
        public void Flash(string text)
        {
            TempData["__flash"] = text;
        }

        protected override void ExecuteCore()
        {
            if (!ControllerContext.IsChildAction)
            {
                base.TempData.Load(ControllerContext, TempDataProvider);
            }

            try
            {
                if (!ActionInvoker.InvokeAction(ControllerContext, "execute"))
                {
                    HandleUnknownAction("execute");
                }
            }
            finally
            {
                if (!ControllerContext.IsChildAction)
                {
                    base.TempData.Save(ControllerContext, TempDataProvider);
                }
            }

        }

    }
}