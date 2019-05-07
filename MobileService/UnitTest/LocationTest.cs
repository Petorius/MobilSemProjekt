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

                int id = dbLocation.Create(location);
                dbLocation.Delete(id);
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
                Location location = new Location
                {
                    User = new User { UserId = 1 },
                    Latitude = 1,
                    Longitude = 1,
                    LocationDescription = "TESTING",
                    LocationName = "TESTSPOT"
                };
                Location newlocation = new Location
                {
                    User = new User { UserId = 1 },
                    Latitude = 1,
                    Longitude = 1,
                    LocationDescription = "TESTING2",
                    LocationName = "TESTSPOT2"
                };
                int id = dbLocation.Create(location);
                location.LocationId = id;
                newlocation.LocationId = id;
                dbLocation.UserUpdateLocation(newlocation);
                string foundLocationName = dbLocation.FindById(id).LocationName;
                if (foundLocationName.Equals("TESTSPOT2"))
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(false);
                }
                dbLocation.Delete(id);
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
            Location location = new Location
            {
                LocationId = 1
            };
            DbLocation dbLocation = new DbLocation();
            try
            {
                dbLocation.UpdateHits(location);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        [TestMethod]
        public void GetListOfLocationsRatedByUser()
        {
            DbLocation dbLocation = new DbLocation();
            List<Location> list = new List<Location>();
            try
            {
                list = dbLocation.LocationsByCommentUserName("Aksel");
                foreach (var element in list)
                {
                    Console.WriteLine(element.LocationName);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            Assert.IsTrue(list.Count > 0);
        }
    }
}
