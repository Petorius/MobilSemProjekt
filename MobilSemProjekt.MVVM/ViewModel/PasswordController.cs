using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLCrypto;

namespace MobilSemProjekt.MVVM.ViewModel {
    public class PasswordController {

  
            public static byte[] CreateSalt(int bytes) {
                return WinRTCrypto.CryptographicBuffer.GenerateRandom(bytes);
            }


            public string GenerateHashedPassword(string password, byte[] salt)
            {
                int iterations = 5000;
                int keyLengthInBytes = 256;
                byte[] key = NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);
                return Convert.ToBase64String(key);
            }

            public string GenerateSalt()
            {
                return Convert.ToBase64String(CreateSalt(64));
            }



    }
}


