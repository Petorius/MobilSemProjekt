using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Exception;
using MobileService.Model;

namespace MobileService.UnitTest
{
    [TestClass]
    public class TagTest
    {
        [TestMethod]
        public void CreateTagTest()
        {
            DbTag dbTag = new DbTag();

            try
            {
                Tag tag = new Tag
                {
                    TagName = "Tag"
                };

                int tagId = dbTag.Create(tag);
                Assert.IsTrue(tagId > 0);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ReadTagTest()
        {
            DbTag dbTag = new DbTag();
            Tag tag = null;

            try
            {
                tag = dbTag.FindById(1);
                Assert.IsNotNull(tag);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateTagTest()
        {
            DbTag dbTag = new DbTag();
            Tag tag = null;

            try
            {
                tag = dbTag.FindById(1);
                bool status = dbTag.Update(tag);
                Assert.IsTrue(status);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }
    }
}
