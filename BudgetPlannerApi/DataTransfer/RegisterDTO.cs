using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class RegisterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal EndingBalance { get; set; }
        public decimal ClearedBalance { get; set; }
        public decimal AvailableBalance { get; set; }
        public bool MarkedForDeletion { get; set; }

        public virtual IList<RegisterEntryDTO> RegisterEntries { get; set; }

    }
    public class RegisterCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
        public bool MarkedForDeletion { get; set; }
    }
}
