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
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PATreeData 的摘要说明。
	/// </summary>
	public partial class PATreeData : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{

				string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
//				string m_strLayer=Request.QueryString["Layer"]+"";					//需要取的层数
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
//				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
//				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度
//				string fromNodeCode = Request["FromNodeCode"]+"";

				DataTable m_Table=new DataTable("Building");
				m_Table.Columns.Add("AlloType");					// 分摊类型
				m_Table.Columns.Add("BuildingName");				// 楼栋名称
				m_Table.Columns.Add("BuildingCode");				// 楼栋编号

				m_Table.Columns.Add("Layer");						// 层数－－－对应deep深度
				m_Table.Columns.Add("ChildNodesCount");				// 子节点数目
				m_Table.Columns.Add("ShowChildNodes");				// 是否显示子节点

//				DataTable m_DataTable=m_Cost.CurrentTable;

//				string filter = "";
				if(m_strGetType=="")
				{
					#region 取第一层
					DataRow m_NewRow=m_Table.NewRow();
					m_NewRow["AlloType"]="P";
					m_NewRow["BuildingCode"]="Project";
					m_NewRow["BuildingName"]="项目";
					m_Table.Rows.Add(m_NewRow);

					EntityData entityPBSUnit = DAL.EntityDAO.PBSDAO.GetPBSUnitByProject(projectCode);
					int pbsUnitCount = entityPBSUnit.CurrentTable.Rows.Count;
					entityPBSUnit.Dispose();

					DataTable noInPBSUnitBuilding = BLL.PBSRule.GetBuildingNoPBSUnit(projectCode);
					int noInPBSBuildingCount = noInPBSUnitBuilding.Rows.Count;
					noInPBSUnitBuilding.Dispose();

					m_NewRow["Layer"]=1;
					m_NewRow["ChildNodesCount"]=pbsUnitCount + noInPBSBuildingCount;
					m_NewRow["ShowChildNodes"]=0;

					#endregion
				}
				else if(m_strGetType=="ChildNodes")
				{
					#region 取某节点子目录

					if ( m_strNodeId == "Project" )
					{
						EntityData entityPBSUnit = DAL.EntityDAO.PBSDAO.GetPBSUnitByProject(projectCode);
						foreach ( DataRow dr in entityPBSUnit.CurrentTable.Rows )
						{
							DataRow m_NewRow=m_Table.NewRow();
							m_NewRow["AlloType"]="U";
							m_NewRow["BuildingCode"]= "PBSUnit" + (string)dr["PBSUnitCode"];
							m_NewRow["BuildingName"]=dr["PBSUnitName"];

							m_NewRow["Layer"]=2;
							m_NewRow["ChildNodesCount"]=dr["ChildNodesCount"];
							m_NewRow["ShowChildNodes"]=0;
							m_Table.Rows.Add(m_NewRow);
						}
						entityPBSUnit.Dispose();

						DataTable noInPBSUnitBuilding = BLL.PBSRule.GetBuildingNoPBSUnit(projectCode);
						foreach ( DataRow dr in noInPBSUnitBuilding.Rows )
						{
							DataRow m_NewRow=m_Table.NewRow();
							m_NewRow["AlloType"]="B";
							m_NewRow["BuildingCode"]= "Building" + (string) dr["BuildingCode"];
							m_NewRow["BuildingName"]=dr["BuildingName"];
							m_NewRow["Layer"]=2;
							m_NewRow["ChildNodesCount"]=0;
							m_NewRow["ShowChildNodes"]=1;
							m_Table.Rows.Add(m_NewRow);
						}						
						noInPBSUnitBuilding.Dispose();

					}
					else if (  (m_strNodeId.Length>= 3 ) &&  ( m_strNodeId.Substring(0,3) == "PBS" ) )
					{
						string pbsUnitCode = m_strNodeId.Replace("PBSUnit","");
						EntityData building = DAL.EntityDAO.ProductDAO.GetBuildingByPBSUnitCode(pbsUnitCode);
						foreach ( DataRow dr in building.CurrentTable.Rows)
						{
							DataRow m_NewRow=m_Table.NewRow();
							m_NewRow["AlloType"]="B";
							m_NewRow["BuildingCode"]= "Building" + (string) dr["BuildingCode"];
							m_NewRow["BuildingName"]=dr["BuildingName"];
							m_NewRow["Layer"]=3;
							m_NewRow["ChildNodesCount"]=0;
							m_NewRow["ShowChildNodes"]=1;
							m_Table.Rows.Add(m_NewRow);
						}
						building.Dispose();
					}

					#endregion
				}
//				else if(m_strGetType=="SelectLayer")
//				{
//					#region 取指定层数结果
//
//					filter = "Deep='1'";
//
//					DataView m_DV=new DataView(m_DataTable,filter,"SortID",DataViewRowState.CurrentRows);
//					foreach(DataRowView m_Row in m_DV)
//					{
//						DataRow m_NewRow=m_Table.NewRow();
//						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
//					
//						m_Table.Rows.Add(m_NewRow);
//						if(int.Parse(m_strSelectedLayer)>1)
//						{
//							m_NewRow["ShowChildNodes"]="1";
//							this.FillSelectedLayerData(ref m_Table,m_Row["CostCode"].ToString(),2,int.Parse(m_strSelectedLayer),m_DataTable);
//						}
//					}
//					#endregion
//				}
//				else if(m_strGetType=="All")
//				{
//					#region 取所有结果
//
//					filter = "ParentCode='1'";
//					DataView m_DV=new DataView(m_DataTable,filter," SortID ",DataViewRowState.CurrentRows);
//					foreach(DataRowView m_Row in m_DV)
//					{
//						DataRow m_NewRow=m_Table.NewRow();
//
//						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
//					
//						m_Table.Rows.Add(m_NewRow);
//
//						if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
//						{
//							m_NewRow["ShowChildNodes"]="1";
//							this.FillAllData(ref m_Table,m_Row["CostCode"].ToString(),2,m_DataTable);
//						}
//					}
//					#endregion
//				}
//				else if(m_strGetType=="SingleNode")
//				{
//					#region 单个节点
//
//					filter = "CostCode='"+Request.QueryString["NodeId"]+""+"'";
//
//					DataView m_DV=new DataView(m_DataTable,filter," SortID ",DataViewRowState.CurrentRows);
//					foreach(DataRowView m_Row in m_DV)
//					{
//						DataRow m_NewRow=m_Table.NewRow();
//
//						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
//					
//						m_Table.Rows.Add(m_NewRow);
//
//					}
//					#endregion
//				}

				Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
				Response.End();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
//
//		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row , DataTable m_DataTable )
//		{
//
//
//			//当前记录是 flag ＝ －1 的预算版本； 
//			int iColumnCount = m_Row.DataView.Table.Columns.Count;
//			for ( int i =0 ; i<iColumnCount; i++)
//			{
//				string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
//				if ( m_NewRow.Table.Columns.Contains(columnName))
//				{
//
//					// 这几个数值特别处理
//					if ( columnName == "TotalMoney" || columnName == "UnitPrice" || columnName == "ProjectQuantity"  )
//					{
//						if( m_Row[columnName] != System.DBNull.Value )
//						{
//							decimal m = (decimal)m_Row[columnName];
//							if ( BLL.MathRule.CheckDecimalEqual(m,decimal.Zero))
//								m_NewRow[columnName] = "";
//							else
//							{
//								if ( columnName == "TotalMoney" )
//									m_NewRow[columnName] = BLL.StringRule.BuildMoneyWanFormatString(m);
//								else
//									m_NewRow[columnName] = BLL.StringRule.BuildShowNumberString(m);
//
//							}
//						}
//					}
//					else
//						m_NewRow[columnName] = m_Row[columnName];
//				}
//			}
//			
//			string costCode = (string) m_Row["CostCode"];
//			m_NewRow["Layer"]=m_Row["Deep"].ToString();
//			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
//			m_NewRow["ShowChildNodes"]="0";
//			m_NewRow["SubjectName"] = BLL.SubjectRule.GetSubjectName(m_NewRow["SubjectCode"].ToString(),m_SubjectSetCode);
//
//
//			string canAccess="0";
//			if ( !m_NewRow.IsNull("CanAccess"))
//				canAccess = (string)m_NewRow["CanAccess"];
//
//			if ( canAccess == "1" )
//			{
//				m_NewRow["ShowSpan"]="none";
//				m_NewRow["ShowHref"]="";
//			}
//			else
//			{
//				m_NewRow["ShowSpan"]="";
//				m_NewRow["ShowHref"]="none";
//			}
//
//
//		}
//
//
//
//		private void FillSelectedLayerData(ref DataTable m_Table,string m_strCostCode,int m_iNowLayer,int m_iStopLayer,DataTable m_DataTable)
//		{
//			DataView m_DV=new DataView(m_DataTable,"ParentCode like '"+m_strCostCode+"'","",DataViewRowState.CurrentRows);
//			foreach(DataRowView m_Row in m_DV)
//			{
//				DataRow m_NewRow=m_Table.NewRow();
//
//				this.FillRow(ref m_NewRow,m_Row, m_DataTable);
//
//				m_Table.Rows.Add(m_NewRow);
//				if(m_iStopLayer>m_iNowLayer)
//				{
//					m_NewRow["ShowChildNodes"]="1";
//					this.FillSelectedLayerData(ref m_Table,m_Row["CostCode"].ToString(),m_iNowLayer+1,m_iStopLayer,m_DataTable);
//				}
//			}
//		}
//
//		private void FillAllData(ref DataTable m_Table,string m_strCostCode,int m_iNowLayer,DataTable m_DataTable)
//		{
//			DataView m_DV=new DataView(m_DataTable,"ParentCode='"+m_strCostCode+"'","",DataViewRowState.CurrentRows);
//			foreach(DataRowView m_Row in m_DV)
//			{
//				DataRow m_NewRow=m_Table.NewRow();
//
//				this.FillRow(ref m_NewRow,m_Row, m_DataTable);
//
//				m_Table.Rows.Add(m_NewRow);
//				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
//				{
//					m_NewRow["ShowChildNodes"]="1";
//					this.FillAllData(ref m_Table,m_Row["CostCode"].ToString(),m_iNowLayer+1,m_DataTable);
//				}
//			}
//		}

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
