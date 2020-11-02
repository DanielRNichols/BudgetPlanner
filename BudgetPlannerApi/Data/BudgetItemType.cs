using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("BudgetItemTypes")]
    public class BudgetItemType : IDbResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsExpense { get; set; }

        public virtual IList<BudgetItemGroup> BudgetItemGroups { get; set; }
    }
}
