using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using Xamarin.Essentials;
using Location = MobilSemProjekt.MVVM.Model.Location;

namespace MobilSemProjekt.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchListView : ContentPage
    {
        public ObservableCollection<Location> Locations { get; set; }
        public bool IsUserLocationSearch { get; set; }
        public bool IsUserCommentSearch { get; set; }
        public User User { get; set; }

        public SearchListView()
        {
            InitializeComponent();

            //Locations = new ObservableCollection<Location>();

            //MyListView.ItemsSource = Locations;

            //Content = new StackLayout() {
            //    Spacing = 5,
            //    Children =
            //    {
            //        SearchListViewDisplay
            //    }

            //};

        }

        protected override void OnAppearing() {
            SearchListViewDisplay.ItemsSource = Locations;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (IsUserLocationSearch)
            {
                Location location = (Location)e.Item;
                EditLocationPage editLocationPage = new EditLocationPage();
                editLocationPage.Location = location;
                editLocationPage.SetPlaceholders();
                await Navigation.PushAsync(editLocationPage);
            }
            else if (IsUserCommentSearch && User != null)
            {
                Location location = (Location)e.Item;
                EditLocationPage editLocationPage = new EditLocationPage();
                editLocationPage.Location = location;
                editLocationPage.SetPlaceholders();
                await Navigation.PushAsync(editLocationPage);
            }

            else
            {

                var itemTappedAnswer = await DisplayAlert("Lokation", "What do you want to do?", "Go to location",
                    "Add location");
                Location location = (Location) e.Item;


                if (itemTappedAnswer)
                {
                    MainPage mainPage = new MainPage();
                    await Navigation.PushAsync(mainPage);
                    mainPage.GoToLocation(location.Latitude, location.Longitude);
                    Debug.WriteLine("Gå til " + location.Latitude + " " + location.Longitude);
                }

                if (!itemTappedAnswer)
                {
                    try
                    {
                        string geocodeAddress;

                        var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);

                        var placemark = placemarks?.FirstOrDefault();
                        if (placemark != null)
                        {
                            geocodeAddress =
                                $"{placemark.CountryName}, " +
                                $"{placemark.Locality}, " +
                                $"{placemark.PostalCode}, " +
                                $"{placemark.Thoroughfare} ";


                            try
                            {
                                PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
                                {
                                    InputType = InputType.Name,
                                    OkText = "Add",
                                    Title = "Enter name for the location",
                                });
                                if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
                                {
                                    location.LocationName = pResult.Text;
                                }
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.StackTrace);
                            }
                        }
                    }


                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                        // Handle exception that may have occurred in geocoding
                    }

                    IRestService restService = new RestService();
                    await restService.Create(location);
                }
            }

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

    }
}
