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

using RmsPM.DAL.EntityDAO;
using Rms.ORMap;

namespace RmsPM.Web.PBS
{
	/// <summary>
	/// PBSTypeData ��ժҪ˵����
	/// </summary>
	public partial class PBSTypeData : PageBase
	{
		private string m_strAct = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
			string m_strLayer=Request.QueryString["Layer"]+"";					//��Ҫȡ�Ĳ���
			string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
			string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
			string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������

			m_strAct = Request.QueryString["Act"] + "";  //building=��Ʒ���ͺ���ʾ¥����Ϣ
			string ProjectCode = Request.QueryString["ProjectCode"]+"";

			DataTable m_Table=new DataTable("PBSType");
			m_Table.Columns.Add("PBSTypeCode");
			m_Table.Columns.Add("ParentCode");
			m_Table.Columns.Add("PBSTypeName");
			m_Table.Columns.Add("Layer");
			m_Table.Columns.Add("ChildNodesCount");
			m_Table.Columns.Add("ShowChildNodes");			
			m_Table.Columns.Add("NodeType");

			m_Table.Columns.Add("DisplayHref");						// �Ƿ���ʾ����
			m_Table.Columns.Add("DisplaySpan");						// �Ƿ���ʾ����

			if (m_strAct.ToLower() == "building") 
			{
				m_Table.Columns.Add("HouseCount");
				m_Table.Columns.Add("HouseArea");
			}

			EntityData m_Task=RmsPM.DAL.EntityDAO.PBSDAO.GetPBSTypeByProject(ProjectCode);
			DataTable m_DataTable=m_Task.Tables["PBSType"];

			if(m_strGetType=="")
			{
				#region ȡ��һ��
				DataView m_DV=new DataView(m_DataTable,"ParentCode=''","",DataViewRowState.CurrentRows);
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
				DataView m_DV=new DataView(m_DataTable,"ParentCode='"+m_strNodeId+"'","",DataViewRowState.CurrentRows);
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
						this.FillSelectedLayerData(ref m_Table,m_Row["PBSTypeCode"].ToString(),2,int.Parse(m_strSelectedLayer),m_DataTable);
					}
				}
				#endregion
			}
			else if(m_strGetType=="All")
			{
				#region ȡ���н��
				DataView m_DV=new DataView(m_DataTable,"ParentCode=''","",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);

					if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
					{
						m_NewRow["ShowChildNodes"]="1";
						this.FillAllData(ref m_Table,m_Row["PBSTypeCode"].ToString(),2,m_DataTable);
					}
				}
				#endregion
			}
			else if(m_strGetType=="SingleNode")
			{
				#region �����ڵ�
				DataView m_DV=new DataView(m_DataTable,"WBSCode='"+Request.QueryString["NodeId"]+""+"'","",DataViewRowState.CurrentRows);
				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);

				}
				#endregion
			}
			m_Task.Dispose();

			Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
			Response.End();
		}

		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row)
		{
			string projectCode = Request.QueryString["ProjectCode"]+"";
			string nodeType=int.Parse(m_Row["ChildNodesCount"].ToString())>0?"folder":"item";
			m_NewRow["PBSTypeCode"]=m_Row["PBSTypeCode"].ToString();
			m_NewRow["ParentCode"]=m_Row["ParentCode"].ToString();
			m_NewRow["PBSTypeName"]=m_Row["PBSTypeName"].ToString();
			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildNodesCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";
			m_NewRow["NodeType"]=nodeType;
			
			int iChildCount = (int)m_Row["ChildNodesCount"];
			if ( iChildCount > 0 )
			{
				m_NewRow["DisplayHref"] = "none";
				m_NewRow["DisplaySpan"] = "";
			}
			else
			{
				m_NewRow["DisplayHref"] = "";
				m_NewRow["DisplaySpan"] = "none";
			}

			if (m_strAct.ToLower() == "building") 
			{
				EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByPBSTypeCode(m_NewRow["PBSTypeCode"].ToString(), projectCode);
				SetBuildingTotal(entity.CurrentTable, m_NewRow);
				entity.Dispose();
			}
		}

		#region FillSelectedLayerData
		private void FillSelectedLayerData(ref DataTable m_Table,string m_strWBSCode,int m_iNowLayer,int m_iStopLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode like '"+m_strWBSCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row);

				m_Table.Rows.Add(m_NewRow);
				if(m_iStopLayer>m_iNowLayer)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillSelectedLayerData(ref m_Table,m_Row["PBSTypeCode"].ToString(),m_iNowLayer+1,m_iStopLayer,m_DataTable);
				}
			}
		}
		#endregion

		#region FillAllData
		private void FillAllData(ref DataTable m_Table,string m_strWBSCode,int m_iNowLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"ParentCode='"+m_strWBSCode+"'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row);

				m_Table.Rows.Add(m_NewRow);
				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillAllData(ref m_Table,m_Row["PBSTypeCode"].ToString(),m_iNowLayer+1,m_DataTable);
				}
			}
		}
		#endregion


		private decimal GetDecimal(DataRow dr, string FieldName) 
		{
			decimal val = 0;

			try 
			{
				val = decimal.Parse(dr[FieldName].ToString());
			}
			catch 
			{
			}

			return val;
		}

		private void SetBuildingTotal(DataTable tb, DataRow NewRow) 
		{
			try 
			{
				decimal HouseCount = 0;
				decimal HouseArea = 0;

				foreach(DataRow dr in tb.Rows) 
				{
					HouseCount = HouseCount + GetDecimal(dr, "House1Count") + GetDecimal(dr, "House2Count") + GetDecimal(dr, "House3Count") + GetDecimal(dr, "House4Count") + GetDecimal(dr, "House5Count");
					HouseArea = HouseArea + GetDecimal(dr, "HouseArea");
				}

				NewRow["HouseCount"] = HouseCount.ToString("0");
				NewRow["HouseArea"] = HouseArea.ToString("0.0000");
			}
			catch (Exception ex) 
			{
				throw ex;
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
