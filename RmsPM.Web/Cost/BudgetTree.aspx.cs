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
	/// BudgetTree ��ժҪ˵����
	/// </summary>
	public partial class BudgetTree : PageBase
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
				case "Budget":
					showItems = "CostName,SortID,TotalMoney,BudgetCost,BeforeHappenCost,CurrentPlanCost,AfterPlanCost,BudgetBalanceSign,Curve";
					break;
			}

			this.txtShowItems.Value = showItems;
			this.txtTreeType.Value = treeType;
			this.txtCheckBalance.Value = checkBalance;

		}

		private void SumTotalMoney()
		{
//			string projectCode = Request["ProjectCode"] + "";
//
//			string totalEstimateCost = BLL.StringRule.BuildMoneyWanFormatString( BLL.CBSRule.SumTotalEstimateCost("",projectCode));
//			this.tdFootTotalMoney.InnerHtml = totalEstimateCost;
//
//			decimal budgetCost = decimal.Zero;
//			decimal beforeHappenCost = decimal.Zero;
//			decimal currentPlanCost = decimal.Zero;
//			decimal afterPlanCost = decimal.Zero;
//
//			string budgetCode = Request["BudgetCode"] + "";
//			BLL.CBSRule.SumBudgetMoney( projectCode, budgetCode
//				, ref budgetCost, ref beforeHappenCost , ref currentPlanCost , ref afterPlanCost );
//
//
//			this.tdFootBudgetCost.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(budgetCost);
//			this.tdFootBeforeHappenCost.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(beforeHappenCost);
//			this.tdFootCurrentPlanCost.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(currentPlanCost);
//			this.tdFootAfterPlanCost.InnerHtml = BLL.StringRule.BuildMoneyWanFormatString(afterPlanCost);

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
