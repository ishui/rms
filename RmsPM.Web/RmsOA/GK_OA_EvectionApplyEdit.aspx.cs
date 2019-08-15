/***************************************************************
 * @Author: Yiwl
 * @CreateDate: 2006-11-15
 ***************************************************************/
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

public partial class RmsOA_GK_OA_EvectionApplyEdit : PageBase
{
    private RmsPM.Web.UserControls.InputUnit ucDept;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                if (Request.QueryString["Type"].Equals("Add"))
                {
                    this.EvectionFormView.ChangeMode(FormViewMode.Insert);
                    return;
                }
            }

            if (EvectionFormView.CurrentMode == FormViewMode.ReadOnly)
            {
                //如果单据不是申请状态，则控制修改/删除/提交/作废等按钮
                RmsOA.BFL.GK_OA_EvectionApplyBFL bfl = new RmsOA.BFL.GK_OA_EvectionApplyBFL();
                RmsOA.MODEL.GK_OA_EvectionApplyModel model = new RmsOA.MODEL.GK_OA_EvectionApplyModel();
                model = bfl.GetGK_OA_EvectionApply(Convert.ToInt32(Request["Code"]));
                if (model.Status != "0")
                {
                    HtmlInputButton btnRequisition = ((HtmlInputButton)this.EvectionFormView.Row.FindControl("btnRequisition"));
                    btnRequisition.Visible = false;

                    this.EvectionFormView.Row.FindControl("EditButton").Visible = false;
                    this.EvectionFormView.Row.FindControl("DeleteButton").Visible = false;
                    this.EvectionFormView.Row.FindControl("btnBankOut").Visible = false;
                }
            }
        }

    }

    public string UserCode
    {
        get
        {
            return this.user.UserCode;
        }
    }

    protected void btnBankOut_Click(object sender, EventArgs e)
    {
        try
        {
            RmsOA.BFL.GK_OA_EvectionApplyBFL bfl = new RmsOA.BFL.GK_OA_EvectionApplyBFL();
            bfl.ModifyBankOutAuditing(int.Parse(this.EvectionFormView.DataKey.Value.ToString()));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
            throw ex;
        }
    }

    protected void EvectionFormView_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        //TextBox tbxUserCount = (TextBox)(this.EvectionFormView.Row.FindControl("UserCountTextBox"));
        TextBox tbxBudgetMoney = (TextBox)(this.EvectionFormView.Row.FindControl("BudgetMoneyTextBox"));
        //if (tbxUserCount.Text.Trim().Equals(String.Empty))
        //{
           e.Values["UserCount"] = "0";
        //}
        if (tbxBudgetMoney.Text.Trim().Equals(String.Empty))
        {
            e.Values["BudgetMoney"] = "0";
        }
        e.Values["Status"] = "0";

        e.Values["ApplyDate"] = System.DateTime.Now.ToString();
    }
    protected void FormViewObjectDataSource_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        FormViewObjectDataSource.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
    }
    protected void EvectionFormView_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        Response.End();
    }
    protected void EvectionFormView_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }
    protected void EvectionFormView_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener.location.reload();window.close();</script>");
        Response.End();
    }
    protected void EvectionFormView_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        //TextBox tbxUserCount = (TextBox)(this.EvectionFormView.Row.FindControl("UserCountTextBox"));
        TextBox tbxBudgetMoney = (TextBox)(this.EvectionFormView.Row.FindControl("BudgetMoneyTextBox"));
        //if (tbxUserCount.Text.Trim().Equals(String.Empty))
        //{
           e.NewValues["UserCount"] = "0";
        //}
        if (tbxBudgetMoney.Text.Trim().Equals(String.Empty))
        {
            e.NewValues["BudgetMoney"] = "0";
        }
    }
    protected void EvectionFormView_DataBound(object sender, EventArgs e)
    {
        if (this.EvectionFormView.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight("320602"))
            {
                Button btEdit = (Button)(this.EvectionFormView.Row.FindControl("EditButton"));
                btEdit.Visible = false;
            }
            if (!user.HasRight("320603"))
            {
                Button btDelete = (Button)(this.EvectionFormView.Row.FindControl("DeleteButton"));
                btDelete.Visible = false;
            }
            if (!user.HasRight("320604"))
            {
                HtmlInputButton btSubmit = (HtmlInputButton)(this.EvectionFormView.Row.FindControl("btnRequisition"));
                btSubmit.Visible = false;
            }
            if (!user.HasRight("320605"))
            {
                Button btBankOut = (Button)(this.EvectionFormView.Row.FindControl("btnBankOut"));
                btBankOut.Visible = false;
            }
            WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.EvectionFormView.Row.FindControl("WorkFlowList1");
            work.ProcedureNameAndApplicationCodeList = "'员工出差申请" + this.EvectionFormView.DataKey.Value.ToString() + "'";
            work.DataBound();


            HiddenField departmentHF = (HiddenField)this.EvectionFormView.Row.FindControl("DepartmentHiddenField");
            Label labDepartmentCode = (Label)this.EvectionFormView.Row.FindControl("DepartmentCodeLabel");
            labDepartmentCode.Text = RmsPM.BLL.SystemRule.GetUnitName(departmentHF.Value);

            HiddenField userHF = (HiddenField)this.EvectionFormView.Row.FindControl("UserHiddenField");
            Label labUserCode = (Label)this.EvectionFormView.Row.FindControl("UserCodeLabel");
            labUserCode.Text = RmsPM.BLL.SystemRule.GetUserName(userHF.Value);
        }

        if (this.EvectionFormView.CurrentMode == FormViewMode.Edit)
        {
            HiddenField ruhf = (HiddenField)this.EvectionFormView.Row.FindControl("UserHiddenField");
            Label rulb = (Label)this.EvectionFormView.Row.FindControl("UserCodeLabel");
            rulb.Text = RmsPM.BLL.SystemRule.GetUserName(ruhf.Value);

            Label runitlbEdit = (Label)this.EvectionFormView.Row.FindControl("DepartmentCodeLabel");
            HiddenField unithfEdit = (HiddenField)this.EvectionFormView.Row.FindControl("DepartmentHiddenField");
            runitlbEdit.Text = RmsPM.BLL.SystemRule.GetUnitName(unithfEdit.Value);
        }

        if (this.EvectionFormView.CurrentMode == FormViewMode.Insert)
        {
            HiddenField hfUser = (HiddenField)this.EvectionFormView.Row.FindControl("UserHiddenField");
            Label lblUser = (Label)this.EvectionFormView.Row.FindControl("UserCodeLabel");

            User u = (User)Session["User"];
            hfUser.Value = u.UserCode;
            lblUser.Text = RmsPM.BLL.SystemRule.GetUserName(u.UserCode);

            Label lblApplySealDate = (Label)this.EvectionFormView.Row.FindControl("ApplySealDateLabel");
            lblApplySealDate.Text = System.DateTime.Now.ToString();

        }
        
    }
   
}
