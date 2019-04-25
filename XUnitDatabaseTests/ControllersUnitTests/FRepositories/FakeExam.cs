using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitDatabaseTests.ControllersUnitTests.FRepositories
{
   public class FakeExam
    {
        public Exams FakeNewExam = new Exams()
        {
            AmountClosedQuestions = 4,
            AmountOpenedQuestions = 4,
            ExamTimeInMinute = 30,
            DateOfExam = new DateTime(),
            CourseID = 1,
            AccountID = 1,
            ExamResult = new Results()
            
        };
    }
}
