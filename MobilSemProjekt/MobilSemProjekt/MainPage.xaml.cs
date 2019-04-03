using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Plugin.Geolocator;
using Xamarin.Forms.Internals;
using SQLite;
using Xamarin.Essentials;
using System.Linq;


namespace MobilSemProjekt {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();



            Content = new StackLayout() {
                Spacing = 5,
                Children =
                {
                    GGMAP,
                    OurEntry
                }

            };
        }
    }
}
