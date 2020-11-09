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
    public class BudgetCyclesDataServices : BudgetPlannerDataService<BudgetCycle>, IBudgetCyclesDataService
    {
        public BudgetCyclesDataServices(HttpClient httpClient) : base(httpClient, Endpoints.BudgetCycles)
        {
        }
    }
    public class BudgetCycleItemsDataServices : BudgetPlannerDataService<BudgetCycleItem>, IBudgetCycleItemsDataService
    {
        public BudgetCycleItemsDataServices(HttpClient httpClient) : base(httpClient, Endpoints.BudgetCycleItems)
        {
        }
    }
    public class MemorizedTransactionsDataServices : BudgetPlannerDataService<MemorizedTransaction>, IMemorizedTransactionsDataService
    {
        public MemorizedTransactionsDataServices(HttpClient httpClient) : base(httpClient, Endpoints.MemorizedTransactions)
        {
        }
    }
    public class RegistersDataServices : BudgetPlannerDataService<Register>, IRegistersDataService
    {
        public RegistersDataServices(HttpClient httpClient) : base(httpClient, Endpoints.Registers)
        {
        }
    }
    public class RegisterEntriesDataServices : BudgetPlannerDataService<RegisterEntry>, IRegisterEntriesDataService
    {
        public RegisterEntriesDataServices(HttpClient httpClient) : base(httpClient, Endpoints.RegisterEntries)
        {
        }
    }
}
