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
	    private string startUrl;

	    public DescPage ()
		{
            InitializeComponent ();
		    startUrl = "http://dmax0917.hegr.dk/";
            StarURL = @"img\stjerne.png";
		    Content = new StackLayout()
		    {
		        Spacing = 5,
		        Children =
		        {
		            picture,
                    locationName,
                    locationDesc,
		            starBar
                }
		    };
        }

	    protected override void OnAppearing()
	    {
            base.OnAppearing();
            picture.Source = ImageSource.FromUri(new Uri(startUrl + "img.png"));
            locationName.Text = Location.LocationName;

	        locationDesc.Text = Location.LocationDescription;
	        star1.Source = ImageSource.FromUri(new Uri(startUrl + "star.png"));
            star2.Source = ImageSource.FromUri(new Uri(startUrl + "star.png"));
	        star3.Source = ImageSource.FromUri(new Uri(startUrl + "star.png"));
	        star4.Source = ImageSource.FromUri(new Uri(startUrl + "star.png"));
	        star5.Source = ImageSource.FromUri(new Uri(startUrl + "star.png"));
        }


    }
}