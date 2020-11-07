using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetItems
{
    public partial class Details
    {
        [Inject]
        private IBudgetItemsDataService _budgetItemsDataService { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }

        public BudgetItem Model { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int id = int.Parse(Id);

            Model = await _budgetItemsDataService.Get(id: id, includeRelated: true);

        }

        public string getItemType()
        {
            return Model.IsIncome ? "Income" : "Expense";
        }

        public void BackToList()
        {
            _navManager.NavigateTo("/budgetitems/");
        }
    }
}
