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

using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingMessageManage 的摘要说明。
	/// </summary>
	public partial class BiddingMessageManage : BiddingWorkFlowBase
	{
		/// <summary>
		/// 工具栏
		/// </summary>
		//protected RmsPM.Web.WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1;
		/// <summary>
		/// 流程步骤察看
		/// </summary>
		protected System.Web.UI.WebControls.DataGrid dgMeetSign;

		/// ****************************************************************************
		/// <summary>
		/// 页面加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
            {
                // //--//之间为新长宁的特殊需求
                string company = this.up_sPMName.ToLower();
                switch (company)
                {
                    case "disaipm":
                    case "zhudingpm":
                        string Binddingcode = "";
                        string ApplicationCode = "";
                        if (Request["ApplicationCode"] != null)
                            ApplicationCode = Request["ApplicationCode"].ToString();
                        if (Request["BiddingCode"] != null)
                            Binddingcode = Request["BiddingCode"].ToString();
                        if (ApplicationCode == "")
                            this.Response.Redirect("XCN_biddingmessagemanage.aspx?BiddingCode=" + Binddingcode);
                        else
                            this.Response.Redirect("XCN_biddingmessagemanage.aspx?BiddingCode=" + Binddingcode + "ApplicationCode=" + ApplicationCode);
                        break;
                }
                //
                InitPage();
            }
			BiddingMessageModify1.LoadAttach();
		}
		/// <summary>
		/// 项目代码
		/// </summary>
		public string BiddingCode
		{
			get
			{
				return Request["BiddingCode"]+"";
			}
		}

		
		/// ****************************************************************************
		/// /// <summary>
		/// 初始化
		/// </summary>
		/// ****************************************************************************		
		
		override protected void InitPage()
		{
			string actCode = Request["ActCode"]+"";
            string caseCode = Request["CaseCode"] + "";
			if(Request["frameType"] != null)//判断是否为流程监控状态
			{
				if(Request["frameType"].ToString() == "List")
				{
					/*if(!user.HasOperationRight("130105"))
					{
						Response.Redirect( "../RejectAccess.aspx" );
						Response.End();
					}*/
					WorkFlowToolbar1.Scout = true;

				}
			}
			
			if(Request["ApplicationCode"] != null)
				WorkFlowToolbar1.ApplicationCode = Request["ApplicationCode"].ToString();
			
			/**************************************************************************************/
			WorkFlowToolbar1.ActCode = actCode;//工具栏设置
            WorkFlowToolbar1.CaseCode = caseCode;
			WorkFlowToolbar1.FlowName = "中标通知书评审";
			WorkFlowToolbar1.SystemUserCode = this.user.UserCode;
			WorkFlowToolbar1.SourceUrl = "../WorkFlowControl/";
            WorkFlowToolbar1.ProjectCode = Request["ProjectCode"] + "";
			WorkFlowToolbar1.ToolbarDataBind();
			bool tempDeleteFlag = false;
			if(this.WorkFlowToolbar1.GetModuleState("DeleteFlag") == ModuleState.Operable)
				tempDeleteFlag = true;
			WorkFlowToolbar1.BtnDeleteVisible = tempDeleteFlag;
			/**************************************************************************************/

			this.BiddingMessageModify1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.BiddingMessageModify1.State = this.WorkFlowToolbar1.GetModuleState("BaseWrite");
            this.BiddingMessageModify1.MoneyState = this.WorkFlowToolbar1.GetModuleState("ShowMoney");
			this.BiddingMessageModify1.BiddingCode = Request["BiddingCode"]+"";
			this.BiddingMessageModify1.UserCode = user.UserCode;
			this.BiddingMessageModify1.InitControl();

			if(this.BiddingMessageModify1.State == ModuleState.Operable)
			{
				this.WorkFlowToolbar1.ScriptCheck = "javascript:if(BiddingMessageSubmitCheck()) ";
			}
			///是否显示Money;
			Control_BiddingEmitMoney1.State = this.WorkFlowToolbar1.GetModuleState("ShowMoney");
			Control_BiddingEmitMoney1.BiddingCode = this.BiddingMessageModify1.BiddingCode;
			//Control_BiddingEmitMoney1.InitPage();			
			/**************************************************************************************/
			/*************************************************************************************/
			BiddingMessageModify1.SetAttachList=this.WorkFlowToolbar1.GetModuleState("附件1");
			
			
			

			/**************************************************************************************
			this.LeavePass1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;//同意按钮组
			this.LeavePass1.State = this.WorkFlowToolbar1.GetModuleState("PassBtn");
			this.LeavePass1.InitControl();
			/**************************************************************************************/

			/**************************************************************************************/
			//流程状态查看
			this.WorkFlowCaseState1.ActCode = this.WorkFlowToolbar1.ActCode;
            this.WorkFlowCaseState1.CaseCode = this.WorkFlowToolbar1.CaseCode;
            this.WorkFlowCaseState1.Toobar = this.WorkFlowToolbar1;
			this.WorkFlowCaseState1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowCaseState1.FlowName = this.WorkFlowToolbar1.FlowName;
			this.WorkFlowCaseState1.UserCode = this.user.UserCode;
			this.WorkFlowCaseState1.Scout = this.WorkFlowToolbar1.Scout;
			this.WorkFlowCaseState1.ControlDataBind();
			/**************************************************************************************/
			/**************************************************************************************/
			base.InitPage();

            string ud_sConfirmOpinionList = "";

            //控制意见是否可以操作
            string ud_sOpinionControlName = "WorkFlowOpinion";
            for (int i = 1; i <= 10; i++)
            {
                RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoControl;
                ud_wfoControl = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)this.Page.FindControl(ud_sOpinionControlName + i.ToString());
                ud_wfoControl.IsRdoCheck = false;
                ud_wfoControl.IsUseTemplateOpinion = true;
                ud_wfoControl.IsUseTextArea = true;
            }

            OpinionControlInit("推荐意见", "SM_BM_建筑设计部", "Opinion1", base.WorkFlowOpinion1, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BM_工程部", "Opinion2", base.WorkFlowOpinion2, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BM_合约部", "Opinion3", base.WorkFlowOpinion3, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BM_项目总监", "Opinion4", base.WorkFlowOpinion4, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BM_总部总监1", "Opinion5", base.WorkFlowOpinion5, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BM_总部总监2", "Opinion6", base.WorkFlowOpinion6, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BM_董事长1", "Opinion7", base.WorkFlowOpinion7, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BM_董事长2", "Opinion8", base.WorkFlowOpinion8, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);
            OpinionControlInit("推荐意见", "SM_BM_董事长3", "Opinion9", base.WorkFlowOpinion9, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);
            OpinionControlInit("推荐意见", "SM_BM_董事长4", "Opinion10", base.WorkFlowOpinion10, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            ViewState["_ConfirmOpinionList"] = ud_sConfirmOpinionList;
			//会签部门表单初始化
			/**************************************************************************************/
			DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("会签部门");

			if ( ud_dtSendItems.Rows.Count > 0 )
			{
				rptMeetSign.DataSource = ud_dtSendItems;
				rptMeetSign.DataBind();
			}
			PageControlInit();
		}
		/// ****************************************************************************
		/// <summary>
		/// 保存流程属性数据
		/// </summary>
		/// ****************************************************************************
		override protected void WorkFlowPropertySave()
		{
			if(this.BiddingMessageModify1.State == ModuleState.Operable)
			{
				WorkFlowToolbar1.SaveCasePropertyValue("主题",this.BiddingMessageModify1.ContractName);
				WorkFlowToolbar1.SaveCasePropertyValue("申请人",user.UserCode);
				WorkFlowToolbar1.SaveCasePropertyValue("估计金额",this.BiddingMessageModify1.Money);
				WorkFlowToolbar1.SaveCasePropertyValue("业务类别",BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(this.BiddingMessageModify1.BiddingType));
				WorkFlowToolbar1.SaveCasePropertyValue("主要标段",this.BiddingMessageModify1.mostly);
				WorkFlowToolbar1.SaveCasePropertyValue("用户类别",user.GetOperationType());
				WorkFlowToolbar1.SaveCasePropertyValue("项目部门",BLL.ProjectRule.GetUnitByProject(this.BiddingMessageModify1.ProjectCode));
				WorkFlowToolbar1.SaveCasePropertyValue("项目代码",this.BiddingMessageModify1.ProjectCode);
                WorkFlowToolbar1.SaveCasePropertyValue("最后报价", this.BiddingMessageModify1.MaxMoney);
			}
			base.WorkFlowPropertySave();
		}
		/// ****************************************************************************
		/// <summary>
		/// 工具栏事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		override protected void WorkFlowToolbar1_ToolbarCommand(object sender , System.EventArgs e)
		{
			/****************************************************************************/
			using(StandardEntityDAO dao=new StandardEntityDAO("Leave"))
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
						if(WorkFlowToolbar1.GetModuleState("Opinion4") == ModuleState.Operable)
							WorkFlowToolbar1.SaveCasePropertyValue("用户类别",user.GetOperationType());

					}
					//发送
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Send)
					{
						//Response.Write(Rms.Web.JavaScript.WriteJS("ggggg"));
						try
						{
							DataSubmit(dao);
							//如果为发起,则将状态改为"中标单位审批中"
							if(WorkFlowToolbar1.GetModuleState("BaseWrite")==ModuleState.Operable)
							{
								BLL.BiddingSystem.Set_BiddingState("41",this.BiddingMessageModify1.BiddingCode);
							}
							if(WorkFlowToolbar1.GetModuleState("End")==ModuleState.Eyeable)
							{
								BLL.BiddingSystem.Set_BiddingState("42",this.BiddingMessageModify1.BiddingCode);
							}
						}
						catch(Exception ex)
						{
							Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
						}
						WorkFlowToolbar1.Send();
					}
					//退回
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Back)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.Back();
					}
					//收回
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Return)
					{
						WorkFlowToolbar1.Return();
					}
					//送经办人
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.BackTop)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.BackTop();
					}
					//保存意见
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Opinion)
					{
						WorkFlowToolbar1.SaveOpinion();
						this.WorkFlowCaseState1.ControlDataBind();
					}
					//保存
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Save)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.Save();
						WorkFlowPropertySave();
						if(!this.WorkFlowToolbar1.IsNew)
						{
							Response.Write(Rms.Web.JavaScript.ScriptStart);
							Response.Write(Rms.Web.JavaScript.OpenerReload(false));
							Response.Write(Rms.Web.JavaScript.WinClose(false));
							Response.Write(Rms.Web.JavaScript.ScriptEnd);
						}
					}
					//完成
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.TaskFinish)
					{
						DataSubmit(dao);
						if(WorkFlowToolbar1.GetModuleState("End")==ModuleState.Eyeable)
						{
							BLL.BiddingSystem.Set_BiddingState("42",this.BiddingMessageModify1.BiddingCode);
						}
						WorkFlowToolbar1.TaskFinish();

					}
					//结束
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Finish)
					{
						DataSubmit(dao);
						if(WorkFlowToolbar1.GetModuleState("End")==ModuleState.Eyeable)
						{
							BLL.BiddingSystem.Set_BiddingState("42",this.BiddingMessageModify1.BiddingCode);
						}
						WorkFlowToolbar1.Finish();

					}
					//抄送
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.MakeCopy)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.MakeCopy();

					}
                    //删除
                    if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Delete)
                    {
                        WorkFlowToolbar1.Delete();
                        BLL.BiddingSystem.Set_BiddingState("3", this.BiddingMessageModify1.BiddingCode,dao);

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
		/// 业务数据操作
		/// </summary>
		/// ****************************************************************************
		override protected Boolean DataSubmit(StandardEntityDAO dao)
		{
			this.WorkFlowCaseState1.SubmitData();
			
			if(this.BiddingMessageModify1.State == ModuleState.Operable)
			{
				this.BiddingMessageModify1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.BiddingMessageModify1.dao = dao;
				this.BiddingMessageModify1.SubmitData();
				WorkFlowToolbar1.ApplicationCode = this.BiddingMessageModify1.ApplicationCode;
			}
			SaveMeetMessage(dao,this.rptMeetSign);
			return base.DataSubmit(dao);

		}
		/// ****************************************************************************
		/// <summary>
		/// 会签控件数据绑定
		/// 世茂特殊要求(2个项目总监会签)
		/// </summary>
		/// ****************************************************************************
		/// 
		override protected void rptMeetSign_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			base.rptMeetSign_ItemDataBound( sender, e);
		}
		protected override void SaveMeetMessage(StandardEntityDAO dao, Repeater rptMeetSign)
		{
			base.SaveMeetMessage (dao, this.rptMeetSign);
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
		override protected void LoadEvent()
		{
			this.rptMeetSign.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptMeetSign_ItemDataBound);
		}
		#endregion		

	}
}


