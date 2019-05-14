using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Exception;
using MobileService.Model;

namespace MobileService.UnitTest
{
    [TestClass]
    public class RatingTest
    {
        [TestMethod]
        public void CreateRatingTest()
        {
            DbRating dbRating = new DbRating();

            try
            {
                Rating rating = new Rating
                {
                    Rate = 4,
                    Comment = "Hej",
                    LocationId = 5,
                    User = new User{
                        UserId = 2
                    }
                };
                
                int ratingId = dbRating.Create(rating);
                Assert.IsTrue(ratingId > 0);
            }
            catch (FaultException<DbConnectionException>)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ReadRatingTest()
        {
            DbRating dbRating = new DbRating();
            Rating rating = null;

            try
            {
                rating = dbRating.FindById(1);
                Assert.IsNotNull(rating);
            }
            catch (FaultException<DbConnectionException>)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateRatingTest()
        {
            DbRating dbRating = new DbRating();
            Rating rating = null;

            try
            {
                rating = dbRating.FindById(1);
                bool status = dbRating.UserUpdateRating(rating);
                Assert.IsTrue(status);
            }
            catch (FaultException<DbConnectionException>)
            {
                Assert.Fail();
            }
        }
    }
}