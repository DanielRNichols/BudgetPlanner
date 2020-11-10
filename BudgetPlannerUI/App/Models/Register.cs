using BudgetPlannerUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class Register : IDbResource
    {
        // Database columns
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }

        // Related
        public virtual IList<RegisterEntry> RegisterEntries { get; set; }

        // Calculated
        public decimal ClearedBalance { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal EndingBalance { get; set; }
        public decimal ClearedNet { get; set; }
        public decimal OutstandingNet { get; set; }
        public decimal PendingNet { get; set; }

        public void Balance ()
        {
            InitializeRegister();
            if (RegisterEntries == null)
                return;

            foreach(var entry in RegisterEntries)
            {
                RegisterEntryStatus status = entry.GetStatus();
                switch (status)
                {
                    case RegisterEntryStatus.Cleared:
                    case RegisterEntryStatus.Reconciled:
                        ClearedNet += entry.NetTotal;
                        break;

                    case RegisterEntryStatus.Pending:
                        PendingNet += entry.NetTotal;
                        break;

                    default:
                        OutstandingNet += entry.NetTotal;
                        break;
                }
            }
            ClearedBalance = StartingBalance + ClearedNet;
            AvailableBalance = ClearedBalance + PendingNet;
            EndingBalance = AvailableBalance + OutstandingNet;
        }

        private void InitializeRegister()
        {
            ClearedBalance = StartingBalance;
            AvailableBalance = StartingBalance;
            EndingBalance = StartingBalance;
            OutstandingNet = 0;
            PendingNet = 0;
            ClearedNet = 0;

            return;
        }
    }
}
