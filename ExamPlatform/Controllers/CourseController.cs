using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPlatform.Data;
using ExamPlatform.Models;
using ExamPlatformDataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamPlatform.Controllers
{
    public class CourseController : Controller
    {

        /// <summary>Shows the available courses for students.</summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowCourses()
        {
            using (var context = new ExamPlatformDbContext())
            {
                var CoursesFromDB = context.Course.GroupBy(c => c.CourseType).Select(c => c.First()).ToList();
                CoursesViewModel model = new CoursesViewModel()
                {
                    Courses = CoursesFromDB.ToList()
                };

                return View("Courses", model);
            }

        }
    }
}