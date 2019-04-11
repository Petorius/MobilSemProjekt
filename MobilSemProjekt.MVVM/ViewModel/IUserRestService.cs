using System.Collections.Generic;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;

namespace MobilSemProjekt.MVVM.ViewModel
{
    public interface IUserRestService
    {
        Task<bool> CompareHashes(string userName, string userHash);
        Task<string> FindSaltByUserName(string userName);
    }
}