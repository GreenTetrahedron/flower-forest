using CatalogueMicroservice.DbContexts;
using CatalogueMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using CatalogueMicroservice.Repositories.UserRepositories;
using ResponseUtility.ResponseServices;
using ResponseUtility.ResponseServices.Models;

namespace CatalogueMicroservice.Repositories.CatalogueRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CatalogueDbContext catalogueDbContext;
        private readonly IResponseService responseService;

        public UserRepository(CatalogueDbContext catalogueDbContext, IResponseService responseService)
        {
            this.catalogueDbContext = catalogueDbContext;
            this.responseService = responseService;
        }

        public async Task<Response> AddUser(User user)
        {
            await catalogueDbContext.Users.AddAsync(user);
            var result = await catalogueDbContext.SaveChangesAsync();

            var message = result == 0?
                Messages.ERROR_POST_INTERNAL :
                Messages.SUCCESS_POST_CREATED;

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> DeleteUserById(Guid id)
        {
            var user = await catalogueDbContext.Users
                .SingleOrDefaultAsync(u => u.Id == id);

            var message = Messages.SUCCESS_DELETE_DELETED;


            if (user != null)
            {
                message = Messages.INFORMATION_DELETE_NOTFOUND;
                catalogueDbContext.Users.Remove(user);
            }

            var result = await catalogueDbContext.SaveChangesAsync();

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> DeleteUser(User user)
        {
            catalogueDbContext.Users.Remove(user);

            var result = await catalogueDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.ERROR_DELETE_INVALIDREQUEST :
                Messages.SUCCESS_DELETE_DELETED;

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> GetUserById(Guid id)
        {
            var user = await catalogueDbContext.Users
                .SingleOrDefaultAsync(u => u.Id == id);

            var message = user == null ?
                Messages.INFORMATION_GET_NOTFOUND :
                Messages.SUCCESS_GET_RETRIEVED;

            return responseService.CreateResponse(message, user);
        }

        public async Task<Response> GetUsers()
        {
            var users = await catalogueDbContext.Users
                .ToListAsync();

            var message = users.Count() > 0 ?
                Messages.INFORMATION_GET_NOTFOUND :
                Messages.SUCCESS_GET_RETRIEVED;

            return responseService.CreateResponse(message, users);
        }

        public async Task<Response> UpdateUser(User user)
        {
            catalogueDbContext.Users.Update(user);
            var result = await catalogueDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.ERROR_POST_INTERNAL :
                Messages.SUCCESS_POST_CREATED;

            return responseService.CreateResponse(message, result);
        }
    }
}
