using PetaPoco;
using PetaPocoWebApplication.Infrastructure;
using PetaPocoWebApplication.Models;

namespace PetaPocoWebApplication.Handlers
{
    public class HomeAddHandler : ICommandHandler<HomeAddInputModel>
    {
        private readonly IDatabase _database;

        public HomeAddHandler(IDatabase database)
        {
            _database = database;
        }

        public void Handle(HomeAddInputModel inputModel)
        {
            var expense = new Expense
                              {
                                  Description = inputModel.Description,
                                  BudgetAmount = inputModel.BudgetAmount,
                                  BudgetPeriodId = inputModel.BudgetPeriodId
                              };

            _database.Insert(expense);
        }
    }

    public class HomeAddInputModel
    {
        public int BudgetPeriodId { get; set; }
        public string Description { get; set; }
        public decimal BudgetAmount { get; set; }
    }
}