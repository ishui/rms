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

public partial class RmsOA_GK_OA_CapitalAssertAcount : PageBase
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
        }

        if (AccountFormView.CurrentMode == FormViewMode.ReadOnly)
        {
             //如果单据不是申请状态，则控制修改/删除/提交/作废等按钮
                RmsOA.BFL.GK_OA_CapitalAssertAcountBFL bfl = new RmsOA.BFL.GK_OA_CapitalAssertAcountBFL();
                RmsOA.MODEL.GK_OA_CapitalAssertAcountModel model = new RmsOA.MODEL.GK_OA_CapitalAssertAcountModel();
                model = bfl.GetGK_OA_CapitalAssertAcount(Convert.ToInt32(Request["Code"]));
                if (model.Status != "0")
                {
                    HtmlInputButton btnRequisition = ((HtmlInputButton)this.AccountFormView.Row.FindControl("btnRequisition"));
                    btnRequisition.Visible = false;

                    this.AccountFormView.Row.FindControl("EditButton").Visible = false;
                    this.AccountFormView.Row.FindControl("DeleteButton").Visible = false;
                    this.AccountFormView.Row.FindControl("btnBankOut").Visible = false;
                }
            }
        }
 
    protected void btnBankOut_Click(object sender, EventArgs e)
    {
        try
        {
            RmsOA.BFL.GK_OA_CapitalAssertAcountBFL bfl =new RmsOA.BFL.GK_OA_CapitalAssertAcountBFL();
            bfl.ModifyBankOutAuditing(int.Parse(this.AccountFormView.DataKey.Value.ToString()));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
            throw ex;
        }
    }


    protected void FormViewObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        FormViewObjectDataSource.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
    }

    protected void AccountFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        DropDownList ddlType = (DropDownList)(this.AccountFormView.Row.FindControl("TypeDropDownList"));
        TextBox tbx = (TextBox)(this.AccountFormView.Row.FindControl("PriceTextBox"));
        TextBox tbxCount = (TextBox)(this.AccountFormView.Row.FindControl("BuyCountTextBox"));
        if (tbx.Text.Trim().Equals(String.Empty))
        {
            e.Values["Price"] = "0";
        }
        if (tbxCount.Text.Trim().Equals(String.Empty))
        {
            e.Values["BuyCount"] = "0";
        }

        if(!ddlType.SelectedIndex.Equals(0))
        {
            e.Values["Type"] = ddlType.SelectedItem.Text;
        }
        e.Values["Status"] = "0";

        RmsPM.Web.UserControls.InputUnit unit = (RmsPM.Web.UserControls.InputUnit)(this.AccountFormView.Row.FindControl("DeptTextBox"));
        if (unit.Text == "")
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "请选择部门"));
            e.Cancel = true;
        }
    }
    protected void AccountFormView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }
    protected void AccountFormView_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }
    protected void AccountFormView_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        Response.End();
    }
    protected void AccountFormView_DataBound(object sender, EventArgs e)
    {
        if (this.AccountFormView.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight("330302"))
            {
                Button btEdit = (Button)(this.AccountFormView.Row.FindControl("EditButton"));
                btEdit.Visible = false;
            }
            if (!user.HasRight("320303"))
            {
                Button btDelete = (Button)(this.AccountFormView.Row.FindControl("DeleteButton"));
                btDelete.Visible = false;
            }
            if (!user.HasRight("320304"))
            {
                HtmlInputButton btSubmit = (HtmlInputButton)(this.AccountFormView.Row.FindControl("btnRequisition"));
                btSubmit.Visible = false;
            }
            if (!user.HasRight("320305"))
            {
                Button btBankOut = (Button)(this.AccountFormView.Row.FindControl("btnBankOut"));
                btBankOut.Visible = false;
            }
            WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.AccountFormView.Row.FindControl("WorkFlowList1");
            work.ProcedureNameAndApplicationCodeList = "'固定资产台帐" + this.AccountFormView.DataKey.Value.ToString() + "'";
            work.DataBound();
        }
    }
    protected void AccountFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        TextBox tbx = (TextBox)(this.AccountFormView.Row.FindControl("PriceTextBox"));
        TextBox tbxCount = (TextBox)(this.AccountFormView.Row.FindControl("BuyCountTextBox"));
        if (tbx.Text.Trim().Equals(String.Empty))
        {
            e.NewValues["Price"] = "0";
        }
        if (tbxCount.Text.Trim().Equals(String.Empty))
        {
            e.NewValues["BuyCount"] = "0";
        }

        RmsPM.Web.UserControls.InputUnit unit = (RmsPM.Web.UserControls.InputUnit)(this.AccountFormView.Row.FindControl("DeptTextBox"));
        if (unit.Text == "")
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "请选择部门"));
            e.Cancel = true;
        }
    }
}
