using FlowerForestAPI.Models;
using FlowerForestAPI.Repositories.CatalogueRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Controllers
{
    [Route("api/{controller}")]
    public class CatalogueController : ControllerBase
    {
        private readonly ICatalogueRepository catalogueRepository;

        public CatalogueController(ICatalogueRepository catalogueRepository)
        {
            this.catalogueRepository = catalogueRepository;
        }

        [HttpPost]
        public IActionResult CreateCataloguedPlant([FromBody] Catalogue catalogue)
        {
            return Ok(catalogueRepository.AddCatalogue(catalogue));
        }

        [HttpGet]
        public IActionResult GetCataloguedPlants()
        {
            return Ok(catalogueRepository.GetCatalogues());
        }

        [Route("/Catalogue/{userId}")]
        [HttpGet]
        public IActionResult GetCataloguedPlantsByUserId([FromRoute] Guid userId)
        {
            return Ok(catalogueRepository.GetCataloguesByUserId(userId));
        }

        [Route("/{id}")]
        [HttpGet]
        public IActionResult GetCataloguedPlantById([FromRoute] Guid id)
        {
            return Ok(catalogueRepository.GetCatalogueById(id));
        }

        [HttpPut]
        public IActionResult UpdateCataloguedPlant([FromBody] Catalogue catalogue)
        {
            return Ok(catalogueRepository.UpdateCatalogue(catalogue));
        }

        [HttpDelete]
        public IActionResult DeleteCataloguedPlant([FromBody] Catalogue catalogue)
        {
            return Ok(catalogueRepository.DeleteCatalogue(catalogue));
        }
    }
}
