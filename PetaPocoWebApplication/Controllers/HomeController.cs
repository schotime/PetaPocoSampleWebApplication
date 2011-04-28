using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaPoco;
using PetaPocoWebApplication.Handlers;
using PetaPocoWebApplication.Infrastructure;
using PetaPocoWebApplication.Models;

namespace PetaPocoWebApplication.Controllers
{
	[HandleError]
	[PetaPocoContext]
    public class HomeController : PetaController
	{
	    private readonly IQueryInvoker _queryInvoker;
	    private readonly ICommandInvoker _commandInvoker;

	    public HomeController(IQueryInvoker queryInvoker, ICommandInvoker commandInvoker)
		{
		    _queryInvoker = queryInvoker;
		    _commandInvoker = commandInvoker;
		}

	    public ActionResult Index()
	    {
	        var viewmodel = _queryInvoker.Invoke<HomeIndexViewModel>();
			return View(viewmodel);
		}

        [HttpPost]
        public ActionResult Delete(HomeDeleteInputModel inputModel)
        {
            var commandResult = _commandInvoker.Invoke(inputModel);
            if (commandResult.Success)
                Flash("Successfully deleted");

            return RedirectToAction("Index");
        }
		
		public ActionResult About()
		{
			return View();
		}
	}
}
