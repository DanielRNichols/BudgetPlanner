using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("RegisterEntries")]
    public class RegisterEntry : IDbResource
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int RegisterId { get; set; }
        public int BudgetCycleId { get; set; }
        public int BudgetItemId { get; set; }
        public int EntryNumber { get; set; }
        [Column(TypeName ="Date")]
        public DateTime TransactionDate { get; set; }
        public int CheckNumber { get; set; }
        public string Payee { get; set; }
        public string Memo { get; set; }
        [Column(TypeName = "money")]
        public decimal WithdrawalAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal DepositAmount { get; set; }

        // Note Status should be an enum
        public int Status { get; set; }
        public bool IsSplit { get; set; }
        public bool MarkedForDeletion { get; set; }

        public virtual Register Register { get; set; }
        public virtual BudgetCycle BudgetCycle { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
    }
}
