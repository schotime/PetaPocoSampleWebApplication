using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;
using PetaPocoWebApplication.Infrastructure;
using PetaPocoWebApplication.Models;

namespace PetaPocoWebApplication.Handlers
{
    public class HomeDeleteHandler : ICommandHandler<HomeDeleteInputModel>
    {
        private readonly IDatabase _database;

        public HomeDeleteHandler(IDatabase database)
        {
            _database = database;
        }

        public CommandResult Handle(HomeDeleteInputModel inputModel)
        {
            _database.Delete<Expense>(inputModel.ExpenseId);
            return new CommandResult { Success = true };
        }
    }

    public class HomeDeleteInputModel
    {
        public int ExpenseId { get; set; }
    }
}
