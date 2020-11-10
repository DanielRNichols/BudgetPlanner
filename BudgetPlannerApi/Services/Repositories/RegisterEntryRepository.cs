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
    public class RegisterEntryRepository : DbResourceRepository<RegisterEntry, RegisterEntriesQueryOptions>, IRegisterEntryRepository
    {
        private readonly ApplicationDbContext _db;

        public RegisterEntryRepository(ApplicationDbContext db) : base(db, db.RegisterEntries)
        {
            _db = db;
        }

        public override async Task<IList<RegisterEntry>> Get(RegisterEntriesQueryOptions options)
        {
            if (options != null)
            {
                var query = _db.RegisterEntries.AsQueryable();
                if (options.RegisterId > 0)
                {
                    query = query.Where(r => r.RegisterId == options.RegisterId);
                }
                if (options.IncludeRelated)
                {
                    query = query
                        .Include(r => r.Register)
                        .Include(c => c.BudgetCycle)
                        .Include(i => i.BudgetItem);
                }
                return await base.ExecuteQuery(query, options);
            }

            return await base.Get();

        }

        public override async Task<RegisterEntry> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.RegisterEntries
                .Include(r => r.Register)
                .Include(c => c.BudgetCycle)
                .Include(i => i.BudgetItem)
                .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id);
        }
    }
}
