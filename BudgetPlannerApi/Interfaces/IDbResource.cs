using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Interfaces
{ 
    public interface IDbResource
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool MarkedForDeletion { get; set; }
    }
}
