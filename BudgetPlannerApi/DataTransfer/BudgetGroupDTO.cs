using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class BudgetGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
 
        public virtual IList<BudgetCategoryDTO> BudgetCategories { get; set; }

    }
    public class BudgetGroupCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
