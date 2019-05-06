using MobilSemProjekt.MVVM.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedMapMainPage : TabbedPage {
        public User User { private get; set; }
        private MainPage MainPage;
        private SettingsPage SettingsPage;
        private ProfilePage ProfilePage;


        public TabbedMapMainPage() {
            InitializeComponent();
            MainPage = new MainPage();
            SettingsPage = new SettingsPage();
            ProfilePage = new ProfilePage();
        }

        private void AddTabs()
        {
            this.Children.Add(MainPage);
            this.Children.Add(SettingsPage);
        }

        public void StartUpWithUser() {
            MainPage.User = User;
            ProfilePage.User = User;
            AddTabs();
            this.Children.Add(ProfilePage);
        }

        public void StartUpWithoutUser() {
            AddTabs();
        }
    }
}