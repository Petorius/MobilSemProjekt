using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<Location> locations { get; set; }

        public SearchListView()
        {
            InitializeComponent();

            //locations = new ObservableCollection<Location>();

            //MyListView.ItemsSource = locations;

            //Content = new StackLayout() {
            //    Spacing = 5,
            //    Children =
            //    {
            //        SearchListViewDisplay
            //    }

            //};

        }

        protected override void OnAppearing() {
            SearchListViewDisplay.ItemsSource = locations;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async Task<List<Location>> GetLocations() {
            IRestService restService = new RestService();
            return await restService.GetAllDataAsync();
        }
    }
}
