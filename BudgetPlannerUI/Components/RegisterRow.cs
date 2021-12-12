using BudgetPlannerUI.Interfaces;
using BudgetPlannerUI.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Components
{
    public partial class RegisterRow
    {
        [Parameter]
        public RegisterEntry Item { get; set; }

        [Parameter]
        public bool HideReconciled { get; set; }

        [Parameter]
        public bool HideRegisterName { get; set; }

        [Parameter]
        public EventCallback<RegisterEntryActionEventArgs> OnAction { get; set; }

        public bool ShowItem()
        {
            if (Item == null)
                return false;
            if (!HideReconciled)
                return true;

            var status = Item.GetStatus();
            return status != RegisterEntryStatus.Reconciled;
        }

        private async Task InvokeAction(RegisterEntryAction action)
        {
            var args = new RegisterEntryActionEventArgs() { Id = Item.Id, Action = action };
            if(OnAction.HasDelegate)
                await OnAction.InvokeAsync(args);
        }

        public async Task NextStatus()
        {
            await InvokeAction(RegisterEntryAction.NextStatus);
        }
        public async Task DecrementDate()
        {
            await InvokeAction(RegisterEntryAction.DecrementDate);
        }
        public async Task IncrementDate()
        {
            await InvokeAction(RegisterEntryAction.IncrementDate);
        }
        public async Task Delete()
        {
            await InvokeAction(RegisterEntryAction.Delete);
        }
    }
}
