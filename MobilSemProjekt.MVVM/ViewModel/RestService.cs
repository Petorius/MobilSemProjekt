using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.LocationServiceReference;
//using LocationServiceReference;
using Newtonsoft.Json;
using Xamarin.Forms.Xaml;
using Location = MobilSemProjekt.MVVM.Model.Location;

namespace MobilSemProjekt.MVVM.ViewModel
{
    public class RestService : IRestService
    {
        private HttpClient _client;
        private const string RestUrl = "http://localhost:8733/";
        public List<Location> Items { get; private set;}
        
        public RestService()
        {
            //_client = new HttpClient();
        }
        
        public Task DeleteLocationAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Location> ReadLocationByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Location>> ReadLocationByTagName(string tagName)
        {
            throw new NotImplementedException();
        }


        public Task SaveLocationAsync(Location location, bool isNew)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Location>> GetAllDataAsync()
        {
            LocationServiceReference.Location[] response = null;
            try
            {
                LocationServiceClient lsr = new LocationServiceClient();

                //await lsr.OpenAsync();
                //LocationServiceReference.Location[] response = await lsr.GetAllLocationsAsync();
                response = lsr.Get
                //await lsr.CloseAsync();
                Debug.WriteLine("ERROR: Der er " + response.Length + " locations i db.");
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR: " + e.Message + " "+ response);
            }
            
            /*
            Items = new List<Location>();
            string locService = "LocationService/GetAllLocations/";
            var uri = new Uri(string.Format(RestUrl + locService, string.Empty));
            var response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync(uri);
                / *if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Location>>(content);
                    Debug.WriteLine(Items.Count);
                }* /
                Debug.WriteLine("Error: you aren't catched - " + response);
            }
            catch (Exception e)
            {
                Debug.WriteLine("ERROR:" + e.Message + " hej " + response);
            }
            */
            return Items;
        }
    }
}
