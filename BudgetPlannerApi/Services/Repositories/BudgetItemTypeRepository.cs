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
    public class BudgetItemTypeRepository : DbResourceRepository<BudgetItemType>, IBudgetItemTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetItemTypeRepository(ApplicationDbContext db) : base(db, db.BudgetItemTypes)
        {
            _db = db;
        }
        public override async Task<IList<BudgetItemType>> GetAll()
        {
            var items = await _db.BudgetItemTypes.Include(g => g.BudgetItemGroups).ToListAsync();

            return items;
        }

        public override async Task<BudgetItemType> GetById(int id)
        {
            var item = await _db.BudgetItemTypes.Include(g => g.BudgetItemGroups).FirstOrDefaultAsync(q => q.Id == id);

            return item;
        }
    }
}
