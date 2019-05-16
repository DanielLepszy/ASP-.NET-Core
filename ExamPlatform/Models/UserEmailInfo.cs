using System;

namespace ExamPlatform.Models
{
    public class UserEmailInfoModel
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String Course { get; set; }
        public double Score { get; set; }
        public double MaxScore { get; set; }
        public double? Grade { get; set; }
        public DateTime ExamDate { get; set; }
        public Boolean IfEmailSent { get; set; }


        public UserEmailInfoModel(String name, String surname, String email, String course, double? grade, DateTime examDate,double score, double maxScore, Boolean emailSent =false)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Course = course;
            this.Grade = grade;
            this.Score = score;
            this.MaxScore = maxScore;
            this.ExamDate = examDate;
            this.IfEmailSent = emailSent;
        }
    }
}
