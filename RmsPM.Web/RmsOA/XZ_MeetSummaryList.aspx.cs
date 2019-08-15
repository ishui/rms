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
using System.Text;

using RmsOA.MODEL;
using RmsOA.BFL;
using RmsPM.Web;


public partial class RmsOA_XZ_MeetSummaryList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!user.HasRight("320201"))
            {
                this.NewButton.Visible = false;
            }
        }
    }

    protected void SearchButton_Click(object sender, EventArgs e)
    {
        this.ObjectDataSource1.SelectParameters["StatusEqual"].DefaultValue = GetListGroupSelectedValues(this.cblStatus);
        SummaryGridView.DataBind();
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
