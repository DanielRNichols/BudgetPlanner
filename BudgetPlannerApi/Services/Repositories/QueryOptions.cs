using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services.Repositories
{
    public class BaseQueryOptions : IBaseQueryOptions
    {
        public string UserId { get; set; } = "";
        public bool? MarkedForDeletion { get; set; } = null;
        public bool IncludeRelated { get; set; } = false;
        public int Limit { get; set; } = 0;
        public int Skip { get; set; } = 0;
    }

    public class BudgetCategoriesQueryOptions : BaseQueryOptions, IBudgetCategoriesQueryOptions
    {
        public int BudgetGroupId { get; set; } = 0;
    }
    public class BudgetCycleItemsQueryOptions : BaseQueryOptions, IBudgetCycleItemsQueryOptions
    {
        public int BudgetCycleId { get; set; } = 0;
    }
    public class RegisterEntriesQueryOptions : BaseQueryOptions, IRegisterEntriesQueryOptions
    {
        public int RegisterId { get; set; } = 0;
        public int Status { get; set; } = -1;
    }
}
