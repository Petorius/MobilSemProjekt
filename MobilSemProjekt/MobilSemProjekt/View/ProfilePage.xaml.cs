using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Themes;
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
            RestService restService = new RestService();
            List<Location> userLocationList = await restService.GetLocationsByUserNameAsync(User.UserName);
            SearchListView searchListView = new SearchListView {
            Locations = new ObservableCollection<Location>(userLocationList)
            };
            searchListView.IsUserLocationSearch = true;
            await Navigation.PushAsync(searchListView);
        }
	    private async void SeeMyRatingsButton_OnClicked(object sender, EventArgs e)
	    {
	        RestService restService = new RestService();
	        List<Location> locationWithCommentsList = await restService.GetLocationsByCommentUserName(User.UserName);
	        SearchListView searchListView = new SearchListView
	        {
	            Locations = new ObservableCollection<Location>(locationWithCommentsList),
	            IsUserCommentSearch = true
            };
	        searchListView.User = User;

            await Navigation.PushAsync(searchListView);
	    }
    }
}