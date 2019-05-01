using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace Test
{
    /// <summary>
    /// Summary description for LocationTest
    /// </summary>
    [TestClass]
    public class LocationTest
    {
        [TestMethod]
        public async Task FindAllTopSpotsTest()
        {
            RestService restService = new RestService();
            List<Location> list = await restService.GetAllDataAsync();
            List<Location> topSpotList = new List<Location>();
            foreach (var location in list)
            {
                if (location.IsTopLocation)
                {
                    topSpotList.Add(location);
                }
            }

            Assert.IsTrue(topSpotList.Count > 0);
        }

        [TestMethod]
        public async Task FindCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
            //Assert.IsTrue(position.Speed==0);
        }
    }
}
