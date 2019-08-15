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
	/// PaymentCheckPrint ��ժҪ˵����
	/// </summary>
	public partial class PaymentCheckPrint : System.Web.UI.Page
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

						//��ʾ��ͬ���
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
