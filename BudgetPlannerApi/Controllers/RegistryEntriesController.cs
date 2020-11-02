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
    public class RegistryEntriesController : ControllerBase
    {
        private readonly IRegistryEntriesControllerHelper _controllerHelper;
        private readonly IRegistryEntryRepository _repo;

        public RegistryEntriesController(IRegistryEntriesControllerHelper controllerHelper,
            IRegistryEntryRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Registry Entries
        /// </summary>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET: api/<RegistryEntriesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] bool includeRelated = false)
        {
            return await _controllerHelper.GetItems<RegistryEntryDTO>(this, _repo, includeRelated);
        }

        /// <summary>
        /// Get a Registry Entry by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET api/<RegistryEntriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeRelated = false)
        {
            return await _controllerHelper.GetItem<RegistryEntryDTO>(this, _repo, id, includeRelated);
        }

        /// <summary>
        /// Create a new Registry Entry
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<RegistryEntriesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] RegistryEntryCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<RegistryEntryCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Registry Entry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<RegistryEntriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] RegistryEntryCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<RegistryEntryCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Registry Entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<RegistryEntriesController>/5
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
