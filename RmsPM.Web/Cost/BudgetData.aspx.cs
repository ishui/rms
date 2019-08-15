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

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// BudgetData ��ժҪ˵����
	/// </summary>
	public partial class BudgetData : PageBase
	{
		private string m_CheckBalance = "";
		private string m_TreeType = "";
		private string m_BudgetCode = "";

		private EntityData m_CBSCost = null;
		private EntityData m_BudgetData = null;
		private EntityData m_CanAccessBudget = null;
		private EntityData m_CanAccessCost = null;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string projectCode = Request["ProjectCode"] + "";
			try
			{
				m_TreeType = Request["TreeType"] + "";
				m_CheckBalance = Request["CheckBalance"] + "";

				string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
				string m_strLayer=Request.QueryString["Layer"]+"";					//��Ҫȡ�Ĳ���
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������
				string fromNodeCode = Request["FromNodeCode"]+"";

				DataTable m_Table=new DataTable("Budget");
				m_Table.Columns.Add("CostCode");					// ���ñ��
				m_Table.Columns.Add("ParentCode");					// ��Codd
				m_Table.Columns.Add("CostName");					// ����������
				m_Table.Columns.Add("Description");					// ˵��
				m_Table.Columns.Add("Layer");						// ������������Ӧdeep���
				m_Table.Columns.Add("ChildNodesCount");				// �ӽڵ���Ŀ
				m_Table.Columns.Add("ShowChildNodes");				// �Ƿ���ʾ�ӽڵ�

				m_Table.Columns.Add("SubjectCode");					// ��Ŀ���
				m_Table.Columns.Add("SortID");						// ��������
				m_Table.Columns.Add("TotalMoney");					// �������
				m_Table.Columns.Add("BudgetCost");					// Ԥ�����
				m_Table.Columns.Add("BeforeHappenCost");			// ��ǰ����
				m_Table.Columns.Add("CurrentPlanCost");				// ����ƻ�
				m_Table.Columns.Add("AfterPlanCost");				// ʣ��Ԥ��
				m_Table.Columns.Add("BudgetBalanceSign");			// ʣ��Ԥ��
				m_Table.Columns.Add("Display");						// �����磩��Ҷ�ڵ㲻��ʾ ��ѡ��
				m_Table.Columns.Add("CanAccess",System.Type.GetType("System.String"));	// �ܲ鿴
				m_Table.Columns.Add("ShowSpan");
				m_Table.Columns.Add("ShowHref");

				// ȡ���÷ֽ�ṹ�͹������

				string stationCodes = user.BuildStationCodes();
				m_CBSCost = BLL.CBSRule.GetAccessCostOperation(projectCode,user.UserCode,stationCodes,"040101",true);
				m_CanAccessCost = BLL.CBSRule.GetAccessV_CBSCost(projectCode,user.UserCode,stationCodes,false , fromNodeCode);
				m_CanAccessBudget = BLL.CBSRule.GetAccessCostOperation(projectCode,user.UserCode,stationCodes,"040301",false);


				// ȡ����Ԥ��
				// ���û��Ԥ���ţ���ʾ�տ��	
				m_BudgetCode = Request["BudgetCode"] + "";
				if ( m_BudgetCode != "" )
				{
					m_BudgetData = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(m_BudgetCode);
					this.m_BudgetData.SetCurrentTable("BudgetCost");
				}

				DataTable m_DataTable=m_CBSCost.CurrentTable;
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
				m_CBSCost.Dispose();
				if ( m_BudgetCode != "" )
					this.m_BudgetData.Dispose();
				this.m_CanAccessBudget.Dispose();
				this.m_CanAccessCost.Dispose();

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
//					if ( columnName == "TotalMoney"   )
//					{
//						m_NewRow[columnName] = BLL.MathRule.GetWanDecimalShowString(m_Row,columnName);
//					}
//					else
					m_NewRow[columnName] = m_Row[columnName];

				}
			}

			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

			if ( m_Row["ChildCount"] != System.DBNull.Value )
			{
				int iChildCount = (int) m_Row["ChildCount"];
				if ( iChildCount > 0)
				{
					m_NewRow["Display"] = "none";
				}
			}

			string costCode = (string) m_Row["CostCode"];
			string fullCode = (string) m_Row["FullCode"];
			int deep = (int) m_Row["Deep"];
			int nextDeep = deep+1;

			m_NewRow["ShowSpan"]="";
			m_NewRow["ShowHref"]="none";
			if ( this.m_BudgetCode != "" )
			{

				decimal budgetCost = decimal.Zero;

				// Ԥ������Ȩ��
				DataRow[] drsB = this.m_CanAccessBudget.CurrentTable.Select( String.Format( " CostCode='{0}' " ,costCode ) );
				if ( drsB.Length> 0)
				{

					//ȡ����Ԥ����
					DataRow[] drs = this.m_BudgetData.CurrentTable.Select( String.Format( " CostCode='{0}' " ,costCode )  );
					if ( drs.Length>0)
					{
						DataRow dr = drs[0];
						m_NewRow["BudgetCost"] = BLL.MathRule.GetWanDecimalShowString(drs[0],"BudgetCost");
						m_NewRow["BeforeHappenCost"] = BLL.MathRule.GetWanDecimalShowString(drs[0],"BeforeHappenCost");
						m_NewRow["CurrentPlanCost"] = BLL.MathRule.GetWanDecimalShowString(drs[0],"CurrentPlanCost");
						m_NewRow["AfterPlanCost"] = BLL.MathRule.GetWanDecimalShowString(drs[0],"AfterPlanCost");
						if ( !drs[0].IsNull("BudgetCost"))
							budgetCost = (decimal) drs[0]["BudgetCost"];
					}

					m_NewRow["ShowSpan"]="none";
					m_NewRow["ShowHref"]="";

				}
				else
				{
					m_NewRow["BudgetCost"] = "-----";
					m_NewRow["BeforeHappenCost"] = "-----";
					m_NewRow["CurrentPlanCost"] = "-----";
					m_NewRow["AfterPlanCost"] = "-----";
				}
			}

			//  ��������Ȩ��
			m_NewRow["TotalMoney"] = "-----";
			DataRow[] drsVCost = this.m_CanAccessCost.CurrentTable.Select(String.Format("CostCode='{0}'",costCode));
			if ( drsVCost.Length > 0 )
			{
				m_NewRow["TotalMoney"] = BLL.StringRule.BuildMoneyWanFormatString(drsVCost[0]["TotalMoney"]);
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
