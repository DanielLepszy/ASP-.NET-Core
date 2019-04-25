using ExamPlatformDataModel;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;
using XUnitDatabaseTests.ControllersUnitTests.FRepositories.IRepositories;

namespace XUnitDatabaseTests.ControllersUnitTests
{
    public class CreatUserAccountTests : TestBase, ICreateNewAccount
    {
        
        public string HashPassword(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
        public void checkIfPasswordExist(String passwordToCheck, List<Accounts> passwordList)
        {
            foreach (Accounts x in passwordList)
            {
                passwordToCheck = HashPassword(passwordToCheck);
                if (x.Password == passwordToCheck)
                {
                    throw new Exception("The password is occupied");
                }
            }
        }

        [Fact]
        public void HashingPasswordTest()
        {
            string source = "Hello World!";
            string encodeSource = HashPassword(source);
            Assert.NotEqual(source, encodeSource);
        }
        [Fact]
        public void CreateAccountTest()
        {
           
            UseSqlite();
            using (var context = GetDBContext())
            {
                //Arrange
                Accounts NewAccount = new Accounts()
                {
                    Name = "Daniel",
                    Surname = "Lepszy",
                    Email = "daniel.lepszy@gmail.com",
                    Username = "U12345",
                    Password = "Daniel_12345"

                };
                var AccountPassword = (from securityAccount
                                        in context.Account
                                       select securityAccount).ToList();


                checkIfPasswordExist(NewAccount.Password, AccountPassword);


                NewAccount.Password = HashPassword(NewAccount.Password);
                context.Account.Add(NewAccount);
                context.SaveChanges();


                var AccountFromDB = (from account
                                    in context.Account
                                     where account.AccountsID == 1
                                     select account).ToList();
                //Assert
                Assert.NotEqual("Daniel_349", AccountFromDB[0].Password);
                context.RemoveRange(AccountFromDB);
                context.SaveChanges();
            }
        }
    }
}

