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
    public class MemorizedTransactionRepository : IMemorizedTransactionRepository
    {
        private readonly ApplicationDbContext _db;

        public MemorizedTransactionRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(MemorizedTransaction entity)
        {
            await _db.AddAsync(entity);

            return await Save();
        }

        public async Task<bool> Delete(MemorizedTransaction entity)
        {
            _db.Remove(entity);

            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.MemorizedTransactions.AnyAsync(row => row.Id == id);
        }

        public async Task<IList<MemorizedTransaction>> GetAll()
        {
            //var items = await _db.MemorizedTransactions.ToListAsync();
            var items = await _db.MemorizedTransactions.Include(i => i.BudgetItem).ToListAsync();

            return items;
        }

        public async Task<MemorizedTransaction> GetById(int id)
        {
            //var item = await _db.MemorizedTransactions.FindAsync(id);
            //Note if you have related items, do something like this
            var item = await _db.MemorizedTransactions.Include(i => i.BudgetItem).FirstOrDefaultAsync(q => q.Id == id);

            return item;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Update(MemorizedTransaction entity)
        {
            _db.MemorizedTransactions.Update(entity);

            return await Save();
        }
    }
}
