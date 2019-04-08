﻿using System;
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
using MobilSemProjekt.MVVM.ViewModel;
using Location = MobilSemProjekt.MVVM.Model.Location;

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
            
            //    IRestService restService = new RestService();
            //  restService.GetAllDataAsync();


        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            List<Location> list = await GetLocations();
            foreach (var location in list)
            {
                GGMAP.Pins.Add(new Pin
                {
                    Label = location.LocationDescription,
                    Position = new Position(location.Latitude, location.Longitude)
                });
            }
        }

        private async Task<List<Location>> GetLocations()
        {
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
                Location fagurt = new Location()
                {
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
    }
}
