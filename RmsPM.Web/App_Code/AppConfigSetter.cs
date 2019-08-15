using System;
using System.Web;
using System.Configuration;
using Rms.ORMap;

namespace RmsPM.Web
{
	/// <summary>
	/// AppConfigSetter 的摘要说明。
	/// </summary>
	public class AppConfigSetter :System.Web.UI.Page, IAppConfigSetter
	{
		private static string DBConnString = "";
		private static string EntityDefinePath = "" ;
		private static string DBType = "";

		public AppConfigSetter()
		{
			if( "" == DBConnString)
			{
				DBConnString=System.Configuration.ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString ;
			}

			if ( "" == DBType )
			{
				DBType=ConfigurationSettings.AppSettings["DBType"];
			}

			if( "" ==EntityDefinePath)
			{
				string vPath = Server.MapPath(ConfigurationSettings.AppSettings["VirtualDirectory"]);
				string path = vPath +  ConfigurationSettings.AppSettings["EntityDefinePath"];
				
				EntityDefinePath=path;
			}
		}

		public  string GetDBConnectionString ()
		{
			return AppConfigSetter.DBConnString;
		}
		public string GetEntityDefinePath()
		{
			return AppConfigSetter.EntityDefinePath;
		}

		public string GetDBType()
		{
			return AppConfigSetter.DBType;
		}

	}
}
