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
    public class BudgetItemsController : ControllerBase
    {
        private readonly IBudgetItemsControllerHelper _controllerHelper;
        private readonly IBudgetItemRepository _repo;

        public BudgetItemsController(IBudgetItemsControllerHelper controllerHelper,
            IBudgetItemRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Budget Items
        /// </summary>
        /// <returns></returns>
        // GET: api/<BudgetItemsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return await _controllerHelper.GetItems<BudgetItemDTO>(this, _repo);
        }

        /// <summary>
        /// Get a Budget Item by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<BudgetItemsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return await _controllerHelper.GetItem<BudgetItemDTO>(this, _repo, id);
        }

        /// <summary>
        /// Create a new Budget Item
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<BudgetItemsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BudgetItemCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<BudgetItemCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Budget Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<BudgetItemsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BudgetItemCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<BudgetItemCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Budget Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<BudgetItemsController>/5
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
