using BudgetPlannerUI.Static;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Services
{
    public class BudgetItemTypesDataService : BudgetPlannerDataService<BudgetItemType>, IBudgetItemTypesDataService
    {
        public BudgetItemTypesDataService(HttpClient httpClient) : base(httpClient, Endpoints.BudgetItemTypes)
        {
        }
    }
}
