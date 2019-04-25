using System;
using System.Collections.Generic;
using System.Text;
using ExamPlatformDataModel;
using Xunit;
using XUnitDatabaseTests.ControllersUnitTests.FRepositories.IRepositories;
using System.Linq;
using XUnitDatabaseTests.ControllersUnitTests.FRepositories;
using MoreLinq;
using Microsoft.AspNetCore.Http;
using ExamPlatform.Data;

namespace XUnitDatabaseTests.ControllersUnitTests
{
    public class DrawLotsQuestions : TestBase, IDrawLotsQuestions

    {
       //Methods used in UnitTests
        public List<ClosedQuestions> SelectClosedQuestions(List<ClosedQuestions> AllQuestionsFromDB, int AmountOfSelecetedQuestions)
        {
            try { 
            Random randomIndex = new Random();
            List<ClosedQuestions> selectedQuestions = new List<ClosedQuestions>();

            for (int i = 0; i < AmountOfSelecetedQuestions; i++)
            {
                selectedQuestions.Add(AllQuestionsFromDB[randomIndex.Next(AllQuestionsFromDB.Count)]);
            }

            return selectedQuestions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;    
            }
        }
        public List<OpenedQuestions> SelectOpenedQuestions(List<OpenedQuestions> AllQuestionsFromDB, int AmountOfSelecetedQuestions)
        {
            Random random = new Random();
            List<OpenedQuestions> selectedQuestions = new List<OpenedQuestions>();

            int RandomIndex = random.Next(AllQuestionsFromDB.Count);


            selectedQuestions.Add(AllQuestionsFromDB[RandomIndex]);

            for (int i = 0; i < AmountOfSelecetedQuestions - 1; i++)
            {
                
                while (selectedQuestions.Contains(AllQuestionsFromDB[RandomIndex]))
                {
                    RandomIndex = random.Next(AllQuestionsFromDB.Count);
                }
                selectedQuestions.Add(AllQuestionsFromDB[RandomIndex]);
            }

            return selectedQuestions;
        }
        public List<ExamClosedQuestions> CreateExamQuestions(IList<ClosedQuestions> list)
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
        public List<ExamOpenedQuestions> SetUserAnswers(DatabaseContext context, Dictionary<string, string> QuestionsAndUserAnswersFromExam, int? examID)
        {

            var QuestionsFromDB = (from x in context.ExamOpenedQuestions
                                   where x.ExamsID == 1 && x.OpenedQuestionsID == x.OpenedQuestions.OpenedQuestionsID
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
        public  void SetExamScoreFromClosedQuestionAnswers(List<ClosedQuestions> examCQuestions, List<KeyValuePair<string, string>> ClosedQuestionsAnswer,Exams exam)
        {
            try
            {
                UseSqlite();
                using (var context = GetDBContext())
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
                                    exam.ExamResult.Score++;
                                }
                                else
                                {
                                    x.AnswerPoints = 0;
                                }
                            }

                        }
                    }
                }
            }
            catch(Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
     
        [Fact]
        public void SetExamTest()
        {
            //Arrange

            UseSqlite();
            using (var context = GetDBContext())
            {
               
                    FakeAccountRepository FAccount = new FakeAccountRepository();
                    FakeCourseQuestions courseQuestions = new FakeCourseQuestions();
                    context.Account.Add(FAccount.FakeAccounts.First());
                    context.Course.Add(courseQuestions.Architecture);
                    context.SaveChanges();


                    var CQuestions = (from cq in context.ClosedQuestions
                                      where cq.CourseID == 1
                                      select cq).ToList();
                    CQuestions = CQuestions.DistinctBy(q => q.Question).ToList();


                    var OQuestions = (from cq in context.OpenedQuestions
                                      where cq.CourseID == 1
                                      select cq).ToList();
                    OQuestions = OQuestions.DistinctBy(q => q.Question).ToList();


                    //Act
                    CQuestions = SelectClosedQuestions(CQuestions, 4);
                    OQuestions = SelectOpenedQuestions(OQuestions, 4);

                    var ExamCQuestions = CreateExamClosedQuestions(CQuestions.ToList());
                    var ExamOQuestions = CreateExamOpenedQuestions(OQuestions.ToList());

                    FakeExam FExam = new FakeExam();
                    FExam.FakeNewExam.ExamClosedQuestions = ExamCQuestions;
                    FExam.FakeNewExam.ExamOpenedQuestions = ExamOQuestions;
                 
                 
                    context.Exams.Add(FExam.FakeNewExam);

                    context.SaveChanges();

                    var exam = (from ex
                                in context.Exams
                                where ex.ExamsID == 1
                                select ex).ToList();
                    var x = exam.GetType();

                    //Assert
                    Assert.Equal("Co to jest Boxing i Unboxing", exam.First().ExamOpenedQuestions.First().OpenedQuestions.Question);
                    
            }
        }
            
        [Fact]
        public void SaveScoreFromClosedQuestionIntoDatabaseTest()
        {
             //Arrange
                UseSqlite();
                using (var context = GetDBContext())
                {

                    FakeAccountRepository FAccount = new FakeAccountRepository();
                    FakeCourseQuestions courseQuestions = new FakeCourseQuestions();

                    context.Account.Add(FAccount.FakeAccounts.First());
                    context.Course.Add(courseQuestions.Architecture);
                    context.SaveChanges();

                    var ClosedQuestionsFromDB = context.ClosedQuestions.DistinctBy(q => q.Question).ToList();
                    var ExamQuestions = CreateExamQuestions(ClosedQuestionsFromDB);

                    Exams exam = new Exams()
                    {
                        AmountClosedQuestions = 7,
                        AmountOpenedQuestions = 7,

                        ExamClosedQuestions = ExamQuestions,
                        CourseID = 1,
                        AccountID = 1,
                        ExamTimeInMinute = 30,
                        DateOfExam = new DateTime(),
                        ExamResult = new Results()

                    };
                    context.Exams.Add(exam);
                    context.SaveChanges();

                
                Dictionary<string, string> ClosedQuestionsAnswer = new Dictionary<string, string>();
                List<KeyValuePair<string, string>> ClosedQuestionsAnswers = new List<KeyValuePair<string, string>>();

             //Act
                foreach (var x in exam.ExamClosedQuestions)
                    {
                        ClosedQuestionsAnswers.Add(new KeyValuePair<string, string>(x.ClosedQuestions.Question, x.ClosedQuestions.ProperAnswer));
                    }

                    var examCQuestions = (from ex
                                          in context.ExamClosedQuestions
                                          where ex.ExamsID == 1
                                          select ex.ClosedQuestions).ToList();

                    SetExamScoreFromClosedQuestionAnswers(examCQuestions, ClosedQuestionsAnswers,exam);

             
                    context.Exams.Update(exam);
                    context.SaveChangesAsync();

            //Assert

                var examScoreFromClosedQuestions = (from ex
                                      in context.ExamClosedQuestions
                                      where ex.ExamsID == 1
                                      select ex.Exam.ExamResult.Score).Single();

                Assert.Equal(4, examScoreFromClosedQuestions);
                }

           
             
        }
       
        [Fact]
        public void SetUserAnswerTest()
        {

            //Arrange
              UseSqlite();
                using (var context = GetDBContext())
                {
                    var openedquestions = (from x in context.ExamOpenedQuestions
                                           where x.ExamsID == 1
                                           select x.OpenedQuestions.Question).ToList();


             //Act
                    List<String> answers = new List<string>()
                    {
                        "Answer1",
                        "Answer2",
                        "Answer3",
                        "Answer4"
                    };

                    Dictionary<String, String> examplesOfUserAnswers = new Dictionary<string, string>();

                    for (int i= 0;  i<openedquestions.Count();i++)
                    {
                        examplesOfUserAnswers.Add(openedquestions[i], answers[i]);
                    }

                     List<ExamOpenedQuestions> UserAnswers =  SetUserAnswers(context, examplesOfUserAnswers, 1);

                    UserAnswers.ForEach(x => context.ExamOpenedQuestions.Update(x));
                    context.SaveChanges();


             //Assert
                    Assert.NotNull(openedquestions);
                }
        

        }
    }
}

