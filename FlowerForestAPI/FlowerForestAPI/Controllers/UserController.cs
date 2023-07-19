using FlowerForestAPI.AuthorizeUserServices;
using FlowerForestAPI.DbContexts;
using FlowerForestAPI.DTOs;
using FlowerForestAPI.Models;
using FlowerForestAPI.Repositories.UserRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Controllers
{
    [Route("api/{controller}")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthorizeUserService authorizeUserService;

        public UserController(IUserRepository userRepository,
            IAuthorizeUserService authorizeUserService)
        {
            this.userRepository = userRepository;
            this.authorizeUserService = authorizeUserService;
        }


        [HttpGet]
        [Authorize] // To be authorized by role
        public IActionResult GetUsers()
        {
            return Ok(userRepository.GetUsers());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id)
        {
            var user = userRepository.GetUserById(id);

            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, ((UserDTO)(user.Data)).Id);

            if (authorizationResult.Succeeded)
                return Ok(user);

            return new ForbidResult();
        }

        [Route("Authenticate")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult AuthenticateUser([FromBody] UserCredentials credentials)
        {
            return Ok(userRepository.AuthenticateUser(credentials));
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody] User user)
        {
            return Ok(userRepository.AddUser(user));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, user.Id);

            if (authorizationResult.Succeeded)
                return Ok(userRepository.UpdateUser(user));

            return new ForbidResult();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] User user)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, user.Id);

            if (authorizationResult.Succeeded)
                return Ok(userRepository.DeleteUser(user));

            return new ForbidResult();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, id);

            if (authorizationResult.Succeeded)
                return Ok(userRepository.DeleteUserById(id));

            return new ForbidResult();
        }
    }
}
