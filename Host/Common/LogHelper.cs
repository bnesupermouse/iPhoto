﻿using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Host.Common
{
    public class LogHelper
    {
        public static void WriteLog(string logContent)
        {
            WriteLog(null, logContent, Log4NetLevel.Error);
        }

        public static void WriteLog(string logContent, Log4NetLevel log4Level)
        {
            WriteLog(null, logContent, log4Level);
        }

        public static void WriteLog(Type type, string logContent, Log4NetLevel log4Level)
        {
            ILog log = type == null ? LogManager.GetLogger("") : LogManager.GetLogger(type);

            switch (log4Level)
            {
                case Log4NetLevel.Warn:
                    log.Warn(logContent);
                    break;
                case Log4NetLevel.Debug:
                    log.Debug(logContent);
                    break;
                case Log4NetLevel.Info:
                    log.Info(logContent);
                    break;
                case Log4NetLevel.Fatal:
                    log.Fatal(logContent);
                    break;
                case Log4NetLevel.Error:
                    log.Error(logContent);
                    break;
            }
        }

    }

    public enum Log4NetLevel
    {
        [Description("Warning")]
        Warn = 1,
        [Description("Debug")]
        Debug = 2,
        [Description("Information")]
        Info = 3,
        [Description("Fatal")]
        Fatal = 4,
        [Description("Error")]
        Error = 5
    }


}