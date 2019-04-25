using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using ExamPlatform.Data;
using ExamPlatform.Models;
using ExamPlatformDataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamPlatform.Controllers
{

    public class StudentExamsController : Controller
    {
        /// <summary>Shows the student exams to check. This exams does not contain grade.</summary>
        /// <returns></returns>
        [Route("StudentsExams")]
        [HttpGet]
        public  ActionResult ShowStudentExamsToCheck()
        {
            try
            {
                using (var context = new ExamPlatformDbContext())
                {
              

                    var data = context.Exam
                         .Include(e => e.ExamResult)
                         .Include(e => e.ExamOpenedQuestions)
                         .ThenInclude(e => e.OpenedQuestions)
                         .Include(e => e.ExamClosedQuestions)
                         .Include(a => a.Account)
                         .Where(e => e.ExamResult.Grade == null)
                         .Select(z => z).ToList();

                    List<SingleStudentExamModel> singleModelList = new List<SingleStudentExamModel>();
                    List<StudentExamsModel> modelList = new List<StudentExamsModel>();

                    foreach (var StudentAccount in data)
                    {
                        singleModelList.Clear();
                        foreach (var ExamOpenQuestions in StudentAccount.ExamOpenedQuestions)
                        {
                            singleModelList.Add(
                                new SingleStudentExamModel
                          (
                          ExamOpenQuestions.ExamOpenedQuestionsID,
                          ExamOpenQuestions.OpenedQuestions.Question,
                          ExamOpenQuestions.UserAnswer,
                          ExamOpenQuestions.OpenedQuestions.MaxPoints
                          )
                            );
                        }

                        modelList.Add(
                            new StudentExamsModel
                   (
                        StudentAccount.ExamsID,
                        StudentAccount.Account.AccountsID,
                        StudentAccount.Account.Name,
                        StudentAccount.Account.Surname,
                        new List<SingleStudentExamModel>(singleModelList)
                   ));
                    }
                    return View("StudentExamsList", modelList);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        /// <summary>Sets the points for opened questions to each student, next set proper grade depends of gained points.At the end send email with grade to students .
        /// The method get IFormCollection of Opened Questions and gained points for each question</summary>
        /// <param name="AnswerPoint">The answer point.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetPointsForOpenedQuestionsToStudent(IFormCollection AnswerPoint)
        {
            try
            {
                var StudentExamModel = new Dictionary<string, List<string>>();
                var Results = new Dictionary<int, double>();

                foreach (var x in AnswerPoint)
                {
                    List<String> valueList = new List<string>();
                    if (!x.Equals(AnswerPoint.Last()))
                    {
                        foreach (var v in x.Value)
                        {
                            valueList.Add(v);
                        }
                        StudentExamModel.Add(x.Key, valueList);
                    }
                }
                List<int> ExamOpenedQuestionIDList = new List<int>();
                List<double> ResultsList = new List<double>();
                List<int> ExamsID = new List<int>();

                StudentExamModel.ElementAt(1).Value.ForEach(x => ExamOpenedQuestionIDList.Add(Int32.Parse(x)));

                StudentExamModel.ElementAt(4).Value.ForEach(
                    x => ResultsList.Add(double.Parse(x.Replace(".", ","))));

                //foreach (var x in StudentExamModel.ElementAt(4).Value)
                //{
                   
                //    ResultsList.Add(double.Parse(x.Replace(".", ",")));
                //}
                StudentExamModel.ElementAt(0).Value.ForEach(x => ExamsID.Add(Int32.Parse(x)));
                ExamsID = ExamsID.Distinct().ToList();

                for (int i = 0; i < ResultsList.Count(); i++)
                {

                    Results.Add(ExamOpenedQuestionIDList[i], ResultsList[i]);
                }

                using (var context = new ExamPlatformDbContext())
                {
                    List<ExamOpenedQuestions> ExamOQuestionList = new List<ExamOpenedQuestions>();

                    foreach (var x in Results)
                    {
                        var ExamOpenedQuestionModel = (from z in context.ExamOpenedQuestions
                                                       where z.ExamOpenedQuestionsID == x.Key
                                                       select z).Single();

                        ExamOpenedQuestionModel.AnswerPoints = x.Value;
                        ExamOQuestionList.Add(ExamOpenedQuestionModel);

                    }
                    context.ExamOpenedQuestions.UpdateRange(ExamOQuestionList);
                    context.SaveChanges();

                    foreach (var examID in ExamsID)
                    {

                        var score = (from x in context.ExamOpenedQuestions
                                     where x.ExamsID == examID
                                     select x.AnswerPoints).ToList();

                        var result = (from r in context.Results
                                      where r.ExamID == examID
                                      select r).Single();

                        score.ForEach(x => result.Score += x.Value);
                        result.Grade = SetExamGrade(result.Score, result.MaxExamPoints);

                        context.Results.Update(result);
                        context.SaveChanges();
                    }

                    SendEmail(ExamsID, context);
                }
                return RedirectToAction("ShowStudentExamsToCheck");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        /// <summary>Sets the exam grade depends on purchased scores.</summary>
        /// <param name="score">The score.</param>
        /// <param name="MaxExamPoints">The maximum exam points.</param>
        /// <returns></returns>
        public double SetExamGrade(double score, double MaxExamPoints)
        {
            try
            {
                double percent = (score * 100) / MaxExamPoints;
                double grade;
                percent = Math.Round(percent);

                if (percent < 50)
                {
                    grade = 2.0;
                }
                else if (percent >= 50 && percent < 70)
                {
                    grade = 3.0;
                }
                else if (percent >= 70 && percent < 85)
                {
                    grade = 4.0;
                }
                else
                {
                    grade = 5.0;
                }
                return grade;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        /// <summary>Sends the email with informations associated with results of exams for each students.</summary>
        /// <param name="ExamIDList">The exam identifier list.</param>
        /// <param name="context">The context.</param>
        public void SendEmail(List<int>ExamIDList, ExamPlatformDbContext context)
        {
            try
            {
                List<UserEmailInfoModel> UserEmailList = new List<UserEmailInfoModel>();

                foreach (var i in ExamIDList)
                {
                    var UserEmail = (from ex in context.Exam
                                     where ex.ExamsID == i
                                     join results in context.Results on ex.ExamResult.ExamID equals results.ExamID
                                     join account in context.Account on ex.AccountID equals account.AccountsID
                                     select new { account.Name, account.Surname, account.Email, ex.Course.CourseType, results.Grade, ex.DateOfExam }).Single();

                   
                    UserEmailInfoModel model = new UserEmailInfoModel
                        (UserEmail.Name,
                        UserEmail.Surname,
                        UserEmail.Email,
                        UserEmail.CourseType,
                        UserEmail.Grade,
                        UserEmail.DateOfExam);

                    UserEmailList.Add(model);
                }
                foreach (var user in UserEmailList)
                {

                    SmtpClient client = new SmtpClient("smtp.gmail.com");
                    client.EnableSsl = true;
                    client.Port = 587;
                    //client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("EMAIL NADAWCY","HASŁO");
                
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("EMAIL NADAWCY");
                    mailMessage.To.Add(user.Email);
                    mailMessage.Body = "Cześć " + user.Name + " " + user.Surname + ". Otrzymałeś ocenę: " + user.Grade + " z egzaminu '" + user.Course + "' odbytego dnia " + user.ExamDate;
                    mailMessage.Subject = "Wyniki egzaminu z '" + user.Course + "'.";
                  
                    client.Send(mailMessage);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>Shows all checked exams.</summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowAllCheckedExams()
        {
            try
            {
                using (var context = new ExamPlatformDbContext())
                {
                    var UserEmail = (from ex in context.Exam
                                     where ex.ExamResult.Grade != null
                                     join results in context.Results on ex.ExamResult.ExamID equals results.ExamID
                                     join account in context.Account on ex.AccountID equals account.AccountsID
                                     select new { account.Name, account.Surname, account.Email, ex.Course.CourseType, results.Grade, ex.DateOfExam }).ToList();

                    List<UserEmailInfoModel> modelList = new List<UserEmailInfoModel>();

                    UserEmail.ForEach(x =>
                     modelList.Add(new UserEmailInfoModel
                     (
                        x.Name,
                        x.Surname,
                        x.Email,
                        x.CourseType,
                        x.Grade,
                        x.DateOfExam
                      ))
                    );
                   return View("ExamsResults", modelList);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }
    }
    
}

