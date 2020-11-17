using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class MemorizedTransactionDTO
    {
        public int Id { get; set; }
        public string Payee { get; set; }

        public decimal Amount { get; set; }
        public int BudgetItemId { get; set; }
        public bool MarkedForDeletion { get; set; }

        public virtual BudgetItemDTO BudgetItem { get; set; }
    }

    public class MemorizedTransactionCreateDTO
    {
        [Required]
        public string Payee { get; set; }

        public decimal Amount { get; set; }

        [Required]
        public int BudgetItemId { get; set; }

        public bool MarkedForDeletion { get; set; }
    }

}
