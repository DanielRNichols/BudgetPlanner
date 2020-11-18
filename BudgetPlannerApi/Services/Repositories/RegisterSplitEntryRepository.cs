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
            if (options != null && options.IncludeRelated)
            {
                var query = _db.RegisterSplitEntries.AsQueryable()
                    .Include(r => r.RegisterEntry)
                    .Include(i => i.BudgetItem);

                return await base.ExecuteQuery(query, options);
            }

            return await base.Get(options);

        }

        public override async Task<RegisterSplitEntry> GetById(int id, IBaseQueryOptions options = null)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.RegisterSplitEntries.AsQueryable()
                        .Include(r => r.RegisterEntry)
                        .Include(i => i.BudgetItem);

                return await base.ExecuteQueryById(id, query, options);
            }

            return await base.GetById(id, options);
        }
    }
}
