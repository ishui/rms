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
	/// ContractChangePrint ��ժҪ˵����
	/// </summary>
	public partial class ContractChangePrint : System.Web.UI.Page
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
				string projectCode = Request["ProjectCode"] + "";
				string contractCode = Request["ContractCode"]+"";
				string contractChangeCode = Request["ContractChangeCode"]+"";

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

				// ��ʾ��ͬ���
				ShowContractMoney(entity,contractChangeCode);

				//��ͬ���������Ϣ
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
