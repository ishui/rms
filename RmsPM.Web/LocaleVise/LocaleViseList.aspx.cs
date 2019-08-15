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

using System.Collections.Generic;
using TiannuoPM.MODEL;
using RmsPM.Web;

public partial class LocaleVise_LocaleViseList : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!user.HasRight("220101"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }
            if (!user.HasRight("220102"))
            {
                this.btnNew.Visible = false;
            }
            this.ViseStatusCheckBoxList.SelectedIndex = this.ViseStatusCheckBoxList.Items.IndexOf(this.ViseStatusCheckBoxList.Items.FindByValue(Request["ViseStatus"] + ""));

            
        }
        this.ViseContractCodeTextBox.ProjectCode = Request["ProjectCode"] + "";
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        GridView1.DataBind();
    }
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
        string ViseStatusStr = "0";
        foreach (ListItem ItemStatus in this.ViseStatusCheckBoxList.Items)
        {
            if (ItemStatus.Selected)
            {
                 ViseStatusStr += "," + ItemStatus.Value;
            }
        }

        ObjectDataSource1.SelectParameters["ViseStatusInStr"].DefaultValue = ViseStatusStr;
    }
    protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (this.GridView1.PageIndex == 0)
        {
            System.Collections.Generic.List<TiannuoPM.MODEL.LocaleViseModel> lst = (System.Collections.Generic.List<TiannuoPM.MODEL.LocaleViseModel>)e.ReturnValue;
            this.lblRecordCount.Text = lst.Count.ToString();
        }
    }

}
