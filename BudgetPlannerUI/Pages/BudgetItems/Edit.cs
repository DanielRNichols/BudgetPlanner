using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetItems
{
    public partial class Edit
    {
        [Inject]
        private IBudgetItemsDataService _budgetItemsDataService { get; set; }
        [Inject]
        private IBudgetCategoriesDataService _budgetCategoriesDataService { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }

        [Parameter]
        public string Id { get; set; }
        private int _id = 0;

        public bool IsSuccess { get; set; } = true;
        public bool IsCreateMode { get; set; } = true;
        public BudgetItem Model { get; set; }

        public IList<BudgetCategory> BudgetCategories { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;

        public string Title
        {
            get
            {
                string mode = IsCreateMode ? "Create" : "Edit";
                return $"{mode} Budget Item";
            }
        }

        public bool ShowDeleteButton
        {
            get
            {
                return !IsCreateMode;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _id = 0;
            if (Int32.TryParse(Id, out _id) && (_id > 0))
                IsCreateMode = false;


            var result = await _budgetCategoriesDataService.Get(includeRelated: false);
            BudgetCategories = result.ToList();

            if (IsCreateMode)
                Model = new BudgetItem();
            else
                Model = await _budgetItemsDataService.Get(id: _id, includeRelated: true);

            Model.SelectedBudgetCategoryId = Model.BudgetCategoryId.ToString();
        }

        private async Task Save()
        {
            Model.BudgetCategoryId = int.Parse(Model.SelectedBudgetCategoryId);
            if (IsCreateMode)
                IsSuccess = await _budgetItemsDataService.Create(Model);
            else
                IsSuccess = await _budgetItemsDataService.Update(_id, Model);
            if (IsSuccess)
            {
                _toastService.ShowSuccess("Save Successful", "");
                BackToList();
            }
            else
            {
                _toastService.ShowWarning("Save Failed", "");
            }
        }

        public void Cancel()
        {
            BackToList();
        }

        public void Delete()
        {
            ShowDeleteDialog = true;
        }

        public async Task OnDeleteClose(bool accepted)
        {
            ShowDeleteDialog = false;
            if (accepted)
            {
                IsSuccess = await _budgetItemsDataService.Delete(_id);
                if (IsSuccess)
                {
                    _toastService.ShowSuccess("Delete Successful", "");
                    BackToList();
                }
                else
                {
                    _toastService.ShowWarning("Delete Failed", "");
                }
            }
        }

        public void BackToList()
        {
            _navManager.NavigateTo("/budgetitems/");
        }
    }
}

