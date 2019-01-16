using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobLoggerBTX.Models
{
    public partial interface ILog
    {
        void Log();
        void Log(LogLevel level, string message);

    }
}