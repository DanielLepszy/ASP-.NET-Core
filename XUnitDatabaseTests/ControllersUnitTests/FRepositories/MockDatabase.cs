using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExamPlatform.Controllers;
using ExamPlatformDataModel;
using Microsoft.EntityFrameworkCore;
using XUnitDatabaseTests.ControllersUnitTests.FRepositories.IRepositories;

namespace XUnitDatabaseTests.ControllersUnitTests.FRepositories
{
    public class MockDatabase : TestBase, IMockDatabase
    {
        public void mockDataIntoDB()
        {
            UseSqlite();
            using (var context = GetDBContext())
            {
                 ClearData(context);
                InitAndGetDBContextWithManyRelations();
                ConnectionClose(context);
            }
        }

        public void mockExamDataIntoDB()
        {
            ExamsController ExamsController = new ExamsController();
            UseSqlite();
            using (var context = GetDBContext())
            {
                Accounts SelectedAccount = context.Account
                                            .Where(a => a.Status == "STUDENT")
                                            .Select(a => a).Single();

                Course SelectedCourse = context.Course
                                        .Include(c => c.ClosedQuestionsList)
                                        .Include(q => q.OpenedQuestionsList)
                                        .Where(course => course.CourseType == "Architecure .NET")
                                        .Select(seleceteCourse => seleceteCourse).Single();


                var CQuestions = context.ClosedQuestions.Where(questions => questions.Course.CourseType.Equals("Architecure .NET"))
                                .Select(questions => questions).ToList().Take(4);

                var OQuestions = context.OpenedQuestions.Where(questions => questions.Course.CourseType.Equals("Architecure .NET"))
                                .Select(questions => questions).ToList().Take(4);


                var ExamCQuestions = ExamsController.CreateExamClosedQuestions(CQuestions.ToList());
                var ExamOQuestions = ExamsController.CreateExamOpenedQuestions(OQuestions.ToList());


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
                    ExamResult = new Results()

                };
                ExamCQuestions.ForEach(x => exam.ExamResult.MaxExamPoints += 1);
                ExamOQuestions.ForEach(x => exam.ExamResult.MaxExamPoints += x.OpenedQuestions.MaxPoints);
                context.Exams.Add(exam);
                context.SaveChanges();
            };
        }
    }
}
