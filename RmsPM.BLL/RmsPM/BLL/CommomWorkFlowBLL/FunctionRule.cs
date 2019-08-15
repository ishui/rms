namespace RmsPM.BLL.CommomWorkFlowBLL
{
    using System;
    using System.Configuration;

    public class FunctionRule
    {
        public static string GetConnectionString()
        {
            if ((ConfigurationSettings.AppSettings["DBConnString"] == null) || (ConfigurationSettings.AppSettings["DBConnString"].ToString().Trim() == ""))
            {
                throw new Exception("数据库连接字符串出现异常！");
            }
            return ConfigurationSettings.AppSettings["DBConnString"].ToString();
        }
    }
}

