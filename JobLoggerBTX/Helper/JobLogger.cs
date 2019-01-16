using JobLoggerBTX.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace JobLoggerBTX.Helper
{
    public partial class JobLogger : ILog
    {
        public static string[] _levels;
        public static string[] _logType;
        private static bool _logMessage = false;
        private static bool _logError = false;
        private static bool _logWarning = false;

        public JobLogger()
        {
            _logType = ConfigurationManager.AppSettings["LogType"].Split(',');
            _levels = ConfigurationManager.AppSettings["LogLevels"].Split(',');
        }

        public void Log()
        {
            throw new NotImplementedException();
        }

        public void Log(LogLevel level, string message)
        {
            if (IsEnabled(level))
            {
                if (!string.IsNullOrEmpty(message))
                {
                    throw new Exception("Invalid message");
                }
                bool logged;

                foreach (string item in _logType)
                {
                    switch ((LogType)int.Parse(item))
                    {
                        case LogType.Database:
                            logged = DatabaseLogger.WriteLog(message, level);
                            break;
                        case LogType.File:
                            logged = FileLogger.WriteLog(message, level);
                            break;
                        case LogType.Console:
                            logged = ConsoleLogger.WriteLog(message, level);
                            break;
                    }
                }
            }
        }

        private bool IsEnabled(LogLevel level)
        {
            bool enable = false;

            if (_levels.Where(x => int.Parse(x) == (int)level).Count() > 0)
            {
                enable = true;
            }

            return enable;
        }
        
    }
}