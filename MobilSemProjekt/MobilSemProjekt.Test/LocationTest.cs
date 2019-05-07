using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Plugin.Geolocator;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.Service;

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

        // this method is not implemented due to the
        // fact that it can not work without a gps 
        //
        //[TestMethod]
        //public async Task FindCurrentLocation()
        //{
        //    string en = await GetCurrentLocation();
        //    Thread.Sleep(500);
        //    string to = await GetCurrentLocation();
        //    Assert.IsTrue(en.Equals(to));
        //}
        
        //private async Task<string> GetCurrentLocation()
        //{
        //    var locator = CrossGeolocator.Current;
        //    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
        //    string latitude = position.Latitude.ToString();
        //    string longitude = position.Longitude.ToString();
        //    return latitude + ", " + longitude;
        //}
    }
}
