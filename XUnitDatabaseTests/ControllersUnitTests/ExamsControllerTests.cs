using ExamPlatform.Controllers;
using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using XUnitDatabaseTests.ControllersUnitTests.FRepositories;

namespace XUnitDatabaseTests.ControllersUnitTests
{
    public class ExamsControllerTests : TestBase
    {
        MockDatabase mockMethod = new MockDatabase();
        [Fact]
        public void SelectClosedQuestionsTest()
        {
            //Arrange
            mockMethod.mockDataIntoDB();
            ExamsController ExamsController = new ExamsController();
            int AmountOfSelecetedQuestions = 4;
            int courseID = 1;


            UseSqlite();
            using (var context = GetDBContext())
            {

                Course SelectedCourse = context.Course
                                            .Include(c => c.ClosedQuestionsList)
                                            .Include(q => q.OpenedQuestionsList)
                                            .Where(course => course.CourseID == courseID)
                                            .Select(seleceteCourse => seleceteCourse).Single();

                //Act
                var CQuestions = SelectedCourse.ClosedQuestionsList.DistinctBy(q => q.Question).ToList();
                CQuestions = ExamsController.SelectClosedQuestions(CQuestions, AmountOfSelecetedQuestions);

                //Assert
                Assert.Equal(4, CQuestions.Count());

            }

        }

        [Fact]
        public void SetExamScoreFromClosedQuestionProperAnswersTest()
        {
            //Arrange
            mockMethod.mockDataIntoDB();
            mockMethod.mockExamDataIntoDB();
            ExamsController ExamsController = new ExamsController();
            List<ClosedQuestions> examCQuestions = new List<ClosedQuestions>();
            var QuestionAnswer = new Dictionary<string, string>();
            Results examResults;
            int examID = 1;

            //Act
            UseSqlite();
            using (var context = GetDBContext())
            { 
                 examCQuestions = (from ex
                                in context.ExamClosedQuestions
                                where ex.ExamsID == examID
                                select ex.ClosedQuestions).ToList();

                examResults = (from ex
                                in context.Results
                                where ex.ExamID == examID
                                select ex).Single();

                for(int i= 0; i < examCQuestions.Count(); i++)
                {
                    QuestionAnswer.Add
                        (
                        examCQuestions.ElementAt(i).Question,
                        examCQuestions.ElementAt(i).ProperAnswer
                        );
                }


                examResults = ExamsController.SetExamScoreFromClosedQuestionAnswers(examCQuestions, QuestionAnswer, examResults);

                //Assert
                Assert.Equal(4, examResults.Score);
            }


        }
        [Fact]
        public void SetExamScoreFromClosedQuestionWrongAnswersTest()
        {
            //Arrange
            mockMethod.mockDataIntoDB();
            mockMethod.mockExamDataIntoDB();
            ExamsController ExamsController = new ExamsController();
            List<ClosedQuestions> examCQuestions = new List<ClosedQuestions>();
            var QuestionAnswer = new Dictionary<string, string>();
            Results examResults;
            int examID = 1;

            //Act
            UseSqlite();
            using (var context = GetDBContext())
            {
                examCQuestions = (from ex
                               in context.ExamClosedQuestions
                                  where ex.ExamsID == examID
                                  select ex.ClosedQuestions).ToList();

                examResults = (from ex
                                in context.Results
                               where ex.ExamID == examID
                               select ex).Single();

                for (int i = 0; i < examCQuestions.Count(); i++)
                {
                    QuestionAnswer.Add
                        (
                        examCQuestions.ElementAt(i).Question,
                        "Wrong Answer"
                        );
                    // QuestionAnswer.Keys=(examCQuestions.ElementAt(i).Question);
                    // QuestionAnswer.Values.Concat(examCQuestions.ElementAt(i).ProperAnswer);

                }

                //QuestionAnswer.Keys = examCQuestions.SelectMany(x=>x.Question);
                //QuestionAnswer.Values.Concat(examCQuestions.SelectMany(x => x.ProperAnswer));


                examResults = ExamsController.SetExamScoreFromClosedQuestionAnswers(examCQuestions, QuestionAnswer, examResults);

                //Assert
                Assert.Equal(0, examResults.Score);
            }

        }
   
        }
}
