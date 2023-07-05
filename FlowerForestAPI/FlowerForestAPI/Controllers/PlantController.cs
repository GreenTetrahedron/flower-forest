using FlowerForestAPI.DbContexts;
using FlowerForestAPI.Models;
using FlowerForestAPI.Repositories.PlantRepositories;
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
    public class PlantController : ControllerBase
    {
        private readonly IPlantRepository plantRepository;

        public PlantController(IPlantRepository plantRepository)
        {
            this.plantRepository = plantRepository;
        }

        [HttpGet]
        public IActionResult GetPlants()
        {
            return Ok(plantRepository.GetPlants());
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetPlantById(Guid id)
        {
            return Ok(plantRepository.GetPlantById(id));
        }

        [HttpPost]
        public IActionResult AddPlant(Plant plant)
        {            
            return Ok(plantRepository.AddPlant(plant));
        }

        [HttpPut]
        public IActionResult UpdatePlant(Plant plant)
        {
            return Ok(plantRepository.UpdatePlant(plant));
        }

        [HttpDelete]
        public IActionResult DeletePlant(Plant plant)
        {
            return Ok(plantRepository.DeletePlant(plant));
        }
    }
}
