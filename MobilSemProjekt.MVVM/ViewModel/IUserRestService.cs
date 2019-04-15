using System.Collections.Generic;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.ViewModel
{
    public interface IUserRestService
    {
        Task Create(User user);
        Task<bool> CompareHashes(User userToCompare);
        Task<string> FindSaltByUserName(string userName);
    }
}