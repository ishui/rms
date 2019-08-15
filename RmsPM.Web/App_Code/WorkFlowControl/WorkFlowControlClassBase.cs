using System;
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;

namespace RmsPM.Web.WorkFlowControl
{
	/// <summary>
	/// PurchaseClassBase 的摘要说明。
	/// </summary>
	public class WorkFlowControlClassBase : System.Web.UI.UserControl
	{
		public WorkFlowControlClassBase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private string _ApplicationCode = "";
		private ModuleState _State = ModuleState.Unbeknown;
		private StandardEntityDAO _dao;

		public string ApplicationCode
		{
			get
			{
				return _ApplicationCode;
			}
			set
			{
				_ApplicationCode = value;
			}
		}
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

	}
}
