using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("BudgetItems")]
    public class BudgetItem : IDbResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsIncome { get; set; }
        public int BudgetCategoryId { get; set; }

        public virtual BudgetCategory BudgetCategory { get; set; }

        public virtual IList<BudgetCycleItem> BudgetCycleItems { get; set; }

    }
}
