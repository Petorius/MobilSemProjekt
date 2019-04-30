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
        
        public bool Update(Rating rating)
        {
            return _dbRating.Update(rating);
        }

        public bool Delete(Rating rating)
        {
            return _dbRating.Delete(rating);
        }
    }
}
