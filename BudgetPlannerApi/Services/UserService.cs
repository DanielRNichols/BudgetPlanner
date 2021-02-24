using BudgetPlannerApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services
{
    public class UserService: IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager,
                           IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IdentityUser> GetCurrentUser()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                return null;

            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null)
                return null;

            return await _userManager.FindByEmailAsync(userId.Value);
        }


        public async Task<string> GetCurrentUserId()
        {
            var user = await GetCurrentUser();
            if (user == null)
                return null;

            return user.Id;
        }

    }
}
