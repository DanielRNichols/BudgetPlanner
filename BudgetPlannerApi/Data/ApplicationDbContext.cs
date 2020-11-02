using System;
using System.Collections.Generic;
using System.Text;
using BudgetPlannerApi.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BudgetItemType> BudgetItemTypes { get; set; }
        public DbSet<BudgetItemGroup> BudgetItemGroups { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<MemorizedTransaction> MemorizedTransactions { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<RegisterEntry> RegisterEntries { get; set; }
        public DbSet<BudgetCycle> BudgetCycles { get; set; }
        public DbSet<BudgetCycleItem> BudgetCyclesItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
