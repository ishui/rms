//====================================================================
// This file is generated as part of Web project conversion.
// The extra class 'ModuleState' in the code behind file in 'workflowcontrol\WorkFlowToolbar.ascx.cs' is moved to this file.
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

	public enum ModuleState
	{
		/// <summary>
		/// 未知的 1
		/// </summary>
		Unbeknown,
		/// <summary>
		/// 可见的  2
		/// </summary>
		Eyeable,
		/// <summary>
		/// 可操作的 3
		/// </summary>
		Operable,
		/// <summary>
		/// 不可见的 4
		/// </summary>
		Sightless,
		/// <summary>
		/// 其它的 5
		/// </summary>
		Other,
		/// <summary>
		/// 条件控制 6
		/// </summary>
		Condition,
		/// <summary>
		/// 等待签收（流程生成） 7
		/// </summary>
		Begin,
		/// <summary>
		/// 已经完成（流程生成） 8
		/// </summary>
		End


	}

}