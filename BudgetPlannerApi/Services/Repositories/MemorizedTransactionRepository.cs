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
            if (options != null && options.IncludeRelated)
            {
                var query = _db.MemorizedTransactions.AsQueryable()
                    .Include(i => i.BudgetItem);
                return await base.ExecuteQuery(query, options);
            }

            return await base.Get(options);
        }

        public override async Task<MemorizedTransaction> GetById(int id, IBaseQueryOptions options = null)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.MemorizedTransactions.AsQueryable()
                    .Include(i => i.BudgetItem);

                return await base.ExecuteQueryById(id, query, options);
            }

            return await base.GetById(id, options);
        }
    }
}
