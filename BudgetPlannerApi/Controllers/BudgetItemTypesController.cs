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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgetPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetItemTypesController : ControllerBase
    {
        private readonly IBudgetItemTypesControllerHelper _controllerHelper;
        private readonly IBudgetItemTypeRepository _repo;

        public BudgetItemTypesController(
            IBudgetItemTypesControllerHelper controllerHelper,
            IBudgetItemTypeRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Budget Item Types
        /// </summary>
        /// <returns></returns>
        // GET: api/<BudgetItemTypesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return await _controllerHelper.GetItems<BudgetItemTypeDTO>(this, _repo);
        }

        /// <summary>
        /// Get a Budget Item Type by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<BudgetItemTypesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return await _controllerHelper.GetItem<BudgetItemTypeDTO>(this, _repo, id);
        }

        /// <summary>
        /// Create a new Budget Item Type
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<BudgetItemTypesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BudgetItemTypeCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<BudgetItemTypeCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Budget Item Type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<BudgetItemTypesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BudgetItemTypeCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<BudgetItemTypeCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Budget Item Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<BudgetItemTypesController>/5
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
