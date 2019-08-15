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
using Infragistics.WebUI.WebDataInput;

public partial class RmsOA_CL_OilEdit : PageBase
{
   protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Code"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            if (FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                if (user.HasRight("290202"))
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                }
                if (user.HasRight("290203"))
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = false;
                }
                WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.FormView1.Row.FindControl("WorkFlowList1");
                //work.ProcedureNameAndApplicationCodeList = "'人力资源需求审批" + this.FormView1.DataKey.Value.ToString() + "'";
                //work.DataBound();
            }

            
        }
    }

    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Write("<script>window.opener.submit= function(){};window.opener.location.reload();</script>");
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
        //e.Values["Status"] = "0";
        //e.Values["Field1"] = "";
        decimal decFirstMil = RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)this.FormView1.Row.FindControl("WebnumericeditFirstMil")).ValueDecimal);
        decimal decThisMil = RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)this.FormView1.Row.FindControl("WebnumericeditThisMil")).ValueDecimal);
        e.Values["FactMil"] = Math.Round(RmsPM.BLL.ConvertRule.ToDecimal((double)decThisMil - (double)decFirstMil), 2);
        
    }
    protected void btnBankOut_Click(object sender, EventArgs e)
    {
        try
        {
            RmsOA.BFL.GK_OA_ManpowerNeedBFL bfl = new RmsOA.BFL.GK_OA_ManpowerNeedBFL();
            bfl.ModifyBankOutAuditing(int.Parse(this.FormView1.DataKey.Value.ToString()));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "作废出错：" + ex.Message));
            throw ex;
        }
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.Insert)
        {
            TextBox tbxID = (TextBox)(this.FormView1.Row.FindControl("IndexNum"));
            tbxID.Text = this.ConstructRule();
        }
    }
    public string ConstructRule()
    {
        return string.Format("XZ-{0:00}{1:00}-", DateTime.Now.Year - 2000, DateTime.Now.Month);
    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        decimal decFirstMil = RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)this.FormView1.Row.FindControl("WebnumericeditFirstMil")).ValueDecimal);
        decimal decThisMil = RmsPM.BLL.ConvertRule.ToDecimal(((WebNumericEdit)this.FormView1.Row.FindControl("WebnumericeditThisMil")).ValueDecimal);
        e.NewValues["FactMil"] = Math.Round(RmsPM.BLL.ConvertRule.ToDecimal((double)decThisMil - (double)decFirstMil), 2);
    }
}
