using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetaPoco;
using PetaPocoWebApplication.Infrastructure;
using PetaPocoWebApplication.Models;

namespace PetaPocoWebApplication.Controllers
{
	[HandleError]
	[PetaPocoContext]
	public class HomeController : Controller
	{
		private readonly IDatabase _database;

		public HomeController(IDatabase database)
		{
			_database = database;
		}

		public ActionResult Index()
		{
			var periods = _database.Fetch<BudgetPeriod>();
			if (!periods.Any())
			{
				var period = new BudgetPeriod
								 {
									 Description = "April",
									 FromDate = new DateTime(2011, 4, 1),
									 ToDate = new DateTime(2011, 4, 30)
								 };

				_database.Insert(period);
			    periods.Add(period);

			    var expense = new Expense
			                      {
			                          BudgetPeriodId = period.BudgetPeriodId,
			                          Description = "Test Expense",
			                          BudgetAmount = 20.0m
			                      };

                var expense2 = new Expense
                {
                    BudgetPeriodId = period.BudgetPeriodId,
                    Description = "Extra meals",
                    BudgetAmount = 100.0m
                };

				_database.Insert(expense);
                _database.Insert(expense2);
			}

		    var model = new HomeIndexViewModel
		                    {
		                        Message = "Welcome to PetaPoco",
		                        BudgetPeriod = periods.First(),
		                        Expenses = _database.Fetch<Expense>("where budgetperiodid = @0", periods.First().BudgetPeriodId)
		                    };

			return View(model);
		}
		
		public ActionResult About()
		{
			return View();
		}
	}

	public class HomeIndexViewModel
	{
		public string Message { get; set; }

		public BudgetPeriod BudgetPeriod { get; set; }
		public IList<Expense> Expenses { get; set; }
	}
}
