using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using PetaPocoWebApplication.Infrastructure;
using PetaPocoWebApplication.Models;

namespace PetaPocoWebApplication.Handlers
{
    public class HomeIndexHandler : IQueryHandler<HomeIndexViewModel>
    {
        private readonly IDatabaseQuery _databaseQuery;

        public HomeIndexHandler(IDatabaseQuery databaseQuery)
        {
            _databaseQuery = databaseQuery;
        }

        public void Handle(HomeIndexViewModel viewmodel)
        {
            viewmodel.Message = "Welcome to PetaPoco";
            viewmodel.BudgetPeriod = _databaseQuery.First<BudgetPeriod>("");
            viewmodel.Expenses = _databaseQuery
                .Fetch<Expense>("where BudgetPeriodId = @0", viewmodel.BudgetPeriod.BudgetPeriodId)
                .OrderBy(x => x.Description)
                .ToList();


        }
    }


    public class HomeIndexViewModel
    {
        public HomeIndexViewModel()
        {
            Expenses = new List<Expense>();
        }

        public string Message { get; set; }

        public BudgetPeriod BudgetPeriod { get; set; }
        public IList<Expense> Expenses { get; set; }
    }
}