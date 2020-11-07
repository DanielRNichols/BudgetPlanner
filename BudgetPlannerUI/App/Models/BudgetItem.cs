using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class BudgetItem : IDbResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsIncome { get; set; }

        public int BudgetCategoryId { get; set; }

        public virtual BudgetCategory BudgetCategory { get; set; }

        // Workaround for select no handling integers
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Must select a Budget Category")]
        public string SelectedBudgetCategoryId { get; set; }
    }
}
