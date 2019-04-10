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

namespace MobilSemProjekt.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchListView : ContentPage
    {
        public ObservableCollection<Location> Locations { get; set; }

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
            Location location = (Location) e.Item;
            MainPage mainPage = new MainPage();
            mainPage.StartUp = false;
            await Navigation.PopAsync();
            await Navigation.PushAsync(mainPage);
            mainPage.GoToLocation(location.Latitude, location.Longitude);
            Debug.WriteLine("Gå til " + location.Latitude + " " + location.Longitude);

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
