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
    public class UserRestService : IUserRestService
    {
        private HttpClient _client;
        private const string RestUrl = "http://localhost:24090/";
        public List<User> Items { get; private set; }

        public UserRestService()
        {
            _client = new HttpClient();
        }

        public async Task Create(User user)
        {
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(user));
            Debug.WriteLine("Welcome "+ user.UserName + " " + user.HashPassword + " " + user.Salt);
            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");
            Debug.WriteLine("Welcome-1");
            using (var httpClient = new HttpClient())
            {
                Debug.WriteLine("Welcome0");
                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "UserService.svc/CreateUser",
                        httpContent);
                Debug.WriteLine("Welcome1");
                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    Debug.WriteLine("Welcome2");
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }
            }
        }

        public async Task<bool> CompareHashes(string userName, string userHash)
        {
            bool result = false;
            string locService = "UserService.svc/CompareHashes/" + userName + "/" + userHash;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<bool>(content);
                }

                Debug.WriteLine("Error: you aren't catched - the result is: " + response);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
            }

            return result;
        }

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

                Debug.WriteLine("Error: you aren't catched - the result is: " + result);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
            }

            return result;
        }
    }
}