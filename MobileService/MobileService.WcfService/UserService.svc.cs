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
        //private RatingCtrl _ratingCtrl;

        public int CreateUser(User user)
        {
            _userCtrl = new UserCtrl();
            return _userCtrl.CreateUser(user);
        }

        public bool CompareHashes(User user)
        {
            _userCtrl = new UserCtrl();
            return _userCtrl.CompareHashes(user.UserName, user.HashPassword);
        }

        public string FindSaltByUserName(string userName)
        {
            _userCtrl = new UserCtrl();
            return _userCtrl.FindSaltByUserName(userName);
        }

        public bool UpdateUser(List<User> users)
        {
            User loggedInUser = users[0];
            User userToUpdate = users[1];
            return _userCtrl.UpdateUser(loggedInUser, userToUpdate);
        }

        public User FindByUserName(string userName)
        {
            _userCtrl = new UserCtrl();
            return _userCtrl.FindByUserName(userName);
        }
    }
}
