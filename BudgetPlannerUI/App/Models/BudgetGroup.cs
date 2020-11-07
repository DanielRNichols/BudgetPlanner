using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class BudgetGroup : IDbResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }


        //public virtual IList<BudgetCategory> BudgetCategories { get; set; }
    }
}
