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

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// CostAccountReportPre 的摘要说明。
	/// </summary>
	public partial class CostAccountReportPre : PageBase
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( !IsPostBack )
			{
				IniPage();
			}
		}

		private void IniPage()
		{
			this.dtbEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
			this.dtbReportDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
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

		protected void btnReport_ServerClick(object sender, System.EventArgs e)
		{
			string sEndDate = this.dtbEndDate.Value;
			string projectCode = Request["ProjectCode"] + "";
			Response.Write(Rms.Web.JavaScript.WinOpenMax(true,@"CostAccountReport.aspx?ProjectCode="+projectCode+"&EndDate=" + sEndDate + "&ReportDate=" + this.dtbReportDate.Value + "&UnitText=" + this.txtUnit.Value ,"成本费用对比表"));
			Response.End();
		}
	}
}
