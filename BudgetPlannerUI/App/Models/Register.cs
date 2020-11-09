using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class Register : IDbResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }

        public virtual IList<RegisterEntry> RegisterEntries { get; set; }
    }
}
