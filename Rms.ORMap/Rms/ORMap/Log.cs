namespace Rms.ORMap
{
    using System;
    using System.IO;

    public sealed class Log
    {
        public static void WriteLog(string className, string logMessage)
        {
            try
            {
                StreamWriter writer = File.AppendText(DateTime.Now.Date.ToShortDateString() + "log.txt");
                writer.WriteLine("\r\nLog ClassName : " + className + " ; ");
                writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                writer.WriteLine(" logMessage :");
                writer.WriteLine("  :{0}", logMessage);
                writer.WriteLine("-------------------------------");
                writer.Flush();
                writer.Close();
            }
            catch
            {
            }
        }

        public static void WriteLog(string className, Exception ex, string otherMessage)
        {
            try
            {
                StreamWriter writer = File.AppendText(DateTime.Now.Date.ToShortDateString() + "log.txt");
                writer.WriteLine("\r\nLog ClassName : " + className + " ; ");
                writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                writer.WriteLine(" LogMessage  :");
                writer.WriteLine("  :{0}", ex.Message);
                writer.WriteLine(" TraceMessage  :");
                writer.WriteLine("  :{0}", ex.StackTrace);
                writer.WriteLine(" logMessage  :");
                writer.WriteLine("  :{0}", otherMessage);
                writer.WriteLine("-------------------------------");
                writer.Flush();
                writer.Close();
            }
            catch
            {
            }
        }

        public static void WriteLog(string className, string logMessage, string traceMessage, string otherMessage)
        {
            try
            {
                StreamWriter writer = File.AppendText(DateTime.Now.Date.ToShortDateString() + "log.txt");
                writer.WriteLine("\r\nLog ClassName : " + className + " ; ");
                writer.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                writer.WriteLine(" LogMessage  :");
                writer.WriteLine("  :{0}", logMessage);
                writer.WriteLine(" TraceMessage  :");
                writer.WriteLine("  :{0}", traceMessage);
                writer.WriteLine(" logMessage  :");
                writer.WriteLine("  :{0}", otherMessage);
                writer.WriteLine("-------------------------------");
                writer.Flush();
                writer.Close();
            }
            catch
            {
            }
        }
    }
}

