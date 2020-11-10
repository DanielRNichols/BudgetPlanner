using BudgetPlannerApi.Data;
using BudgetPlannerApi.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IDbResourceRepository<T,O> where T : IDbResource where O: IBaseQueryOptions
    {
        Task<IList<T>> Get(O options);
        Task<T> GetById(int id, bool includeRelated);
        Task<bool> Exists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();

    }

    public interface IBudgetGroupRepository : IDbResourceRepository<BudgetGroup, BaseQueryOptions>
    {
    }
    public interface IBudgetCategoryRepository : IDbResourceRepository<BudgetCategory, BudgetCategoriesQueryOptions>
    {
    }
    public interface IBudgetItemRepository : IDbResourceRepository<BudgetItem, BaseQueryOptions>
    {
    }
    public interface IMemorizedTransactionRepository : IDbResourceRepository<MemorizedTransaction, BaseQueryOptions>
    {
    }
    public interface IRegisterRepository : IDbResourceRepository<Register, BaseQueryOptions>
    {
    }
    public interface IRegisterEntryRepository : IDbResourceRepository<RegisterEntry, RegisterEntriesQueryOptions>
    {
    }
    public interface IRegisterSplitEntryRepository : IDbResourceRepository<RegisterSplitEntry, BaseQueryOptions>
    {
    }
    public interface IBudgetCycleRepository : IDbResourceRepository<BudgetCycle, BaseQueryOptions>
    {
    }

    public interface IBudgetCycleItemRepository : IDbResourceRepository<BudgetCycleItem, BudgetCycleItemsQueryOptions>
    {
    }

}
