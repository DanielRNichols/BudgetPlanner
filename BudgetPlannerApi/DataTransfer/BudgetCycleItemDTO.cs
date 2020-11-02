using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class BudgetCycleItemDTO
    {
        public int Id { get; set; }
        public int BudgetCycleId { get; set; }
        public int BudgetItemId { get; set; }

        public decimal Amount { get; set; }

        public virtual BudgetCycleDTO BudgetCycle { get; set; }
        public virtual BudgetItemDTO BudgetItem { get; set; }
    }
    public class BudgetCycleItemCreateDTO
    {
        [Required]
        public int BudgetCycleId { get; set; }

        [Required]
        public int BudgetItemId { get; set; }

        public decimal Amount { get; set; }

    }
}
