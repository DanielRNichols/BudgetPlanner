using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.DataTransfer;
using BudgetPlannerApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetCycleItemsController : ControllerBase
    {
        private readonly IBudgetCycleItemsControllerHelper _controllerHelper;
        private readonly IBudgetCycleItemRepository _repo;

        public BudgetCycleItemsController(IBudgetCycleItemsControllerHelper controllerHelper,
            IBudgetCycleItemRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Budget Cycles Items
        /// </summary>
        /// <returns></returns>
        // GET: api/<BudgetCyclesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] bool includeRelated = false)
        {
            return await _controllerHelper.GetItems<BudgetCycleItemDTO>(this, _repo, includeRelated);
        }

        /// <summary>
        /// Get a Budget Cycle Item by Id
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
            return await _controllerHelper.GetItem<BudgetCycleItemDTO>(this, _repo, id, includeRelated);
        }

        /// <summary>
        /// Create a new Budget Cycle Item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<BudgetCyclesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BudgetCycleItemCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<BudgetCycleItemCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Budget Cycle Item
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
        public async Task<IActionResult> Update(int id, [FromBody] BudgetCycleItemCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<BudgetCycleItemCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Budget Cycle Item
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
