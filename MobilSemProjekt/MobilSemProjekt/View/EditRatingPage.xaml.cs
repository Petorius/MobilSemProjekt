using System;
using System.ServiceModel;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditRatingPage : ContentPage
	{
        public Rating Rating { get; set; }

		public EditRatingPage ()
		{
			InitializeComponent ();

        }

        public void SetPlaceholders()
        {
            RatingEntry.Placeholder= Rating.Rate.ToString();
            CommentEditor.Placeholder = Rating.Comment;
        }

        private async void SaveRatingEditsButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                bool status = double.TryParse(RatingEntry.Text, out double result);
                if (status)
                {
                    Rating.Rate = result;
                    Rating.Comment = CommentEditor.Text;
                    IRatingRestService restService = new RatingRestService();
                    restService.Update(Rating);
                }
            }
            catch (FaultException<Exception> exc)
            {
                await DisplayAlert("Fejl", exc.Message, "OK");
            }
        }
    }
}