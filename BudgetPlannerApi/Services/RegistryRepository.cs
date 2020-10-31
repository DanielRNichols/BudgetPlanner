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
    public class RegistryRepository : IRegistryRepository
    {
        private readonly ApplicationDbContext _db;

        public RegistryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Registry entity)
        {
            await _db.AddAsync(entity);

            return await Save();
        }

        public async Task<bool> Delete(Registry entity)
        {
            _db.Remove(entity);

            return await Save();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Registries.AnyAsync(row => row.Id == id);
        }

        public async Task<IList<Registry>> GetAll()
        {
            var items = await _db.Registries.ToListAsync();
            //var items = await _db.Registries.Include(i => i.BudgetItem).ToListAsync();

            return items;
        }

        public async Task<Registry> GetById(int id)
        {
            var item = await _db.Registries.FindAsync(id);
            //Note if you have related items, do something like this
            //var item = await _db.Registrys.Include(i => i.BudgetItem).FirstOrDefaultAsync(q => q.Id == id);

            return item;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }

        public async Task<bool> Update(Registry entity)
        {
            _db.Registries.Update(entity);

            return await Save();
        }
    }
}
