using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages
{
    public partial class BudgetItemTypesList
    {
        public IEnumerable<BudgetItemType> BudgetItemTypes { get; set; }

        [Inject]
        public IBudgetPlannerDataService BudgetPlannerDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await BudgetPlannerDataService.GetBudgetItemTypes();
            BudgetItemTypes = result.ToList();
        }
    }
}
