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
    }
}