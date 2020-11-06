using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages.BudgetItemGroups
{
    public partial class Edit
    {
        [Inject]
        private IBudgetItemGroupsDataService _budgetItemGroupsDataService { get; set; }
        [Inject]
        private IBudgetItemTypesDataService _budgetItemTypesDataService { get; set; }
        [Inject]
        private NavigationManager _navManager { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }

        [Parameter]
        public string Id { get; set; }
        private int _id = 0;

        private bool IsSuccess { get; set; } = true;
        private bool IsCreateMode { get; set; } = true;
        public BudgetItemGroup Model { get; set; }
        public string SelectedBudgetItemTypeId { get; set; }
        public IList<BudgetItemType> BudgetItemTypes { get; set; }

        public bool ShowDeleteDialog { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            _id = 0;
            if (Int32.TryParse(Id, out _id) && (_id > 0))
                IsCreateMode = false;


            var result = await _budgetItemTypesDataService.Get(includeRelated: false);
            BudgetItemTypes = result.ToList();

            if (IsCreateMode)
                Model = new BudgetItemGroup();
            else
                Model = await _budgetItemGroupsDataService.Get(id: _id, includeRelated: true);

            SelectedBudgetItemTypeId = Model.BudgetItemTypeId.ToString();
        }

        private async Task Save()
        {
            Model.BudgetItemTypeId = int.Parse(SelectedBudgetItemTypeId);
            if (IsCreateMode)
                IsSuccess = await _budgetItemGroupsDataService.Create(Model);
            else
                IsSuccess = await _budgetItemGroupsDataService.Update(_id, Model);
            if (IsSuccess)
            {
                _toastService.ShowSuccess("Update Successful", "");
                BackToList();
            }
        }

        public void Cancel()
        {
            BackToList();
        }

        public void Delete()
        {
            ShowDeleteDialog = true;
            //IsSuccess = await _budgetItemGroupsDataService.Delete(_id);
            //if (IsSuccess)
            //{
            //    _toastService.ShowSuccess("Delete Successful", "");
            //    BackToList();
            //}
        }

        public async Task OnDeleteClose(bool accepted)
        {
            ShowDeleteDialog = false;
            if(accepted)
            {
                IsSuccess = await _budgetItemGroupsDataService.Delete(_id);
                if (IsSuccess)
                {
                    _toastService.ShowSuccess("Delete Successful", "");
                    BackToList();
                }
            }
        }

        public void BackToList()
        {
            _navManager.NavigateTo("/budgetitemgroups/");
        }
    }
}
