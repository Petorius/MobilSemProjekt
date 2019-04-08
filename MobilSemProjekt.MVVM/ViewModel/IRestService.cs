using System.Collections.Generic;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.ViewModel
{
    public interface IRestService
    {
        Task<List<Location>> GetAllDataAsync();
        Task<List<Location>> ReadLocationByTagName(string tagName);
        Task<Location> ReadLocationByName(string name);
        Task Create(Location location);
        Task DeleteLocationAsync(string name);
    }
}