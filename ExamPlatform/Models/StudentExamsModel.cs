using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPlatform.Models
{
    public class StudentExamsModel
    {
       
        public int ExamsUserID { get; set; }
        public int AccountID { get; set; }
        public String StudentName { get; set; }
        public String StudentSurname { get; set; }
        public IList<SingleStudentExamModel> SingleStudentExam { get; set; }
 
        public StudentExamsModel(
            int ExamsUserID,
            int AccountID,
            String StudentName,
            String StudentSurname,
            IList<SingleStudentExamModel> SingleStudentExam
            
            )
        {
            this.ExamsUserID = ExamsUserID;
            this.AccountID = AccountID;
            this.StudentName = StudentName;
            this.StudentSurname = StudentSurname;
            this.SingleStudentExam = SingleStudentExam;
        }   
    }
}
