using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("BudgetCategories")]
    public class BudgetCategory : IDbResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BudgetGroupId { get; set; }
        public bool MarkedForDeletion { get; set; }

        public virtual BudgetGroup BudgetGroup { get; set; }

        public virtual IList<BudgetItem> BudgetItems { get; set; }
    }
}
