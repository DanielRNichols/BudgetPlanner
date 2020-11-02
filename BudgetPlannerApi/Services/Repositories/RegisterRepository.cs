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
    public class RegisterRepository : DbResourceRepository<Register>, IRegisterRepository
    {
        private readonly ApplicationDbContext _db;

        public RegisterRepository(ApplicationDbContext db) : base(db, db.Registers)
        {
            _db = db;
        }
    }
}
