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
using Rms.Web;
using RmsPM.BLL;
using RmsPM.DAL;
using RmsPM.Web;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// 工作计划页面
	/// </summary>
    public partial class WBSPlan : PageBase
	{
		
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if (!this.IsPostBack)
			{
				InitPage();
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
			this.dgMyPlan.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgMyPlan_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void InitPage()
		{
			string ProjectCode = base.ProjectCode;
			User objUser = (User)Session["User"];
			string strSearchTitle = Request.QueryString["ExecuteName"]+"";

			try
			{
				EntityData entityPlan = WBSDAO.GetTaskPlanByProjectCode(ProjectCode);
			
				if (entityPlan.HasRecord())
				{
					DataView dvOther = new DataView(entityPlan.CurrentTable,"UserCode <> '" + objUser.UserCode + "' ","PlanDate DESC",System.Data.DataViewRowState.CurrentRows);
					DataView dvOwn = new DataView(entityPlan.CurrentTable,"UserCode = '" + objUser.UserCode + "' ","PlanDate DESC",System.Data.DataViewRowState.CurrentRows);
					if(strSearchTitle.Length>0)
						dvOwn.RowFilter = "Title like '%"+strSearchTitle+"%'";	

					this.dgOtherPlan.DataSource = dvOther;
					this.dgOtherPlan.DataBind();
					this.tbOther.Visible = (dvOther.Count > 0)?false:true;

					this.dgMyPlan.DataSource = dvOwn;
					this.dgMyPlan.DataBind();
					this.tbOwn.Visible = (dvOther.Count > 0)?false:true;

					int ColumnCount = this.dgOtherPlan.Columns.Count;

					//审阅后的工作计划不可再次审阅
					for(int i=0;i<dvOther.Count;i++)
					{
						if (dvOther[0]["CheckFlag"].ToString() == "1")
						{
							this.dgOtherPlan.Items[i].Cells[ColumnCount - 1].Text = "";
						}
					}
				}
				entityPlan.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"获取计划列表失败");
			}
		}

		//删除工作计划
		private void dgMyPlan_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			User objUser = (User)Session["User"];
			if (e.CommandName == "Delete")
			{
				EntityData entityDelete = WBSDAO.GetTaskPlanByCode(this.dgMyPlan.DataKeys[e.Item.ItemIndex].ToString());
				WBSDAO.DeleteTaskPlan(entityDelete);

				EntityData entityPlan = WBSDAO.GetTaskPlanByProjectCode(ProjectCode);
				DataView dvOwn = new DataView(entityPlan.CurrentTable,"UserCode = '" + objUser.UserCode + "' ","PlanDate DESC",System.Data.DataViewRowState.CurrentRows);

				entityDelete.Dispose();
				entityPlan.Dispose();

				this.dgMyPlan.DataSource = dvOwn;
				this.dgMyPlan.DataBind();
			}
		}

	}
}
