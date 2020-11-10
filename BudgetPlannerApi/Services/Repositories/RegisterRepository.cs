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
    public class RegisterRepository : DbResourceRepository<Register, BaseQueryOptions>, IRegisterRepository
    {
        private readonly ApplicationDbContext _db;

        public RegisterRepository(ApplicationDbContext db) : base(db, db.Registers)
        {
            _db = db;
        }

        public override async Task<IList<Register>> Get(BaseQueryOptions options)
        {
            bool includeRelated = options != null && options.IncludeRelated;
            if (includeRelated)
            {
                return await _db.Registers
                    .Include(e => e.RegisterEntries)
                    .ToListAsync();
            }

            return await base.Get(options);
        }

        public override async Task<Register> GetById(int id, bool includeRelated = false)
        {
            if (includeRelated)
            {
                return await _db.Registers
                    .Include(e => e.RegisterEntries)
                    .FirstOrDefaultAsync(q => q.Id == id);
            }

            return await base.GetById(id);
        }


    }
}
