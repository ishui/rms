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

public partial class RmsOA_ZC_AssetTransfer : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                if (Request.QueryString["Type"].Equals("Add"))
                {
                    this.FormView1.ChangeMode(FormViewMode.Insert);
                }
            }
            if (FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                //如果单据不是申请状态，则控制修改/删除/提交/作废等按钮
                RmsOA.BFL.GK_OA_AssetTransferBFL bfl = new RmsOA.BFL.GK_OA_AssetTransferBFL();
                RmsOA.MODEL.GK_OA_AssetTransferModel model = new RmsOA.MODEL.GK_OA_AssetTransferModel();
                model = bfl.GetGK_OA_AssetTransfer(Convert.ToInt32(Request["Code"]));
                if (model.Status != "0")
                {
                    HtmlInputButton btnRequisition = ((HtmlInputButton)this.FormView1.Row.FindControl("btnRequisition"));
                    btnRequisition.Visible = false;

                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                    this.FormView1.Row.FindControl("DeleteButton").Visible = false;
                    this.FormView1.Row.FindControl("btnBankOut").Visible = false;
                }
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


    protected void btnBankOut_Click(object sender, EventArgs e)
    {
        try
        {
            RmsOA.BFL.GK_OA_AssetTransferBFL bfl = new RmsOA.BFL.GK_OA_AssetTransferBFL();
            bfl.ModifyBankOutAuditing(int.Parse(this.FormView1.DataKey.Value.ToString()));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
            throw ex;
        }
    }

    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        TextBox tbxPrice = (TextBox)(this.FormView1.Row.FindControl("OriginalPriceTextBox"));
        if (tbxPrice.Text.Trim().Equals(String.Empty))
        {
            e.Values["OriginalPrice"] = "0";
        }
        e.Values["Status"] = "0";
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight("330202"))
            {
                Button btnEdit = (Button)(this.FormView1.Row.FindControl("EditButton"));
                btnEdit.Visible = false;
            }
            if (!user.HasRight("330203"))
            {
                Button btnDelete = (Button)(this.FormView1.Row.FindControl("DeleteButton"));
                btnDelete.Visible = false;
            }
            if (!user.HasRight("330204"))
            {
                HtmlInputButton btnSubmit = (HtmlInputButton)(this.FormView1.Row.FindControl("btnRequisition"));
                btnSubmit.Visible = false;
            }
            if (!user.HasRight("330205"))
            {
                Button btnBankOut = (Button)(this.FormView1.Row.FindControl("btnBankOut"));
                btnBankOut.Visible = false;
            }
            WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.FormView1.Row.FindControl("WorkFlowList1");
            work.ProcedureNameAndApplicationCodeList = "'固定资产转移" + this.FormView1.DataKey.Value.ToString() + "'";
            work.DataBound();

        }

    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        TextBox tbxPrice = (TextBox)(this.FormView1.Row.FindControl("OriginalPriceTextBox"));
        if (tbxPrice.Text.Trim().Equals(String.Empty))
        {
            e.NewValues["OriginalPrice"] = "0";
        }
    }
}
