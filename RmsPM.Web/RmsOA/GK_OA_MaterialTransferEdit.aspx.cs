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

public partial class RmsOA_GK_OA_MaterialTransferEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                if (Request.QueryString["Type"].Equals("Add"))
                {
                    this.AccountFormView.ChangeMode(FormViewMode.Insert);
                    return;
                }
            }
            //if (AccountFormView.CurrentMode.Equals(FormViewMode.ReadOnly))
            //{
            //    if (user.HasRight("050103"))
            //    {
            //        this.AccountFormView.Row.FindControl("EditButton").Visible = true;
            //        this.AccountFormView.Row.FindControl("DeleteButton").Visible = true;
            //    }
            //    else
            //    {
            //        this.AccountFormView.Row.FindControl("EditButton").Visible = false;
            //        this.AccountFormView.Row.FindControl("DeleteButton").Visible = false;
            //    }
            //    //WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.AccountFormView.Row.FindControl("WorkFlowList1");
            //    //work.ProcedureNameAndApplicationCodeList = "'资产转移" + this.AccountFormView.DataKey.Value.ToString() + "'";
            //    //work.DataBound();
            //}

        }

    }

    protected void btnBankOut_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    RmsOA.BFL.GK_OA_CapitalAssertAcountBFL bfl = new RmsOA.BFL.GK_OA_CapitalAssertAcountBFL();
        //    bfl.ModifyBankOutAuditing(int.Parse(this.AccountFormView.DataKey.Value.ToString()));
        //}
        //catch (Exception ex)
        //{
        //    ApplicationLog.WriteLog(this.ToString(), ex, "");
        //    Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
        //    throw ex;
        //}
    }


    protected void FormViewObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        FormViewObjectDataSource.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
    }

    protected void AccountFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["Status"] = "0";
    }
    protected void AccountFormView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        //Response.Write("<script>window.opener.location.reload();window.close();</script>");
        //Response.End();
    }
    protected void AccountFormView_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        Response.End();
    }
    protected void AccountFormView_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        Response.End();
    }
}
