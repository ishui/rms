namespace Rms.LogHelper
{
    using log4net;
    using log4net.Appender;
    using log4net.Config;
    using log4net.Repository.Hierarchy;
    using System;
    using System.IO;

    public class LogHelper
    {
        private static readonly ILog errorlog = LogManager.GetLogger("ErrorLog");
        private static readonly ILog infolog = LogManager.GetLogger("InfoLog");

        protected LogHelper()
        {
        }

        public static void Debug(string message)
        {
            infolog.Debug(message);
        }

        public static void Debug(string message, Exception exp)
        {
            infolog.Debug(message, exp);
        }

        public static void Error(string message)
        {
            errorlog.Error(message);
        }

        public static void Error(string message, Exception exp)
        {
            errorlog.Error(message, exp);
        }

        public static void Fatal(string message)
        {
            errorlog.Fatal(message);
        }

        public static void Fatal(string message, Exception exp)
        {
            errorlog.Fatal(message, exp);
        }

        public static void Info(string message)
        {
            infolog.Info(message);
        }

        public static void Info(string message, Exception exp)
        {
            infolog.Info(message, exp);
        }

        public static void SetConfig()
        {
            XmlConfigurator.Configure();
        }

        public static void SetConfig(FileInfo f, string connectionstring)
        {
            XmlConfigurator.ConfigureAndWatch(f);
            if (connectionstring.Length > 10)
            {
                Logger logger = infolog.Logger as Logger;
                AdoNetAppender appender = logger.GetAppender("AdoNetAppender") as AdoNetAppender;
                if (appender != null)
                {
                    appender.ConnectionString = connectionstring;
                    appender.ActivateOptions();
                }
                logger = errorlog.Logger as Logger;
                appender = logger.GetAppender("AdoNetAppender") as AdoNetAppender;
                if (appender != null)
                {
                    appender.ConnectionString = connectionstring;
                    appender.ActivateOptions();
                }
            }
        }

        public static void Warn(string message)
        {
            errorlog.Warn(message);
        }

        public static void Warn(string message, Exception exp)
        {
            errorlog.Warn(message, exp);
        }

        [Obsolete]
        public static void WriteLog(string info)
        {
            infolog.Info(info);
        }

        [Obsolete]
        public static void WriteLog(string info, Exception exp)
        {
            errorlog.Info(info, exp);
        }
    }
}

