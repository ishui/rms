using System;
using System.Configuration;
using System.IO;



namespace RmsPM.Web
{

	public class ApplicationConfigSetup :System.Web.UI.Page
	{


		public ApplicationConfigSetup()
		{

		}

		public void LoadLogPath()
		{
			string vPath = Server.MapPath(ConfigurationSettings.AppSettings["VirtualDirectory"]);
			ApplicationLog.SetLogPath( vPath + ConfigurationSettings.AppSettings["ApplicationLogPath"] );
		}


	}

	/// <summary>
	/// ApplicationLog 的摘要说明。
	/// </summary>
	public sealed class ApplicationLog 
	{


		private static string m_LogPath = "";


		public ApplicationLog()
		{
		}


		public static void SetLogPath ( string logPath )
		{
			m_LogPath = logPath;
		}



		public static void WriteLog (String className , String logMessage)
		{

			/*try
			{
				StreamWriter w = File.AppendText( GetLogFileName());
				w.WriteLine("\r\nLog ClassName : "  + className  + " ; "  );
				w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
					DateTime.Now.ToLongDateString());
				w.WriteLine(" logMessage :");
				w.WriteLine("  :{0}", logMessage);
				w.WriteLine ("-------------------------------");
				w.Flush(); 
				w.Close();

			}
			catch 
			{
				

			}*/
            LogHelper.WriteLog("ClassName:" + className + "  logMessage:" + logMessage);
		}


		public static void WriteLog (String className , String logMessage , String traceMessage , String otherMessage )
		{

            LogHelper.WriteLog("ClassName:" + className + "  logMessage:" + logMessage+"<br>traceMessage:"+traceMessage+"  otherMessage"+otherMessage);
            /*try
			{
				StreamWriter w = File.AppendText( GetLogFileName());
				w.WriteLine("\r\nLog ClassName : "  + className  + " ; "  );
				w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
					DateTime.Now.ToLongDateString());
				w.WriteLine(" LogMessage  :");
				w.WriteLine("  :{0}", logMessage);
				w.WriteLine(" TraceMessage  :");
				w.WriteLine("  :{0}", traceMessage);
				w.WriteLine(" logMessage  :");
				w.WriteLine("  :{0}", otherMessage);
				w.WriteLine ("-------------------------------");
				w.Flush(); 
				w.Close();

			}
			catch 
			{

			}*/
		}

		public static void WriteLog (String className ,Exception ex , String otherMessage )
		{
            LogHelper.WriteLog("ClassName:" + className + "  otherMessage:" + otherMessage,ex);

			/*try
			{
				StreamWriter w = File.AppendText( GetLogFileName());
				w.WriteLine("\r\nLog ClassName : "  + className  + " ; "  );
				w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
					DateTime.Now.ToLongDateString());
				w.WriteLine(" LogMessage  :");
				w.WriteLine("  :{0}", ex.Message);
				w.WriteLine(" TraceMessage  :");
				w.WriteLine("  :{0}", ex.StackTrace);
				w.WriteLine(" logMessage  :");
				w.WriteLine("  :{0}", otherMessage);
				w.WriteLine("  语句 :{0}", ex.HelpLink);
				w.WriteLine ("-------------------------------");
				w.Flush(); 
				w.Close();

			}
			catch 
			{

			}*/
		}

		/// <summary>
		/// 日志写入指定文件
		/// </summary>
		/// <param name="FileName"></param>
		/// <param name="className"></param>
		/// <param name="logMessage"></param>
		public static void WriteFile (string FileName, string className, String logMessage)
		{

			try
			{
				StreamWriter w = File.AppendText( GetLogFileName(FileName));

				w.WriteLine("\r\nLog ClassName : "  + className  + " ; "  );
				w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
					DateTime.Now.ToLongDateString());
				w.WriteLine(" logMessage :");

				//有换行符时，分行写入
				string[] arr = logMessage.Split("\n"[0]);
				foreach(string mess in arr) 
				{
					w.WriteLine("{0}", mess);
				}

				w.WriteLine ("-------------------------------");
				w.Flush(); 
				w.Close();

			}
			catch 
			{
				

			}
		}

		public static string GetLogFileName ()
		{
			return m_LogPath + DateTime.Now.Date.ToShortDateString()+ DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +  "log.txt" ;
		}

		public static string GetLogFileName (string FileName)
		{
			return m_LogPath + FileName;
		}

	}
}
