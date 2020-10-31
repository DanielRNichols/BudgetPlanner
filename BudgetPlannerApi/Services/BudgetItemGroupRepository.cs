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
    public class BudgetItemGroupRepository : IBudgetItemGroupRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetItemGroupRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(BudgetItemGroup entity)
        {
            await _db.AddAsync(entity);

            return await Save();
        }

        public async Task<bool> Delete(BudgetItemGroup entity)
        {
            _db.Remove(entity);

            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.BudgetItemGroups.AnyAsync(row => row.Id == id);
        }

        public async Task<IList<BudgetItemGroup>> GetAll()
        {
            //var items = await _db.BudgetItemGroups.ToListAsync();
            var items = await _db.BudgetItemGroups.Include(t => t.BudgetItemType).ToListAsync();

            return items;
        }

        public async Task<BudgetItemGroup> GetById(int id)
        {
            //var item = await _db.BudgetItemGroups.FindAsync(id);
            //Note if you have related items, do something like this
            var item = await _db.BudgetItemGroups.Include(t => t.BudgetItemType).FirstOrDefaultAsync(q => q.Id == id);

            return item;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Update(BudgetItemGroup entity)
        {
            _db.BudgetItemGroups.Update(entity);

            return await Save();
        }
    }
}
