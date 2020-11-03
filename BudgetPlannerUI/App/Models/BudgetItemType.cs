using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class BudgetItemType : IDbResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsExpense { get; set; }

        //public virtual IList<BudgetItemGroup> BudgetItemGroups { get; set; }
    }
}
