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
using RmsPM.Web;
using RmsPM.DAL.EntityDAO;
using Rms.ORMap;


namespace RmsPM.Web.Project
{
	/// <summary>
	/// PBSData 的摘要说明。
	/// </summary>
	public partial class PBSData : PageBase
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string m_strPBSCode=(Request.QueryString["PBSCode"]==null?"":Request.QueryString["PBSCode"]);
			string m_strLayers=(Request.QueryString["Layers"]==null?"*":Request.QueryString["Layers"]);
			string m_strLayer=(Request.QueryString["Layer"]==null?"0":Request.QueryString["Layer"]);
			string[] m_Layers=m_strLayers.Split('.');

			DataTable m_Table=new DataTable("Product");
			m_Table.Columns.Add("Id");
			m_Table.Columns.Add("Name");
			m_Table.Columns.Add("Col1");
			m_Table.Columns.Add("Col2");
			m_Table.Columns.Add("Col3");
			m_Table.Columns.Add("Col4");
			m_Table.Columns.Add("Col5");
			m_Table.Columns.Add("Col6");
			m_Table.Columns.Add("Col7");
			m_Table.Columns.Add("Col8");
			m_Table.Columns.Add("Layer");
			m_Table.Columns.Add("ChildNodesCount");
			m_Table.Columns.Add("BuildingCount");

			EntityData m_PBS=ProductDAO.GetPBSByParentCode((string)ViewState["ProjectCode"],m_strPBSCode);

			foreach(DataRow m_Row in m_PBS.Tables["PBS"].Rows)
			{
				DataRow m_NewRow = m_Table.NewRow();
				m_NewRow["Id"]=m_Row["PBSCode"].ToString();
				m_NewRow["Name"]=m_Row["Name"].ToString();
				m_NewRow["Col1"]=m_Row["FloorSpace"].ToString();
				m_NewRow["Col2"]=m_Row["VolumeRate"].ToString();
				m_NewRow["Col3"]=m_Row["BuildingArea"].ToString();
				m_NewRow["Col4"]=m_Row["RateForSale"].ToString();
				m_NewRow["Col5"]=m_Row["AreaForSale"].ToString();
				m_NewRow["Col6"]=m_Row["ProductRate"].ToString();
				m_NewRow["Col7"]=m_Row["AreaPerHouse"].ToString();
				m_NewRow["Col8"]=m_Row["TotalHouseCount"].ToString();
				m_NewRow["Layer"]=m_Row["Deep"].ToString();
				m_NewRow["ChildNodesCount"]=m_Row["ChildCount"].ToString();
				m_NewRow["BuildingCount"]=m_Row["BuildingCount"].ToString();
				m_Table.Rows.Add(m_NewRow);
			}
			m_PBS.Dispose();

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
