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
    public class BudgetCycleRepository : DbResourceRepository<BudgetCycle, BaseQueryOptions>, IBudgetCycleRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetCycleRepository(ApplicationDbContext db) : base(db, db.BudgetCycles)
        {
            _db = db;
        }
        public override async Task<IList<BudgetCycle>> Get(BaseQueryOptions options)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.BudgetCycles.AsQueryable()
                    .Include(i => i.BudgetCycleItems);
                return await base.ExecuteQuery(query, options);
            }

            return await base.Get(options);
        }

        public override async Task<BudgetCycle> GetById(int id, IBaseQueryOptions options = null)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.BudgetCycles.AsQueryable()
                    .Include(i => i.BudgetCycleItems);

                return await base.ExecuteQueryById(id, query, options);
            }

            return await base.GetById(id, options);

        }
    }
}
