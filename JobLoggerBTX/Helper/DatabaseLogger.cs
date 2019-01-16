using JobLoggerBTX.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace JobLoggerBTX.Helper
{
    public class DatabaseLogger
    {
        public DatabaseLogger()
        {
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        private static string ConnectionString { get; set; }

        public static bool WriteLog(string messageText, LogLevel logType)
        {
            bool logged = false;
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand("Insert into Log Values('" + messageText + "', " + logType.ToString() + ")");
            int result = command.ExecuteNonQuery();

            if (result > 0)
            {
                logged = true;
            }

            return logged;
        }
    }
}