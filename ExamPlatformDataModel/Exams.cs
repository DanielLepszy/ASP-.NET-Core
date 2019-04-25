using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ExamPlatformDataModel
{
    public class Exams
    {
        public int ExamsID { get; set; }

        public int AmountClosedQuestions { get; set; }
        public int AmountOpenedQuestions { get; set; }

        public int ExamTimeInMinute { get; set; }
        public DateTime DateOfExam { get; set; }

        public int AccountID { get; set; }
        public virtual Accounts Account { get; set; }


        public virtual Course Course { get; set; }
        public int CourseID { get; set; }

        public virtual IList<ExamClosedQuestions> ExamClosedQuestions { get; set; }
        public virtual IList<ExamOpenedQuestions> ExamOpenedQuestions { get; set; }
        public virtual Results ExamResult { get; set; }
       
    }
}
