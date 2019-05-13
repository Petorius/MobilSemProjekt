using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Plugin.Geolocator;
using Xamarin.Essentials;
using System.Linq;
using Acr.UserDialogs;
using System.Collections.ObjectModel;
using System.ServiceModel;
using MobilSemProjekt.MVVM.Exception;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.Service;
using Location = MobilSemProjekt.MVVM.Model.Location;
using Math = System.Math;
using Exception = System.Exception;

namespace MobilSemProjekt.View
{
    public partial class MainPage : ContentPage
    {
        public User User { private get; set; }
        private string StartUrl { get; set; }
        private readonly string _topLocation;

        public MainPage()
        {
            InitializeComponent();

            Content = new StackLayout()
            {
                Children =
                {
                    BtnFindMyLocation,
                    GoogleMap,
                    OurEntry
                }
            };

            _topLocation = "**Top Location** ";
            StartUrl = "http://dmax0917.hegr.dk/";
            BtnFindMyLocation.Source = ImageSource.FromUri(new Uri(StartUrl + "navArrow.png"));
            BtnFindMyLocation.GestureRecognizers.Add(ReturnCall());

            GoogleMap.MapClicked += (sender, e) => PlaceMarker(e);
            Task.Run(async () => await GoToCurrentLocation());
            GoogleMap.PinClicked += (sender, e) => OnTouchAsync(e);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await UpdateLocationsOnMap();
        }

        private TapGestureRecognizer ReturnCall()
        {
            return new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await GoToCurrentLocation();
                }),
                NumberOfTapsRequired = 1
            };
        }

        private async void OnTouchAsync(PinClickedEventArgs e)
        {
            try
            {
                LocationRestService restservice = new LocationRestService();
                string label = e.Pin.Label;

                int pos = label.IndexOf(_topLocation, StringComparison.Ordinal);
                if (label.Contains(_topLocation) && pos >= 0)
                {
                    label = label.Remove(0, _topLocation.Length);
                }

                Location location = await restservice.ReadLocationByNameAsync(label);
                if (location != null)
                {
                    restservice.UpdateHits(location);
                    var page = new DescPage
                    {
                        Location = location,
                        User = User
                    };

                    await Navigation.PushAsync(page);
                }
                else
                {
                    await UpdateLocationsOnMap();
                }
            }
            catch (FaultException<Exception> exc)
            {
                await DisplayAlert("Fejl", exc.Message, "OK");
            }
        }

        private async Task UpdateLocationsOnMap()
        {
            try
            {
                string labelText;
                ILocationRestService restService = new LocationRestService();
                List<Location> list = await restService.GetAllDataAsync();
                foreach (var location in list)
                {
                    if (location.IsTopLocation)
                    {
                        labelText = _topLocation + location.LocationName;
                    }
                    else
                    {
                        labelText = location.LocationName;
                    }

                    GoogleMap.Pins.Add(new Pin
                    {
                        Label = labelText,
                        Position = new Position(location.Latitude, location.Longitude),
                        Address = location.LocationDescription
                        //Burde opdateres til at tage en location address
                    });
                }
            }
            catch (FaultException<NoLocationsInDatabaseException> e)
            {
                await DisplayAlert("Fejl", e.Message, "OK");
            }
            catch (FaultException<Exception> e)
            {
                await DisplayAlert("Fejl", e.Message, "OK");
            }

        }
        
        private async void PlaceMarker(MapClickedEventArgs e)
        {
            try
            {
                var answer = await DisplayAlert("Marker", "Would you like to place a marker", "Yes", "No");
                string geocodeAddress = "";
                string nameMarker = "Unnamed location";
                string urlMarker = "http://dmax0917.hegr.dk/img.png";

                if (answer)
                {

                    var lat = e.Point.Latitude;
                    var lon = e.Point.Longitude;
                    var placemarks = await Geocoding.GetPlacemarksAsync(lat, lon);
                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        geocodeAddress =
                            $"{placemark.Thoroughfare}, " +
                            $"{placemark.PostalCode} " +
                            $"{placemark.Locality}, " +
                            $"{placemark.CountryName}";

                        var nameAnswer = await DisplayAlert("Marker", "Would you like to give the marker a name?",
                            "Yes", "No");
                        if (nameAnswer)
                        {
                            try
                            {
                                PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
                                {
                                    InputType = InputType.Name,
                                    OkText = "Add",
                                    Title = "Enter name",
                                });
                                if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
                                {
                                    nameMarker = pResult.Text;
                                }
                            }
                            catch (FaultException<Exception> exc)
                            {
                                await DisplayAlert("Fejl", exc.Message, "OK");
                            }
                        }

                        var imageAnswer = await DisplayAlert("Marker", "Would you like to give the marker an image?",
                            "Yes", "No");
                        if (imageAnswer)
                        {
                            PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
                            {
                                InputType = InputType.Name,
                                OkText = "Add",
                                Title = "Enter imageurl",
                            });
                            if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
                            {
                                urlMarker = pResult.Text;
                            }
                        }
                    }

                    GoogleMap.Pins.Add(new Pin
                    {
                        Label = geocodeAddress,
                        Position = new Position(e.Point.Latitude, e.Point.Longitude)
                    });
                }

                Location location = new Location()
                {
                    LocationName = nameMarker,
                    Latitude = e.Point.Latitude,
                    Longitude = e.Point.Longitude,
                    LocationDescription = geocodeAddress,
                    User = User,
                    Pictures = new List<Picture>()
                };

                Picture picture = new Picture()
                {
                    Url = urlMarker
                };

                location.Pictures.Add(picture);

                ILocationRestService restService = new LocationRestService();
                await restService.Create(location);
                //To be added: InfoWindow that contain most of the description and are tied to markers..
            }
            catch (FaultException<Exception> exc)
            {
                await DisplayAlert("Fejl", exc.Message, "OK");
            }
        }

        private async void OurEntry_OnCompleted(object sender, EventArgs e)
        {
            try
            {
                List<Location> combinedList = new List<Location>();
                LocationRestService restService = new LocationRestService();
                var locationListVar = await restService.ReadLocationByTagNameAsync(OurEntry.Text);
                var locationVar = await restService.ReadLocationByNameAsync(OurEntry.Text);
                var locationListUserVar = await restService.GetLocationsByUserNameAsync(OurEntry.Text);
                if (locationListVar != null)
                {
                    combinedList.AddRange(locationListVar);
                }

                if (locationVar != null)
                {
                    combinedList.Add(locationVar);
                }

                if (locationListUserVar != null)
                {
                    combinedList.AddRange(locationListUserVar);
                }

                if (combinedList.Count == 0)
                {
                    var address = OurEntry.Text;
                    var geocodeLocationList = await Geocoding.GetLocationsAsync(address);
                    if (geocodeLocationList != null)
                    {
                        foreach (var geocodeLocationVar in geocodeLocationList)
                        {
                            var placemarks = await Geocoding.GetPlacemarksAsync(geocodeLocationVar.Latitude,
                                geocodeLocationVar.Longitude);

                            foreach (var placemark in placemarks)
                            {
                                if (!string.IsNullOrWhiteSpace(placemark.CountryName) && !string.IsNullOrWhiteSpace(placemark.Locality) &&
                                    !string.IsNullOrWhiteSpace(placemark.PostalCode) && !string.IsNullOrWhiteSpace(placemark.Thoroughfare))
                                {
                                    string oldGeocodeDescription = "";
                                    string geocodeLocationVarDescription =
                                        $"{placemark.CountryName}, " +
                                        $"{placemark.Locality}, " +
                                        $"{placemark.PostalCode}, " +
                                        $"{placemark.Thoroughfare} " +
                                        $"{placemark.SubThoroughfare} ";

                                    if (!oldGeocodeDescription.Equals(geocodeLocationVarDescription))
                                    {
                                        Location locationFromLocationVar = new Location()
                                        {
                                            Latitude = geocodeLocationVar.Latitude,
                                            Longitude = geocodeLocationVar.Longitude,
                                            LocationDescription = geocodeLocationVarDescription
                                        };

                                        //oldGeocodeDescription = geocodeLocationVarDescription;

                                        if (combinedList.Count <= 10 && combinedList.Count > 0)
                                        {
                                            foreach (var locationFromLocationVarInCombinedList in combinedList)
                                            {
                                                if (Math.Abs(locationFromLocationVarInCombinedList.Latitude - locationFromLocationVar.Latitude) > 0.001
                                                    && Math.Abs(locationFromLocationVarInCombinedList.Longitude - locationFromLocationVar.Longitude) > 0.001)
                                                {
                                                    combinedList.Add(locationFromLocationVar);
                                                }
                                            }
                                        }

                                        if (combinedList.Count == 0)
                                        {
                                            combinedList.Add(locationFromLocationVar);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (combinedList.Count > 0)
                {
                    SearchListView searchListView = new SearchListView
                    {
                        Locations = new ObservableCollection<Location>(combinedList)
                    };
                    await Navigation.PushAsync(searchListView);
                }
            }
            catch (FaultException<Exception> exc)
            {
                await DisplayAlert("Fejl", exc.Message, "OK");
            }
        }

        public void GoToLocation(double latitude, double longitude)
        {
            GoogleMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude),
                Distance.FromMiles(1)));
        }

        public async Task GoToCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
            GoToLocation(position.Latitude, position.Longitude);
        }
    }
}