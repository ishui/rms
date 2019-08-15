using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using Rms.Web;
using RmsPM.DAL;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using RmsPM.BLL;
using Telerik.WebControls;


namespace RmsPM.Web.SelectBox
{
    /// <summary>
    /// SelectCostBudgetDtl 的摘要说明。
    /// </summary>
    public partial class SelectCostBudgetDtl : PageBase
    {
        DataTable tbUnit = null;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                //返回函数名
                string ReturnFunc = Request.QueryString["ReturnFunc"] + "";
                if (ReturnFunc == "")
                {
                    ReturnFunc = "SelectCostBudgetDtlReturn";
                }
                ViewState["ReturnFunc"] = ReturnFunc;

                string CostBudgetSetCode = "" + Request["CostBudgetSetCode"];

                //switch (this.up_sPMName.ToUpper())
                //{
                //    case "SHIMAOPM":
                //        //世茂：只显示本部门的预算表 xyq 2007.7.25
                //        BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, CostBudgetSetCode, "" + Request["ProjectCode"], user.m_EntityDataAccessUnit);
                //        break;

                //    default:
                //        BLL.PageFacade.LoadCostBudgetSetSelect(this.sltCostBudgetSet, CostBudgetSetCode, "" + Request["ProjectCode"]);
                //        break;

                //}
                showtree();
            }
        }




        private void showtree()
        {
            string CostBudgetSetCode = "" + Request["CostBudgetSetCode"];
            ((TreeView)this.RadComboBox1.Items[0].FindControl("TreeView1")).Nodes.Clear();
            string sqlUnit = "Select * from Building";
            QueryAgent qa = new QueryAgent();
            tbUnit = qa.ExecSqlForDataSet(sqlUnit).Tables[0];
            string projectcode = Request.QueryString["ProjectCode"] + "";


            //取目标费用表
            CostBudgetSetStrategyBuilder sb = new CostBudgetSetStrategyBuilder();
            sb.AddStrategy(new Strategy(CostBudgetSetStrategyName.ProjectCode, projectcode));
            string sql;
            //权限
            //ArrayList arA = new ArrayList();
            //arA.Add(user.UserCode);
            //arA.Add(user.BuildStationCodes());
            //sb.AddStrategy(new Strategy(DAL.QueryStrategy.CostBudgetSetStrategyName.AccessRange, arA));

            //缺省排序（项目总体费用排在最后）
            sb.AddOrder("GroupSortID", true);
            sb.AddOrder("PBSType", true);
            sb.AddOrder("CostBudgetSetName", true);
            sql = sb.BuildQueryViewString();

            QueryAgent qal = new QueryAgent();
            DataTable m_DataTable = qal.ExecSqlForDataSet(sql).Tables[0];
            qal.Dispose();

            //为初始化树节点做准备
            DataRow[] inidrs = m_DataTable.Select("CostBudgetSetCode = '" + CostBudgetSetCode + "'");


            if (inidrs.Length > 0)
            {
                this.PBSTypeID.Value = inidrs[0]["PBSType"].ToString();
                PBSCodeID.Value = inidrs[0]["PBSCode"].ToString();
                PBSNameID.Value = inidrs[0]["PBSName"].ToString();
                this.txtSelectCostBudgetSetCode.Value = inidrs[0]["CostBudgetSetCode"].ToString();
                RegisterClientScriptBlock("IsExecSearchTree", "<script>IsExecSearchTree = 1;</script>");
                this.RadComboBox1.Items[0].Text = inidrs[0]["CostBudgetSetName"].ToString();
            }
            ArrayList al = new ArrayList();
            foreach (DataRow dr in m_DataTable.Rows)
            {
                string GroupCode = RmsPM.BLL.ConvertRule.ToString(dr["GroupCode"]);
                if ((GroupCode != "") && (al.IndexOf(GroupCode) < 0))
                {
                    al.Add(GroupCode);
                }
            }

            foreach (string GroupCode in al)
            {
                DataRow[] drs = m_DataTable.Select("GroupCode = '" + GroupCode + "'");
                if (drs.Length > 0)
                {
                    TreeNode mytree = new TreeNode();

                    mytree.Value = drs[0]["GroupCode"].ToString();
                    mytree.Text = drs[0]["GroupName"].ToString();
                    mytree.NavigateUrl = "#";
                    ((TreeView)this.RadComboBox1.Items[0].FindControl("TreeView1")).Nodes.Add(mytree);
                    My2((TreeView)this.RadComboBox1.Items[0].FindControl("TreeView1"), mytree, m_DataTable);

                }

            }



            ((TreeView)this.RadComboBox1.Items[0].FindControl("TreeView1")).ExpandAll();
            //((TreeView)this.RadComboBox1.Items[0].FindControl("TreeView1")).ShowExpandCollapse = false;
            //((TreeView)this.RadComboBox1.Items[0].FindControl("TreeView1")).ImageSet = System.Web.UI.WebControls.TreeViewImageSet.Arrows;
            //((TreeView)this.RadComboBox1.Items[0].FindControl("TreeView1")).Attributes.Add("onclick", "clicktr()");

        }


        private void My2(TreeView tv, TreeNode tnParent, DataTable m_DataTable)
        {
            if ("shimaopm" == this.up_sPMNameLower && user.m_EntityDataAccessUnit != null)
            {
                //加权限控制：只能看本部门下的预算表  2006.7.25
                if (user.m_EntityDataAccessUnit != null)
                {
                    for (int i = m_DataTable.Rows.Count - 1; i >= 0; i--)
                    {
                        //m_DataTable.SetCurrentRow(i);
                        string UnitCode = m_DataTable.Rows[i]["UnitCode"].ToString();
                        if (user.m_EntityDataAccessUnit.CurrentTable.Select("UnitCode='" + UnitCode + "'").Length <= 0) //没权限
                        {
                            m_DataTable.Rows.Remove(m_DataTable.Rows[i]);
                        }
                    }
                }

            }



            string GroupCode = tnParent.Value;
            DataRow[] drs = m_DataTable.Select(string.Format("GroupCode = '{0}'", GroupCode));
            ArrayList ary = new ArrayList();
            foreach (DataRow dr in drs)
            {
                string DistrictCode = RmsPM.BLL.CostBudgetRule.GetPBSDistrictCode(RmsPM.BLL.ConvertRule.ToString(dr["PBSType"]), RmsPM.BLL.ConvertRule.ToString(dr["PBSCode"]));
                if ("" == DistrictCode)
                {
                    TreeNode tn = new TreeNode();

                    tn.Value += dr["PBSType"] + ",";
                    tn.Value += dr["PBSCode"] + ",";
                    tn.Value += dr["PBSName"] + ",";

                    tn.Value += dr["CostBudgetSetCode"].ToString() + ",";
                    tn.Value += dr["CostBudgetSetName"].ToString();
                    tn.Text = dr["CostBudgetSetName"].ToString();
                    //tn.
                    // mytree "frameMain";
                    tnParent.ChildNodes.Add(tn);


                }
                else    //区域
                {

                    if (ary.IndexOf(DistrictCode) < 0)
                    {
                        ary.Add(DistrictCode);
                        TreeNode tn = new TreeNode();
                        tn.Value = "";
                        tn.Text = RmsPM.BLL.ProductRule.GetBuildingName(DistrictCode);
                        tn.NavigateUrl = "#";
                        
                        tnParent.ChildNodes.Add(tn);
                        
                        //区域中加预算
                        foreach (DataRow drw in drs)
                        {
                            string CurrentDistrictCode = RmsPM.BLL.CostBudgetRule.GetPBSDistrictCode(RmsPM.BLL.ConvertRule.ToString(drw["PBSType"]), RmsPM.BLL.ConvertRule.ToString(drw["PBSCode"]));
                            if (DistrictCode == CurrentDistrictCode)
                            {
                                TreeNode tnSub = new TreeNode();
                                tnSub.Value += drw["PBSType"] + ",";
                                tnSub.Value += drw["PBSCode"] + ",";
                                tnSub.Value += drw["PBSName"] + ",";

                                tnSub.Value += drw["CostBudgetSetCode"].ToString() + ",";
                                tnSub.Value += drw["CostBudgetSetName"].ToString();
                                tnSub.Text = drw["CostBudgetSetName"].ToString();

                                // mytree.Target = "frameMain";
                                tn.ChildNodes.Add(tnSub);
                            }
                        }
                    }
                }

            }


        }


        public void TreeNode_Click(object ob, EventArgs e)
        {
            if (((TreeView)ob).SelectedNode != null && ((TreeView)ob).SelectedNode.ChildNodes.Count==0)
            {

                string[] trValue = ((TreeView)ob).SelectedNode.Value.Split(',');
                this.PBSTypeID.Value = trValue[0];
                PBSCodeID.Value = trValue[1];
                PBSNameID.Value = trValue[2];
                this.txtSelectCostBudgetSetCode.Value = trValue[3];
                //this.txtSelectCostBudgetSetName.Value = trValue[4];
                RegisterClientScriptBlock("IsExecSearchTree", "<script>IsExecSearchTree = 1;</script>");
                this.RadComboBox1.Items[0].Text = trValue[4];
                //this.btnSearchTree.click();
                //               RegisterClientScriptBlock("SearchTree111", string.Format("<script>SearchTree(\"{0}\");</script>", trValue[3]));
                // register
                //RegisterStartupScript("SearchTree111", string.Format("<script>SearchTree(\"{0}\");</script>", trValue[3]));

            }

        }

        public void TreeNode_Collapsed(object ob, EventArgs e)
        {
           ((TreeView)ob).ExpandAll();
        }












        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion


    }
}

