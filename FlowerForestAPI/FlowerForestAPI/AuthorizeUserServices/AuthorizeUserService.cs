using FlowerForestAPI.Models;
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

        public AuthorizeUserService(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        public async Task<AuthorizationResult> AuthorizeUserId(ClaimsPrincipal claims, Guid id)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(claims, new User() { Id = id }, "CreatorOnlyPolicy");

            return authorizationResult;
        }
    }
}
