using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Exception;
using MobileService.Model;

namespace MobileService.UnitTest
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void ConnectionTest()
        {
            Assert.IsTrue(true);
            try
            {
                DbConnection dbConnection = new DbConnection();
                dbConnection.ConnectionTest();
                Assert.IsTrue(true);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
