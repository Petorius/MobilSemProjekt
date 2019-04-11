using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLCrypto;

namespace MobilSemProjekt.MVVM.ViewModel {
    public class PasswordController {

  
            public static byte[] CreateSalt(int lengthInBytes) {
                return WinRTCrypto.CryptographicBuffer.GenerateRandom(lengthInBytes);
            }


            public string generateSaltedAndHashedPassword(string password)
            {
                byte[] salt = CreateSalt(64);
                int iterations = 5000;
                int keyLengthInBytes = 256;
                byte[] key = NetFxCrypto.DeriveBytes.GetBytes(password, salt, iterations, keyLengthInBytes);
                return Convert.ToBase64String(key);
            }
    }
    }


