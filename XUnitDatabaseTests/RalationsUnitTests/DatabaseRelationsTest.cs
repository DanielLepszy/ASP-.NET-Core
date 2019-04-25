using ExamPlatformDataModel;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitDatabaseTests
{
    // Before running test. delete all records in DB
    public class UnitTest1 : TestBase
    {
        [Fact]
        public void OneToOneRelationsTest()
        {
            //arrange
            UseSqlite();
            using (var context = InitAndGetDBContext())
            {
                try
                {
                   // act
                    //List<Accounts> examWithGrade = (from exam
                    //                               in context.ExamOpenedQuestions
                    //                              where exam.Exam == accounts.
                    //                              select accounts).ToList();
                    //Assert.NotEmpty(allAccounts);
                }
                finally
                {
                    ConnectionClose(context);
                    context.Database.EnsureDeleted();
                }
            }
        }
        [Fact]
        public void OneToManyRelationsTest()
        {
            //arrange
            UseSqlite();
            using (var context = InitAndGetDBContextWithManyRelations())
            {
                try
                {
                    //act
                    List<Exams> allExams = (from exams
                                            in context.Exams
                                            where exams.CourseID == 1
                                            select exams).ToList();

                    List<Course> allCourse = (from course
                                            in context.Course
                                              where (course.CourseID == 1)
                                              select course).ToList();

                    Assert.Equal(2, allExams.Count);
                    Assert.NotEmpty(allCourse);
                }

                finally
                {
                    ConnectionClose(context);
                    context.Database.EnsureDeleted();
                }

            }
        }
        [Fact]
        public void ManyToManyRelationsTest()
        {
            //arrange
            UseSqlite();
            using (var context = InitAndGetDBContextWithManyRelations())
            {
                try
                {
                   // act
                List<Exams> Exam = (from exams
                                        in context.Exams
                                    where exams.AccountID == 1
                                    select exams).ToList();

                    List<ClosedQuestions> AllClosedQuestionsInExam = (from questions
                                                                    in context.ClosedQuestions
                                                                      where questions.ExamClosedQuestions.Any(x => x.ExamsID == 1)
                                                                      select questions).ToList();

                    Assert.NotEmpty(Exam);
                    Assert.Equal(2, AllClosedQuestionsInExam.Count);
                }

                finally
                {
                    ConnectionClose(context);
                    context.Database.EnsureDeleted();
                }

            }
        }
    }
}
   
