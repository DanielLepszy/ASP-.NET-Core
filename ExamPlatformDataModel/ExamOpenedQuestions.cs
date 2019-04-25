using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPlatformDataModel
{
    public class ExamOpenedQuestions
    {
        public int ExamOpenedQuestionsID { get; set; }

        public int? ExamsID { get; set; }
        public virtual Exams Exam { get; set; }

        public int OpenedQuestionsID { get; set; }
        public virtual OpenedQuestions OpenedQuestions { get; set; }

        public String UserAnswer { get; set; }
        public double? AnswerPoints { get; set; }
    }
}
