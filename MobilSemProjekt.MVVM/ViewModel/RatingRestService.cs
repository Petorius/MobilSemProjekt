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
    public class RatingRestService : IRatingRestService
    {
        private HttpClient _client;
        private const string RestUrl = "http://dmax0917.hegr.dk/";
        public List<User> Items { get; private set; }

        public RatingRestService()
        {
            _client = new HttpClient();
        }

        public async Task Create(Rating rating)
        {
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(rating));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "RatingService.svc/CreateRating",
                        httpContent);
                try
                {
                    // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("Success!");
                    }
                    else
                    {
                        Debug.WriteLine("Failure");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error: " + e.Message);
                }
            }
        }

        public async Task<double> GetAverageRating(Location location)
        {
            double result = 0;
            string locService = "RatingService.svc/GetAverageRating/" + location.LocationId;
            var uri = new Uri(string.Format(RestUrl + locService));
            var response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<double>(content);
                }

                Debug.WriteLine("Error: you aren't catched - the result is: " + result);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.Message);
            }

            return result;
        }

        public Task Update(Rating rating, int ratingId) //TOBE async
        {
            throw new NotImplementedException();
        }
    }
}