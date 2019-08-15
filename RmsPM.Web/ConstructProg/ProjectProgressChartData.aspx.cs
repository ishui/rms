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
	/// ProjectProgressChartData ��ժҪ˵����
	/// </summary>
	public partial class ProjectProgressChartData : System.Web.UI.Page
	{
		private int m_Layer = 0;
//		private string m_GanttType = "";

		/// <summary>
		/// ���ؽ��ȱ�
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
			string m_strGetType=Request.QueryString["GetType"]+"";				//���ݷ���
			string m_strNodeId=Request.QueryString["NodeId"]+"";				//���ڵ���
			string[] m_Layers=(Request.QueryString["Layers"]+"").Split('.');	//����չ��������
			string m_strSelectedLayer=Request.QueryString["SelectedLayer"]+"";	//����չ�������
			string ProjectCode = Request.QueryString["ProjectCode"] + "";

//			m_GanttType = Request.QueryString["GanttType"] + "";  //����ͼ����

			m_Layer = BLL.ConvertRule.ToInt(Request.QueryString["Layer"]);					//���ڵ����

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

			//���ؽ��ȱ�
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

			//��ʾ
			string hint = "�������ƣ�" + m_Row["TaskName"].ToString()
				+ "%br%" + "״����̬��" + ComSource.GetTaskStatusName(BLL.ConvertRule.ToInt(m_Row["Status"]).ToString())
				+ "%br%" + "��ǰ���ȣ�" + BLL.ConvertRule.ToInt(m_Row["CompletePercent"]).ToString() + "%"
				+ "%br%" + "�ƻ���ֹ��" + BLL.ConvertRule.ToDateString(m_Row["PlannedStartDate"], "yyyy-MM-dd") + "..." + BLL.ConvertRule.ToDateString(m_Row["PlannedFinishDate"], "yyyy-MM-dd")
				+ "%br%" + "ʵ����ֹ��" + BLL.ConvertRule.ToDateString(m_Row["ActualStartDate"], "yyyy-MM-dd") + "..." + BLL.ConvertRule.ToDateString(m_Row["ActualFinishDate"], "yyyy-MM-dd")
				;
			m_NewRow["TaskHint"] = hint;
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
