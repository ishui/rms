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
	/// ContractAccountPrint ��ժҪ˵����
	/// </summary>
	public partial class ContractAccountPrint : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
			if ( !IsPostBack )
			{
				LoadData();
			}
		}

		/// <summary>
		/// װ�ؿؼ�����
		/// </summary>
		public void LoadData()
		{
			try
			{
				string contractCode = Request["ContractCode"]+"";
				string projectCode = Request["ProjectCode"]+"";

				EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);

				if ( !entity.HasRecord() )
				{
					return;						
				}



				//��ͬ������Ϣ
				lblProjectName.Text = BLL.ProjectRule.GetProjectName(projectCode);
				lblContractID.Text = entity.GetString("ContractID");

				lblContractName.Text  = entity.GetString("ContractName");
				lblSupplierName.Text = BLL.ProjectRule.GetSupplierName( entity.GetString("SupplierCode"));

				//��ʾ��ͬ���
				decimal TotalMoney,TotalChangeMoney,OriginalMoney,NewTotalMoney,ChangeMoney,BudgetMoney,AdjustMoney,EstimateChangeMoney;

				string contractLabel = entity.GetString("ContractLabel");

				TotalMoney = entity.GetDecimal("TotalMoney");
				OriginalMoney = entity.GetDecimal("OriginalMoney");
				BudgetMoney = entity.GetDecimal("BudgetMoney");
				AdjustMoney = entity.GetDecimal("AdjustMoney");

				TotalChangeMoney = TotalMoney - OriginalMoney;

				if ( contractLabel != "" )
				{
					NewTotalMoney = BLL.MathRule.SumColumn(entity.Tables["ContractCostChange"].Select(String.Format("ContractChangeCode='{0}'",contractLabel)),"NewMoney");
				}
				else
				{
					NewTotalMoney = TotalMoney;
				}
				ChangeMoney = NewTotalMoney - TotalMoney;
				EstimateChangeMoney = TotalChangeMoney + ChangeMoney;

				lblTotalChangeMoney.Text = TotalChangeMoney.ToString("#,##0.00");
				lblChangeMoney.Text = ChangeMoney.ToString("#,##0.00");
				lblEstimateChangeMoney.Text = EstimateChangeMoney.ToString("#,##0.00");

				entity.Dispose();

			}
			catch(Exception ex)
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
