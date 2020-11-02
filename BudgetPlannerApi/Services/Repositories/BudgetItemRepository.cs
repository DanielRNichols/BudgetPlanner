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
    public class BudgetItemRepository : DbResourceRepository<BudgetItem>, IBudgetItemRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetItemRepository(ApplicationDbContext db) : base(db, db.BudgetItems)
        {
            _db = db;
        }

        public override async Task<IList<BudgetItem>> GetAll()
        {
            var items = await _db.BudgetItems.Include(g => g.BudgetItemGroup).ToListAsync();

            return items;
        }

        public override async Task<BudgetItem> GetById(int id)
        {
            var item = await _db.BudgetItems.Include(g => g.BudgetItemGroup).FirstOrDefaultAsync(q => q.Id == id);

            return item;
        }
    }
}
