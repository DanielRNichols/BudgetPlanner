using BudgetPlannerApi.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IDbResourceControllerHelper<T> where T : class, IDbResource
    {
        Task<ObjectResult> GetItems<D>(ControllerBase controller, IDbResourceRepository<T> repo, bool includeRelated);
        Task<IActionResult> GetItem<D>(ControllerBase controller, IDbResourceRepository<T> repo, int id, bool includeRelated);
        Task<IActionResult> CreateItem<D>(ControllerBase controller, IDbResourceRepository<T> repo, D itemDTO);
        Task<IActionResult> UpdateItem<D>(ControllerBase controller, IDbResourceRepository<T> repo, int id, D itemDTO);
        Task<IActionResult> DeleteItem(ControllerBase controller, IDbResourceRepository<T> repo, int id);
    }

    public interface IBudgetItemTypesControllerHelper : IDbResourceControllerHelper<BudgetItemType>
    {
    }

    public interface IBudgetItemGroupsControllerHelper : IDbResourceControllerHelper<BudgetItemGroup>
    {
    }

    public interface IBudgetItemsControllerHelper : IDbResourceControllerHelper<BudgetItem>
    {
    }

    public interface IMemorizedTransactionsControllerHelper : IDbResourceControllerHelper<MemorizedTransaction>
    {
    }

    public interface IRegistersControllerHelper : IDbResourceControllerHelper<Register>
    {
    }

    public interface IRegisterEntriesControllerHelper : IDbResourceControllerHelper<RegisterEntry>
    {
    }

    public interface IRegisterSplitEntriesControllerHelper : IDbResourceControllerHelper<RegisterSplitEntry>
    {
    }

    public interface IBudgetCyclesControllerHelper : IDbResourceControllerHelper<BudgetCycle>
    {
    }

    public interface IBudgetCycleItemsControllerHelper : IDbResourceControllerHelper<BudgetCycleItem>
    {
    }

}
