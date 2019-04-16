using MobilSemProjekt.MVVM.Model;
using System;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DescPage : ContentPage
	{
	    public Location Location { get; set; }
	    public string StarURL { get; set; }
	    private double AvgStars;
	    private string startUrl;
	    private string grayStar;
	    private string yellowStar;

        public DescPage ()
		{
            InitializeComponent ();
		    AvgStars = 0;
		    startUrl = "http://dmax0917.hegr.dk/";
            grayStar = "star-gray.png";
            yellowStar = "star.png";

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

	        star1.Source = ImageSource.FromUri(new Uri(startUrl + grayStar));
            star2.Source = ImageSource.FromUri(new Uri(startUrl + grayStar));
	        star3.Source = ImageSource.FromUri(new Uri(startUrl + grayStar));
	        star4.Source = ImageSource.FromUri(new Uri(startUrl + grayStar));
	        star5.Source = ImageSource.FromUri(new Uri(startUrl + grayStar));
            LoadStars();
	    }

	    private async void LoadStars()
	    {
            IRatingRestService ratingRestService = new RatingRestService();
	        AvgStars = await ratingRestService.GetAverageRating(Location);
	    
	        if (AvgStars >= 1)
	        {
	            star1.Source = ImageSource.FromUri(new Uri(startUrl + yellowStar));
            }
	        if (AvgStars >= 2)
	        {
	            star2.Source = ImageSource.FromUri(new Uri(startUrl + yellowStar));
            }
	        if (AvgStars >= 3)
	        {
	            star3.Source = ImageSource.FromUri(new Uri(startUrl + yellowStar));
            }
	        if (AvgStars >= 4)
	        {
	            star4.Source = ImageSource.FromUri(new Uri(startUrl + yellowStar));
            }
	        if (AvgStars >= 5)
	        {
	            star5.Source = ImageSource.FromUri(new Uri(startUrl + yellowStar));
            }
	    }
	}
}