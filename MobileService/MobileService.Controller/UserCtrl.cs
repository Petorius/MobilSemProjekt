using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using MobileService.Database;
using MobileService.Exception;
using MobileService.Model;

namespace MobileService.Controller
{
    public class UserCtrl
    {
        private DbUser _dbUser = new DbUser();

        public UserCtrl()
        {
            _dbUser = new DbUser();
        }
        public int CreateUser(User user)
        {
            return _dbUser.Create(user);
        }

        public bool CompareHashes(string userName, string userHash)
        {   
            string dbHash = FindHashByUserName(userName);
            if (!dbHash.Equals(userHash))
            {
                throw new FaultException<UserOrPasswordException>(new UserOrPasswordException());
            }
            return true;
        }

        public User FindByUserName(string userName)
        {
            return _dbUser.FindUserByUserName(userName, false);
        }

        private User FindUserForLogin(string userName)
        {
            return _dbUser.FindUserByUserName(userName, true);
        }

        private string FindHashByUserName(string userName)
        {
            User u = FindUserForLogin(userName);
            string val = "";
            if (u != null)
            {
                val = u.HashPassword;
            }
            return val;
        }

        public string FindSaltByUserName(string userName)
        {
            User u = FindUserForLogin(userName);
            string val = "";
            if (u != null)
            {
                val = u.Salt;
            }
            return val;
        }

        public bool UpdateUser(User loggedInUser, User userToUpdate)
        {
            bool status = false;
            if (loggedInUser.UserType.TypeName == "admin")
            {
                status = _dbUser.Update(userToUpdate);
            }
            return status;
        }
    }
}

