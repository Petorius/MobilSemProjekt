using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobilSemProjekt.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedMapMainPage : TabbedPage
    {
        public User User { private get; set; }
        public TabbedMapMainPage ()
        {
            InitializeComponent();

            var tab1 = new MainPage();
            var tab2 = new SettingsPage();
            var tab3 = new ProfilePage();

            tab1.User = User;
            this.Children.Add(tab1);
            this.Children.Add(tab2);
            this.Children.Add(tab3);
        }
    }
}