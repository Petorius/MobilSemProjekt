using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.ViewModel
{
    public class RestService : IRestService
    {
        private HttpClient _client;
        private const string RestUrl = "http://dmax0917.hegr.dk/";
        public List<Location> Items { get; private set; }

        public RestService()
        {
            _client = new HttpClient();
        }

        public Task DeleteLocationAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Location> ReadLocationByNameAsync(string name)
        {
            Location location = new Location();
            string locService = "LocationService.svc/GetLocationByLocationName/" + name;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = new HttpResponseMessage();
            try {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    location = JsonConvert.DeserializeObject<Location>(content);
                }

                Debug.WriteLine("Error: you aren't catched - " + response);
            }
            catch (Exception e) {
                Debug.WriteLine("Error: " + e.Message + " hej " + response);
            }

            return location;
        }

        public async Task<List<Location>> ReadLocationByTagNameAsync(string tagName)
        {
            Items = new List<Location>();
            string locService = "LocationService.svc/GetLocationsByTagName/" + tagName;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = new HttpResponseMessage();
            try {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Location>>(content);
                    Debug.WriteLine(Items.Count);
                }

                Debug.WriteLine("Error: you aren't catched - " + response);
            }
            catch (Exception e) {
                Debug.WriteLine("Error: " + e.Message + " hej " + response);
            }

            return Items;
        }


        public async Task Create(Location location)
        {
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(location));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "LocationService.svc/CreateLocation",
                        httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }
            }
        }


        public async Task<List<Location>> GetAllDataAsync()
        {

            Items = new List<Location>();
            string locService = "LocationService.svc/GetAllLocations";
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Location>>(content);
                    Debug.WriteLine(Items.Count);
                }

                Debug.WriteLine("Error: you aren't catched - " + response);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message + " hej " + response);
            }

            return Items;
        }

        public async Task<List<Location>> GetLocationsByUserNameAsync(string name) {
            Items = new List<Location>();
            string locService = "LocationService.svc/GetLocationsByUserName/" + name;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = new HttpResponseMessage();
            try {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode) {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Location>>(content);
                    Debug.WriteLine(Items.Count);
                }

                Debug.WriteLine("Error: you aren't catched - " + response);
            }
            catch (Exception e) {
                Debug.WriteLine("Error: " + e.Message + " hej " + response);
            }

            return Items;

        }
    }
}