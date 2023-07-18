using Microsoft.AspNetCore.Authorization;
using FlowerForestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace FlowerForestAPI.Requirements.SameUserAuthorizationHandler
{
    public class SameUserAuthorizationHandler : AuthorizationHandler<SameUserRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement, User resource)
        {
            if (resource == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.HasClaim(x => x.Type == "Id" && x.Value == resource.Id.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
