﻿using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Pages
{
    public partial class BudgetItemTypesList
    {
        public IEnumerable<BudgetItemType> BudgetItemTypes { get; set; }

        [Inject]
        public IBudgetItemTypesDataService BudgetItemTypesDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await BudgetItemTypesDataService.Get();
            BudgetItemTypes = result.ToList();
        }
    }
}
