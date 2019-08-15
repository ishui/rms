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
using RmsOA.MODEL;

public partial class RmsOA_YF_AssetMainEdit : PageBase
{
    private RmsOA.BFL.YF_AssetFacade aBFL = new  RmsOA.BFL.YF_AssetFacade();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.QueryString["Type"].Equals("Add"))
        {
            this.FormView1.ChangeMode(FormViewMode.Insert);
        }
        if(!String.IsNullOrEmpty(Request.QueryString["Code"]))
        {
            ViewState["Code"] = Request.QueryString["Code"];
        }
    }
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["UserCode"] = user.UserName;
        e.Values["ManageCode"] = Int32.Parse(Request.QueryString["ManageCode"]);
    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        this.RefreshParentPage();
        this.ClosePage();
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        this.RefreshParentPage();
    }
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
        ViewState["Code"] = e.ReturnValue.ToString();
        YF_AssetMainRecordBFL amrBFL = new YF_AssetMainRecordBFL();
        YF_AssetMainRecordModel amrModel = new YF_AssetMainRecordModel();
        amrModel.Code = Int32.Parse(e.ReturnValue.ToString());
        amrBFL.Insert(amrModel);
        this.RefreshParentPage();
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        RmsOA.BFL.AssetModel aModel = new RmsOA.BFL.AssetModel();
        Label lblCodeNO = (Label)(this.FormView1.Row.FindControl("CodeNOLabel"));
        Label lblApplyer = (Label)(this.FormView1.Row.FindControl("UserCodeLabel"));
        aModel = aBFL.GetAssetName(Request.QueryString["ManageCode"]);
        if(!aModel.Equals(null))
        {
            lblCodeNO.Text = aModel.SortCode;
        }
        lblApplyer.Text = user.UserName;
        if(this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
             this.CheckFormViewButtonRight();
        }
    }

    public void CheckFormViewButtonRight()
    {
        if(!user.HasRight(YF_AssetMainApplyRight.Edit))
        {
            Button btnEdit = (Button)(this.FormView1.Row.FindControl("EditButton"));
            btnEdit.Visible = false;
        }
        if(!user.HasRight(YF_AssetMainApplyRight.Delete))
        {
            Button btnDel = (Button)(this.FormView1.Row.FindControl("DeleteButton"));
            btnDel.Visible = false;
        }
    }

    public void ClosePage()
    {
        Response.Write("<script>window.close();</script>");
    }
    public void RefreshParentPage()
    {
        Response.Write("<script>window.opener.location.reload();</script>");
    }
}
