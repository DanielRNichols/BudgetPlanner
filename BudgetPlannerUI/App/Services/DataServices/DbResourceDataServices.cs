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
    public class BudgetItemTypesDataServices : BudgetPlannerDataService<BudgetItemType>, IBudgetItemTypesDataService
    {
        public BudgetItemTypesDataServices(HttpClient httpClient) : base(httpClient, Endpoints.BudgetItemTypes)
        {
        }
    }
    public class BudgetItemGroupsDataServices : BudgetPlannerDataService<BudgetItemGroup>, IBudgetItemGroupsDataService
    {
        public BudgetItemGroupsDataServices(HttpClient httpClient) : base(httpClient, Endpoints.BudgetItemGroups)
        {
        }
    }
}
