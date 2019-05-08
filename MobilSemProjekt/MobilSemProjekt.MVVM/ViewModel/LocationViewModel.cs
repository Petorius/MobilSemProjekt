using System.Collections.Generic;
using System.Collections.ObjectModel;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.ViewModel
{
    public class LocationViewModel
    {
        public LocationViewModel(Location location)
        {
            Location = location;
        }

        public Location Location { private set; get; }

        public static void AddParameters(ObservableCollection<Location> locations)
        {
            All = new List<LocationViewModel>();
            foreach (var location in locations)
            {
                LocationViewModel locationViewModel = new LocationViewModel(location);
                All.Add(locationViewModel);
            }
        }

        public static IList<LocationViewModel> All { private set; get; }
    }
}