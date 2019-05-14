using System;
using System.Collections.Generic;
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
                    LocationDescription = "Test description",
                    LocationName = "CreateTest"
                };

                int id = dbLocation.Create(location);
                Assert.IsTrue(id > 0);
                dbLocation.Delete(id);
            }
            catch (FaultException<System.Exception>)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ReadLocationTest()
        {
            try
            {
                DbLocation dbLocation = new DbLocation();
                Location location = dbLocation.FindById(10);
                Assert.IsNotNull(location);
            }
            catch (FaultException<System.Exception>)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ReadAllLocationsTest()
        {
            try
            {
                DbLocation dbLocation = new DbLocation();
                List<Location> locations = dbLocation.FindAll();
                Assert.IsTrue(locations.Count > 0);
            }
            catch (FaultException<NoLocationsInDatabaseException>)
            {
                Assert.Fail();
            }
            catch (FaultException<System.Exception>)
            {
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
                    LocationDescription = "Test description",
                    LocationName = "UpdateTest1"
                };
                Location newLocation = location;
                newLocation.LocationDescription = "Test description";
                newLocation.LocationName = "UpdateTest2";

                int id = dbLocation.Create(location);
                location.LocationId = id;
                newLocation.LocationId = id;
                dbLocation.UserUpdateLocation(newLocation);
                string foundLocationName = dbLocation.FindById(id).LocationName;
                Assert.IsTrue(foundLocationName.Equals("UpdateTest2"));
                dbLocation.Delete(id);
            }
            catch (FaultException<System.Exception>)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateHitsTest()
        {
            try
            {
                DbLocation dbLocation = new DbLocation();
                Location location = new Location
                {
                    User = new User { UserId = 1 },
                    Latitude = 1,
                    Longitude = 1,
                    LocationDescription = "Test description",
                    LocationName = "UpdateHitsTest",
                    Hits = 0
                };
                int id = dbLocation.Create(location);
                location.LocationId = id;
                dbLocation.UpdateHits(location);
                Location newLocation = dbLocation.FindById(id);
                Assert.IsTrue(location.Hits < newLocation.Hits);
                dbLocation.Delete(location.LocationId);
            }
            catch (FaultException<System.Exception>)
            {
                Assert.Fail();
            }
            
        }

        [TestMethod]
        public void GetListOfLocationsRatedByUser()
        {
            try
            {
                DbLocation dbLocation = new DbLocation();
                List<Location> list = dbLocation.LocationsByCommentUserName("Aksel");
                Assert.IsTrue(list.Count > 0);
            }
            catch (FaultException<System.Exception>)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void GetAvgRatingTest()
        {
            try
            {
                DbLocation dbLocation = new DbLocation();
                DbRating dbRating = new DbRating();
                Location location = new Location
                {
                    Latitude = 1,
                    Longitude = 1,
                    LocationName = "AVGTEST",
                    LocationDescription = "AVGDescription"
                };
                int id = dbLocation.Create(location);
                Rating rating1 = new Rating
                {
                    LocationId = id,
                    Rate = 1,
                    Comment = "RateTest1"
                };
                Rating rating2 = new Rating
                {
                    LocationId = id,
                    Rate = 5,
                    Comment = "RateTest2"
                };
                rating1.RatingId = dbRating.Create(rating1);
                rating2.RatingId = dbRating.Create(rating2);
                double rate = dbRating.GetAverageRating(id);
                Assert.IsTrue(rate == 3);
                dbRating.Delete(rating1);
                dbRating.Delete(rating2);
                dbLocation.Delete(id);

            }
            catch (FaultException<System.Exception>)
            {
                Assert.Fail();
            }
        }
    }
}
