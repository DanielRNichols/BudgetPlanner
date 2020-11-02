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
        public override async Task<IList<BudgetItemType>> Get(bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.BudgetItemTypes.Include(g => g.BudgetItemGroups).ToListAsync();
            }

            return await base.Get(includeRelated);
        }

        public override async Task<BudgetItemType> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.BudgetItemTypes.Include(g => g.BudgetItemGroups).FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id, includeRelated);
        }
    }
}
