using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class BudgetCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BudgetGroupId { get; set; }
        public virtual BudgetGroupDTO BudgetGroup { get; set; }
        public virtual IList<BudgetItemDTO> BudgetItems { get; set; }
        public bool MarkedForDeletion { get; set; }
    }

    public class BudgetCategoryCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int BudgetGroupId { get; set; }
        public bool MarkedForDeletion { get; set; }
    }
}
