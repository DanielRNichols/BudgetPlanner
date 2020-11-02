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
    public class BudgetCycleRepository : DbResourceRepository<BudgetCycle>, IBudgetCycleRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetCycleRepository(ApplicationDbContext db) : base(db, db.BudgetCycles)
        {
            _db = db;
        }
        public override async Task<IList<BudgetCycle>> Get(bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.BudgetCycles.Include(i => i.BudgetCycleItems).ToListAsync();
            }

            return await base.Get(includeRelated);
        }

        public override async Task<BudgetCycle> GetById(int id, bool includeRelated = false)
        {
            if(includeRelated)
            {
                return await _db.BudgetCycles
                    .Include(i => i.BudgetCycleItems)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id, includeRelated);

        }
    }
}
