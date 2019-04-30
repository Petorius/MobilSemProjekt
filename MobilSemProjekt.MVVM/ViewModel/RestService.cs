﻿using Newtonsoft.Json;
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

                Debug.WriteLine("ReadLocationByName - Error: you aren't catched - " + response);
            }
            catch (Exception e) {
                Debug.WriteLine("ReadLocationByName - Error: " + e.Message);
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

                Debug.WriteLine("ReadLocationByTagName - Error: you aren't catched - " + response);
            }
            catch (Exception e) {
                Debug.WriteLine("ReadLocationByTagName - Error: " + e.Message + " hej " + response);
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

                try
                {
                    // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("CreateLocation - Success!");
                    }
                    else
                    {
                        Debug.WriteLine("CreateLocation - Failure");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("CreateLocation - Error: " + e.Message);
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

                Debug.WriteLine("GetAllData - Error: you aren't catched - " + response);
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetAllData - Error: " + e.Message + " hej " + response);
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

                Debug.WriteLine("GetLocationsByUserName - Error: you aren't catched - " + response);
            }
            catch (Exception e) {
                Debug.WriteLine("GetLocationsByUserName - Error: " + e.Message);
            }

            return Items;

        }

        public async void UpdateHits(Location location)
        {
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(location));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient()) {

                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "LocationService.svc/UpdateHits",
                        httpContent);

                try {
                    // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode) {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("UpdateHits - Success!");
                    }
                    else {
                        Debug.WriteLine("UpdateHits - Failure");
                    }
                }
                catch (Exception e) {
                    Debug.WriteLine("UpdateHits - Error: " + e.Message);
                }
            }
        }

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

                try {
                    // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode) {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("UpdateLocation - Success!");
                    }
                    else {
                        Debug.WriteLine("UpdateLocation - Failure");
                    }
                }
                catch (Exception e) {
                    Debug.WriteLine("UpdateLocation - Error: " + e.Message);
                }
            }
        }



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

                try
                {
                    // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("UserUpdateLocation - Success!");
                    }
                    else
                    {
                        Debug.WriteLine("UserUpdateLocation - Failure");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("UserUpdateLocation - Error: " + e.Message);
                }
            }
        }
    }
}