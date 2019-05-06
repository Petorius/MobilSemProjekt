using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobilSemProjekt.MVVM.Model;
using PCLCrypto;

namespace MobilSemProjekt.MVVM.ViewModel {
    public class PasswordController {
        /// <summary>
        /// Creates a salt
        /// </summary>
        /// <param name="bytes">int</param>
        /// <returns>byte[]</returns>
        public static byte[] CreateSalt(int bytes) {
            return WinRTCrypto.CryptographicBuffer.GenerateRandom(bytes);
        }
        /// <summary>
        /// Generates a hash based on a password and a salt
        /// </summary>
        /// <param name="password">string</param>
        /// <param name="salt">byte[]</param>
        /// <returns>string</returns>
        public string GenerateHashedPassword(string password, byte[] salt)
        {
            int iterations = 5000;
            int keyLengthInBytes = 256;
            byte[] key = NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);
            return Convert.ToBase64String(key);
        }
        /// <summary>
        /// Generates a random salt with 64 bit and converts it to a string
        /// </summary>
        /// <returns>string</returns>
        public string GenerateSalt()
        {
            return Convert.ToBase64String(CreateSalt(64));
        }
        /// <summary>
        /// Checks if the user exist in database with password
        /// </summary>
        /// <param name="username">string</param>
        /// <param name="password">string</param>
        /// <returns>Task<bool/></returns>
        public async Task<bool> VerifyLogin(string username, string password)
        {
            IUserRestService userRestService = new UserRestService();
            string salt = await userRestService.FindSaltByUserName(username);
            byte[] theSalt = Encoding.UTF8.GetBytes(salt);
            string hashPassword = GenerateHashedPassword(password, theSalt);
            User userToCompare = new User {UserName = username, HashPassword = hashPassword}; 
            bool status = await userRestService.CompareHashes(userToCompare);
            return status;
        }
    }
}


