﻿using BudgetPlannerUI.Static;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Services
{
    public class BudgetGroupsDataServices : BudgetPlannerDataService<BudgetGroup>, IBudgetGroupsDataService
    {
        public BudgetGroupsDataServices(HttpClient httpClient) : base(httpClient, Endpoints.BudgetGroups)
        {
        }
    }
    public class BudgetCategoriesDataServices : BudgetPlannerDataService<BudgetCategory>, IBudgetCategoriesDataService
    {
        public BudgetCategoriesDataServices(HttpClient httpClient) : base(httpClient, Endpoints.BudgetCategories)
        {
        }
    }
    public class BudgetItemsDataServices : BudgetPlannerDataService<BudgetItem>, IBudgetItemsDataService
    {
        public BudgetItemsDataServices(HttpClient httpClient) : base(httpClient, Endpoints.BudgetItems)
        {
        }
    }
}
