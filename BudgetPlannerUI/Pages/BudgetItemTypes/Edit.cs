using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetItemTypes
{
    public partial class Edit
    {
        [Inject]
        private IBudgetItemTypesDataService _budgetItemTypesDataService { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }

        [Parameter]
        public string Id { get; set; }
        private int _id = 0;

        public bool IsSuccess { get; set; } = true;
        public bool IsCreateMode { get; set; } = true;
        public BudgetItemType Model { get; set; }
        public bool ShowDeleteDialog { get; set; } = false;

        public string Title
        {
            get
            {
                string mode = IsCreateMode ? "Create" : "Edit";
                return $"{mode} Budget Item Type";
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
            if(Int32.TryParse(Id, out _id) && (_id > 0))
                IsCreateMode = false;

            if (IsCreateMode)
                Model = new BudgetItemType();
            else
                Model = await _budgetItemTypesDataService.Get(id: _id, includeRelated: false);
        }

        private async Task Save()
        {
            if (IsCreateMode)
                IsSuccess = await _budgetItemTypesDataService.Create(Model);
            else
                IsSuccess = await _budgetItemTypesDataService.Update(_id, Model);
            if(IsSuccess)
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
                IsSuccess = await _budgetItemTypesDataService.Delete(_id);
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
            _navManager.NavigateTo("/budgetitemtypes/");
        }
    }
}
