using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaPoco;
using PetaPocoWebApplication.Infrastructure;

namespace PetaPocoWebApplication.Controllers
{
    [HandleError]
    [PetaPocoContext]
    public class HomeController : Controller
    {
        private readonly IDatabaseQuery _databaseQuery;

        public HomeController(IDatabaseQuery databaseQuery)
        {
            _databaseQuery = databaseQuery;
        }

        public ActionResult Index()
        {
            ViewData["Message"] = "Peta Poco Sample Application";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }

}
