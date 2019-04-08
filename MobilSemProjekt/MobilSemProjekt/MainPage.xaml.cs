using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Plugin.Geolocator;
using Xamarin.Forms.Internals;
using SQLite;
using Xamarin.Essentials;
using System.Linq;
using MobilSemProjekt.ViewModel;
using Acr.UserDialogs;
using System.Net;
using System.IO;
using MobilSemProjekt.MVVM.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MobilSemProjekt {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();



            Content = new StackLayout() {
                Spacing = 5,
                Children =
                {
                    GGMAP,
                    OurEntry,
                    Beton
                }

            };

            GGMAP.MapClicked += (sender, e) => placeMarker(e);

            //    IRestService restService = new RestService();
            //  restService.GetAllDataAsync();


        }

        private async void placeMarker(MapClickedEventArgs e) {
            var answer = await DisplayAlert("Marker", "Would you like to place a marker", "Yes", "No");
            string geocodeAddress = "";
            string nameMarker = "";
            if (answer) {
                try {

                    var lat = e.Point.Latitude;
                    var lon = e.Point.Longitude;

                    var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);

                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null) {
                        geocodeAddress =
                           $"{placemark.CountryName}, " +
                           $"{placemark.Locality}, " +
                           $"{placemark.PostalCode}, " +
                           $"{placemark.Thoroughfare} ";

                        var nameAnswer = await DisplayAlert("Marker", "Would you like to give the marker a name?", "Yes", "No");
                        if (nameAnswer) {
                            try {
                                PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig {
                                    InputType = InputType.Name,
                                    OkText = "Add",
                                    Title = "Enter name",
                                });
                                if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text)) {
                                    nameMarker = pResult.Text;
                                }
                            }
                            catch (Exception exception) {

                            }
                        }
                    }
                }


                catch (Exception ex) {
                    // Handle exception that may have occurred in geocoding
                }

                GGMAP.Pins.Add(new Pin {
                    Label = geocodeAddress,
                    Position = new Position(e.Point.Latitude, e.Point.Longitude)
                });
                //To be added: InfoWindow that contain most of the description and are tied to markers..
            }
        }

        private async Task PushDataThingyAsync()
        {
            var thingy = new MVVM.Model.Location
            {
                LocationName = "Ole",
                LocationDescription = "En person, der har fået ny autobil",
                Latitude = 57,
                Longitude = 9,
                User = null
            };

            // Serialize our concrete class into a JSON String
            var stringThingy = await Task.Run(() => JsonConvert.SerializeObject(thingy));

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

        private void Beton_OnClicked(object sender, EventArgs e)
        {
            Task.Run(() => PushDataThingyAsync());
        }
    }
}
