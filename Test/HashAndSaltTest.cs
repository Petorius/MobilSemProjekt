using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilSemProjekt.MVVM.ViewModel;
using PCLCrypto;

namespace Test
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void SaltConsistencyTest()
        {
            string salty1 = "";
            string salty2 = "";

            try
            {
                PasswordController passwordController = new PasswordController();
                salty1 = passwordController.GenerateSalt();
                salty2 = passwordController.GenerateSalt();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Debug.WriteLine("saltyfish: " + salty1 + ", " + salty2);
            Assert.IsFalse(salty1.Equals(salty2));
        }
    }
}
