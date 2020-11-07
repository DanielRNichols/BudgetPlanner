using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetCategories
{
    public partial class Edit
    {
        [Inject]
        private IBudgetCategoriesDataService _budgetCategoriesDataService { get; set; }
        [Inject]
        private IBudgetGroupsDataService _budgetGroupsDataService { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }

        [Parameter]
        public string Id { get; set; }
        private int _id = 0;

        public bool IsSuccess { get; set; } = true;
        public bool IsCreateMode { get; set; } = true;
        public BudgetCategory Model { get; set; }

        public IList<BudgetGroup> BudgetGroups { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;

        public string Title
        {
            get
            {
                string mode = IsCreateMode ? "Create" : "Edit";
                return $"{mode} Budget Category";
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


            var result = await _budgetGroupsDataService.Get(includeRelated: false);
            BudgetGroups = result.ToList();

            if (IsCreateMode)
                Model = new BudgetCategory();
            else
                Model = await _budgetCategoriesDataService.Get(id: _id, includeRelated: true);

            Model.SelectedBudgetGroupId = Model.BudgetGroupId.ToString();
        }

        private async Task Save()
        {
            Model.BudgetGroupId = int.Parse(Model.SelectedBudgetGroupId);
            if (IsCreateMode)
                IsSuccess = await _budgetCategoriesDataService.Create(Model);
            else
                IsSuccess = await _budgetCategoriesDataService.Update(_id, Model);
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
            if(accepted)
            {
                IsSuccess = await _budgetCategoriesDataService.Delete(_id);
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
            _navManager.NavigateTo("/budgetcategories/");
        }
    }
}
