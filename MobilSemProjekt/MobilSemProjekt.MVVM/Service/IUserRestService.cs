using System.Collections.Generic;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.Service
{
    public interface IUserRestService
    {
        Task Create(User user);
        Task<User> FindByUserName(string userName);
        Task<bool> CompareHashes(User userToCompare);
        Task<string> FindSaltByUserName(string userName);
    }
}