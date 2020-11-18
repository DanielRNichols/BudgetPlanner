using System;
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

namespace BudgetPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MemorizedTransactionsController : ControllerBase
    {
        private readonly IMemorizedTransactionsControllerHelper _controllerHelper;
        private readonly IMemorizedTransactionRepository _repo;

        public MemorizedTransactionsController(IMemorizedTransactionsControllerHelper controllerHelper,
            IMemorizedTransactionRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Memorized Transactions
        /// </summary>
        /// <param name="includeRelated"></param>
        /// <param name="limit"></param>
        /// <param name="skip"></param>
        /// <param name="markedForDeletion"></param>
        /// <returns></returns>
        // GET: api/<MemorizedTransactionsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromQuery] bool includeRelated = false,
            [FromQuery] int limit = 0,
            [FromQuery] int skip = 0,
            [FromQuery] bool? markedForDeletion = null)
        {
            return await _controllerHelper.GetItems<MemorizedTransactionDTO>(this, _repo, new BaseQueryOptions()
            {
                IncludeRelated = includeRelated,
                Limit = limit,
                Skip = skip,
                MarkedForDeletion = markedForDeletion
            });
        }

        /// <summary>
        /// Get a Memorized Transaction by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET api/<MemorizedTransactionsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeRelated = false)
        {
            var options = new BaseQueryOptions() { IncludeRelated = includeRelated };
            return await _controllerHelper.GetItem<MemorizedTransactionDTO>(this, _repo, id, options);
        }

        /// <summary>
        /// Create a new Memorized Transaction
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<MemorizedTransactionsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] MemorizedTransactionCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<MemorizedTransactionCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Memorized Transaction
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<MemorizedTransactionsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] MemorizedTransactionCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<MemorizedTransactionCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Memorized Transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<MemorizedTransactionsController>/5
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
