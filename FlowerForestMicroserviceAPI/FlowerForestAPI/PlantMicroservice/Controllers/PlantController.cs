using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantMicroservice.Models;
using PlantMicroservice.Repositories.CatalogueRepositories;
using PlantMicroservice.Repositories.PlantRepositories;

namespace PlantMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlantController : ControllerBase
    {
        private readonly ICatalogueRepository catalogueRepository;
        private readonly IPlantRepository plantRepository;
        private readonly IAuthorizationService authorizationService;

        public PlantController(IPlantRepository plantRepository, ICatalogueRepository catalogueRepository, IAuthorizationService authorizationService)
        {
            this.plantRepository = plantRepository;
            this.authorizationService = authorizationService;
            this.catalogueRepository = catalogueRepository;
        }

        private async Task<IActionResult> IsUserAuthorizedByPolicy(object resource, string policyName, Func<Task<object>> doWhenAuthorized)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, resource, policyName);
            IActionResult result;

            if (!authorizationResult.Succeeded)
            {
                result = User.Identity != null && User.Identity.IsAuthenticated ?
                    new ForbidResult("User may not perform requested action on specified object") :
                    new ChallengeResult("Unauthorized user");
            }
            else
            {
                result = Ok(await doWhenAuthorized.Invoke());
            }


            return result;
        }



        [HttpGet]
        [Route("catalogue/{id}")]
        public async Task<IActionResult> GetPlantsByCatalogueId([FromRoute] Guid id)
        {
            var catalogue = await catalogueRepository.GetCatalogueById(id);

            return await IsUserAuthorizedByPolicy(catalogue.Data, "SameCatalogueAuthorPolicy", async () => await plantRepository.GetPlantsByCatalogueId(id));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePlantById([FromRoute] Guid id)
        {
            var plant = await plantRepository.GetPlantById(id);

            return await IsUserAuthorizedByPolicy(plant.Data, "SamePlantAuthorPolicy", async () => plant);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlant([FromBody] Plant plant)
        {
            return await IsUserAuthorizedByPolicy(plant, "SamePlantAuthorPolicy", async () => await plantRepository.UpdatePlant(plant));
        }

        [HttpPost]
        public async Task<IActionResult> AddPlant([FromBody] Plant plant)
        {
            return await IsUserAuthorizedByPolicy(plant, "SamePlantAuthorPolicy", async () => await plantRepository.AddPlant(plant));
        }
    }
}
