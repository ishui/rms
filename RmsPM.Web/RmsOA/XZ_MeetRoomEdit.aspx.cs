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

public partial class RmsOA_XZ_MeetRoomEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string type = Request.QueryString["Type"];
            if (!string.IsNullOrEmpty(type))
            {
                if (type.Equals("Add"))
                {
                    this.FormView1.ChangeMode(FormViewMode.Insert);
                }
            }
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
    }
    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        this.ObjectDataSource1.SelectParameters.Clear();
        this.ObjectDataSource1.SelectParameters.Add("Code", e.ReturnValue.ToString());
        this.FormView1.DataBind();
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            if (!user.HasRight(OperRightCollection.MeetRoomOper.Edit))
            {
                this.EditButton.Visible = false;
            }
            if (!user.HasRight(OperRightCollection.MeetRoomOper.Delete))
            {
                this.DeleteButton.Visible = false;
            }
        }
    }

    public Button EditButton
    {
        get
        {
            Button btn = (Button)this.FormView1.Row.FindControl("EditButton");
            return btn;
        }
    }
    public Button DeleteButton
    {
        get
        {
            Button btn = (Button)this.FormView1.Row.FindControl("DeleteButton");
            return btn;
        }
    }
}
