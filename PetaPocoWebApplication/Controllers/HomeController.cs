using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaPoco;
using PetaPocoWebApplication.Handlers;
using PetaPocoWebApplication.Infrastructure;
using PetaPocoWebApplication.Models;
using System.Diagnostics;

namespace PetaPocoWebApplication.Controllers
{
	[HandleError]
	[PetaPocoContext]
    public class HomeController : PetaController
	{
	    private readonly IInvoker _invoker;

        public HomeController(IInvoker invoker)
        {
            _invoker = invoker;
        }

	    public ActionResult Index()
	    {
            var viewmodel = _invoker.InvokeQuery<HomeIndexViewModel>();
			return View(viewmodel);
		}

        [HttpPost]
        public ActionResult Delete(HomeDeleteInputModel inputModel)
        {
            var commandResult = _invoker.InvokeCommand<HomeDeleteInputModel, bool>(inputModel);
            if (commandResult)
                Flash("Successfully deleted");

            return RedirectToAction("Index");
        }
		
		public ActionResult About()
		{
			return View();
		}
	}
}
