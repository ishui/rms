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

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ��ִͬ�мƻ�ִ����Ϣ ��ժҪ˵����
	/// </summary>
	public partial class ContractExecutePlanInfo : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
				LoadData();
			}
		}

		private void IniPage()
		{
		}

		private void LoadData()
		{
			string code = Request["ContractExecutePlanCode"] + "";
			if (code == "" )
				return;

			try
			{
				EntityData entity=DAL.EntityDAO.ContractDAO.GetContractExecutePlanByCode(code);
				if ( entity.HasRecord())
				{
					this.LabelsltPerson.Text =BLL.SystemRule.GetUserName( entity.GetString("Executor"));
					this.LabeldtbExecuteDate.Text = entity.GetDateTimeOnlyDate("ExecuteDate");
					this.lblExecuteDetail.Text = entity.GetString("ExecuteDetail");
					this.lblDescription.Text = entity.GetString("Description").Replace("\n", "<br>");
					this.lblFactExecuteDate.Text = entity.GetDateTimeOnlyDate("FactExecuteDate");
					this.lblFactPerson.Text  = BLL.SystemRule.GetUserName(entity.GetString("FactExecutor") );
				}
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
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

	}
}
