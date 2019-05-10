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

        /// <summary>
        /// Creates a location in database
        /// </summary>
        /// <param name="location"></param>
        /// <returns>int</returns>
        public int CreateLocation(Location location)
        {
            return _dbLocation.Create(location);
        }

        /// <summary>
        /// gets a location by its id
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns>Location</returns>
        public Location GetLocationById(int locationId)
        {
            return _dbLocation.FindById(locationId);
        }

        /// <summary>
        /// gets a location by its name
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns>Location</returns>
        public Location GetLocationByName(string locationName)
        {
            return _dbLocation.FindByName(locationName);
        }
        
        /// <summary>
        /// gets all Locations
        /// </summary>
        /// <returns></returns>
        public List<Location> GetAllLocations()
        {
            return _dbLocation.FindAll();
        }

        /// <summary>
        /// get locations with a tag
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// gets location created by user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>List<Location/></returns>
        public List<Location> GetLocationsByUserName(string userName)
        {
            return _dbLocation.FindLocationsByUserName(userName);
        }
        
        /// <summary>
        /// gets location assosiated with rating created by user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>List<Location/></returns>
        public List<Location> GetLocationsByCommentUserName(string userName)
        {
            return _dbLocation.LocationsByCommentUserName(userName);
        }

        /// <summary>
        /// gets locations avg rate
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns>double</returns>
        public double GetAverageRating(int locationId)
        {
            return _dbRating.GetAverageRating(locationId);
        }
        
        /// <summary>
        /// Updates amount of hits
        /// </summary>
        /// <param name="location"></param>
        public void UpdateHits(Location location) {
            _dbLocation.UpdateHits(location);
        }
        
        /// <summary>
        /// Updates a location
        /// </summary>
        /// <param name="location"></param>
        public void UpdateLocation(Location location) {
            _dbLocation.UpdateLocation(location);
        }

        /// <summary>
        /// updates a location based on user input
        /// </summary>
        /// <param name="location"></param>
        public void UserUpdateLocation(Location location)
        {
            _dbLocation.UserUpdateLocation(location);
        }   

        /// <summary>
        /// Finds and sets a toplocation
        /// </summary>
        public void SetTopLocations()
        {
            List<Location> allLocations = GetAllLocations();
            int allHits = 0;
            
            foreach (var location in allLocations)
            {
                allHits += location.Hits;
            }

            foreach (var location in allLocations)
            {
                bool oldIsTopLocation = location.IsTopLocation;
                location.IsTopLocation = false;
                double averageRating = 0;
                foreach (var rating in location.Ratings)
                {
                    averageRating += rating.Rate;
                }

                if (location.Ratings.Count > 0)
                {
                    averageRating = averageRating / location.Ratings.Count;
                }

                if (location.Hits > allHits / 10000 || location.Hits > 1000 && averageRating >= 4.5
                                                    || location.Hits > 10000 && averageRating >= 4 ||
                                                    location.Ratings.Count > 100 && averageRating >= 4.5)
                {
                    location.IsTopLocation = true;
                }

                if (oldIsTopLocation != location.IsTopLocation)
                {
                    _dbLocation.UpdateLocationAsTopLocation(location);
                }
            }
        }
    }
}
