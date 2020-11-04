using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages
{
    public partial class BudgetItemTypeDetails
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IBudgetItemTypesDataService BudgetItemTypesDataService { get; set; }

        public BudgetItemType BudgetItemType { get; set; }

        protected override async Task OnInitializedAsync()
        {
            int itemTypeId = int.Parse(Id);
            BudgetItemType = await BudgetItemTypesDataService.Get(itemTypeId);
        }
    }
}
