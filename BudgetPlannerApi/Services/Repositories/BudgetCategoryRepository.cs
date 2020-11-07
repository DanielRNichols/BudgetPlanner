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
    public class BudgetCategoryRepository : DbResourceRepository<BudgetCategory>, IBudgetCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetCategoryRepository(ApplicationDbContext db) : base(db, db.BudgetCategories)
        {
            _db = db;
        }
        public override async Task<IList<BudgetCategory>> Get(bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.BudgetCategories
                    .Include(t => t.BudgetGroup)
                    .Include(i => i.BudgetItems)
                    .ToListAsync();
            }

            return await base.Get(includeRelated);
        }

        public override async Task<BudgetCategory> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.BudgetCategories
                    .Include(t => t.BudgetGroup)
                    .Include(i => i.BudgetItems)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id, includeRelated);
        }
    }
}
