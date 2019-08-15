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
	/// 合同执行计划执行信息 的摘要说明。
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

		}
		#endregion

	}
}
