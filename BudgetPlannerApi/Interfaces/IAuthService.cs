using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJWT(IdentityUser user);
    }
}
