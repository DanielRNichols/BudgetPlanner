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
    public class RegisterEntryRepository : DbResourceRepository<RegisterEntry>, IRegisterEntryRepository
    {
        private readonly ApplicationDbContext _db;

        public RegisterEntryRepository(ApplicationDbContext db) : base(db, db.RegisterEntries)
        {
            _db = db;
        }

        public override async Task<IList<RegisterEntry>> Get(bool includeRelated = false)
        {
            if(includeRelated)
            {
                return await _db.RegisterEntries
                .Include(r => r.Register)
                .Include(c => c.BudgetCycle)
                .Include(i => i.BudgetItem)
                .ToListAsync();
            }

            return await base.Get(includeRelated);

        }

        public override async Task<RegisterEntry> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.RegisterEntries
                .Include(r => r.Register)
                .Include(c => c.BudgetCycle)
                .Include(i => i.BudgetItem)
                .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id, includeRelated);
        }
    }
}