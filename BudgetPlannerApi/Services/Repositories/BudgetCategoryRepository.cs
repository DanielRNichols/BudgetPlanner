using BudgetPlanner.Data;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
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
                return await base.ExecuteQuery(query, options);
            }

            return await base.Get(options);
        }

        public override async Task<BudgetCategory> GetById(int id, IBaseQueryOptions options = null)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.BudgetCategories.AsQueryable()
                    .Include(t => t.BudgetGroup)
                    .Include(i => i.BudgetItems);
                return await base.ExecuteQueryById(id, query, options);
            }

            return await base.GetById(id, options);
        }
    }
}
