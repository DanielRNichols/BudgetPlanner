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
    public class RegistryRepository : DbResourceRepository<Registry>, IRegistryRepository
    {
        private readonly ApplicationDbContext _db;

        public RegistryRepository(ApplicationDbContext db) : base(db, db.Registries)
        {
            _db = db;
        }
    }
}
