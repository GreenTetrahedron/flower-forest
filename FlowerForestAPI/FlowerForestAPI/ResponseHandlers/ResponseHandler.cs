using FlowerForestAPI.Models;
using FlowerForestAPI.ResponseHandlers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.ResponseHandlers
{
    public class ResponseHandler : IResponseHandler
    {
        public Response CreateResponse(Messages message, object data)
        {
            return new Response { Message = message.ToString(), Data = data};
        }
    }
}
