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
    public class RegistryEntryRepository : DbResourceRepository<RegistryEntry>, IRegistryEntryRepository
    {
        private readonly ApplicationDbContext _db;

        public RegistryEntryRepository(ApplicationDbContext db) : base(db, db.RegistryEntries)
        {
            _db = db;
        }

        public override async Task<IList<RegistryEntry>> Get(bool includeRelated = false)
        {
            if(includeRelated)
            {
                return await _db.RegistryEntries
                .Include(r => r.Registry)
                .Include(c => c.BudgetCycle)
                .Include(i => i.BudgetItem)
                .ToListAsync();
            }

            return await base.Get(includeRelated);

        }

        public override async Task<RegistryEntry> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.RegistryEntries
                .Include(r => r.Registry)
                .Include(c => c.BudgetCycle)
                .Include(i => i.BudgetItem)
                .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id, includeRelated);
        }
    }
}
