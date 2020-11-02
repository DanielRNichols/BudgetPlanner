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
    public class BudgetCycleRepository : DbResourceRepository<BudgetCycle>, IBudgetCycleRepository
    {
        private readonly ApplicationDbContext _db;

        public BudgetCycleRepository(ApplicationDbContext db) : base(db, db.BudgetCycles)
        {
            _db = db;
        }
    }
}
