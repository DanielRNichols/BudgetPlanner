using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BudgetPlannerUI.Components
{
    public partial class RegisterEntryInlineEdit
    {
        [Parameter]
        public RegisterEntry Entry { get; set; }

        [Parameter]
        public IList<BudgetCycle> BudgetCycles { get; set; }

        [Parameter]
        public IList<BudgetItem> BudgetItems { get; set; }

        [Parameter]
        public EventCallback<RegisterEntry> OnSave { get; set; }

        protected override void OnInitialized()
        {
            //if (Entry != null)
            //{
            //    Entry.SelectedRegisterId = Entry.RegisterId.ToString();
            //    Entry.SelectedBudgetCycleId = Entry.BudgetCycleId.ToString();
            //    Entry.SelectedBudgetItemId = Entry.BudgetItemId.ToString();
            //}

            base.OnInitialized();

        }

        protected override void OnParametersSet()
        {
            if (Entry != null)
            {
                Entry.SelectedRegisterId = Entry.RegisterId.ToString();
                Entry.SelectedBudgetCycleId = Entry.BudgetCycleId.ToString();
                Entry.SelectedBudgetItemId = Entry.BudgetItemId.ToString();
            }
            base.OnParametersSet();
        }

        private async Task Save()
        {
            // workaround for selects not able to use int
            Entry.BudgetCycleId = int.Parse(Entry.SelectedBudgetCycleId);
            Entry.BudgetItemId = int.Parse(Entry.SelectedBudgetItemId);
            if (OnSave.HasDelegate)
                await OnSave.InvokeAsync(Entry);
        }

        private Task Cancel()
        {
            return Task.CompletedTask;
        }

    }
}
