using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
                DbLocation dbLocation = new DbLocation();
                dbLocation.FindAll();
        }
    }
}
