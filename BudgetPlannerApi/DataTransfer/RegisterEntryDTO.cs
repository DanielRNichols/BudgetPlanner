﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class RegisterEntryDTO
    {
        public int Id { get; set; }
        public int RegisterId { get; set; }
        public int BudgetCycleId { get; set; }
        public int BudgetItemId { get; set; }
        public int EntryNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CheckNumber { get; set; }
        public string Payee { get; set; }
        public string Memo { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public decimal DepositAmount { get; set; }

        // Note Status should be an enum
        public int Status { get; set; }
        public bool IsSplit { get; set; }

        public bool MarkedForDeletion { get; set; }

        public virtual RegisterDTO Register { get; set; }
        public virtual BudgetCycleDTO BudgetCycle { get; set; }
        public virtual BudgetItemDTO BudgetItem { get; set; }
    }
    public class RegisterEntryCreateDTO
    {
        [Required]
        public int RegisterId { get; set; }
        public int BudgetCycleId { get; set; }
        public int BudgetItemId { get; set; }
        [Required]
        public int EntryNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public int CheckNumber { get; set; }
        [Required]
        public string Payee { get; set; }
        public string Memo { get; set; }
        public decimal WithdrawalAmount { get; set; }
        public decimal DepositAmount { get; set; }

        // Note Status should be an enum
        public int Status { get; set; }
        public bool IsSplit { get; set; }
        public bool MarkedForDeletion { get; set; }
    }
}
