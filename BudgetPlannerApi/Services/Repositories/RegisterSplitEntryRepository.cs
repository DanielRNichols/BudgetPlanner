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
    public class RegisterSplitEntryRepository : DbResourceRepository<RegisterSplitEntry, BaseQueryOptions>, IRegisterSplitEntryRepository
    {
        private readonly ApplicationDbContext _db;

        public RegisterSplitEntryRepository(ApplicationDbContext db) : base(db, db.RegisterSplitEntries)
        {
            _db = db;
        }

        public override async Task<IList<RegisterSplitEntry>> Get(BaseQueryOptions options)
        {
            bool includeRelated = options != null && options.IncludeRelated;
            if (includeRelated)
            {
                return await _db.RegisterSplitEntries
                .Include(r => r.RegisterEntry)
                .Include(i => i.BudgetItem)
                .ToListAsync();
            }

            return await base.Get(options);

        }

        public override async Task<RegisterSplitEntry> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.RegisterSplitEntries
                .Include(r => r.RegisterEntry)
                .Include(i => i.BudgetItem)
                .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id);
        }
    }
}
