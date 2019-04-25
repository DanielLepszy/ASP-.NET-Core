using ExamPlatform.Controllers;
using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace XUnitDatabaseTests.ControllersUnitTests
{
    public class SecurityAccountAnnotationTests
    {

        public FakeAccountRepository FSA_Repository;
        public List<Accounts> SecurityAccount;

        private void Arrange()
        {
            FSA_Repository = new FakeAccountRepository();
            SecurityAccount = FSA_Repository.FakeAccounts;
        }
        [Fact]
        public void SecurityAccountDataTest_1()
        {
            //Arrange
            Arrange();


            //Act
            var validationContext = new ValidationContext(SecurityAccount[0], null, null);
            var validationResults = new List<ValidationResult>();
            var validationException = new List<ValidationException>();

            try
            {
                Validator.TryValidateObject(SecurityAccount[0], validationContext, validationResults, true);

            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert
            Assert.Equal(5,validationResults.Count);
        }
        [Fact]
        public void SecurityAccountDataTest_2()
        {
            //Arrange
            Arrange();

           // Set
            var validationContext = new ValidationContext(SecurityAccount[1], null, null);
            var validationResults = new List<ValidationResult>();
            var validationException = new List<ValidationException>();

            try
            {
                Validator.TryValidateObject(SecurityAccount[1], validationContext, validationResults, true);

            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert
            Assert.Single(validationResults);
        }

        [Fact]
        public void SecurityAccountDataTest_3()
        {
            //Arrange
            Arrange();

            //Set
            var validationContext = new ValidationContext(SecurityAccount[2], null, null);
            var validationResults = new List<ValidationResult>();
            var validationException = new List<ValidationException>();

            try
            {
                Validator.TryValidateObject(SecurityAccount[2], validationContext, validationResults, true);

            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert
            Assert.Single(validationResults);
        }

        [Fact]
        public void SecurityAccountDataTest_4()
        {
            //Arrange
            Arrange();

            //Set
            var validationContext = new ValidationContext(SecurityAccount[3], null, null);
            var validationResults = new List<ValidationResult>();
            var validationException = new List<ValidationException>();

            try
            {
                Validator.TryValidateObject(SecurityAccount[3], validationContext, validationResults, true);
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert
            Assert.Equal(2, validationResults.Count);
        }
        [Fact]
        public void SecurityAccountDataTest_5()
        {
            //Arrange
            Arrange();

            //Set
            var validationContext = new ValidationContext(SecurityAccount[4], null, null);
            var validationResults = new List<ValidationResult>();
            var validationException = new List<ValidationException>();

            try
            {
                Validator.TryValidateObject(SecurityAccount[4], validationContext, validationResults, true);
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert
            Assert.Equal(3, validationResults.Count);
        }
    }
}
    