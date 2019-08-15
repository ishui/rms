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

public partial class SelectBox_selectindesignchange : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ReturnFunc"] = Request.QueryString["ReturnFunc"];
            //this.StatusCheckBoxList.SelectedIndex = this.StatusCheckBoxList.Items.IndexOf(this.StatusCheckBoxList.Items.FindByValue(Request["State"] + ""));
            StatusCheckBoxList.SelectedIndex = 2;
        }
        this.txtContract.ProjectCode = Request["ProjectCode"] + "";
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.GridView1.PageIndex = 0;
        this.GridView1.DataBind();
    }
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
        string StatusStr = "";
        foreach (ListItem ItemStatus in this.StatusCheckBoxList.Items)
        {
            if (ItemStatus.Selected)
            {
                StatusStr += "'" + ItemStatus.Value + "',";
            }
        }
        if (StatusStr.Length > 0)
            StatusStr = StatusStr.Substring(0, StatusStr.Length - 1);

        ObjectDataSource1.SelectParameters["StateInStr"].DefaultValue = StatusStr;
    }
}
