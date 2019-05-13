using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
        public User User { get; set; }

		public ProfilePage ()
		{
			InitializeComponent();
        }

        private async void SeeMyLocationsButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                LocationRestService restService = new LocationRestService();
                List<Location> userLocationList = await restService.GetLocationsByUserNameAsync(User.UserName);
                SearchListView searchListView = new SearchListView
                {
                    Locations = new ObservableCollection<Location>(userLocationList),
                    IsUserLocationSearch = true
                };
                await Navigation.PushAsync(searchListView);
            }
            catch (FaultException<Exception> exc)
            {
                await DisplayAlert("Fejl", exc.Message, "OK");
            }
        }
	    private async void SeeMyRatingsButton_OnClicked(object sender, EventArgs e)
	    {
	        try
	        {
	            LocationRestService restService = new LocationRestService();
	            List<Location> locationWithCommentsList = await restService.GetLocationsByCommentUserName(User.UserName);
	            SearchListView searchListView = new SearchListView
	            {
	                Locations = new ObservableCollection<Location>(locationWithCommentsList),
	                IsUserCommentSearch = true,
	                User = User
	            };
	            await Navigation.PushAsync(searchListView);
            }
	        catch (FaultException<Exception> exc)
	        {
	            await DisplayAlert("Fejl", exc.Message, "OK");
	        }
	    }
    }
}