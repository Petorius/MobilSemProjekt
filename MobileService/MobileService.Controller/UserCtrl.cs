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
        private DbUser _dbUser;

        public UserCtrl()
        {
            _dbUser = new DbUser();
        }
        /// <summary>
        /// creates a user in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>int</returns>
        public int CreateUser(User user)
        {
            return _dbUser.Create(user);
        }
        /// <summary>
        /// compares two hashes
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userHash"></param>
        /// <returns>bool</returns>
        public bool CompareHashes(string userName, string userHash)
        {   
            string dbHash = FindHashByUserName(userName);
            if (!dbHash.Equals(userHash))
            {
                throw new FaultException<UserOrPasswordException>(new UserOrPasswordException());
            }
            return true;
        }
        /// <summary>
        /// finds a user by its name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>user</returns>
        public User FindByUserName(string userName)
        {
            return _dbUser.FindUserByUserName(userName, false);
        }

        /// <summary>
        /// finds a user by its name for login purpose
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private User FindUserForLogin(string userName)
        {
            return _dbUser.FindUserByUserName(userName, true);
        }
        /// <summary>
        /// gets a hash from a user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>string</returns>
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
        /// <summary>
        /// gets a salt from a user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>string</returns>
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
        /// <summary>
        /// updates a user
        /// </summary>
        /// <param name="loggedInUser"></param>
        /// <param name="userToUpdate"></param>
        /// <returns>bool</returns>
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

