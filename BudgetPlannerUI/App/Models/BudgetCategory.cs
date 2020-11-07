using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class BudgetCategory : IDbResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int BudgetGroupId { get; set; }

        public virtual BudgetGroup BudgetGroup { get; set; }

        // Workaround for select no handling integers
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Must select a Budget Group")]
        public string SelectedBudgetGroupId { get; set; }
    }

 }

