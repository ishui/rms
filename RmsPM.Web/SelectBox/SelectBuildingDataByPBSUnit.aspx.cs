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
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;
using RmsPM.DAL.EntityDAO;
using Rms.Web;


namespace RmsPM.Web.SelectBox
{
	/// <summary>
	/// SelectBuildingDataByPBSUnit 的摘要说明。
	/// </summary>
	public partial class SelectBuildingDataByPBSUnit : PageBase
	{
		// 所有文档类型的数据表
		private DataTable m_DataTable = null;

		// 保存返回数据的数据表
		private DataTable m_Table = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{

			string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
			string m_strLayer=Request.QueryString["Layer"]+"";					//需要取的层数
			string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
			string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
			string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度
			string ProjectCode = Request.QueryString["ProjectCode"] + "";
			string PBSTypeCode = Request.QueryString["PBSTypeCode"] + "";

			m_Table = new DataTable("Building");
			m_Table.Columns.Add("BuildingCode");
			m_Table.Columns.Add("ParentCode");
			m_Table.Columns.Add("BuildingName");
			m_Table.Columns.Add("Description");
			m_Table.Columns.Add("Layer");
			m_Table.Columns.Add("ChildNodesCount");
			m_Table.Columns.Add("ShowChildNodes");
			m_Table.Columns.Add("NodeType");
			m_Table.Columns.Add("IsPBSUnit");
			m_Table.Columns.Add("NoSelectPBSUnit");
			m_Table.Columns.Add("IconName");
			m_Table.Columns.Add("PBSUnitName");

			//查询条件
			BuildingStrategyBuilder sb = new BuildingStrategyBuilder("V_Building");
			sb.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));

			if (PBSTypeCode != "") 
				sb.AddStrategy(new Strategy(BuildingStrategyName.PBSTypeCodeAllChild, PBSTypeCode));

			EntityData entityU;

			DataTable m_DataTable;
			DataView m_DV;

			switch (m_strGetType.ToLower())
			{
				case "pbsunit":
					#region 取第一层 单位工程，下分楼栋

					entityU = DAL.EntityDAO.PBSDAO.GetPBSUnitByProject(ProjectCode);
					entityU.Dispose();
					m_DataTable = entityU.CurrentTable;
					m_DV = new DataView(m_DataTable);

					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRowPBSUnit(ref m_NewRow,m_Row, true);
					
						m_Table.Rows.Add(m_NewRow);
					}

					break;
					#endregion

				case "onlypbsunit":
					#region 取单位工程

					entityU = DAL.EntityDAO.PBSDAO.GetPBSUnitByProject(ProjectCode);
					entityU.Dispose();
					m_DataTable = entityU.CurrentTable;
					m_DV = new DataView(m_DataTable);

					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRowPBSUnit(ref m_NewRow,m_Row, false);
					
						m_Table.Rows.Add(m_NewRow);
					}

					break;
					#endregion

				case "buildingofpbsunit":
					#region 取某单位工程下的楼栋

					sb.AddStrategy(new Strategy(BuildingStrategyName.PBSUnitCode, m_strNodeId));
					sb.AddOrder("BuildingName", true);
					m_DataTable = BuildStrategy(sb);
					m_DV = new DataView(m_DataTable);

					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row);
					
						m_Table.Rows.Add(m_NewRow);
					}

					break;
					#endregion

				case "building":
					#region 取所有楼栋

					sb.AddStrategy(new Strategy(BuildingStrategyName.IsArea, "2"));
					sb.AddOrder("BuildingName", true);
					m_DataTable = BuildStrategy(sb);
					m_DV = new DataView(m_DataTable);

					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row);
					
						m_Table.Rows.Add(m_NewRow);
					}

					break;
					#endregion

				default:
					break;
			}

			Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
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
				EntityData entity = qa.FillEntityData( "V_Building",sql );
				qa.Dispose();

				return entity.CurrentTable;
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		private void FillRootRow(ref DataRow m_NewRow)
		{
			m_NewRow["BuildingCode"] = "";
			m_NewRow["ParentCode"] = "";
			m_NewRow["BuildingName"] = "所有楼栋";
			m_NewRow["Description"] = "";
			m_NewRow["Layer"]=1;
			m_NewRow["ChildNodesCount"]=1;

			m_NewRow["ShowChildNodes"]="0";

			m_NewRow["NodeType"] = "root";

/*			string s = JavaScript.ScriptStart + "\n";
			s = s + "var NodeType = 'root';" + "\n";
			s = s + JavaScript.ScriptEnd + "\n";
			Page.RegisterStartupScript("SetNodeType", s);
*/

		}

		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row)
		{
			try 
			{
				m_NewRow["Layer"] = 2;
				m_NewRow["BuildingCode"] = m_Row["BuildingCode"];
				m_NewRow["BuildingName"] = m_Row["BuildingName"];

				m_NewRow["ChildNodesCount"] = 0;

				m_NewRow["ShowChildNodes"]="0";
				m_NewRow["NodeType"] = "";

				//只显示楼栋上的CheckBox，单位工程不显示CheckBox
				m_NewRow["IsPBSUnit"] = "0";
				m_NewRow["NoSelectPBSUnit"] = "block";

				//显示图标
				m_NewRow["IconName"] = "Building.gif";

				//单位工程名称
				if (m_Row.Row.Table.Columns.Contains("PBSUnitName"))
				{
					m_NewRow["PBSUnitName"] = m_Row["PBSUnitName"];
				}
				else
				{
					m_NewRow["PBSUnitName"] = BLL.PBSRule.GetPBSUnitName(m_Row["PBSUnitCode"]);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void FillRowPBSUnit(ref DataRow m_NewRow,DataRowView m_Row, bool ShowChild)
		{
			try 
			{
				m_NewRow["Layer"] = 1;
				m_NewRow["BuildingCode"] = m_Row["PBSUnitCode"];
				m_NewRow["BuildingName"] = m_Row["PBSUnitName"];

				int ChildCount = 0;

				if (ShowChild) 
				{
					EntityData entityB = DAL.EntityDAO.ProductDAO.GetBuildingByPBSUnitCode(m_Row["PBSUnitCode"].ToString());
					ChildCount = entityB.CurrentTable.Rows.Count;
				}
				m_NewRow["ChildNodesCount"] = ChildCount;

				m_NewRow["ShowChildNodes"] = "0";
				m_NewRow["NodeType"] = "";

				//只显示楼栋上的CheckBox，单位工程不显示CheckBox
				m_NewRow["IsPBSUnit"] = "1";
				m_NewRow["NoSelectPBSUnit"] = "none";

				//显示图标
				m_NewRow["IconName"] = "BuildingArea.gif";

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		private void FillSelectedLayerData( string BuildingCode,int m_iNowLayer,int m_iStopLayer)
		{
			try 
			{
				DataView m_DV=new DataView(m_DataTable,"ParentCode like '"+BuildingCode+"'","",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();
					this.FillRow(ref m_NewRow,m_Row);
					m_Table.Rows.Add(m_NewRow);
					if(m_iStopLayer>m_iNowLayer)
					{
						m_NewRow["ShowChildNodes"]="1";
						this.FillSelectedLayerData(m_Row["BuildingCode"].ToString(),m_iNowLayer+1,m_iStopLayer);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private void FillAllData(string BuildingCode,int m_iNowLayer)
		{
			try 
			{
				DataView m_DV=new DataView(m_DataTable,"ParentCode='"+BuildingCode+"'","",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();
					this.FillRow(ref m_NewRow,m_Row);
					m_Table.Rows.Add(m_NewRow);
					if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
					{
						m_NewRow["ShowChildNodes"]="1";
						this.FillAllData(m_Row["BuildingCode"].ToString(),m_iNowLayer+1);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
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
