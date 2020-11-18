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
    public class BudgetGroupRepository : DbResourceRepository<BudgetGroup, BaseQueryOptions>, IBudgetGroupRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetGroupRepository(ApplicationDbContext db) : base(db, db.BudgetGroups)
        {
            _db = db;
        }
        public override async Task<IList<BudgetGroup>> Get(BaseQueryOptions options)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.BudgetGroups.AsQueryable()
                    .Include(g => g.BudgetCategories);
                return await base.ExecuteQuery(query, options);
            }

            return await base.Get(options);
        }

        public override async Task<BudgetGroup> GetById(int id, IBaseQueryOptions options = null)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.BudgetGroups.AsQueryable()
                    .Include(g => g.BudgetCategories);

                return await base.ExecuteQueryById(id, query, options);
            }

            return await base.GetById(id, options);
        }
    }
}
