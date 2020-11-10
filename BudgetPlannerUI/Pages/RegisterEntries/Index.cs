using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.RegisterEntries
{
    public partial class Index
    {
        public IEnumerable<RegisterEntry> Model { get; set; }

        public string[] HideColumns = new string[] { "id", "register", "memo" };

        [Inject]
        private IRegisterEntriesDataService _dataService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            var result = await _dataService.Get(includeRelated: true);
            Model = result.ToList();
        }

    }
}
