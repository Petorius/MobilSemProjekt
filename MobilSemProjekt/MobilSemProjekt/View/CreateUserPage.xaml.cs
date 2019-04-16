using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateUserPage : ContentPage
	{
		public CreateUserPage ()
		{
			InitializeComponent ();
		}

        private async void CreateAccountButton_OnClicked(object sender, EventArgs e)
        {
            if (CreatePasswordEntry.Text.Equals(CreatePasswordConfirmationEntry.Text))
            {
                PasswordController passwordController = new PasswordController();
                IUserRestService userRestService = new UserRestService();

                User user = new User
                {
                    UserName = CreateUserNameEntry.Text,
                    Salt = passwordController.GenerateSalt()
                };
                user.HashPassword = passwordController.GenerateHashedPassword(CreatePasswordEntry.Text, Encoding.ASCII.GetBytes(user.Salt));
                
                await userRestService.Create(user);
                Debug.WriteLine("Hashes and salt be here: " + user.HashPassword + " " + user.Salt);
            }
            else
            {
                Console.WriteLine("Ya got an error, m8");
            }
        }
    }
}