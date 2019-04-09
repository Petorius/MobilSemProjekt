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
        public int Update(Rating rating, int id)
        {
            return _dbRating.Update
        }


    }
}
