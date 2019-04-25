using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPlatform.Models
{
    public class SingleStudentExamModel
    {
        public int ExamOpenedQuestionID { get; set; }
        public String OpenedQuestion { get; set; }
        public String Answer { get; set; }
        public int MaxPoint { get; set; }
    
    public SingleStudentExamModel(
          
           int ExamOpenedQuestionID,
           String OpenedQuestion,
           String Answer,
           int MaxPoint
           )
    {
        this.ExamOpenedQuestionID = ExamOpenedQuestionID;
        this.OpenedQuestion = OpenedQuestion;
        this.Answer = Answer;
        this.MaxPoint = MaxPoint;

    }
    }
}
