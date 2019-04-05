using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using MobileService.Model;

namespace MobileService.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        [WebGet(UriTemplate = "GetLocationById/{locationId}")]
        Location GetLocationById(string locationId);

        [OperationContract]
        [WebGet(UriTemplate = "GetLocationsByTagName/{tagName}")]
        List<Location> GetLocationsByTagName(string tagName);

        [OperationContract]
        [WebGet(UriTemplate = "GetLocationByLocationName/{locationName}")]
        Location GetLocationByLocationName(string locationName);

        [OperationContract]
        [WebGet(UriTemplate = "GetAllLocations")]
        List<Location> GetAllLocations();
    }
}
