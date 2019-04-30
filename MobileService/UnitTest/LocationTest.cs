using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                dbLocation.UpdateHits(location);
                //dbLocation.FindAll();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        [TestMethod]
        public void GetListOfLocationsCommentedByUser()// UpdateHitsTest()
        {
            DbLocation dbLocation = new DbLocation();
            List<Location> list = null;
            try
            {
                //dbLocation.UpdateHits(location);
                list = dbLocation.LocationsByCommentUserName("Aksel");
                foreach (var element in list)
                {
                    Console.WriteLine(element.LocationName);
                }
                //dbLocation.FindAll();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Assert.IsNotNull(list);

        }
    }
}
