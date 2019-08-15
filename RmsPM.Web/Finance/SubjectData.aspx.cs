using System;
using System.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using RmsPM.WebControls;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// SubjectData 的摘要说明。
	/// </summary>
	public partial class SubjectData : Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
			string m_strLayer=Request.QueryString["Layer"]+"";					//需要取的层数
			string m_strNodeId=Request.QueryString["NodeId"]+"";		//父节点编号
			string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
			string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度
			string m_strRootId=Request.QueryString["RootId"]+"";				//数据分类

			string m_SubjectSetCode=Request["SubjectSetCode"] + "";

			DataTable m_Table=new DataTable("Subject");
			m_Table.Columns.Add("SubjectCode");
			m_Table.Columns.Add("SubjectSetCode");
			m_Table.Columns.Add("SubjectName");
			m_Table.Columns.Add("SubjectFullName");
			m_Table.Columns.Add("IsDebit");
			m_Table.Columns.Add("IsCrebit");
			m_Table.Columns.Add("Layer");
			m_Table.Columns.Add("ChildNodesCount");
			m_Table.Columns.Add("ShowChildNodes");

            m_Table.Columns.Add("DisplayHref");						// 是否显示链接
            m_Table.Columns.Add("DisplaySpan");						// 是否显示文字

//			EntityData m_Subject=SubjectDAO.GetSubjectBySubjectSet(m_SubjectSetCode);
//			DataTable m_DataTable=m_Subject.Tables["Subject"];

			//查询条件
			SubjectStrategyBuilder sb = new SubjectStrategyBuilder();
			sb.AddStrategy(new Strategy(SubjectStrategyName.SubjectSetCode, m_SubjectSetCode));

			if(m_strGetType=="")
			{
				#region 取第一层

				DataView m_DV;

				if (m_strRootId == "") 
				{
					sb.AddStrategy(new Strategy(SubjectStrategyName.Layer, "1"));
//					m_DV = new DataView(m_DataTable,"layer='1'","",DataViewRowState.CurrentRows);
				}
				else 
				{
					sb.AddStrategy(new Strategy(SubjectStrategyName.SubjectCode, m_strRootId));
//					m_DV = new DataView(m_DataTable,"SubjectCode='" + m_strRootId + "'","",DataViewRowState.CurrentRows);
				}

				DataTable m_DataTable = BuildStrategy(sb);
				m_DV = new DataView(m_DataTable);

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
				#region 取某节点子目录
//				DataView m_DV=new DataView(m_DataTable,"layer='"+m_strLayer+"' and SubjectCode like '"+m_strNodeId+"%'","",DataViewRowState.CurrentRows);

				sb.AddStrategy(new Strategy(SubjectStrategyName.Layer, m_strLayer));
				sb.AddStrategy(new Strategy(SubjectStrategyName.ParentCode, m_strNodeId));

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
				#region 取制定层数结果
//				DataView m_DV=new DataView(m_DataTable,"layer='1'","",DataViewRowState.CurrentRows);

				sb.AddStrategy(new Strategy(SubjectStrategyName.Layer, "1"));

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
						this.FillSelectedLayerData(ref m_Table,m_SubjectSetCode,m_Row["SubjectCode"].ToString(),2,int.Parse(m_strSelectedLayer),m_DataTable);
					}
				}
				#endregion
			}
			else if(m_strGetType=="All")
			{
				#region 取所有结果
//				DataView m_DV=new DataView(m_DataTable,"layer='1'","",DataViewRowState.CurrentRows);

				sb.AddStrategy(new Strategy(SubjectStrategyName.Layer, "1"));

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
						this.FillAllData(ref m_Table,m_SubjectSetCode,m_Row["SubjectCode"].ToString(),2,m_DataTable);
					}
				}
				#endregion
			}
			else if(m_strGetType=="SingleNode")
			{
				#region 单个节点
//				DataView m_DV=new DataView(m_DataTable,"SubjectCode='"+Request.QueryString["NodeId"]+""+"'","",DataViewRowState.CurrentRows);

				sb.AddStrategy(new Strategy(SubjectStrategyName.SubjectCode, m_strNodeId));

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
		/// 按查询条件输出结果集
		/// </summary>
		/// <param name="sb"></param>
		/// <returns></returns>
		private DataTable BuildStrategy(SubjectStrategyBuilder sb) 
		{
			try 
			{
				string sql = sb.BuildMainQueryString();

				QueryAgent qa = new QueryAgent();
				EntityData entity = qa.FillEntityData( "Subject",sql );
				qa.Dispose();

				return entity.CurrentTable;
			}
			catch (Exception ex) 
			{
				throw ex;
			}
		}

		private void FillSelectedLayerData(ref DataTable m_Table,string m_strSubjectSetCode,string m_strSubjectCode,int m_iNowLayer,int m_iStopLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"layer='"+m_iNowLayer.ToString()+"' and SubjectCode like '"+m_strSubjectCode+"%'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row);

				m_Table.Rows.Add(m_NewRow);
				if(m_iStopLayer>m_iNowLayer)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillSelectedLayerData(ref m_Table,m_strSubjectSetCode,m_Row["SubjectCode"].ToString(),m_iNowLayer+1,m_iStopLayer,m_DataTable);
				}
			}
		}

		private void FillAllData(ref DataTable m_Table,string m_strSubjectSetCode,string m_strSubjectCode,int m_iNowLayer,DataTable m_DataTable)
		{
			DataView m_DV=new DataView(m_DataTable,"layer='"+m_iNowLayer+"' and SubjectCode like '"+m_strSubjectCode+"%'","",DataViewRowState.CurrentRows);
			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row);

				m_Table.Rows.Add(m_NewRow);
				if(int.Parse(m_NewRow["ChildNodesCount"].ToString())>0)
				{
					m_NewRow["ShowChildNodes"]="1";
					this.FillAllData(ref m_Table,m_strSubjectSetCode,m_Row["SubjectCode"].ToString(),m_iNowLayer+1,m_DataTable);
				}
			}
		}

		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row)
		{
			m_NewRow["SubjectCode"]=m_Row["SubjectCode"].ToString();
			m_NewRow["SubjectSetCode"]=m_Row["SubjectSetCode"].ToString();
			m_NewRow["SubjectName"]=m_Row["SubjectName"].ToString();
			m_NewRow["SubjectFullName"] = BLL.SubjectRule.GetSubjectFullName(m_NewRow["SubjectCode"].ToString(), m_NewRow["SubjectSetCode"].ToString());
			m_NewRow["IsDebit"]=m_Row["IsDebit"].ToString();
			m_NewRow["IsCrebit"]=m_Row["IsCrebit"].ToString();
			m_NewRow["Layer"]=m_Row["Layer"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildNodesCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

            int iChildCount = (int)m_Row["ChildNodesCount"];
            if (iChildCount > 0)
            {
                m_NewRow["DisplayHref"] = "none";
                m_NewRow["DisplaySpan"] = "";
            }
            else
            {
                m_NewRow["DisplayHref"] = "";
                m_NewRow["DisplaySpan"] = "none";
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
