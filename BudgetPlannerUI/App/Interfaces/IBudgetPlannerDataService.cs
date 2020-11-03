using BudgetPlannerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Interfaces
{
    public interface IBudgetPlannerDataService
    {
        // BudgetItemTypes
        Task<IEnumerable<BudgetItemType>> GetBudgetItemTypes(bool includeRelated = false);
        Task<BudgetItemType> GetBudgetItemType(int id, bool includeRelated = false);
    }
}
