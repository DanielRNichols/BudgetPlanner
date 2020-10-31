using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("MemorizedTransactions")]
    public class MemorizedTransaction
    {
        public int Id { get; set; }
        public string Payee { get; set; }

        [Column(TypeName ="money")]
        public decimal Amount { get; set; }
        public int BudgetItemId { get; set; }

        public virtual BudgetItem BudgetItem { get; set; }

    }
}
