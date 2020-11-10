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

        [Inject]
        private IRegisterEntriesDataService _dataService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;
        public bool IsSuccess { get; set; } = true;
        private int SelectedId { get; set; }

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
                IsSuccess = await _dataService.Delete(SelectedId);
                if (IsSuccess)
                {
                    _toastService.ShowSuccess("Delete Successful", "");
                    var result = await _dataService.Get(includeRelated: true);
                    Entries = result.ToList();
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
