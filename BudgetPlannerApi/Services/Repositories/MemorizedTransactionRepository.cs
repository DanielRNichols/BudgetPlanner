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
    public class MemorizedTransactionRepository : DbResourceRepository<MemorizedTransaction, BaseQueryOptions>, IMemorizedTransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public MemorizedTransactionRepository(ApplicationDbContext db) : base(db, db.MemorizedTransactions)
        {
            _db = db;
        }

        public override async Task<IList<MemorizedTransaction>> Get(BaseQueryOptions options)
        {
            bool includeRelated = options != null && options.IncludeRelated;
            if (includeRelated)
            {
                return await _db.MemorizedTransactions.Include(i => i.BudgetItem).ToListAsync();
            }

            return await base.Get(options);
        }

        public override async Task<MemorizedTransaction> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.MemorizedTransactions
                    .Include(i => i.BudgetItem)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id);
        }
    }
}
