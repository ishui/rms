namespace RmsPM.BFL
{
    using System;
    using System.Configuration;

    public class FunctionRule
    {
        public static string GetConnectionString()
        {
            if ((ConfigurationManager.AppSettings["DBConnString"] == null) || (ConfigurationManager.AppSettings["DBConnString"].ToString() == ""))
            {
                throw new Exception("数据库连接字符串出现异常！");
            }
            return ConfigurationManager.AppSettings["DBConnString"].ToString();
        }
    }
}

