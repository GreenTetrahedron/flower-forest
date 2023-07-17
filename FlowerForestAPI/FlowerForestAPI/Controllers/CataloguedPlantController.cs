using FlowerForestAPI.Models;
using FlowerForestAPI.Repositories.CataloguedPlantRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Controllers
{
    [Route("api/{controller}")]
    public class CataloguedPlantController : ControllerBase
    {
        private readonly ICataloguedPlantRepository cataloguedPlantRepository;

        public CataloguedPlantController(ICataloguedPlantRepository cataloguedPlantRepository)
        {
            this.cataloguedPlantRepository = cataloguedPlantRepository;
        }

        [HttpPost]
        public IActionResult CreateCataloguedPlant([FromBody] Catalogue plant)
        {
            return Ok(cataloguedPlantRepository.AddCataloguedPlant(plant));
        }

        [HttpGet]
        public IActionResult GetCataloguedPlants()
        {
            return Ok(cataloguedPlantRepository.GetCataloguedPlants());
        }

        [Route("/Catalogue/{userId}")]
        [HttpGet]
        public IActionResult GetCataloguedPlantsByUserId([FromRoute] Guid userId)
        {
            return Ok(cataloguedPlantRepository.GetCataloguedPlantsByUserId(userId));
        }

        [Route("/{id}")]
        [HttpGet]
        public IActionResult GetCataloguedPlantById([FromRoute] Guid id)
        {
            return Ok(cataloguedPlantRepository.GetCataloguedPlantById(id));
        }

        [HttpPut]
        public IActionResult UpdateCataloguedPlant([FromBody] Catalogue plant)
        {
            return Ok(cataloguedPlantRepository.UpdateCataloguedPlant(plant));
        }

        [HttpDelete]
        public IActionResult DeleteCataloguedPlant([FromBody] Catalogue plant)
        {
            return Ok(cataloguedPlantRepository.DeleteCataloguedPlant(plant));
        }
    }
}
