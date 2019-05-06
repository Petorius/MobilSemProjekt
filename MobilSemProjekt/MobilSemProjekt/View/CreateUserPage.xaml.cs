using System;
using System.Diagnostics;
using System.Text;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.Service;
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

                User user = new User();
                user.UserName = CreateUserNameEntry.Text;
                user.Salt = passwordController.GenerateSalt();
                user.HashPassword = passwordController.GenerateHashedPassword(CreatePasswordEntry.Text, Encoding.ASCII.GetBytes(user.Salt));
                user.UserType = new UserType
                {
                    TypeName = "personal"
                };


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