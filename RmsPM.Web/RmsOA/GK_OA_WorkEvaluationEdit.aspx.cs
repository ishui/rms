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

public partial class RmsOA_GK_OA_WorkEvaluationEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Code"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            if (FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                if (user.HasRight("050103"))
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                }
                if (user.HasRight("050103"))
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = false;
                }
                WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.FormView1.Row.FindControl("WorkFlowList1");
                work.ProcedureNameAndApplicationCodeList = "'工作鉴定审批" + this.FormView1.DataKey.Value.ToString() + "'";
                work.DataBound();
            }


        }
    }

    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        Response.End();
    }

    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
    }

    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["Status"] = "0";
        e.Values["Field1"] = "";
    }
    protected void btnBankOut_Click(object sender, EventArgs e)
    {
        try
        {
            RmsOA.BFL.GK_OA_WorkEvaluationBFL bfl = new RmsOA.BFL.GK_OA_WorkEvaluationBFL();
            bfl.ModifyBankOutAuditing(int.Parse(this.FormView1.DataKey.Value.ToString()));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
            throw ex;
        }
    }
}
