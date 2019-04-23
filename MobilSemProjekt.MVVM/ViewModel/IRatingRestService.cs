using System.Collections.Generic;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.ViewModel
{
    public interface IRatingRestService
    {
        Task Create(Rating rating);
        Task<double> GetAverageRating(Location location);
        
        Task<bool> Update(User user, Rating rating);
    }
}