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

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// CostEstimateTree 的摘要说明。
	/// </summary>
	public partial class CostEstimateTree : PageBase
	{
		protected System.Web.UI.HtmlControls.HtmlTableCell tdFootReviseCost;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdFootDynamicCost;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			IniPage();
			SumTotalMoney();
		}

		private void IniPage()
		{
		}

		private void SumTotalMoney()
		{
//			string projectCode = Request["ProjectCode"] + "";
//			string totalEstimateCost = BLL.StringRule.BuildMoneyWanFormatString( BLL.CBSRule.SumTotalEstimateCost("",projectCode));
//			this.tdFootTotalMoney.InnerHtml = totalEstimateCost;
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
