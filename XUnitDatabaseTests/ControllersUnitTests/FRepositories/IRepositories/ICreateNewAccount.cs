using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitDatabaseTests.ControllersUnitTests.FRepositories.IRepositories
{
    interface ICreateNewAccount
    {
        String HashPassword(String password);
        void checkIfPasswordExist(String passwordToCheck, List<Accounts> passwordList);

    }
}
