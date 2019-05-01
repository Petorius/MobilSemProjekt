using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Exception;
using MobileService.Model;

namespace UnitTest
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
                Rating Rating = new Rating
                {
                    
                };

                //dbRating.Create(Rating);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
            catch(FaultException<Exception> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ReadRatingTest()
        {
            DbRating dbRating = new DbRating();
            Rating Rating = null;

            try
            {
                Rating = dbRating.FindById(1);
                Assert.IsNotNull(Rating);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
            catch (FaultException<Exception> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateRatingTest()
        {
            DbRating dbRating = new DbRating();
            Rating Rating = null;

            try
            {
                Rating = dbRating.FindById(1);
                bool status = dbRating.UserUpdateRating(Rating);
                Assert.IsTrue(status);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
            catch (FaultException<Exception> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }
    }
}
