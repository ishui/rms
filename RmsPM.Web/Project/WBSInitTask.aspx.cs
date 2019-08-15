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

namespace RmsPM.Web.Project
{
	/// <summary>
	/// WBSInitTask 的摘要说明。
	/// </summary>
    public partial class WBSInitTask : System.Web.UI.Page
	{
		protected string strState = "";
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			InitPage();
			LoadDate();
		}

		private void InitPage()
		{
			
		}

		private DataView GetRoot()
		{
			string strProjectCode = (string)Session["ProjectCode"];
			EntityData entity = DAL.EntityDAO.WBSDAO.GetAllTask();
			DataView dv = entity.Tables[0].DefaultView;
			dv.RowFilter = " ParentCode = '' and Deep=0 "; // 父节点为空且深度为0；
			return dv;
		}

		private void LoadDate()
		{
			// 检测是否已经初始化过
			DataView dv = this.GetRoot();
			if(dv.Table.Rows.Count>0)
			{
				DataRow dr = dv.Table.Rows[0];
				this.tdTaskName.Controls.Clear();
				this.tdTaskName.InnerText = dr["TaskName"].ToString();
				this.tdTaskCode.Controls.Clear();
				this.tdTaskCode.InnerText = dr["TaskCode"].ToString();
				this.tdTaskStatus.Controls.Clear();
				this.tdTaskStatus.InnerText = BLL.ComSource.GetTaskStatusName(dr["Status"].ToString());
				this.tdImportantLevel.Controls.Clear();
				this.tdImportantLevel.InnerText = dr["TaskCode"].ToString();
				this.tdPlanStartDate.Controls.Clear();
				this.tdPlanStartDate.InnerText = dr["TaskCode"].ToString();
				this.tdPlanFinishDate.Controls.Clear();
				this.tdPlanFinishDate.InnerText = dr["TaskCode"].ToString();
				this.tdTaskDesc.Controls.Clear();
				this.tdTaskDesc.InnerText = dr["TaskCode"].ToString();

				this.btSubmit.Visible = false;
				this.btCancel.Visible = false;
				this.strState = "List";
			}
			else
			{
				this.trTemplate.Visible = false;
				this.strState = "Insert";
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
			this.btSubmit.ServerClick += new System.EventHandler(this.SaveToolsButton_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{

//			EntityData entity = new EntityData("Task");
//			DataRow drTask = entity.GetNewRecord();
//			drTask["TaskCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskCode");
//			drTask["TaskName"] = this.txtTaskName.Text;


		}
	}
}
