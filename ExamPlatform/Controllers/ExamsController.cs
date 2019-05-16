using System;
using System.Collections.Generic;
using ExamPlatform.Data;
using ExamPlatform.Models;
using ExamPlatformDataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MoreLinq;
using Castle.DynamicProxy;
using System.Threading.Tasks;

namespace ExamPlatform.Controllers
{
    public class ExamsController : Controller
    {
        /// <summary>  Drawing random closed questions without repetitions.</summary>
        /// <param name="AllQuestionsFromDB">All questions from database.</param>
        /// <param name="AmountOfSelecetedQuestions">The amount of seleceted questions.</param>
        /// <returns></returns>
        public List<ClosedQuestions> SelectClosedQuestions(List<ClosedQuestions> AllQuestionsFromDB, int AmountOfSelecetedQuestions)
        {
            Random random = new Random();
            List<ClosedQuestions> selectedQuestions = new List<ClosedQuestions>();
            
            int RandomIndex = random.Next(AllQuestionsFromDB.Count);
            
      
                selectedQuestions.Add(AllQuestionsFromDB[RandomIndex]);
             
            for(int i=0;i< AmountOfSelecetedQuestions-1; i++)
            {
                while (selectedQuestions.Contains(AllQuestionsFromDB[RandomIndex]))
                {
                    RandomIndex= random.Next(AllQuestionsFromDB.Count);
                }
                selectedQuestions.Add(AllQuestionsFromDB[RandomIndex]);
            }
         
            return selectedQuestions;
        }
        /// <summary>  Drawing the opened questions without repetitions.</summary>
        /// <param name="AllQuestionsFromDB">All questions from database.</param>
        /// <param name="AmountOfSelecetedQuestions">The amount of seleceted questions.</param>
        /// <returns></returns>
        public List<OpenedQuestions> SelectOpenedQuestions(List<OpenedQuestions> AllQuestionsFromDB, int AmountOfSelecetedQuestions)
        {
            Random random = new Random();
            List<OpenedQuestions> selectedQuestions = new List<OpenedQuestions>();

            int RandomIndex = random.Next(AllQuestionsFromDB.Count);

            
            selectedQuestions.Add(AllQuestionsFromDB[RandomIndex]);
            
            for (int i = 0; i < AmountOfSelecetedQuestions-1; i++)
            {
                while (selectedQuestions.Contains(AllQuestionsFromDB[RandomIndex]))
                {
                    RandomIndex = random.Next(AllQuestionsFromDB.Count);
                }
                selectedQuestions.Add(AllQuestionsFromDB[RandomIndex]);
            }

            return selectedQuestions;
        }
        /// <summary>Creates the exam closed questions model depends of received of closed qouestions model .</summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public List<ExamClosedQuestions> CreateExamClosedQuestions(IList<ClosedQuestions> list)
        {
            List<ExamClosedQuestions> ExamQuestions = new List<ExamClosedQuestions>();
            try
            {
                foreach (var x in list)
                {
                    ExamQuestions.Add(
                        new ExamClosedQuestions()
                        {
                            ClosedQuestions = x
                        }
                        );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ExamQuestions;

        }
        /// <summary>Creates the exam opened questions model depends of received of opened qouestions model .</summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        public List<ExamOpenedQuestions> CreateExamOpenedQuestions(IList<OpenedQuestions> list)
        {
            List<ExamOpenedQuestions> ExamQuestions = new List<ExamOpenedQuestions>();
            try
            {
                foreach (var x in list)
                {
                    ExamQuestions.Add(
                        new ExamOpenedQuestions()
                        {
                            OpenedQuestions = x
                        }
                        );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return ExamQuestions;

        }
        /// <summary>Sets the exam score from each closed question answers.</summary>
        /// <param name="examCQuestions">The exam c questions.</param>
        /// <param name="ClosedQuestionsAnswer">The closed questions answer.</param>
        /// <param name="examResult">The exam result.</param>
        /// <returns></returns>
        public Results SetExamScoreFromClosedQuestionAnswers(List<ClosedQuestions> examCQuestions, Dictionary<string, string> ClosedQuestionsAnswer, Results examResult)
        {
            try
            {
                {

                    foreach (var x in examCQuestions)
                    {
                        foreach (var z in ClosedQuestionsAnswer)
                        {
                            if (z.Key == x.Question)
                            {

                                if (z.Value == x.ProperAnswer)
                                {
                                    x.AnswerPoints = 1;
                                   examResult.Score++;
                                }
                                else
                                {
                                    x.AnswerPoints = 0;
                                }
                            }

                        }
                    }
                }
                return examResult;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>Sets the user answers in dictionary&lt;Key,Value&gt;</summary>
        /// <param name="context">The context.</param>
        /// <param name="QuestionsAndUserAnswersFromExam">The questions and user answers from exam.</param>
        /// <param name="examID">The exam identifier.</param>
        /// <returns></returns>
        public List<ExamOpenedQuestions> SetUserAnswers(ExamPlatformDbContext context, Dictionary<string, string> QuestionsAndUserAnswersFromExam, int? examID)
        {
            
                var QuestionsFromDB = (from x in context.ExamOpenedQuestions
                                       where x.ExamsID == examID && x.OpenedQuestionsID == x.OpenedQuestions.OpenedQuestionsID
                                       select new { ExamOpenedQuestionObject = x, OpenedQuestionObject = x.OpenedQuestions })
                                             .ToDictionary(z => z.OpenedQuestionObject, t => t.ExamOpenedQuestionObject);

                foreach (var singleQuestion in QuestionsFromDB)
                {
                    foreach (var questionAndAnswerExam in QuestionsAndUserAnswersFromExam)
                    {
                        if (singleQuestion.Key.Question == questionAndAnswerExam.Key)
                        {
                            singleQuestion.Value.UserAnswer = questionAndAnswerExam.Value;
                        }
                    }
                }
                return QuestionsFromDB.Values.ToList();
        }

        /// <summary>Creates and shows the exam with random questions for single student.</summary>
        /// <param name="courseID">The course identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ShowExam(int courseID)
        {
            var accountID = HttpContext.Session.GetInt32("UserID");
            using (var context = new ExamPlatformDbContext())
            {
                try
                {
                    Accounts SelectedAccount = await context.Account
                                                    .Where(a => a.AccountsID == accountID)
                                                    .Select(a => a).SingleAsync();

                    Course SelectedCourse = await context.Course
                                            .Include(c => c.ClosedQuestionsList)
                                            .Include(q => q.OpenedQuestionsList)
                                            .Where(course => course.CourseID == courseID)
                                            .Select(seleceteCourse => seleceteCourse).SingleAsync();

            

                    var CQuestions = SelectedCourse.ClosedQuestionsList.DistinctBy(q => q.Question).ToList();
                    var OQuestions = SelectedCourse.OpenedQuestionsList.DistinctBy(q => q.Question).ToList();

                    CQuestions = SelectClosedQuestions(CQuestions, 7);
                    OQuestions = SelectOpenedQuestions(OQuestions, 6);

                    var ExamCQuestions = CreateExamClosedQuestions(CQuestions);
                    var ExamOQuestions = CreateExamOpenedQuestions(OQuestions);


                    Exams exam = new Exams()
                    {
                        AmountClosedQuestions = CQuestions.Count(),
                        AmountOpenedQuestions = OQuestions.Count(),
                        ExamTimeInMinute = 30,
                        DateOfExam = DateTime.Now,

                        ExamClosedQuestions = ExamCQuestions,
                        ExamOpenedQuestions = ExamOQuestions,
                        CourseID = SelectedCourse.CourseID,
                        AccountID = SelectedAccount.AccountsID,
                        ExamResult = new Results() { ifResultSent = false }
                        
                    };
                    ExamCQuestions.ForEach(x => exam.ExamResult.MaxExamPoints += 1);
                    ExamOQuestions.ForEach(x => exam.ExamResult.MaxExamPoints += x.OpenedQuestions.MaxPoints);
                    context.Exam.Add(exam);
                    context.SaveChanges();
                    HttpContext.Session.SetInt32("ExamID", exam.ExamsID);

                    return View("ExamClosedQuestions", exam);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View();
                }
            }

        }

        /// <summary>Sets the closed questions answers and save in database. Redirect to controller wchich show closed quesitons view</summary>
        /// <param name="AnswersForm">The answers form.</param>
        /// <returns></returns> 
        [HttpPost]
        public IActionResult SetClosedQuestionsAnswers(IFormCollection AnswersForm)
        {
            try
            {
                var QuestionAnswer = new Dictionary<string, string>();
                foreach (var key in AnswersForm)
                {
                    if (!key.Equals(AnswersForm.Last()))
                    {
                        QuestionAnswer.Add(key.Key, key.Value);
                    }
                }

                var exID = HttpContext.Session.GetInt32("ExamID");
                using (var context = new ExamPlatformDbContext())
                {

                    var examCQuestions = (from ex
                                          in context.ExamClosedQuestions
                                          where ex.ExamsID == exID
                                          select ex.ClosedQuestions).ToList();

                    Results examResults = (from ex
                               in context.Results
                                where ex.ExamID == exID
                                select ex).Single();

                    examResults = SetExamScoreFromClosedQuestionAnswers(examCQuestions, QuestionAnswer, examResults);
                    context.Results.Update(examResults);
                    context.SaveChanges();

                    var examQuestions = context.OpenedQuestion
                                         .Where(q => q.ExamOpenedQuestions.All(x=>x.ExamsID==exID))
                                         .Select(q => q).ToList();

                    return RedirectToAction("ShowOpenedQuestions");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        /// <summary>Shows the opened questions for single student. Redirect to view to inform student of finishing exam</summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowOpenedQuestions()
        {
            try
            {
                var exID = HttpContext.Session.GetInt32("ExamID");
                using (var context = new ExamPlatformDbContext())
                {

                    var openedQuestions = context.ExamOpenedQuestions
                              .Where(q => q.ExamsID == exID)
                              .Select(o => o.OpenedQuestions).ToList();


                    return View("ExamOpenedQuestions", openedQuestions);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }
        }

        /// <summary>Sets the opened questions answers and save in database.</summary>
        /// <param name="AnswersForm">The answers form.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetOpenedQuestionsAnswers(IFormCollection AnswersForm)
        {
            try
            {
                var QuestionUserAnswer = new Dictionary<string, string>();
                foreach (var key in AnswersForm)
                {
                    if (!key.Equals(AnswersForm.Last()))
                    {
                        QuestionUserAnswer.Add(key.Key, key.Value);
                    }
                }
                var exID = HttpContext.Session.GetInt32("ExamID");
                using (var context = new ExamPlatformDbContext())
                {

                    List<ExamOpenedQuestions> UserAnswers = SetUserAnswers(context, QuestionUserAnswer, exID);

                    UserAnswers.ForEach(x => context.ExamOpenedQuestions.Update(x));
                    context.SaveChanges();
                    return View("ExamFinished");
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