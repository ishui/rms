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
using RmsPM.DAL.QueryStrategy;
using Rms.Web;

namespace RmsPM.Web.CostBudget
{
	/// <summary>
	/// CostBudgetOnlineUpdate ��ժҪ˵����
	/// </summary>
	public partial class CostBudgetOnlineUpdate : PageBase
	{
		protected System.Web.UI.WebControls.Label lblCostBudgetSetName;

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
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
				this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
				this.txtAct.Value = Request.QueryString["Act"];
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʼ��ҳ�����" + ex.Message));
			}
		}

		private void LoadData()
		{
			string ProjectCode = this.txtProjectCode.Value;

			try
			{
				if ( ProjectCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "δ������Ŀ���"));
					Response.Write(Rms.Web.JavaScript.WinClose(true));
					return;
				}

				if (this.txtAct.Value == "OnlineUpdate")
				{
					OnlineUpdate();
				}
			}
			catch (  Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��ʾ���ݳ���" + ex.Message));
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
		/// ��ʱ����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnlineUpdate()
		{
			string CostBudgetBackupCode = "";

			try
			{
				CostBudgetBackupCode = BLL.CostBudgetRule.OnlineUpdate(this.txtProjectCode.Value, base.user.UserCode);
			}
			catch(Exception ex) 
			{
				Response.Write(JavaScript.Alert(true, "��ʱ����ʧ�ܣ�" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack(CostBudgetBackupCode);
		}

		/// <summary>
		/// ����
		/// </summary>
		private void GoBack(string CostBudgetBackupCode) 
		{
			Response.Write(Rms.Web.JavaScript.ScriptStart);
			Response.Write(string.Format("window.opener.OnlineUpdateReturn('{0}');", CostBudgetBackupCode));
			Response.Write(Rms.Web.JavaScript.WinClose(false));
			Response.Write(Rms.Web.JavaScript.ScriptEnd);
		}

	}
}
