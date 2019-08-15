using System;
using System.Data;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// YX_WorkFlowPageBase 的摘要说明。
	/// </summary>
	public class YX_WorkFlowPageBase : PageBase
	{
		protected RmsPM.Web.WorkFlowControl.WorkFlowToolbar wftToolbar;
		protected RmsPM.Web.WorkFlowControl.WorkFlowCaseState wfcCaseState;
		protected RmsPM.Web.WorkFlowOperation.CheckControl ucCheckControl;

		protected RmsPM.Web.WorkFlowOperation.WorkFlowBase ucOperationControl;

		#region --- 属性集合 ---
		/// <summary>
		/// 数据对象名
		/// </summary>
		public string EntityName
		{
			get
			{
				if(this.ViewState["_EntityName"] != null)
					return this.ViewState["_EntityName"].ToString();
				return "";
			}
			set
			{
				this.ViewState["_EntityName"] = value;
			}
		}

		/// <summary>
		/// 流程名
		/// </summary>
		public string WorkFlowName
		{
			get
			{
				if(this.ViewState["_WorkFlowName"] != null)
					return this.ViewState["_WorkFlowName"].ToString();
				return "";
			}
			set
			{
				this.ViewState["_WorkFlowName"] = value;
			}
		}

		/// <summary>
		/// 流程意见控件数
		/// </summary>
		public int OpinionCount
		{
			get
			{
				if(this.ViewState["_OpinionCount"] != null)
					return (int)this.ViewState["_OpinionCount"];
				return 0;
			}
			set
			{
				ViewState["_OpinionCount"] = value;
			}
		}
		#endregion

		#region 工具栏事件
		/// ****************************************************************************
		/// <summary>
		/// 工具栏事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		virtual protected void WorkFlowToolbar_ToolbarCommand(object sender , System.EventArgs e)
		{
			using(StandardEntityDAO dao=new StandardEntityDAO(this.EntityName))
			{
				dao.BeginTrans();
				try
				{
			
					//签收
					if(wftToolbar.CommandType == ToolbarCommandType.SignIn)
					{
						ToolBarSignIn(dao);
					}
					//发送
					if(wftToolbar.CommandType == ToolbarCommandType.Send)
					{
						ToolBarSend(dao);
					}
					//退回
					if(wftToolbar.CommandType == ToolbarCommandType.Back)
					{
						ToolBarBack(dao);
					}
					//送经办人
					if(wftToolbar.CommandType == ToolbarCommandType.BackTop)
					{
						ToolBarBackTop(dao);
					}
					//收回
					if(wftToolbar.CommandType == ToolbarCommandType.Return)
					{
						ToolBarReturn(dao);
					}
					//保存意见
					if(wftToolbar.CommandType == ToolbarCommandType.Opinion)
					{
						ToolBarSaveOpinion(dao);
					}
					//保存
					if(wftToolbar.CommandType == ToolbarCommandType.Save)
					{
						ToolBarSave(dao);
					}
					//完成
					if(wftToolbar.CommandType == ToolbarCommandType.TaskFinish)
					{
						ToolBarTaskFinish(dao);

					}
					//结束
					if(wftToolbar.CommandType == ToolbarCommandType.Finish)
					{
						ToolBarFinish(dao);

					}
					//抄送
					if(wftToolbar.CommandType == ToolbarCommandType.MakeCopy)
					{
						ToolBarMakeCopy(dao);
					}
                    //删除
                    if(wftToolbar.CommandType == ToolbarCommandType.Delete)
                    {
                        ToolBarDelete(dao);
                    }
					dao.CommitTrans();
				}
				catch(Exception ex)
				{
					dao.RollBackTrans();
					Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
					throw ex;
				}
			}	
		}

		/// <summary>
		/// 签收
		/// </summary>
		virtual protected void ToolBarSignIn(StandardEntityDAO dao)
		{
			wftToolbar.SignIn(dao);
			WorkFlowPropertySave();
			this.InitPage();
		}

		/// <summary>
		/// 发送
		/// </summary>
		virtual protected void ToolBarSend(StandardEntityDAO dao)
		{
			if ( DataSubmit(dao,true) )
			{
				wftToolbar.Send();
				WorkFlowPropertySave();
			}
			else
			{
				dao.RollBackTrans();
				wftToolbar.IsNew = false;
			}
		}

		/// <summary>
		/// 退回
		/// </summary>
		virtual protected void ToolBarBack(StandardEntityDAO dao)
		{
			if ( DataSubmit(dao) )
			{
				wftToolbar.Back();
			}
			else
			{
				dao.RollBackTrans();
				wftToolbar.IsNew = false;
			}
		}

		/// <summary>
		/// 退返经办人
		/// </summary>
		virtual protected void ToolBarBackTop(StandardEntityDAO dao)
		{
			if ( DataSubmit(dao) )
			{
				wftToolbar.BackTop();
			}
			else
			{
				dao.RollBackTrans();
				wftToolbar.IsNew = false;
			}
		}

		/// <summary>
		/// 退返经办人
		/// </summary>
		virtual protected void ToolBarReturn(StandardEntityDAO dao)
		{
			wftToolbar.Return();
		}

		/// <summary>
		/// 保存意见
		/// </summary>
		virtual protected void ToolBarSaveOpinion(StandardEntityDAO dao)
		{
			this.wftToolbar.SaveOpinion();
			this.wfcCaseState.ControlDataBind();
		}

		/// <summary>
		/// 保存
		/// </summary>
		virtual protected void ToolBarSave(StandardEntityDAO dao)
		{
			if ( DataSubmit(dao) )
			{
				wftToolbar.Save();
				WorkFlowPropertySave();
				if(!this.wftToolbar.IsNew)
				{
					Response.Write(Rms.Web.JavaScript.ScriptStart);
					Response.Write(Rms.Web.JavaScript.OpenerReload(false));
					Response.Write(Rms.Web.JavaScript.WinClose(false));
					Response.Write(Rms.Web.JavaScript.ScriptEnd);
				}
			}
			else
			{
				dao.RollBackTrans();
				wftToolbar.IsNew = false;
			}
		}

		/// <summary>
		/// 完成
		/// </summary>
		virtual protected void ToolBarTaskFinish(StandardEntityDAO dao)
		{
			if ( DataSubmit(dao) )
			{
				wftToolbar.TaskFinish();
			}
			else
			{
				dao.RollBackTrans();
				wftToolbar.IsNew = false;
			}
		}

		/// <summary>
		/// 结束
		/// </summary>
		virtual protected void ToolBarFinish(StandardEntityDAO dao)
		{
			if ( DataSubmit(dao,true) )
			{
				wftToolbar.Finish();
			}
			else
			{
				dao.RollBackTrans();
				wftToolbar.IsNew = false;
			}
		}

		/// <summary>
		/// 抄送
		/// </summary>
		virtual protected void ToolBarMakeCopy(StandardEntityDAO dao)
		{
			if ( DataSubmit(dao) )
			{
				wftToolbar.MakeCopy();
			}
			else
			{
				dao.RollBackTrans();
				wftToolbar.IsNew = false;
			}
		}
        virtual protected void ToolBarDelete(StandardEntityDAO dao) 
        {
            wftToolbar.Delete();
        }
		#endregion

		#region 绑定工具栏事件(重写父类)
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			//InitializeComponent();
            SetBaseControl();
			base.OnInit(e);
			//InitEventHandler();
		}
        virtual protected void SetBaseControl()
        {
            wftToolbar = (WorkFlowToolbar)Page.FindControl("wftToolbar");
            wfcCaseState = (WorkFlowCaseState)Page.FindControl("wfcCaseState");
            ucCheckControl = (RmsPM.Web.WorkFlowOperation.CheckControl)Page.FindControl("ucCheckControl");
        }
		override protected void InitEventHandler()
		{
			this.wftToolbar.ToolbarCommand += new System.EventHandler(WorkFlowToolbar_ToolbarCommand);
		}
		#endregion

		#region 流程控件初始化

		virtual protected void InitPage()
		{
			WorkFlowInit();
			PageControlInit();
		}

		/// <summary>
		/// 流程工具栏设置
		/// </summary>
		virtual protected void WorkFlowInit()
		{
			string actCode = Request["ActCode"]+"";

			if(Request["frameType"] != null)//判断是否为流程监控状态
			{
				if(Request["frameType"].ToString() == "List")
				{
					wftToolbar.Scout = true;
				}
			}
			
			if(Request["ApplicationCode"] != null)
			{
				wftToolbar.ApplicationCode = Request["ApplicationCode"].ToString();
			}
			
			/**************************************************************************************/
			wftToolbar.ActCode = actCode;//工具栏设置
			wftToolbar.FlowName = this.WorkFlowName;
			wftToolbar.SystemUserCode = this.user.UserCode;
			wftToolbar.SourceUrl = "../WorkFlowControl/";
			wftToolbar.ToolbarDataBind();

			if(this.wftToolbar.GetModuleState("Delete") == ModuleState.Operable)
			{
				wftToolbar.BtnDeleteVisible = true;
			}
			else
			{
				wftToolbar.BtnDeleteVisible = false;
			}

			//流程状态查看
			this.wfcCaseState.ActCode = this.wftToolbar.ActCode;
			this.wfcCaseState.ApplicationCode = this.wftToolbar.ApplicationCode;
			this.wfcCaseState.FlowName = this.wftToolbar.FlowName;
			this.wfcCaseState.UserCode = this.user.UserCode;
			this.wfcCaseState.Scout = this.wftToolbar.Scout;
			this.wfcCaseState.ControlDataBind();
		}

		/// ****************************************************************************
		/// <summary>
		/// 保存流程属性数据
		/// </summary>
		/// ****************************************************************************
		virtual protected void WorkFlowPropertySave()
		{
			if ( wftToolbar.IsNew || ucOperationControl.State == ModuleState.Operable)
			{
				this.wftToolbar.SaveCasePropertyValue("申请人",this.user.UserCode);
				this.wftToolbar.SaveCasePropertyValue("主题",this.ucOperationControl.ApplicationTitle);
				this.wftToolbar.SaveCasePropertyValue("项目代码",this.ucOperationControl.ProjectCode);
				this.wftToolbar.SaveCasePropertyValue("项目部门",BLL.ProjectRule.GetUnitByProject(this.ucOperationControl.ProjectCode));
				this.wftToolbar.SaveCasePropertyValue("业务类别",this.ucOperationControl.ApplicationType);
			}

			if ( wftToolbar.IsNew || ucCheckControl.State == ModuleState.Operable)
			{
				this.wftToolbar.SaveCasePropertyValue("审核状态",this.ucCheckControl.Result);
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// 页面控件初始化
		/// </summary>
		/// ****************************************************************************
		virtual protected void PageControlInit()
		{
			string ud_sResult = this.wftToolbar.GetCasePropertyValue("审核状态");
			this.ucCheckControl.Result = ud_sResult == "" ? "Unknow":ud_sResult;
		}

		/// ****************************************************************************
		/// <summary>
		/// 流程意见控件初始化
		/// </summary>
		/// ****************************************************************************
		protected WorkFlowControl.ModuleState[] OpinionControlInit( string pm_sOpinionTitle,string pm_sOpinionType,string pm_sModuleName,
			WorkFlowFormOpinion pm_wfoOpinion,string pm_sConfirmOpinionList,out string po_sConfirmOpinionList)
		{
			return OpinionControlInit(pm_sOpinionTitle,pm_sOpinionType,pm_sModuleName,pm_wfoOpinion,"",pm_sConfirmOpinionList,out po_sConfirmOpinionList,"");
		}

		protected WorkFlowControl.ModuleState[] OpinionControlInit( string pm_sOpinionType,string pm_sModuleName,
			WorkFlowFormOpinion pm_wfoOpinion,string pm_sConfirmOpinionList,out string po_sConfirmOpinionList)
		{
			return OpinionControlInit("",pm_sOpinionType,pm_sModuleName,pm_wfoOpinion,"",pm_sConfirmOpinionList,out po_sConfirmOpinionList,"");
		}

		protected WorkFlowControl.ModuleState[] OpinionControlInit( string pm_sOpinionTitle,string pm_sOpinionType,string pm_sModuleName,
			WorkFlowFormOpinion pm_wfoOpinion,string pm_sUserCode,string pm_sConfirmOpinionList,out string po_sConfirmOpinionList)
		{
			return OpinionControlInit(pm_sOpinionTitle,pm_sOpinionType,pm_sModuleName,pm_wfoOpinion,pm_sUserCode,pm_sConfirmOpinionList,out po_sConfirmOpinionList,"");
		}

		protected WorkFlowControl.ModuleState[] OpinionControlInit( string pm_sOpinionType,string pm_sModuleName,
			WorkFlowFormOpinion pm_wfoOpinion,string pm_sUserCode,string pm_sConfirmOpinionList,out string po_sConfirmOpinionList)
		{
			return OpinionControlInit("",pm_sOpinionType,pm_sModuleName,pm_wfoOpinion,pm_sUserCode,pm_sConfirmOpinionList,out po_sConfirmOpinionList,"");
		}

		protected WorkFlowControl.ModuleState[] OpinionControlInit( string pm_sOpinionTitle,string pm_sOpinionType,string pm_sModuleName,
			WorkFlowFormOpinion pm_wfoOpinion,string pm_sConfirmOpinionList,out string po_sConfirmOpinionList,string pm_sInputType)
		{
			return OpinionControlInit(pm_sOpinionTitle,pm_sOpinionType,pm_sModuleName,pm_wfoOpinion,"",pm_sConfirmOpinionList,out po_sConfirmOpinionList,pm_sInputType);
		}

		protected WorkFlowControl.ModuleState[] OpinionControlInit(string pm_sOpinionType,string pm_sModuleName,
			WorkFlowFormOpinion pm_wfoOpinion,string pm_sConfirmOpinionList,out string po_sConfirmOpinionList,string pm_sInputType)
		{
			return OpinionControlInit("",pm_sOpinionType,pm_sModuleName,pm_wfoOpinion,"",pm_sConfirmOpinionList,out po_sConfirmOpinionList,pm_sInputType);
		}

		protected WorkFlowControl.ModuleState[] OpinionControlInit( string pm_sOpinionType,string pm_sModuleName,
			WorkFlowFormOpinion pm_wfoOpinion,string pm_sUserCode,string pm_sConfirmOpinionList,out string po_sConfirmOpinionList,string pm_sInputType)
		{
			return OpinionControlInit( "",pm_sOpinionType,pm_sModuleName,pm_wfoOpinion,pm_sUserCode,pm_sConfirmOpinionList,out po_sConfirmOpinionList,pm_sInputType);
		}

		/// <param name="po_sConfirmOpinionList">控件名1,控件名2,控件名3,</param>
		/// <param name="pm_sInputType">Text,TextArea,TextAreaEsay,TextNum</param>
		protected WorkFlowControl.ModuleState[] OpinionControlInit( string pm_sOpinionTitle,string pm_sOpinionType,string pm_sModuleName,
			WorkFlowFormOpinion pm_wfoOpinion,string pm_sUserCode,string pm_sConfirmOpinionList,out string po_sConfirmOpinionList,string pm_sInputType)
		{
			int ud_iOpinionStateCount = 2;
			WorkFlowControl.ModuleState[] ud_wfmaWorkFlowModuleState = new ModuleState[ud_iOpinionStateCount];

			for ( int i=0;i<ud_iOpinionStateCount;i++ )
			{
				ud_wfmaWorkFlowModuleState[i] = this.wftToolbar.GetModuleState(pm_sModuleName,i);
			}

			if ( ud_wfmaWorkFlowModuleState[1] == ModuleState.Operable )
			{
				pm_sConfirmOpinionList += pm_wfoOpinion.UniqueID + ",";
			}

			po_sConfirmOpinionList = pm_sConfirmOpinionList;

			switch ( pm_sInputType.ToLower() )
			{
				case "text":
					pm_wfoOpinion.ControlType = "Text";
					break;
				case "textarea":
					pm_wfoOpinion.ControlType = "TextArea";
					break;
				case "textareaesay":
					pm_wfoOpinion.ControlType = "TextAreaEsay";
					break;
				case "textnum":
					pm_wfoOpinion.ControlType = "TextNum";
					break;
				default:
					break;
			}
			pm_wfoOpinion.Title = pm_sOpinionTitle;
			pm_wfoOpinion.OpinionType = pm_sOpinionType;
			pm_wfoOpinion.ApplicationCode = this.wftToolbar.ApplicationCode;
            pm_wfoOpinion.CaseCode = this.wftToolbar.CaseCode;
			pm_wfoOpinion.State = ud_wfmaWorkFlowModuleState[0];
			pm_wfoOpinion.StateConfirm = ud_wfmaWorkFlowModuleState[1];

			if ( pm_sUserCode.Trim() != "" )
			{
				pm_wfoOpinion.OpinionUserCode = pm_sUserCode;
			}

			pm_wfoOpinion.InitControl();

			return ud_wfmaWorkFlowModuleState;
		}

		protected DataTable GetSendItemsByCasePropertyValue( string pm_sCasePropertyValue)
		{
			DataTable ud_dtSendItems = new DataTable();

			ud_dtSendItems.Columns.Add("UserCode");
			ud_dtSendItems.Columns.Add("RoleCode");
			ud_dtSendItems.Columns.Add("UserName");
			ud_dtSendItems.Columns.Add("RoleName");

			string ud_sSendItems = wftToolbar.GetCasePropertyValue(pm_sCasePropertyValue) == null ? "":wftToolbar.GetCasePropertyValue(pm_sCasePropertyValue);

			foreach(string tmpStr in ud_sSendItems.Split(';'))
			{
				string[] ud_saTmp = tmpStr.Split(',');
				if ( ud_saTmp.Length == 4  )
				{
					DataRow ud_drNew = ud_dtSendItems.NewRow();
					ud_drNew["UserCode"] = ud_saTmp[0];
					ud_drNew["RoleCode"] = ud_saTmp[1];
					ud_drNew["UserName"] = ud_saTmp[2];
					ud_drNew["RoleName"] = ud_saTmp[3];
					ud_dtSendItems.Rows.Add(ud_drNew);
				}
			}

			return ud_dtSendItems;

		}

		#endregion

		#region  业务数据操作
		/// ****************************************************************************
		/// <summary>
		/// 业务数据操作
		/// </summary>
		/// ****************************************************************************
		virtual protected Boolean DataSubmit(StandardEntityDAO dao)
		{
			return DataSubmit(dao,false);
		}

		virtual protected Boolean DataSubmit(StandardEntityDAO dao, Boolean AuditFlag)
		{
			Boolean ReturnValue;

			ReturnValue = OperationDataSubmit(dao);
			
			if ( ReturnValue )
			{
				ReturnValue = OpinionDataSubmit(dao);
			}

			if ( ReturnValue && AuditFlag )
			{
				ReturnValue = Audit(dao);
			}

			return ReturnValue;
		}
		/// <summary>
		/// 是否保存业务中数据
		/// </summary>
		/// <param name="dao"></param>
		/// <param name="AuditFlag"></param>
		/// <returns></returns>
		virtual protected Boolean DataSubmit(StandardEntityDAO dao, Boolean AuditFlag,Boolean OperationFlag)
		{
			Boolean ReturnValue;

			if(OperationFlag)
			{
				ReturnValue = OperationDataSubmit(dao);
			}
			else
			{
				ReturnValue=true;
			}			
			if ( ReturnValue )
			{
				ReturnValue = OpinionDataSubmit(dao);
			}

			if ( ReturnValue && AuditFlag )
			{
				ReturnValue = Audit(dao);
			}

			return ReturnValue;
		}

		/// ****************************************************************************
		/// <summary>
		/// 业务控件数据保存
		/// </summary>
		/// ****************************************************************************
		virtual protected Boolean OperationDataSubmit(StandardEntityDAO dao)
		{

			try
			{
				//业务控件数据保存
				//if(this.ucOperationControl.State == ModuleState.Operable)
				//{
				//	this.ucOperationControl.dao = dao;
				///	string ErrMsg = this.ucOperationControl.SubmitData();

				//	if ( ErrMsg != "" )
				//	{
				//		Response.Write(Rms.Web.JavaScript.Alert(true,ErrMsg));
				//		return false;
				//	}				
				//	this.wftToolbar.ApplicationCode = this.ucOperationControl.ApplicationCode;
			//	}
				return true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// 流程意见控件数据保存
		/// </summary>
		/// ****************************************************************************
		virtual protected Boolean OpinionDataSubmit(StandardEntityDAO dao)
		{
			try
			{
				string ud_sOpinionControlName = "wfoOpinion";

				for (int i=1;i<=this.OpinionCount;i++ )
				{
					RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoContraol;
					ud_wfoContraol = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)this.Page.FindControl(ud_sOpinionControlName + i.ToString());

					if(ud_wfoContraol.State == ModuleState.Operable)
					{
						ud_wfoContraol.ApplicationCode = wftToolbar.ApplicationCode;
                        ud_wfoContraol.CaseCode = wftToolbar.CaseCode;
						ud_wfoContraol.dao = dao;
						ud_wfoContraol.SubmitData();
					}
				}
				return true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// ****************************************************************************
		/// <summary>
		/// 业务审核
		/// </summary>
		/// ****************************************************************************
		virtual protected Boolean Audit(StandardEntityDAO dao)
		{
			return true;
		}
		/// ****************************************************************************
		/// <summary>
		/// 获取页面上所有有效确认框的值
		/// </summary>
		/// <param name="pm_iConfirmType">确认框取值原则: 0 多数通过,1 一票否决, 2 一票赞成</param>
		/// ****************************************************************************
		protected string GetOpinionConfirm(string pm_sConfirmOpinionList,int pm_iConfirmType)
		{
			string ud_sOpinionConfirm = "";

			int ud_iApproveCount = 0;
			int ud_iRejectCount = 0;

			if ( pm_sConfirmOpinionList.Trim() != "" )
			{
				string[] ud_saConfirmOpinion = pm_sConfirmOpinionList.Split(',');

				for(int i=0;i<ud_saConfirmOpinion.Length;i++ )
				{
					if ( ud_saConfirmOpinion[i].Trim() != "" )
					{
						WorkFlowControl.WorkFlowFormOpinion ud_wfoOpinion = (WorkFlowControl.WorkFlowFormOpinion)this.FindControl(ud_saConfirmOpinion[i].Trim());

						switch ( ud_wfoOpinion.OpinionConfirm )
						{
							case "Approve":
								if ( pm_iConfirmType == 2 )
								{
									return "Approve";
								}
								else
								{
									ud_iApproveCount++;
								}
								break;
							case "Reject":
								if ( pm_iConfirmType == 1 )
								{
									return "Reject";
								}
								else
								{
									ud_iRejectCount++;
								}
								break;
							default:
								break;
						}
					}
				}

				switch (pm_iConfirmType)
				{
					case 0:
						if ( ud_iApproveCount > 0 || ud_iRejectCount > 0)
						{
							if ( ud_iApproveCount >= ud_iRejectCount  )
							{
								ud_sOpinionConfirm = "Approve";
							}
							else
							{
								ud_sOpinionConfirm = "Reject";
							}
						}
						ud_sOpinionConfirm = "Unknow";
						break;
					case 1:
						if ( ud_iApproveCount > 0 )
						{
							ud_sOpinionConfirm = "Approve";
						}
						else
						{
							ud_sOpinionConfirm = "Unknow";
						}
						break;
					case 2:
						if ( ud_iRejectCount > 0 )
						{
							ud_sOpinionConfirm = "Reject";
						}
						else
						{
							ud_sOpinionConfirm = "Unknow";
						}
						break;
				}
			}

			return ud_sOpinionConfirm;
		}

		protected string GetOpinionConfirm(string pm_sConfirmOpinionList)
		{
			return GetOpinionConfirm(pm_sConfirmOpinionList,1);
		}
		#endregion
	}
}
