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
	/// �����ƻ�����
	/// </summary>
    public partial class WBSPlanCheck : System.Web.UI.Page
	{
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ�������ƻ��б�ʧ��");
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
				Response.Write("alert('����ɹ���');");
				Response.Write("window.close()");
				Response.Write(JavaScript.ScriptEnd);
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"���湤���������Ľ��ʧ�ܣ�");
			}
		}
	}
}
