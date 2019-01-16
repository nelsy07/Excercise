using JobLoggerBTX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobLoggerBTX.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            JobLogger jobLogger = new JobLogger(LogType.Console, new string[] { "message", "error", "warning" });
            jobLogger.Log(LogLevel.Error, "Hola");
            return View();
        }
    }
}