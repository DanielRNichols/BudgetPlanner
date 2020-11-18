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
    public class RegistersController : ControllerBase
    {
        private readonly IRegistersControllerHelper _controllerHelper;
        private readonly IRegisterRepository _repo;

        public RegistersController(IRegistersControllerHelper controllerHelper,
            IRegisterRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Registers
        /// </summary>
        /// <param name="includeRelated"></param>
        /// <param name="limit"></param>
        /// <param name="skip"></param>
        /// <param name="markedForDeletion"></param>
        /// <returns></returns>
        // GET: api/<RegistersController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromQuery] bool includeRelated = false,
            [FromQuery] int limit = 0,
            [FromQuery] int skip = 0,
            [FromQuery] bool? markedForDeletion = null)
        {
            return await _controllerHelper.GetItems<RegisterDTO>(this, _repo,
                new BaseQueryOptions()
                {
                    IncludeRelated = includeRelated,
                    Limit = limit,
                    Skip = skip,
                    MarkedForDeletion = markedForDeletion
                });
        }

        /// <summary>
        /// Get a Register by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET api/<RegistersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeRelated = false)
        {
            var options = new BaseQueryOptions() { IncludeRelated = includeRelated };
            return await _controllerHelper.GetItem<RegisterDTO>(this, _repo, id, options);
        }

        /// <summary>
        /// Create a new Register
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<RegistersController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] RegisterCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<RegisterCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Register
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<RegistersController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<RegisterCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Register
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<RegistersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            return await _controllerHelper.DeleteItem(this, _repo, id);
        }


        /// <summary>
        /// Reconcile register with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST api/<RegistersController>
        [HttpPost("{id}/reconcile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Reconcile(int id)
        {
            return await _controllerHelper.Reconcile(this, _repo, id);
        }

        /// <summary>
        /// Balance register with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST api/<RegistersController>
        [HttpPost("{id}/balance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Balance(int id)
        {
            return await _controllerHelper.Balance(this, _repo, id);
        }

    }
}

