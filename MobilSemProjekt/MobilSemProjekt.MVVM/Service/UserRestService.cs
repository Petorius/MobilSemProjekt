using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Exception;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.Service
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
        /// Creates a user in database
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Task</returns>
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
                // If the response contains content we want to read it!
                if (httpResponse.IsSuccessStatusCode)
                {
                    //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Debug.WriteLine("CreateUser - successful");
                }
                else
                {
                    Debug.WriteLine("CreateUser - Failure");
                }
            }
        }
        /// <summary>
        /// Compares two hashes
        /// </summary>
        /// <param name="user">User</param>
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
                // If the response contains content we want to read it!
                if (httpResponse.IsSuccessStatusCode)
                {
                    //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Debug.WriteLine("CompareHashes - successful");
                
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<bool>(content);
                }
                else
                {
                    Debug.WriteLine("CompareHashes - Failure");
                    throw new UserOrPasswordException();
                }
            }
            return result;
        }
        /// <summary>
        /// finds a user by its name
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>Task<User/></returns>
        public async Task<User> FindByUserName(string userName)
        {
            User result = null;
            string locService = "UserService.svc/FindByUserName/" + userName;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<User>(content);
                Debug.WriteLine("FindByUserName - successful");
            }

            return result;
        }
        /// <summary>
        /// Finds a users salt
        /// </summary>
        /// <param name="userName">string</param>
        /// <returns>Task<string/></returns>
        public async Task<string> FindSaltByUserName(string userName)
        {
            string result = "";
            string locService = "UserService.svc/FindSaltByUserName/" + userName;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<string>(content);
                Debug.WriteLine("FindSaltByUserName - successful");
            }
            else
            {
                throw new UserOrPasswordException();
            }
            
            return result;
        }
    }
}