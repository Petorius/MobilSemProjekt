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
    public partial class RatingSearchListView : ContentPage
    {
        public ObservableCollection<Rating> Ratings { get; set; }
        public bool IsUserRatingSearch { get; set; }

        public RatingSearchListView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing() {
            SearchListViewDisplay.ItemsSource = Ratings;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Rating rating = (Rating)e.Item;
            EditRatingPage editRatingPage = new EditRatingPage();
            editRatingPage.Rating = rating;
            editRatingPage.SetPlaceholders();
            await Navigation.PushAsync(editRatingPage);
        
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
