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
using RmsOA.MODEL;
using RmsOA.BFL;


public partial class RmsOA_YF_AssetTransEdit : PageBase
{
    private RmsOA.BFL.YF_AssetFacade aBFL = new RmsOA.BFL.YF_AssetFacade();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            if(!String.IsNullOrEmpty(Request.QueryString["Type"]))
            {
                if(Request.QueryString["Type"].Equals("Add"))
                {
                    this.FormView1.ChangeMode(FormViewMode.Insert);
                }
            }
        }
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if(this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
              this.CheckFormViewButtonRight();
        }

        Label lblCodeNO = (Label)(this.FormView1.Row.FindControl("CodeNOLabel"));
        if(!String.IsNullOrEmpty(Request.QueryString["ManageCode"]))
        {
            AssetModel aModel = aBFL.GetAssetName(Request.QueryString["ManageCode"]);
            lblCodeNO.Text = aModel.SortCode;
        }


        
    }

    public void CheckFormViewButtonRight()
    {
        if(!user.HasRight(YF_AssetTransRight.Edit))
        {
            Button btnEdit = (Button)(this.FormView1.Row.FindControl("EditButton"));
            btnEdit.Visible = false;
        }
        if(!user.HasRight(YF_AssetTransRight.Delete))
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
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
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
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        int code = 0;
        Int32.TryParse(Request.QueryString["ManageCode"],out code);
        e.Values["ManageCode"] = Request.QueryString["ManageCode"];
        e.Values["Applyer"] = user.UserName;
    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        this.RefreshParentPage();
    }
}
