using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetCycleItems
{
    public partial class Details
    {
        [Inject]
        private IBudgetCycleItemsDataService _dataService { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }

        public BudgetCycleItem Model { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int id = int.Parse(Id);

            Model = await _dataService.Get(id: id, includeRelated: true);

        }

        public void BackToList()
        {
            _navManager.NavigateTo("/budgetcycleitems/");
        }
    }
}
