using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using LocationServiceReference;
using Location = MobilSemProjekt.MVVM.Model.Location;

namespace MobilSemProjekt.MVVM.ViewModel
{
    public class UserRestService : IUserRestService
    {
        private HttpClient _client;
        private const string RestUrl = "http://dmax0917.hegr.dk/";
        public List<Location> Items { get; private set; }

        public UserRestService()
        {
            _client = new HttpClient();
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