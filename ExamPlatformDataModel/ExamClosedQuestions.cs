using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPlatformDataModel
{
    public class ExamClosedQuestions
    {
        public int ExamClosedQuestionsID { get; set; }

        public int? ExamsID { get; set; }
        public virtual Exams Exam { get; set; }

        public int ClosedQuestionsId { get; set; }
        public virtual ClosedQuestions ClosedQuestions { get; set; }
    }
}
