using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sat.Recruitment.Api.Dtos.Request;
using Sat.Recruitment.Api.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Test.Validators
{
    [TestClass]
    public class UserRequestValidatorTest
    {
        [DataRow("", "", 2, DisplayName = "Email and Phone empty")]
        [TestMethod]
        public void ShouldBeReturnErrorWhenEmailAndPhoneAreEmptyOrNull(string email, string phone, int errorCount)
        {
            //init
            var userValidator = new UserValidator();

            //act
            var result = userValidator.Validate(new UserRequestDTO() { Email = email, Phone = phone, Name = "Richard", Address = "Argentina" });

            //Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(errorCount, result.Errors.Count);
        }

        [DataRow("", "", 2, DisplayName = "Name and Address empty")]
        [TestMethod]
        public void ShouldBeReturnErrorWhenNameAndAddressAreEmptyOrNull(string name, string address, int errorCount)
        {
            //init
            var userValidator = new UserValidator();

            //act
            var result = userValidator.Validate(new UserRequestDTO() { Email = "richard_iorio@gmail.com", Phone = "2216096299", Name = name, Address = address });

            //Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(errorCount, result.Errors.Count);
        }

        [DataRow("oconnor@hotmail", 1, DisplayName = "Email is not valid")]
        [DataRow("richard_iorio@gmail.", 1, DisplayName = "Email is not valid")]
        [DataRow("iorio@iorio", 1, DisplayName = "Email is not valid")]
        [DataRow("++@++", 1, DisplayName = "Email is not valid")]
        [TestMethod]
        public void ShouldBeReturnErrorWheEmailIsNotValid(string email, int errorCount)
        {
            //init
            var userValidator = new UserValidator();

            //act
            var result = userValidator.Validate(new UserRequestDTO() { Email = email, Phone = "2216096299", Name = "Richard", Address = "Argentina" });

            //Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(errorCount, result.Errors.Count);
        }


        [TestMethod]
        public void ShouldBeReturnOk()
        {
            //init
            var userValidator = new UserValidator();

            //act
            var result = userValidator.Validate(new UserRequestDTO() { Email = "richard_iorio@gmail.com", Phone = "2216096299", Name = "Richard", Address = "Argentina" });

            //Assert
            Assert.IsTrue(result.IsValid);
        }
    }
}
