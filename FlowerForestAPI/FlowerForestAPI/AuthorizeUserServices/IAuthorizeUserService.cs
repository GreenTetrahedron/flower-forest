using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlowerForestAPI.AuthorizeUserServices
{
    public interface IAuthorizeUserService
    {
        Task<AuthorizationResult> AuthorizeUserId(ClaimsPrincipal claims, Guid id);

        Task<AuthorizationResult> AuthorizeCatalogueUserId(ClaimsPrincipal claims, Guid id);

        Task<AuthorizationResult> AuthorizePublicCatalogueById(ClaimsPrincipal claims, Guid id);
    }
}
