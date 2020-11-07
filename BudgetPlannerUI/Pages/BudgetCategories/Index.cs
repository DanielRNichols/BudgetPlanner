using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetCategories
{
    public partial class Index
    {
        public IEnumerable<BudgetCategory> Model { get; set; }

        [Inject]
        private IBudgetCategoriesDataService _budgetCategoriesDataService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;
        public bool IsSuccess { get; set; } = true;
        private int SelectedId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await _budgetCategoriesDataService.Get(includeRelated: true);
            Model = result.ToList();
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
                IsSuccess = await _budgetCategoriesDataService.Delete(SelectedId);
                if (IsSuccess)
                {
                    _toastService.ShowSuccess("Delete Successful", "");
                    var result = await _budgetCategoriesDataService.Get(includeRelated: true);
                    Model = result.ToList();
                }
                else
                {
                    _toastService.ShowWarning("Delete Failed", "");
                }
            }

        }
    }
}