using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("BudgetGroups")]
    public class BudgetGroup : IDbResource
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<BudgetCategory> BudgetCategories { get; set; }
    }
}
