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

public partial class RmsDM_DocumentFileModify : PageBase
{
    private RmsPM.Web.UserControls.AttachMentAdd ucadd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) 
        {
            if (Request["DocumentFileCode"] + "" == "")
                FormView1.ChangeMode(FormViewMode.Insert);

            if (this.FormView1.CurrentMode == FormViewMode.ReadOnly)
            {
                Button EditB = (Button)this.FormView1.Row.FindControl("EditButton");
                if (Request["Debug"] == "1") 
                {
                    EditB.Visible = true;
                }
            }
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        HtmlInputControl TemplateNameHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateName");
        //HtmlInputControl TemplateHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateCode");
        TemplateNameHIC.Value = "";

    }

    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        switch (this.FormView1.CurrentMode)
        {
            case FormViewMode.Edit:

                HtmlInputControl TemplateNameHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateName");
                HtmlInputControl TemplateHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateCode");
                TemplateNameHIC.Value =RmsDM.BFL.FileTemplateBFL.GetTemplateName(TemplateHIC.Value);

                HtmlInputHidden tbxUnitCode = (HtmlInputHidden)this.FormView1.Row.FindControl("txtUnit");
                HtmlInputText tbxUnitName = (HtmlInputText)this.FormView1.Row.FindControl("txtUnitName");
                tbxUnitName.Value = RmsPM.BLL.SystemRule.GetUnitName(tbxUnitCode.Value);
               
                break;
            case FormViewMode.Insert:
                 RmsDM.BFL.DocumentDirectoryBFL docDirBFL = new RmsDM.BFL.DocumentDirectoryBFL();
                RmsDM.MODEL.DocumentDirectoryModel ddModel = docDirBFL.GetDocumentDirectory(int.Parse(Request["DirectorCode"]));
                int FileTemplateCode = ddModel.FileTemplateCode;
                string DepartmentCode = ddModel.DepartmentCode;

                Label labFileTemplateCode = (Label)this.FormView1.Row.FindControl("FileTemplateCodeLabel");
                labFileTemplateCode.Text = RmsDM.BFL.FileTemplateBFL.GetTemplateName(Convert.ToString(FileTemplateCode));


                Label labDepartmentCode = (Label)this.FormView1.Row.FindControl("ApplyDepartmentCodeLabel");
                labDepartmentCode.Text = RmsPM.BLL.SystemRule.GetUnitName(DepartmentCode);

                TextBox txtCounts = (TextBox)this.FormView1.Row.FindControl("txtCounts");
                txtCounts.Text = "1";
                TextBox txtLeaves = (TextBox)this.FormView1.Row.FindControl("txtLeaves");
                txtLeaves.Text = "1";
               
                break;
            case FormViewMode.ReadOnly:
               
                Label tbxUnit = (Label)this.FormView1.Row.FindControl("ApplyDepartmentCodeLabel");
                tbxUnit.Text = RmsPM.BLL.SystemRule.GetUnitName(tbxUnit.Text);

                Label labUser = (Label)this.FormView1.Row.FindControl("ApplyUserCodeLabel");
                labUser.Text = WebFunctionRule.GetUserNameByCode(labUser.Text);

   
                Label labGreateUser = (Label)this.FormView1.Row.FindControl("CreateUserCodeLabel");
                labGreateUser.Text = WebFunctionRule.GetUserNameByCode(labGreateUser.Text);

                Label labLastModifyUser = (Label)this.FormView1.Row.FindControl("LastModifyByUserCodeLabel");
                labLastModifyUser.Text = WebFunctionRule.GetUserNameByCode(labLastModifyUser.Text);
                break;

        }
    }

    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        RmsDM.BFL.DocumentDirectoryBFL docDirBFL = new RmsDM.BFL.DocumentDirectoryBFL();
        RmsDM.MODEL.DocumentDirectoryModel ddModel = docDirBFL.GetDocumentDirectory(int.Parse(Request["DirectorCode"]));
        int FileTemplateCode = ddModel.FileTemplateCode;
        string DepartmentCode = ddModel.DepartmentCode;

        e.Values["DeleteFlag"] = "";
        e.Values["ApplyDepartmentCode"] = DepartmentCode;
        e.Values["FileTemplateCode"] = FileTemplateCode;
       
        
    }

    protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ObjectDataSource1.SelectParameters["Code"].DefaultValue = e.ReturnValue.ToString();

        this.ucadd = (RmsPM.Web.UserControls.AttachMentAdd)this.FormView1.Row.FindControl("Attachmentadd1");
        ucadd.SaveAttachMent(e.ReturnValue.ToString());
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
}
