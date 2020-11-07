using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetCategories
{
    public partial class Details
    {
        [Inject]
        private IBudgetCategoriesDataService _budgetCategoriesDataService { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }

        public BudgetCategory Model { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int id = int.Parse(Id);

             Model = await _budgetCategoriesDataService.Get(id: id, includeRelated: true);

        }


        public void BackToList()
        {
            _navManager.NavigateTo("/budgetcategories/");
        }
    }
}

