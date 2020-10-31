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
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
