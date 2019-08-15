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
using RmsPM.DAL;
using Rms.Web;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.ConstructProg
{
	/// <summary>
	/// ProjectProgressChartData 的摘要说明。
	/// </summary>
	public partial class ProjectProgressChartData : System.Web.UI.Page
	{
		private int m_Layer = 0;
//		private string m_GanttType = "";

		/// <summary>
		/// 加载进度表
		/// </summary>
		/// <param name="WBSCode"></param>
		public void LoadChartDataTable(string WBSCode)
		{
			try 
			{
				DataSet ds = null;
				if (Session["dsGantt"] != null)
				{
					ds = (DataSet)Session["dsGantt"];

					BLL.ConstructProgRule.ExpandProjectProgressChartDataTable(WBSCode, ds);
				}
				else
				{
					ds = BLL.ConstructProgRule.GetProjectProgressChartDataTable(WBSCode);
				}

				Session["dsGantt"] = ds;
			}
			catch(Exception ex) 
			{
				throw ex;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			string m_strGetType=Request.QueryString["GetType"]+"";				//数据分类
			string m_strNodeId=Request.QueryString["NodeId"]+"";				//父节点编号
			string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//定点展开的序列
			string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//定层展开的深度
			string ProjectCode = Request.QueryString["ProjectCode"] + "";

//			m_GanttType = Request.QueryString["GanttType"] + "";  //甘特图类型

			m_Layer = BLL.ConvertRule.ToInt(Request.QueryString["Layer"]);					//父节点层数

			DataTable m_Table=new DataTable("Task");
			m_Table.Columns.Add("WBSCode");
			m_Table.Columns.Add("ParentCode");
			m_Table.Columns.Add("TaskName");
			m_Table.Columns.Add("Layer");
			m_Table.Columns.Add("ChildNodesCount");
			m_Table.Columns.Add("ShowChildNodes");			
			m_Table.Columns.Add("NodeType");
			m_Table.Columns.Add("CompletePercent");
			m_Table.Columns.Add("TaskDesc");
			m_Table.Columns.Add("TaskHint");
			m_Table.Columns.Add("HrefDisplay");
			m_Table.Columns.Add("NoHrefDisplay");

			EntityData entity = DAL.EntityDAO.WBSDAO.GetChildTask(m_strNodeId);

			//加载进度表
			LoadChartDataTable(m_strNodeId);

			DataView m_DV = new DataView(entity.CurrentTable);

			foreach(DataRowView m_Row in m_DV)
			{
				DataRow m_NewRow=m_Table.NewRow();

				this.FillRow(ref m_NewRow,m_Row);
					
				m_Table.Rows.Add(m_NewRow);
			}

			entity.Dispose();

			Response.Write(RmsPM.WebControls.TreeView.XmlTree.GetDataToXmlString(m_Table));
			Response.End();
		}

		private void FillRow(ref DataRow m_NewRow,DataRowView m_Row)
		{
			int ChildCount = BLL.ConvertRule.ToInt(m_Row["ChildNodesCount"]);
			string nodeType = ChildCount>0?"folder":"item";

			m_NewRow["WBSCode"]=m_Row["WBSCode"];
			m_NewRow["ParentCode"]=m_Row["ParentCode"];
			m_NewRow["TaskName"]=m_Row["TaskName"];

//			m_NewRow["Layer"]=m_Row["Deep"].ToString();
			m_NewRow["Layer"] = m_Layer + 1;

			m_NewRow["ChildNodesCount"] = ChildCount;
			m_NewRow["ShowChildNodes"]="0";
			m_NewRow["NodeType"]=nodeType;

			m_NewRow["HrefDisplay"] = ChildCount>0?"":"none";
			m_NewRow["NoHrefDisplay"] = ChildCount>0?"none":"";

			decimal CompletePercent = BLL.ConvertRule.ToDecimal(m_Row["CompletePercent"]);
			m_NewRow["CompletePercent"] = CompletePercent;

			string TaskDesc = BLL.ConvertRule.ToString(m_Row["TaskName"]);
			TaskDesc = BLL.StringRule.TruncText(TaskDesc, 8);
			TaskDesc = TaskDesc + "(" + CompletePercent + "%)";
			m_NewRow["TaskDesc"] = TaskDesc; 

			//提示
			string hint = "工作名称：" + m_Row["TaskName"].ToString()
				+ "%br%" + "状　　态：" + ComSource.GetTaskStatusName(BLL.ConvertRule.ToInt(m_Row["Status"]).ToString())
				+ "%br%" + "当前进度：" + BLL.ConvertRule.ToInt(m_Row["CompletePercent"]).ToString() + "%"
				+ "%br%" + "计划起止：" + BLL.ConvertRule.ToDateString(m_Row["PlannedStartDate"], "yyyy-MM-dd") + "..." + BLL.ConvertRule.ToDateString(m_Row["PlannedFinishDate"], "yyyy-MM-dd")
				+ "%br%" + "实际起止：" + BLL.ConvertRule.ToDateString(m_Row["ActualStartDate"], "yyyy-MM-dd") + "..." + BLL.ConvertRule.ToDateString(m_Row["ActualFinishDate"], "yyyy-MM-dd")
				;
			m_NewRow["TaskHint"] = hint;
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
