//====================================================================
// This file is generated as part of Web project conversion.
// The extra class 'EventType' in the code behind file in 'workflowcontrol\WorkFlowControlButton.ascx.cs' is moved to this file.
//====================================================================




namespace RmsPM.Web.WorkFlowControl
 {

	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rms.ORMap;
	using Rms.WorkFlow;

	public enum EventType
	{
		/// <summary>
		/// 非操作
		/// </summary>
		No,
		/// <summary>
		/// 发送
		/// </summary>
		Send,
		/// <summary>
		/// 保存
		/// </summary>
		Save,
		/// <summary>
		/// 签收
		/// </summary>
		In,
		/// <summary>
		/// 结束
		/// </summary>
		End,
		/// <summary>
		/// 当前操作完成（会签时使用）
		/// </summary>
		TaskFinish

	}

}