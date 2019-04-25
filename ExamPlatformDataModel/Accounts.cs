using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExamPlatformDataModel
{
    
    public class Accounts
    {
        public int AccountsID { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public String Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public String Surname { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        [RegularExpression(@"^([A-Za-z0-9]){4,20}$",    
         ErrorMessage = "Value must be from 4 to 20 characters in length, only allow letters and numbers, no special characters.")]
        public String Username { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
           ErrorMessage = "Password should contain at least 8 characters,"
           + " 1 number,1 uppercase and lowercase letter."
           + "\n Special characters ale allowed.")]
        public String Password { get; set; }

        public virtual List<Exams> SelectedExam { get; set; }
        public String Status { get; set; }

}
}

