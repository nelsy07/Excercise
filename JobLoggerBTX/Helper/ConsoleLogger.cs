using JobLoggerBTX.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace JobLoggerBTX.Helper
{
    public class ConsoleLogger
    {
        public ConsoleLogger()
        {
        }

        private static string PathFile { get; set; }

        public static bool WriteLog(string messageText, LogLevel level)
        {
            bool logged = false;
            switch (level)
            {
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogLevel.Message:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine(DateTime.Now.ToShortDateString() + messageText);
            logged = true;

            return logged;
        }
    }
}