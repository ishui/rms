/*
特别说明：
	IsAvailable 是否有效
		0： 有效； 1 无效

	IsRightControlPoint 权限控制点
		0： 有权限控制； 1 没有权限控制

	IsRoleControlPoint  角色管理是否能控制改权限， 有些权限是在systemgroupclass中进行控制的
		0： 是在角色管理中控制； 1 不是在角色管理中控制
		
	IsSystemClass	是否系统类别的大类 ( 不由角色管理进行控制，由系统类别进控制的大类 )
		1：	是	；	0：否


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
	/// FunctionStructureData 的摘要说明。
	/// </summary>
	public partial class FunctionStructureData : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{

			try
			{
				string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
				string m_strLayer=Request.QueryString["Layer"]+"";					//需要取的层数
				string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
				string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
				string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度

				DataTable m_Table=new DataTable("FunctionStructure");
				m_Table.Columns.Add("FunctionStructureCode");					// 费用编号
				m_Table.Columns.Add("ParentCode");					// 父Code
				m_Table.Columns.Add("FunctionStructureName");					// 费用项名称
				m_Table.Columns.Add("Description");					// 说明
				m_Table.Columns.Add("Layer");						// 层数－－－对应deep深度
				m_Table.Columns.Add("ChildNodesCount");				// 子节点数目
				m_Table.Columns.Add("ShowChildNodes");				// 是否显示子节点

				m_Table.Columns.Add("IsAvailable");						// 是否角色控制点
				m_Table.Columns.Add("IsRightControlPoint");				// 是否权限控制点
				m_Table.Columns.Add("IsSystemClass");					// 是否系统类别大类
				m_Table.Columns.Add("IsRoleControlPoint");				// 是否角色控制点


				EntityData m_FS = DAL.EntityDAO.SystemManageDAO.GetAllFunctionStructure();

				DataTable m_DataTable=m_FS.CurrentTable;

				string filter = "";
				if(m_strGetType=="")
				{
					#region 取第一层

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
					#region 取某节点子目录

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
					#region 单个节点

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
				m_NewRow["IsRightControlPoint"] = "否";
			else
				m_NewRow["IsRightControlPoint"] = "是";

			if ( m_Row["IsAvailable"].ToString() == "1" )
				m_NewRow["IsAvailable"] = "否";
			else
				m_NewRow["IsAvailable"] = "是";

			if ( m_Row["IsRoleControlPoint"].ToString() == "1" )
				m_NewRow["IsRoleControlPoint"] = "否";
			else
				m_NewRow["IsRoleControlPoint"] = "是";

			if ( m_Row["IsSystemClass"].ToString() == "1" )
				m_NewRow["IsSystemClass"] = "是";
			else
				m_NewRow["IsSystemClass"] = "否";

			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
			m_NewRow["ShowChildNodes"]="0";

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
