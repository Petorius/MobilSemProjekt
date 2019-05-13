using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Controller;
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

                int userId = dbUser.Create(user);
                Assert.IsTrue(userId > 0);
            }
            catch (FaultException<DbConnectionException>)
            {
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
            catch (FaultException<DbConnectionException>)
            {
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
            catch (FaultException<DbConnectionException>)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CompareTrueTest()
        {
            try
            {
                string hash = "H7rdT7K2Chxiz8xnzyayYoJtT9fPvL0hF8eUNbo3" +
                              "ejg4uxbrFgVQR7qk7uteirsId7Ys2RKYOYRgAd2fOi" +
                              "N5VhJKoPzjQPtzl6iGndVgjQoP74IaMlUbC0a0x0oul5" +
                              "OMwymKDR9Gk953fKM1nwu2gHmHX/JtKXUf6QqwDtvLCpHT" +
                              "7YfOLRQMIm98C2+adlOvH+pXDoWmMAVmkFg2hKW08OQJytOY" +
                              "HgFexvkkLoAjSOw0pqb7ri39pFZrzssxjsd/HBYU4QWAD7EOrQJB" +
                              "xjqcSxGQp73JeRFeFU1ESqcjN2nYmo1epT63d3So2uH97kYZWc1vWP4" +
                              "mpf3nf0/HYt4kfg==";
                UserCtrl userCtrl = new UserCtrl();
                bool status = userCtrl.CompareHashes("Aksel", hash);
                Assert.IsTrue(status);
            }
            catch (FaultException<DbConnectionException>)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void CompareFalseTest()
        {
            bool status = false;
            try
            {
                string hash = "WRONG";
                UserCtrl userCtrl = new UserCtrl();
                status = userCtrl.CompareHashes("Aksel", hash);
                Assert.IsFalse(status);
            }
            catch (FaultException<UserOrPasswordException>)
            {
                Assert.IsFalse(status);
            }
            catch (FaultException<DbConnectionException>)
            {
                Assert.Fail();
            }
        }
        [TestMethod]
        public void FindSaltByUserNameTest()
        {
            try
            {
                UserCtrl userCtrl = new UserCtrl();
                string salt = userCtrl.FindSaltByUserName("Aksel");
                string saltInDB = "yR9c4qazcTfrnEJb8USdqruRe+3+iq/s95r+GmFJW0ed" +
                                  "AvJLGgHbW0o59L+JGMWWB9aU4SSa5Z24n37laq+DeA==";
                Assert.IsTrue(salt.Equals(saltInDB));
            }
            catch (FaultException<DbConnectionException>)
            {
                Assert.Fail();
            }
        }
    }
}
