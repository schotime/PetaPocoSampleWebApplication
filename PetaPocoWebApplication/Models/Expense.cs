using PetaPoco;

namespace PetaPocoWebApplication.Models
{
    [TableName("Expenses")]
    [PrimaryKey("ExpenseId")]
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int BudgetPeriodId { get; set; }
        public string Description { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public string Remark { get; set; }
        
        [VersionColumn]
        public int Version { get; set; }
    }
}