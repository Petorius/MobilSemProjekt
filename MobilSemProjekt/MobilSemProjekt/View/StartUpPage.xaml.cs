using System;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.Service;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartUpPage : ContentPage
	{
        private TabbedMapMainPage TabbedMapMainPage;

        public StartUpPage ()
		{
			InitializeComponent();
            TabbedMapMainPage = new TabbedMapMainPage();
            TopLocationController topLocationController = new TopLocationController();
            topLocationController.SetTopLocations();
        }

        private async void ContinueWithoutAccountButton_OnClicked(object sender, EventArgs e)
        {
            TabbedMapMainPage = new TabbedMapMainPage();
            TabbedMapMainPage.StartUpWithoutUser();
            await Navigation.PushAsync(TabbedMapMainPage);
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
                    TabbedMapMainPage = new TabbedMapMainPage
                    {
                        User = user
                    };
                    TabbedMapMainPage.StartUpWithUser();
                    await Navigation.PushAsync(TabbedMapMainPage);
                    Navigation.RemovePage(this);
                }
            }           
        }
    }
}