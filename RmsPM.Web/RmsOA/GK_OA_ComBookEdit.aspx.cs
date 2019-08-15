﻿using System;
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

public partial class RmsOA_GK_OA_ComBookEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["Code"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            if (FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                if (user.HasRight("350302"))
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = true;
                }
                else
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                }
                if (user.HasRight("350303"))
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
            HtmlInputHidden tbxUnitCode = (HtmlInputHidden)this.FormView1.Row.FindControl("txtUnit");
            HtmlInputText tbxUnitName = (HtmlInputText)this.FormView1.Row.FindControl("txtUnitName");
            tbxUnitName.Value = RmsPM.BLL.SystemRule.GetUnitName(tbxUnitCode.Value);

        }
    }
}
