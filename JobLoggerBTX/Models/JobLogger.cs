using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobLoggerBTX.Models
{
    public partial class JobLogger : ILog
    {
        public static string[] _levels;
        public static LogType _logType;
        private static bool _logMessage = false;
        private static bool _logError = false;
        private static bool _logWarning = false;

        public JobLogger(LogType logType, string[] levels)
        {
            _logType = logType;
            _levels = levels;
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

                switch (_logType)
                {
                    case LogType.Database:
                        DatabaseLogger.WriteLog(message, level);
                        break;
                    case LogType.File:
                        FileLogger.WriteLog(message, level);
                        break;
                    case LogType.Console:
                        ConsoleLogger.WriteLog(message, level);
                        break;
                }
            }
        }

        private bool IsEnabled(LogLevel level)
        {
            bool enable = false;
            switch (level)
            {
                case LogLevel.Error:
                    if (_levels.Where(x => x == "error").Count() > 0)
                    {
                        enable = true;
                    }
                    break;
                case LogLevel.Message:
                    if (_levels.Where(x => x == "message").Count() > 0)
                    {
                        enable = true;
                    }
                    break;
                case LogLevel.Warning:
                    if (_levels.Where(x => x == "warning").Count() > 0)
                    {
                        enable = true;
                    }
                    break;
            }

            return enable;
        }

        public
    }
}