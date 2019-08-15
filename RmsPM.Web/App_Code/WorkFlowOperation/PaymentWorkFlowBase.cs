using System;

namespace RmsPM.Web.WorkFlowOperation
{
	/// <summary>
	/// PaymentWorkFlowBase 的摘要说明。
	/// </summary>
	public class PaymentWorkFlowBase : WorkFlowBase
	{

		#region --- 私有成员集合 ---
		/// <summary>
		/// 请款单编号
		/// </summary>
		private string _PaymentCode = "";
		/// <summary>
		/// 合同编号
		/// </summary>
		private string _ContractCode = "";
		/// <summary>
		/// 合同名称
		/// </summary>
		private string _ContractName = "";
		/// <summary>
		/// 合同金额
		/// </summary>
		private decimal _Money = Decimal.Zero;
		/// <summary>
		/// 付款金额
		/// </summary>
		private decimal _PayMoney = Decimal.Zero;
		/// <summary>
		/// 累计已批
		/// </summary>
		private decimal _AHMoney = Decimal.Zero;
		/// <summary>
		/// 累计已付
		/// </summary>
		private decimal _APMoney = Decimal.Zero;

		#endregion --- 私有成员集合 ---

		#region --- 属性集合 ---
		/// <summary>
		/// 请款单编号
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
		/// 合同编号
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
		/// 合同名称
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
		/// 合同金额
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
		/// 付款金额
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
		/// 累计已批
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
		/// 累计已付
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

		#endregion --- 属性集合 ---

		#region --- 公共方法 ---

		protected string ShowHyperLinkContractName()
		{
            return "<a href=\"##\" style =\"text-decoration:none;\" onclick='doViewContractInfo(pCode,cCode);return false;' cCode='" + this.ContractCode + "' pCode='" + this.ProjectCode + "'>" + this.ContractName + "</a>";
		}

		#endregion --- 公共方法 ---

		public PaymentWorkFlowBase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
