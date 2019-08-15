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

public partial class RmsDM_DocumentDirectory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        DocumentDirectoryBFL BFL = new DocumentDirectoryBFL();
        XmlDocument doc = BFL.GetDocumentDirectoryDataSource(0, "文档管理");
        this.XmlDataSource1.Data = doc.InnerXml;
       // this.XmlDataSource1.XPath = "*[@Code = 6]";
        
    }
    protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        //if (e.Node.Depth != 0)
        e.Node.NavigateUrl = "~/RmsDM/DocumentDirectoryList.aspx?NodeValue=" + e.Node.Value + "&FullID=" + e.Node.ValuePath + "&Deep=" + e.Node.Depth;
        //else
         //   e.Node.NavigateUrl = "#";
    }
}
