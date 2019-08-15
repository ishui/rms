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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.Web;
using RmsPM.DAL.EntityDAO;
using Rms.Web;
using Rms.ORMap;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// 工作计划审阅
	/// </summary>
    public partial class WBSPlanCheck : System.Web.UI.Page
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
			this.SaveToolsButton.ServerClick += new System.EventHandler(this.SaveToolsButton_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void InitPage()
		{
			string Code = Request.QueryString["Code"] + "";
			User objUser = (User)Session["User"];

			this.lblCheckDate.Text = System.DateTime.Now.ToShortDateString();
			this.lblCheckPerson.Text = objUser.UserName;
			
			try
			{
				EntityData entityExecute = WBSDAO.GetTaskPlanByCode(Code);

				if (entityExecute.HasRecord())
				{
					this.lblTitle.Text = entityExecute.GetString("Title");
					this.lblPlanDate.Text = entityExecute.GetDateTimeOnlyDate("PlanDate");
					this.lblContent.Text = entityExecute.GetString("Content");
					this.arCheckResult.InnerText = entityExecute.GetString("CheckResult");
				}
				entityExecute.Dispose();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"初始化工作计划列表失败");
			}
		}

		private void SaveToolsButton_ServerClick(object sender, System.EventArgs e)
		{
			string Code = Request.QueryString["Code"] + "";
			User objUser = (User)Session["User"];

			try
			{
				EntityData entityExecute = WBSDAO.GetTaskPlanByCode(Code);

				if (entityExecute.HasRecord())
				{
					DataRow dr = entityExecute.CurrentRow;
					dr["CheckDate"] = System.DateTime.Now;
					dr["CheckPerson"] = objUser.UserCode;
					dr["CheckResult"] = this.arCheckResult.InnerText.Trim();
					dr["CheckFlag"] = 1;
					WBSDAO.UpdateTaskPlan(entityExecute);
				}
				entityExecute.Dispose();

				Response.Write(JavaScript.ScriptStart);
				Response.Write("alert('保存成功！');");
				Response.Write("window.close()");
				Response.Write(JavaScript.ScriptEnd);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"保存工作报告审阅结果失败！");
			}
		}
	}
}
