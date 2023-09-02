using MessageBrokerClient.MessageSenderServices;
using MessageBrokerClient.Models.Exchanges;
using Microsoft.EntityFrameworkCore;
using ResponseUtility.ResponseServices;
using ResponseUtility.ResponseServices.Models;
using UserMicroservice.AuthenticationService;
using UserMicroservice.DbContexts;
using UserMicroservice.Models;
using UserMicroservice.Models.DTOs;

namespace UserMicroservice.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AmqpExchange exchange;

        private readonly UserDbContext userDbContext;
        
        private readonly IResponseService responseService;
        private readonly IMessageSenderService messageSenderService;

        private readonly ITokenService tokenService;

        public UserRepository(UserDbContext userDbContext,
            IResponseService responseService,
            IMessageSenderService messageSenderService,
            ITokenService tokenService)
        {
            exchange = MessageBrokerExchanges.Exchanges[MessageBrokerExchangeNames.User];

            this.userDbContext = userDbContext;
            
            this.responseService = responseService;
            this.messageSenderService = messageSenderService;
            
            this.tokenService = tokenService;
        }

        private static UserDTO UserToUserDTO(User user)
        {
            if (user == null)
                return null;

            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username
            };
        }

        public async Task<Response> DeleteUserById(Guid id)
        {
            var user = await userDbContext.Users
                .SingleOrDefaultAsync(u => u.Id == id);
            userDbContext.Users.Remove(user);

            var result = await userDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.INFORMATION_DELETE_NOTFOUND : Messages.SUCCESS_DELETE_DELETED;

            messageSenderService.SendData(UserToUserDTO(user), "delete-user", exchange);

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> GetUserById(Guid id)
        {
            var result = await userDbContext.Users
                .SingleOrDefaultAsync(u => u.Id == id);

            var message = Messages.INFORMATION_GET_NOTFOUND;

            UserDTO? responseData = null;

            if (result != null)
            {
                message = Messages.SUCCESS_GET_RETRIEVED;

                responseData = UserToUserDTO(result);
            }

            return responseService.CreateResponse(message, responseData);
        }

        public async Task<Response> AuthenticateUser(UserCredentials credentials)
        {
            var resultUser = userDbContext.Users
                .SingleOrDefault(u => u.Username == credentials.Username && u.Password == credentials.Password);

            var message = Messages.INFORMATION_AUTHENTICATION_INVALIDCREDENTIALS;
            string? token = null;

            if (resultUser != null)
            {
                message = Messages.SUCCESS_AUTHENTICATION_VALIDCREDENTIALS;

                token = tokenService.GenerateToken(resultUser);
            }

            return responseService.CreateResponse(message, token);
        }

        public async Task<Response> GetUsers()
        {
            var users = await userDbContext.Users
                .Select(u => UserToUserDTO(u))
                .ToListAsync();

            var message = users.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseService.CreateResponse(message, users);
        }

        public async Task<Response> AddUser(User user)
        {
            await userDbContext.Users.AddAsync(user);
            var result = await userDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.ERROR_POST_INTERNAL : Messages.SUCCESS_POST_CREATED;

            messageSenderService.SendData(UserToUserDTO(user), "add-user", exchange);

            return responseService.CreateResponse(message, result);
        }

        public async Task<Response> UpdateUser(User user)
        {
            userDbContext.Users.Update(user);
            var result = await userDbContext.SaveChangesAsync();

            var message = result == 0 ?
                Messages.INFORMATION_PUT_NOTFOUND : Messages.SUCCESS_PUT_UPDATED;

            messageSenderService.SendData(UserToUserDTO(user), "update-user", exchange);

            return responseService.CreateResponse(message, result);
        }
    }
}
