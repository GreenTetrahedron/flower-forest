using System;

namespace FlowerForestAPI.DTOs
{
    public class UserDTOWithToken
    {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
