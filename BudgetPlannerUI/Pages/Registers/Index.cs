using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.Registers
{
    public partial class Index
    {
        public IList<Register> Registers { get; set; }

        public IList<RegisterEntry> Entries { get; set; }
        public Register SelectedRegister { get; set; }

        [Inject]
        private IRegistersDataService _dataService { get; set; }

        [Inject]
        private IRegisterEntriesDataService _registerEntriesDataService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;
        private string SelectedId { get; set; }

        private string[] HideColumns = new string[] { "id", "register" };

        protected override async Task OnInitializedAsync()
        {
            var result = await _dataService.Get(includeRelated: false);
            Registers = result.ToList();
            if (Registers.Count > 0)
            {
                SelectedId = Registers.First().Id.ToString();
                await OnRegisterSelected(SelectedId);
            }
        }

        public async Task OnRegisterSelected(string selectedId)
        {
            int id = int.Parse(selectedId);
            SelectedRegister = await _dataService.Get(id, includeRelated: false);
            string suppQueryStr = $"registerId={id}";
            var result = await _registerEntriesDataService.Get(
                                    includeRelated: true,
                                    supplementalQueryStr: suppQueryStr);
            SelectedRegister.RegisterEntries = result.ToList();
            SelectedRegister.Balance();
        }

        public Task OnUpdate(IEnumerable<RegisterEntry> entries)
        {
            Entries = entries.ToList();
            SelectedRegister.RegisterEntries = Entries;
            SelectedRegister.Balance();

            return Task.CompletedTask;
        }

    }
}
