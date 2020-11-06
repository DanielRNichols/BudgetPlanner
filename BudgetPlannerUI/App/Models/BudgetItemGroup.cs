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

        [Required]
        public int BudgetItemTypeId { get; set; }

        public virtual BudgetItemType BudgetItemType { get; set; }
    }
}
