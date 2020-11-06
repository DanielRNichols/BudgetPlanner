using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class BudgetItemGroup : IDbResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int BudgetItemTypeId { get; set; }

        public virtual BudgetItemType BudgetItemType { get; set; }

        // Workaround for select no handling integers
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Must select a Budget Item Type")]
        public string SelectedBudgetItemTypeId { get; set; }
    }

 }

