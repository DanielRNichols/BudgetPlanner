﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BudgetPlanner.Interfaces;
using BudgetPlannerApi.Data;
using BudgetPlannerApi.DataTransfer;
using BudgetPlannerApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BudgetPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetCyclesController : ControllerBase
    {
        private readonly IBudgetCycleRepository _budgetItemRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BudgetCyclesController(
            IBudgetCycleRepository budgetItemRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _budgetItemRepository = budgetItemRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Budget Cycles
        /// </summary>
        /// <returns></returns>
        // GET: api/<BudgetCyclesController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                _logger.LogInfo("GetItems");
                var items = await _budgetItemRepository.GetAll();
                var response = _mapper.Map<IList<BudgetCycleDTO>>(items);
                _logger.LogInfo("Successfully retrieved Items");

                return Ok(response);
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        /// <summary>
        /// Get Budget Cycle by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<BudgetCyclesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItem(int id)
        {
            try
            {
                _logger.LogInfo($"GetItem by id: {id}");
                var item = await _budgetItemRepository.GetById(id);
                if (item == null)
                {
                    _logger.LogWarn($"Item not found: {id}");

                    return NotFound();
                }
                var response = _mapper.Map<BudgetCycleDTO>(item);
                _logger.LogInfo($"Successfully retrieved Item {id}");

                return Ok(response);
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        /// <summary>
        /// Create a new Budget Cycle
        /// </summary>
        /// <param name="bugetItemTypeDTO"></param>
        /// <returns></returns>
        // POST api/<BudgetCyclesController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BudgetCycleCreateDTO bugetItemTypeDTO)
        {
            try
            {
                _logger.LogInfo("Attempted to create Item");
                if (bugetItemTypeDTO == null)
                {
                    _logger.LogWarn($"Empty request submitted");
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Invalid request submitted");
                    return BadRequest(ModelState);
                }
                var budgetItem = _mapper.Map<BudgetCycle>(bugetItemTypeDTO);

                var isSuccess = await _budgetItemRepository.Create(budgetItem);
                if (!isSuccess)
                {
                    return ServerError("Item creation failed");
                }
                _logger.LogInfo("Item created");
                return Created("", new { budgetItem });
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        /// <summary>
        /// Update a Budget Cycle
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bugetItemTypeDTO"></param>
        /// <returns></returns>
        // PUT api/<BudgetCyclesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BudgetCycleCreateDTO bugetItemTypeDTO)
        {
            try
            {
                _logger.LogInfo("Attempted to update Item");
                if (id < 1)
                {
                    _logger.LogWarn($"Invalid id submitted: {id}");
                    return BadRequest();
                }

                if (bugetItemTypeDTO == null)
                {
                    _logger.LogWarn($"Empty request submitted");
                    return BadRequest();
                }
                var exists = await _budgetItemRepository.Exists(id);
                if (!exists)
                {
                    _logger.LogWarn($"Item with id {id} was not found");
                    return NotFound();
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarn($"Invalid request submitted");
                    return BadRequest(ModelState);
                }

                var item = _mapper.Map<BudgetCycle>(bugetItemTypeDTO);
                // force item.Id to be id passed in 
                item.Id = id;

                var isSuccess = await _budgetItemRepository.Update(item);
                if (!isSuccess)
                {
                    return ServerError("Item update failed");
                }
                _logger.LogInfo("Item updated");
                return Ok(new { item });
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        /// <summary>
        /// Delete a Budget Cycle
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
            try
            {
                _logger.LogInfo("Attempted to delete Item");
                if (id < 1)
                {
                    _logger.LogWarn($"Empty request submitted");
                    return BadRequest();
                }
                var item = await _budgetItemRepository.GetById(id);
                if (item == null)
                {
                    _logger.LogWarn($"Item with id ${id} was not found");
                    return NotFound();
                }

                var isSuccess = await _budgetItemRepository.Delete(item);
                if (!isSuccess)
                {
                    return ServerError("Item delete failed");
                }
                _logger.LogInfo("Item deleted");
                return Ok(id);
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        private ObjectResult ServerError(string msg)
        {
            _logger.LogServerError(msg);
            return StatusCode(500, "Server Error");
        }
        private ObjectResult ServerError(Exception e)
        {
            _logger.LogServerError(e);
            return StatusCode(500, "Server Error");
        }
    }
}