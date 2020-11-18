using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("BudgetCycleItems")]
    public class BudgetCycleItem : IDbResource
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BudgetCycleId { get; set; }
        public int BudgetItemId { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public bool MarkedForDeletion { get; set; }

        public virtual BudgetCycle BudgetCycle {get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
    }
}
