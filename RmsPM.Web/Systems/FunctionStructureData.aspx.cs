/*
�ر�˵����
	IsAvailable �Ƿ���Ч
		0�� ��Ч�� 1 ��Ч

	IsRightControlPoint Ȩ�޿��Ƶ�
		0�� ��Ȩ�޿��ƣ� 1 û��Ȩ�޿���

	IsRoleControlPoint  ��ɫ�����Ƿ��ܿ��Ƹ�Ȩ�ޣ� ��ЩȨ������systemgroupclass�н��п��Ƶ�
		0�� ���ڽ�ɫ�����п��ƣ� 1 �����ڽ�ɫ�����п���
		
	IsSystemClass	�Ƿ�ϵͳ���Ĵ��� ( ���ɽ�ɫ������п��ƣ���ϵͳ�������ƵĴ��� )
		1��	��	��	0����


*/
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

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// FunctionStructureData ��ժҪ˵����
	/// </summary>
	public partial class FunctionStructureData : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{

			try
			{
				string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
				string m_strLayer=Request.QueryString["Layer"]+"";					//��Ҫȡ�Ĳ���
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������

				DataTable m_Table=new DataTable("FunctionStructure");
				m_Table.Columns.Add("FunctionStructureCode");					// ���ñ��
				m_Table.Columns.Add("ParentCode");					// ��Code
				m_Table.Columns.Add("FunctionStructureName");					// ����������
				m_Table.Columns.Add("Description");					// ˵��
				m_Table.Columns.Add("Layer");						// ������������Ӧdeep���
				m_Table.Columns.Add("ChildNodesCount");				// �ӽڵ���Ŀ
				m_Table.Columns.Add("ShowChildNodes");				// �Ƿ���ʾ�ӽڵ�

				m_Table.Columns.Add("IsAvailable");						// �Ƿ��ɫ���Ƶ�
				m_Table.Columns.Add("IsRightControlPoint");				// �Ƿ�Ȩ�޿��Ƶ�
				m_Table.Columns.Add("IsSystemClass");					// �Ƿ�ϵͳ������
				m_Table.Columns.Add("IsRoleControlPoint");				// �Ƿ��ɫ���Ƶ�


				EntityData m_FS = DAL.EntityDAO.SystemManageDAO.GetAllFunctionStructure();

				DataTable m_DataTable=m_FS.CurrentTable;

				string filter = "";
				if(m_strGetType=="")
				{
					#region ȡ��һ��

					filter = " isnull(ParentCode,'') =''";

					DataView m_DV=new DataView(m_DataTable,filter,"FunctionStructureCode",DataViewRowState.CurrentRows);
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
					DataView m_DV=new DataView(m_DataTable,filter,"FunctionStructureCode",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);
					}
					#endregion
				}
				else if(m_strGetType=="SingleNode")
				{
					#region �����ڵ�

					filter = "FunctionStructureCode='"+Request.QueryString["NodeId"]+""+"'";

					DataView m_DV=new DataView(m_DataTable,filter," FunctionStructureCode ",DataViewRowState.CurrentRows);
					foreach(DataRowView m_Row in m_DV)
					{
						DataRow m_NewRow=m_Table.NewRow();

						this.FillRow(ref m_NewRow,m_Row,m_DataTable);
					
						m_Table.Rows.Add(m_NewRow);

					}
					#endregion
				}
				m_FS.Dispose();

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

			int iColumnCount = m_Row.DataView.Table.Columns.Count;
			for ( int i =0 ; i<iColumnCount; i++)
			{
				string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
				if ( m_NewRow.Table.Columns.Contains(columnName))
				{
					m_NewRow[columnName] = m_Row[columnName];
				}
			}
			
			if ( m_Row["IsRightControlPoint"].ToString() == "1" )
				m_NewRow["IsRightControlPoint"] = "��";
			else
				m_NewRow["IsRightControlPoint"] = "��";

			if ( m_Row["IsAvailable"].ToString() == "1" )
				m_NewRow["IsAvailable"] = "��";
			else
				m_NewRow["IsAvailable"] = "��";

			if ( m_Row["IsRoleControlPoint"].ToString() == "1" )
				m_NewRow["IsRoleControlPoint"] = "��";
			else
				m_NewRow["IsRoleControlPoint"] = "��";

			if ( m_Row["IsSystemClass"].ToString() == "1" )
				m_NewRow["IsSystemClass"] = "��";
			else
				m_NewRow["IsSystemClass"] = "��";

			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

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
