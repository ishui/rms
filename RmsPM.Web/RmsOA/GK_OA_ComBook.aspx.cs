using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RmsPM.Web;

using Microsoft.Web;
using Microsoft.Web.UI;
using Rms.ORMap;

public partial class RmsOA_GK_OA_ComBook : PageBase
{
    DataTable tbUnit = null;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        showtree();
    }

    private void showtree()
    {
        this.TreeView1.Nodes.Clear();
        string sqlUnit = "Select * from Unit";
        QueryAgent qa = new QueryAgent();
        tbUnit = qa.ExecSqlForDataSet(sqlUnit).Tables[0];

        if (tbUnit.Select("parentunitcode=''").Length > 0)
        {
            for (int i = 0; i < tbUnit.Select("parentunitcode=''").Length; i++)
            {
                string TempId = tbUnit.Select("parentunitcode=''")[i]["UnitCode"].ToString();
                string TempText = tbUnit.Select("parentunitcode=''")[i]["UnitName"].ToString();
                TreeNode mytree = new TreeNode();
                mytree.Value = TempId;
                mytree.Text = TempText;

                mytree.NavigateUrl = "GK_OA_ComBookList.aspx?UnitCode=" + mytree.Value;
                mytree.Target = "frameMain";

                mytree = My(mytree, TempId);
                this.TreeView1.Nodes.Add(mytree);
            }
        }

        this.TreeView1.Nodes[0].CollapseAll();
        this.TreeView1.Nodes[0].Expand();
       
    }

    //递归法生成树
    private TreeNode My(TreeNode ParentNode, string Fid)
    {
        if (tbUnit.Select("parentunitcode='" + ParentNode.Value + "'").Length > 0)
        {
            for (int j = 0; j < tbUnit.Select("parentunitcode='" + ParentNode.Value + "'").Length; j++)
            {
                TreeNode mytree = new TreeNode();
                string Tid = tbUnit.Select("parentunitcode='" + ParentNode.Value + "'")[j]["UnitCode"].ToString();
                string Ttext = tbUnit.Select("parentunitcode='" + ParentNode.Value + "'")[j]["UnitName"].ToString();
                mytree.Value = Tid;
                mytree.Text = Ttext;

                mytree.NavigateUrl = "GK_OA_ComBookList.aspx?UnitCode=" + mytree.Value;
                mytree.Target = "frameMain";

                mytree = My(mytree, Tid);   //递归开始


                ParentNode.ChildNodes.Add(mytree);   //完成一次，将值加入


            }
        }
        return ParentNode;
    }

}

