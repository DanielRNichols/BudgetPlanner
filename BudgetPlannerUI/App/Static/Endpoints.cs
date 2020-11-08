using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:5001/";
        public static string ApiUrl = $"{BaseUrl}api/";
        public static string BudgetGroups = $"{BaseUrl}api/budgetgroups/";
        public static string BudgetCategories = $"{BaseUrl}api/budgetcategories/";
        public static string BudgetItems = $"{BaseUrl}api/budgetitems/";
        public static string BudgetCycles = $"{BaseUrl}api/budgetcycles/";
        public static string MemorizedTransactions = $"{BaseUrl}api/memorizedtransactions/";
    }
}
