using FlowerForestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.TokenHandlers
{
    public interface ITokenHandler
    {
        string GenerateToken(User user);
    }
}
