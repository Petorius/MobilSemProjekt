using System;
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

        private void SaveRatingEditsButton_OnClicked(object sender, EventArgs e)
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
    }
}