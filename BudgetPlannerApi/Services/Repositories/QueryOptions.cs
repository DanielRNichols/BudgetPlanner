﻿using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services.Repositories
{
    public class BaseQueryOptions : IBaseQueryOptions
    {
        public bool IncludeRelated { get; set; } = false;
    }

    public class BudgetCategoriesQueryOptions : BaseQueryOptions, IBudgetCategoriesQueryOptions
    {
        public int BudgetGroupId { get; set; } = 0;
    }
    public class BudgetCycleItemsQueryOptions : BaseQueryOptions, IBudgetCycleItemsQueryOptions
    {
        public int BudgetCycleId { get; set; } = 0;
    }
}
