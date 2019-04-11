using System.Collections.Generic;
using MobileService.Controller;
using MobileService.Model;

namespace MobileService.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        private UserCtrl _userCtrl;

        public bool CompareHashes(string userName, string userHash)
        {
            _userCtrl = new UserCtrl();
            return _userCtrl.CompareHashes(userName, userHash);
        }

        public string FindSaltByUserName(string userName)
        {
            _userCtrl = new UserCtrl();
            return _userCtrl.FindSaltByUserName(userName);
        }
    }
}
