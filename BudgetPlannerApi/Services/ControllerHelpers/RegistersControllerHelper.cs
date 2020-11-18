using AutoMapper;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.Interfaces;
using BudgetPlannerApi.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services.ControllerHelpers
{
    public class RegistersControllerHelper : 
        DbResourceControllerHelper<Register, BaseQueryOptions>, IRegistersControllerHelper
    {
        public RegistersControllerHelper(ILoggerService logger,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
            : base(logger, mapper, userManager, httpContextAccessor)
        {
        }

        public async Task<IActionResult> Reconcile(ControllerBase controller,
            IRegisterRepository repo, int id)
        {
            try
            {
                string desc = GetControllerDescription(controller);
                _logger.LogInfo(desc);
                if (id < 1)
                {
                    _logger.LogWarn($"{desc}: Empty request submitted");
                    return controller.BadRequest();
                }
                var exists = await repo.Exists(id);
                if (!exists)
                {
                    _logger.LogWarn($"{desc}: Repository with id {id} was not found");
                    return controller.NotFound();
                }

                var isSuccess = await repo.Reconcile(id);
                if (!isSuccess)
                {
                    return InternalError(controller, $"{desc}: Reconcile failed");
                }
                _logger.LogInfo($"{desc}: Reconcile Successful");
                return controller.Ok();
            }
            catch (Exception e)
            {

                return InternalError(controller, $"Server Error: {e.Message} - {e.InnerException}");
            }
        }

        public async Task<IActionResult> Balance(ControllerBase controller,
            IRegisterRepository repo, int id)
        {
            try
            {
                string desc = GetControllerDescription(controller);
                _logger.LogInfo(desc);
                if (id < 1)
                {
                    _logger.LogWarn($"{desc}: Empty request submitted");
                    return controller.BadRequest();
                }
                var exists = await repo.Exists(id);
                if (!exists)
                {
                    _logger.LogWarn($"{desc}: Repository with id {id} was not found");
                    return controller.NotFound();
                }

                var isSuccess = await repo.Balance(id);
                if (!isSuccess)
                {
                    return InternalError(controller, $"{desc}: Balance failed");
                }
                _logger.LogInfo($"{desc}: Balance Successful");
                return controller.Ok();
            }
            catch (Exception e)
            {

                return InternalError(controller, $"Server Error: {e.Message} - {e.InnerException}");
            }
        }
    }
}

