using System;
using System.Collections.Generic;
using System.Text;

namespace ExamPlatformDataModel
{
    public class Results
    {
        public int ResultsID { get; set; }
        public virtual Exams Exam { get; set; }
        public  int ExamID { get; set; }
        public double Score { get; set; }
        public double MaxExamPoints { get; set; }
        public double? Grade { get; set; }
    }
}
