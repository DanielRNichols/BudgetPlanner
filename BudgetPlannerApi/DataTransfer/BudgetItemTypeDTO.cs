using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class BudgetItemTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsExpense { get; set; }
    }
    public class BudgetItemTypeCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsExpense { get; set; }
    }
}
