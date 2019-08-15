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


namespace RmsPM.Web.PBS
{
	/// <summary>
	/// SelectBuildingData ��ժҪ˵����
	/// </summary>
	public partial class SelectBuildingData : PageBase
	{
		// �����ĵ����͵����ݱ�
		private DataTable m_DataTable = null;

		// ���淵�����ݵ����ݱ�
		private DataTable m_Table = null;

		protected void Page_Load(object sender, System.EventArgs e)
		{

			string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
			string m_strLayer=Request.QueryString["Layer"]+"";					//��Ҫȡ�Ĳ���
			string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
			string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
			string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������
			string ProjectCode = Request.QueryString["ProjectCode"] + "";
			string PBSTypeCode = Request.QueryString["PBSTypeCode"] + "";

			m_Table=new DataTable("Building");
			m_Table.Columns.Add("BuildingCode");
			m_Table.Columns.Add("ParentCode");
			m_Table.Columns.Add("BuildingName");
			m_Table.Columns.Add("Description");
			m_Table.Columns.Add("Layer");
			m_Table.Columns.Add("ChildNodesCount");
			m_Table.Columns.Add("ShowChildNodes");
			m_Table.Columns.Add("NodeType");
			m_Table.Columns.Add("IsArea");
			m_Table.Columns.Add("NoSelectArea");
			m_Table.Columns.Add("PBSUnitName");
			m_Table.Columns.Add("IconName");

//			EntityData entity = DAL.EntityDAO.ProductDAO.GetBuildingByProjectCode(ProjectCode);
//			m_DataTable=entity.CurrentTable;

			//��ѯ����
			BuildingStrategyBuilder sb = new BuildingStrategyBuilder("V_Building");
			sb.AddStrategy(new Strategy(BuildingStrategyName.ProjectCode, ProjectCode));

			if (PBSTypeCode != "") 
				sb.AddStrategy(new Strategy(BuildingStrategyName.PBSTypeCodeAllChild, PBSTypeCode));

			if(m_strGetType=="")
			{
				#region ȡ��һ��
//				DataRow m_NewRow=m_Table.NewRow();
//				this.FillRootRow(ref m_NewRow);
//				m_Table.Rows.Add(m_NewRow);

				//ȱʡ�򿪸��ڵ�
//				DataView m_DV=new DataView(m_DataTable,"isnull(ParentCode,'')=''","",DataViewRowState.CurrentRows);

				sb.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, ""));
				sb.AddOrder("BuildingName", true);
				DataTable m_DataTable = BuildStrategy(sb);
				DataView m_DV = new DataView(m_DataTable);

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
//				DataView m_DV=new DataView(m_DataTable,"isnull(ParentCode,'')='"+m_strNodeId+"'","",DataViewRowState.CurrentRows);

				sb.AddStrategy(new Strategy(BuildingStrategyName.ParentCode, m_strNodeId));
				sb.AddOrder("BuildingName", true);
				DataTable m_DataTable = BuildStrategy(sb);
				DataView m_DV = new DataView(m_DataTable);

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
//				DataView m_DV=new DataView(m_DataTable,"Deep='1'","",DataViewRowState.CurrentRows);

				sb.AddStrategy(new Strategy(BuildingStrategyName.Layer, "1"));
				sb.AddOrder("BuildingName", true);
				DataTable m_DataTable = BuildStrategy(sb);
				DataView m_DV = new DataView(m_DataTable);

				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);
					if(int.Parse(m_strSelectedLayer)>1)
					{
						m_NewRow["ShowChildNodes"]="1";
						this.FillSelectedLayerData(m_Row["BuildingCode"].ToString(),2,int.Parse(m_strSelectedLayer));
					}
				}
				#endregion
			}
			else if(m_strGetType=="All")
			{
				#region ȡ���н��
//				DataView m_DV=new DataView(m_DataTable,"isnull(ParentCode,'')=''","",DataViewRowState.CurrentRows);

				sb.AddStrategy(new Strategy(BuildingStrategyName.Layer, "1"));
				sb.AddOrder("BuildingName", true);
				DataTable m_DataTable = BuildStrategy(sb);
				DataView m_DV = new DataView(m_DataTable);

				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);

					if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
					{
						m_NewRow["ShowChildNodes"]="1";
						this.FillAllData(m_Row["BuildingCode"].ToString(),2);
					}
				}
				#endregion
			}
			else if(m_strGetType=="SingleNode")
			{
				#region �����ڵ�
//				DataView m_DV=new DataView(m_DataTable,"BuildingCode='"+Request.QueryString["NodeId"]+""+"'","",DataViewRowState.CurrentRows);

				sb.AddStrategy(new Strategy(BuildingStrategyName.BuildingCode, m_strNodeId));
				sb.AddOrder("BuildingName", true);
				DataTable m_DataTable = BuildStrategy(sb);
				DataView m_DV = new DataView(m_DataTable);

				foreach(DataRowView m_Row in m_DV)
				{
					DataRow m_NewRow=m_Table.NewRow();

					this.FillRow(ref m_NewRow,m_Row);
					
					m_Table.Rows.Add(m_NewRow);

				}
				#endregion
			}

			Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
			Response.End();
		}

		/// <summary>
		/// ����ѯ������������
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
			m_NewRow["BuildingName"] = "����¥��";
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
				int iColumnCount = m_Row.DataView.Table.Columns.Count;
				for ( int i =0 ; i<iColumnCount; i++)
				{
					string columnName= m_Row.DataView.Table.Columns[i].ColumnName;
					if ( m_NewRow.Table.Columns.Contains(columnName))
						m_NewRow[columnName] = m_Row[columnName];
				}

				m_NewRow["Layer"]=int.Parse(m_Row["Layer"].ToString());

				int ChildCount = BLL.ProductRule.GetBuildingChildCount(m_Row["BuildingCode"].ToString());
				m_NewRow["ChildNodesCount"] = ChildCount;

				m_NewRow["ShowChildNodes"]="0";
				m_NewRow["NodeType"] = "";

				//ֻ��ʾ¥���ϵ�CheckBox��������ʾCheckBox
				if ((m_NewRow["IsArea"] != null) && (m_NewRow["IsArea"].ToString() == "1") )
				{
					m_NewRow["NoSelectArea"] = "none";
				}
				else 
				{
					m_NewRow["NoSelectArea"] = "block";
				}

				//��ʾͼ��
				if ((m_NewRow["IsArea"] != null) && (m_NewRow["IsArea"].ToString() == "1") )
				{
					m_NewRow["IconName"] = "BuildingArea.gif";
				}
				else 
				{
					m_NewRow["IconName"] = "Building.gif";
				}

				//��λ��������
				m_NewRow["PBSUnitName"] = BLL.PBSRule.GetPBSUnitName(m_Row["PBSUnitCode"]);
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
