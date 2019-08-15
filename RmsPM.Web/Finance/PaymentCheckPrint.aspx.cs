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

namespace RmsPM.Web.Finance
{
	/// <summary>
	/// PaymentCheckPrint 的摘要说明。
	/// </summary>
	public partial class PaymentCheckPrint : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			if ( !IsPostBack )
			{
				LoadData();
			}
		}

		
		/// <summary>
		/// 装载控件数据
		/// </summary>
		public void LoadData()
		{
			try
			{
				string projectCode = Request["ProjectCode"]+"";
				string paymentCode = Request["PaymentCode"]+"";

				EntityData entity = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(paymentCode);

				if ( entity.HasRecord())
				{
					string contractCode = entity.GetString("ContractCode");
					EntityData entityCon =  DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
					if (entityCon.HasRecord()) 
					{
						lblProjectName.Text = BLL.ProjectRule.GetProjectName(projectCode); 
						lblContractName.Text = entityCon.GetString("ContractName");
						lblContractID.Text = entityCon.GetString("ContractID");
						lblSupplierName.Text = entity.GetString("SupplyName");
						lblCheckOpinion.Text = HttpUtility.HtmlEncode(entity.GetString("CheckOpinion")).Replace("\n","<br>");

						//显示合同金额
						decimal TotalMoney,TotalChangeMoney,ChangeMoney,OriginalMoney,BudgetMoney,AdjustMoney,NewTotalMoney;

						TotalMoney = entityCon.GetDecimal("TotalMoney");
						OriginalMoney = entityCon.GetDecimal("OriginalMoney");
						BudgetMoney = entityCon.GetDecimal("BudgetMoney");
						AdjustMoney = entityCon.GetDecimal("AdjustMoney");

						TotalChangeMoney = TotalMoney - OriginalMoney;
						ChangeMoney = BLL.MathRule.SumColumn(entityCon.Tables["ContractChange"].Select("Status in ( 1,2)","",System.Data.DataViewRowState.CurrentRows),"ChangeMoney");
						NewTotalMoney = TotalMoney + ChangeMoney;


						lblTotalMoney.Text = TotalMoney.ToString("N");
						lblTotalChangeMoney.Text = TotalChangeMoney.ToString("N");
						lblChangeMoney.Text = ChangeMoney.ToString("N");
						lblNewTotalMoney.Text = NewTotalMoney.ToString("N");
					}

					decimal negAHMoney,TotalItemMoney,TotalPayMoney;

					TotalItemMoney = entity.GetDecimal("Money");
					negAHMoney = - BLL.CBSRule.GetAHMoney("","","",contractCode,"1","");
					TotalPayMoney = TotalItemMoney - negAHMoney;
				

					lblTotalPayMoney.Text = TotalPayMoney.ToString("N");
					lblNegAHMoney.Text = negAHMoney.ToString("N");
					lblTotalItemMoney.Text = TotalItemMoney.ToString("N");
				}
			}
			catch(Exception ex)
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
