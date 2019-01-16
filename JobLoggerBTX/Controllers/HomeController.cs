using JobLoggerBTX.Helper;
using JobLoggerBTX.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            JobLogger jobLogger = new JobLogger();
            jobLogger.Log(LogLevel.Error, "Mensaje de prueba");
            return View();
        }
    }
}