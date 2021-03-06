﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.DataTransfer;
using BudgetPlannerApi.Interfaces;
using BudgetPlannerApi.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgetPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BudgetGroupsController : ControllerBase
    {
        private readonly IBudgetGroupsControllerHelper _controllerHelper;
        private readonly IBudgetGroupRepository _repo;

        public BudgetGroupsController(
            IBudgetGroupsControllerHelper controllerHelper,
            IBudgetGroupRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeRelated"></param>
        /// <param name="limit"></param>
        /// <param name="skip"></param>
        /// <param name="markedForDeletion"></param>
        /// <returns></returns>
        // GET: api/<BudgetGroupsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromQuery] bool includeRelated = false,
            [FromQuery] int limit = 0,
            [FromQuery] int skip = 0,
            [FromQuery] bool? markedForDeletion = null)
        {
            return await _controllerHelper.GetItems<BudgetGroupDTO>(this, _repo,
                new BaseQueryOptions()
                {
                    IncludeRelated = includeRelated,
                    Limit = limit,
                    Skip = skip,
                    MarkedForDeletion = markedForDeletion
                });
        }

        /// <summary>
        /// Get a Budget Group by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET api/<BudgetGroupsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeRelated = false)
        {
            var options = new BaseQueryOptions() { IncludeRelated = includeRelated };
            return await _controllerHelper.GetItem<BudgetGroupDTO>(this, _repo, id, options);
        }

        /// <summary>
        /// Create a new Budget Group
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<BudgetGroupsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BudgetGroupCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<BudgetGroupCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Budget Group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<BudgetGroupsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BudgetGroupCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<BudgetGroupCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Budget Group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<BudgetGroupsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _controllerHelper.DeleteItem(this, _repo, id);
        }
    }
}
