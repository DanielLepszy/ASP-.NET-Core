using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPlatformDataModel
{
    public class Course
    {
        public int CourseID { get; set; }
        public String CourseType { get; set; }

        public virtual ICollection<Exams> ExamsList { get; set; }
        public virtual  ICollection<ClosedQuestions> ClosedQuestionsList { get; set; }
        public virtual  ICollection<OpenedQuestions> OpenedQuestionsList { get; set; }
    }
}
