using System.Collections.Generic;
using System.Threading.Tasks;
using MobileService.Controller;
using MobileService.Model;

namespace MobileService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class LocationService : ILocationService
    {
        private LocationCtrl _locationCtrl;
        
        public List<Location> GetAllLocations()
        {
            List<Location> list = new List<Location>();
            Location l = new Location{ LocationName = "Olsen", User = new User() {UserName = "Ole"}};
            
            list.Add(l);
            return list;
            //_locationCtrl = new LocationCtrl();
            //return _locationCtrl.GetAllLocations();
        }

        public int CreateLocation(Location location)
        {
            _locationCtrl = new LocationCtrl();
            return _locationCtrl.CreateLocation(location);
        }

        public Location GetLocationById(int locationId)
        {
            _locationCtrl = new LocationCtrl();
            return _locationCtrl.GetLocationById(locationId);
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

        public void UpdateHits(Location location)
        {
            _locationCtrl = new LocationCtrl();
            _locationCtrl.UpdateHits(location);
        }

        public string TestMethod()
        {
            return "Oles nye autobil";
        }
    }
}
