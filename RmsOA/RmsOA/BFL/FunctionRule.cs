namespace RmsOA.BFL
{
    using System;
    using System.Configuration;

    public class FunctionRule
    {
        public static string GetConnectionString()
        {
            if ((ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString == null) || (ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString == ""))
            {
                throw new Exception("数据库连接字符串出现异常！");
            }
            return ConfigurationManager.ConnectionStrings["RmsPM.Data.ConnectionString"].ConnectionString;
        }
    }
}

