using CatalogueMicroservice.Authentication.Requirements;
using CatalogueMicroservice.Models;
using Microsoft.AspNetCore.Authorization;

namespace CatalogueMicroservice.Authentication.Handlers
{
    public class CatalogueIsPublicHandler : AuthorizationHandler<CatalogueIsPublicRequirement, Catalogue>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CatalogueIsPublicRequirement requirement, Catalogue resource)
        {
            if (resource.IsPublic)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
