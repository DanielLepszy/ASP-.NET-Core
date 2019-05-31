using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using ExamPlatform.Data;
using ExamPlatform.Logger;
using ExamPlatformDataModel;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamPlatform.Controllers
{
   
    public class AccountsController : Controller
    {
        ILog logger = SingletonFirst.Instance.GetLogger();
        
        /// <summary>Hashes the password of accounts.</summary>
        /// <param name="pass">The pass.</param>
        /// <returns></returns>
         public String HashPass(String pass)
         {
            using (MD5 md5Hash = MD5.Create())
            {

                try
                {
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(pass));
                    StringBuilder sBuilder = new StringBuilder();

                    //Loop through each byte of the hashed data
                    //and format each one as a hexadecimal string.
                    for (int i = 0; i < data.Length; i++)
                    {
                        sBuilder.Append(data[i].ToString("x2"));
                    }
                    return sBuilder.ToString();
                }
                catch(Exception ex)
                {
                    logger.Error(ex.Message);
                    return "";
                }
                
            }
        }

        [Route("SignUp")]
        [HttpGet]
        public IActionResult UserSignUp()
        {
            Accounts account = new Accounts()
            {

            };
            return View(account);
        }

        /// <summary>Checks the login details.
        /// If username and password exists in database, user will sign up to application. Otherwise, user will not able to log in</summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        [Route("SignUp")]
        [HttpPost]
        public IActionResult CheckLoginDetails(Accounts account)
        {
            
            try {
                account.Password = HashPass(account.Password);
                using (var context = new ExamPlatformDbContext())
                {

                

                    var selectedAccount = (from accounts
                                            in context.Account
                                           where accounts.Username == account.Username && accounts.Password == account.Password
                                           select accounts).Single();

                    int amountOfUncheckedExams = (from exams in context.Exam
                                                  where exams.ExamResult.Grade == null
                                                  select exams).ToList().Count();

                   
                    HttpContext.Session.SetInt32("UserID", selectedAccount.AccountsID);

                    if (selectedAccount.Status == "STUDENT")
                    {
                        return View("LogIn", selectedAccount);
                    }
                    else
                    {
                        return View("AdminLogIn", amountOfUncheckedExams);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("AccountsController - CheckLoginDetails. " + ex.Message);
                return View("LoginError");
            }
        }

        /// <summary>Logouts this instance of account. It clean account ID from session</summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return View("Logout");
            }
            catch(Exception ex)
            {
                logger.Error("AccountsController - Logout " + ex.Message);
                return View();
            }
        }
        /// <summary>Navigates to main page, depends on type of user Admin/Student</summary>
        /// <returns></returns>
        public IActionResult NavigateToMainPage()
        {
            var accountID = HttpContext.Session.GetInt32("UserID");
            using (var context = new ExamPlatformDbContext())
            {
                if(accountID !=null)
                {
                    var SelectedAccount = (from account
                                                in context.Account
                                                where account.AccountsID == accountID
                                                select account).First();

                    int amountOfUncheckedExams = (from exams in context.Exam
                                                  where exams.ExamResult.Grade == null
                                                  select exams).ToList().Count();

                    if (SelectedAccount.Status=="STUDENT")
                    {
                        return View("LogIn", SelectedAccount);
                    }
                    else if(SelectedAccount.Status == "ADMIN")
                    {
                        return View("AdminLogIn", amountOfUncheckedExams);
                    }
                }
                    return View("UserSignUp");
            }

                    
        }


        /// <summary>Creates the account for student.
        /// This type of account containe propertise 'Status = "Student"</summary>
        /// <returns></returns>
        [Route("CreateAccount")]
        [HttpGet]
            public IActionResult CreateAccountForStudent()
            {
             try
                {
                    Accounts NewAccount = new Accounts() { Status = "STUDENT" };
                    return View(NewAccount);
                }
             catch (Exception ex)
                {
                    logger.Error("AccountsController - CreateAccountForStudent " + ex.Message);
                    return View();
                }
            }

        /// <summary>  Check if username is not duplicated. If not, it's create new student account with new properties. Otherwise send feedback that someone use the same username</summary>
        /// <param name="NewAccount">The new account.</param>
        /// <returns></returns>
        [Route("Account")]
        [HttpPost]
        public IActionResult SetNewAccountIntoDatabase(Accounts NewAccount)
        {
            try
            {
                bool ifNewUsernameExistInDB(String usernameFromUserLoginToCheck, List<Accounts> accountsListFromDB)
                {
                    foreach (Accounts account in accountsListFromDB)
                    {
                       
                    if (account.Username == usernameFromUserLoginToCheck)
                    {
                        return true;
                        throw new Exception("The username is occupied");
                        
                    }}
                    return false;
                }
           
                string newUsername = NewAccount.Username;
            using (var context = new ExamPlatformDbContext())
            {

                var accountsList = (from securityAccount
                                    in context.Account
                                    select securityAccount).ToList();

                if (!ifNewUsernameExistInDB(newUsername, accountsList))
                {
                    NewAccount.Username = newUsername;
                    NewAccount.Password = HashPass(NewAccount.Password);
                    context.Account.Add(NewAccount);
                    context.SaveChanges();
                    HttpContext.Session.SetInt32("UserID", NewAccount.AccountsID);

                    return View("LogIn", NewAccount);
                }
                else
                {
                    return View("RegisterError");
                }
            }
            }
            catch (Exception ex)
            {
                logger.Error("AccountsController - SetNewAccountIntoDatabase " + ex.Message);
                return View();
            }
        }
    }
}
