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

public partial class SelectBox_SelectDocumentTemplate : System.Web.UI.Page
{
    private string ParentCode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ParentCode = Request["ParentCode"];
        FileTemplateTypeBFL BFL = new FileTemplateTypeBFL();
        XmlDocument doc = BFL.GetFileTemplateTypeDataSource(0, "模版管理");
        this.XmlDataSource1.Data = doc.InnerXml;
        string ChooseType = Request["ChooseType"];
        if (ChooseType != "" && ChooseType != null)
        {
            this.ChooseButton.Visible = true;
            this.CloseButton.Visible = true;

        }
        else
        {
            this.TreeView1.ShowCheckBoxes =TreeNodeTypes.None;
        }
    }
    protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        e.Node.NavigateUrl = "~/RmsDM/SelectDocumentTemplateList.aspx?NodeValue=" + e.Node.Value + "&AddSignleType=" + Request["AddSignleType"] + "&ParentCode=" + Request["Parentcode"] + "";
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string tempStr="";
        if (this.TreeView1.CheckedNodes.Count > 0) 
        {
            foreach (TreeNode tn in this.TreeView1.CheckedNodes) 
            {
                if (tn.Value != "0")
                {
                    tempStr += tn.Value + ",";
                }
            }
            tempStr = tempStr.Remove(tempStr.Length - 1,1);  
        }
        if (tempStr == "") 
        {
            Response.Write ("<script>alert('请选择模板类型');</script>");           
        }
        else
        {
            FileTemplateTypeBFL BFL = new FileTemplateTypeBFL();
            XmlDocument doc = BFL.GetFileTemplateTypeDataSourcePart(tempStr, 0, "部分模板管理");
            if (this.ParentCode != "" && this.ParentCode != null)
            {
                DocumentDirectoryBFL DDBFL = new DocumentDirectoryBFL();
                string DepartmentCode = DDBFL.GetDocumentDirectory(int.Parse(this.ParentCode)).DepartmentCode;
                string DirectoryNodeCode = DDBFL.GetDocumentDirectory(int.Parse(this.ParentCode)).DirectoryNodeCode;

                string FullID = DDBFL.GetDocumentDirectory(int.Parse(this.ParentCode)).FullID;
                int Deep = DDBFL.GetDocumentDirectory(int.Parse(this.ParentCode)).Deep;
                DDBFL.Insert(doc, int.Parse(this.ParentCode), doc.DocumentElement.ChildNodes, DepartmentCode, FullID, Deep, DirectoryNodeCode);
                Response.Write("<script>opener.parent.location.reload();window.close();</script>");
            }
        }
    }
}
