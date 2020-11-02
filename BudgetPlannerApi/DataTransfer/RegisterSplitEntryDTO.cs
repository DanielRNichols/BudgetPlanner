using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class RegisterSplitEntryDTO
    {
        public int Id { get; set; }
        public int RegisterEntryId { get; set; }
        public int BudgetItemId { get; set; }
        public string Payee { get; set; }
        public string Memo { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public decimal DepositAmount { get; set; }

        public virtual RegisterEntryDTO RegisterEntry { get; set; }
        public virtual BudgetItemDTO BudgetItem { get; set; }

    }
    public class RegisterSplitEntryCreateDTO
    {
        public int Id { get; set; }
        [Required]
        public int RegisterEntryId { get; set; }
        public int BudgetItemId { get; set; }
        public string Payee { get; set; }
        public string Memo { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public decimal DepositAmount { get; set; }

    }
}
