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
    public class BudgetCycleRepository : IBudgetCycleRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetCycleRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(BudgetCycle entity)
        {
            await _db.AddAsync(entity);

            return await Save();
        }

        public async Task<bool> Delete(BudgetCycle entity)
        {
            _db.Remove(entity);

            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.BudgetCycles.AnyAsync(row => row.Id == id);
        }

        public async Task<IList<BudgetCycle>> GetAll()
        {
            var items = await _db.BudgetCycles.ToListAsync();
            //var items = await _db.BudgetCycles.Include(i => i.BudgetItem).ToListAsync();

            return items;
        }

        public async Task<BudgetCycle> GetById(int id)
        {
            var item = await _db.BudgetCycles.FindAsync(id);
            //Note if you have related items, do something like this
            //var item = await _db.BudgetCycles.Include(i => i.BudgetItem).FirstOrDefaultAsync(q => q.Id == id);

            return item;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Update(BudgetCycle entity)
        {
            _db.BudgetCycles.Update(entity);

            return await Save();
        }
    }
}
