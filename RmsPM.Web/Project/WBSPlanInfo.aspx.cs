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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.EntityDAO;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// 工作计划新增、修改
	/// </summary>
    public partial class WBSPlanInfo : PageBase
	{
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			string Action = Request.QueryString["Action"].ToUpper();
			this.tbAdd.Visible = false;
			this.tbInfo.Visible = false;
			this.tbModify.Visible = false;
			switch (Action)
			{
				case "INSERT":
					this.lblTitle.Text = "新增工作计划";
					this.tbModify.Visible = true;
					break;

				case "MODIFY":
					this.lblTitle.Text = "修改工作计划";
					this.tbModify.Visible = true;
					break;

				case "VIEW":
					this.lblTitle.Text = "查看工作计划";
					this.tbInfo.Visible = true;
					break;
			}
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
			this.SaveToolsButton.ServerClick += new System.EventHandler(this.SaveToolsButton_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void InitPage()
		{
			//若传入计划编号，则初始化计划信息
			string Action = Request.QueryString["Action"].ToUpper();
			string Code = Request.QueryString["Code"] + "";

			this.hAction.Value = Action;
			this.hCode.Value = Code;

			//查看计划信息
			if ((Action == "VIEW") && (Code != ""))
			{
				EntityData entityPlan = WBSDAO.GetTaskPlanByCode(Code);
				if (entityPlan.HasRecord())
				{
					this.lblPlanTitle.Text = entityPlan.GetString("Title");
					this.tdPlanContent.InnerText = entityPlan.GetString("Content");
					this.lblPlanDate.Text = entityPlan.GetDateTimeOnlyDate("PlanDate");
				}
				entityPlan.Dispose();
			}

			//查看计划信息
			if ((Action == "MODIFY") && (Code != ""))
			{
				EntityData entityPlan = WBSDAO.GetTaskPlanByCode(Code);
				if (entityPlan.HasRecord())
				{
					this.txtPlanTitle.Text = entityPlan.GetString("Title");
					this.arPlanContent.InnerText = entityPlan.GetString("Content");
				}
			}

			//新增工作计划时
			if (Action == "INSERT")
			{
//				User objUser = (User)Session["User"];
//				DAL.QueryStrategy.WBSStrategyBuilder WSB = new RmsPM.DAL.QueryStrategy.WBSStrategyBuilder();
//				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.UserAccess,objUser.UserCode));
//				WSB.AddStrategy(new Strategy(DAL.QueryStrategy.WBSStrategyName.ProjectCode,base.ProjectCode));
//				string sql = WSB.BuildMainQueryString();
//				QueryAgent QA = new QueryAgent();
//
//				EntityData entityTask = QA.FillEntityData("Task",sql);
//				this.dgTaskList.DataSource = entityTask.CurrentTable;
//				this.dgTaskList.DataBind();
//				entityTask.Dispose();
//				QA.Dispose();
			}
		}

		private void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				

				DataRow dr;
				switch(this.hAction.Value)
				{
					case "VIEW":
						break;

					case "MODIFY":
						EntityData entityModify = WBSDAO.GetTaskPlanByCode(this.hCode.Value);
						dr = entityModify.CurrentRow;
						dr["Title"] = this.txtPlanTitle.Text.Trim();
						dr["Content"] = this.arPlanContent.InnerText.Trim();
						WBSDAO.UpdateTaskPlan(entityModify);
						entityModify.Dispose();
						Response.Write(JavaScript.ScriptStart);
						Response.Write("window.opener.Update();");
						Response.Write("window.close();");
						Response.Write(JavaScript.ScriptEnd);
						break;
				
					case "INSERT":
						EntityData entityInsert = WBSDAO.GetAllTaskPlan();
						dr = entityInsert.GetNewRecord();
						dr["UserCode"] = base.user.UserCode;
						dr["ProjectCode"] = base.ProjectCode;
						dr["TaskPlanCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskPlanCode");
						dr["CheckFlag"] = 0;
						dr["Title"] = this.txtPlanTitle.Text.Trim();
						dr["Content"] = this.arPlanContent.InnerText.Trim();
						entityInsert.AddNewRecord(dr);
						WBSDAO.SubmitAllTaskPlan(entityInsert);
						entityInsert.Dispose();
						Response.Write(JavaScript.ScriptStart);
						Response.Write("window.opener.Update();");
						Response.Write("window.close();");
						Response.Write(JavaScript.ScriptEnd);
						break;
				}
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}
	}
}
