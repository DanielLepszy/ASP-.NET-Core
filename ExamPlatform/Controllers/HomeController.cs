using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExamPlatform.Models;
using System.Globalization;
using ExamPlatformDataModel;
using ExamPlatform.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Net;
using log4net;
using System.Reflection;
using log4net.Config;
using ExamPlatform.Logger;
using System.Security.Cryptography;

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
