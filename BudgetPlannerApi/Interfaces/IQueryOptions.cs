﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IBaseQueryOptions
    {
        string UserId { get; set; }
        bool IncludeRelated { get; set; }
        bool? MarkedForDeletion { get; set; }
        int Limit { get; set; }
        int Skip {get; set; }
    }

    public interface IBudgetCategoriesQueryOptions : IBaseQueryOptions
    {
        int BudgetGroupId { get; set; }
    }
    public interface IBudgetCycleItemsQueryOptions : IBaseQueryOptions
    {
        int BudgetCycleId { get; set; }
    }
    public interface IRegisterEntriesQueryOptions : IBaseQueryOptions
    {
        int RegisterId { get; set; }
        int Status { get; set; }
    }
}
