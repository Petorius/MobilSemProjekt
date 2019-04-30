using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Model;

namespace UnitTest
{
    [TestClass]
    public class LocationTest
    {
        [TestMethod]
        public void UpdateHitsTest()
        {
            Location location = new Location();
            location.LocationId = 1;
            DbLocation dbLocation = new DbLocation();
            dbLocation.UpdateHits(location);
            //dbLocation.FindAll();
        }
    }
}
