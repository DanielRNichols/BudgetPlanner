using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class BudgetItemGroupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BudgetItemTypeId { get; set; }
        public virtual BudgetItemTypeDTO BudgetItemType { get; set; }
    }

    public class BudgetItemGroupCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int BudgetItemTypeId { get; set; }
    }
}
