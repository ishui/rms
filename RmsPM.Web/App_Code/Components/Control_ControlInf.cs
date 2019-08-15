using System;
using RmsPM.Web;
using System.Data;
using RmsPM.Web.WorkFlowControl;
namespace RmsPM.Web.Components
{
	
	
	public interface IUserControl_Control
	{
		DataTable ModuleTable
		{
			get;
			set;
		}
		PublicControl.ControlMessage ControlInfo
		{
			get;
			set;
		}
		void SetControlMessage();
	}
	#region 模块控制基类
		#endregion
	#region 共公信息类
	public class PublicControl
	{
		public static bool IsAllowShow(object nowState,object[] stateList)
		{
			foreach(object sl in stateList)
			{
				if(nowState.ToString()==sl.ToString())
					return true;
			}
			return false;
			//if(nowState.ToString)
		}
		public static bool IsAllowShow(object nowState,object stateList)
		{
			//foreach(object sl in stateList)
			//{
				if(nowState.ToString()==stateList.ToString())
					return true;

			return false;
			//if(nowState.ToString)
		}
		/// <summary>
		/// 控件信息
		/// </summary>
		public struct ControlMessage
		{
			/// <summary>
			/// 控件名字
			/// </summary>
			public string Name;
			/// <summary>
			/// 存放在址
			/// </summary>
			public string Url;
			/// <summary>
			/// 控件ID
			/// </summary>
			public string ID;
			/// <summary>
			/// 控件备注
			/// </summary>
			public string Remark;
			/// <summary>
			/// 控件状态
			/// </summary>
			public string State;
			//Rms.WorkFlow.
		}
		/// <summary>
		/// 模块信息
		/// </summary>
		public struct ModuleMessage
		{
			/// <summary>
			/// 模块名字
			/// </summary>
			public string ModuleName;
			/// <summary>
			/// 模块ID
			/// </summary>
			public string ModuleID;
			/// <summary>
			/// 模块备注
			/// </summary>
			public string Remark;
			/// <summary>
			/// 模块状态
			/// </summary>
			public RmsPM.Web.WorkFlowControl.ModuleState State;
		}
		//public struct 
	}
	#endregion
	
	/// <summary>
	/// Control_ControlInf 的摘要说明。
	/// </summary>
	public class Control_ControlInf
	{
		public Control_ControlInf()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
	}
}
