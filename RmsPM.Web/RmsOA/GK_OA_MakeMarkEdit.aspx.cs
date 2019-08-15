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

public partial class RmsOA_GK_OA_MakeMarkEdit : PageBase
{
    private RmsPM.Web.UserControls.AttachMentAdd ucadd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Code"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            if (FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                if (user.HasRight("300202"))
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                }
                if (user.HasRight("300203"))
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("DeleteButton").Visible = false;
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
        Response.End();
    }

    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();
        this.ucadd = (RmsPM.Web.UserControls.AttachMentAdd)this.FormView1.Row.FindControl("Attachmentadd1");
        ucadd.SaveAttachMent(e.ReturnValue.ToString());
    }

    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        e.Values["RegisterPerson"] = "";

        DropDownList ddlMarkType = (DropDownList)this.FormView1.Row.FindControl("drpMarkType");
        e.Values["MarkType"] = ddlMarkType.SelectedValue;
    }

    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.Edit)
        {
            DropDownList ddlMarkType = (DropDownList)this.FormView1.Row.FindControl("drpMarkType");
            e.NewValues["MarkType"] = ddlMarkType.SelectedValue;
        }
    }

    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            Label tbxUnit = (Label)this.FormView1.Row.FindControl("UnitLabel");
            tbxUnit.Text = RmsPM.BLL.SystemRule.GetUnitName(tbxUnit.Text);
        }
        if (this.FormView1.CurrentMode == FormViewMode.Edit)
        {

            DropDownList ddlMarkType = (DropDownList)this.FormView1.Row.FindControl("drpMarkType");
            TextBox txtMarkType = (TextBox)this.FormView1.Row.FindControl("MarkTypeTextBox");
            ddlMarkType.SelectedValue = txtMarkType.Text;

            HtmlInputHidden tbxUnitCode = (HtmlInputHidden)this.FormView1.Row.FindControl("txtUnit");
            HtmlInputText tbxUnitName = (HtmlInputText)this.FormView1.Row.FindControl("txtUnitName");
            tbxUnitName.Value = RmsPM.BLL.SystemRule.GetUnitName(tbxUnitCode.Value);
        }
    }
}
