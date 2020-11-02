﻿using BudgetPlannerApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Data
{
    [Table("Registries")]
    public class Registry : IDbResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "money")]
        public decimal StartingBalance { get; set; }
    }
}