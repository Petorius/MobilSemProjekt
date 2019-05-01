﻿using System;
using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileService.Database;
using MobileService.Exception;
using MobileService.Model;

namespace MobileService.UnitTest
{
    [TestClass]
    public class PictureTest
    {
        [TestMethod]
        public void CreatePictureTest()
        {
            DbPicture dbPicture = new DbPicture();

            try
            {
                Picture picture = new Picture
                {
                    Description = "Some text",
                    PictureName = "MyPicture",
                    Url = "http://AStrangeUrl.dk/picture.jpg"
                };

                dbPicture.Create(picture, 1);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ReadPictureTest()
        {
            DbPicture dbPicture = new DbPicture();
            Picture picture = null;

            try
            {
                picture = dbPicture.FindById(1);
                Assert.IsNotNull(picture);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdatePictureTest()
        {
            DbPicture dbPicture = new DbPicture();
            Picture picture = null;

            try
            {
                picture = dbPicture.FindById(1);
                dbPicture.Update(picture, 1, picture.PictureId);
            }
            catch (FaultException<DbConnectionException> e)
            {
                Console.WriteLine(e);
                Assert.Fail();
            }
        }
    }
}
