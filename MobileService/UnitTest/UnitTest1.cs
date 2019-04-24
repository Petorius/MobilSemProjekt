using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Model;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Location l = new Location();
            l.LocationId = 1;
            DbLocation dbLocation = new DbLocation();
            dbLocation.UpdateHits(l);
            //dbLocation.FindAll();
        }
    }
}
