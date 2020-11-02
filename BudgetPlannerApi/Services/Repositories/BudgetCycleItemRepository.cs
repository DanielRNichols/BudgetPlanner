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
    public class BudgetCycleItemRepository : DbResourceRepository<BudgetCycleItem>, IBudgetCycleItemRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetCycleItemRepository(ApplicationDbContext db) : base(db, db.BudgetCyclesItems)
        {
            _db = db;
        }
        public override async Task<IList<BudgetCycleItem>> GetAll()
        {
            var items = await _db.BudgetCyclesItems
                .Include(c => c.BudgetCycle)
                .Include(i => i.BudgetItem)
                .ToListAsync();

            return items;
        }

        public override async Task<BudgetCycleItem> GetById(int id)
        {
            var item = await _db.BudgetCyclesItems
                .Include(c => c.BudgetCycle)
                .Include(i => i.BudgetItem)
                .FirstOrDefaultAsync(q => q.Id == id);

            return item;
        }
    }
}
