using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Components
{
    public partial class RegisterEntryList
    {
        [Parameter]
        [Required]
        public IEnumerable<RegisterEntry> Entries { get; set; }

        [Parameter]
        public string[] HideColumns { get; set; }

        [Parameter]
        public EventCallback<IEnumerable<RegisterEntry>> OnUpdate { get; set; }

        [Inject]
        private IRegisterEntriesDataService _dataService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;
        private int SelectedId { get; set; }

        private async Task UpdateEntry(int id, RegisterEntry entry)
        {
            bool success = await _dataService.Update(id, entry);
            if (success)
            {
                var result = await _dataService.Get(includeRelated: true);
                Entries = result.ToList();
                if (OnUpdate.HasDelegate)
                    await OnUpdate.InvokeAsync(Entries);
            }
        }

        public async Task NextStatus(int id)
        {
            RegisterEntry entry = await _dataService.Get(id, includeRelated: false);
            entry.Status = (int)entry.NextStatus();

            await UpdateEntry(id, entry);

        }

        public async Task IncrementDate(int id)
        {
            RegisterEntry entry = await _dataService.Get(id, includeRelated: false);
            entry.TransactionDate = entry.TransactionDate.AddDays(1);

            await UpdateEntry(id, entry);

        }

        public async Task DecrementDate(int id)
        {
            RegisterEntry entry = await _dataService.Get(id, includeRelated: false);
            entry.TransactionDate = entry.TransactionDate.AddDays(-1);

            await UpdateEntry(id, entry);

        }

        public void Delete(int id)
        {
            SelectedId = id;
            ShowDeleteDialog = true;
        }

        public async Task OnDeleteClose(bool accepted)
        {
            ShowDeleteDialog = false;
            if (accepted)
            {
                bool success = await _dataService.Delete(SelectedId);
                if (success)
                {
                    _toastService.ShowSuccess("Delete Successful", "");
                    var result = await _dataService.Get(includeRelated: true);
                    Entries = result.ToList();
                    if (OnUpdate.HasDelegate)
                        await OnUpdate.InvokeAsync(Entries);
                }
                else
                {
                    _toastService.ShowWarning("Delete Failed", "");
                }
            }

        }

        public bool ShowColumn(string colName)
        {
            if (HideColumns == null)
                return true;

            return !HideColumns.Contains(colName, StringComparer.InvariantCultureIgnoreCase);
        }

    }
}
