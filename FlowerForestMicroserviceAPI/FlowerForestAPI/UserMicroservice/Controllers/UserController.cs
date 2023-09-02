using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UserMicroservice.Models;
using UserMicroservice.Models.DTOs;
using UserMicroservice.Repositories.UserRepositories;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthorizationService authorizationService;
        
        public UserController(IUserRepository userRepository, IAuthorizationService authorizationService)
        {
            this.userRepository = userRepository;
            this.authorizationService = authorizationService;
        }

        private async Task<IActionResult> IsUserAuthorizedByPolicy(object user, string policyName, Func<Task<object>> doWhenAuthorized)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, user, policyName);
            IActionResult result;

            if (!authorizationResult.Succeeded)
            {
                result = User.Identity != null && User.Identity.IsAuthenticated ?
                    new ForbidResult("User may not perform requested action on specified object") :
                    new ChallengeResult("Unauthorized user");
            }
            else
            {
                result = Ok(await doWhenAuthorized.Invoke());
            }


            return result;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserCredentials userCredentials)
        {
            return Ok(await userRepository.AuthenticateUser(userCredentials));
        }


        //TODO: Authorize with roles
        //[HttpGet]
        //public async Task<IActionResult> GetUsers()
        //{
        //    return Ok(await userRepository.GetUsers());
        //}

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var user = await userRepository.GetUserById(id);

            return await IsUserAuthorizedByPolicy(user, "SameAuthorPolicy",
                async () => user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            return Ok(await userRepository.AddUser(user));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            return await IsUserAuthorizedByPolicy(user, "SameAuthorPolicy",
                async () => await userRepository.UpdateUser(user));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserById([FromRoute] Guid id)
        {
            return await IsUserAuthorizedByPolicy(new User { Id = id}, "SameAuthorPolicy",
                async () => await userRepository.DeleteUserById(id));
        }
    }
}