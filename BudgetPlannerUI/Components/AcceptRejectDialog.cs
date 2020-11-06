using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Components
{
    public partial class AcceptRejectDialog
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string Message { get; set; }

        [Parameter]
        public EventCallback<bool> OnClose { get; set; }

        public async Task Accept()
        {
            await OnClose.InvokeAsync(true);
        }

        public async Task Reject()
        {
            await OnClose.InvokeAsync(false);
        }
    }
}
