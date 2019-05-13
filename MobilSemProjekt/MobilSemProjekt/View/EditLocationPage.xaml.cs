using System;
using System.ServiceModel;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditLocationPage : ContentPage
	{
        public Location Location { get; set; }

		public EditLocationPage ()
		{
			InitializeComponent ();

        }

        public void SetPlaceholders()
        {
            LocationNameEntry.Placeholder= Location.LocationName;
            LocationDescriptionEditor.Placeholder = Location.LocationDescription;
        }

        private async void SaveLocationEditsButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                Location.LocationName = LocationNameEntry.Text;
                Location.LocationDescription = LocationDescriptionEditor.Text;

                ILocationRestService restService = new LocationRestService();
                restService.UserUpdateLocation(Location);
            }
            catch (FaultException<Exception> exc)
            {
                await DisplayAlert("Fejl", exc.Message, "OK");
            }
        }
    }
}