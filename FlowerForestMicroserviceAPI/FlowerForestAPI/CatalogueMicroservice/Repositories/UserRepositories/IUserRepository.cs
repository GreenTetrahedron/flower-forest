using CatalogueMicroservice.Models;
using ResponseUtility.ResponseServices.Models;

namespace CatalogueMicroservice.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<Response> GetUserById(Guid id);
        Task<Response> GetUsers();
        Task<Response> AddUser(User User);
        Task<Response> UpdateUser(User User);
        Task<Response> DeleteUserById(Guid id);
        Task<Response> DeleteUser(User user);
    }
}
