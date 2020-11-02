using BudgetPlanner.Data;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services
{
    public class MemorizedTransactionRepository : DbResourceRepository<MemorizedTransaction>, IMemorizedTransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public MemorizedTransactionRepository(ApplicationDbContext db) : base(db, db.MemorizedTransactions)
        {
            _db = db;
        }

        public override async Task<IList<MemorizedTransaction>> GetAll()
        {
            var items = await _db.MemorizedTransactions.Include(i => i.BudgetItem).ToListAsync();

            return items;
        }

        public override async Task<MemorizedTransaction> GetById(int id)
        {
            var item = await _db.MemorizedTransactions.Include(i => i.BudgetItem).FirstOrDefaultAsync(q => q.Id == id);

            return item;
        }
    }
}
