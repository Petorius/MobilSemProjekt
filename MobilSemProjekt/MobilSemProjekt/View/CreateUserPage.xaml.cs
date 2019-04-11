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
                user.HashPassword = passwordController.generateSaltedAndHashedPassword(CreatePasswordEntry.Text.ToString());
                Debug.WriteLine("Hashes be here: " + user.HashPassword);
            }
            else
            {
                Console.WriteLine("Ya got an error, m8");
            }
        }
    }
}