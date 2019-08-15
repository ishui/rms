using System;

namespace RmsPM.Web.WorkFlowOperation
{
	/// <summary>
	/// ContractWorkFlowBase 的摘要说明。
	/// </summary>
	public class ContractWorkFlowBase : WorkFlowBase
	{

		#region --- 私有成员集合 ---
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
		/// 主要标段
		/// </summary>
		private int _Mostly = -1;

		#endregion --- 私有成员集合 ---

		#region --- 属性集合 ---
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
		/// 主要标段
		/// </summary>
		public int Mostly
		{
			get
			{
				if ( _Mostly == -1 )
				{
					if(this.ViewState["_Mostly"] != null)
						return (int)this.ViewState["_Mostly"];
					return 0;
				}
				return _Mostly;
			}
			set
			{
				_Mostly = value;
				this.ViewState["_Mostly"] = value;
			}
		}

		#endregion --- 属性集合 ---

		#region --- 公共方法 ---

		protected string ShowHyperLinkContractName()
		{
			return "<a href=\"##\" onclick='doViewContractInfo(pCode,cCode);return false;' cCode='"  + this.ContractCode + "' pCode='" + this.ProjectCode + "'>" + this.ContractName + "</a>";
		}


		#endregion --- 公共方法 ---

		public ContractWorkFlowBase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
