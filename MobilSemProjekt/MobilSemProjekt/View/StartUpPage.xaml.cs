using System;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartUpPage : ContentPage
	{
	    private MainPage MapMainPage;

        public StartUpPage ()
		{
			InitializeComponent ();
		    MapMainPage = new MainPage();
		    MapMainPage.User = null;
        }

        private async void ContinueWithoutAccountButton_OnClicked(object sender, EventArgs e)
        {
            MainPage mainPage = new MainPage();
            await Navigation.PushAsync(mainPage);
            Navigation.RemovePage(this);
        }

        private async void SignUpButton_OnClicked(object sender, EventArgs e)
        {
            ContentPage createUserPage = new CreateUserPage();
            await Navigation.PushAsync(createUserPage);
        }

        private async void SignInButton_OnClicked(object sender, EventArgs e)
        {
            var uName = UserNameEntry.Text;
            var pWord = PasswordEntry.Text;
            PasswordController pCtrl = new PasswordController();
            bool status = await pCtrl.VerifyLogin(uName, pWord);
            if (status)
            {
                IUserRestService restService = new UserRestService();
                User user = await restService.FindByUserName(uName);

                if (user != null)
                {
                    MapMainPage.User = user;
                    await Navigation.PushAsync(MapMainPage);
                }
            }
            
        }
    }
}