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
using RmsPM.BLL;
using RmsPM.DAL;

namespace RmsPM.Web.Cost
{
	/// <summary>
	/// DynamicCostTree 的摘要说明。
	/// </summary>
	public partial class DynamicCostTree : PageBase
	{

	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if ( ! this.IsPostBack )
			{
				IniPage();
				SumTotalMoney();
			}
		}

		private void IniPage()
		{
			string treeType = Request["treeType"] + "";
			string checkBalance = Request["CheckBalance"] + "";
			string showItems = "";
			switch (treeType)
			{
				case "DynamicCost":
					showItems = "CostName,SortID,DynamicCost,BeforeHappenCost,CurrentPlanCost,CurrentMonthBudget,CurrentMonthAH,CurrentMonthContract";
					break;
			}

			this.txtShowItems.Value = showItems;
			this.txtTreeType.Value = treeType;
			this.txtCheckBalance.Value = checkBalance;

		}

		private void SumTotalMoney()
		{
//
//			
////
////			string totalEstimateCost = BLL.StringRule.BuildMoneyWanFormatString( BLL.CBSRule.SumTotalEstimateCost("",projectCode));
////			this.tdFootTotalMoney.InnerHtml = totalEstimateCost;
////
////			decimal budgetCost = decimal.Zero;
////			decimal beforeHappenCost = decimal.Zero;
////			decimal currentPlanCost = decimal.Zero;
////			decimal afterPlanCost = decimal.Zero;
////
//			
////			BLL.CBSRule.SumBudgetMoney( projectCode, budgetCode
////				, ref budgetCost, ref beforeHappenCost , ref currentPlanCost , ref afterPlanCost );
////
////
////			this.tdFootBudgetCost.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(budgetCost);
////			this.tdFootBeforeHappenCost.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(beforeHappenCost);
////			this.tdFootCurrentPlanCost.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(currentPlanCost);
////			this.tdFootAfterPlanCost.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(afterPlanCost);
//
//			string budgetCode = Request["BudgetCode"] + "";
//			string projectCode = Request["ProjectCode"] + "";
//
//			try
//			{
//				EntityData dynamicData = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(budgetCode);
//				int iYear = dynamicData.GetInt("IYear");
//				int iMonth = dynamicData.GetInt("IMonth");
//				// 本月是第几月
//				int months = (DateTime.Now.Year - iYear)*12 + ( DateTime.Now.Month - iMonth ) + 1; 
//
//				//string startDate = iYear.ToString() + "-" + iMonth.ToString() + "-1";
//
//				DateTime periodEndDate = DateTime.Parse(dynamicData.GetDateTime("EndDate"));
//				DateTime periodStartDate = DateTime.Parse(dynamicData.GetDateTime("StartDate"));
//
//				//string currentBudgetPlanCode = dynamicData.GetString("ReferBudgetCode");
//				//EntityData budgetData = DAL.EntityDAO.CBSDAO.GetStandard_BudgetByCode(currentBudgetPlanCode);
//
//				this.tdFootDynamicCost.InnerText = BLL.StringRule.BuildMoneyWanFormatString( dynamicData.GetDecimal("TotalMoney") ) ;
//				this.tdFootCurrentPlanCost.InnerText = BLL.StringRule.BuildMoneyWanFormatString( dynamicData.GetDecimal("CurrentPlanCost") ) ;
//				this.tdFootCurrentMonthBudget.InnerHtml= BLL.StringRule.BuildMoneyWanFormatString( BLL.MathRule.SumColumn( dynamicData.Tables["BudgetMonth"],"Money",String.Format( "IMonth={0} " ,months )));
//
//				DateTime currentMonthStartDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-1"));
//				DateTime currentMonthEndDate = currentMonthStartDate.AddMonths(1).AddDays(-1);
//
//				// 上月末为止，已经发生数
////				DateTime preMonthEndDate = currentMonthStartDate.AddDays(-1);
////				decimal preMonthAHMoney = BLL.CBSRule.GetProjectAHMoney(projectCode,"",preMonthEndDate.ToString("yyyy-MM-dd"));
//				
//				// 本月的已经发生
//				decimal currentMonthAH = BLL.CBSRule.GetProjectAHMoney(projectCode,currentMonthStartDate.ToString("yyyy-MM-dd"),currentMonthEndDate.ToString("yyyy-MM-dd"));
//				this.tdFootCurrentMonthAH.InnerText = BLL.StringRule.BuildMoneyWanFormatString( currentMonthAH);
//
//				// 到本月月末为止，总的合同应付款
//				decimal currentMonthContract = BLL.CBSRule.GetContractAllocationCost("","",projectCode,periodStartDate.ToString("yyyy-MM-dd"),currentMonthEndDate.ToString("yyyy-MM-dd"))
//					- BLL.CBSRule.GetContractAllocationHappenedCost("","",projectCode,periodStartDate.ToString("yyyy-MM-dd"),currentMonthEndDate.ToString("yyyy-MM-dd"));
//
//				
//				// 到上月末为止，总到合同实际付款
////				decimal preMonthPayed = BLL.CBSRule.GetProjectAPCost(projectCode,"",preMonthEndDate.ToString("yyyy-MM-dd"),"1");
//
////				// 本月的合同应付款
////				decimal currentMonthContract = planPayMoney - preMonthPayed;
////				if ( currentMonthContract < decimal.Zero )
////					currentMonthContract = decimal.Zero;
//
//				this.tdFootCurrentMonthContract.InnerText = BLL.StringRule.BuildMoneyWanFormatString(currentMonthContract);
//
//				dynamicData.Dispose();
//				//budgetData.Dispose();
//
//			}
//			catch ( Exception ex )
//			{
//				ApplicationLog.WriteLog(this.ToString(),ex,"");
//			}


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
