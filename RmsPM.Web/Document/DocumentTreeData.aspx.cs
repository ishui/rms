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


namespace RmsPM.Web.Document
{
	/// <summary>
	/// DocumentTreeData ��ժҪ˵����
	/// </summary>
	public partial class DocumentTreeData : PageBase
	{

		// �����ĵ����͵����ݱ�
		private DataTable m_DataTable = null;

		// ���淵�����ݵ����ݱ�
		private DataTable m_Table = null ;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
			string m_strLayer=Request.QueryString["Layer"]+"";					//��Ҫȡ�Ĳ���
			string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
			string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
			string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������

			m_Table=new DataTable("DocumentType");
			m_Table.Columns.Add("DocumentTypeCode");
			m_Table.Columns.Add("ParentCode");
			m_Table.Columns.Add("TypeName");
			m_Table.Columns.Add("Description");
			m_Table.Columns.Add("Layer");
			m_Table.Columns.Add("ChildNodesCount");
			m_Table.Columns.Add("ShowChildNodes");
			m_Table.Columns.Add("NodeType");

			EntityData entity = DocumentDAO.GetAllDocumentType();
			m_DataTable=entity.CurrentTable;

			if(m_strGetType=="")
			{
				#region ȡ��һ��
				DataRow m_NewRow=m_Table.NewRow();
				this.FillRootRow(ref m_NewRow);
				m_Table.Rows.Add(m_NewRow);

				//ȱʡ�򿪸��ڵ�
//				DataView m_DV=new DataView(m_DataTable,"isnull(ParentCode,'')=''","",DataViewRowState.CurrentRows);
//				foreach(DataRowView m_Row in m_DV)
//				{
//					DataRow m_NewRow2=m_Table.NewRow();
//					
//					this.FillRow(ref m_NewRow2,m_Row);
//
//					m_Table.Rows.Add(m_NewRow2);
//				}

				#endregion
			}
			else if(m_strGetType=="Fix")
			{
				#region ȡĳ�ڵ���Ŀ¼
				DataView m_DV=new DataView(m_DataTable,"isnull(ParentCode,'')='"+m_strNodeId+"' and Fixed=1","",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);
				}
				#endregion
			}
			else if(m_strGetType=="ChildNodes")
			{
				#region ȡĳ�ڵ���Ŀ¼
				DataView m_DV=new DataView(m_DataTable,"isnull(ParentCode,'')='"+m_strNodeId+"' and isnull(Fixed,0)=0","",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);
				}
				#endregion
			}
			else if(m_strGetType=="SelectLayer")
			{
				#region ȡ�ƶ��������
				DataView m_DV=new DataView(m_DataTable,"Deep='1'","",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);
					if(int.Parse(m_strSelectedLayer)>1)
					{
						m_NewRow["ShowChildNodes"]="1";
						this.FillSelectedLayerData(m_Row["DocumentTypeCode"].ToString(),2,int.Parse(m_strSelectedLayer));
					}
				}
				#endregion
			}
			else if(m_strGetType=="All")
			{
				#region ȡ���н��
				DataView m_DV=new DataView(m_DataTable,"ParentCode='1'","",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);

					if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
					{
						m_NewRow["ShowChildNodes"]="1";
						this.FillAllData(m_Row["DocumentTypeCode"].ToString(),2);
					}
				}
				#endregion
			}
			else if(m_strGetType=="SingleNode")
			{
				#region �����ڵ�
				DataView m_DV=new DataView(m_DataTable,"DocumentTypeCode='"+Request.QueryString["NodeId"]+""+"'","",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);

				}
				#endregion
			}
			entity.Dispose();

			Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
			Response.End();
		}

		private void FillRootRow(ref DataRow m_NewRow)
		{
			m_NewRow["DocumentTypeCode"] = "";
			m_NewRow["ParentCode"] = "";
			m_NewRow["TypeName"] = "��������";
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

			int iColumnCount = m_Row.DataView.Table.Columns.Count;
			for ( int i =0 ; i<iColumnCount; i++)
			{
				string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
				if ( m_NewRow.Table.Columns.Contains(columnName))
					m_NewRow[columnName] = m_Row[columnName];
			}

			m_NewRow["Layer"]=int.Parse(m_Row["Deep"].ToString()) + 1;
			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";
			m_NewRow["NodeType"] = "";
		}


		private void FillSelectedLayerData( string DocumentTypeCode,int m_iNowLayer,int m_iStopLayer)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode like '"+DocumentTypeCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();
				this.FillRow(ref m_NewRow,m_Row);
				m_Table.Rows.Add(m_NewRow);
				if(m_iStopLayer>m_iNowLayer)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillSelectedLayerData(m_Row["DocumentTypeCode"].ToString(),m_iNowLayer+1,m_iStopLayer);
				}
			}
		}

		private void FillAllData(string DocumentTypeCode,int m_iNowLayer)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode='"+DocumentTypeCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();
				this.FillRow(ref m_NewRow,m_Row);
				m_Table.Rows.Add(m_NewRow);
				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillAllData(m_Row["DocumentTypeCode"].ToString(),m_iNowLayer+1);
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
