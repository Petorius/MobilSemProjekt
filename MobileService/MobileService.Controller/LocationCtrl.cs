using MobileService.Database;
using System.Collections.Generic;
using MobileService.Model;

namespace MobileService.Controller
{
    public class LocationCtrl
    {
        private readonly DbLocation _dbLocation;
        private readonly DbTag _dbTag;
        private readonly DbRating _dbRating;
        public LocationCtrl()
        {
            _dbLocation = new DbLocation();
            _dbTag = new DbTag();
            _dbRating = new DbRating();
        }

        public int CreateLocation(Location location)
        {
            return _dbLocation.Create(location);
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

        public List<Location> GetLocationsByTagName(string tagName)
        {
            Tag tag = _dbTag.FindByName(tagName);
            List<Location> locations = null;
            if (tag != null)
            {
                locations = tag.Locations;
            }
            return locations;
        }

        public List<Location> GetLocationsByUserName(string userName)
        {
            return _dbLocation.FindLocationsByUserName(userName);
        }

        public double GetAverageRating(int locationId)
        {
            return _dbRating.GetAverageRating(locationId);
        }

        public void UpdateHits(Location location) {
            _dbLocation.UpdateHits(location);
        }

        public void UpdateLocation(Location location) {
            _dbLocation.UpdateLocation(location);
        }

    }
}
