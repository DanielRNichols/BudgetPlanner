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

        Task<bool> Create(T entity);
        Task<bool> Update(int id, T entity);
        Task<bool> Delete(int id);

    }

    public interface IBudgetGroupsDataService : IBudgetPlannerDataService<BudgetGroup>
    {
    }
    public interface IBudgetCategoriesDataService : IBudgetPlannerDataService<BudgetCategory>
    {
    }
    public interface IBudgetItemsDataService : IBudgetPlannerDataService<BudgetItem>
    {
    }
}
