using System.Collections.Generic;
using MobileService.Controller;
using MobileService.Model;

namespace MobileService.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class LocationService : ILocationService
    {
        private LocationCtrl _locationCtrl;
        
        public List<Location> GetAllLocations()
        {
            _locationCtrl = new LocationCtrl();
            return _locationCtrl.GetAllLocations();
        }

        public int CreateLocation(Location location)
        {
            _locationCtrl = new LocationCtrl();
            return _locationCtrl.CreateLocation(location);
        }

        public Location GetLocationById(string locationId)
        {
            _locationCtrl = new LocationCtrl();
            int.TryParse(locationId, out int locId);
            return _locationCtrl.GetLocationById(locId);
        }

        public Location GetLocationByLocationName(string locationName)
        {
            _locationCtrl = new LocationCtrl();
            return _locationCtrl.GetLocationByName(locationName);
        }

        public List<Location> GetLocationsByTagName(string tagName)
        {
            _locationCtrl = new LocationCtrl();
            return _locationCtrl.GetLocationsByTagName(tagName);
        }

        public List<Location> GetLocationsByUserName(string userName)
        {
            _locationCtrl = new LocationCtrl();
            return _locationCtrl.GetLocationsByUserName(userName);
        }

        public double GetAverageRating(string locationId)
        {
            _locationCtrl = new LocationCtrl();
            int.TryParse(locationId, out int locId);
            return _locationCtrl.GetAverageRating(locId);
        }

        public void UpdateHits(Location location)
        {
            _locationCtrl = new LocationCtrl();
            _locationCtrl.UpdateHits(location);
        }
    }
}
