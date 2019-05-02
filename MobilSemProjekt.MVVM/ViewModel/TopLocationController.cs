using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.ViewModel {
    public class TopLocationController {
        public List<Location> allLocations { get; private set; }

        /// <summary>
        /// finds and sets a toplocation
        /// </summary>
        public async void SetTopLocations() {
            int allHits = 0;
            RestService restService = new RestService();
            allLocations = await restService.GetAllDataAsync();

            foreach (var location in allLocations) {
                location.Hits += allHits;
            }

            foreach (var location in allLocations) {
                var averageRating = 0;
                foreach (var rating in location.Ratings) {
                    rating.Rate += averageRating;
                }

                if (location.Ratings.Count > 0)
                {
                    averageRating = averageRating / location.Ratings.Count();

                }
                if (location.Hits > allHits / 10000 || location.Hits > 1000 && averageRating >= 4.5
                                                    || location.Hits > 10000 && averageRating >= 4 ||
                                                    location.Ratings.Count > 100 && averageRating >= 4.5) {
                    location.IsTopLocation = true;
                }
            }

        }


    }
}
