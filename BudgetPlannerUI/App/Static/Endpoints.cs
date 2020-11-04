using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:44371/";
        public static string ApiUrl = $"{BaseUrl}api/";
        public static string BudgetItemTypes = $"{BaseUrl}api/budgetitemtypes/";
    }
}
