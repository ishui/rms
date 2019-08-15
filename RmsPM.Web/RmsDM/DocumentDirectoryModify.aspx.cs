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
using RmsDM.BFL;
using RmsPM.Web;
public partial class RmsDM_DocumentDirecotoryModify : PageBase
{
    private string FullPath;
    private string ParentCode;
    private string Deep;
    private string DocDirCode;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.FullPath = Request["FullPath"];
        this.ParentCode = Request["ParentCode"];
        this.Deep = Request["Deep"];
        this.DocDirCode = Request["DocDirCode"];
        if (!IsPostBack) 
        {           
            if (Request["FormType"] == "Add") 
            {
                this.FormView1.ChangeMode(FormViewMode.Insert);                
            }           
        }
    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        Label PCLabel = (Label)this.FormView1.Row.FindControl("ParentCodeLabel");
        HiddenField PCHiddenField = (HiddenField)this.FormView1.Row.FindControl("ParentCodeHiddenField");
        HtmlInputControl UnitNameHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtUnitName");
        HtmlInputControl UnitHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtUnit");
        switch (this.FormView1.CurrentMode) 
        {           
            case FormViewMode.Edit:    
                PCLabel.Text = WebFunctionRule.GetTreeViewFullPath(PCHiddenField.Value+"/"+this.DocDirCode); 
                UnitNameHIC.Value = RmsPM.BLL.SystemRule.GetUnitName(UnitHIC.Value);
                HtmlInputControl TemplateNameHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateName");
                HtmlInputControl TemplateHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateCode");
                TemplateNameHIC.Value = FileTemplateBFL.GetTemplateName(TemplateHIC.Value);
                if (TemplateNameHIC.Value != "") 
                {
                    LinkButton lb = (LinkButton)this.FormView1.Row.FindControl("LinkButton1");
                    lb.Visible = true;
                }
                break; 
            case FormViewMode.Insert:
                if (this.ParentCode != ""&&this.ParentCode!=null)
                {                    
                    PCLabel.Text = WebFunctionRule.GetTreeViewFullPath(this.FullPath);
                    PCHiddenField.Value = this.ParentCode;
                    DocumentDirectoryBFL DDBFL = new DocumentDirectoryBFL();
                    UnitHIC.Value = DDBFL.GetDocumentDirectory(int.Parse(this.ParentCode)).DepartmentCode;
                    UnitNameHIC.Value = RmsPM.BLL.SystemRule.GetUnitName(UnitHIC.Value);
                }
                else
                {
                    PCLabel.Text = "此目录为一级目录！";
                    PCHiddenField.Value = "0";
                }                
                break;
            case FormViewMode.ReadOnly:
                if (!user.HasRight("3602"))
                {
                    this.FormView1.Row.FindControl("EditButton").Visible = false;
                }
                break;

        }
    }  
    protected void FormView1_ItemInserting(object sender, FormViewInsertEventArgs e)
    {
        HtmlInputControl TemplateHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateCode");
        if (TemplateHIC.Value == "") 
        {
            e.Values["FileTemplateCode"] = -1;
        }
        if (this.FullPath == "")
        {
            e.Values["FullID"] = "0";
        }
        else
        {
            e.Values["FullID"] = this.FullPath;
        }
        if (this.Deep == "")
        {
            e.Values["Deep"] = 1;
        }
        else 
        {
            e.Values["Deep"] = this.Deep;
        }
        e.Values["CreateDate"] = System.DateTime.Now;       
    } 

    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        this.RegisterStartupScript("", "<script>opener.parent.location.reload();</script>");       
    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Write("<script>opener.parent.location.reload();window.close();</script>");
        Response.End();
    }
    protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
    {
        HtmlInputControl TemplateNameHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateName");
        
        if (TemplateNameHIC.Value == "")
        {
            e.NewValues["FileTemplateCode"] = -1;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        HtmlInputControl TemplateNameHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateName");
        //HtmlInputControl TemplateHIC = (HtmlInputControl)this.FormView1.Row.FindControl("txtTemplateCode");
        TemplateNameHIC.Value = "";
        
    }
    protected void FormView1_ItemDeleting(object sender, FormViewDeleteEventArgs e)
    {

    }
}
