using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingPage : ContentPage
	{
		public SettingPage ()
		{
			InitializeComponent ();

		    Content = new StackLayout()
		    {
		        Spacing = 5,
		        Children =
		        {
		            ShowItAll
		        }
		    };
        }

	    //private void BtnSettings_OnClicked(object sender, EventArgs e)
	    //{
	        
	    //}

	    //private void BtnProfile_OnClicked(object sender, EventArgs e)
	    //{
	        
	    //}

	    //private void BtnLocations_OnClicked(object sender, EventArgs e)
	    //{
	        
	    //}

	    //private void BtnRatings_OnClicked(object sender, EventArgs e)
	    //{
	        
	    //}

	    //private void BtnAdmin_OnClicked(object sender, EventArgs e)
	    //{
	        
	    //}
	}
}