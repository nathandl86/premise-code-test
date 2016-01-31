using DutyHours.Code;
using DutyHours.Models;
using DutyHours.Models.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DutyHours.Tests.Web
{
    [TestClass]
    public class HttpAssertTests
    {
        [TestMethod]
        public void Success_Valid()
        {
            //Arrange
            var response = new ResponseModel<bool> { HasError = false };

            //Act
            HttpAssert.Success(response);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Success_InValid()
        {
            //Arrange
            var dalResp = new ResponseModel<bool>
            {
                HasError = true,
                Error = new NullReferenceException()
            };

            //Act
            HttpAssert.Success(dalResp);
        }

        [TestMethod]
        public void NotNull_Valid()
        {
            //Arrange
            var dalResp = new ResponseModel<User>
            {
                HasError = false,
                Result = new User()
            };

            //Act
            HttpAssert.NotNull(dalResp, "");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void NotNull_Invalid()
        {
            //Arrange
            var dalResp = new ResponseModel<User>
            {
                Result = null,
                HasError = false
            };

            //Act
            HttpAssert.NotNull(dalResp, "");
        }
    }
}
