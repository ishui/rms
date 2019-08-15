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
using RmsOA.BFL;


public partial class RmsOA_YF_AssetMainRecord : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void FormView2_DataBound(object sender, EventArgs e)
    {
        if(this.FormView2.CurrentMode == FormViewMode.ReadOnly)
        {
            if(!user.HasRight(YF_AssetMainApplyRight.Edit))
            {
                Button btnEdit = (Button)(this.FormView2.Row.FindControl("EditButton"));
                btnEdit.Visible = false;
            }
            if(!user.HasRight(YF_AssetMainApplyRight.Delete))
            {
                Button btnDel = (Button)(this.FormView2.Row.FindControl("DeleteButton"));
                btnDel.Visible = false;
            }
        }
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if(this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
             if(!user.HasRight(YF_AssetMainRecordRight.Edit))
             {
                 Button btnEdit = (Button)(this.FormView1.Row.FindControl("EditButton"));
                 btnEdit.Visible = false;
             }
        }
    }
}
