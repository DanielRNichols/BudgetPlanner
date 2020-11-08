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
    public class RegisterEntriesController : ControllerBase
    {
        private readonly IRegisterEntriesControllerHelper _controllerHelper;
        private readonly IRegisterEntryRepository _repo;

        public RegisterEntriesController(IRegisterEntriesControllerHelper controllerHelper,
            IRegisterEntryRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Register Entries
        /// </summary>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET: api/<RegisterEntriesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] bool includeRelated = false)
        {
            return await _controllerHelper.GetItems<RegisterEntryDTO>(this, _repo, 
                new BaseQueryOptions() { IncludeRelated = includeRelated });
        }

        /// <summary>
        /// Get a Register Entry by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET api/<RegisterEntriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeRelated = false)
        {
            return await _controllerHelper.GetItem<RegisterEntryDTO>(this, _repo, id, 
                new BaseQueryOptions() { IncludeRelated = includeRelated });
        }

        /// <summary>
        /// Create a new Register Entry
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<RegisterEntriesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] RegisterEntryCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<RegisterEntryCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Register Entry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<RegisterEntriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterEntryCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<RegisterEntryCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Register Entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<RegisterEntriesController>/5
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
