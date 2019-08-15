using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class Finance_PaymentSumList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ObjectDataSource1.SelectParameters["StatusEqual"].DefaultValue = GetListGroupSelectedValues(this.cblStatus);
    }

    protected void Button1_ServerClick(object sender, EventArgs e)
    {
        this.ObjectDataSource1.SelectParameters["StatusEqual"].DefaultValue = GetListGroupSelectedValues(this.cblStatus);
        this.GridView1.DataBind();
    }

    /// <summary>
    /// 获取选中的状态

    /// </summary>
    /// <param name="cbl"></param>
    /// <returns></returns>
    public static string GetListGroupSelectedValues(CheckBoxList cbl)
    {
        string re = "";
        foreach (ListItem li in cbl.Items)
        {
            if (li.Selected)
            {
                if (re != "")
                {
                    re += ",";
                }

                re += li.Value;

            }
        }
        return re;
    }

    public static string GetStatusName(string str)
    {
        string strReturn;
        switch (str)
        {
            case "1":
                strReturn = "申请";
                break;
            case "2":
                strReturn = "审核中";
                break;
            case "3":
                strReturn = "通过";
                break;
            case "4":
                strReturn = "未通过";
                break;
            case "5":
                strReturn = "作废";
                break;
            default:
                strReturn = "";
                break;
        }
        return strReturn;
    }
}
