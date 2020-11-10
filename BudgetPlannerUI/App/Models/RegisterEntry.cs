﻿using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
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
        public DateTime TransactionDate { get; set; }
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

        public override string ToString()
        {
            RegisterEntryStatus status = RegisterEntryStatus.Outstanding;
        
            return status.ToString();
        }

    }
}