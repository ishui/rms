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

public partial class RmsOA_GK_OA_OutFileSignEdit : PageBase
{
    private RmsPM.Web.UserControls.AttachMentAdd ucadd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Code"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            if (FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                if (user.HasRight("310202"))
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                }
                if (user.HasRight("310203"))
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = false;
                }

                if (user.HasRight("310204"))
                {
                    this.FormView1.Row.FindControl("btnRequisition").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("btnRequisition").Visible = false;
                }

                if (user.HasRight("310205"))
                {
                    this.FormView1.Row.FindControl("btnBankOut").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("btnBankOut").Visible = false;
                }
                WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.FormView1.Row.FindControl("WorkFlowList1");
                work.ProcedureNameAndApplicationCodeList = "'文件签发单" + this.FormView1.DataKey.Value.ToString() + "'";
                work.DataBound();

                //如果单据不是申请状态，则控制修改/删除/提交/作废等按钮

                RmsOA.BFL.GK_OA_OutFileSignBFL bfl = new RmsOA.BFL.GK_OA_OutFileSignBFL();
                RmsOA.MODEL.GK_OA_OutFileSignModel model = new RmsOA.MODEL.GK_OA_OutFileSignModel();
                model = bfl.GetGK_OA_OutFileSign(Convert.ToInt32(Request["Code"]));
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
        this.ucadd = (RmsPM.Web.UserControls.AttachMentAdd)this.FormView1.Row.FindControl("Attachmentadd1");
        ucadd.SaveAttachMent(e.ReturnValue.ToString());
    }


    protected void btnBankOut_Click(object sender, EventArgs e)
    {
        try
        {
            RmsOA.BFL.GK_OA_OutFileSignBFL bfl = new RmsOA.BFL.GK_OA_OutFileSignBFL();
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
        if (this.FormView1.CurrentMode == FormViewMode.Insert)
        {
            CheckBoxList CheckBoxLisUrgent = (CheckBoxList)this.FormView1.Row.FindControl("CheckBoxLisUrgent");
            string strCheckBoxLisUrgent = GetListGroupSelectedValues(CheckBoxLisUrgent);
            e.Values["Urgent"] = strCheckBoxLisUrgent;

            CheckBoxList CheckBoxListSecret = (CheckBoxList)this.FormView1.Row.FindControl("CheckBoxListSecret");
            string strCheckBoxListSecret = GetListGroupSelectedValues(CheckBoxListSecret);
            e.Values["Secret"] = strCheckBoxListSecret;
        }

        e.Values["Status"] = "0";
        e.Values["SystemCode"] = "";
    }

    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.Edit)
        {
            CheckBoxList CheckBoxLisUrgent = (CheckBoxList)this.FormView1.Row.FindControl("CheckBoxLisUrgent");
            string strCheckBoxLisUrgent = GetListGroupSelectedValues(CheckBoxLisUrgent);
            e.NewValues["Urgent"] = strCheckBoxLisUrgent;

            CheckBoxList CheckBoxListSecret = (CheckBoxList)this.FormView1.Row.FindControl("CheckBoxListSecret");
            string strCheckBoxListSecret = GetListGroupSelectedValues(CheckBoxListSecret);
            e.NewValues["Secret"] = strCheckBoxListSecret;
        }
    }

    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Label tbxUnit = (Label)this.FormView1.Row.FindControl("UnitLabel");
            tbxUnit.Text = RmsPM.BLL.SystemRule.GetUnitName(tbxUnit.Text);

            Label tbxUnit1 = (Label)this.FormView1.Row.FindControl("UnitLabel1");
            tbxUnit1.Text = RmsPM.BLL.SystemRule.GetUnitName(tbxUnit1.Text);
        }
        if (this.FormView1.CurrentMode == FormViewMode.Edit)
        {

            HtmlInputHidden tbxUnitCode = (HtmlInputHidden)this.FormView1.Row.FindControl("txtUnit");
            HtmlInputText tbxUnitName = (HtmlInputText)this.FormView1.Row.FindControl("txtUnitName");
            tbxUnitName.Value = RmsPM.BLL.SystemRule.GetUnitName(tbxUnitCode.Value);

            HtmlInputHidden tbxUnitCode1 = (HtmlInputHidden)this.FormView1.Row.FindControl("txtUnit1");
            HtmlInputText tbxUnitName1 = (HtmlInputText)this.FormView1.Row.FindControl("txtUnitName1");
            tbxUnitName1.Value = RmsPM.BLL.SystemRule.GetUnitName(tbxUnitCode1.Value);

            CheckBoxList CheckBoxLisUrgent = (CheckBoxList)this.FormView1.Row.FindControl("CheckBoxLisUrgent");
            TextBox UrgentTextBox = (TextBox)this.FormView1.Row.FindControl("UrgentTextBox");
            SetListGroupSelectedValues(CheckBoxLisUrgent, UrgentTextBox.Text);

            CheckBoxList CheckBoxListSecret = (CheckBoxList)this.FormView1.Row.FindControl("CheckBoxListSecret");
            TextBox SecretTextBox = (TextBox)this.FormView1.Row.FindControl("SecretTextBox");
            SetListGroupSelectedValues(CheckBoxListSecret, SecretTextBox.Text);
        }
        //User u = (User)Session["User"];
        //RmsPM.BLL.SystemRule.GetUnitListByUserCode(u.UserCode);
    }

    /// <summary>
    /// 设置chkboxlist的选定值 ----可以作为公共方法
    /// </summary>
    /// <param name="cbl"></param>
    /// <param name="selectedValues"></param>
    public static void SetListGroupSelectedValues(CheckBoxList cbl, string selectedValues)
    {
        foreach (string stemp in selectedValues.Split(new char[] { ',' }))
        {
            foreach (ListItem li in cbl.Items)
                if (li.Value == stemp)
                    li.Selected = true;
        }
    }

    /// <summary>
    /// 获取checkboxlist中得值--可以作为公共方法
    /// </summary>
    /// <param name="cbl"></param>
    /// <returns></returns>
    public static string GetListGroupSelectedValues(CheckBoxList cbl)
    {
        string re = "";
        foreach (ListItem li in cbl.Items)
        {
            if (li.Selected)
            {
                if (re != "")
                    re += ",";
                re += li.Value;
            }
        }
        return re;
    }
}
