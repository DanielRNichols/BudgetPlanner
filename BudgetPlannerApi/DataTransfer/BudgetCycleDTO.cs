using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.DataTransfer
{
    public class BudgetCycleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal StartingBalance { get; set; }

        public virtual IList<BudgetCycleItemDTO> BudgetCycleItems { get; set; }
    }

    public class BudgetCycleCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public decimal StartingBalance { get; set; }
    }

}
