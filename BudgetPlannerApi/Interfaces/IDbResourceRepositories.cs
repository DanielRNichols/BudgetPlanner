using BudgetPlannerApi.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IDbResourceRepository<T> where T : IDbResource
    {
        Task<IList<T>> Get(bool includeRelated = false);
        Task<T> GetById(int id, bool includeRelated = false);
        Task<bool> Exists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();

    }

    public interface IBudgetItemTypeRepository : IDbResourceRepository<BudgetItemType>
    {
    }
    public interface IBudgetItemGroupRepository : IDbResourceRepository<BudgetItemGroup>
    {
    }
    public interface IBudgetItemRepository : IDbResourceRepository<BudgetItem>
    {
    }
    public interface IMemorizedTransactionRepository : IDbResourceRepository<MemorizedTransaction>
    {
    }
    public interface IRegisterRepository : IDbResourceRepository<Register>
    {
    }
    public interface IRegisterEntryRepository : IDbResourceRepository<RegisterEntry>
    {
    }
    public interface IRegisterSplitEntryRepository : IDbResourceRepository<RegisterSplitEntry>
    {
    }
    public interface IBudgetCycleRepository : IDbResourceRepository<BudgetCycle>
    {
    }

    public interface IBudgetCycleItemRepository : IDbResourceRepository<BudgetCycleItem>
    {
    }

}
