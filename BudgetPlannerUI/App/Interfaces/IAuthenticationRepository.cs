using BudgetPlannerUI.Models;
using BudgetPlannerUI.Pages.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Interfaces
{
    public interface IAuthenticationRepository
    {
        public Task<bool> Register(UserRegistrationModel user);

        public Task<bool> Login(UserLoginModel user);

        public Task Logout();
    }
}
