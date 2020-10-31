using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("BudgetItems")]
    public class BudgetItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BudgetItemGroupId { get; set; }

        public virtual BudgetItemGroup BudgetItemGroup { get; set; }
    }
}
