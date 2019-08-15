using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using RmsDM.DAL;
using RmsDM.BFL;
using RmsDM.MODEL;

/// <summary>
/// WebFunctionRule 的摘要说明

/// </summary>
public class WebFunctionRule
{   
    
    /// <summary>
    /// 根据用户代码获取单个用户名称或一组用户名称

    /// </summary>
    /// <param name="Code">代码</param>    
    /// <returns>用户名称</returns>
    public static string GetUserNameByCode(string UserCode)
    {
        string delimStr = ",";
        char[] delimiter = delimStr.ToCharArray();
        string UserName;
        if (UserCode != "")
        {
            string[] UserCodeArr = UserCode.Split(delimiter);
            int i;
            UserName = "";
            for (i = 0; i < UserCodeArr.Length; i++)
            {
                string tempUserName = RmsPM.BLL.SystemRule.GetUserName(UserCodeArr[i]);
                if (tempUserName != "")
                {
                    UserName += (UserName == "") ? "" : ",";
                    UserName += tempUserName;
                }
            }

        }
        else
        {
            UserName = "";
        }

        return UserName;
    }
   
    /// <summary>
    /// 获取树上某节点完整路径

    /// </summary>
    /// <param name="FullPathString"></param>
    /// <returns></returns>
    public static string GetTreeViewFullPath(string FullPathString)
    {
        string delimStr = "/";
        char[] delimiter = delimStr.ToCharArray();
        string FullPathName;
        
        if (FullPathString != ""&&FullPathString!=null)
        {
            string[] FullPathArr = FullPathString.Split(delimiter);
            int i;
            FullPathName = "";
            DocumentDirectoryBFL DDBFL = new DocumentDirectoryBFL();
            for (i = 0; i < FullPathArr.Length; i++)
            {

                if (FullPathArr[i] != "" && FullPathArr[i] != null)
                {
                    string tempFullPathName = DDBFL.GetDocumentDirectory(int.Parse(FullPathArr[i])).DirectoryName;

                    if (tempFullPathName != "")
                    {
                        FullPathName += (FullPathName == "") ? "" : "-->";
                        FullPathName += tempFullPathName;
                    }
                }

            }

        }
        else
        {
            FullPathName = "";
        }

        return FullPathName;
    }  
    
}
