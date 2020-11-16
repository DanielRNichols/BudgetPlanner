﻿using AutoMapper;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlannerApi.Services.ControllerHelpers
{
    public class DbResourceControllerHelper<T,O> : 
        IDbResourceControllerHelper<T,O> where T : class, IDbResource where O : class, IBaseQueryOptions
    {
        protected readonly ILoggerService _logger;
        protected readonly IMapper _mapper;

        public DbResourceControllerHelper(ILoggerService logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Items
        /// </summary>
        /// <typeparam name="D">Data Transfer Object class</typeparam>
        /// <param name="controller"></param>
        /// <param name="repo"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<ObjectResult> GetItems<D>(ControllerBase controller, IDbResourceRepository<T,O> repo, O options = null)
        {
            try
            {
                string desc = GetControllerDescription(controller);
                _logger.LogInfo(desc);
                var items = await repo.Get(options);
                var response = _mapper.Map<IList<D>>(items);
                _logger.LogInfo($"{desc} - Successful");

                return controller.Ok(response);
            }
            catch (Exception e)
            {
                return InternalError(controller, e);
            }
        }

        /// <summary>
        /// Get Item by Id
        /// </summary>
        /// <typeparam name="D">Data Transfer Object class</typeparam>
        /// <param name="controller"></param>
        /// <param name="repo"></param>
        /// <param name="id"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetItem<D>(ControllerBase controller, 
            IDbResourceRepository<T,O> repo, int id, bool includeRelated)
        {
            try
            {
                string desc = GetControllerDescription(controller);
                _logger.LogInfo($"{desc}: {id}");
                var item = await repo.GetById(id, includeRelated);
                if (item == null)
                {
                    _logger.LogWarn($"{desc}: Item not found: {id}");
                    return controller.NotFound();
                }
                var response = _mapper.Map<D>(item);
                _logger.LogInfo($"{desc} - Successful");
                return controller.Ok(response);
            }
            catch (Exception e)
            {
                return InternalError(controller, $"Server Error: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Create Item
        /// </summary>
        /// <typeparam name="D">Data Transfer Object class</typeparam>
        /// <param name="controller"></param>
        /// <param name="repo"></param>
        /// <param name="itemDTO"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateItem<D>(ControllerBase controller, 
            IDbResourceRepository<T,O> repo, D itemDTO)
        {
            string desc = GetControllerDescription(controller);
            try
            {
                _logger.LogInfo(desc);
                if (itemDTO == null)
                {
                    _logger.LogWarn($"{desc}: Empty request submitted");
                    return controller.BadRequest();
                }
                if (!controller.ModelState.IsValid)
                {
                    _logger.LogWarn($"{desc}: Invalid request submitted");
                    return controller.BadRequest(controller.ModelState);
                }
                var item = _mapper.Map<T>(itemDTO);

                var isSuccess = await repo.Create(item);
                if (!isSuccess)
                {
                    return InternalError(controller, $"{desc}: Item creation failed");
                }
                _logger.LogInfo($"{desc}: Item created");
                return controller.Created("", new { item });
            }
            catch (Exception e)
            {
                return InternalError(controller, e);
            }
        }

        public async Task<IActionResult> UpdateItem<D>(ControllerBase controller, 
            IDbResourceRepository<T,O> repo, int id, D itemDTO)
        {
            try
            {
                string desc = GetControllerDescription(controller);
                _logger.LogInfo(desc);
                if (id < 1 || itemDTO == null)
                {
                    _logger.LogWarn($"{desc}: Empty request submitted");
                    return controller.BadRequest();
                }
                var exists = await repo.Exists(id);
                if (!exists)
                {
                    _logger.LogWarn($"{desc}: Item with id {id} was not found");
                    return controller.NotFound();
                }
                if (!controller.ModelState.IsValid)
                {
                    _logger.LogWarn($"{desc}: Invalid request submitted");
                    return controller.BadRequest(controller.ModelState);
                }

                var item = _mapper.Map<T>(itemDTO);
                // force item.Id to be id passed in 
                item.Id = id;

                var isSuccess = await repo.Update(item);
                if (!isSuccess)
                {
                    return InternalError(controller, $"{desc}: Update failed");
                }
                _logger.LogInfo($"{desc}: Update Successful");
                return controller.Ok(new { item });
            }
            catch (Exception e)
            {

                return InternalError(controller, $"Server Error: {e.Message} - {e.InnerException}");
            }
        }

        public async Task<IActionResult> DeleteItem(ControllerBase controller,
            IDbResourceRepository<T,O> repo, int id)
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
                var item = await repo.GetById(id, false);
                if (item == null)
                {
                    _logger.LogWarn($"{desc}: Item with id ${id} was not found");
                    return controller.NotFound();
                }

                var isSuccess = await repo.Delete(item);
                if (!isSuccess)
                {
                    return InternalError(controller, "{desc}: Item delete failed");
                }
                _logger.LogInfo($"{desc}: Item deleted");
                return controller.Ok(id);
            }
            catch (Exception e)
            {

                return InternalError(controller, e);
            }
        }

        protected string GetControllerDescription(ControllerBase controller)
        {
            return $"{controller.ControllerContext.ActionDescriptor.ControllerName} - " +
                $"{controller.ControllerContext.ActionDescriptor.ActionName}";
        }

        protected ObjectResult InternalError(ControllerBase controller, string msg)
        {
            _logger.LogServerError(msg);
            ServerError serverError = new ServerError
            {
                Error = $"Server Error: {msg}",
                Details = ""
            };
            return controller.StatusCode(500, new { serverError });
        }
        protected ObjectResult InternalError(ControllerBase controller, Exception e)
        {
            _logger.LogServerError(e);
            ServerError serverError = new ServerError
            {
                Error = e.Message,
                Details = e.InnerException?.ToString()
            };
            return controller.StatusCode(500, new { serverError});
        }

    }
}
