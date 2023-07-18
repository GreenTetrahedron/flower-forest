using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.ResponseServices
{
    public class ResponseService : IResponseService
    {
        public Response CreateResponse(Messages message, object data)
        {
            return new Response { Message = message.ToString(), Data = data};
        }
    }
}
