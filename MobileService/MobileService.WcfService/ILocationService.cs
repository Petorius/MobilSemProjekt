using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.Services.Description;
using MobileService.Model;

namespace MobileService.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetLocationById/{locationId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Location GetLocationById(string locationId);

        [OperationContract]
        [WebInvoke(Method = "POST",
            UriTemplate = "CreateLocation",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        int CreateLocation(Location location);

        [OperationContract]
        [WebGet(UriTemplate = "GetLocationsByTagName/{tagName}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<Location> GetLocationsByTagName(string tagName);

        [OperationContract]
        [WebGet(UriTemplate = "GetLocationByLocationName/{locationName}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        Location GetLocationByLocationName(string locationName);

        [OperationContract]
        [WebGet(UriTemplate = "GetAverageRating/{locationId}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        double GetAverageRating(string locationId);

        [OperationContract]
        [WebGet(UriTemplate = "GetLocationsByUserName/{userName}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<Location> GetLocationsByUserName(string userName);

        [OperationContract]
        [WebGet(UriTemplate = "GetAllLocations",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        List<Location> GetAllLocations();

        [OperationContract]
        [WebInvoke(Method = "PUT",
            UriTemplate = "UpdateHits",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void UpdateHits(Location location);

        [OperationContract]
        [WebInvoke(Method = "PUT",
            UriTemplate = "UpdateLocation",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        void UpdateLocation(Location location);
    }
}
