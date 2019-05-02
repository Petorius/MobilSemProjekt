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
        private readonly DbRating _dbRating;
        public RatingCtrl()
        {
            _dbRating = new DbRating();
        }

        /// <summary>
        /// Create a rating in database
        /// </summary>
        /// <param name="rating">Rating</param>
        /// <returns>int</returns>
        public int CreateRating(Rating rating)
        {
            return _dbRating.Create(rating);
        }

        /// <summary>
        /// Get an average rating of a specific location in database
        /// </summary>
        /// <param name="locationId">int</param>
        /// <returns>double</returns>
        public double GetAverageRating(int locationId)
        {
            return _dbRating.GetAverageRating(locationId);
        }
        
        /// <summary>
        /// Update a rating in database
        /// </summary>
        /// <param name="rating">Rating</param>
        /// <returns>bool</returns>
        public bool Update(Rating rating)
        {
            return _dbRating.Update(rating);
        }

        /// <summary>
        /// Updates a rating based on specific parameters in database
        /// </summary>
        /// <param name="rating">Rating</param>
        /// <returns>bool</returns>
        public bool UserUpdateRating(Rating rating)
        {
            return _dbRating.UserUpdateRating(rating);
        }
        
        /// <summary>
        /// Delete a rating in database
        /// </summary>
        /// <param name="rating">Rating</param>
        /// <returns>bool</returns>
        public bool Delete(Rating rating)
        {
            return _dbRating.Delete(rating);
        }
    }
}
