using ExamPlatformDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPlatform.Models
{
    public class CoursesViewModel
    {
        public int[] CourseID { get; set; }
        public List<Course> Courses {get; set;}
    }
}
