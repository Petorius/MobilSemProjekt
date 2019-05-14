using System;
using System.ServiceModel;
using MobilSemProjekt.MVVM.Exception;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.Service;
using MobilSemProjekt.MVVM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartUpPage : ContentPage
	{
        private TabbedMapMainPage _tabbedMapMainPage;

        public StartUpPage ()
		{
			InitializeComponent();
		    try
		    {
		        _tabbedMapMainPage = new TabbedMapMainPage();
		        ILocationRestService iRestService = new LocationRestService();
		        iRestService.SetTopLocations();
		    }
		    catch (FaultException<Exception> exc)
		    {
		        DisplayAlert("Fejl", exc.Message, "OK");
            }
		}

        private async void ContinueWithoutAccountButton_OnClicked(object sender, EventArgs e)
        {
            _tabbedMapMainPage = new TabbedMapMainPage();
            _tabbedMapMainPage.StartUpWithoutUser();
            await Navigation.PushAsync(_tabbedMapMainPage);
            Navigation.RemovePage(this);
        }

        private async void SignUpButton_OnClicked(object sender, EventArgs e)
        {
            ContentPage createUserPage = new CreateUserPage();
            await Navigation.PushAsync(createUserPage);
        }

        private async void SignInButton_OnClicked(object sender, EventArgs e)
        {
            try
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
                        _tabbedMapMainPage = new TabbedMapMainPage
                        {
                            User = user
                        };
                        _tabbedMapMainPage.StartUpWithUser();
                        await Navigation.PushAsync(_tabbedMapMainPage);
                        Navigation.RemovePage(this);
                    }
                }
            }
            catch (EmptyInputException exc)
            {
                await DisplayAlert("Fejl", exc.ReturnMessage, "OK");
            }
            catch (UserOrPasswordException exc)
            {
                await DisplayAlert("Fejl", exc.ReturnMessage, "OK");
            }
            catch (FaultException<UserNotFoundException> exc)
            {
                await DisplayAlert("Fejl", exc.Message, "OK");
            }
            catch (FaultException<Exception> exc)
            {
                await DisplayAlert("Fejl", exc.Message, "OK");
            }
            catch (Exception exc)
            {
                await DisplayAlert("Fejl", exc.Message, "OK");
            }
        }
    }
}