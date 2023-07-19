using FlowerForestAPI.Models;
using FlowerForestAPI.Repositories.CatalogueRepositories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlowerForestAPI.AuthorizeUserServices
{
    public class AuthorizeUserService : IAuthorizeUserService
    {
        private readonly IAuthorizationService authorizationService;

        private readonly ICatalogueRepository catalogueRepository;

        public AuthorizeUserService(IAuthorizationService authorizationService,
            ICatalogueRepository catalogueRepository)
        {
            this.authorizationService = authorizationService;

            this.catalogueRepository = catalogueRepository;
        }

        public async Task<AuthorizationResult> AuthorizeUserId(ClaimsPrincipal claims, Guid id)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(claims, new User() { Id = id }, "CreatorOnlyPolicy");

            return authorizationResult;
        }

        public async Task<AuthorizationResult> AuthorizeCatalogueUserId(ClaimsPrincipal claims, Guid id)
        {
            Guid userId = (Guid)catalogueRepository.GetColumnByCatalogueId(nameof(Catalogue.UserId), id);

            return await AuthorizeUserId(claims, userId);
        }

        public async Task<AuthorizationResult> AuthorizePublicCatalogueById(ClaimsPrincipal claims, Guid id)
        {
            bool isPublic = (bool)catalogueRepository.GetColumnByCatalogueId(nameof(Catalogue.IsPublic), id);

            if (isPublic)
            {
                return AuthorizationResult.Success();
            }

            return AuthorizationResult.Failed();
        }

    }
}
