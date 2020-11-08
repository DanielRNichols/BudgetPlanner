using AutoMapper;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.Interfaces;
using BudgetPlannerApi.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services.ControllerHelpers
{
    public class BudgetGroupsControllerHelper : 
        DbResourceControllerHelper<BudgetGroup, BaseQueryOptions>, IBudgetGroupsControllerHelper
    {
        public BudgetGroupsControllerHelper(ILoggerService logger, IMapper mapper) : base(logger, mapper)
        {

        }
    }
}

