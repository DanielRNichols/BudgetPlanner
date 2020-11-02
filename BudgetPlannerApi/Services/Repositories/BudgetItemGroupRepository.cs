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
    public class BudgetItemGroupRepository : DbResourceRepository<BudgetItemGroup>, IBudgetItemGroupRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetItemGroupRepository(ApplicationDbContext db) : base(db, db.BudgetItemGroups)
        {
            _db = db;
        }
        public override async Task<IList<BudgetItemGroup>> Get(bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.BudgetItemGroups.Include(t => t.BudgetItemType).ToListAsync();
            }

            return await base.Get(includeRelated);
        }

        public override async Task<BudgetItemGroup> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.BudgetItemGroups
                    .Include(t => t.BudgetItemType)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id, includeRelated);
        }
    }
}
