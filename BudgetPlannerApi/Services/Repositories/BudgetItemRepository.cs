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
    public class BudgetItemRepository : DbResourceRepository<BudgetItem, BaseQueryOptions>, IBudgetItemRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetItemRepository(ApplicationDbContext db) : base(db, db.BudgetItems)
        {
            _db = db;
        }

        public override async Task<IList<BudgetItem>> Get(BaseQueryOptions options)
        {
            bool includeRelated = options != null && options.IncludeRelated;
            if (includeRelated)
            {
                return await _db.BudgetItems
                .Include(g => g.BudgetCategory)
                .Include(i => i.BudgetCycleItems)
                .ToListAsync();
            }

            return await base.Get(options);

        }

        public override async Task<BudgetItem> GetById(int id, BaseQueryOptions options)
        {
            bool includeRelated = options != null && options.IncludeRelated;
            if (includeRelated)
            {
                return await _db.BudgetItems
                    .Include(g => g.BudgetCategory)
                    .Include(i => i.BudgetCycleItems)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id, options);
        }
    }
}
