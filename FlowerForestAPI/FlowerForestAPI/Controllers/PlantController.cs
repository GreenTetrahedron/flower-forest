using FlowerForestAPI.AuthorizeUserServices;
using FlowerForestAPI.DbContexts;
using FlowerForestAPI.DTOs;
using FlowerForestAPI.Models;
using FlowerForestAPI.Repositories.CatalogueRepositories;
using FlowerForestAPI.Repositories.PlantRepositories;
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
    [Authorize]
    public class PlantController : ControllerBase
    {
        private readonly IPlantRepository plantRepository;

        private readonly IAuthorizeUserService authorizeUserService;

        public PlantController(IPlantRepository plantRepository,
            IAuthorizeUserService authorizeUserService)
        {
            this.plantRepository = plantRepository;

            this.authorizeUserService = authorizeUserService;
        }

        [HttpGet]
        [Authorize] // To be authorized by roles
        public IActionResult GetPlants()
        {
            return Ok(plantRepository.GetPlants());
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPlantById(Guid id)
        {
            var response = plantRepository.GetPlantById(id);
            var plant = (PlantDTO)(response.Data);

            var catalogueIsPublicAuthorizationResult = await authorizeUserService.AuthorizePublicCatalogueById(User, plant.CatalogueId);

            if (catalogueIsPublicAuthorizationResult.Succeeded)
                return Ok(response);

            var authorizationResult = await authorizeUserService.AuthorizeCatalogueUserId(User, plant.CatalogueId);

            if (authorizationResult.Succeeded)
                return Ok(response);

            return new ForbidResult();
        }

        [Route("Catalogue/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPlantsByCatalogueId([FromRoute] Guid id)
        {
            var catalogueIsPublicAuthorizationResult = await authorizeUserService.AuthorizePublicCatalogueById(User, id);

            if (catalogueIsPublicAuthorizationResult.Succeeded)
                return Ok(plantRepository.GetPlantsByCatalogueId(id));


            var authorizationResult = await authorizeUserService.AuthorizeCatalogueUserId(User, id);

            if (authorizationResult.Succeeded)
                return Ok(plantRepository.GetPlantsByCatalogueId(id));

            return new ForbidResult();
        }

        [HttpPost]
        public async Task<IActionResult> AddPlant([FromBody] Plant plant)
        {
            var authorizationResult = await authorizeUserService.AuthorizeCatalogueUserId(User, plant.CatalogueId);

            if (authorizationResult.Succeeded)
                return Ok(plantRepository.AddPlant(plant));

            return new ForbidResult();
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlant([FromBody] Plant plant)
        {
            var authorizationResult = await authorizeUserService.AuthorizeCatalogueUserId(User, plant.CatalogueId);

            if (authorizationResult.Succeeded)
                return Ok(plantRepository.UpdatePlant(plant));

            return new ForbidResult();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePlant([FromBody] Plant plant)
        {
            var authorizationResult = await authorizeUserService.AuthorizeCatalogueUserId(User, plant.CatalogueId);

            if (authorizationResult.Succeeded)
                return Ok(plantRepository.DeletePlant(plant));

            return new ForbidResult();
        }
    }
}
