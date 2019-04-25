using ExamPlatform.Data;
using ExamPlatform.Models;
using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using XUnitDatabaseTests.ControllersUnitTests.FRepositories;

namespace XUnitDatabaseTests
{
    public class StudentExamsTests : TestBase
    {
        MockDatabase mockMethod = new MockDatabase();

        [Fact]
        public void StudentExamsTest()
        {
            //Arrange
            using (var context = GetDBContext())
            {
                mockMethod.mockDataIntoDB();
                mockMethod.mockExamDataIntoDB();

                var data = context.Exams
                       .Include(e => e.ExamResult)
                       .Include(e => e.ExamOpenedQuestions)
                       .ThenInclude(e => e.OpenedQuestions)
                       .Include(e => e.ExamClosedQuestions)
                       .Include(a => a.Account)
                       .Where(e => e.ExamResult.Grade == null)
                       .Select(z => z).ToList();

                List<SingleStudentExamModel> singleModelList = new List<SingleStudentExamModel>();
                List<StudentExamsModel> modelList = new List<StudentExamsModel>();


            //Act
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
                //Assert
                Assert.Equal("Daniel", modelList.First().StudentName);
            }
        }
      
        [Fact]
        public void ShowAllExamsWithoutGradeTest()
        {
            mockMethod.mockDataIntoDB();
            mockMethod.mockExamDataIntoDB();
            //Arrange
            using (var context = GetDBContext())
            {

                var data = context.Account
                            .Where(a => a.Status == "STUDENT")
                            .Include(e => e.SelectedExam).ThenInclude(y => y.ExamResult)
                            .Include(e => e.SelectedExam).ThenInclude(y => y.ExamOpenedQuestions)
                            .Include(e => e.SelectedExam).ThenInclude(y => y.ExamClosedQuestions)
                            .Select(z => z).ToList();


                var data2 = context.Exams
                            .Include(e => e.ExamResult)
                            .Include(e => e.ExamOpenedQuestions)
                            .ThenInclude(e => e.OpenedQuestions)
                            .Include(e => e.ExamClosedQuestions)
                            .Include(a => a.Account)
                            .Where(e=>e.ExamResult.Grade==null)
                            .Select(z => z).ToList();

                    List<SingleStudentExamModel> singleModelList = new List<SingleStudentExamModel>();
                    List<StudentExamsModel> modelList = new List<StudentExamsModel>();

                //Act
                    foreach (var StudentAccount in data2)
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
                   //Assert
                    Assert.NotEmpty(data);
              

            }
        }
    }
}
