using System.Collections.Generic;
using System.ServiceModel;
using MobileService.Model;

namespace MobileService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILocationService
    {
        [OperationContract]
        Location GetLocationById(int locationId);

        [OperationContract]
        int CreateLocation(Location location);

        [OperationContract]
        List<Location> GetLocationsByTagName(string tagName);

        [OperationContract]
        Location GetLocationByLocationName(string locationName);

        [OperationContract]
        List<Location> GetAllLocations();

        [OperationContract]
        void UpdateHits(Location location);
        
        [OperationContract]
        string TestMethod();
    }
}
