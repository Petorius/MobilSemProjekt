using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Exception;
using MobileService.Model;

namespace MobileService.UnitTest
{
    [TestClass]
    public class LocationTest
    {
        [TestMethod]
        public void CreateLocationTest()
        {
            try
            {
                DbLocation dbLocation = new DbLocation();
                Location location = new Location
                {
                    User = new User { UserId = 1 },
                    Latitude = 1,
                    Longitude = 1,
                    LocationDescription = "A nice spot",
                    LocationName = "MySpot"
                };

                dbLocation.Create(location);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
            catch (FaultException<System.Exception> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ReadLocationTest()
        {
            try
            {
                DbLocation dbLocation = new DbLocation();
                Location location = dbLocation.FindById(1);
                Assert.IsNotNull(location);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
            catch (FaultException<System.Exception> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateLocationTest()
        {
            try
            {
                DbLocation dbLocation = new DbLocation();
                Location location = dbLocation.FindById(1);
                dbLocation.UserUpdateLocation(location);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
            catch (FaultException<System.Exception> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

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
        public void GetListOfLocationsCommentedByUser()
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
