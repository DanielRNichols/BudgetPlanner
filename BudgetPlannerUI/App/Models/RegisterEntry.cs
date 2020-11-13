using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{

    public enum RegisterEntryAction
    {
        IncrementDate,
        DecrementDate,
        NextStatus,
        Create,
        Modify,
        Delete
    }

    public enum RegisterEntryStatus
    {
        Outstanding = 0,
        Pending = 1,
        Cleared = 2,
        Reconciled = 3
    }

    public class RegisterEntry : IDbResource
    {
        public int Id { get; set; }
        public int RegisterId { get; set; }
        public int BudgetCycleId { get; set; }
        public int BudgetItemId { get; set; }
        public int EntryNumber { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public int CheckNumber { get; set; }
        public string Payee { get; set; }
        public string Memo { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public int Status { get; set; }
        public bool IsSplit { get; set; }

        public virtual Register Register { get; set; }
        public virtual BudgetCycle BudgetCycle { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }

        // Workaround for select no handling integers
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Must select a Register")]
        public string SelectedRegisterId { get; set; }

        // Workaround for select no handling integers
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Must select a Budget Cycle")]
        public string SelectedBudgetCycleId { get; set; }

        // Workaround for select no handling integers
        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Must select a Budget Item")]
        public string SelectedBudgetItemId { get; set; }


        // UI Properties
        public bool RowExpanded { get; set; } = false;
        public bool ConfirmDelete { get; set; } = false;

        // Calculated Properties

        public decimal NetTotal
        {
            get
            {
                return DepositAmount - WithdrawalAmount;
            }
        }


        // Methods

        public RegisterEntryStatus GetStatus()
        {
            RegisterEntryStatus status = RegisterEntryStatus.Outstanding;
            switch (Status)
            {
                case 1:
                    status = RegisterEntryStatus.Pending;
                    break;
                case 2:
                    status = RegisterEntryStatus.Cleared;
                    break;
                case 3:
                    status = RegisterEntryStatus.Reconciled;
                    break;
                default:
                    break;
            }

            return status;
        }
        public RegisterEntryStatus NextStatus()
        {
            RegisterEntryStatus currStatus = GetStatus();
            RegisterEntryStatus status = RegisterEntryStatus.Outstanding;
            switch (currStatus)
            {
                case RegisterEntryStatus.Outstanding:
                    status = RegisterEntryStatus.Pending;
                    break;
                case RegisterEntryStatus.Pending:
                    status = RegisterEntryStatus.Cleared;
                    break;
                case RegisterEntryStatus.Cleared:
                    status = RegisterEntryStatus.Reconciled;
                    break;
                case RegisterEntryStatus.Reconciled:
                    status = RegisterEntryStatus.Outstanding;
                    break;
                default:
                    break;
            }

            return status;
        }

        public override string ToString()
        {
            RegisterEntryStatus status = RegisterEntryStatus.Outstanding;
        
            return status.ToString();
        }

    }
}
