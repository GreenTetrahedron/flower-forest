using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Requirements
{
    public class SameUserRequirement : IAuthorizationRequirement
    {
    }
}
