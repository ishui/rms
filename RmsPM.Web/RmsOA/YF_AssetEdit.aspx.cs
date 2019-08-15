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
using RmsOA.BFL;

public partial class RmsOA_YF_AssetEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!String.IsNullOrEmpty(Request.QueryString["Type"]))
        {
            if(Request.QueryString["Type"].Equals("Add"))
            {
                this.FormView1.ChangeMode(FormViewMode.Insert);
            }
        }
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if(this.FormView1.CurrentMode == FormViewMode.Insert)
        {
            Label regLable = (Label)(this.FormView1.Row.FindControl("RegisterLabel"));
            regLable.Text = this.user.UserName;
            Label dateLable = (Label)(this.FormView1.Row.FindControl("BookINTimeTextBox"));
            dateLable.Text = DateTime.Now.Date.ToString();
        }
        if(this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
             this.CheckFormViewButtonRight();
        }
    }

    public void CheckFormViewButtonRight()
    {
        if(!user.HasRight(YF_AssetRight.Edit))
        {
            Button btnEdit = (Button)(this.FormView1.Row.FindControl("EditButton"));
            btnEdit.Visible = false;
        }
        if(!user.HasRight(YF_AssetRight.Delete))
        {
            Button btnDel = (Button)(this.FormView1.Row.FindControl("DeleteButton"));
            btnDel.Visible = false;
        }
        if(!user.HasRight(YF_AssetDrawRight.Add))
        {
            Panel btnDrawAdd = (Panel)(this.FormView1.Row.FindControl("btnDraw"));
            btnDrawAdd.Visible = false;
        }
        if(!user.HasRight(YF_AssetTransRight.Add))
        {
            Panel btnDrawAdd = (Panel)(this.FormView1.Row.FindControl("btnTrans"));
            btnDrawAdd.Visible = false;
        }
        if(!user.HasRight(YF_AssetMainApplyRight.Add))
        {
            Panel btnDrawAdd = (Panel)(this.FormView1.Row.FindControl("btnMain"));
            btnDrawAdd.Visible = false;
        }
    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["Register"] = this.user.UserName;
        e.Values["BookINTime"] = DateTime.Now.Date;
    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
         this.RefreshParentPage();
    }

    public void ClosePage()
    {
        Response.Write("<script>window.close();</script>");
    }
    public void RefreshParentPage()
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        RmsPM.Web.UserControls.AttachMentAdd ucAdd = (RmsPM.Web.UserControls.AttachMentAdd)this.FormView1.Row.FindControl("Attachmentadd1");
        ucAdd.SaveAttachMent(e.ReturnValue.ToString());
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        this.RefreshParentPage();
    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        this.RefreshParentPage();
        this.ClosePage();
    }
}
