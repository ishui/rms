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
using RmsReport;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using System.IO;

public partial class RmsOA_GK_OA_FileChangeEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Code"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            if (FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                if (user.HasRight("320302"))
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                }
                if (user.HasRight("320303"))
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = false;
                }

                if (user.HasRight("320304"))
                {
                    this.FormView1.Row.FindControl("btnRequisition").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("btnRequisition").Visible = false;
                }

                if (user.HasRight("320305"))
                {
                    this.FormView1.Row.FindControl("btnBankOut").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("btnBankOut").Visible = false;
                }
                WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.FormView1.Row.FindControl("WorkFlowList1");
                work.ProcedureNameAndApplicationCodeList = "'文件更改审批" + this.FormView1.DataKey.Value.ToString() + "'";
                work.DataBound();


                //如果单据不是申请状态，则控制修改/删除/提交/作废等按钮

                RmsOA.BFL.GK_OA_FileChangeBFL bfl = new RmsOA.BFL.GK_OA_FileChangeBFL();
                RmsOA.MODEL.GK_OA_FileChangeModel model = new RmsOA.MODEL.GK_OA_FileChangeModel();
                model = bfl.GetGK_OA_FileChange(Convert.ToInt32(Request["Code"]));
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
            RmsOA.BFL.GK_OA_FileChangeBFL bfl = new RmsOA.BFL.GK_OA_FileChangeBFL();
            bfl.ModifyBankOutAuditing(int.Parse(this.FormView1.DataKey.Value.ToString()));

            Response.Write("<script>window.opener.location.reload();window.close();</script>");
 
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
        e.Values["Status"] = "0";
        e.Values["SystemCode"] = "GKFC-JL-CX-420105";
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Label tbxUnit = (Label)this.FormView1.Row.FindControl("UnitLabel");
            tbxUnit.Text = RmsPM.BLL.SystemRule.GetUnitName(tbxUnit.Text);
        }
        if (this.FormView1.CurrentMode == FormViewMode.Edit)
        {

            HtmlInputHidden tbxUnitCode = (HtmlInputHidden)this.FormView1.Row.FindControl("txtUnit");
            HtmlInputText tbxUnitName = (HtmlInputText)this.FormView1.Row.FindControl("txtUnitName");
            tbxUnitName.Value = RmsPM.BLL.SystemRule.GetUnitName(tbxUnitCode.Value);
        }
    }
}
