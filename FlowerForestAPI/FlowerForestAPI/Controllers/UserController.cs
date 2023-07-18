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
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthorizationService authorizationService;

        public UserController(IUserRepository userRepository,
            IAuthorizationService authorizationService)
        {
            this.userRepository = userRepository;
            this.authorizationService = authorizationService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(userRepository.GetUsers());
        }

        [Route("{id}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            var user = userRepository.GetUserById(id);

            var authorizationResult = await authorizationService.AuthorizeAsync(User, new User() { Id = ((UserDTO)(user.Data)).Id }, "CreatorOnlyPolicy");

            if (authorizationResult.Succeeded)
            {
                return Ok(user);
            }

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
        public IActionResult AddUser(User user)
        {
            return Ok(userRepository.AddUser(user));
        }

        [HttpPut]
        public IActionResult UpdateUser(User user)
        {
            return Ok(userRepository.UpdateUser(user));
        }

        [HttpDelete]
        public IActionResult DeleteUser(User user)
        {
            return Ok(userRepository.DeleteUser(user));
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteUserById(Guid id)
        {
            return Ok(userRepository.DeleteUserById(id));
        }
    }
}
