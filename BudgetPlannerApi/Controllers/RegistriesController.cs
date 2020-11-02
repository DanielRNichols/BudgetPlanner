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
    public class RegistriesController : ControllerBase
    {
        private readonly IRegistriesControllerHelper _controllerHelper;
        private readonly IRegistryRepository _repo;

        public RegistriesController(IRegistriesControllerHelper controllerHelper,
            IRegistryRepository repo)
        {
            _controllerHelper = controllerHelper;
            _repo = repo;
        }

        /// <summary>
        /// Get all Registries
        /// </summary>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET: api/<RegistrysController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] bool includeRelated = false)
        {
            return await _controllerHelper.GetItems<RegistryDTO>(this, _repo, includeRelated);
        }

        /// <summary>
        /// Get a Registry by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeRelated"></param>
        /// <returns></returns>
        // GET api/<RegistrysController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, [FromQuery] bool includeRelated = false)
        {
            return await _controllerHelper.GetItem<RegistryDTO>(this, _repo, id, includeRelated);
        }

        /// <summary>
        /// Create a new Registry
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/<RegistrysController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] RegistryCreateDTO dto)
        {
            return await _controllerHelper.CreateItem<RegistryCreateDTO>(this, _repo, dto);
        }

        /// <summary>
        /// Update a Registry
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        // PUT api/<RegistrysController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] RegistryCreateDTO dto)
        {
            return await _controllerHelper.UpdateItem<RegistryCreateDTO>(this, _repo, id, dto);
        }

        /// <summary>
        /// Delete a Registry
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<RegistrysController>/5
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

