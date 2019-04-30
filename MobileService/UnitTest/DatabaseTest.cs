using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Exception;
using MobileService.Model;

namespace UnitTest
{
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void ConnectionTest()
        {
            try
            {
                DbConnection dbConnection = new DbConnection();
                dbConnection.ConnectionTest();
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
