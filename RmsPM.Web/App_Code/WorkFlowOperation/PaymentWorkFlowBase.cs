using System;

namespace RmsPM.Web.WorkFlowOperation
{
	/// <summary>
	/// PaymentWorkFlowBase ��ժҪ˵����
	/// </summary>
	public class PaymentWorkFlowBase : WorkFlowBase
	{

		#region --- ˽�г�Ա���� ---
		/// <summary>
		/// �����
		/// </summary>
		private string _PaymentCode = "";
		/// <summary>
		/// ��ͬ���
		/// </summary>
		private string _ContractCode = "";
		/// <summary>
		/// ��ͬ����
		/// </summary>
		private string _ContractName = "";
		/// <summary>
		/// ��ͬ���
		/// </summary>
		private decimal _Money = Decimal.Zero;
		/// <summary>
		/// ������
		/// </summary>
		private decimal _PayMoney = Decimal.Zero;
		/// <summary>
		/// �ۼ�����
		/// </summary>
		private decimal _AHMoney = Decimal.Zero;
		/// <summary>
		/// �ۼ��Ѹ�
		/// </summary>
		private decimal _APMoney = Decimal.Zero;

		#endregion --- ˽�г�Ա���� ---

		#region --- ���Լ��� ---
		/// <summary>
		/// �����
		/// </summary>
		public string PaymentCode
		{
			get
			{
				if ( _PaymentCode == "" )
				{
					if(this.ViewState["_PaymentCode"] != null)
						return this.ViewState["_PaymentCode"].ToString();
					return "";
				}
				return _PaymentCode;
			}
			set
			{
				_PaymentCode = value;
				this.ViewState["_PaymentCode"] = value;
			}
		}

		/// <summary>
		/// ��ͬ���
		/// </summary>
		public string ContractCode
		{
			get
			{
				if ( _ContractCode == "" )
				{
					if(this.ViewState["_ContractCode"] != null)
						return this.ViewState["_ContractCode"].ToString();
					return "";
				}
				return _ContractCode;
			}
			set
			{
				_ContractCode = value;
				this.ViewState["_ContractCode"] = value;
			}
		}

		/// <summary>
		/// ��ͬ����
		/// </summary>
		public string ContractName
		{
			get
			{
				if ( _ContractName == "" )
				{
					if(this.ViewState["_ContractName"] != null)
						return this.ViewState["_ContractName"].ToString();
					return "";
				}
				return _ContractName;
			}
			set
			{
				_ContractName = value;
				this.ViewState["_ContractName"] = value;
			}
		}

		/// <summary>
		/// ��ͬ���
		/// </summary>
		public decimal Money
		{
			get
			{
				if ( _Money == Decimal.Zero )
				{
					if(this.ViewState["_Money"] != null)
						return (decimal)this.ViewState["_Money"];
					return Decimal.Zero;
				}
				return _Money;
			}
			set
			{
				_Money = value;
				this.ViewState["_Money"] = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public decimal PayMoney
		{
			get
			{
				if ( _PayMoney == Decimal.Zero )
				{
					if(this.ViewState["_PayMoney"] != null)
						return (decimal)this.ViewState["_PayMoney"];
					return Decimal.Zero;
				}
				return _PayMoney;
			}
			set
			{
				_PayMoney = value;
				this.ViewState["_PayMoney"] = value;
			}
		}

		/// <summary>
		/// �ۼ�����
		/// </summary>
		public decimal AHMoney
		{
			get
			{
				if ( _AHMoney == Decimal.Zero )
				{
					if(this.ViewState["_AHMoney"] != null)
						return (decimal)this.ViewState["_AHMoney"];
					return Decimal.Zero;
				}
				return _AHMoney;
			}
			set
			{
				_AHMoney = value;
				this.ViewState["_AHMoney"] = value;
			}
		}

		/// <summary>
		/// �ۼ��Ѹ�
		/// </summary>
		public decimal APMoney
		{
			get
			{
				if ( _APMoney == Decimal.Zero )
				{
					if(this.ViewState["_APMoney"] != null)
						return (decimal)this.ViewState["_APMoney"];
					return Decimal.Zero;
				}
				return _APMoney;
			}
			set
			{
				_APMoney = value;
				this.ViewState["_APMoney"] = value;
			}
		}

		#endregion --- ���Լ��� ---

		#region --- �������� ---

		protected string ShowHyperLinkContractName()
		{
            return "<a href=\"##\" style =\"text-decoration:none;\" onclick='doViewContractInfo(pCode,cCode);return false;' cCode='" + this.ContractCode + "' pCode='" + this.ProjectCode + "'>" + this.ContractName + "</a>";
		}

		#endregion --- �������� ---

		public PaymentWorkFlowBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
