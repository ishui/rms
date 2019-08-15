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

public partial class LocaleVise_LocaleViseDtl : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["ViseCostCode"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);
        }

    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        if (((RmsPM.Web.UserControls.InputCostBudgetDtl)this.FormView1.Row.FindControl("InputCostBudgetDtl1")).CostCode == "")
        {
            ((HtmlGenericControl)this.FormView1.Row.FindControl("CostMsgSpan")).InnerHtml = "<font color='red'>必填</font>";
            e.Cancel = true;
        }
        e.Values["ViseCode"] = Request["ViseCode"] + "";
    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        if ("FromFlow" == Request.QueryString["Type"] + "")
        {
            Response.Write("<script>window.opener.refresh();window.close();</script>");
        }
        else
        {
            Response.Write(string.Format("<script>window.opener.location=\"LocaleViseInfo.aspx?ViseCode={0}&ProjectCode={1}\";window.close();</script>", Request.QueryString["ViseCode"] + "", Request.QueryString["ProjectCode"] + ""));
        }
        Response.End();
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener.WinReload();window.close();</script>");
        Response.End();
    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.WinReload();window.close();</script>");
        Response.End();
    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        if (((RmsPM.Web.UserControls.InputCostBudgetDtl)this.FormView1.Row.FindControl("InputCostBudgetDtl1")).CostCode == "")
        {
            ((HtmlGenericControl)this.FormView1.Row.FindControl("CostMsgSpan")).InnerHtml = "<font color='red'>必填</font>";
            e.Cancel = true;
        }
    }
}
