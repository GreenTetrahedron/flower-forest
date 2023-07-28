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
        [Authorize]
        public async Task<IActionResult> CreateCatalogue([FromBody] CatalogueDTO catalogue)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, catalogue.UserId);

            if (authorizationResult.Succeeded)
                return Ok(catalogueRepository.AddCatalogue(catalogue));

            return NotFound();
        }

        [HttpGet]
        [Authorize] // To be authorized with roles
        public IActionResult GetCatalogues()
        {
            return Ok(catalogueRepository.GetCatalogues());
        }

        [Route("Public")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetPublicCatalogues()
        {
            return Ok(catalogueRepository.GetPublicCatalogues());
        }

        [Route("User/{userId}")]
        [HttpGet]
        public async Task<IActionResult> GetCataloguesByUserId([FromRoute] Guid userId)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, userId);

            if (authorizationResult.Succeeded)
                return Ok(catalogueRepository.GetCataloguesByUserId(userId));

            return NotFound();

        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCatalogueById([FromRoute] Guid id)
        {
            var response = catalogueRepository.GetCatalogueById(id);
            var catalogue = (CatalogueDTO)response.Data;

            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, catalogue.UserId);
            var publicCatalogueAuthorizationResult = await authorizeUserService.AuthorizePublicCatalogueById(User, id);

            if (publicCatalogueAuthorizationResult.Succeeded || authorizationResult.Succeeded)
                return Ok(response);

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCatalogue([FromBody] Catalogue catalogue)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, catalogue.UserId);
            var publicCatalogueAuthorizationResult = await authorizeUserService.AuthorizePublicCatalogueById(User, catalogue.Id);

            if (authorizationResult.Succeeded)
                return Ok(catalogueRepository.UpdateCatalogue(catalogue));

            if (publicCatalogueAuthorizationResult.Succeeded)
                return new ForbidResult();

            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCatalogue([FromBody] Catalogue catalogue)
        {
            var authorizationResult = await authorizeUserService.AuthorizeUserId(User, catalogue.UserId);
            var publicCatalogueAuthorizationResult = await authorizeUserService.AuthorizePublicCatalogueById(User, catalogue.Id);

            if (authorizationResult.Succeeded)
                return Ok(catalogueRepository.DeleteCatalogue(catalogue));

            if (publicCatalogueAuthorizationResult.Succeeded)
                return new ForbidResult();

            return NotFound();
        }
    }
}
