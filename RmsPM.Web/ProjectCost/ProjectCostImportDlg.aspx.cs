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
using RmsPM.BLL;
using Rms.Web;

public partial class ProjectCost_ProjectCostImportDlg : RmsPM.Web.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            this.lblFieldDesc.Text = RmsPM.BFL.ProjectCostBFL.GetImportProjectCostFieldDesc();
                
    }
    protected void btnOK_ServerClick(object sender, EventArgs e)
    {
        if (this.txtFile.PostedFile.FileName == "")
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "请选择文件"));
            return;
        }
        //txtResult.style.display = "block";
        this.tabResult.Style["display"] = "";
        txtResult.Value = RmsPM.BFL.ProjectCostBFL.ImportProjectCost(this.txtFile.PostedFile.InputStream, user.UserCode);
        RefreshParent();
    }
    protected void btnDeleteAll_ServerClick(object sender, EventArgs e)
    {
        RmsPM.BFL.ProjectCostBFL.DeleteAllProjectCost();
        RefreshParent();
        Response.Write("<script>alert(\"项目造价已清空\");</script>");

    }
    private void RefreshParent()
    {
        Response.Write(JavaScript.ScriptStart);
        Response.Write("window.opener.location = window.opener.location;");
        Response.Write(JavaScript.ScriptEnd);
    }
}

