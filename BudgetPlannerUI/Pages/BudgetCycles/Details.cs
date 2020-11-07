using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetCycles
{
    public partial class Details
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        private IBudgetCyclesDataService _dataService { get; set; }
        [Inject]
        public NavigationManager _navManager { get; set; }

        public BudgetCycle Model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int id = int.Parse(Id);
            Model = await _dataService.Get(id: id, includeRelated: false);
        }

        public string FormatDate(DateTime dt)
        {
            return String.Format("{0:MM/dd/yyyy}", dt);
        }

        public string FormatCurrency(decimal amount)
        {
            return String.Format("{0:0.00}", amount);
        }


        public void BackToList()
        {
            _navManager.NavigateTo("/budgecycles/");
        }
    }
}
