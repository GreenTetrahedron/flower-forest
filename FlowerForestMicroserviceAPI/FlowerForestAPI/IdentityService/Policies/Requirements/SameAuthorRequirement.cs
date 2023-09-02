using Microsoft.AspNetCore.Authorization;

namespace IdentityUtility.Authorization.Policies.Requirements
{
    public class SameAuthorRequirement<T> : IAuthorizationRequirement
    {
        public Func<T, string, bool> AuthorIdsEqual { get; }
        public string AuthorIdClaimName { get; }

        public SameAuthorRequirement(Func<T, string, bool> authorIdsEqual, string authorIdClaimName)
        {
            AuthorIdsEqual = authorIdsEqual;
            AuthorIdClaimName = authorIdClaimName;
        }
    }
}
