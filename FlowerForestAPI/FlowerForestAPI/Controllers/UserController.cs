using FlowerForestAPI.DbContexts;
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

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(userRepository.GetUsers());
        }

        [Route("{id}")]
        [HttpGet]
        [Authorize]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(userRepository.GetUserById(id));
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
    }
}
