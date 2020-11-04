using BudgetPlannerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Interfaces
{
    public interface IBudgetPlannerDataService<T> where T : class
    {
        Task<IEnumerable<T>> Get(bool includeRelated = false);
        Task<T> Get(int id, bool includeRelated = false);
    }

    public interface IBudgetItemTypesDataService : IBudgetPlannerDataService<BudgetItemType>
    {
    }
}
