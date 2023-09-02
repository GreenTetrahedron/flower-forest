using ResponseUtility.ResponseServices.Models;
using UserMicroservice.Models;
using UserMicroservice.Models.DTOs;

namespace UserMicroservice.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<Response> AuthenticateUser(UserCredentials userCredentials);

        Task<Response> GetUsers();

        Task<Response> GetUserById(Guid id);

        Task<Response> UpdateUser(User user);

        Task<Response> AddUser(User user);

        Task<Response> DeleteUserById(Guid id);
    }
}
