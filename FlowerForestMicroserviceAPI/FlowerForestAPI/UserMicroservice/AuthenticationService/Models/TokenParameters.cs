using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace UserMicroservice.AuthenticationService.Models
{
    public class TokenParameters
    {
        public TokenParameters()
        {
            secret = "";
            issuer = "";
            lifetime_minutes = 0;
        }

        public TokenParameters(string secret, string issuer, int lifetime_minutes)
        {
            this.secret = secret;
            this.lifetime_minutes = lifetime_minutes;
            this.issuer = issuer;
        }

        public string secret { get; set; }
        public string issuer { get; set; }
        public int lifetime_minutes { get; set; }
    }
}
