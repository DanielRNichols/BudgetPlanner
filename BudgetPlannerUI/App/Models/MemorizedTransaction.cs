using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class MemorizedTransaction : IDbResource
    {
        public int Id { get; set; }
        [Required]
        public string Payee { get; set; }
        public decimal Amount { get; set; }
        public int BudgetItemId { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }

        // Workaround for select no handling integers
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Must select a Budget Item")]
        public string SelectedBudgetItemId { get; set; }
    }
}
