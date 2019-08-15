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

using RmsDM.MODEL;
using RmsDM.BFL;
using RmsPM.Web;

public partial class RmsDM_FileTemplateAdd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int nodeValue = Int32.Parse(Request.QueryString["NodeValue"]);
        FileTemplateTypeModel fttModel = new FileTemplateTypeModel();
        FileTemplateTypeBFL fttBFL = new FileTemplateTypeBFL();
        fttModel = fttBFL.GetFileTemplateType(nodeValue);
        int parentCode = 0;
        parentCode = fttModel.ParentCode;
        string Text = fttModel.FileTemplateTypeName;
        while (parentCode != 0)
        {
            fttModel = new FileTemplateTypeModel();
            fttModel = fttBFL.GetFileTemplateType(parentCode);
            parentCode = fttModel.ParentCode;
            Text = fttModel.FileTemplateTypeName + " --> " + Text;
        }
        this.lblSortName.Text = Text;
    }

    public void btAdd_Click(object sender, EventArgs e)
    {
        int nodeValue = Int32.Parse(Request.QueryString["NodeValue"]);
        FileTemplateTypeModel fttModel = new FileTemplateTypeModel();
        FileTemplateTypeBFL fttBFL = new FileTemplateTypeBFL();
        fttModel = fttBFL.GetFileTemplateType(nodeValue);
        FileTemplateBFL ftBFL = new FileTemplateBFL();
        FileTemplateModel ftModel = new FileTemplateModel();
        ftModel.FileTemplateName = this.tboxName.Text.Trim();
        ftModel.SortCode = this.tboxSort.Text.Trim();
        ftModel.FileTemplateTypeCode = nodeValue;
        int code=ftBFL.Insert(ftModel);
        Response.Write("<script>opener.parent.location.reload();location.href('FileTemplateVersionList.aspx?Code=" + code.ToString() + "');</script>");
        //
        //this.AddButton.Visible = false;
        //this.tboxName.Enabled = false;
        //this.tboxSort.Enabled = false;
        
    }
}
