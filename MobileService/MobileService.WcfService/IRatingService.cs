using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using MobileService.Model;

namespace MobileService.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRatingService
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "CreateRating",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        int CreateRating(Rating rating);


        [OperationContract]
        [WebGet(UriTemplate = "GetAverageRating/{locationId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        double GetAverageRating(string locationId);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "Update",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool UpdateRating(Rating rating);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "Delete",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        bool DeleteRating(Rating rating);
    }
}
