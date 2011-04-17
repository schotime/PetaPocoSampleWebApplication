using System;
using PetaPoco;

namespace PetaPocoWebApplication.Models
{
    [TableName("BudgetPeriods")]
    [PrimaryKey("BudgetPeriodId")]
    public class BudgetPeriod
    {
        public int BudgetPeriodId { get; set; }
        public string Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}