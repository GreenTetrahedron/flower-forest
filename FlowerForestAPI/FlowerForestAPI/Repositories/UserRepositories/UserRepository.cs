using FlowerForestAPI.DbContexts;
using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseHandlers;
using FlowerForestAPI.ResponseHandlers.Models;
using FlowerForestAPI.TokenHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FlowerForestContext flowerForestContext;
        
        private readonly IResponseHandler responseHandler;
        private readonly ITokenHandler tokenHandler;

        public UserRepository(FlowerForestContext flowerForestContext,
            IResponseHandler responseHandler,
            ITokenHandler tokenHandler)
        {
            this.flowerForestContext = flowerForestContext;
            
            this.responseHandler = responseHandler;
            this.tokenHandler = tokenHandler;
        }

        public Response DeleteUser(User user)
        {
            flowerForestContext.Users.Remove(user);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_DELETE_NOTFOUND : Messages.SUCCESS_DELETE_DELETED;

            return responseHandler.CreateResponse(message, result);
        }

        public Response GetUserById(Guid id)
        {
            var user = flowerForestContext.Users
                .SingleOrDefault(u => u.Id == id);

            var message = user != null ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseHandler.CreateResponse(message, user);
        }

        public Response AuthenticateUser(UserCredentials credentials)
        {
            var user = flowerForestContext.Users
                .SingleOrDefault(u => u.Username == credentials.Username && u.Password == credentials.Password);

            var message = Messages.INFORMATION_AUTHENTICATION_INVALIDCREDENTIALS;
            var userWithToken = new UserWithToken();

            if (user != null)
            {
                message = Messages.SUCCESS_AUTHENTICATION_VALIDCREDENTIALS;
                
                userWithToken = new UserWithToken()
                {
                    User = user,
                    Token = tokenHandler.GenerateToken(user)
                };
            }

            return responseHandler.CreateResponse(message, userWithToken);
        }

        public Response GetUsers()
        {
            var users = flowerForestContext.Users.ToList();

            var message = users.Count() > 0 ?
                Messages.SUCCESS_GET_RETRIEVED : Messages.INFORMATION_GET_NOTFOUND;

            return responseHandler.CreateResponse(message, users);
        }

        public Response AddUser(User user)
        {
            flowerForestContext.Users.Update(user);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.ERROR_POST_INTERNAL : Messages.SUCCESS_POST_CREATED;

            return responseHandler.CreateResponse(message, result);
        }

        public Response UpdateUser(User user)
        {
            flowerForestContext.Users.Update(user);
            var result = flowerForestContext.SaveChanges();

            var message = result == 0 ?
                Messages.INFORMATION_PUT_NOTFOUND : Messages.SUCCESS_PUT_UPDATED;

            return responseHandler.CreateResponse(message, result);
        }
    }
}
