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
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL;
using Rms.Web;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.PBS
{
    /// <summary>
    /// PBSBuildingData 的摘要说明。
    /// </summary>
    partial class PBSBuildingData : PageBase
    {
        private string m_strGetType = "";
        private string m_strNodeId = "";
        private string m_strLayer = "";
        private string m_ProjectCode = "";
        private string m_strShowSum = "";

        private void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                m_strGetType = Request.QueryString["GetType"] + "";				//数据分类
                m_strLayer = Request.QueryString["Layer"] + "";					//需要取的层数
                m_strNodeId = Request.QueryString["NodeId"] + "";				//父节点编号
                string[] m_Layers = (Request.QueryString["Layers"] + "").Split('.');	//定点展开的序列
                string m_strSelectedLayer = Request.QueryString["SelectedLayer"] + "";	//定层展开的深度
                m_ProjectCode = Request.QueryString["ProjectCode"] + "";
                m_strShowSum = Request.QueryString["ShowSum"] + "";  //是否显示合计行

                DataTable m_Table = new DataTable("Building");
                m_Table.Columns.Add("BuildingCode");
                m_Table.Columns.Add("ParentCode");
                m_Table.Columns.Add("BuildingName");
                m_Table.Columns.Add("Layer");
                m_Table.Columns.Add("ChildNodesCount");
                m_Table.Columns.Add("ShowChildNodes");
                m_Table.Columns.Add("NodeType");
                m_Table.Columns.Add("IsArea");
                m_Table.Columns.Add("IconName");
                m_Table.Columns.Add("PBSTypeName");
                m_Table.Columns.Add("FloorCount");
                m_Table.Columns.Add("HouseArea");
                m_Table.Columns.Add("RoomArea");
                m_Table.Columns.Add("PBSUnitCode");
                m_Table.Columns.Add("PBSUnitName");
                m_Table.Columns.Add("PBSType");  //单位工程类型：P=项目；B=楼

                m_Table.Columns.Add("dHouseArea", typeof(decimal));
                m_Table.Columns.Add("dRoomArea", typeof(decimal));

                m_Table.Columns.Add("ShowSpan");
                m_Table.Columns.Add("ShowHref");

                //			EntityData m_Task=RmsPM.DAL.EntityDAO.ProductDAO.GetBuildingByProjectCode(ProjectCode);
                //			DataTable m_DataTable=m_Task.Tables["Building"];

                //查询条件
                BuildingStrategyBuilder sb = new BuildingStrategyBuilder();
                sb.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, m_ProjectCode));

                if (m_strGetType == "")
                {
                    #region 取第一层
                    //				DataView m_DV=new DataView(m_DataTable,"ParentCode=''","",DataViewRowState.CurrentRows);

                    sb.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, ""));
                    sb.AddOrder("BuildingName", true);
                    DataTable m_DataTable = BuildStrategy(sb);
                    DataView m_DV = new DataView(m_DataTable);

                    foreach (DataRowView m_Row in m_DV)
                    {
                        DataRow m_NewRow = m_Table.NewRow();

                        this.FillRow(ref m_NewRow, m_Row);

                        m_Table.Rows.Add(m_NewRow);
                    }

                    #endregion
                }
                else if (m_strGetType == "ChildNodes")
                {
                    #region 取某节点子目录
                    //				DataView m_DV=new DataView(m_DataTable,"ParentCode='"+m_strNodeId+"'","",DataViewRowState.CurrentRows);

                    sb.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, m_strNodeId));
                    sb.AddOrder("BuildingName", true);
                    DataTable m_DataTable = BuildStrategy(sb);
                    DataView m_DV = new DataView(m_DataTable);

                    foreach (DataRowView m_Row in m_DV)
                    {
                        DataRow m_NewRow = m_Table.NewRow();

                        this.FillRow(ref m_NewRow, m_Row);

                        m_Table.Rows.Add(m_NewRow);
                    }

                    if (m_strShowSum == "1")
                    {
                        AddSumRow(m_Table);
                    }

                    #endregion
                }
                else if (m_strGetType == "SelectLayer")
                {
                    #region 取制定层数结果
                    //				DataView m_DV=new DataView(m_DataTable,"Layer='1'","",DataViewRowState.CurrentRows);

                    sb.AddStrategy(new Strategy(BuildingStrategyName.Layer, "1"));
                    sb.AddOrder("BuildingName", true);
                    DataTable m_DataTable = BuildStrategy(sb);
                    DataView m_DV = new DataView(m_DataTable);

                    foreach (DataRowView m_Row in m_DV)
                    {
                        DataRow m_NewRow = m_Table.NewRow();

                        this.FillRow(ref m_NewRow, m_Row);

                        m_Table.Rows.Add(m_NewRow);
                        if (int.Parse(m_strSelectedLayer) > 1)
                        {
                            m_NewRow["ShowChildNodes"] = "1";
                            this.FillSelectedLayerData(ref m_Table, m_Row["PBSTypeCode"].ToString(), 2, int.Parse(m_strSelectedLayer), m_DataTable);
                        }
                    }
                    #endregion
                }
                else if (m_strGetType == "All")
                {
                    #region 取所有结果
                    //				DataView m_DV=new DataView(m_DataTable,"Layer='1'","",DataViewRowState.CurrentRows);

                    sb.AddStrategy(new Strategy(BuildingStrategyName.Layer, "1"));
                    sb.AddOrder("BuildingName", true);
                    DataTable m_DataTable = BuildStrategy(sb);
                    DataView m_DV = new DataView(m_DataTable);

                    foreach (DataRowView m_Row in m_DV)
                    {
                        DataRow m_NewRow = m_Table.NewRow();

                        this.FillRow(ref m_NewRow, m_Row);

                        m_Table.Rows.Add(m_NewRow);

                        if (int.Parse(m_NewRow["ChildNodesCount"].ToString()) > 0)
                        {
                            m_NewRow["ShowChildNodes"] = "1";
                            this.FillAllData(ref m_Table, m_Row["PBSTypeCode"].ToString(), 2, m_DataTable);
                        }
                    }
                    #endregion
                }
                else if (m_strGetType == "SingleNode")
                {
                    #region 单个节点
                    //				DataView m_DV=new DataView(m_DataTable,"BuildingCode='"+Request.QueryString["NodeId"]+""+"'","",DataViewRowState.CurrentRows);

                    sb.AddStrategy(new Strategy(BuildingStrategyName.BuildingCode, m_strNodeId));
                    sb.AddOrder("BuildingName", true);
                    DataTable m_DataTable = BuildStrategy(sb);
                    DataView m_DV = new DataView(m_DataTable);

                    foreach (DataRowView m_Row in m_DV)
                    {
                        DataRow m_NewRow = m_Table.NewRow();

                        this.FillRow(ref m_NewRow, m_Row);

                        m_Table.Rows.Add(m_NewRow);

                    }
                    #endregion
                }
                else if (m_strGetType == "Project")
                {
                    #region 项目

                    EntityData entityProject = DAL.EntityDAO.ProjectDAO.GetProjectByCode(m_ProjectCode);
                    DataTable m_DataTable = entityProject.CurrentTable;
                    DataView m_DV = new DataView(m_DataTable);

                    foreach (DataRowView m_Row in m_DV)
                    {
                        DataRow m_NewRow = m_Table.NewRow();

                        this.FillRowProject(ref m_NewRow, m_Row);

                        m_Table.Rows.Add(m_NewRow);

                    }
                    entityProject.Dispose();

                    #endregion
                }
                else if (m_strGetType == "ChildNodesOfProject")
                {
                    #region 取某节点子目录
                    //				DataView m_DV=new DataView(m_DataTable,"ParentCode='"+m_strNodeId+"'","",DataViewRowState.CurrentRows);

                    sb.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, ""));
                    sb.AddOrder("BuildingName", true);
                    DataTable m_DataTable = BuildStrategy(sb);
                    DataView m_DV = new DataView(m_DataTable);

                    foreach (DataRowView m_Row in m_DV)
                    {
                        DataRow m_NewRow = m_Table.NewRow();

                        this.FillRow(ref m_NewRow, m_Row);

                        m_Table.Rows.Add(m_NewRow);
                    }
                    #endregion
                }

                Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

            Response.End();
        }

        /// <summary>
        /// 按查询条件输出结果集
        /// </summary>
        /// <param name="sb"></param>
        /// <returns></returns>
        private DataTable BuildStrategy(BuildingStrategyBuilder sb)
        {
            try
            {
                string sql = sb.BuildMainQueryString();

                QueryAgent qa = new QueryAgent();
                EntityData entity = qa.FillEntityData("Building", sql);
                qa.Dispose();

                return entity.CurrentTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillRow(ref DataRow m_NewRow, DataRowView m_Row)
        {
            int ChildCount = BLL.ProductRule.GetBuildingChildCount(m_Row["BuildingCode"].ToString());
            string nodeType = ChildCount > 0 ? "folder" : "item";

            m_NewRow["BuildingCode"] = m_Row["BuildingCode"].ToString();

            if (m_strGetType == "ChildNodesOfProject")  //项目下的一级楼栋
            {
                m_NewRow["ParentCode"] = m_ProjectCode;
            }
            else
            {
                m_NewRow["ParentCode"] = m_Row["ParentCode"].ToString();
            }

            m_NewRow["BuildingName"] = m_Row["BuildingName"].ToString();
            m_NewRow["Layer"] = m_Row["Layer"].ToString();
            m_NewRow["ChildNodesCount"] = ChildCount;
            m_NewRow["ShowChildNodes"] = "0";
            m_NewRow["NodeType"] = nodeType;
            m_NewRow["IsArea"] = m_Row["IsArea"];

            if (BLL.ConvertRule.ToInt(m_Row["IsArea"]) == 1) //区域
            {
                //区域面积从楼栋累加
                BuildingStrategyBuilder sb = new BuildingStrategyBuilder();
                sb.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, m_ProjectCode));
                sb.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, m_Row["BuildingCode"].ToString()));
                DataTable tb = BuildStrategy(sb);

                decimal[] area = BLL.MathRule.SumColumn(tb, new string[] { "HouseArea", "RoomArea" });

                m_NewRow["HouseArea"] = BLL.StringRule.BuildShowNumberString(area[0]);
                m_NewRow["RoomArea"] = BLL.StringRule.BuildShowNumberString(area[1]);

                m_NewRow["IconName"] = "BuildingArea.gif";
            }
            else //楼栋
            {
                m_NewRow["PBSTypeName"] = BLL.PBSRule.GetPBSTypeFullName(BLL.ConvertRule.ToString(m_Row["PBSTypeCode"]));
                m_NewRow["FloorCount"] = BLL.ConvertRule.ToInt(m_Row["IFloorCount"]);
                m_NewRow["HouseArea"] = BLL.MathRule.GetDecimalShowString(m_Row["HouseArea"]);
                m_NewRow["RoomArea"] = BLL.MathRule.GetDecimalShowString(m_Row["RoomArea"]);
                m_NewRow["PBSUnitCode"] = BLL.ConvertRule.ToString(m_Row["PBSUnitCode"]);
                m_NewRow["PBSUnitName"] = BLL.PBSRule.GetPBSUnitName(BLL.ConvertRule.ToString(m_Row["PBSUnitCode"]));

                m_NewRow["IconName"] = "Building.gif";
            }

            m_NewRow["PBSType"] = "B";
            m_NewRow["ShowSpan"] = "none";
            m_NewRow["ShowHref"] = "";
        }

        private void FillRowProject(ref DataRow m_NewRow, DataRowView m_Row)
        {
            int ChildCount = 1;
            string nodeType = ChildCount > 0 ? "folder" : "item";

            m_NewRow["BuildingCode"] = "P_" + m_Row["ProjectCode"].ToString();
            m_NewRow["ParentCode"] = "";
            m_NewRow["BuildingName"] = m_Row["ProjectName"].ToString();
            m_NewRow["Layer"] = 0;
            m_NewRow["ChildNodesCount"] = ChildCount;
            m_NewRow["ShowChildNodes"] = "0";
            m_NewRow["NodeType"] = nodeType;
            m_NewRow["IsArea"] = DBNull.Value;

            //显示图标
            m_NewRow["IconName"] = "BuildingArea.gif";

            m_NewRow["PBSType"] = "P";
            m_NewRow["ShowSpan"] = "none";
            m_NewRow["ShowHref"] = "";
        }

        /// <summary>
        /// 加合计行
        /// </summary>
        /// <param name="m_Table"></param>
        private void AddSumRow(DataTable m_Table)
        {
            DataRow m_NewRow = m_Table.NewRow();

            m_NewRow["BuildingCode"] = "P_0";
            m_NewRow["ParentCode"] = "";
            m_NewRow["BuildingName"] = "合计";

            m_NewRow["Layer"] = 1;
            m_NewRow["ChildNodesCount"] = 0;
            m_NewRow["ShowChildNodes"] = "0";
            m_NewRow["NodeType"] = "item";
            m_NewRow["IsArea"] = DBNull.Value;

            //显示图标
            m_NewRow["IconName"] = "BuildingArea.gif";

            m_NewRow["PBSType"] = "P";

            //面积累加
            decimal[] area = BLL.MathRule.SumColumn(m_Table, new string[] { "HouseArea", "RoomArea" });

            m_NewRow["HouseArea"] = BLL.MathRule.GetDecimalShowString(area[0]);
            m_NewRow["RoomArea"] = BLL.MathRule.GetDecimalShowString(area[1]);

            m_NewRow["ShowSpan"] = "";
            m_NewRow["ShowHref"] = "none";

            m_Table.Rows.Add(m_NewRow);
        }

        #region FillSelectedLayerData
        private void FillSelectedLayerData(ref DataTable m_Table, string m_strWBSCode, int m_iNowLayer, int m_iStopLayer, DataTable m_DataTable)
        {
            DataView m_DV = new DataView(m_DataTable, "ParentCode like '" + m_strWBSCode + "'", "", DataViewRowState.CurrentRows);
            foreach (DataRowView m_Row in m_DV)
            {
                DataRow m_NewRow = m_Table.NewRow();

                this.FillRow(ref m_NewRow, m_Row);

                m_Table.Rows.Add(m_NewRow);
                if (m_iStopLayer > m_iNowLayer)
                {
                    m_NewRow["ShowChildNodes"] = "1";
                    this.FillSelectedLayerData(ref m_Table, m_Row["BuildingCode"].ToString(), m_iNowLayer + 1, m_iStopLayer, m_DataTable);
                }
            }
        }
        #endregion

        #region FillAllData
        private void FillAllData(ref DataTable m_Table, string m_strWBSCode, int m_iNowLayer, DataTable m_DataTable)
        {
            DataView m_DV = new DataView(m_DataTable, "ParentCode='" + m_strWBSCode + "'", "", DataViewRowState.CurrentRows);
            foreach (DataRowView m_Row in m_DV)
            {
                DataRow m_NewRow = m_Table.NewRow();

                this.FillRow(ref m_NewRow, m_Row);

                m_Table.Rows.Add(m_NewRow);
                if (int.Parse(m_NewRow["ChildNodesCount"].ToString()) > 0)
                {
                    m_NewRow["ShowChildNodes"] = "1";
                    this.FillAllData(ref m_Table, m_Row["BuildingCode"].ToString(), m_iNowLayer + 1, m_DataTable);
                }
            }
        }
        #endregion

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
