using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.Model;
using Newtonsoft.Json;

namespace MobilSemProjekt.ViewModel
{
    public class RestService : IRestService
    {
        private HttpClient Client;
        private const string RestUrl = "KonndtKylllær";
        public List<Location> Items { get; private set;}
        
        public RestService()
        {
            Client = new HttpClient();
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


        public Task SaveLocationAsync(Location location, bool isNew)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Location>> GetAllDataAsync()
        {
            Items = new List<Location>();
            var uri = new Uri(string.Format(RestUrl,string.Empty));
            try
            {
                var response = await Client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<List<Location>>(content);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR: {0}",e.Message);
            }
            Debug.WriteLine(Items.Count);
            return Items;
        }
    }
}
