using FlowerForestAPI.ResponseHandlers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.ResponseHandlers
{
    public interface IResponseHandler
    {
        Response CreateResponse(Messages message, object data);
    }
}
