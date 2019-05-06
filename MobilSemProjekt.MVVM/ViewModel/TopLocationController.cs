﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;
using MobilSemProjekt.MVVM.Service;

namespace MobilSemProjekt.MVVM.ViewModel {
    public class TopLocationController {
        public List<Location> AllLocations { get; private set; }

        /// <summary>
        /// Finds and sets a toplocation
        /// </summary>
        public async void SetTopLocations() {
            int allHits = 0;
            RestService restService = new RestService();
            AllLocations = await restService.GetAllDataAsync();

            foreach (var location in AllLocations) {
                location.Hits += allHits;
            }

            foreach (var location in AllLocations)
            {
                var averageRating = 0;
                foreach (var rating in location.Ratings)
                {
                    rating.Rate += averageRating;
                }

                if (location.Ratings.Count > 0)
                {
                    averageRating = averageRating / location.Ratings.Count();

                }

                if (location.Hits > allHits / 10000 || location.Hits > 1000 && averageRating >= 4.5
                                                    || location.Hits > 10000 && averageRating >= 4 ||
                                                    location.Ratings.Count > 100 && averageRating >= 4.5)
                {
                    location.IsTopLocation = true;
                }
            }
        }
    }
}
