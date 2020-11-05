using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetItemTypes
{
    public partial class Details
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IBudgetItemTypesDataService BudgetItemTypesDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public BudgetItemType Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int itemTypeId = int.Parse(Id);
            Model = await BudgetItemTypesDataService.Get(itemTypeId);
        }

        public void BackToList()
        {
            NavigationManager.NavigateTo("/budgetitemtypes/");
        }
    }
}
