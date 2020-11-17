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
    public class RegisterSplitEntriesController : ControllerBase
    {
        private readonly IRegisterSplitEntriesControllerHelper _controllerHelper;
        private readonly IRegisterSplitEntryRepository _repo;

        public RegisterSplitEntriesController(IRegisterSplitEntriesControllerHelper controllerHelper,
            IRegisterSplitEntryRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Register Entries
        /// </summary>
        /// <param name="includeRelated"></param>
        /// <param name="limit"></param>
        /// <param name="skip"></param>
        /// <param name="markedForDeletion"></param>
        /// <returns></returns>
        // GET: api/<RegisterSplitEntriesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromQuery] bool includeRelated = false,
            [FromQuery] int limit = 0,
            [FromQuery] int skip = 0,
            [FromQuery] bool? markedForDeletion = null)
        {
            return await _controllerHelper.GetItems<RegisterSplitEntryDTO>(this, _repo,
                new BaseQueryOptions()
                {
                    IncludeRelated = includeRelated,
                    Limit = limit,
                    Skip = skip,
                    MarkedForDeletion = markedForDeletion
                });
        }

        /// <summary>
        /// Get a Register Entry by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET api/<RegisterSplitEntriesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeRelated = false)
        {
            return await _controllerHelper.GetItem<RegisterSplitEntryDTO>(this, _repo, id, includeRelated);
        }

        /// <summary>
        /// Create a new Register Entry
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<RegisterSplitEntriesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] RegisterSplitEntryCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<RegisterSplitEntryCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Register Entry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<RegisterSplitEntriesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterSplitEntryCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<RegisterSplitEntryCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Register Entry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<RegisterSplitEntriesController>/5
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
