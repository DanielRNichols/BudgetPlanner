using System;
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
    public class BudgetItemGroupsController : ControllerBase
    {
        private readonly IBudgetItemGroupRepository _budgetItemGroupRepository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BudgetItemGroupsController(
            IBudgetItemGroupRepository budgetItemGroupRepository,
            ILoggerService logger,
            IMapper mapper)
        {
            _budgetItemGroupRepository = budgetItemGroupRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all Budget Item Groups
        /// </summary>
        /// <returns></returns>
        // GET: api/<BudgetItemGroupsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItemTypes()
        {
            try
            {
                _logger.LogInfo("GetItems");
                var items = await _budgetItemGroupRepository.GetAll();
                var response = _mapper.Map<IList<BudgetItemGroupDTO>>(items);
                _logger.LogInfo("Successfully retrieved Items");

                return Ok(response);
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        /// <summary>
        /// Get Budget Item Group by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<BudgetItemGroupsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetItemType(int id)
        {
            try
            {
                _logger.LogInfo($"GetItem by id: {id}");
                var item = await _budgetItemGroupRepository.GetById(id);
                if (item == null)
                {
                    _logger.LogWarn($"Item not found: {id}");

                    return NotFound();
                }
                var response = _mapper.Map<BudgetItemGroupDTO>(item);
                _logger.LogInfo($"Successfully retrieved Item {id}");

                return Ok(response);
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        /// <summary>
        /// Create a new Budget Item Group
        /// </summary>
        /// <param name="bugetItemTypeDTO"></param>
        /// <returns></returns>
        // POST api/<BudgetItemGroupsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] BudgetItemGroupCreateDTO bugetItemTypeDTO)
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
                var budgetItemGroup = _mapper.Map<BudgetItemGroup>(bugetItemTypeDTO);

                var isSuccess = await _budgetItemGroupRepository.Create(budgetItemGroup);
                if (!isSuccess)
                {
                    return ServerError("Item creation failed");
                }
                _logger.LogInfo("Item created");
                return Created("", new { budgetItemGroup });
            }
            catch (Exception e)
            {
                return ServerError(e);
            }
        }

        /// <summary>
        /// Update a Budget Item Group
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bugetItemTypeDTO"></param>
        /// <returns></returns>
        // PUT api/<BudgetItemGroupsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] BudgetItemGroupCreateDTO bugetItemTypeDTO)
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
                var exists = await _budgetItemGroupRepository.Exists(id);
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

                var item = _mapper.Map<BudgetItemGroup>(bugetItemTypeDTO);
                // force item.Id to be id passed in 
                item.Id = id;

                var isSuccess = await _budgetItemGroupRepository.Update(item);
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
            try
            {
                _logger.LogInfo("Attempted to delete Item");
                if (id < 1)
                {
                    _logger.LogWarn($"Empty request submitted");
                    return BadRequest();
                }
                var item = await _budgetItemGroupRepository.GetById(id);
                if (item == null)
                {
                    _logger.LogWarn($"Item with id ${id} was not found");
                    return NotFound();
                }

                var isSuccess = await _budgetItemGroupRepository.Delete(item);
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
