using NLog;
using StudentManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManager.Logs
{
    public class ExceptionLogger
    {
        static private Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static public void LogException(Exception exception)
        {
            if (exception is CustomException)
            {
                (exception as CustomException).LogSelf();
            }
            else
            {
                logger.Fatal(exception);
            }
        }
    }
}