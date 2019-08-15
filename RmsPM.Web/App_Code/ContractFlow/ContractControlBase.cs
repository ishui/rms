using System;

using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;
using System.Data;

namespace RmsPM.Web.ContractFlow
{
	/// <summary>
	/// ContractControlBase 的摘要说明。
	/// </summary>
	public class ContractControlBase : System.Web.UI.UserControl
	{
		public ContractControlBase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private string _ApplicationCode = "";
		private ModuleState _State = ModuleState.Unbeknown;

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

		public static void ContractAuditing(string ContractCode)
		{
			try
			{
				string contractCode = ContractCode;
				EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
				int status = entity.GetInt("Status");
				entity.CurrentRow["Status"] = 0;
				entity.CurrentRow["CheckDate"]=DateTime.Now.ToShortDateString();
				DAL.EntityDAO.ContractDAO.UpdateContract(entity);
				entity.Dispose();
			}
			catch ( Exception ex )
			{
				ApplicationLog.WriteLog("",ex,"");
			}

		}

	}
}
