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
    public partial class List
    {
        public IEnumerable<Register> Registers { get; set; }

        [Inject]
        private IRegistersDataService _dataService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;
        public bool IsSuccess { get; set; } = true;
        private int SelectedId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await _dataService.Get(includeRelated: true);
            Registers = result.ToList();
            foreach (var register in Registers)
            {
                register.Balance();
            }
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
                IsSuccess = await _dataService.Delete(SelectedId);
                if (IsSuccess)
                {
                    _toastService.ShowSuccess("Delete Successful", "");
                    var result = await _dataService.Get(includeRelated: true);
                    Registers = result.ToList();
                }
                else
                {
                    _toastService.ShowWarning("Delete Failed", "");
                }
            }

        }

    }
}
