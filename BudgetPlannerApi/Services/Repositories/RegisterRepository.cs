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
    public class RegisterRepository : DbResourceRepository<Register, BaseQueryOptions>, IRegisterRepository
    {
        private readonly ApplicationDbContext _db;

        public RegisterRepository(ApplicationDbContext db)
            : base(db, db.Registers)
        {
            _db = db;
        }

        public override async Task<IList<Register>> Get(BaseQueryOptions options)
        {

            if (options != null && options.IncludeRelated)
            {
                var query = _db.Registers.AsQueryable()
                    .Include(e => e.RegisterEntries);
                return await base.ExecuteQuery(query, options);
            }

            return await base.Get(options);
        }

        public override async Task<Register> GetById(int id, IBaseQueryOptions options = null)
        {
            if (options != null && options.IncludeRelated)
            {
                var query = _db.Registers.AsQueryable()
                    .Include(e => e.RegisterEntries);
                return await base.ExecuteQueryById(id, query, options);
            }

            return await base.GetById(id, options);
        }

        public async Task<bool> Reconcile(int id, string userId)
        {
            // status = 2 is "cleared", stats = 3 is "reconciled" (need better way to specify this)
            // change all cleard to reconciled

            var options = new RegisterEntriesQueryOptions() { RegisterId = id, Status = 2, UserId = userId };

            var entries = await _db.RegisterEntries
                .Where(r => r.RegisterId == id && r.MarkedForDeletion == false && r.Status == 2)
                .ToListAsync();

            if ((entries == null) || (entries.Count() == 0))
                return false;

            foreach (var entry in entries)
            {
                entry.Status = 3;
                _db.RegisterEntries.Update(entry);
            }

            bool status = await base.Save();
            if (!status)
                return false;

            return await Balance(id, userId);


        }

        public async Task<bool> Balance(int id, string userId)
        {
            BaseQueryOptions options = new BaseQueryOptions() { UserId = userId };
            var register = await GetById(id, options);
            if (register == null)
                return false;

            return await Balance(register);
        }

        private async Task<bool> Balance(Register register)
        {
            var netTotals = _db.RegisterEntries
                .Where(r => r.RegisterId ==register.Id && r.MarkedForDeletion == false)
                .GroupBy(r => r.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Net = g.Sum(r => r.DepositAmount - r.WithdrawalAmount)
                });

            if (netTotals == null)
                return false;

            decimal pendingNet = 0;
            decimal clearedNet = 0;
            decimal outstandingNet = 0;

            foreach (var row in netTotals)
            {
                switch (row.Status)
                {
                    // pending
                    case 1:
                        pendingNet += row.Net;
                        break;

                    // cleared or reconciled
                    case 2:
                    case 3:
                        clearedNet += row.Net;
                        break;

                    // outstanding
                    default:
                        outstandingNet += row.Net;
                        break;
                }
            }
            register.ClearedBalance = register.StartingBalance + clearedNet;
            register.AvailableBalance = register.ClearedBalance + pendingNet;
            register.EndingBalance = register.AvailableBalance - outstandingNet;

            _db.Registers.Update(register);

            return await base.Save();

        }
    }
}
