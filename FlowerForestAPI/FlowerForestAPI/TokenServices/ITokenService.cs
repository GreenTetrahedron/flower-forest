using FlowerForestAPI.DTOs;
using FlowerForestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.TokenServices
{
    public interface ITokenService
    {
        string GenerateToken();
    }
}
