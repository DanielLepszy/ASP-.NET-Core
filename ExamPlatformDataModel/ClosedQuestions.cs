using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPlatformDataModel
{
    public class ClosedQuestions
    {
        public int ClosedQuestionsID { get; set; }
        public String Question { get; set; }
        public String ProperAnswer { get; set; }
        public int AnswerPoints { get; set; }
        public String Answer_1 { get; set; }
        public String Answer_2 { get; set; }
        public String Answer_3 { get; set; }
        public String Answer_4 { get; set; }

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }

       
        public virtual IList<ExamClosedQuestions> ExamClosedQuestions { get; set; }

    }
}
