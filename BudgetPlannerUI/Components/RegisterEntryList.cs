using Blazored.Toast.Services;
using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Components
{
    public partial class RegisterEntryList
    {
        [Parameter]
        public IEnumerable<RegisterEntry> Entries { get; set; }

        [Parameter]
        public EventCallback<RegisterEntryActionEventArgs> OnAction { get; set; }

        [Parameter]
        public bool HideReconciled { get; set; }

    }
}
