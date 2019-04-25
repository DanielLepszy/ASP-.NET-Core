using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPlatform.Models
{
    public class AnswersModel
    {
       public List<KeyValuePair<String,String>> QuestionAnswer { get; set; }
       public  String Answer { get; set; }
   }
}
