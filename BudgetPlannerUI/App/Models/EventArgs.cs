using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class RegisterEntryActionEventArgs
    {
        public int Id { get; set; }
        public RegisterEntryAction Action { get; set; }

    }
}
