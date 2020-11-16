using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("Registers")]
    public class Register : IDbResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal StartingBalance { get; set; }
        [Column(TypeName = "money")]
        public decimal EndingBalance { get; set; }
        [Column(TypeName = "money")]
        public decimal ClearedBalance { get; set; }
        [Column(TypeName = "money")]
        public decimal AvailableBalance { get; set; }

        public virtual IList<RegisterEntry> RegisterEntries { get; set; }
    }
}
