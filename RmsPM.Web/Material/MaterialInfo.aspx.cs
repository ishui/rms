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
using Rms.Web;

public partial class Material_MaterialInfo : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request["MaterialCode"] + "" != "")
                this.ViewState["MaterialCode"] = Request["MaterialCode"] + "";
            if (this.ViewState["MaterialCode"] == null)
            {
                FormView1.ChangeMode(FormViewMode.Insert);
            }

            else
            {
                string MaterialCode = Request.QueryString["MaterialCode"] + "";
                ArrayList ar = user.GetResourceRight(MaterialCode, "Material");
               if (!ar.Contains("150101"))
               {
                   Response.Redirect("../RejectAccess.aspx");
                   Response.End();
               }
               /* 
               if (!ar.Contains("150102"))
               {
                   ((Button)this.FormView1.Row.FindControl("btnModify")).Visible = false;
               }
               if (!ar.Contains("150105"))
               {
                   ((Button)this.FormView1.Row.FindControl("btnModify")).Visible = false;
               }*/
                if (!ar.Contains("150103"))
                {
                    ((Button)this.FormView1.Row.FindControl("btnModify")).Visible = false;
                }
                if (!ar.Contains("150104"))
                {
                    ((Button)this.FormView1.Row.FindControl("btnDelete")).Visible = false;
                }
            }
        }
    }

    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Write("<script>window.opener.location = window.opener.location;</script>");
        Response.Write(string.Format("<script>window.location='MaterialInfo.aspx?MaterialCode={0}';</script>", e.Values["MaterialCode"].ToString()));
        //Response.Redirect(string.Format("<script>window.location=MaterialInfo.aspx?MaterialCode={0};</script>", e.Values["MaterialCode"].ToString()));
        
    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "删除材料出错：" + e.Exception.Message));
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
        e.Values["MaterialCode"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("MaterialCode");
        e.Cancel = InvalidInput();
       // e.Values["MaterialCode"] = Request["ProjectCode"] + "";
        if (!user.HasTypeOperationRight("150102", ((RmsPM.Web.UserControls.InputSystemGroup)this.FormView1.Row.FindControl("InputSystemGroup")).Value) && !e.Cancel)
        {   
            e.Cancel = true;
            Response.Write("<script>alert(\"您不能操作这类材料\");</script>");
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
        e.Cancel = InvalidInput();
    }
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
        this.ViewState["MaterialCode"] = e.ReturnValue.ToString();
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
                btnDelete.Attributes["OnClick"] = "javascript:return confirm('确实要删除当前材料吗？')";
     

        }
        /*
       //如果有领料,则不能修改名称
        if (RmsPM.BLL.ConvertRule.ToDecimal(Request.QueryString["OutQty"]) != 0 && FormView1.CurrentMode == FormViewMode.Edit)
        {
            
            TextBox MaterialNameTextBox = ((TextBox)this.FormView1.Row.FindControl("MaterialNameTextBox"));
            if (MaterialNameTextBox != null)
                MaterialNameTextBox.ReadOnly = true;

        }
        */
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
