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
using System.Data.SqlClient;

using RmsDM.MODEL;
using RmsDM.BFL;
using RmsDM.DAL;
using RmsPM.Web;

public partial class RmsDM_FileTemplateVersionModify : PageBase
{
    private RmsPM.Web.UserControls.AttachMentAdd ucadd;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["FormType"] == "Add")
            {
                this.FormView1.ChangeMode(FormViewMode.Insert);
            }
        }
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>opener.parent.location.reload();</script>");
    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["FileTemplateCode"] = Int32.Parse(Request.QueryString["Tempcode"]);
        DropDownList ddlStatus = (DropDownList)(FormView1.Row.FindControl("IsAvailabilityDropDownList"));
        DropDownList ddlSave = (DropDownList)(FormView1.Row.FindControl("IsPigeonholeDropDownList"));
        DropDownList ddlWorkFlow = (DropDownList)(FormView1.Row.FindControl("DropDownList1"));
        if (ddlSave.SelectedItem.Text.Trim() == "请选择")
        {
            return;
        }
        if (ddlStatus.SelectedItem.Text == "有效")
        {
            int code = Int32.Parse(Page.Request.QueryString["Tempcode"]);
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = FunctionRule.GetConnectionString();
            HandMadeDAL hmd = new HandMadeDAL(sqlConn);
            hmd.UpVersionState(code);
        }
        e.Values["WorkFlowProcedureName"] = ddlWorkFlow.SelectedItem.Text;
        e.Values["IsPigeonhole"] = ddlSave.SelectedItem.Text;
        e.Values["IsAvailability"] = ddlStatus.SelectedItem.Text;
    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {

        DropDownList ddlStatus = (DropDownList)(FormView1.Row.FindControl("IsAvailabilityDropDownList"));
        DropDownList ddlSave = (DropDownList)(FormView1.Row.FindControl("IsPigeonholeDropDownList"));
        DropDownList ddlWorkFlow = (DropDownList)(FormView1.Row.FindControl("DropDownList1"));
        if (ddlStatus.SelectedItem.Text == "有效")
        {
            int code = Int32.Parse(Page.Request.QueryString["Tempcode"]);
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = FunctionRule.GetConnectionString();
            HandMadeDAL hmd = new HandMadeDAL(sqlConn);
            hmd.UpVersionState(code);
        }
        e.NewValues["WorkFlowProcedureName"] = ddlWorkFlow.SelectedItem.Text;
        e.NewValues["IsPigeonhole"] = ddlSave.SelectedItem.Text;
        e.NewValues["IsAvailability"] = ddlStatus.SelectedItem.Text;
    }
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.ucadd = (RmsPM.Web.UserControls.AttachMentAdd)this.FormView1.Row.FindControl("Attachmentadd1");
        ucadd.SaveAttachMent(e.ReturnValue.ToString());
        Response.Write("<script>opener.parent.location.reload();</script>");
        Response.Write("<script>location.href('FileTemplateVersionModify.aspx?FormType=Read&Tempcode=" + Request.QueryString["Tempcode"] + "&Code=" + e.ReturnValue.ToString() + "');</script>");
    }

    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        int code = Int32.Parse(Request.QueryString["code"]);
        FileTemplateVersionModel ftvModel = new FileTemplateVersionModel();
        FileTemplateVersionBFL ftvBFL = new FileTemplateVersionBFL();
        ftvModel.Code = code;
        ftvModel.IsAvailability = "无效";
        ftvBFL.Update(ftvModel);
        Response.Write("<script>opener.parent.location.reload();window.close();</script>");
        Response.End();
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (!user.HasRight("380202"))
        {
            Button btEdit = (Button)(this.FormView1.Row.FindControl("EditButton"));
            btEdit.Visible = false;
        }
        if(!user.HasRight("380203"))
        {
            Button btOut = (Button)(this.FormView1.Row.FindControl("DeleteButton"));
            btOut.Visible = false;
        }
    }
}
