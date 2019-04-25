using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobilSemProjekt.View;
using Xamarin.Forms.Themes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobilSemProjekt {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            MainPage = new NavigationPage( new StartUpPage());
            if (Application.Current.Properties.ContainsKey("ThemePreference"))
            {
                if (Application.Current.Properties["ThemePreference"].Equals("Dark"))
                {
                    Resources = new DarkThemeResources();
                }

                else
                {
                    Resources = new LightThemeResources();

                }
            }
            else
            {
             Resources = new DarkThemeResources();
            }
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
