using MobileService.Database;
using System.Collections.Generic;
using MobileService.Model;

namespace MobileService.Controller
{
    public class LocationCtrl
    {
        private DbLocation _dbLocation;
        private DbTag _dbTag;
        public LocationCtrl()
        {
            _dbLocation = new DbLocation();
            _dbTag = new DbTag();
        }

        public Location GetLocationById(int locationId)
        {
            return _dbLocation.FindById(locationId);
        }
        public Location GetLocationByName(string locationName)
        {
            return _dbLocation.FindByName(locationName);
        }
        public List<Location> GetAllLocations()
        {
            return _dbLocation.FindAll();
        }

        public List<Location> GetTagByTagName(string tagName)
        {
            Tag tag = _dbTag.FindByName(tagName);
            return tag.Locations;
        }
    }
}
