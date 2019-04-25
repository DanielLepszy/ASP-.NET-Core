using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitDatabaseTests.ControllersUnitTests
{
    public class FakeAccountRepository
    {
        public List<Accounts> FakeAccounts = new List<Accounts>()
        {
             new Accounts(){
                Name = "",
                Surname = "",
                Email = "daniel.com",
                Username="s",
                Password ="s"
             },
              new Accounts(){
                Name = "Dan",
                Surname = "Lep",
                Email = "danielgmail.com",
                Username="U1993",
                Password ="349_danieL"
             },
              new Accounts(){
                Name = "Dan ",
                Surname = "Lep ",
                Email = "daniell@@@gmail.com",
                Username="dn1993",
                Password ="Daniel349"
             },
              new Accounts(){
                Name = "ThereIsFullNameWithLengthMoreThanTweenty",
                Surname = "ThereIsFullSurnameWithLengthMoreThanTweenty",
                Email = "Fake@Mail",
                Username="dn1993",
                Password ="Daniel_349"
             },
              new Accounts(){
                Name = "D",
                Surname = "L",
                Email = "Fake@Mail",
                Username="dn1993",
                Password ="Dn"
             },
        };
        }
}
 