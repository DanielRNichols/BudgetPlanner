using BudgetPlannerApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("BudgetCycles")]
    public class BudgetCycle : IDbResource
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        [Column(TypeName = "money")]
        public decimal StartingBalance { get; set; }
        public bool MarkedForDeletion { get; set; }

        public virtual IList<BudgetCycleItem> BudgetCycleItems { get; set; }
    }
}
