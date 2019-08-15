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


public partial class RmsOA_CL_CarRegisterEdit : PageBase
{
   protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Car_code"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            if (FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                if (user.HasRight("290102"))
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                }
                if (user.HasRight("290103"))
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = false;
                }

                if (user.HasRight("290201"))
                {
                    this.FormView1.Row.FindControl("AddButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("AddButton").Visible = false;
                }

                //System.Web.UI.WebControls.Button AddButton=(System.Web.UI.WebControls.Button)this.FormView1.Row.FindControl("AddButton");

                //AddButton.Attributes["OnClick"] = "javascript:OpenMiddleWindow('CL_CarMaintenanceEdit.aspx?ActType=add&Car_Code=" + Request["Car_code"] + "','CarMaintenanceEdit');";
                //WorkFlowControl_WorkFlowList work = (WorkFlowControl_WorkFlowList)this.FormView1.Row.FindControl("WorkFlowList1");
                //work.ProcedureNameAndApplicationCodeList = "'人力资源需求审批" + this.FormView1.DataKey.Value.ToString() + "'";
                //work.DataBound();

                this.ObjectDataSource2.SelectParameters.Clear();
                this.ObjectDataSource2.SelectParameters.Add("SortColumns", "");
                this.ObjectDataSource2.SelectParameters.Add("StartRecord", "0");
                this.ObjectDataSource2.SelectParameters.Add("MaxRecords", "-1");
                this.ObjectDataSource2.SelectParameters.Add("Car_CodeEqual", Request["Car_code"] + "");
                
            }
            if (this.FormView1.CurrentMode == FormViewMode.Insert)
            {
                TextBox tbxID = (TextBox)(this.FormView1.Row.FindControl("IndexNum"));
                tbxID.Text = this.ConstructRule();
            }

            
        }
        if (!string.IsNullOrEmpty(Request.QueryString["Inserted"]))
        {
            Response.Write("<script>window.opener.location.reload();</script>");
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
        ObjectDataSource1.SelectParameters["Car_Code"].DefaultValue = e.ReturnValue.ToString();
        ObjectDataSource2.SelectParameters["Car_CodeEqual"].DefaultValue = e.ReturnValue.ToString();
        Response.Redirect(string.Format("CL_CarRegisterEdit.aspx?Car_code={0}&Inserted=true", e.ReturnValue));
    }

    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        //e.Values["Status"] = "0";
        //e.Values["Field1"] = "";
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


    public string ConstructRule()
    {
        return string.Format("XZ-{0:00}{1:00}-",DateTime.Now.Year - 2000,DateTime.Now.Month);
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.Insert)
        {
            TextBox tbxID = (TextBox)(this.FormView1.Row.FindControl("IndexNum"));
            tbxID.Text = this.ConstructRule();
        }
        
    }
    protected void FormView1_DataBinding(object sender, EventArgs e)
    {
    }
}
