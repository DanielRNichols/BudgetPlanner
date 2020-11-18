using BudgetPlannerApi.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    public static class SeedData
    {
        public async static Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private async static Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if(await userManager.FindByEmailAsync(Users.AdminEmail) == null)
            {
                var user = new IdentityUser { UserName = Users.AdminEmail, Email = Users.AdminEmail };
                var result = await userManager.CreateAsync(user, Users.AdminPassword);
                if(result.Succeeded)
                {
                    await userManager.AddToRolesAsync(user, new List<string>() { Roles.Administrator, Roles.User });
                }
            }

        }

        private async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync(Roles.Administrator))
            {
                var role = new IdentityRole { Name = Roles.Administrator };
                await roleManager.CreateAsync(role);
            }

            if (!await roleManager.RoleExistsAsync(Roles.User))
            {
                var role = new IdentityRole { Name = Roles.User };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
