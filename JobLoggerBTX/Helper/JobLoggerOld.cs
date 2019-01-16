using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace LogsApp.Models
{
    public class JobLogger
    {

        private static bool _logToFile;
        private static bool _logToConsole;
        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;
        //Esta variable debería ser renombrada de la siguiente manera por ser una variable global: "_logToDatabase"
        //private static bool LogToDatabase;
        private static bool _logToDatabase;
        private bool _initialized;
        public JobLogger(bool logToFile, bool logToConsole, bool logToDatabase, bool logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            //Se debe renombrar la variable 
            //LogToDatabase = logToDatabase;
            _logToDatabase = logToDatabase;
            _logToFile = logToFile;
            _logToConsole = logToConsole;
        }

        //En este método existen dos variables con el mismo nombre de "message".
        //Se debe modificar el nombre de una de las variables y renombrar la variable en los casos que sea necesario.
        //De igual manera crearía este método en una clase adicional, utilizando una interface para luego darle más utilidad al método y poder sobreescribirlo.
        //public static void LogMessage(string message, bool message, bool warning, bool error)
        public void LogMessage(string messageText, bool message, bool warning, bool error)
        {
            //Acá se realiza un trim de la variable message del tipo string, pero nunca se reasigna el nuevo valor a la variable, y además se debe renombrar la misma
            //message.Trim();
            messageText = messageText.Trim();

            //Esta comparación debería ser modificada utilizando la funcionalidad: !string.IsNullOrEmpty(message), para corroborar que el valor no venga vacío ni null,
            //y además debe ser renombrada la variable a messageText
            //if (message == null || message.Length == 0)
            if (!string.IsNullOrEmpty(messageText))
            {
                return;
            }
            //Se debe renombrar la variable a _logToDatabase
            if (!_logToConsole && !_logToFile && !_logToDatabase)
            {
                throw new Exception("Invalid configuration");
            }
            if ((!_logError && !_logMessage && !_logWarning) || (!message && !warning && !error))
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            //Para cada caso de log, crearía una clase helper invidual para mantener el código y las funcionalidades separadas.
            //Por ejemplo: 1 clase para Database, 1 clase para Console y por último una para File, donde cada una realice su respectivo log de manera independiente.


            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]);
            connection.Open();

            //Esta variable debe ser inicializada como t = 0 para que pueda ser utilizada en el código más abajo
            //Y además utilizar un nombre más representativo
            //int t;
            int logType = 0;
            if (message && _logMessage)
            {
                logType = 1;
            }
            if (error && _logError)
            {
                logType = 2;
            }
            if (warning && _logWarning)
            {
                logType = 3;
            }

            //Acá se debe colocar el nombre de la variable renombrada que contiene el mensaje del tipo string.
            //SqlCommand command = new SqlCommand("Insert into Log Values('" + message + "', " + logType.ToString() + ")");
            SqlCommand command = new SqlCommand("Insert into Log Values('" + messageText + "', " + logType.ToString() + ")");
            command.ExecuteNonQuery();

            //Esta variable también debe ser inicializada como l = string.Empty para que pueda ser utilizada en el código más abajo
            //string l;
            string l = string.Empty;

            
            if (!File.Exists(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"))
            {
                //De esta manera se puede leer el archivo correctamente, pero también se podría leer el texto con StreamReader, ya que es más performante
                //l = File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt");
                using (StreamReader reader = new StreamReader(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt"))
                {
                    l = reader.ReadToEnd();
                }
            }

            //Este código se puede simplificar, juntando las validaciones para no realizarlas de manera repetitiva.

            if (error && _logError)
            {
                l = l + DateTime.Now.ToShortDateString() + message;
            }
            if (warning && _logWarning)
            {
                l = l + DateTime.Now.ToShortDateString() + message;
            }
            if (message && _logMessage)
            {
                l = l + DateTime.Now.ToShortDateString() + message;
            }

            File.WriteAllText(System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt", l);

            if (error && _logError)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (warning && _logWarning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (message && _logMessage)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(DateTime.Now.ToShortDateString() + message);


            //NOTA: En mi ejercicio creo un proyecto que contiene una interface para el método de log, 
            //de manera que se podría sobreescribir para reutilizar a medida que se necesite.

            //Contiene cuatro clases helper:
            //1. JobLoggercs: es la clase que se va a utilizar para invocar y realizar el registro del mensaje enviado por parámetro.
            //2. DataBaseLogger.cs: es la clase que realiza el registro del mensaje en la Base de Datos.
            //3. FileLogger.cs: es la clase que realiza el registro del mensaje en un nuevo archivo.
            //4. ConsoleLogger.cs: es la clase que realiza el registro del mensaje en la consola.

            //Es importante recalcar que no es el proyecto más indicado para realizar este tipo de log, ya que lo ideal es crear una librería de clases,
            //Y luego incluirla en el proyecto donde se vaya a utilizar.
        }
    }
}