using ExamPlatform.Data;
using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using NUnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{

    public class Tests
    {
        [Fact]
        public void Setup()
        {

            var builder = new DbContextOptionsBuilder<DatabaseContextTest>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var AccountData = new List<Accounts> {
            new Accounts
            {

                Name = "Daniel",
                Surname = "Lepszy",
                Email = "daniel.lepszy@gmail.com",
                //SecurityDetails = new AccountSecurity()
            },
            // new Accounts
            //{

            //    Name = "£ukasz",
            //    Surname = "Lepszy",
            //    Email = "lukasz398@gmail.com",
            //    SecurityDetails = new AccountSecurity()
            //},
            // new Accounts
            //{
            //    Name = "Daria",
            //    Surname = "KoperFild",
            //    Email = "Daria.Koper@gmail.com",
            //    SecurityDetails = new AccountSecurity()
            //},

            //};
            //var SecurityData = new List<AccountSecurity> {
            //    new AccountSecurity
            //{
            //    Username = "U195015",
            //    Password ="Daniel_234",
            //    AccountRef =1

            //},
            //    new AccountSecurity
            //{
            //    Username = "U1115",
            //    Password ="£ukasz_398",
            //    AccountRef =2
            //},
            //    new AccountSecurity
            //{
            //    Username = "UD1344",
            //    Password ="Daria_245",
            //    AccountRef =3
            //},
            };

            using (var context = new DatabaseContextTest(builder.Options))
            {
                try
                {
                   // context.Security.AddRange(SecurityData);
                    context.Account.AddRange(AccountData);
                    context.SaveChanges();
                    Assert.Equal(1, context.Account.Count(c => c.Name == "Daniel"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //dodac tylko jeden obiekt i zoabczyc sie stworzy
                
            }


           

            //var queryAccountData =
            //    from ac in context.Account
            //    where
        }

    
    }
}