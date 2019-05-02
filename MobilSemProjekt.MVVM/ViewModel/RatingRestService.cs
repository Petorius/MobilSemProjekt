using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
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
        public List<Rating> RatingItems { get; private set; }
        public RatingRestService()
        {
            _client = new HttpClient();
        }
        /// <summary>
        /// create a rating in the database
        /// </summary>
        /// <param name="rating"></param>
        /// <returns>Task</returns>
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
                Debug.WriteLine(httpResponse);
                try
                {
                    // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("CreateRating - Success!");
                    }
                    else
                    {
                        Debug.WriteLine("CreateRating - Failure");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("CreateRating - Error: " + e.Message);
                }
            }
        }
        /// <summary>
        /// gets the average rating of a location
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Task<double/></returns>
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

                Debug.WriteLine("Ratings - Error: you aren't catched - the result is: " + result);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Ratings - Error: " + e.Message);
            }

            return result;
        }

        
        /// <summary>
        ///     Updates a rating in Database
        /// </summary>
        /// <param name="rating"></param>
        public async void Update(Rating rating2)
        {
            Rating rating = new Rating
            {
                LocationId = rating2.LocationId,
                RatingId = rating2.RatingId,
                User = rating2.User,
                Rate = rating2.Rate,
                Comment = rating2.Comment
            };
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(rating));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");


            using (var httpClient = new HttpClient())
            {
                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "RatingService.svc/UserUpdateRating",
                        httpContent);
                Debug.WriteLine(httpResponse);
                try
                {
                   // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("UserUpdateRating - Success!");
                        //var content = await httpResponse.Content.ReadAsStringAsync();
                        // result = JsonConvert.DeserializeObject<bool>(content);
                    }
                    else
                    {
                        Debug.WriteLine("UpdateRating - Failure");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("UpdateRating - Error: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Gets all ratings tied to user in database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Task<List<Rating/>/></returns>
        public async Task<List<Rating>> GetRatingsByUserName(string name)
        {
            RatingItems = new List<Rating>();
            string RateService = "RatingService.svc/GetRatingsByUserName/" + name;
            var uri = new Uri(string.Format(RestUrl + RateService));
            var response = new HttpResponseMessage();
            try
            {
                response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    RatingItems = JsonConvert.DeserializeObject<List<Rating>>(content);
                    Debug.WriteLine(RatingItems.Count);
                }

                Debug.WriteLine("GetRatingsByUserName - Error: you aren't catched - " + response);
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetRatingsByUserName - Error: " + e.Message);
            }

            return RatingItems;

        }
        /// <summary>
        /// Deletes a Rating in database
        /// </summary>
        /// <param name="rating"></param>
        /// <returns>Task<bool/></returns>
        public async Task<bool> Delete(Rating rating)
        {
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(rating));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");
            var result = false;

            using (var httpClient = new HttpClient())
            {
                // Do the actual request and await the response
                var httpResponse =
                    await httpClient.PostAsync(RestUrl + "RatingService.svc/DeleteRating",
                        httpContent);
                Debug.WriteLine(httpResponse);
                try
                {
                    // If the response contains content we want to read it!
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        //var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        Debug.WriteLine("DeleteRating - Success!");
                        var content = await httpResponse.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<bool>(content);
                    }
                    else
                    {
                        Debug.WriteLine("DeleteRating - Failure");
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("DeleteRating - Error: " + e.Message);
                }
            }

            return result;
        }
    }
}