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

public partial class RmsOA_GK_OA_EquipmentMaintainApplyEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                if (Request.QueryString["Type"].Equals("Add"))
                {
                    this.EquipmentFormView.ChangeMode(FormViewMode.Insert);
                }
            }

            if (EquipmentFormView.CurrentMode == FormViewMode.ReadOnly)
            {
                //如果单据不是申请状态，则控制修改/删除/提交/作废等按钮
                RmsOA.BFL.GK_OA_EquipmentMaintainApplyBFL bfl = new RmsOA.BFL.GK_OA_EquipmentMaintainApplyBFL();
                RmsOA.MODEL.GK_OA_EquipmentMaintainApplyModel model = new RmsOA.MODEL.GK_OA_EquipmentMaintainApplyModel();
                model = bfl.GetGK_OA_EquipmentMaintainApply(Convert.ToInt32(Request["Code"]));
                if (model.State!= "0")
                {
                    HtmlInputButton btnRequisition = ((HtmlInputButton)this.EquipmentFormView.Row.FindControl("btnRequisition"));
                    btnRequisition.Visible = false;

                    this.EquipmentFormView.Row.FindControl("EditButton").Visible = false;
                    this.EquipmentFormView.Row.FindControl("DeleteButton").Visible = false;
                    this.EquipmentFormView.Row.FindControl("btnBankOut").Visible = false;
                }
            }
        }
    }

    protected void btnBankOut_Click(object sender, EventArgs e)
    {
        try
        {
            RmsOA.BFL.GK_OA_EquipmentMaintainApplyBFL bfl = new RmsOA.BFL.GK_OA_EquipmentMaintainApplyBFL();
            bfl.ModifyBankOutAuditing(int.Parse(this.EquipmentFormView.DataKey.Value.ToString()));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
            throw ex;
        }
    }

    protected void EquipmentFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["State"] = "0";

        RmsPM.Web.UserControls.InputUnit unit = (RmsPM.Web.UserControls.InputUnit)(this.EquipmentFormView.Row.FindControl("DeptTextBox"));
        if (unit.Text == "")
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "请选择部门"));
            e.Cancel = true;
        }
    }
    protected void FormViewObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        FormViewObjectDataSource.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
    }
    protected void EquipmentFormView_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        Response.End();
    }
    protected void EquipmentFormView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();</script>");
        this.EquipmentFormView.ChangeMode(FormViewMode.ReadOnly);
    }
    protected void EquipmentFormView_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }
    protected void EquipmentFormView_DataBound(object sender, EventArgs e)
    {
        if (this.EquipmentFormView.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight("330102"))
            {
                Button btEdit = (Button)(this.EquipmentFormView.Row.FindControl("EditButton"));
                btEdit.Visible = false;
            }
            if (!user.HasRight("330103"))
            {
                Button btDelete = (Button)(this.EquipmentFormView.Row.FindControl("DeleteButton"));
                btDelete.Visible = false;
            }
            if (!user.HasRight("330104"))
            {
                HtmlInputButton btSubmit = (HtmlInputButton)(this.EquipmentFormView.Row.FindControl("btnRequisition"));
                btSubmit.Visible = false;
            }
            if (!user.HasRight("330105"))
            {
                Button btBankOut = (Button)(this.EquipmentFormView.Row.FindControl("btnBankOut"));
                btBankOut.Visible = false;
            }
            WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.EquipmentFormView.Row.FindControl("WorkFlowList1");
            work.ProcedureNameAndApplicationCodeList = "'设备维护申请" + this.EquipmentFormView.DataKey.Value.ToString() + "'";
            work.DataBound();


            HiddenField userHF = (HiddenField)this.EquipmentFormView.Row.FindControl("UserHiddenField");
            Label labUserCode = (Label)this.EquipmentFormView.Row.FindControl("UserCodeLabel");
            labUserCode.Text = RmsPM.BLL.SystemRule.GetUserName(userHF.Value);
        }

        if (this.EquipmentFormView.CurrentMode == FormViewMode.Edit)
        {
            HiddenField ruhf = (HiddenField)this.EquipmentFormView.Row.FindControl("UserHiddenField");
            Label rulb = (Label)this.EquipmentFormView.Row.FindControl("UserCodeLabel");
            rulb.Text = RmsPM.BLL.SystemRule.GetUserName(ruhf.Value);

        }

        if (this.EquipmentFormView.CurrentMode == FormViewMode.Insert)
        {
            HiddenField hfUser = (HiddenField)this.EquipmentFormView.Row.FindControl("UserHiddenField");
            Label lblUser = (Label)this.EquipmentFormView.Row.FindControl("UserCodeLabel");

            User u = (User)Session["User"];
            hfUser.Value = u.UserCode;
            lblUser.Text = RmsPM.BLL.SystemRule.GetUserName(u.UserCode);

        }
    }
    protected void EquipmentFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        RmsPM.Web.UserControls.InputUnit unit = (RmsPM.Web.UserControls.InputUnit)(this.EquipmentFormView.Row.FindControl("DeptTextBox"));
        if (unit.Text == "")
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "请选择部门"));
            e.Cancel = true;
        }
    }
}
