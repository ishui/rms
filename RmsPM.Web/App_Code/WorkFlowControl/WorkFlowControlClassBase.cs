using System;
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;

namespace RmsPM.Web.WorkFlowControl
{
	/// <summary>
	/// PurchaseClassBase ��ժҪ˵����
	/// </summary>
	public class WorkFlowControlClassBase : System.Web.UI.UserControl
	{
		public WorkFlowControlClassBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
