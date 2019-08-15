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

public partial class DesignChange_DesignChangeList : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!user.HasRight("2401"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }
            if (!user.HasRight("2403"))
            {
                this.btnNew1.Visible = false;
                this.btnNew2.Visible = false;
            }
            //内外设计变更是野风特殊需求

            if (this.up_sPMNameLower == "yefengpm")
            {
                this.btnNew1.Visible = true;
                this.btnNew2.Visible = true;
                this.btnNew3.Visible = false;
                this.GridView1.Columns[7].Visible = true;
            }
            else 
            {

                this.btnNew1.Visible = false;
                this.btnNew2.Visible = false;
                this.btnNew3.Visible = true;
                this.GridView1.Columns[7].Visible = false;
                this.DropDownList1.Visible = false;
                this.ForDropDownList1.Visible = false;
            
            }

            this.StatusCheckBoxList.SelectedIndex = this.StatusCheckBoxList.Items.IndexOf(this.StatusCheckBoxList.Items.FindByValue(Request["State"] + ""));
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
                StatusStr += "'" + ItemStatus.Value+"',";
            }
        }
        if (StatusStr.Length > 0)
            StatusStr = StatusStr.Substring(0, StatusStr.Length - 1);

        ObjectDataSource1.SelectParameters["StateInStr"].DefaultValue = StatusStr;
    }

    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (this.GridView1.PageIndex == 0)
        {
            System.Collections.Generic.List<TiannuoPM.MODEL.DesignChangeModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.DesignChangeModel>)e.ReturnValue;
            this.lblRecordCount.Text = lst.Count.ToString();
        }
    }
}
