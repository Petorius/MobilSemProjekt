﻿using MobileService.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MobileService.Model;

namespace MobileService.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class LocationService : ILocationService
    {
        private LocationCtrl _locationCtrl;
        public Location GetLocationById(int locationId)
        {
            _locationCtrl = new LocationCtrl();
            return _locationCtrl.GetLocationById(locationId);
        }

        public List<Location> GetLocationsByTag(Tag tag)
        {
            _locationCtrl = new LocationCtrl();
            return _locationCtrl.GetLocationByTag(tag);
        }
    }
}