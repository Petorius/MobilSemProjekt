using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Exception;
using MobileService.Model;

namespace MobileService.UnitTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateUserTest()
        {
            DbUser dbUser = new DbUser();

            try
            {
                User user = new User
                {
                    HashPassword = "",
                    Salt = "",
                    UserName = "User1",
                    UserType = new UserType
                    {
                        UserTypeId = 1
                    }
                };

                dbUser.Create(user);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ReadUserTest()
        {
            DbUser dbUser = new DbUser();
            User user = null;

            try
            {
                user = dbUser.FindById(1);
                Assert.IsNotNull(user);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            DbUser dbUser = new DbUser();
            User user = null;

            try
            {
                user = dbUser.FindById(1);
                bool status = dbUser.Update(user);
                Assert.IsTrue(status);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }
    }
}
