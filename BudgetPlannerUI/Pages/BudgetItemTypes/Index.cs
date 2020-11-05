using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetItemTypes
{
    public partial class Index
    {
        public IEnumerable<BudgetItemType> Model { get; set; }

        [Inject]
        private IBudgetItemTypesDataService _dataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await _dataService.Get();
            Model = result.ToList();
        }
    }
}
