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
	/// �����ƻ��������޸�
	/// </summary>
    public partial class WBSPlanInfo : PageBase
	{
			
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			string Action = Request.QueryString["Action"].ToUpper();
			this.tbAdd.Visible = false;
			this.tbInfo.Visible = false;
			this.tbModify.Visible = false;
			switch (Action)
			{
				case "INSERT":
					this.lblTitle.Text = "���������ƻ�";
					this.tbModify.Visible = true;
					break;

				case "MODIFY":
					this.lblTitle.Text = "�޸Ĺ����ƻ�";
					this.tbModify.Visible = true;
					break;

				case "VIEW":
					this.lblTitle.Text = "�鿴�����ƻ�";
					this.tbInfo.Visible = true;
					break;
			}
			if (!this.IsPostBack)
			{
				InitPage();
			}
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
			this.SaveToolsButton.ServerClick += new System.EventHandler(this.SaveToolsButton_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void InitPage()
		{
			//������ƻ���ţ����ʼ���ƻ���Ϣ
			string Action = Request.QueryString["Action"].ToUpper();
			string Code = Request.QueryString["Code"] + "";

			this.hAction.Value = Action;
			this.hCode.Value = Code;

			//�鿴�ƻ���Ϣ
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

			//�鿴�ƻ���Ϣ
			if ((Action == "MODIFY") && (Code != ""))
			{
				EntityData entityPlan = WBSDAO.GetTaskPlanByCode(Code);
				if (entityPlan.HasRecord())
				{
					this.txtPlanTitle.Text = entityPlan.GetString("Title");
					this.arPlanContent.InnerText = entityPlan.GetString("Content");
				}
			}

			//���������ƻ�ʱ
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
