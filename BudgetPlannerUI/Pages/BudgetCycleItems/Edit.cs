using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetCycleItems
{
    public partial class Edit
    {
        [Inject]
        private IBudgetCycleItemsDataService _dataService { get; set; }
        [Inject]
        private IBudgetCyclesDataService _budgetCyclesDataService { get; set; }
        [Inject]
        private IBudgetItemsDataService _budgetItemsDataService { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }

        [Parameter]
        public string Id { get; set; }
        private int _id = 0;

        public bool IsSuccess { get; set; } = true;
        public bool IsCreateMode { get; set; } = true;
        public BudgetCycleItem Model { get; set; }

        public IList<BudgetCycle> BudgetCycles { get; set; }
        public IList<BudgetItem> BudgetItems { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;

        public string Title
        {
            get
            {
                string mode = IsCreateMode ? "Create" : "Edit";
                return $"{mode} Budget Cycle Item";
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


            var result = await _budgetCyclesDataService.Get(includeRelated: false);
            BudgetCycles = result.ToList();

            var result1 = await _budgetItemsDataService.Get(includeRelated: false);
            BudgetItems = result1.ToList();

            if (IsCreateMode)
                Model = new BudgetCycleItem();
            else
                Model = await _dataService.Get(id: _id, includeRelated: true);

            Model.SelectedBudgetCycleId = Model.BudgetCycleId.ToString();
            Model.SelectedBudgetItemId = Model.BudgetItemId.ToString();
        }

        private async Task Save()
        {
            Model.BudgetCycleId = int.Parse(Model.SelectedBudgetCycleId);
            Model.BudgetItemId = int.Parse(Model.SelectedBudgetItemId);
            if (IsCreateMode)
                IsSuccess = await _dataService.Create(Model);
            else
                IsSuccess = await _dataService.Update(_id, Model);
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
                IsSuccess = await _dataService.Delete(_id);
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
            _navManager.NavigateTo("/budgetcycleitems/");
        }
    }
}
