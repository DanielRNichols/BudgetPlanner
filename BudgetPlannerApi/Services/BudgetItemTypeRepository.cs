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
    public class BudgetItemTypeRepository : IBudgetItemTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetItemTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(BudgetItemType entity)
        {
            await _db.AddAsync(entity);

            return await Save();
        }

        public async Task<bool> Delete(BudgetItemType entity)
        {
            _db.Remove(entity);

            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.BudgetItemTypes.AnyAsync(row => row.Id == id);
        }

        public async Task<IList<BudgetItemType>> GetAll()
        {
            var itemTypes = await _db.BudgetItemTypes.ToListAsync();

            //Note if you have related items, do something like this
            // var authors = await _db.Authors.Include(a => a.Books).ToListAsync();

            return itemTypes;
        }

        public async Task<BudgetItemType> GetById(int id)
        {
            var itemType = await _db.BudgetItemTypes.FindAsync(id);
            // Note if you have related items, do something like this
            // var author = await _db.Authors.Include(a => a.Books).FirstOrDefaultAsync(q => q.Id == id);

            return itemType;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Update(BudgetItemType entity)
        {
            _db.BudgetItemTypes.Update(entity);

            return await Save();
        }
    }
}
