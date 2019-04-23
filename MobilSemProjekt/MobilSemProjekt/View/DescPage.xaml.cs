using MobilSemProjekt.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DescPage : ContentPage
	{
	    public User User { private get; set; }
        public Location Location { private get; set; }
	    private double AvgStars { get; set; }
        private string startUrl;
	    private string grayStar;
	    private string yellowStar;
	    private int CurrVote { get; set; }

        public DescPage ()
		{
            InitializeComponent ();
		    AvgStars = 0;
		    CurrVote = 0;
		    startUrl = "http://dmax0917.hegr.dk/";
            grayStar = "star-gray.png";
            yellowStar = "star.png";

            //StarURL = @"img\stjerne.png";
		    votingStar1.GestureRecognizers.Add(ReturnCall(1));
		    votingStar2.GestureRecognizers.Add(ReturnCall(2));
		    votingStar3.GestureRecognizers.Add(ReturnCall(3));
		    votingStar4.GestureRecognizers.Add(ReturnCall(4));
		    votingStar5.GestureRecognizers.Add(ReturnCall(5));
		    

            Content = new StackLayout()
		    {
		        Spacing = 5,
		        Children =
		        {
		            showItAll
                }
		    };
        }
        
	    protected override void OnAppearing()
	    {
            base.OnAppearing();
            picture.Source = ImageSource.FromUri(new Uri(startUrl + "img.png"));
	        locationName.Text = Location.LocationName;
            locationDesc.Text = Location.LocationDescription;
            LoadStars();
	    }

	    private async void LoadStars()
	    {
            IRatingRestService ratingRestService = new RatingRestService();
	        AvgStars = await ratingRestService.GetAverageRating(Location);
	        votingList.ItemsSource = Location.Ratings;
            
            MakeStarYellow(1, star1);
            MakeStarYellow(2, star2);
	        MakeStarYellow(3, star3);
	        MakeStarYellow(4, star4);
	        MakeStarYellow(5, star5);

	        ColorizeRatings(0, 1, votingStar1);
	        ColorizeRatings(0, 2, votingStar2);
	        ColorizeRatings(0, 3, votingStar3);
	        ColorizeRatings(0, 4, votingStar4);
	        ColorizeRatings(0, 5, votingStar5);
        }

	    private void MakeStarYellow(int maxValue, Image image)
	    {
	        if (AvgStars >= maxValue)
	        {
	            image.Source = ImageSource.FromUri(new Uri(startUrl + yellowStar));
	        }
	        else
	        {
	            image.Source = ImageSource.FromUri(new Uri(startUrl + grayStar));
            }
	    }

	    private void ColorizeRatings(int rating, int starNo, Image image)
	    {
	        if (rating == 0)
	        {
                image.Source = ImageSource.FromUri(new Uri(startUrl + grayStar));
            }
	        else if (rating >= starNo)
	        {
	            image.Source = ImageSource.FromUri(new Uri(startUrl + yellowStar));
	        }
        }

	    private TapGestureRecognizer ReturnCall(int votingStarNo)
	    {
	        return new TapGestureRecognizer
	        {
	            Command = new Command(() => { SetLocalVote(votingStarNo); }),
	            NumberOfTapsRequired = 1
	        };
	    }

	    private void SetLocalVote(int votingStarNo)
	    {
	        CurrVote = votingStarNo;
	        ColorizeRatings(votingStarNo, 1, votingStar1);
	        ColorizeRatings(votingStarNo, 2, votingStar2);
	        ColorizeRatings(votingStarNo, 3, votingStar3);
	        ColorizeRatings(votingStarNo, 4, votingStar4);
	        ColorizeRatings(votingStarNo, 5, votingStar5);
	    }

        private void SendVote(object sender, EventArgs eventArgs)
	    {
	        IRatingRestService ratingRestService = new RatingRestService();
	        Rating rating = new Rating
	        {
	            Comment = ratingComment.Text,
	            User = User,
	            Rate = CurrVote,
                LocationId = Location.LocationId
	        };
	        ratingRestService.Create(rating);
	        LoadStars();
	    }
    }
}