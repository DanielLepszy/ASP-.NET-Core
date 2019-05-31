using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPlatform.Data;
using ExamPlatform.Logger;
using ExamPlatform.Models;
using ExamPlatformDataModel;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamPlatform.Controllers
{
    
    public class CourseController : Controller
    {
        ILog logger = SingletonFirst.Instance.GetLogger();
        /// <summary>Shows the available courses for students.</summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowCourses()
        {
            try
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
            } catch (Exception ex)
            {
                logger.Error("CourseController - ShowCourses " + ex.Message);
                return View();
            }
           

        }
    }
}