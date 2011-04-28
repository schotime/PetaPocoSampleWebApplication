using System.Web.Mvc;

namespace PetaPocoWebApplication.Infrastructure
{
    public class PetaController : Controller
    {
        public void Flash(string text)
        {
            TempData["__flash"] = text;
        }
    }
}