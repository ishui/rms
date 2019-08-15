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
using RmsPM.DAL;
using RmsPM.BLL;
using RmsPM.DAL.QueryStrategy;

namespace RmsPM.Web.Project
{
	/// <summary>
	/// TaskBudgetConditionModify ��ժҪ˵����
	/// </summary>
	public partial class TaskBudgetConditionModify : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlInputHidden txtProjectCode;
		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.spanTaskName.InnerText = this.txtTaskName.Value;

			if ( !IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
			try 
			{
				this.txtTaskBudgetCode.Value = Request.QueryString["TaskBudgetCode"];
				this.txtInputWBSCode.Value = Request.QueryString["InputWBSCode"];
				this.txtTaskBudgetConditionCode.Value = Request.QueryString["TaskBudgetConditionCode"];

				if ((this.txtTaskBudgetCode.Value == "") || (this.txtInputWBSCode.Value == ""))
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ��������Ż��ͬ���"));
					return;
				}
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try 
			{
				string taskBudgetConditionCode = this.txtTaskBudgetConditionCode.Value;

				if (taskBudgetConditionCode != "") 
				{
					EntityData entity = (EntityData)Session["WBSEntity"];
					DataTable dt = entity.Tables["TaskBudgetCondition"];

					DataRow dr = dt.Select("TaskBudgetConditionCode='" + taskBudgetConditionCode + "'")[0];
					this.txtInputWBSCode.Value = BLL.ConvertRule.ToString(dr["WBSCode"]);
					this.txtWBSCode.Value = BLL.ConvertRule.ToString(dr["RelationWBSCode"]);
					this.txtTaskName.Value = BLL.ConvertRule.ToString(dr["TaskName"]);
					this.spanTaskName.InnerText = this.txtTaskName.Value;
					this.txtCompletePercent.Value = BLL.ConvertRule.ToInt(dr["CompletePercent"]).ToString();
					this.sltDelayType.Value = BLL.ConvertRule.ToInt(dr["DelayType"]).ToString();
					this.txtDelayDays.Value = BLL.ConvertRule.ToInt(dr["DelayDays"]).ToString();
					entity.Dispose();
				}
				else 
				{
					this.btnDelete.Style["display"] = "none";
					//����ȱʡֵ
					this.txtCompletePercent.Value = "100";
				}
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"��ʼ��ҳ�����");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
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

		}
		#endregion

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.txtWBSCode.Value == "")
			{
				Hint = "��ѡ������";
				return false;
			}

			if ( this.txtCompletePercent.Value != "" )
			{
				if ( ! Rms.Check.StringCheck.IsInt(txtCompletePercent.Value))
				{
					Hint = "��ɰٷֱȱ��������� �� ";
					return false;
				}

				int CompletePercent = BLL.ConvertRule.ToInt(this.txtCompletePercent.Value);
				if ((CompletePercent < 0) || (CompletePercent > 100))
				{
					Hint = "��ɰٷֱȱ���λ�� 0 �� 100 ֮�� �� ";
					return false;
				}
			}

			if (this.sltDelayType.Value != "0") 
			{
				if (this.txtDelayDays.Value == "")
				{
					Hint = "��������ǰ���Ӻ������";
					return false;
				}

				if ( this.txtDelayDays.Value != "" )
				{
					if ( ! Rms.Check.StringCheck.IsInt(txtDelayDays.Value))
					{
						Hint = "��ǰ���Ӻ���������������� �� ";
						return false;
					}
				}
			}

			return true;
		}


		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_Click(object sender, System.EventArgs e)
		{
			try 
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				EntityData entity = (EntityData)Session["WBSEntity"];
				DataTable dt = entity.Tables["TaskBudgetCondition"];

				string wbsCode = this.txtInputWBSCode.Value;
				string taskBudgetCode = this.txtTaskBudgetCode.Value;
				string taskBudgetConditionCode = this.txtTaskBudgetConditionCode.Value;

				DataRow dr;

				if (taskBudgetConditionCode == "") 
				{
					dr = dt.NewRow();
					dr["TaskBudgetConditionCode"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("TaskBudgetConditionCode");
					dr["TaskBudgetCode"] = taskBudgetCode;
					dr["WBSCode"] = wbsCode;
					dt.Rows.Add(dr);
				}
				else 
				{
					dr = dt.Select("TaskBudgetConditionCode='" + taskBudgetConditionCode + "'")[0];
				}

				dr["RelationWBSCode"] = this.txtWBSCode.Value;
				dr["TaskName"] = this.txtTaskName.Value;
				dr["CompletePercent"] = BLL.ConvertRule.ToInt(this.txtCompletePercent.Value);
				dr["DelayType"] = this.sltDelayType.Value;

				if (BLL.ConvertRule.ToInt(dr["DelayType"]) == 0) 
				{
					dr["DelayDays"] = DBNull.Value;
					dr["DelayTypeName"] = "";
				}
				else 
				{
					dr["DelayDays"] = BLL.ConvertRule.ToInt(this.txtDelayDays.Value);
					dr["DelayTypeName"] = this.sltDelayType.Items[this.sltDelayType.SelectedIndex].Text;
				}

				//���¿���ĸ���������ʾ
				DataTable dtTaskBudget = entity.Tables["TaskBudget"];
				foreach ( DataRow drTaskBudget in dtTaskBudget.Select("TaskBudgetCode='" + taskBudgetCode + "'"))
					drTaskBudget["PayConditionHtml"] = BLL.WBSRule.GetTaskBudgetPayConditionHtml(taskBudgetCode, entity.Tables["TaskBudgetCondition"], true);

				//���㸶��ʱ��
				string sPayDate = "";
				object PayDate = BLL.WBSRule.CalcTaskBudgetDateByTask(taskBudgetCode, dt);
				if (PayDate != null) 
				{
					sPayDate = ((DateTime)PayDate).ToString("yyyy-MM-dd");
				}
				Session["WBSEntity"]=entity;
				entity.Dispose();

				GoBack(sPayDate);
			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"�������");
				Response.Write(Rms.Web.JavaScript.Alert(true, "�������" + ex.Message));
				return;
			}

		}

		private void GoBack(string sPayDate)
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write( string.Format("window.opener.PayConditionReturn('{0}');", sPayDate));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ɾ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{
				EntityData entity = (EntityData)Session["WBSEntity"];
				DataTable dt = entity.Tables["TaskBudgetCondition"];

				string wbsCode = this.txtInputWBSCode.Value;
				string taskBudgetCode = this.txtTaskBudgetCode.Value;
				string taskBudgetConditionCode = this.txtTaskBudgetConditionCode.Value;

				DataRow[] drs = dt.Select("TaskBudgetConditionCode='" + taskBudgetConditionCode + "'");
				if (drs.Length > 0) 
				{
					drs[0].Delete();
				}

				//���¿���ĸ���������ʾ
				DataTable dtTaskBudget = entity.Tables["TaskBudget"];
				foreach ( DataRow dr in dtTaskBudget.Select("TaskBudgetCode='" + taskBudgetCode + "'"))
					dr["PayConditionHtml"] = BLL.WBSRule.GetTaskBudgetPayConditionHtml(taskBudgetCode, dtTaskBudget, true);
				Session["WBSEntity"]=entity;
				entity.Dispose();

			}
			catch( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"ɾ������");
				Response.Write(Rms.Web.JavaScript.Alert(true, "ɾ������" + ex.Message));
				return;
			}

			GoBack("");
		}

	}
}
