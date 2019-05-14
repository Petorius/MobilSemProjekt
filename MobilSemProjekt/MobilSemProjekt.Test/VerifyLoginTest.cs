using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilSemProjekt.MVVM.Exception;
using MobilSemProjekt.MVVM.ViewModel;

namespace MobilSemProjekt.Test
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
            catch (EmptyInputException)
            {
                Assert.Fail();
            }
            catch (UserOrPasswordException)
            {
                Assert.Fail();
            }
            catch (FaultException<UserOrPasswordException>)
            {
                Assert.Fail();
            }
            catch (FaultException<UserNotFoundException>)
            {
                Assert.Fail();
            }
            catch (FaultException<Exception>)
            {
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Fail();
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
            catch (EmptyInputException)
            {
                Assert.Fail();
            }
            catch (UserOrPasswordException)
            {
                Assert.Fail();
            }
            catch (FaultException<UserOrPasswordException>)
            {
                Assert.Fail();
            }
            catch (FaultException<UserNotFoundException>)
            {
                Assert.Fail();
            }
            catch (FaultException<Exception>)
            {
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Fail();
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
            catch (EmptyInputException)
            {
                Assert.Fail();
            }
            catch (UserOrPasswordException)
            {
                Assert.Fail();
            }
            catch (FaultException<UserOrPasswordException>)
            {
                Assert.Fail();
            }
            catch (FaultException<UserNotFoundException>)
            {
                Assert.Fail();
            }
            catch (FaultException<Exception>)
            {
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Fail();
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
            catch (EmptyInputException)
            {
                Assert.Fail();
            }
            catch (UserOrPasswordException)
            {
                Assert.Fail();
            }
            catch (FaultException<UserOrPasswordException>)
            {
                Assert.Fail();
            }
            catch (FaultException<UserNotFoundException>)
            {
                Assert.Fail();
            }
            catch (FaultException<Exception>)
            {
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
