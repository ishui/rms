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

public partial class RmsOA_UserGroupUserEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = Request.QueryString["Code"];
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(code) || code.Equals("0"))
            {
                this.MultiView1.ActiveViewIndex = 0;
                this.GridView2.ShowFooter = false;
            }
            else
            {
                this.MultiView1.ActiveViewIndex = 1;
                this.GridView1.ShowFooter = false;
            }
            Response.Expires = -1;
            Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
        this.ObjectDataSource2.SelectParameters["UserCodeEqual"].DefaultValue = user.UserCode;

    }
    protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        Response.Write("<script>window.parent.location.reload();</script>");
    }
    protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        Response.Write("<script>window.parent.location.reload();</script>");
    }
    protected void AddGroup_Click(object sender, EventArgs e)
    {
        this.GridView2.ShowFooter = true;
    }
    protected void btnCancelAddGroup_Click(object sender, EventArgs e)
    {
        this.GridView2.ShowFooter = false;
    }
    protected void btnAddGroup_Click(object sender, EventArgs e)
    {
        TextBox tbxGroupName = (TextBox)(this.GridView2.FooterRow.FindControl("tbxGroupNameAdd"));
        UserHelpGroupBFL gBFL = new UserHelpGroupBFL();
        UserHelpGroupModel gModel = new UserHelpGroupModel();
        gModel.GroupName = tbxGroupName.Text.Trim();
        gModel.CreateTime = DateTime.Now;
        gModel.UserCode = user.UserCode;
        gBFL.Insert(gModel);
        Response.Write("<script>window.parent.location.reload();</script>");
    }
    protected void btnAddGroup_Click1(object sender, EventArgs e)
    {
        TextBox tbxGroupName = (TextBox)(this.GridView2.Controls[0].Controls[0].FindControl("tbxGroupName"));
        UserHelpGroupBFL gBFL = new UserHelpGroupBFL();
        UserHelpGroupModel gModel = new UserHelpGroupModel();
        gModel.GroupName = tbxGroupName.Text.Trim();
        gModel.CreateTime = DateTime.Now;
        gModel.UserCode = user.UserCode;
        gBFL.Insert(gModel);
        Response.Write("<script>window.parent.location.reload();</script>");
    }
    public string GroupName
    {
        get
        {
            if (!string.IsNullOrEmpty(Request.QueryString["Code"]))
            {
                return RmsOA.BFL.UserHelpSelcect.GetGroupName(Request.QueryString["Code"]);
            }
            else
            {
                return string.Empty;
            }
        }
    }
    public void WriteGroupName()
    {
        Response.Write(GroupName);
    }
    protected void btnEMPAddUser_Click(object sender, EventArgs e)
    {
        RmsPM.Web.UserControls.InputUser usecode = (RmsPM.Web.UserControls.InputUser)(this.GridView1.Controls[0].Controls[0].FindControl("InputuserAdd"));
        UserHelpUserBFL uBFL = new UserHelpUserBFL();
        UserHelpUserModel uModel = new UserHelpUserModel();
        uModel.AddDate = DateTime.Now;
        uModel.GroupCode = int.Parse(Request.QueryString["Code"]);
        uModel.UserCode = usecode.Value;
        uBFL.Insert(uModel);
        Response.Redirect(string.Format("UserGroupUserEdit.aspx?Code={0}",Request.QueryString["Code"]));
    }
    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        RmsPM.Web.UserControls.InputUser usecode = (RmsPM.Web.UserControls.InputUser)(this.GridView1.FooterRow.FindControl("InputuserAdd"));
        UserHelpUserBFL uBFL = new UserHelpUserBFL();
        UserHelpUserModel uModel = new UserHelpUserModel();
        uModel.AddDate = DateTime.Now;
        uModel.GroupCode = int.Parse(Request.QueryString["Code"]);
        uModel.UserCode = usecode.Value;
        uBFL.Insert(uModel);
        Response.Redirect(string.Format("UserGroupUserEdit.aspx?Code={0}", Request.QueryString["Code"]));

    }
    protected void btnCancelAddUser_Click(object sender, EventArgs e)
    {
        this.GridView1.ShowFooter = false;
    }
    protected void AddUser_Click(object sender, EventArgs e)
    {
        this.GridView1.ShowFooter = true;
    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        Response.Redirect(string.Format("UserGroupUserEdit.aspx?Code={0}", Request.QueryString["Code"]));
    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        Response.Redirect(string.Format("UserGroupUserEdit.aspx?Code={0}", Request.QueryString["Code"]));
    }
}
