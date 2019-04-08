using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
//using LocationServiceReference;
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


        public async Task Create(Location location)
        {
            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(location));

            // Wrap our JSON inside a StringContent which then can be used by the HttpClient class
            var httpContent = new StringContent(stringThingy, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                // Do the actual request and await the response
                var httpResponse = await httpClient.PostAsync("http://dmax0917.hegr.dk/LocationService.svc/CreateLocation", httpContent);

                // If the response contains content we want to read it!
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }
            }
        }

        public Task<List<Location>> GetAllDataAsync()
        {
            throw new NotImplementedException();
        }
        /*
public async Task<List<Location>> GetAllDataAsync()
{
   LocationServiceReference.Location response = null;
   try
   {
       LocationServiceClient lsr = new LocationServiceClient();

       //LocationServiceReference.Location[] response = await lsr.GetAllLocationsAsync();
       Task noget = lsr.GetLocationByIdAsync(1);

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

   return Items;
}*/
    }
}
