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

public partial class ProjectChangeMes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void WriteProjectName()
    {
        string name = Request.QueryString["ProjectName"];
        if (!string.IsNullOrEmpty(name))
        {
            Response.Write(string.Format("项目管理—>{0}", name));
        }
        else
        {
            Response.Write("项目管理");
        }
    }
}
