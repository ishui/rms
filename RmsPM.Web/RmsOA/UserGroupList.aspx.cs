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
using System.Xml;
using RmsPM.Web;
using RmsOA.BFL;

public partial class RmsOA_UserGroupList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = -1;
        UserHelpSelcect uhelp = new UserHelpSelcect();
        XmlDocument doc = uhelp.GetTreeStruct(user.UserCode);
        this.XmlDataSource1.Data = doc.InnerXml;
    }
    protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        e.Node.NavigateUrl = string.Format("~/RmsOA/UserGroupUserEdit.aspx?Code={0}", e.Node.Value);
    }
}
