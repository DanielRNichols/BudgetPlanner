﻿using BudgetPlanner.Data;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services.Repositories
{
    public class BudgetGroupRepository : DbResourceRepository<BudgetGroup>, IBudgetGroupRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetGroupRepository(ApplicationDbContext db) : base(db, db.BudgetGroups)
        {
            _db = db;
        }
        public override async Task<IList<BudgetGroup>> Get(bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.BudgetGroups.Include(g => g.BudgetCategories).ToListAsync();
            }

            return await base.Get(includeRelated);
        }

        public override async Task<BudgetGroup> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.BudgetGroups.Include(g => g.BudgetCategories).FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id, includeRelated);
        }
    }
}