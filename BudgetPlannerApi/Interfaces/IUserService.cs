using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IUserService
    {
        Task<IdentityUser> GetCurrentUser();
        Task<string> GetCurrentUserId();
    }
}
