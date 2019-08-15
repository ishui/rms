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
	#region ģ����ƻ���
		#endregion
	#region ������Ϣ��
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
		/// �ؼ���Ϣ
		/// </summary>
		public struct ControlMessage
		{
			/// <summary>
			/// �ؼ�����
			/// </summary>
			public string Name;
			/// <summary>
			/// �����ַ
			/// </summary>
			public string Url;
			/// <summary>
			/// �ؼ�ID
			/// </summary>
			public string ID;
			/// <summary>
			/// �ؼ���ע
			/// </summary>
			public string Remark;
			/// <summary>
			/// �ؼ�״̬
			/// </summary>
			public string State;
			//Rms.WorkFlow.
		}
		/// <summary>
		/// ģ����Ϣ
		/// </summary>
		public struct ModuleMessage
		{
			/// <summary>
			/// ģ������
			/// </summary>
			public string ModuleName;
			/// <summary>
			/// ģ��ID
			/// </summary>
			public string ModuleID;
			/// <summary>
			/// ģ�鱸ע
			/// </summary>
			public string Remark;
			/// <summary>
			/// ģ��״̬
			/// </summary>
			public RmsPM.Web.WorkFlowControl.ModuleState State;
		}
		//public struct 
	}
	#endregion
	
	/// <summary>
	/// Control_ControlInf ��ժҪ˵����
	/// </summary>
	public class Control_ControlInf
	{
		public Control_ControlInf()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
