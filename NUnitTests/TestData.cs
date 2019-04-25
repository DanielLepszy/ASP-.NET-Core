using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests
{
    public class TestData
    {
        #region Add Authors
        List<Accounts> accounts = new List<Accounts>
        {
            new Accounts()
            {
                Name = "Daniel",
                Surname = "Lepszy",
                Email = "dan.l@gmail.com"
            },
               new Accounts()
            {
                Name = "AAA",
                Surname = "AAA",
                Email = "aa.l@gmail.com"
            },
               new Accounts()
            {
                Name = "BBB",
                Surname = "BBB",
                Email = "bb.l@gmail.com"
            },
        };
        #endregion
        #region Add SecurityData
        List<AccountSecurity> security = new List<AccountSecurity>
        {
            new AccountSecurity()
            {
                Username = "UAAA",
                Password="HAAA"
            }
        };
        #endregion
    }
}