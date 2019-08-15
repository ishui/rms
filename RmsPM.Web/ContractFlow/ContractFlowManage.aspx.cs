using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;

namespace RmsPM.Web.ContractFlow
{
	/// *******************************************************************************************
	/// <summary>
	/// ContractFlowManage 的摘要说明。合同申请
	/// </summary>
	/// *******************************************************************************************
	public partial class ContractFlowManage : PageBase
	{
		/// <summary>
		/// 工具栏
		/// </summary>
		/// <summary>
		/// 合同基本信息
		/// </summary>
		/// <summary>
		/// 合同修改
		/// </summary>
		/// <summary>
		/// 费用项
		/// </summary>
		/// <summary>
		/// 部门经理审核
		/// </summary>
		/// <summary>
		/// 设计部意见
		/// </summary>
		/// <summary>
		/// 物资部意见
		/// </summary>
		/// <summary>
		/// 工程部意见
		/// </summary>
		/// <summary>
		/// 配套部意见
		/// </summary>
		/// <summary>
		/// 预决算部意见
		/// </summary>
		/// <summary>
		/// 法务部意见
		/// </summary>
		/// <summary>
		/// 部门经理总结意见
		/// </summary>
		/// <summary>
		/// 部门（项目）负责人意见
		/// </summary>
		/// <summary>
		/// 董事长总经理意见
		/// </summary>
		/// <summary>
		/// 办公室意见
		/// </summary>
		/// <summary>
		/// 流程状态查询
		/// </summary>


		/// ****************************************************************************
		/// <summary>
		/// 页面加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
				InitPage();
		}
		/// ****************************************************************************
		/// <summary>
		/// 初始化
		/// </summary>
		/// ****************************************************************************
		private void InitPage()
		{
			string actCode = Request["ActCode"]+"";

			/********************************* 工具栏设置 **********************************/
			if(Request["frameType"] != null)//判断是否为流程监控状态
			{
				if(Request["frameType"].ToString() == "List")
				{
					WorkFlowToolbar1.Scout = true;
				}
			}
			
			if(Request["ApplicationCode"] != null)
				WorkFlowToolbar1.ApplicationCode = Request["ApplicationCode"].ToString();
			
			WorkFlowToolbar1.ActCode = actCode;//工具栏设置
			WorkFlowToolbar1.FlowName = "合同申请审核";
			WorkFlowToolbar1.SystemUserCode = this.user.UserCode;
			WorkFlowToolbar1.SourceUrl = "../WorkFlowControl/";
			WorkFlowToolbar1.ToolbarDataBind();
			/******************************************************************************/

			/********************************* 合同信息设置 ********************************/
			this.ContractInfoControl1.ContractCode = this.WorkFlowToolbar1.ApplicationCode;
			//this.ContractInfoControl1.ProjectCode = this.project.ProjectCode;
			this.ContractInfoControl1.UserCode = this.user.UserCode;
			this.ContractInfoControl1.State = this.WorkFlowToolbar1.GetModuleState("Base");
			this.ContractInfoControl1.InitControl();
			/******************************************************************************/

			/********************************* 合同修改设置 ********************************/
			this.ContractModifyButtonControl1.ContractCode = this.WorkFlowToolbar1.ApplicationCode;
			this.ContractModifyButtonControl1.ProjectCode = this.ContractInfoControl1.ProjectCode;
			this.ContractModifyButtonControl1.State = this.WorkFlowToolbar1.GetModuleState("Modify");
			this.ContractModifyButtonControl1.ActCode = this.WorkFlowToolbar1.ActCode;
			this.ContractModifyButtonControl1.InitControl();
			/******************************************************************************/

			/********************************* 合同费用项设置 ********************************/
			this.ContractAuditingControl1.ContractCode = this.WorkFlowToolbar1.ApplicationCode;
			this.ContractAuditingControl1.ProjectCode = this.ContractInfoControl1.ProjectCode;
			this.ContractAuditingControl1.State = this.WorkFlowToolbar1.GetModuleState("Money");
			this.ContractAuditingControl1.InitControl();
			/*******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion1.OpinionName = "部门经理意见";
			this.WorkFlowOpinion1.OpinionType = "ContractOpinion1";
			this.WorkFlowOpinion1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion1.State = this.WorkFlowToolbar1.GetModuleState("UM1");
			this.WorkFlowOpinion1.IsTextBox = false;
			this.WorkFlowOpinion1.DISPLAY = this.WorkFlowToolbar1.GetModuleState("UMD1");
			this.WorkFlowOpinion1.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion2.OpinionName = "设计部意见";
			this.WorkFlowOpinion2.OpinionType = "ContractOpinion2";
			this.WorkFlowOpinion2.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			if(this.WorkFlowToolbar1.GetModuleState("DUM1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion2.State = this.WorkFlowToolbar1.GetActorModuleState("DUM1",this.user.UserCode);
			else
				this.WorkFlowOpinion2.State = this.WorkFlowToolbar1.GetModuleState("DUM1");
			this.WorkFlowOpinion2.IsTextBox = false;
			if(this.WorkFlowToolbar1.GetModuleState("DUMD1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion2.DISPLAY = this.WorkFlowToolbar1.GetActorModuleState("DUMD1",this.user.UserCode);
			else
				this.WorkFlowOpinion2.DISPLAY = this.WorkFlowToolbar1.GetModuleState("DUMD1");
			this.WorkFlowOpinion2.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion3.OpinionName = "物资部意见";
			this.WorkFlowOpinion3.OpinionType = "ContractOpinion3";
			this.WorkFlowOpinion3.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			if(this.WorkFlowToolbar1.GetModuleState("MUM1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion3.State = this.WorkFlowToolbar1.GetActorModuleState("MUM1",this.user.UserCode);
			else
				this.WorkFlowOpinion3.State = this.WorkFlowToolbar1.GetModuleState("MUM1");
			this.WorkFlowOpinion3.IsTextBox = false;
			if(this.WorkFlowToolbar1.GetModuleState("MUMD1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion3.DISPLAY = this.WorkFlowToolbar1.GetActorModuleState("MUMD1",this.user.UserCode);
			else
				this.WorkFlowOpinion3.DISPLAY = this.WorkFlowToolbar1.GetModuleState("MUMD1");
			this.WorkFlowOpinion3.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion4.OpinionName = "工程部意见";
			this.WorkFlowOpinion4.OpinionType = "ContractOpinion4";
			this.WorkFlowOpinion4.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			if(this.WorkFlowToolbar1.GetModuleState("SUM1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion4.State = this.WorkFlowToolbar1.GetActorModuleState("SUM1",this.user.UserCode);
			else
				this.WorkFlowOpinion4.State = this.WorkFlowToolbar1.GetModuleState("SUM1");
			this.WorkFlowOpinion4.IsTextBox = false;
			if(this.WorkFlowToolbar1.GetModuleState("SUMD1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion4.DISPLAY = this.WorkFlowToolbar1.GetActorModuleState("SUMD1",this.user.UserCode);
			else
				this.WorkFlowOpinion4.DISPLAY = this.WorkFlowToolbar1.GetModuleState("SUMD1");
			this.WorkFlowOpinion4.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion5.OpinionName = "配套部意见";
			this.WorkFlowOpinion5.OpinionType = "ContractOpinion5";
			this.WorkFlowOpinion5.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			if(this.WorkFlowToolbar1.GetModuleState("WUM1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion5.State = this.WorkFlowToolbar1.GetActorModuleState("WUM1",this.user.UserCode);
			else
				this.WorkFlowOpinion5.State = this.WorkFlowToolbar1.GetModuleState("WUM1");
			this.WorkFlowOpinion5.IsTextBox = false;
			if(this.WorkFlowToolbar1.GetModuleState("WUMD1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion5.DISPLAY = this.WorkFlowToolbar1.GetActorModuleState("WUMD1",this.user.UserCode);
			else
				this.WorkFlowOpinion5.DISPLAY = this.WorkFlowToolbar1.GetModuleState("WUMD1");
			this.WorkFlowOpinion5.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion6.OpinionName = "预决算部意见";
			this.WorkFlowOpinion6.OpinionType = "ContractOpinion6";
			this.WorkFlowOpinion6.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			if(this.WorkFlowToolbar1.GetModuleState("PUM1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion6.State = this.WorkFlowToolbar1.GetActorModuleState("PUM1",this.user.UserCode);
			else
				this.WorkFlowOpinion6.State = this.WorkFlowToolbar1.GetModuleState("PUM1");
			this.WorkFlowOpinion6.IsTextBox = false;
			if(this.WorkFlowToolbar1.GetModuleState("PUMD1") == ModuleState.Unbeknown)
				this.WorkFlowOpinion6.DISPLAY = this.WorkFlowToolbar1.GetActorModuleState("PUMD1",this.user.UserCode);
			else
				this.WorkFlowOpinion6.DISPLAY = this.WorkFlowToolbar1.GetModuleState("PUMD1");
			this.WorkFlowOpinion6.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion7.OpinionName = "法务部意见";
			this.WorkFlowOpinion7.OpinionType = "ContractOpinion7";
			this.WorkFlowOpinion7.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion7.State = this.WorkFlowToolbar1.GetModuleState("CUM1");
			this.WorkFlowOpinion7.IsTextBox = false;
			this.WorkFlowOpinion7.DISPLAY = this.WorkFlowToolbar1.GetModuleState("CUMD1");
			this.WorkFlowOpinion7.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion8.OpinionName = "项目经理审核";
			this.WorkFlowOpinion8.OpinionType = "ContractOpinion8";
			this.WorkFlowOpinion8.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion8.State = this.WorkFlowToolbar1.GetModuleState("UM2");
			this.WorkFlowOpinion8.IsTextBox = false;
			this.WorkFlowOpinion8.DISPLAY = this.WorkFlowToolbar1.GetModuleState("UMD2");
			this.WorkFlowOpinion8.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion9.OpinionName = "项目总监审核";
			this.WorkFlowOpinion9.OpinionType = "ContractOpinion9";
			this.WorkFlowOpinion9.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion9.State = this.WorkFlowToolbar1.GetModuleState("PMUM1");
			this.WorkFlowOpinion9.IsTextBox = false;
			this.WorkFlowOpinion9.DISPLAY = this.WorkFlowToolbar1.GetModuleState("PMUMD1");
			this.WorkFlowOpinion9.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion10.OpinionName = "董事长总经理意见";
			this.WorkFlowOpinion10.OpinionType = "ContractOpinion10";
			this.WorkFlowOpinion10.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion10.State = this.WorkFlowToolbar1.GetModuleState("BUM1");
			this.WorkFlowOpinion10.IsTextBox = false;
			this.WorkFlowOpinion10.DISPLAY = this.WorkFlowToolbar1.GetModuleState("BUMD1");
			this.WorkFlowOpinion10.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion11.OpinionName = "办公室意见";
			this.WorkFlowOpinion11.OpinionType = "ContractOpinion11";
			this.WorkFlowOpinion11.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion11.State = this.WorkFlowToolbar1.GetModuleState("OUM1");
			this.WorkFlowOpinion11.IsTextBox = false;
			this.WorkFlowOpinion11.DISPLAY = this.WorkFlowToolbar1.GetModuleState("OUMD1");
			this.WorkFlowOpinion11.InitControl();
			/******************************************************************************/

			string action = Request["action"]+"";//流程状态查看
			if(action == "View")
			{
				this.WorkFlowCaseState1.ActCode = actCode;
			}
			else
			{
				this.WorkFlowCaseState1.ActCode = "";
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// 工具栏事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void WorkFlowToolbar1_ToolbarCommand(object sender , System.EventArgs e)
		{
			/****************************************************************************/
			using(StandardEntityDAO dao=new StandardEntityDAO("Standard_WorkFlowProcedure"))
			{
				dao.BeginTrans();
				try
				{
					/***********************************************************/
					//签收
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.SignIn)
					{
						WorkFlowToolbar1.SignIn(dao);
						InitPage();

					}
					//发送
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Send)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.Send();

					}
					//保存
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Save)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.Save();
						WorkFlowPropertySave();

						Response.Write(Rms.Web.JavaScript.ScriptStart);
						Response.Write(Rms.Web.JavaScript.OpenerReload(false));
						//Response.Write(Rms.Web.JavaScript.WinClose(false));
						Response.Write(Rms.Web.JavaScript.ScriptEnd);
					}
					//完成
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.TaskFinish)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.TaskFinish();

					}
					//结束
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Finish)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.Finish();
						ContractControlBase.ContractAuditing(WorkFlowToolbar1.ApplicationCode);

					}
					//抄送
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.MakeCopy)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.MakeCopy();

					}
					/*******************************************************/
					dao.CommitTrans();
				}
				catch(Exception ex)
				{
					dao.RollBackTrans();
					throw ex;
				}
			}
			/*******************************************************************/
		}
		/// ****************************************************************************
		/// <summary>
		/// 保存流程属性数据
		/// </summary>
		/// ****************************************************************************
		private void WorkFlowPropertySave()
		{
			
		}
		/// ****************************************************************************
		/// <summary>
		/// 业务数据操作
		/// </summary>
		/// ****************************************************************************
		private void DataSubmit(StandardEntityDAO dao)
		{
			if(this.WorkFlowOpinion1.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion1.dao = dao;
				this.WorkFlowOpinion1.SubmitData();
			}
			if(this.WorkFlowOpinion2.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion2.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion2.dao = dao;
				this.WorkFlowOpinion2.SubmitData();
			}
			if(this.WorkFlowOpinion3.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion3.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion3.dao = dao;
				this.WorkFlowOpinion3.SubmitData();
			}
			if(this.WorkFlowOpinion4.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion4.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion4.dao = dao;
				this.WorkFlowOpinion4.SubmitData();
			}
			if(this.WorkFlowOpinion5.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion5.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion5.dao = dao;
				this.WorkFlowOpinion5.SubmitData();
			}
			if(this.WorkFlowOpinion6.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion6.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion6.dao = dao;
				this.WorkFlowOpinion6.SubmitData();
			}
			if(this.WorkFlowOpinion7.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion7.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion7.dao = dao;
				this.WorkFlowOpinion7.SubmitData();
			}
			if(this.WorkFlowOpinion8.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion8.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion8.dao = dao;
				this.WorkFlowOpinion8.SubmitData();
			}
			if(this.WorkFlowOpinion9.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion9.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion9.dao = dao;
				this.WorkFlowOpinion9.SubmitData();
			}
			if(this.WorkFlowOpinion10.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion10.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion10.dao = dao;
				this.WorkFlowOpinion10.SubmitData();
			}
			if(this.WorkFlowOpinion11.State == ModuleState.Operable)
			{
				this.WorkFlowOpinion11.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.WorkFlowOpinion11.dao = dao;
				this.WorkFlowOpinion11.SubmitData();
			}
		}




		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
		//this.WorkFlowToolbar1.ToolbarCommand += new System.EventHandler(this.WorkFlowToolbar1_ToolbarCommand);
	}
}
