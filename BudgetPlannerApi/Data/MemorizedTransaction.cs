using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("MemorizedTransactions")]
    public class MemorizedTransaction : IDbResource
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Payee { get; set; }

        [Column(TypeName ="money")]
        public decimal Amount { get; set; }
        public int BudgetItemId { get; set; }
        public bool MarkedForDeletion { get; set; }

        public virtual BudgetItem BudgetItem { get; set; }

    }
}
