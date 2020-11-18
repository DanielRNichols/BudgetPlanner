﻿using AutoMapper;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.Interfaces;
using BudgetPlannerApi.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services.ControllerHelpers
{
    public class BudgetCycleItemsControllerHelper : 
        DbResourceControllerHelper<BudgetCycleItem, BudgetCycleItemsQueryOptions>, IBudgetCycleItemsControllerHelper
    {
        public BudgetCycleItemsControllerHelper(ILoggerService logger,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
            : base(logger, mapper, userManager, httpContextAccessor)
        {

        }
    }
}

