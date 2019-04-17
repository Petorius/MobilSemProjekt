using MobilSemProjekt.MVVM.Model;
using System;
using System.Collections.Generic;
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
		    star1.GestureRecognizers.Add(ReturnCall(1));
            star2.GestureRecognizers.Add(ReturnCall(2));
            star3.GestureRecognizers.Add(ReturnCall(3));
            star4.GestureRecognizers.Add(ReturnCall(4));
		    star5.GestureRecognizers.Add(ReturnCall(5));

            Content = new StackLayout()
		    {
		        Spacing = 5,
		        Children =
		        {
		            picture,
                    locationName,
                    locationDesc,
		            starBar,
                    ratingComment
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
	        
	        MakeStarYellow(1, star1);
	        MakeStarYellow(2, star2);
	        MakeStarYellow(3, star3);
	        MakeStarYellow(4, star4);
	        MakeStarYellow(5, star5);
	    }

	    private void MakeStarYellow(int maxValue, Image image)
	    {
	        if (AvgStars >= maxValue)
	        {
	            image.Source = ImageSource.FromUri(new Uri(startUrl + yellowStar));
	        }
	    }

	    private TapGestureRecognizer ReturnCall(int starNo)
	    {
	        return new TapGestureRecognizer
	        {
	            Command = new Command(() => { SendVote(starNo); }),
	            NumberOfTapsRequired = 1
	        };
	    }

	    private void SendVote(int starNo)
	    {
	        IRatingRestService ratingRestService = new RatingRestService();
	        Rating rating = new Rating
	        {
	            Comment = "It is nice!",
	            Location = Location,
	            User = null,
	            Rate = Convert.ToDouble(starNo)
	        };
	        ratingRestService.Create(rating);
	    }

        private void OnTap1(Xamarin.Forms.View obj)
        {
	        SendVote(1);
	    }

	    private void OnTap2(Xamarin.Forms.View obj)
        {
	        SendVote(2);
        }
	    private void OnTap3(Xamarin.Forms.View obj)
        {
	        SendVote(3);
        }
	    private void OnTap4(Xamarin.Forms.View obj)
        {
	        SendVote(4);
        }
	    private void OnTap5(Xamarin.Forms.View obj)
        {
	        SendVote(5);
        }


    }
}