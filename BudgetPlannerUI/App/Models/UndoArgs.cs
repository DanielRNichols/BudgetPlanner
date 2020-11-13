using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerUI.Models
{
    public class UndoArgs
    {
        public RegisterEntryAction Action { get; set; }
        public RegisterEntry Entry { get; set; }
    }
}
