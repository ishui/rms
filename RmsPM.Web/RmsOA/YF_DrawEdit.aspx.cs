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

using RmsOA.BFL;
using RmsOA.MODEL;

using RmsPM.Web;

public partial class RmsOA_YF_DrawEdit : PageBase
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
        if(!String.IsNullOrEmpty(Request.QueryString["ManageCode"]))
        {
            RmsOA.BFL.YF_AssetFacade af = new RmsOA.BFL.YF_AssetFacade();
            RmsOA.BFL.AssetModel aModel = new RmsOA.BFL.AssetModel();
            aModel = af.GetAssetName(Request.QueryString["ManageCode"]);
            if(!aModel.Equals(null))
            {
                Label lblName = (Label)(this.FormView1.Row.FindControl("NameLabel"));
                lblName.Text= aModel.EquiName;
                Label lblSortNo = (Label)(this.FormView1.Row.FindControl("SortNOLabel"));
                lblSortNo.Text = aModel.SortCode;
            }
        }
        if(this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            this.CheckFormViewButtonRight();
        }
    }

    public void CheckFormViewButtonRight()
    {
        if(!user.HasRight(YF_AssetDrawRight.Edit))
        {
            Button btnEdit = (Button)(this.FormView1.Row.FindControl("EditButton"));
            btnEdit.Visible = false;
        }
        if(!user.HasRight(YF_AssetDrawRight.Delete))
        {
            Button btnDel = (Button)(this.FormView1.Row.FindControl("DeleteButton"));
            btnDel.Visible = false;
        }
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
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
        //Response.Write("<script>self.location.href('YF_DrawEdit?Code="+e.ReturnValue.ToString()+"')</script>");
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
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        RmsPM.Web.UserControls.InputUnit ucUnit = (RmsPM.Web.UserControls.InputUnit)(this.FormView1.Row.FindControl("DrawUnitInputunit"));
        if(String.IsNullOrEmpty(ucUnit.Value))
        {
            Response.Write("<script>window.alert('«Î—°‘Ò…Í«Î≤ø√≈')</script>");
            e.Cancel = true;
            return;
        }
        int code = 0;
        Int32.TryParse(Request.QueryString["ManageCode"],out code);
        e.Values["ManageCode"] = Request.QueryString["ManageCode"];
    }
}
