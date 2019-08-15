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
using Rms.Web;
using Rms.ORMap;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Systems
{
	/// <summary>
	/// DepartmentData 的摘要说明。
	/// </summary>
	public partial class DepartmentData : PageBase
	{
		private int ToInt(string s) 
		{
			try 
			{
				return int.Parse(s);
			}
			catch 
			{
				return 0;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
			string m_strUnitCode=(Request.QueryString["UnitCode"]==null?"":Request.QueryString["UnitCode"]);
			string m_strLayers=(Request.QueryString["Layers"]==null?"*":Request.QueryString["Layers"]);
			string m_strParentLayer=(Request.QueryString["ParentLayer"]==null?"0":Request.QueryString["ParentLayer"]);
			int m_iParentLayer = -1;

			if (m_strParentLayer != "") 
			{
				m_iParentLayer = ToInt(m_strParentLayer);
			}

			string[] m_Layers=m_strLayers.Split('.');

			DataTable m_Table=new DataTable("Unit");
			m_Table.Columns.Add("Id");
			m_Table.Columns.Add("UnitName");
			m_Table.Columns.Add("Remark");
			m_Table.Columns.Add("Layer");
			m_Table.Columns.Add("ChildNodesCount");
			m_Table.Columns.Add("ShowChildNodes");
			m_Table.Columns.Add("PrincipalName");

			if(m_strGetType=="all") 
			{
				DataRow m_NewRow = m_Table.NewRow();
				m_NewRow["Id"]="";
				m_NewRow["UnitName"]="所有部门";
				m_NewRow["Layer"]=1 + m_iParentLayer;
				m_NewRow["ChildNodesCount"]=1;
				m_NewRow["ShowChildNodes"]=0;

				m_Table.Rows.Add(m_NewRow);
			}
			else 
			{
				EntityData m_Unit=OBSDAO.GetOBSUnitByParent(m_strUnitCode);

				foreach(DataRow m_Row in m_Unit.Tables["Unit"].Rows)
				{
					DataRow m_NewRow = m_Table.NewRow();
					m_NewRow["Id"]=m_Row["UnitCode"].ToString();
					m_NewRow["UnitName"]=m_Row["UnitName"].ToString();
					m_NewRow["PrincipalName"]=m_Row["PrincipalName"].ToString();
					m_NewRow["Remark"]=m_Row["Remark"].ToString();

					if (m_iParentLayer < 0) 
					{
						m_NewRow["Layer"]=m_Row["Deep"].ToString();
					}
					else 
					{
						m_NewRow["Layer"]=1 + m_iParentLayer;
					}

					m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
					m_NewRow["ShowChildNodes"]=0;

					m_Table.Rows.Add(m_NewRow);
				}
				m_Unit.Dispose();
			}

			Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
			Response.End();
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
