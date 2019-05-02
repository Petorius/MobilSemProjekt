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
    public class UserRestService : IUserRestService
    {
        private HttpClient _client;
        private const string RestUrl = "http://dmax0917.hegr.dk/";
        public List<User> Items { get; private set; }

        public UserRestService()
        {
            _client = new HttpClient();
        }
        /// <summary>
        /// creates a user in databse
        /// </summary>
        /// <param name="user"></param>
        /// <returns> Task </returns>
        public async Task Create(User user)
        {
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(user));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "UserService.svc/CreateUser",
                        httpContent);
                try
                {
                    // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("CreateUser - Success!");
                    }
                    else
                    {
                        Debug.WriteLine("CreateUser - Failure");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("CreateUser - Error: " + e.Message);
                }
            }
        }
        /// <summary>
        /// compares two hashes
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Task<bool/></returns>
        public async Task<bool> CompareHashes(User user)
        {
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(user));
            bool result = false;
            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "UserService.svc/CompareHashes",
                        httpContent);
                try
                {
                    // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("CompareHashes - Success!");
                    
                        var content = await httpResponse.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<bool>(content);
                    }
                    else
                    {
                        Debug.WriteLine("CompareHashes - Failure");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("CompareHashes - Error: " + e.Message);
                }
            }

            return result;
        }
        /// <summary>
        /// finds a user by its name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Task<User/></returns>
        public async Task<User> FindByUserName(string userName)
        {
            User result = null;
            string locService = "UserService.svc/FindByUserName/" + userName;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<User>(content);
                }

                Debug.WriteLine("FindByUserName - Error: you aren't catched - the result is: " + result);
            }
            catch (Exception e)
            {
                Debug.WriteLine("FindByUserName - Error: " + e.Message);
            }

            return result;
        }
        /// <summary>
        /// finds a users salt
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Task<string/></returns>
        public async Task<string> FindSaltByUserName(string userName)
        {
            string result = "";
            string locService = "UserService.svc/FindSaltByUserName/" + userName;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<string>(content);
                }

                Debug.WriteLine("FindSaltByUserName - Error: you aren't catched - the result is: " + result);
            }
            catch (Exception e)
            {
                Debug.WriteLine("FindSaltByUserName - Error: " + e.Message);
            }

            return result;
        }
    }
}