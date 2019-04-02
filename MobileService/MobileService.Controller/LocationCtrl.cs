using MobileService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileService.Model;

namespace MobileService.Controller
{
    public class LocationCtrl
    {
        private DbLocation _dbLocation;
        public LocationCtrl()
        {
            _dbLocation = new DbLocation();
        }

        public Location GetLocationById(int locationId)
        {
            return _dbLocation.FindById(locationId);
        }

        public List<Location> GetLocationByTag(Tag tag)
        {
            return _dbLocation.FindByTagId(tag.TagId);
        }
    }
}
