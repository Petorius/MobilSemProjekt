using MobilSemProjekt.MVVM.Model;
using System;
using MobilSemProjekt.MVVM.Service;
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
        private readonly string StartUrl;
	    private readonly string GrayStar;
	    private readonly string YellowStar;
	    private int CurrVote { get; set; }
        public DescPage ()
		{
            InitializeComponent ();
		    AvgStars = 0;
		    CurrVote = 0;
		    StartUrl = "http://dmax0917.hegr.dk/";
            GrayStar = "star-gray.png";
            YellowStar = "star.png";

            //StarURL = @"img\stjerne.png";
		    VotingStar1.GestureRecognizers.Add(ReturnCall(1));
		    VotingStar2.GestureRecognizers.Add(ReturnCall(2));
		    VotingStar3.GestureRecognizers.Add(ReturnCall(3));
		    VotingStar4.GestureRecognizers.Add(ReturnCall(4));
		    VotingStar5.GestureRecognizers.Add(ReturnCall(5));
		    

            Content = new StackLayout()
		    {
		        Spacing = 5,
		        Children =
		        {
		            showItAll
                }
		    };
        }

	    private void SetImage(Image image, string url)
	    {
	        image.Source = ImageSource.FromUri(new Uri(url));
	    }

	    protected override void OnAppearing()
	    {
            base.OnAppearing();

	        SetImage(Picture, StartUrl + "img.png");
            if (Location.Pictures != null)
	        {
	            if (Location.Pictures.Count != 0)
	            {
                    SetImage(Picture, Location.Pictures[0].Url);
	            }
            }
            
	        LocationName.Text = Location.LocationName;
            LocationDesc.Text = Location.LocationDescription;
            LoadStars();
	    }

	    private async void LoadStars()
	    {
            IRatingRestService ratingRestService = new RatingRestService();
	        AvgStars = await ratingRestService.GetAverageRating(Location);
	        VotingList.ItemsSource = Location.Ratings;
            
            MakeStarYellow(1, Star1);
            MakeStarYellow(2, Star2);
	        MakeStarYellow(3, Star3);
	        MakeStarYellow(4, Star4);
	        MakeStarYellow(5, Star5);

	        ColorizeRatings(0, 1, VotingStar1);
	        ColorizeRatings(0, 2, VotingStar2);
	        ColorizeRatings(0, 3, VotingStar3);
	        ColorizeRatings(0, 4, VotingStar4);
	        ColorizeRatings(0, 5, VotingStar5);
        }

	    private void MakeStarYellow(int starNo, Image image)
        {
            if (AvgStars < starNo)
            {
                image.Source = ImageSource.FromUri(new Uri(StartUrl + GrayStar));
            }
            else
	        {
	            image.Source = ImageSource.FromUri(new Uri(StartUrl + YellowStar));
	        }
	    }

	    private void ColorizeRatings(int rating, int starNo, Image image)
	    {
	        if (rating < starNo)
	        {
                image.Source = ImageSource.FromUri(new Uri(StartUrl + GrayStar));
            }
	        else
	        {
	            image.Source = ImageSource.FromUri(new Uri(StartUrl + YellowStar));
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
	        ColorizeRatings(votingStarNo, 1, VotingStar1);
	        ColorizeRatings(votingStarNo, 2, VotingStar2);
	        ColorizeRatings(votingStarNo, 3, VotingStar3);
	        ColorizeRatings(votingStarNo, 4, VotingStar4);
	        ColorizeRatings(votingStarNo, 5, VotingStar5);
	    }

        private void SendVote(object sender, EventArgs eventArgs)
	    {
	        IRatingRestService ratingRestService = new RatingRestService();
	        Rating rating = new Rating
	        {
	            Comment = RatingComment.Text,
	            User = User,
	            Rate = CurrVote,
                LocationId = Location.LocationId
	        };
	        ratingRestService.Create(rating);
	        LoadStars();
	        CurrVote = 0;
	    }
        /*
	    private void BtnEdit_OnClicked(object sender, EventArgs e)
	    {
	        IRatingRestService ratingRestService = new RatingRestService();
	        Rating rating = (Rating)sender;
            ratingRestService.Update(rating);
	    }

	    private void BtnDelete_OnClicked(object sender, EventArgs e)
	    {
	        IRatingRestService ratingRestService = new RatingRestService();
	        Rating rating = (Rating) sender;
	        ratingRestService.Delete(rating);
	    }*/
	}
}