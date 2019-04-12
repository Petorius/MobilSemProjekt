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

        private void CreateAccountButton_OnClicked(object sender, EventArgs e)
        {
            if (CreatePasswordEntry.Text.ToString().Equals(CreatePasswordConfirmationEntry.Text.ToString()))
            {
                User user = new User();
                user.UserName = CreateUserNameEntry.Text.ToString();
                PasswordController passwordController = new PasswordController();
                user.Salt = passwordController.GenerateSalt();
                user.HashPassword = passwordController.GenerateHashedPassword(CreatePasswordEntry.Text.ToString(), Encoding.ASCII.GetBytes(user.Salt));
                IUserRestService userRestService = new UserRestService();
                userRestService.Create(user);
                Debug.WriteLine("Hashes and salt be here: " + user.HashPassword + " " + user.Salt);
            }
            else
            {
                Console.WriteLine("Ya got an error, m8");
            }
        }
    }
}