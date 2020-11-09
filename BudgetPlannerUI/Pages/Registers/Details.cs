using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.Registers
{
    public partial class Details
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        private IRegistersDataService _dataService { get; set; }
        [Inject]
        public NavigationManager _navManager { get; set; }

        public Register Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int itemTypeId = int.Parse(Id);
            Model = await _dataService.Get(id: itemTypeId, includeRelated: false);
        }

        public void BackToList()
        {
            _navManager.NavigateTo("/registers/");
        }
    }
}
