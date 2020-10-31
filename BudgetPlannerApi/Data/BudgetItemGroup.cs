using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("BudgetItemGroups")]
    public class BudgetItemGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BudgetItemTypeId { get; set; }
        public virtual BudgetItemType BudgetItemType { get; set; }
    }
}
