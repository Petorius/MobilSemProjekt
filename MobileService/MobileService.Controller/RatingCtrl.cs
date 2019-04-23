using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileService.Database;
using MobileService.Model;

namespace MobileService.Controller
{
    public class RatingCtrl
    {
        private DbRating _dbRating;
        public RatingCtrl()
        {
            _dbRating = new DbRating();
        }

        public int CreateRating(Rating rating)
        {
            return _dbRating.Create(rating);
        }

        public double GetAverageRating(int locationId)
        {
            return _dbRating.GetAverageRating(locationId);
        }

        public bool Update(User loggedInUser, Rating ratingToUpdate)
        {
            bool status = false;
            if (loggedInUser.UserType.TypeName == "admin")
            {
                status = _dbRating.Update(ratingToUpdate);
            }
            return status;
        }
    }
}
