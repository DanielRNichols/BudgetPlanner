using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class BudgetItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; }
        public int BudgetCategoryId { get; set; }
        public bool MarkedForDeletion { get; set; }

        public virtual BudgetCategoryDTO BudgetCategory { get; set; }

        public virtual IList<BudgetCycleItemDTO> BudgetCycleItems { get; set; }
    }

    public class BudgetItemCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsIncome { get; set; }
        [Required]
        public int BudgetCategoryId { get; set; }
        public bool MarkedForDeletion { get; set; }
    }
}
