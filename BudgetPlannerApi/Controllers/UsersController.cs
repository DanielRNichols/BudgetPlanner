﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BudgetPlannerApi.Interfaces;
using BudgetPlannerApi.DataTransfer;
using BudgetPlannerApi.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BudgetPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggerService _logger;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;


        public UsersController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILoggerService logger,
            IConfiguration config,
            IUserService userService,
            IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _config = config;
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        [Route("currentuser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            var description = GetControllerDescription();
            _logger.LogInfo($"{description}: Getting info for current user");
            var user = await _userService.GetCurrentUser();
            return Ok(new { userName = user?.UserName, email = user?.Email });

        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] UserDTO userDTO)
        {
            return await Register(userDTO, new List<string>() { Roles.User });
        }

        [HttpPost]
        [Route("registeradmin")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserDTO userDTO)
        {
            return await Register(userDTO, new List<string>() { Roles.Administrator, Roles.User });
        }

        private async Task<IActionResult> Register(UserDTO userDTO, IList<string> roles)
        {
            var description = GetControllerDescription();
            try
            {
                var emailAddress = userDTO.EmailAddress;
                var password = userDTO.Password;
                _logger.LogInfo($"{description}: Registration attempt from user {emailAddress}");
                var user = new IdentityUser { Email = emailAddress, UserName = emailAddress };

                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError($"{description}: {error.Code} {error.Description}");
                    }

                    return InternalError($"{description}: Registration failed user {emailAddress}");
                }

                if (roles != null)
                    await _userManager.AddToRolesAsync(user, roles);

                _logger.LogInfo($"{description}: {emailAddress} successfully registered");
                return Ok(new { user });
            }
            catch (Exception e)
            {
                return InternalError(e);
            }

        }




        /// <summary>
        /// User Login Endpoint
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            var description = GetControllerDescription();
            try
            {
                var emailAddress = userDTO.EmailAddress;
                var password = userDTO.Password;
                _logger.LogInfo($"{description}: Login attempt from user {emailAddress}");
                var result = await _signInManager.PasswordSignInAsync(emailAddress, password, false, false);
                if (result.Succeeded)
                {
                    _logger.LogInfo($"{description}: {emailAddress} successfully authenticated");
                    var user = await _userManager.FindByNameAsync(emailAddress);
                    var token = await _authService.GenerateJWT(user);
                    return Ok(new { token = token });
                }

                _logger.LogInfo($"{description}: {emailAddress} not authenticated");
                return Unauthorized(userDTO);
            }
            catch (Exception e)
            {
                return InternalError(e);
            }
        }

        private string GetControllerDescription()
        {
            return $"{ControllerContext.ActionDescriptor.ControllerName} - " +
                $"{ControllerContext.ActionDescriptor.ActionName}";
        }

        private ObjectResult InternalError(string msg)
        {
            _logger.LogServerError(msg);
            return StatusCode(500, "Server Error");
        }
        private ObjectResult InternalError(Exception e)
        {
            _logger.LogServerError(e);
            return StatusCode(500, "Server Error");
        }

    }
}
