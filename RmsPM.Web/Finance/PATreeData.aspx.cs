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
	/// PATreeData ��ժҪ˵����
	/// </summary>
	public partial class PATreeData : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{

				string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
//				string m_strLayer=Request.QueryString["Layer"]+"";					//��Ҫȡ�Ĳ���
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
//				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
//				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������
//				string fromNodeCode = Request["FromNodeCode"]+"";

				DataTable m_Table=new DataTable("Building");
				m_Table.Columns.Add("AlloType");					// ��̯����
				m_Table.Columns.Add("BuildingName");				// ¥������
				m_Table.Columns.Add("BuildingCode");				// ¥�����

				m_Table.Columns.Add("Layer");						// ������������Ӧdeep���
				m_Table.Columns.Add("ChildNodesCount");				// �ӽڵ���Ŀ
				m_Table.Columns.Add("ShowChildNodes");				// �Ƿ���ʾ�ӽڵ�

//				DataTable m_DataTable=m_Cost.CurrentTable;

//				string filter = "";
				if(m_strGetType=="")
				{
					#region ȡ��һ��
					DataRow m_NewRow=m_Table.NewRow();
					m_NewRow["AlloType"]="P";
					m_NewRow["BuildingCode"]="Project";
					m_NewRow["BuildingName"]="��Ŀ";
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
					#region ȡĳ�ڵ���Ŀ¼

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
//					#region ȡָ���������
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
//					#region ȡ���н��
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
//					#region �����ڵ�
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
//			//��ǰ��¼�� flag �� ��1 ��Ԥ��汾�� 
//			int iColumnCount = m_Row.DataView.Table.Columns.Count;
//			for ( int i =0 ; i<iColumnCount; i++)
//			{
//				string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
//				if ( m_NewRow.Table.Columns.Contains(columnName))
//				{
//
//					// �⼸����ֵ�ر���
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

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
