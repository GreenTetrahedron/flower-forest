using ResponseUtility.ResponseServices.Models;

namespace ResponseUtility.ResponseServices
{
    public class ResponseService : IResponseService
    {
        public Response CreateResponse(Messages message, object data)
        {
            return new Response { Message = message.ToString(), Data = data};
        }
    }
}
