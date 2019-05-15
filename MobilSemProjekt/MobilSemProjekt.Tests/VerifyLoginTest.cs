using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilSemProjekt.MVVM.Exception;
using MobilSemProjekt.MVVM.ViewModel;

namespace MobilSemProjekt.Tests
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
                Assert.IsTrue(await passwordController.VerifyLogin("Aksel", "1234"));
            }
            catch (EmptyInputException exc)
            {
                Console.WriteLine(exc.ReturnMessage);
                Assert.Fail();
            }
            catch (UserOrPasswordException exc)
            {
                Console.WriteLine(exc.ReturnMessage);
                Assert.Fail();
            }
            catch (FaultException<UserOrPasswordException> exc)
            {
                Console.WriteLine(exc.Message);
                Assert.Fail();
            }
            catch (FaultException<UserNotFoundException> exc)
            {
                Console.WriteLine(exc.Message);
                Assert.Fail();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
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
            catch (EmptyInputException exc)
            {
                Console.WriteLine(exc.ReturnMessage);
                Assert.Fail();
            }
            catch (UserOrPasswordException exc)
            {
                Console.WriteLine(exc.ReturnMessage);
                Assert.IsTrue(true);
            }
            catch (FaultException<UserOrPasswordException> exc)
            {
                Console.WriteLine(exc.Message);
                Assert.Fail();
            }
            catch (FaultException<UserNotFoundException> exc)
            {
                Console.WriteLine(exc.Message);
                Assert.Fail();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
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
            catch (EmptyInputException exc)
            {
                Console.WriteLine(exc.ReturnMessage);
                Assert.Fail();
            }
            catch (UserOrPasswordException exc)
            {
                Console.WriteLine(exc.ReturnMessage);
                Assert.IsTrue(true);
            }
            catch (FaultException<UserOrPasswordException> exc)
            {
                Console.WriteLine(exc.Message);
                Assert.Fail();
            }
            catch (FaultException<UserNotFoundException> exc)
            {
                Console.WriteLine(exc.Message);
                Assert.Fail();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
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
            catch (EmptyInputException exc)
            {
                Console.WriteLine(exc.ReturnMessage);
                Assert.Fail();
            }
            catch (UserOrPasswordException exc)
            {
                Console.WriteLine(exc.ReturnMessage);
                Assert.IsTrue(true);
            }
            catch (FaultException<UserOrPasswordException> exc)
            {
                Console.WriteLine(exc.Message);
                Assert.Fail();
            }
            catch (FaultException<UserNotFoundException> exc)
            {
                Console.WriteLine(exc.Message);
                Assert.Fail();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                Assert.Fail();
            }
        }
    }
}
