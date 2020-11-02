using BudgetPlannerApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IDbResourceRepository<T> where T : IDbResource
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
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
    public interface IRegistryRepository : IDbResourceRepository<Registry>
    {
    }
    public interface IBudgetCycleRepository : IDbResourceRepository<BudgetCycle>
    {
    }

}
