using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IBaseQueryOptions
    {
        bool IncludeRelated { get; set; }
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
}
