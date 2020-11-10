using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Services
{
    public static class Formatters
    {
        public static string FormatDate(DateTime dt)
        {
            return String.Format("{0:MM/dd/yyyy}", dt);
        }

        public static string FormatCurrency(decimal amount)
        {
            return String.Format("{0:0.00}", amount);
        }

        public static string FormatCheckNumber(int checkNo)
        {
            return checkNo <= 1 ? "" : checkNo.ToString();
        }
    }
}
