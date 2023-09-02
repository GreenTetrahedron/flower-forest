using UserMicroservice.Models;

namespace UserMicroservice.AuthenticationService
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
