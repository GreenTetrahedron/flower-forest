using FlowerForestAPI.ResponseServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerForestAPI.ResponseServices
{
    public interface IResponseService
    {
        Response CreateResponse(Messages message, object data);
    }
}
