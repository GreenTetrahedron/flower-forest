using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserMicroservice.AuthenticationService.Models;
using UserMicroservice.Models;

namespace UserMicroservice.AuthenticationService
{
    public class JWTTokenService : ITokenService
    {
        private readonly TokenParameters tokenParameters;


        public JWTTokenService(IConfiguration configuration)
        {
            tokenParameters = configuration.GetSection("Jwt").Get<TokenParameters>();
        }

        public string GenerateToken(User user)
        {
            if (tokenParameters == null)
                throw new Exception("tokenParameters was null");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameters.secret));

            var token = new JwtSecurityToken(
                claims: new List<Claim>
                {
                    new Claim("UserId", user.Id.ToString())
                },
                expires: DateTime.Now.AddMinutes(tokenParameters.lifetime_minutes),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature),
                issuer: tokenParameters.issuer
            );

            return token.ToString();
        }
    }
}
