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

namespace RmsPM.Web.Contract
{
	/// <summary>
	/// ContractChangePrint 的摘要说明。
	/// </summary>
	public partial class ContractChangePrint : System.Web.UI.Page
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
				string projectCode = Request["ProjectCode"] + "";
				string contractCode = Request["ContractCode"]+"";
				string contractChangeCode = Request["ContractChangeCode"]+"";

				EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);

				if ( !entity.HasRecord() )
				{
					return;						
				}

				//合同基本信息
				lblProjectName.Text = BLL.ProjectRule.GetProjectName(projectCode);
				lblContractID.Text = entity.GetString("ContractID");

				lblContractName.Text  = entity.GetString("ContractName");
				lblSupplierName.Text = BLL.ProjectRule.GetSupplierName( entity.GetString("SupplierCode"));

				// 显示合同金额
				ShowContractMoney(entity,contractChangeCode);

				//合同变更基本信息
				entity.SetCurrentTable("ContractChange");
				foreach ( DataRow dr in entity.CurrentTable.Select(String.Format("ContractChangeCode='{0}'",contractChangeCode)))
				{
					lblVoucher.Text = dr["Voucher"].ToString();
					lblChangeID.Text =dr["ContractChangeId"].ToString();
					lblChangeReason.Text = dr["ChangeReason"].ToString();

					lblSupplierChangeMoney.Text = ((Decimal)dr["SupplierChangeMoney"]).ToString("N");
					lblConsultantAuditMoney.Text = ((Decimal)dr["ConsultantAuditMoney"]).ToString("N");
					lblProjectAuditMoney.Text = ((Decimal)dr["ProjectAuditMoney"]).ToString("N");
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

		private void ShowContractMoney( EntityData entity,string contractChangeCode )
		{
			entity.SetCurrentTable("Contract");

			decimal TotalMoney,TotalChangeMoney,OriginalMoney,NewTotalMoney,ChangeMoney,BudgetMoney,AdjustMoney;;

			OriginalMoney = entity.GetDecimal("OriginalMoney");
			BudgetMoney = entity.GetDecimal("BudgetMoney");
			AdjustMoney = entity.GetDecimal("AdjustMoney");

			TotalMoney = Decimal.Zero;
			TotalChangeMoney = Decimal.Zero;
			NewTotalMoney = Decimal.Zero;
			ChangeMoney = Decimal.Zero;

			foreach ( DataRow dr in entity.Tables["ContractChange"].Select( string.Format("ContractChangeCode={0}",contractChangeCode),"",System.Data.DataViewRowState.CurrentRows) )
			{
				TotalMoney =  dr["Money"] != DBNull.Value ? (decimal)dr["Money"] : Decimal.Zero;
				TotalChangeMoney = dr["TotalChangeMoney"] != DBNull.Value ? (decimal)dr["TotalChangeMoney"] : Decimal.Zero;
				NewTotalMoney = dr["NewMoney"] != DBNull.Value ? (decimal)dr["NewMoney"] : Decimal.Zero;
				ChangeMoney = dr["ChangeMoney"] != DBNull.Value ? (decimal)dr["ChangeMoney"] : Decimal.Zero;
			}


			lblTotalMoney.Text = TotalMoney.ToString("N");
			lblTotalChangeMoney.Text = TotalChangeMoney.ToString("N");
			lblChangeMoney.Text = ChangeMoney.ToString("N");
			lblNewTotalMoney.Text = NewTotalMoney.ToString("N");

		}
	}
}
