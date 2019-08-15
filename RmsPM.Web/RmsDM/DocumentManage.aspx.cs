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

public partial class RmsDM_DocumentManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DocumentDirectoryBFL BFL = new DocumentDirectoryBFL();
        XmlDocument doc = BFL.GetDocumentDirectoryDataSource(0,"文档管理");
        this.CommonTreeView.LeafNodeStyle.ImageUrl = this.CommonTreeView.NodeStyle.ImageUrl;
        this.XmlDataSource1.Data = doc.InnerXml;
    }
    protected void CommonTreeView_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.Depth != 0)
            e.Node.NavigateUrl = "~/RmsDM/DocumentFileList.aspx?NodeValue=" + e.Node.Value;
        else
            e.Node.NavigateUrl = "#";
    }
}
