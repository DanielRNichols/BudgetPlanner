using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Interfaces
{
    public interface ILocalStorageService
    {
        Task<string> GetStringAsync(string key);
        Task<bool> GetBoolAsync(string key);
        Task<int> GetIntAsync(string key);
        Task<double> GetRealAsync(string key);

        Task SetItemAsync(string key, object obj);

        Task RemoveItemAsync(string key);
    }
}
