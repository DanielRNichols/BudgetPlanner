using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetItemGroups
{
    public partial class Details
    {
        [Inject]
        private IBudgetItemGroupsDataService _budgetItemGroupsDataService { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }

        public BudgetItemGroup Model { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int id = int.Parse(Id);

             Model = await _budgetItemGroupsDataService.Get(id: id, includeRelated: true);

        }


        public void BackToList()
        {
            _navManager.NavigateTo("/budgetitemgroups/");
        }
    }
}

