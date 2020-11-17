using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.DataTransfer;
using BudgetPlannerApi.Interfaces;
using BudgetPlannerApi.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetCyclesController : ControllerBase
    {
        private readonly IBudgetCyclesControllerHelper _controllerHelper;
        private readonly IBudgetCycleRepository _repo;

        public BudgetCyclesController(IBudgetCyclesControllerHelper controllerHelper,
            IBudgetCycleRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Budget Cycles
        /// </summary>
        /// <param name="includeRelated"></param>
        /// <param name="limit"></param>
        /// <param name="skip"></param>
        /// <param name="markedForDeletion"></param>
        /// <returns></returns>
        // GET: api/<BudgetCyclesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromQuery] bool includeRelated = false,
            [FromQuery] int limit = 0,
            [FromQuery] int skip = 0,
            [FromQuery] bool? markedForDeletion = null)
        {
            return await _controllerHelper.GetItems<BudgetCycleDTO>(this, _repo, 
                new BaseQueryOptions() 
                { 
                    IncludeRelated = includeRelated,
                    Limit = limit,
                    Skip = skip,
                    MarkedForDeletion = markedForDeletion
                });
        }

        /// <summary>
        /// Get a Budget Cycle by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET api/<BudgetCyclesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeRelated = false)
        {
            return await _controllerHelper.GetItem<BudgetCycleDTO>(this, _repo, id, includeRelated);
        }

        /// <summary>
        /// Create a new Budget Cycle
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<BudgetCyclesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BudgetCycleCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<BudgetCycleCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Budget Cycle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<BudgetCyclesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BudgetCycleCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<BudgetCycleCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Budget Cycle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<BudgetCyclesController>/5
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
