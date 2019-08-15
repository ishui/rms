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
	/// CostBudgetOnlineUpdate 的摘要说明。
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void LoadData()
		{
			string ProjectCode = this.txtProjectCode.Value;

			try
			{
				if ( ProjectCode == "")
				{
					Response.Write(Rms.Web.JavaScript.Alert(true, "未传入项目编号"));
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
				Response.Write(Rms.Web.JavaScript.Alert(true, "显示数据出错：" + ex.Message));
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

		/// <summary>
		/// 即时更新
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
				Response.Write(JavaScript.Alert(true, "即时更新失败：" + ex.Message));
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				return;
			}

			GoBack(CostBudgetBackupCode);
		}

		/// <summary>
		/// 返回
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
