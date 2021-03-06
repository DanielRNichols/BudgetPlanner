﻿using BudgetPlannerApi.Data;
using BudgetPlannerApi.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IDbResourceControllerHelper<T,O> where T : class, IDbResource where O : class, IBaseQueryOptions
    {
        Task<ObjectResult> GetItems<D>(ControllerBase controller, IDbResourceRepository<T,O> repo, O options);
        Task<IActionResult> GetItem<D>(ControllerBase controller, IDbResourceRepository<T,O> repo, int id, IBaseQueryOptions options);
        Task<IActionResult> CreateItem<D>(ControllerBase controller, IDbResourceRepository<T,O> repo, D itemDTO);
        Task<IActionResult> UpdateItem<D>(ControllerBase controller, IDbResourceRepository<T,O> repo, int id, D itemDTO);
        Task<IActionResult> DeleteItem(ControllerBase controller, IDbResourceRepository<T,O> repo, int id);
    }

    public interface IBudgetGroupsControllerHelper : IDbResourceControllerHelper<BudgetGroup, BaseQueryOptions>
    {
    }

    public interface IBudgetCategoriesControllerHelper : IDbResourceControllerHelper<BudgetCategory, BudgetCategoriesQueryOptions>
    {
    }

    public interface IBudgetItemsControllerHelper : IDbResourceControllerHelper<BudgetItem, BaseQueryOptions>
    {
    }

    public interface IMemorizedTransactionsControllerHelper : IDbResourceControllerHelper<MemorizedTransaction, BaseQueryOptions>
    {
    }

    public interface IRegistersControllerHelper : IDbResourceControllerHelper<Register, BaseQueryOptions>
    {
        Task<IActionResult> Reconcile(ControllerBase controller, IRegisterRepository repo, int id);
        Task<IActionResult> Balance(ControllerBase controller, IRegisterRepository repo, int id);
    }

    public interface IRegisterEntriesControllerHelper : IDbResourceControllerHelper<RegisterEntry, RegisterEntriesQueryOptions>
    {
    }

    public interface IRegisterSplitEntriesControllerHelper : IDbResourceControllerHelper<RegisterSplitEntry, BaseQueryOptions>
    {
    }

    public interface IBudgetCyclesControllerHelper : IDbResourceControllerHelper<BudgetCycle, BaseQueryOptions>
    {
    }

    public interface IBudgetCycleItemsControllerHelper : IDbResourceControllerHelper<BudgetCycleItem, BudgetCycleItemsQueryOptions>
    {
    }

}
