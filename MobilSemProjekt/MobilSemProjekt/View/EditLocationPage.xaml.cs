using System;
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

        private void SaveLocationEditsButton_OnClicked(object sender, EventArgs e)
        {
            Location.LocationName = LocationNameEntry.Text;
            Location.LocationDescription = LocationDescriptionEditor.Text;

            ILocationRestService restService = new LocationRestService();
            restService.UserUpdateLocation(Location);
        }
    }
}