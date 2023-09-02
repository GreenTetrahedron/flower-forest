using CatalogueMicroservice.Models;
using CatalogueMicroservice.Models.DTOs;
using CatalogueMicroservice.Repositories.CatalogueRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogueMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CatalogueController : ControllerBase
    {
        private readonly ICatalogueRepository catalogueRepository;
        private readonly IAuthorizationService authorizationService;

        public CatalogueController(ICatalogueRepository catalogueRepository, IAuthorizationService authorizationService)
        {
            this.catalogueRepository = catalogueRepository;
            this.authorizationService = authorizationService;
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


        //[HttpGet] // TODO: AUTHORIZE WITH ROLES
        //public async Task<IActionResult> GetCatalogues()
        //{
        //    return Ok(await catalogueRepository.GetCatalogues());
        //}

        [HttpGet]
        [Route("public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPublicCatalogues()
        {
            return Ok(await catalogueRepository.GetPublicCatalogues());
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCatalogueById(Guid id)
        {
            var catalogue = await catalogueRepository.GetCatalogueById(id);

            return await IsUserAuthorizedByPolicy(catalogue.Data, "SameCatalogueAuthorOrCatalogueIsPublic", async () => catalogue);
        }


        [HttpGet]
        [Route("User/{id}")]
        public async Task<IActionResult> GetCataloguesByUserId(Guid id)
        {
            return await IsUserAuthorizedByPolicy(new Catalogue { UserId = id }, "SameCatalogueAuthor",
                async () => await catalogueRepository.GetCataloguesByUserId(id));
        }


        [HttpPost]
        public async Task<IActionResult> AddCatalogue([FromBody] CatalogueDTO catalogue)
        {
            return await IsUserAuthorizedByPolicy(new Catalogue { UserId = catalogue.UserId }, "SameCatalogueAuthor",
                async () => await catalogueRepository.AddCatalogue(catalogue));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCatalogue([FromBody] Catalogue catalogue)
        {
            return await IsUserAuthorizedByPolicy(catalogue, "SameCatalogueAuthor",
                async () => await catalogueRepository.UpdateCatalogue(catalogue));
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCatalogueById(Guid id)
        {
            var catalogue = await catalogueRepository.GetCatalogueById(id);
            return await IsUserAuthorizedByPolicy(catalogue.Data, "SameCatalogueAuthor",
                async () => await catalogueRepository.DeleteCatalogueById(id));
        }
    }
}