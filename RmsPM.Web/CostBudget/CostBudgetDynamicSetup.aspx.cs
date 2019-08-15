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
using RmsPM.DAL.EntityDAO;
using RmsPM.BLL;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetDynamicSetup ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetDynamicSetup : PageBase
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage() 
		{
			try 
			{
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			try
			{
				if (this.txtProjectCode.Value == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ������Ŀ���"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
				}

				decimal ValidHours = BLL.CostBudgetRule.GetOfflineValidHours(this.txtProjectCode.Value);
				this.txtValidHours.Value = ValidHours.ToString();

				if (ValidHours > 0)
				{
					this.rdoOfflineType1.Checked = true;
				}
				else
				{
					this.rdoOfflineType0.Checked = true;
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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
		/// ����
		/// </summary>
		private void SavaData()
		{
			try
			{
				if (this.rdoOfflineType0.Checked) 
				{
					this.txtValidHours.Value = "";
				}

				BLL.SystemRule.UpdateProjectConfigValue(this.txtProjectCode.Value, BLL.SystemRule.m_ConfigCostBudgetOffineValidHours, BLL.ConvertRule.ToDecimal(this.txtValidHours.Value));

				if (this.rdoOfflineType0.Checked) //��ʱ
				{
					//ɾ���Ǽ�ʱ�汾
					BLL.CostBudgetRule.DeleteCostBudgetBackup(BLL.CostBudgetRule.GetOfflineBackupCode(this.txtProjectCode.Value));
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// ��Ч�Լ��
		/// </summary>
		/// <param name="Hint"></param>
		/// <returns></returns>
		private bool CheckValid(ref string Hint) 
		{
			Hint = "";

			if (this.rdoOfflineType1.Checked) 
			{
				if (this.txtValidHours.Value == "")
				{
					Hint = "������Ǽ�ʱ״̬�µ���Ч��";
					return false;
				}

				if (this.txtValidHours.Value != "")
				{
					if (!Rms.Check.StringCheck.IsNumber(this.txtValidHours.Value))
					{
						Hint = "�Ǽ�ʱ״̬�µ���Ч�ڱ�������ֵ";
						return false;
					}

					if (BLL.ConvertRule.ToDecimal(this.txtValidHours.Value) <= 0)
					{
						Hint = "�Ǽ�ʱ״̬�µ���Ч�ڱ������0";
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack() 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);

			string ReturnFunc = "" + Request.QueryString["ReturnFunc"];
			if (ReturnFunc != "")
			{
				Response.Write(string.Format("window.opener.{0}();", ReturnFunc));
			}

//			Response.Write("window.opener.location = window.opener.location;");
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

		/// <summary>
		/// ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSave_ServerClick(object sender, System.EventArgs e)
		{
			try
			{
				string Hint = "";
				if (!CheckValid(ref Hint)) 
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
					return;
				}

				SavaData();

				GoBack();
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
			}
		}

	}
}
