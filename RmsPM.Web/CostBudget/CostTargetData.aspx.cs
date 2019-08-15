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

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostTargetData ��ժҪ˵����
	/// </summary>
	public partial class CostTargetData : PageBase
	{
		private string m_SubjectSetCode = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			m_SubjectSetCode = BLL.ProjectRule.GetSubjectSetCodeByProject(projectCode);
			try
			{

				string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
				string m_strLayer=Request.QueryString["Layer"]+"";					//��Ҫȡ�Ĳ���
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������
				string fromNodeCode = Request["FromNodeCode"]+"";

				DataTable m_Table=new DataTable("CBS");
				m_Table.Columns.Add("CostCode");					// ���ñ��
				m_Table.Columns.Add("ParentCode");					// ��Code
				m_Table.Columns.Add("CostName");					// ����������
				m_Table.Columns.Add("Description");					// ˵��
				m_Table.Columns.Add("Layer");						// ������������Ӧdeep���
				m_Table.Columns.Add("ChildNodesCount");				// �ӽڵ���Ŀ
				m_Table.Columns.Add("ShowChildNodes");				// �Ƿ���ʾ�ӽڵ�

				m_Table.Columns.Add("SubjectCode");					// ��Ŀ���
				m_Table.Columns.Add("SubjectName");					// ��Ŀ����

				m_Table.Columns.Add("SortID");						// ��������
				m_Table.Columns.Add("CostAllocationDescription");	// ���÷ֽ�˵��

				m_Table.Columns.Add("CanAccess",System.Type.GetType("System.String"));	// �ܲ鿴
				m_Table.Columns.Add("ShowSpan");
				m_Table.Columns.Add("ShowHref");

				m_Table.Columns.Add("TotalTargetMoney");
				m_Table.Columns.Add("TotalBudgetMoney");

//				m_Table.Columns.Add("IsFixed");						// ����



				EntityData m_Cost = BLL.CBSRule.GetAccessCBS(projectCode,user.UserCode,user.BuildStationCodes());

				DataTable m_DataTable=m_Cost.CurrentTable;

				string filter = "";
				if(m_strGetType=="")
				{
					#region ȡ��һ��

					filter = " isnull(ParentCode,'') =''";

					DataView m_DV=new DataView(m_DataTable,filter," SortID ",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();
					
						this.FillRow(ref m_NewRow,m_Row,m_DataTable);

						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="ChildNodes")
				{
					#region ȡĳ�ڵ���Ŀ¼

					filter = "ParentCode='"+m_strNodeId+"'";
					DataView m_DV=new DataView(m_DataTable,filter,"SortID",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="SelectLayer")
				{
					#region ȡָ���������

					filter = "Deep='1'";

					DataView m_DV=new DataView(m_DataTable,filter,"SortID",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();
						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);
						if(int.Parse(m_strSelectedLayer)>1)
						{
							m_NewRow["ShowChildNodes"]="1";
							this.FillSelectedLayerData(ref m_Table,m_Row["CostCode"].ToString(),2,int.Parse(m_strSelectedLayer),m_DataTable);
						}
					}
					#endregion
				}
				else if(m_strGetType=="All")
				{
					#region ȡ���н��

					filter = "ParentCode='1'";
					DataView m_DV=new DataView(m_DataTable,filter," SortID ",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);

						if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
						{
							m_NewRow["ShowChildNodes"]="1";
							this.FillAllData(ref m_Table,m_Row["CostCode"].ToString(),2,m_DataTable);
						}
					}
					#endregion
				}
				else if(m_strGetType=="SingleNode")
				{
					#region �����ڵ�

					filter = "CostCode='"+Request.QueryString["NodeId"]+""+"'";

					DataView m_DV=new DataView(m_DataTable,filter," SortID ",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);

					}
					#endregion
				}
				m_Cost.Dispose();

				Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
				Response.End();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row , DataTable m_DataTable )
		{


			//��ǰ��¼�� flag �� ��1 ��Ԥ��汾�� 
			int iColumnCount = m_Row.DataView.Table.Columns.Count;
			for ( int i =0 ; i<iColumnCount; i++)
			{
				string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
				if ( m_NewRow.Table.Columns.Contains(columnName))
				{

					// �⼸����ֵ�ر���
					if ( columnName == "TotalMoney" || columnName == "UnitPrice" || columnName == "ProjectQuantity" || columnName == "TotalTargetMoney" || columnName == "TotalBudgetMoney")
					{
						if( m_Row[columnName] != System.DBNull.Value )
						{
							decimal m = (decimal)m_Row[columnName];
							if ( BLL.MathRule.CheckDecimalEqual(m,decimal.Zero))
								m_NewRow[columnName] = "";
							else
							{
								if (( columnName == "TotalMoney" ) || ( columnName == "TotalTargetMoney" ) || ( columnName == "TotalBudgetMoney" ))
									m_NewRow[columnName] = BLL.StringRule.BuildMoneyWanFormatString(m);
								else
									m_NewRow[columnName] = BLL.StringRule.BuildShowNumberString(m);

							}
						}
					}
					else
						m_NewRow[columnName] = m_Row[columnName];
				}
			}
			
			string costCode = (string) m_Row["CostCode"];
			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";
			m_NewRow["SubjectName"] = BLL.SubjectRule.GetSubjectName(m_NewRow["SubjectCode"].ToString(),m_SubjectSetCode);


			string canAccess="0";
			if ( !m_NewRow.IsNull("CanAccess"))
				canAccess = (string)m_NewRow["CanAccess"];

			if ( canAccess == "1" )
			{
				m_NewRow["ShowSpan"]="none";
				m_NewRow["ShowHref"]="";
			}
			else
			{
				m_NewRow["ShowSpan"]="";
				m_NewRow["ShowHref"]="none";
			}


		}



		private void FillSelectedLayerData(ref DataTable m_Table,string m_strCostCode,int m_iNowLayer,int m_iStopLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode like '"+m_strCostCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row, m_DataTable);

				m_Table.Rows.Add(m_NewRow);
				if(m_iStopLayer>m_iNowLayer)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillSelectedLayerData(ref m_Table,m_Row["CostCode"].ToString(),m_iNowLayer+1,m_iStopLayer,m_DataTable);
				}
			}
		}

		private void FillAllData(ref DataTable m_Table,string m_strCostCode,int m_iNowLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode='"+m_strCostCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row, m_DataTable);

				m_Table.Rows.Add(m_NewRow);
				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillAllData(ref m_Table,m_Row["CostCode"].ToString(),m_iNowLayer+1,m_DataTable);
				}
			}
		}

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
