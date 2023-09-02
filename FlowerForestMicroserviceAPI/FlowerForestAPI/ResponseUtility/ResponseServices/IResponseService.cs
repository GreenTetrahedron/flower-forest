using ResponseUtility.ResponseServices.Models;

namespace ResponseUtility.ResponseServices
{
    public interface IResponseService
    {
        Response CreateResponse(Messages message, object data);
    }
}
