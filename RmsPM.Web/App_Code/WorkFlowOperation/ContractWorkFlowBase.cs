using System;

namespace RmsPM.Web.WorkFlowOperation
{
	/// <summary>
	/// ContractWorkFlowBase ��ժҪ˵����
	/// </summary>
	public class ContractWorkFlowBase : WorkFlowBase
	{

		#region --- ˽�г�Ա���� ---
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
		/// ��Ҫ���
		/// </summary>
		private int _Mostly = -1;

		#endregion --- ˽�г�Ա���� ---

		#region --- ���Լ��� ---
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
		/// ��Ҫ���
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

		#endregion --- ���Լ��� ---

		#region --- �������� ---

		protected string ShowHyperLinkContractName()
		{
			return "<a href=\"##\" onclick='doViewContractInfo(pCode,cCode);return false;' cCode='"  + this.ContractCode + "' pCode='" + this.ProjectCode + "'>" + this.ContractName + "</a>";
		}


		#endregion --- �������� ---

		public ContractWorkFlowBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
