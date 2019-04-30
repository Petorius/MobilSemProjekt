using System.Collections.Generic;
using MobileService.Controller;
using MobileService.Model;

namespace MobileService.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class RatingService : IRatingService
    {
        private RatingCtrl _ratingCtrl;

        public int CreateRating(Rating rating)
        {
            _ratingCtrl = new RatingCtrl();
            return _ratingCtrl.CreateRating(rating);
        }
        
        public double GetAverageRating(string locationId)
        {
            _ratingCtrl = new RatingCtrl();
            int.TryParse(locationId, out int locId);
            return _ratingCtrl.GetAverageRating(locId);
        }

        public bool UpdateRating(Rating rating)
        {
            _ratingCtrl = new RatingCtrl();
            return _ratingCtrl.Update(rating);
        }

        public bool DeleteRating(Rating rating)
        {
            _ratingCtrl = new RatingCtrl();
            return _ratingCtrl.Delete(rating);
        }
    }
}
