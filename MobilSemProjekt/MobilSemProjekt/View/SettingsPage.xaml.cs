using Xamarin.Forms;
using Xamarin.Forms.Themes;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent();
            if (Application.Current.Properties.ContainsKey("ThemePreference")) {
                if (Application.Current.Properties["ThemePreference"].Equals("Dark"))
                {
                    SettingsPageColorThemeSwitch.IsToggled = true;
                }

                else {
                    SettingsPageColorThemeSwitch.IsToggled = false;
                }
            }
            else {
                SettingsPageColorThemeSwitch.IsToggled = true;
            }
        }
        
        private void SettingsPageColorThemeSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            if (SettingsPageColorThemeSwitch.IsToggled)
            {
                Application.Current.Resources = new DarkThemeResources();
                Application.Current.Properties["ThemePreference"] = "Dark";
            }
            else
            {
                Application.Current.Resources = new LightThemeResources();
                Application.Current.Properties["ThemePreference"] = "Light";

            }
        }
    }
}