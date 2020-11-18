using BudgetPlanner.Data;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services.Repositories
{
    public class BudgetCycleItemRepository : 
                    DbResourceRepository<BudgetCycleItem, BudgetCycleItemsQueryOptions>, IBudgetCycleItemRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetCycleItemRepository(ApplicationDbContext db) : base(db, db.BudgetCyclesItems)
        {
            _db = db;
        }
        public override async Task<IList<BudgetCycleItem>> Get(BudgetCycleItemsQueryOptions options)
        {
            if(options != null)
            {
                var query = _db.BudgetCyclesItems.AsQueryable();
                if(options.BudgetCycleId > 0)
                {
                    query = query.Where(r => r.BudgetCycleId == options.BudgetCycleId);
                }
                if (options.IncludeRelated)
                {
                    query = query
                        .Include(c => c.BudgetCycle)
                        .Include(i => i.BudgetItem);
                }
                return await base.ExecuteQuery(query, options);
            }

            return await base.Get();

        }

        public override async Task<BudgetCycleItem> GetById(int id, IBaseQueryOptions options = null)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.BudgetCyclesItems.AsQueryable()
                        .Include(c => c.BudgetCycle)
                        .Include(i => i.BudgetItem);
                return await base.ExecuteQueryById(id, query, options);
            }

            return await base.GetById(id, options);
        }
    }
}
