using System.Collections.Generic;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.ViewModel
{
    public interface IRestService
    {
        Task<List<Location>> GetAllDataAsync();
        Task<List<Location>> ReadLocationByTagNameAsync(string tagName);
        Task<Location> ReadLocationByNameAsync(string name);
        Task Create(Location location);
        Task<List<Location>> GetLocationsByUserNameAsync(string name);
        Task<List<Location>> GetLocationsByCommentUserName(string username);
        void UpdateHits(Location location);
        void UpdateLocation(Location location);
        void UserUpdateLocation(Location location);
    }
}