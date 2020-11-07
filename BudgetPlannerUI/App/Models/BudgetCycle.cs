using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class BudgetCycle : IDbResource
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime EndDate { get; set; } = DateTime.Now;
        public decimal  StartingBalance { get; set; }

    }
}
