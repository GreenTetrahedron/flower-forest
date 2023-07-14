using FlowerForestAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FlowerForestAPI.TokenHandlers
{
    public class JWTTokenHandler : ITokenHandler
    {
        private readonly string secret;
        private readonly double expirationTime_minutes;
        private readonly string issuer;

        public JWTTokenHandler(IConfiguration configuration)
        {
            secret = configuration["JWTConfiguration:Secret"];
            expirationTime_minutes = double.Parse(configuration["JWTConfiguration:ExpirationTimeInMinutes"]);
            issuer = configuration["JWTConfiguration:Issuer"];
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationTime_minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Issuer = issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
