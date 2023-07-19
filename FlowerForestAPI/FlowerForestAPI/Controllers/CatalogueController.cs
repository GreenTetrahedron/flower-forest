using FlowerForestAPI.AuthorizeUserServices;
using FlowerForestAPI.DTOs;
using FlowerForestAPI.Models;
using FlowerForestAPI.Repositories.CatalogueRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Controllers
{
    [Route("api/{controller}")]
    [Authorize]
    public class CatalogueController : ControllerBase
    {
        private readonly ICatalogueRepository catalogueRepository;
        private readonly IAuthorizeUserService authorizeUserService;

        public CatalogueController(ICatalogueRepository catalogueRepository,
            IAuthorizeUserService authorizeUserService)
        {
            this.catalogueRepository = catalogueRepository;
            this.authorizeUserService = authorizeUserService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateCatalogue([FromBody] Catalogue catalogue)
        {
            return Ok(catalogueRepository.AddCatalogue(catalogue));
        }

        [HttpGet]
        [Authorize] // To be authorized with roles
        public IActionResult GetCatalogues()
        {
            return Ok(catalogueRepository.GetCatalogues());
        }

        [Route("User/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetCataloguesByUserId([FromRoute] Guid userId)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, userId);

            if (authorizationResult.Succeeded)
                return Ok(catalogueRepository.GetCataloguesByUserId(userId));

            return new ForbidResult();

        }

        [Route("/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCatalogueById([FromRoute] Guid id)
        {
            var response = catalogueRepository.GetCatalogueById(id);

            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, ((CatalogueDTO)(response.Data)).UserId);

            if (authorizationResult.Succeeded)
                return Ok(response);

            return new ForbidResult();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCatalogue([FromBody] Catalogue catalogue)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, catalogue.UserId);

            if (authorizationResult.Succeeded)
                return Ok(catalogueRepository.UpdateCatalogue(catalogue));

            return new ForbidResult();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCatalogue([FromBody] Catalogue catalogue)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, catalogue.UserId);

            if (authorizationResult.Succeeded)
                return Ok(catalogueRepository.DeleteCatalogue(catalogue));

            return new ForbidResult();
        }
    }
}
