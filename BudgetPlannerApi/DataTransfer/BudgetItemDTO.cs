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
        public int BudgetItemGroupId { get; set; }

        public virtual BudgetItemGroupDTO BudgetItemGroup { get; set; }

        public virtual IList<BudgetCycleItemDTO> BudgetCycleItems { get; set; }
    }

    public class BudgetItemCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int BudgetItemGroupId { get; set; }
    }
}
