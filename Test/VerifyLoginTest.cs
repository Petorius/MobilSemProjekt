using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilSemProjekt.MVVM.ViewModel;

namespace Test
{
    [TestClass]
    public class VerifyLoginTest
    {
        [TestMethod]
        public async Task VerifyLoginWithCorrectNameAndCorrectPassword()
        {
            try
            {
                PasswordController passwordController = new PasswordController();
                Assert.IsTrue(await passwordController.VerifyLogin("aksel", "1234"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        [TestMethod]
        public async Task VerifyLoginWithCorrectPasswordButWrongName()
        {
            try
            {
                PasswordController passwordController = new PasswordController();
                Assert.IsFalse(await passwordController.VerifyLogin("wrong", "1234"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        [TestMethod]
        public async Task VerifyLoginWithWrongPasswordAndWrongName()
        {
            try
            {
                PasswordController passwordController = new PasswordController();
                Assert.IsFalse(await passwordController.VerifyLogin("wrong", "Wrong"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        [TestMethod]
        public async Task VerifyLoginWithWrongPasswordAndCorrectName()
        {
            try
            {
                PasswordController passwordController = new PasswordController();
                Assert.IsFalse(await passwordController.VerifyLogin("aksel", "Wrong"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
