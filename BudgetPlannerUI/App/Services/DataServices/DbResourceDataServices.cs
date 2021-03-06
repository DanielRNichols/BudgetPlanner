﻿using BudgetPlannerUI.Static;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace BudgetPlannerUI.Services
{
    public class BudgetGroupsDataServices : BudgetPlannerDataService<BudgetGroup>, IBudgetGroupsDataService
    {
        public BudgetGroupsDataServices(HttpClient httpClient, ILocalStorageService localStorageService) 
            : base(httpClient, localStorageService, Endpoints.BudgetGroups)
        {
        }
    }
    public class BudgetCategoriesDataServices : BudgetPlannerDataService<BudgetCategory>, IBudgetCategoriesDataService
    {
        public BudgetCategoriesDataServices(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService, Endpoints.BudgetCategories)
        {
        }
    }
    public class BudgetItemsDataServices : BudgetPlannerDataService<BudgetItem>, IBudgetItemsDataService
    {
        public BudgetItemsDataServices(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService, Endpoints.BudgetItems)
        {
        }
    }
    public class BudgetCyclesDataServices : BudgetPlannerDataService<BudgetCycle>, IBudgetCyclesDataService
    {
        public BudgetCyclesDataServices(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService, Endpoints.BudgetCycles)
        {
        }
    }
    public class BudgetCycleItemsDataServices : BudgetPlannerDataService<BudgetCycleItem>, IBudgetCycleItemsDataService
    {
        public BudgetCycleItemsDataServices(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService, Endpoints.BudgetCycleItems)
        {
        }
    }
    public class MemorizedTransactionsDataServices : BudgetPlannerDataService<MemorizedTransaction>, IMemorizedTransactionsDataService
    {
        public MemorizedTransactionsDataServices(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService, Endpoints.MemorizedTransactions)
        {
        }
    }
    public class RegistersDataServices : BudgetPlannerDataService<Register>, IRegistersDataService
    {
        public RegistersDataServices(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService, Endpoints.Registers)
        {
        }

        public async Task<bool> Reconcile(int id)
        { 
            //_httpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("bearer", await GetBearerToken());

            var response = await _httpClient.PostAsync($"{_resourceUrl}{id}/reconcile", null);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;

        }

    }
    public class RegisterEntriesDataServices : BudgetPlannerDataService<RegisterEntry>, IRegisterEntriesDataService
    {
        public RegisterEntriesDataServices(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService, Endpoints.RegisterEntries)
        {
        }
    }
}
