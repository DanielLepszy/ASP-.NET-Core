﻿using System;

namespace ExamPlatform.Models
{
    public class UserEmailInfoModel
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String Course { get; set; }
        public double Score { get; set; }
        public double? Grade { get; set; }
        public DateTime ExamDate { get; set; }

        public UserEmailInfoModel(String name, String surname, String email, String course, double? grade, DateTime examDate)
        {
            this.Name = name;
            this.Surname = surname;
            this.Email = email;
            this.Course = course;
            this.Grade = grade;
            this.ExamDate = examDate;
        }
    }
}
