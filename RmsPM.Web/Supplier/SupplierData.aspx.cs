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
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;
using RmsPM.Web;
using Rms.ORMap;


namespace RmsPM.Web.Supplier
{
	/// <summary>
	/// Supplier ��ժҪ˵����
	/// </summary>
	public partial class SupplierData : PageBase
	{

		//������
		private string m_TreeType = "";

//		// ���й�Ӧ��
//		private DataTable m_DataTableSupplier = null ;

		// ���й�Ӧ�����͵����ݱ�
		private DataTable m_DataTableSupplierType = null;

		// ���淵�����ݵ����ݱ�
		private DataTable m_Table = null ;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				m_TreeType = Request["TreeType"] + "";

				string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
				string m_strLayer=Request.QueryString["Layer"]+"";					//��Ҫȡ�Ĳ���
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������

				m_Table=new DataTable("SupplierList");
				m_Table.Columns.Add("GroupCode");
				m_Table.Columns.Add("ParentCode");
				m_Table.Columns.Add("GroupName");
				m_Table.Columns.Add("Description");
				m_Table.Columns.Add("Layer");
				m_Table.Columns.Add("ChildNodesCount");
				m_Table.Columns.Add("ShowChildNodes");
				m_Table.Columns.Add("SortID");
//
//				m_Table.Columns.Add("SupplierCode");
//				m_Table.Columns.Add("SupplierName");

				EntityData entityDataSupplierType = DAL.EntityDAO.SystemManageDAO.GetSystemGroupByClassCode("1401");
				m_DataTableSupplierType = entityDataSupplierType.CurrentTable;

//				EntityData entityDataSupplier = DAL.EntityDAO.ProjectDAO.GetAllSupplier();
//				m_DataTableSupplier = entityDataSupplier.CurrentTable;

				string filter = "";
				switch (m_strGetType)
				{
					case "":
						filter = " isnull(ParentCode,'') =''";
						break;
					case "ChildNodes":
						filter = "ParentCode='"+m_strNodeId+"'";
						break;
					case "SelectLayer":
						filter = "Deep='1'";
						break;
					case "All":
						filter = "ParentCode='1'";
						break;
					case "SingleNode":
						filter = "CostCode='"+Request.QueryString["NodeId"]+""+"'";
						break;
				}

				DataView m_DV=new DataView(m_DataTableSupplierType,filter,"",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();
					this.FillRow(ref m_NewRow,m_Row);
					m_Table.Rows.Add(m_NewRow);

					if(m_strGetType=="SelectLayer")
					{
						if(int.Parse(m_strSelectedLayer)>1)
						{
							m_NewRow["ShowChildNodes"]="1";
							this.FillSelectedLayerData(m_Row["GroupCode"].ToString(),2,int.Parse(m_strSelectedLayer));
						}
					}

					if(m_strGetType=="All")
					{
						if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
						{
							m_NewRow["ShowChildNodes"]="1";
							this.FillAllData( m_Row["GroupCode"].ToString(),2);
						}
					}
				}

				entityDataSupplierType.Dispose();
//				entityDataSupplier.Dispose();

				Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
				Response.End();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row)
		{

			int iColumnCount = m_Row.DataView.Table.Columns.Count;
			for ( int i =0 ; i<iColumnCount; i++)
			{
				string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
				if ( m_NewRow.Table.Columns.Contains(columnName))
					m_NewRow[columnName] = m_Row[columnName];
			}

			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildNodesCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

		}


		private void FillSelectedLayerData( string GroupCode,int m_iNowLayer,int m_iStopLayer)
		{
			DataView m_DV=new DataView(m_DataTableSupplierType,"ParentCode like '"+GroupCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();
				this.FillRow(ref m_NewRow,m_Row);
				m_Table.Rows.Add(m_NewRow);
				if(m_iStopLayer>m_iNowLayer)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillSelectedLayerData(m_Row["GroupCode"].ToString(),m_iNowLayer+1,m_iStopLayer);
				}
			}
		}

		private void FillAllData(string GroupCode,int m_iNowLayer)
		{
			DataView m_DV=new DataView(m_DataTableSupplierType,"ParentCode='"+GroupCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();
				this.FillRow(ref m_NewRow,m_Row);
				m_Table.Rows.Add(m_NewRow);
				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillAllData(m_Row["GroupCode"].ToString(),m_iNowLayer+1);
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
