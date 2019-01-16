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
    public class FileLogger
    {
        public FileLogger()
        {
            PathFile = ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt";
        }

        private static string PathFile { get; set; }

        public static bool WriteLog(string messageText, LogLevel level)
        {
            bool logged = false;
            if (!File.Exists(PathFile))
            {
                using (StreamReader reader = new StreamReader(PathFile))
                {
                    string txtFile = reader.ReadToEnd();
                    txtFile += DateTime.Now.ToShortDateString() + messageText;
                    File.WriteAllText(PathFile, txtFile);
                    logged = true;
                }
            }
            else
            {
                throw new Exception("The file already exists.");
            }

            return logged;
        }
    }
}