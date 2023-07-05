using FlowerForestAPI.DbContexts;
using FlowerForestAPI.Models;
using FlowerForestAPI.Repositories.UserRepositories;
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
        private readonly IUserRepository plantRepository;

        public UserController(IUserRepository plantRepository)
        {
            this.plantRepository = plantRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(plantRepository.GetUsers());
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetUserById(Guid id)
        {
            return Ok(plantRepository.GetUserById(id));
        }

        [HttpPost]
        public IActionResult AddUser(User plant)
        {
            return Ok(plantRepository.AddUser(plant));
        }

        [HttpPut]
        public IActionResult UpdateUser(User plant)
        {
            return Ok(plantRepository.UpdateUser(plant));
        }

        [HttpDelete]
        public IActionResult DeleteUser(User plant)
        {
            return Ok(plantRepository.DeleteUser(plant));
        }
    }
}
