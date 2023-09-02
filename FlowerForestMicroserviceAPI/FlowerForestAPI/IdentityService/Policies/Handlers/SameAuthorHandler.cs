using Microsoft.AspNetCore.Authorization;
using IdentityUtility.Authorization.Policies.Requirements;

namespace IdentityUtility.Authorization.Policies.Handlers
{
    public class SameAuthorHandler<T> : AuthorizationHandler<SameAuthorRequirement<T>, T>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameAuthorRequirement<T> requirement, T resource)
        {
            var authorIdClaim = context.User.Claims
                .FirstOrDefault(c => c.Type == requirement.AuthorIdClaimName);

            if (authorIdClaim == null)
            {
                context.Fail(new AuthorizationFailureReason(this, "Invalid token"));
                return Task.CompletedTask;
            }

            if (requirement.AuthorIdsEqual(resource, authorIdClaim.Value))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
