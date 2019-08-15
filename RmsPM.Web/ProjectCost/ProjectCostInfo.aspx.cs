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
using System.Collections.Generic;
using Infragistics.WebUI.WebDataInput;
using RmsPM.DAL;
using TiannuoPM.MODEL;

public partial class ProjectCost_ProjectCostInfo : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request["ProjectCostCode"] + "" != "")
                this.ViewState["ProjectCostCode"] = Request["ProjectCostCode"] + "";
            if (this.ViewState["ProjectCostCode"] == null)
            {
                FormView1.ChangeMode(FormViewMode.Insert);
            }

            else
            {
                string ProjectCostCode = Request.QueryString["ProjectCostCode"] + "";
                ArrayList ar = user.GetResourceRight(ProjectCostCode, "ProjectCost");
                if (!ar.Contains("152101"))
                {
                    Response.Redirect("../RejectAccess.aspx");
                    Response.End();
                }
                if (!ar.Contains("152103"))
                {
                    ((Button)this.FormView1.Row.FindControl("btnModify")).Visible = false;
                }
                if (!ar.Contains("152104"))
                {
                    ((Button)this.FormView1.Row.FindControl("btnDelete")).Visible = false;
                }
            }
        }
    }

    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Write("<script>window.opener.location = window.opener.location;</script>");

    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "删除项目造价出错：" + e.Exception.Message));
        }

        else
        {
            Response.Write("<script>window.opener.location = window.opener.location;window.close();</script>");
            //this.FormView1.DataBind();
            Response.End();
        }
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.opener.location = window.opener.location;</script>");
        //for refresh

    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
       
        e.Values["InputPerson"] = base.user.UserCode;
        e.Values["ProjectCostCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ProjectCostCode");
        e.Values["Money"] = Math.Round(((WebNumericEdit)this.FormView1.Row.FindControl("txtPrice")).ValueDecimal * ((WebNumericEdit)this.FormView1.Row.FindControl("Area")).ValueDecimal,2);
        e.Cancel = InvalidInput();
        // e.Values["ProjectCostCode"] = Request["ProjectCode"] + "";
        if (!user.HasTypeOperationRight("152102", ((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.Row.FindControl("InputSystemGroup")).Value) && !e.Cancel)
        {
            e.Cancel = true;
            Response.Write("<script>alert(\"您不能操作这类项目造价\");</script>");
            return;

        }
    }
    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
    {

        // Response.Write("<script>window.opener.location.reload();</script>");

        //        Response.Write("<script>window.opener.location = window.opener.location;</script>"); 
    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        e.NewValues["InputPerson"] = base.user.UserCode;
        e.NewValues["Money"] = Math.Round(((WebNumericEdit)this.FormView1.Row.FindControl("txtPrice")).ValueDecimal * ((WebNumericEdit)this.FormView1.Row.FindControl("Area")).ValueDecimal, 2);
        e.Cancel = InvalidInput();
    }
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
        this.ViewState["ProjectCostCode"] = e.ReturnValue.ToString();
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        this.FormView1.DataBind();
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Button btnDelete = ((Button)this.FormView1.Row.FindControl("btnDelete"));
            if (btnDelete != null)
                btnDelete.Attributes["OnClick"] = "javascript:return confirm('确实要删除当前项目造价吗？')";
          

        }


    }
    private bool InvalidInput()
    {
        bool ReturnCancel = false;
        if (((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.Row.FindControl("InputSystemGroup")).Value == "")
        {
            ((HtmlGenericControl)this.FormView1.Row.FindControl("GroupSpan")).InnerHtml = "<font color='red'>必填</font>";
            ReturnCancel = true;
        }
        else
        {
            ((HtmlGenericControl)this.FormView1.Row.FindControl("GroupSpan")).InnerHtml = "";
        }
        return ReturnCancel;
    }
}

