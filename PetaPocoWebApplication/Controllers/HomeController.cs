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
	public class HomeController : Controller
	{
	    private readonly IQueryInvoker _queryInvoker;

		public HomeController(IQueryInvoker queryInvoker)
		{
		    _queryInvoker = queryInvoker;
		}

	    public ActionResult Index()
	    {
	        var viewmodel = _queryInvoker.Invoke<HomeIndexViewModel>();
			return View(viewmodel);
		}
		
		public ActionResult About()
		{
			return View();
		}
	}

}
