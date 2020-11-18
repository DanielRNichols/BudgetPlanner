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
    public class RegisterEntryRepository : DbResourceRepository<RegisterEntry, RegisterEntriesQueryOptions>, IRegisterEntryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IRegisterRepository _registerRepo;

        public RegisterEntryRepository(ApplicationDbContext db, IRegisterRepository registerRepo) 
            : base(db, db.RegisterEntries)
        {
            _db = db;
            _registerRepo = registerRepo;
        }

        public override async Task<IList<RegisterEntry>> Get(RegisterEntriesQueryOptions options)
        {
            if (options != null)
            {
                var query = _db.RegisterEntries.AsQueryable();
                if (options.RegisterId > 0)
                {
                    query = query.Where(r => r.RegisterId == options.RegisterId);
                }
                if (options.Status >= 0)
                {
                    query = query.Where(r => r.Status == options.Status);
                }
                if (options.IncludeRelated)
                {
                    query = query
                        .Include(r => r.Register)
                        .Include(c => c.BudgetCycle)
                        .Include(i => i.BudgetItem);
                }
                return await base.ExecuteQuery(query, options);
            }

            return await base.Get();

        }

        public override async Task<RegisterEntry> GetById(int id, IBaseQueryOptions options)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.RegisterEntries.AsQueryable()
                        .Include(r => r.Register)
                        .Include(c => c.BudgetCycle)
                        .Include(i => i.BudgetItem);

                return await base.ExecuteQueryById(id, query, options);
            }

            return await base.GetById(id, options);
        }

        // override to balance the register after adding new entry
        public override async Task<bool> Create(RegisterEntry entry)
        {
            bool status = await base.Create(entry);
            if (!status)
                return false;

            return await _registerRepo.Balance(entry.RegisterId);
        }

        // override to balance the register after updating entry
        public override async Task<bool> Update(RegisterEntry entry)
        {
            bool status = await base.Update(entry);
            if (!status)
                return false;

            return await _registerRepo.Balance(entry.RegisterId);
        }

        // override to balance the register after deleting entry
        public override async Task<bool> Delete(RegisterEntry entry)
        {
            bool status = await base.Delete(entry);
            if (!status)
                return false;

            return await _registerRepo.Balance(entry.RegisterId);
        }
    }
}
