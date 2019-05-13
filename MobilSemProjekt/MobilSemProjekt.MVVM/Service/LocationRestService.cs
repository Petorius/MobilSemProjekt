using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.Service
{
    public class LocationRestService : ILocationRestService
    {
        private HttpClient _client;
        private const string RestUrl = "http://dmax0917.hegr.dk/";
        public List<Location> Items { get; private set; }

        public LocationRestService()
        {
            _client = new HttpClient();
        }
        /// <summary>
        /// Gets a location using its name in database
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>Task<Location/></returns>
        public async Task<Location> ReadLocationByNameAsync(string name)
        {
            Location location = new Location();
            string locService = "LocationService.svc/GetLocationByLocationName/" + name;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                location = JsonConvert.DeserializeObject<Location>(content);
            }

            Debug.WriteLine("ReadLocationByName - successful");
            
            return location;
        }
        /// <summary>
        /// Gets a location using its tagname in database
        /// </summary>
        /// <param name="tagName">string</param>
        /// <returns>Task<List<Location/>/></returns>
        public async Task<List<Location>> ReadLocationByTagNameAsync(string tagName)
        {
            Items = new List<Location>();
            string locService = "LocationService.svc/GetLocationsByTagName/" + tagName;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<Location>>(content);
                Debug.WriteLine(Items.Count);
            }

            Debug.WriteLine("ReadLocationByTagName  - successful");
            
            return Items;
        }

        /// <summary>
        /// Creates a location in Database
        /// </summary>
        /// <param name="location">Location</param>
        /// <returns>Task</returns>
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
                if (httpResponse.IsSuccessStatusCode)
                {
                    //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Debug.WriteLine("CreateLocation - successful");
                }
                else
                {
                    Debug.WriteLine("CreateLocation - Failure");
                }
            }
        }

        /// <summary>
        /// Gets all locations in database
        /// </summary>
        /// <returns>Task<List<Location/>/></returns>
        public async Task<List<Location>> GetAllDataAsync()
        {

            Items = new List<Location>();
            string locService = "LocationService.svc/GetAllLocations";
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<Location>>(content);
                Debug.WriteLine(Items.Count);
            }

            Debug.WriteLine("GetAllData - successful");

            return Items;
        }

        /// <summary>
        /// Gets all locations created by user in database
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>Task<List<Location/>/></returns>
        public async Task<List<Location>> GetLocationsByUserNameAsync(string name) {
            Items = new List<Location>();
            string locService = "LocationService.svc/GetLocationsByUserName/" + name;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<Location>>(content);
                Debug.WriteLine(Items.Count);
            }

            Debug.WriteLine("GetLocationsByUserName - successful");
            
            return Items;

        }
        /// <summary>
        /// Gets all location rated by in database
        /// </summary>
        /// <param name="username">string</param>
        /// <returns>Task<List<Location/>/></returns>
        public async Task<List<Location>> GetLocationsByCommentUserName(string username)
        {
            Items = new List<Location>();
            string locService = "LocationService.svc/GetLocationsByCommentUserName/" + username;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<List<Location>>(content);
                Debug.WriteLine(Items.Count);
            }

            Debug.WriteLine("GetLocationsByCommentUserName - successful");
            
            return Items;
        }
        /// <summary>
        /// Updates number of hits on a location in database
        /// </summary>
        /// <param name="location">Location</param>
        public async void UpdateHits(Location location2)
        {
            Location location = new Location
            {
                LocationId = location2.LocationId
            };
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(location));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient()) {

                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "LocationService.svc/UpdateHits",
                        httpContent);
                // If the response contains content we want to read it!
                if (httpResponse.IsSuccessStatusCode) {
                    //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Debug.WriteLine("UpdateHits - successful");
                }
                else {
                    Debug.WriteLine("UpdateHits - Failure");
                }
            }
        }
        /// <summary>
        /// Updates a location in database
        /// </summary>
        /// <param name="location"></param>
        public async void UpdateLocation(Location location) {
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(location));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient()) {

                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "LocationService.svc/UpdateLocation",
                        httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.IsSuccessStatusCode) {
                    //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Debug.WriteLine("UpdateLocation - successful");
                }
                else {
                    Debug.WriteLine("UpdateLocation - Failure");
                }
            }
        }


        /// <summary>
        /// Updates a location based on user input
        /// </summary>
        /// <param name="location2">Location</param>
        public async void UserUpdateLocation(Location location2)
        {
            // Serialize our concrete class into a JSON String
            Location location = new Location
            {
                LocationName = location2.LocationName,
                LocationId = location2.LocationId,
                LocationDescription = location2.LocationDescription

            };
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(location));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "LocationService.svc/UserUpdateLocation",
                        httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.IsSuccessStatusCode)
                {
                    //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Debug.WriteLine("UserUpdateLocation - successful");
                }
                else
                {
                    Debug.WriteLine("UserUpdateLocation - Failure");
                }
            }
        }

        /// <summary>
        /// Finds and sets a toplocation
        /// </summary>
        public async void SetTopLocations()
        {
            string locService = "LocationService.svc/SetTopLocations";
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("SetTopLocations - successful");
            }
        }
    }
}