using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetPlannerUI.Models;

namespace BudgetPlannerUI.Services
{
    public static class Formatters
    {
        public static string FormatDate(DateTime dt)
        {
            return $"{dt:MM/dd/yy}";
        }

        public static string FormatCurrency(decimal amount, bool showZero = true)
        {
            if (showZero || Math.Abs(amount) > (decimal)0.001)  
                return $"{amount:0.00}";

            return "";
        }

        public static string FormatCheckNumber(int checkNo)
        {
            return checkNo <= 1 ? "" : checkNo.ToString();
        }

        public static string FormatRegisterEntryStatus(RegisterEntryStatus status)
        {
            switch (status)
            {
                case RegisterEntryStatus.Pending:
                    return "p";
                case RegisterEntryStatus.Cleared:
                    return "c";
                case RegisterEntryStatus.Reconciled:
                    return "R";
                default:
                    return "o";
            }
        }
    }
}
