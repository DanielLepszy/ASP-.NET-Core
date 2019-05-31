using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExamPlatform.Models;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using log4net;
using ExamPlatform.Logger;


namespace ExamPlatform.Controllers
{
    public class HomeController : Controller
    {
        ILog logger = SingletonFirst.Instance.GetLogger();

        public IActionResult Index()
        {
                DateTime loaclDate = DateTime.Now;
                CultureInfo eng = new CultureInfo("en-US");
                var dates = loaclDate.DayOfWeek.ToString() + ", " + loaclDate.Day.ToString("d2") + " " + loaclDate.ToString("MMMM", eng);

            return View("Index",dates);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
