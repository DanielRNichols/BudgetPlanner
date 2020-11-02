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
    public class BudgetItemGroupsController : ControllerBase
    {
        private readonly IBudgetItemGroupsControllerHelper _controllerHelper;
        private readonly IBudgetItemGroupRepository _repo;

        public BudgetItemGroupsController(IBudgetItemGroupsControllerHelper controllerHelper,
            IBudgetItemGroupRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Budget Item Groups
        /// </summary>
        /// <returns></returns>
        // GET: api/<BudgetItemGroupsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return await _controllerHelper.GetItems<BudgetItemGroupDTO>(this, _repo);
        }

        /// <summary>
        /// Get a Budget Item Group by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<BudgetItemGroupsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return await _controllerHelper.GetItem<BudgetItemGroupDTO>(this, _repo, id);
        }

        /// <summary>
        /// Create a new Budget Item Group
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<BudgetItemGroupsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BudgetItemGroupCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<BudgetItemGroupCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Budget Item Group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<BudgetItemGroupsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BudgetItemGroupCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<BudgetItemGroupCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Budget Item Group
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<BudgetItemGroupsController>/5
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
