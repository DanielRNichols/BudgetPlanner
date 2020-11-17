using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("RegisterSplitEntries")]
    public class RegisterSplitEntry : IDbResource
    {
        public int Id { get; set; }
        public int RegisterEntryId { get; set; }
        public int BudgetItemId { get; set; }
        public string Payee { get; set; }
        public string Memo { get; set; }

        [Column(TypeName = "money")]
        public decimal WithdrawalAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal DepositAmount { get; set; }
        public bool MarkedForDeletion { get; set; }

        public virtual RegisterEntry RegisterEntry { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
    }
}
