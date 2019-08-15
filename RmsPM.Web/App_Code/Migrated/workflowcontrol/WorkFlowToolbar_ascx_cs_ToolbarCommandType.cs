//====================================================================
// This file is generated as part of Web project conversion.
// The extra class 'ToolbarCommandType' in the code behind file in 'workflowcontrol\WorkFlowToolbar.ascx.cs' is moved to this file.
//====================================================================




namespace RmsPM.Web.WorkFlowControl
 {

	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using System.Text;
	using Rms.ORMap;
	using Rms.WorkFlow;

	public enum ToolbarCommandType
	{
		/// <summary>
		/// 未知
		/// </summary>
		Unbeknown,
		/// <summary>
		/// 签收
		/// </summary>
		SignIn,
		/// <summary>
		/// 保存
		/// </summary>
		Save,
		/// <summary>
		/// 发送
		/// </summary>
		Send,
		/// <summary>
		/// 结束
		/// </summary>
		Finish,
		/// <summary>
		/// 完成（会签时使用）
		/// </summary>
		TaskFinish,
		/// <summary>
		/// 抄送
		/// </summary>
		MakeCopy,
		/// <summary>
		/// 删除
		/// </summary>
		Delete,
		/// <summary>
		/// 退回
		/// </summary>
		Back,
		/// <summary>
		/// 直送经办人
		/// </summary>
		BackTop,
		/// <summary>
		/// 意见保存
		/// </summary>
		Opinion,
		/// <summary>
		/// 收回
		/// </summary>
		Return,
        /// <summary>
        /// 作废
        /// </summary>
        BlankOut,
        /// <summary>
        /// 表单意见保存
        /// </summary>
        OpinionForward
	}

}