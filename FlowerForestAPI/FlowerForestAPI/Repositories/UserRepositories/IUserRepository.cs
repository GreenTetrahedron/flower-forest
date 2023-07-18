using FlowerForestAPI.DTOs;
using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseServices.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Response GetUsers();
        Response GetUserById(Guid id);
        Response AuthenticateUser(UserCredentials credentials);
        Response AddUser(User user);
        Response UpdateUser(User user);
        Response DeleteUser(User user);

        Response DeleteUserById(Guid id);
    }
}
