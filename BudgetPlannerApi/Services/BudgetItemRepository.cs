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
    public class BudgetItemRepository : IBudgetItemRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(BudgetItem entity)
        {
            await _db.AddAsync(entity);

            return await Save();
        }

        public async Task<bool> Delete(BudgetItem entity)
        {
            _db.Remove(entity);

            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.BudgetItems.AnyAsync(row => row.Id == id);
        }

        public async Task<IList<BudgetItem>> GetAll()
        {
            //var items = await _db.BudgetItems.ToListAsync();
            var items = await _db.BudgetItems.Include(g => g.BudgetItemGroup).ToListAsync();

            return items;
        }

        public async Task<BudgetItem> GetById(int id)
        {
            //var item = await _db.BudgetItems.FindAsync(id);
            //Note if you have related items, do something like this
            var item = await _db.BudgetItems.Include(g => g.BudgetItemGroup).FirstOrDefaultAsync(q => q.Id == id);

            return item;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Update(BudgetItem entity)
        {
            _db.BudgetItems.Update(entity);

            return await Save();
        }
    }
}
