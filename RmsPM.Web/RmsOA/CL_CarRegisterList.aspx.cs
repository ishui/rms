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
using RmsPM.Web;

public partial class RmsOA_CL_CarRegisterList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (user.HasRight("290101"))
            {
                this.NewButton.Visible = true;
                this.NewButton.Attributes["OnClick"] = "javascript:OpenMiddleWindow('CL_CarRegisterEdit.aspx?ActType=add','CarRegisterEdit');";
            }
            else
            {
                this.NewButton.Visible = false;
            }

        }
    }
    protected void Button1_ServerClick(object sender, EventArgs e)
    {
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
}
