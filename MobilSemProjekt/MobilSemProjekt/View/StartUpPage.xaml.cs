using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartUpPage : ContentPage
	{
		public StartUpPage ()
		{
			InitializeComponent ();
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

        private void SignInButton_OnClicked(object sender, EventArgs e)
        {
            var uName = UserNameEntry.Text;
            var pWord = PasswordEntry.Text;
            Console.WriteLine("Ib is a Console!");
            PasswordController pCtrl = new PasswordController();
            pCtrl.VerifyLogin(uName, pWord);
        }
    }
}