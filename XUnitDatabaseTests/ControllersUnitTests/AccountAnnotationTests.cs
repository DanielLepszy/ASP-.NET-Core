using ExamPlatform.Controllers;
using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using System.ComponentModel.DataAnnotations;

namespace XUnitDatabaseTests.ControllersUnitTests
{
    public class AccountAnnotationTests
    {
    
        public FakeAccountRepository FA_Repository;
        public List<Accounts> NewAccount;

        private void Arrange()
        {
            
            FA_Repository = new FakeAccountRepository();
            NewAccount = FA_Repository.FakeAccounts;
        }


        [Fact]
        public void PrimaryAccountDataTest_1()
        {
            //Arrange
            Arrange();

            //Set
            var validationContext = new ValidationContext(NewAccount[0], null, null);
            var validationResults = new List<ValidationResult>();
            var validationException = new List<ValidationException>();

            try
            {
                Validator.TryValidateObject(NewAccount[0], validationContext, validationResults, true);

            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert
            Assert.Empty(validationResults);
        }
        [Fact]
        public void PrimaryAccountDataTest_2()
        {
            //Arrange
            Arrange();

            //Set
            var validationContext = new ValidationContext(NewAccount[1], null, null);
            var validationResults = new List<ValidationResult>();
            var validationException = new List<ValidationException>();

            try
            {
                Validator.TryValidateObject(NewAccount[1], validationContext, validationResults, true);

            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert
            Assert.Single(validationResults);
        }
        [Fact]
        public void PrimaryAccountDataTest_3()
        {
            //Arrange
            Arrange();

            //Set
            var validationContext = new ValidationContext(NewAccount[2], null, null);
            var validationResults = new List<ValidationResult>();
            var validationException = new List<ValidationException>();

            try
            {
                Validator.TryValidateObject(NewAccount[2], validationContext, validationResults, true);

            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert
            Assert.Single(validationResults);
        }
        [Fact]
        public void PrimaryAccountDataTest_4()
        {
            //Arrange
            Arrange();

            //Set
            var validationContext = new ValidationContext(NewAccount[3], null, null);
            var validationResults = new List<ValidationResult>();
            var validationException = new List<ValidationException>();

            try
            {
                Validator.TryValidateObject(NewAccount[3], validationContext, validationResults, true);

            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Assert
            Assert.Equal(2, validationResults.Count);
        }
        [Fact]
        public void SimpleTest()
        {
            string x = "0.5";
            string v = x.Replace(".",",");
            double y= double.Parse(v);
            Assert.Equal(0.5, y);

        }
    }
}

