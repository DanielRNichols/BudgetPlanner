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
    public class BudgetCategoryRepository : DbResourceRepository<BudgetCategory, BudgetCategoriesQueryOptions>, IBudgetCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetCategoryRepository(ApplicationDbContext db) : base(db, db.BudgetCategories)
        {
            _db = db;
        }
        public override async Task<IList<BudgetCategory>> Get(BudgetCategoriesQueryOptions options)
        {
            if (options != null)
            {
                var query = _db.BudgetCategories.AsQueryable();
                if (options.BudgetGroupId > 0)
                {
                    query = query.Where(r => r.BudgetGroupId == options.BudgetGroupId);
                }

                if (options.IncludeRelated)
                {
                    query = query
                        .Include(t => t.BudgetGroup)
                        .Include(i => i.BudgetItems);
                }
                return await query.ToListAsync();
            }

            return await base.Get(options);
        }

        public override async Task<BudgetCategory> GetById(int id, BudgetCategoriesQueryOptions options)
        {
            bool includeRelated = options != null && options.IncludeRelated;
            if (includeRelated)
            {
                return await _db.BudgetCategories
                    .Include(t => t.BudgetGroup)
                    .Include(i => i.BudgetItems)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id, options);
        }
    }
}
