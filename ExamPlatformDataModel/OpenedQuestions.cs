using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPlatformDataModel
{
    public class OpenedQuestions
    {
        public int OpenedQuestionsID { get; set; }
        public String Question { get; set; }
        public int MaxPoints { get; set; }
      

        public int CourseID { get; set; }
        public virtual Course Course { get; set; }
      
        public virtual IList<ExamOpenedQuestions> ExamOpenedQuestions { get; set; }

    }
}
