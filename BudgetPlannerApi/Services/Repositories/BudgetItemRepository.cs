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
    public class BudgetItemRepository : DbResourceRepository<BudgetItem, BaseQueryOptions>, IBudgetItemRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetItemRepository(ApplicationDbContext db) : base(db, db.BudgetItems)
        {
            _db = db;
        }

        public override async Task<IList<BudgetItem>> Get(BaseQueryOptions options)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.BudgetItems.AsQueryable()
                    .Include(g => g.BudgetCategory)
                    .Include(i => i.BudgetCycleItems);
;
                return await base.ExecuteQuery(query, options);
            }

            return await base.Get(options);

        }

        public override async Task<BudgetItem> GetById(int id, IBaseQueryOptions options = null)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.BudgetItems.AsQueryable()
                    .Include(g => g.BudgetCategory)
                    .Include(i => i.BudgetCycleItems);

                return await base.ExecuteQueryById(id, query, options);
            }

            return await base.GetById(id, options);
        }
    }
}
