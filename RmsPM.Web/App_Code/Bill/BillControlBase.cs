using System;
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;

namespace RmsPM.Web.Bill
{
	/// *********************************************************************************************
	/// <summary>
	/// LeaveControlBaseClass 的摘要说明。公章领用申请基类
	/// </summary>
	/// *********************************************************************************************
	public class BillControlBase : System.Web.UI.UserControl
	{
		/// <summary>
		/// 业务代码
		/// </summary>
		private string _ApplicationCode = "";
		/// <summary>
		/// 模块状态
		/// </summary>
		private ModuleState _State = ModuleState.Unbeknown;
		/// <summary>
		/// 事务对象
		/// </summary>
		private StandardEntityDAO _dao;

		/// <summary>
		/// 业务代码
		/// </summary>
		public string ApplicationCode
		{
			get
			{
				if ( _ApplicationCode == "" )
				{
					if(this.ViewState["_ApplicationCode"] != null)
						return this.ViewState["_ApplicationCode"].ToString();
					return "";
				}
				return _ApplicationCode;
			}
			set
			{
				_ApplicationCode = value;
				this.ViewState["_ApplicationCode"] = value;
			}
		}

		/// <summary>
		/// 模块状态
		/// </summary>
		public ModuleState State
		{
			get
			{
				if ( _State == ModuleState.Unbeknown )
				{
					if(this.ViewState["_State"] != null)
						return (ModuleState)this.ViewState["_State"];
					return ModuleState.Unbeknown;
				}
				return _State;
			}
			set
			{
				_State = value;
				this.ViewState["_State"] = value;
			}
		}
		/// <summary>
		/// 事务对象
		/// </summary>
		public StandardEntityDAO dao
		{
			get
			{
				return this._dao;
			}
			set
			{
				_dao = value;
			}
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		public BillControlBase()
		{
		}
	}
}
