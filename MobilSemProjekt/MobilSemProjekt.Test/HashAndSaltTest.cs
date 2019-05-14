using System;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobilSemProjekt.MVVM.ViewModel;

namespace MobilSemProjekt.Test
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
        [TestMethod]
        public void HashingExpectTrue()
        {
            try
            {
                PasswordController passwordController = new PasswordController();
                string pass = "Test";
                string salt =
                    "ThN0qWHV8YV4nOI/kGwjNWRpw16kBt9enVuJaDQFFCRBxQNBFE" +
                    "gu6vhHMb/9++njP32Z5fkrYC0K/cre9KKbgw==";
                string expectedOutput =
                    "NPU+/EqR5h0Cs8SnFsJREoAIsmezp/8m4M3NPhLMY/GHz+SPBA0dk" +
                    "7+VmZHlRAUMZzsYJWm1ZvZtr7XEaTwzRr/M5sKPrtvtSQrWKEEprczoC" +
                    "oPTxPbq20OzXzLUgZ5CPVekeC617vYfvsc57vv9Ekj7hmpoLd05EJ9UHItwU" +
                    "zwhuXLlMFoBK8/8WjKXZ09Hm1h8Sh+LUBXUqyEd4/zswOg2QQDcMjg8YiIQumQfk" +
                    "3mZoVz3fFYv8uBHsKE2M0VzH7F7fyR70yP1Oq0VxIXF/qmJbVYn7yAbuNsaQDXRoXwa" +
                    "2LjM/HQKjlAQhqTAzwephHD+x3/FOKAvpJMqKZNcug==";
                byte[] saltByte = Encoding.UTF8.GetBytes(salt);
                string hashOutput = passwordController.GenerateHashedPassword(pass,saltByte);
                Debug.WriteLine(hashOutput);
                Assert.IsTrue(expectedOutput.Equals(hashOutput));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        [TestMethod]
        public void HashingExpectFalse()
        {
            try
            {
                PasswordController passwordController = new PasswordController();
                string pass = "Test";
                string salt =
                    "ThN0qWHV8YV4nOI/kGwjNWRpw16kBt9enVuJaDQFFCRBxQNBFEgu6vhHMb/" +
                    "9++njP32Z5fkrYC0K/cre9KKbgw==";
                string expectedOutput =
                    "Lille peter edderkop kravlede op af muren";
                byte[] saltByte = Encoding.UTF8.GetBytes(salt);
                string hashOutput = passwordController.GenerateHashedPassword(pass, saltByte);
                Debug.WriteLine(hashOutput);
                Assert.IsFalse(expectedOutput.Equals(hashOutput));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
