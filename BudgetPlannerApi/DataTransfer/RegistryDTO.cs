using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class RegistryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
    }
    public class RegistryCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public decimal StartingBalance { get; set; }
    }
}
