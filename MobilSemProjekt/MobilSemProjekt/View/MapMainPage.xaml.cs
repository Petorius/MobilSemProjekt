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
using MobilSemProjekt.MVVM.ViewModel;
using Acr.UserDialogs;
using System.Net;
using System.IO;
using MobilSemProjekt.MVVM.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using MobilSemProjekt.MVVM.ViewModel;
using MobilSemProjekt.View;
using Location = MobilSemProjekt.MVVM.Model.Location;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace MobilSemProjekt {
    public partial class MainPage : ContentPage {

        public MainPage() {
            InitializeComponent();

            Content = new StackLayout() {
                Spacing = 5,
                Children =
                {
                    GGMAP,
                    OurEntry
                    
                }


            };


            GGMAP.MapClicked += (sender, e) => placeMarker(e);

                Task task = Task.Run(async () => await GoToCurrentLocation());
            

        }

        protected async override void OnAppearing() {
            base.OnAppearing();
            List<Location> list = await GetLocations();
            foreach (var location in list) {
                GGMAP.Pins.Add(new Pin {
                    Label = location.LocationDescription,
                    Position = new Position(location.Latitude, location.Longitude)
                });
            }
        }

        private async Task<List<Location>> GetLocations() {
            IRestService restService = new RestService();
            return await restService.GetAllDataAsync();
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
                                Console.WriteLine(exception.StackTrace);
                            }
                        }
                    }
                }


                catch (Exception ex) {
                    Console.WriteLine(ex.StackTrace);
                    // Handle exception that may have occurred in geocoding
                }

                GGMAP.Pins.Add(new Pin {
                    Label = geocodeAddress,
                    Position = new Position(e.Point.Latitude, e.Point.Longitude)
                });
                Location fagurt = new Location() {
                    LocationName = nameMarker,
                    Latitude = e.Point.Latitude,
                    Longitude = e.Point.Longitude,
                    LocationDescription = geocodeAddress
                };
                MVVM.ViewModel.IRestService restService = new MVVM.ViewModel.RestService();
                await restService.Create(fagurt);
                //To be added: InfoWindow that contain most of the description and are tied to markers..
            }
        }

        private async void OurEntry_OnCompleted(object sender, EventArgs e) {
            List<Location> combinedList = new List<Location>();
            RestService restService = new RestService();
            var locationListVar = await restService.ReadLocationByTagNameAsync(OurEntry.Text.ToString());
            var locationVar = await restService.ReadLocationByNameAsync(OurEntry.Text.ToString());
            var locationListUserVar = await restService.GetLocationsByUserNameAsync(OurEntry.Text.ToString());
            if (locationListVar != null) {
                combinedList.AddRange(locationListVar);
            }

            if (locationVar != null) {
                combinedList.Add(locationVar);
            }

            if (locationListUserVar != null) {
                combinedList.AddRange(locationListUserVar);
            }

            if (combinedList.Count == 0) {
                try {
                    var address = OurEntry.Text.ToString();
                    var geocodeLocationList = await Geocoding.GetLocationsAsync(address);
                    if (geocodeLocationList != null) {
                        foreach (var geocodeLocationVar in geocodeLocationList) {
                            var placemarks = await Geocoding.GetPlacemarksAsync(geocodeLocationVar.Latitude,
                                geocodeLocationVar.Longitude);

                            foreach (var placemark in placemarks) {
                                if (!string.IsNullOrWhiteSpace(placemark.CountryName) && !string.IsNullOrWhiteSpace(placemark.Locality) &&
                                    !string.IsNullOrWhiteSpace(placemark.PostalCode) && !string.IsNullOrWhiteSpace(placemark.Thoroughfare)) {
                                    string oldGeocodeDescription = "";
                                    string geocodeLocationVarDescription =
                                        $"{placemark.CountryName}, " +
                                        $"{placemark.Locality}, " +
                                        $"{placemark.PostalCode}, " +
                                        $"{placemark.Thoroughfare} " +
                                        $"{placemark.SubThoroughfare} ";

                                    if (!oldGeocodeDescription.Equals(geocodeLocationVarDescription)) {
                                        Location locationFromLocationVar = new Location();
                                        locationFromLocationVar.Latitude = geocodeLocationVar.Latitude;
                                        locationFromLocationVar.Longitude = geocodeLocationVar.Longitude;
                                        locationFromLocationVar.LocationDescription = geocodeLocationVarDescription;
                                        oldGeocodeDescription = geocodeLocationVarDescription;

                                        if (combinedList.Count <= 10 && combinedList.Count > 0) {
                                            foreach (var locationFromLocationVarInCombinedList in combinedList) {
                                                if (locationFromLocationVarInCombinedList.Latitude !=
                                                    locationFromLocationVar.Latitude
                                                    && locationFromLocationVarInCombinedList.Longitude !=
                                                    locationFromLocationVar.Longitude) {
                                                    combinedList.Add(locationFromLocationVar);
                                                }
                                            }

                                        }

                                        if (combinedList.Count == 0) {
                                            combinedList.Add(locationFromLocationVar);
                                        }
                                    }
                                }
                            }

                        }
                    }

                }


                catch (Exception geocodeException) {
                    Console.WriteLine(geocodeException.StackTrace);
                }
            }

            if (combinedList.Count > 0) {
                SearchListView searchListView = new SearchListView();
                searchListView.Locations = new ObservableCollection<Location>(combinedList);
                await Navigation.PushAsync(searchListView);
                //Location location = combinedList.First();
                //Debug.Write("GPS punkter " + location.Latitude + " " + location.Longitude);
            }
        }

        public void GoToLocation(double Latitude, double Longitude) {


            GGMAP.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitude, Longitude),
                Distance.FromMiles(1)));
        }

        public async Task GoToCurrentLocation() {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
            GoToLocation(position.Latitude, position.Longitude);
        }

    }


}