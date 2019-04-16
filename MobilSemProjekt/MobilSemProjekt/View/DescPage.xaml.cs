using MobilSemProjekt.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DescPage : ContentPage
	{
	    public Location Location { get; set; }
	    public string StarURL { get; set; }

	    public DescPage ()
		{
			InitializeComponent ();
		    StarURL = @"img\stjerne.png";
		    Content = new StackLayout()
		    {
		        Spacing = 5,
		        Children =
		        {
		            picture,
                    locationName,
                    locationDesc,
                    star1,
                    star2,
                    star3,
                    star4,
                    star5
                    
		        }
		    };
        }

	    protected override void OnAppearing()
	    {
            base.OnAppearing();
            picture.Source = ImageSource.FromUri(new Uri("http://dmax0917.hegr.dk/img.png"));
            locationName.Text = Location.LocationName;

	        locationDesc.Text = Location.LocationDescription;
	        star1.Source = ImageSource.FromFile("img/Stjerne.png");
	        star2.Source = ImageSource.FromUri(new Uri("http://dmax0917.hegr.dk/img.png"));
	        star3.Source = ImageSource.FromUri(new Uri("http://dmax0917.hegr.dk/img.png"));
	        star4.Source = ImageSource.FromUri(new Uri("http://dmax0917.hegr.dk/img.png"));
	        star5.Source = ImageSource.FromUri(new Uri("http://dmax0917.hegr.dk/img.png"));
        }


    }
}