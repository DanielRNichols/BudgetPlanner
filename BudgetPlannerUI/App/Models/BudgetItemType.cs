using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class BudgetItemType : IDbResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }

        public bool IsExpense { get; set; }

        //public virtual IList<BudgetItemGroup> BudgetItemGroups { get; set; }
    }
}
