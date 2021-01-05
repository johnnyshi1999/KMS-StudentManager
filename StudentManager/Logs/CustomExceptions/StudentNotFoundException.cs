using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using AutoMapper;
using NLog;

namespace StudentManager.Logs.CustomExceptions
{
    public class StudentNotFoundException : CustomException
    {
        public StudentNotFoundException()
        {
        }

        public StudentNotFoundException(string message) : base(message)
        {
        }

        public StudentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public StudentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void LogSelf()
        {
            Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Error(this);
        }
    }
}