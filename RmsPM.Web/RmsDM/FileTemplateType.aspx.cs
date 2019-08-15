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

public partial class RmsDM_FileTemplateType : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = -1;
        FileTemplateTypeBFL BFL = new FileTemplateTypeBFL();
        XmlDocument doc = BFL.GetFileTemplateTypeDataSource(0, "模版类别");
        this.XmlDataSource1.Data = doc.InnerXml;
        // this.XmlDataSource1.XPath = "*[@Code = 6]";
    }
    protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        //if (e.Node.Depth != 0)
        e.Node.NavigateUrl = "~/RmsDM/FileTemplateTypeList.aspx?NodeValue=" + e.Node.Value + "&FullID=" + e.Node.ValuePath + "&Deep=" + e.Node.Depth;
        //else
        //   e.Node.NavigateUrl = "#";
    }
}
