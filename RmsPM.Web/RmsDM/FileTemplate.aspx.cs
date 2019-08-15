using System;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RmsDM.BFL;
using RmsDM.MODEL;

using RmsPM.Web;

public partial class RmsDM_FileTemplate : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = -1;
        FileTemplateTypeBFL BFL = new FileTemplateTypeBFL();
        XmlDocument doc = BFL.GetFileTemplateTypeDataSource(0, "模版管理");
        this.XmlDataSource1.Data = doc.InnerXml;
    }
    protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        //if (e.Node.Depth != 0)
        e.Node.NavigateUrl = "~/RmsDM/FileTemplateList.aspx?NodeValue=" + e.Node.Value;
        //else
        //   e.Node.NavigateUrl = "#";
    }
}
