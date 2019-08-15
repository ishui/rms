using System;
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;

namespace RmsPM.Web.Bill
{
	/// *********************************************************************************************
	/// <summary>
	/// LeaveControlBaseClass ��ժҪ˵�������������������
	/// </summary>
	/// *********************************************************************************************
	public class BillControlBase : System.Web.UI.UserControl
	{
		/// <summary>
		/// ҵ�����
		/// </summary>
		private string _ApplicationCode = "";
		/// <summary>
		/// ģ��״̬
		/// </summary>
		private ModuleState _State = ModuleState.Unbeknown;
		/// <summary>
		/// �������
		/// </summary>
		private StandardEntityDAO _dao;

		/// <summary>
		/// ҵ�����
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
		/// ģ��״̬
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
		/// �������
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
		/// ���캯��
		/// </summary>
		public BillControlBase()
		{
		}
	}
}
