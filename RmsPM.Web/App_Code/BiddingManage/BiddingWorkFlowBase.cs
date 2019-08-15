using System;
using System.Web;
using System.Web.UI;
using RmsPM.BLL;
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingWorkFlowBase 的摘要说明。
	/// </summary>
	public class BiddingWorkFlowBase : PageBase
	{
		protected RmsPM.Web.WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1;
		protected RmsPM.Web.WorkFlowOperation.CheckControl ucCheckControl;	
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion1;
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion2;
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion3;
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion4;
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion5;
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion6;
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion7;
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion8;
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion9;
		protected RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion WorkFlowOpinion10;
		protected int OpinionCount=10;


		public BiddingWorkFlowBase()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 页面数初化,针对招头标
		virtual protected void InitPage()
		{
			
		}
		#endregion


		#region 页面初始化,公共基类
		/// ****************************************************************************
		/// <summary>
		/// 页面控件初始化
		/// </summary>
		/// ****************************************************************************
		virtual protected void PageControlInit()
		{
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
				ud_wfmaWorkFlowModuleState[i] = this.WorkFlowToolbar1.GetModuleState(pm_sModuleName,i);
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
			pm_wfoOpinion.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
            pm_wfoOpinion.CaseCode = this.WorkFlowToolbar1.CaseCode;
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

			string ud_sSendItems = WorkFlowToolbar1.GetCasePropertyValue(pm_sCasePropertyValue) == null ? "":WorkFlowToolbar1.GetCasePropertyValue(pm_sCasePropertyValue);

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

		#region 保存数据
		
		#region 存流程数据
        
		/// ****************************************************************************
		/// <summary>
		/// 保存流程属性数据
		/// </summary>
		/// ****************************************************************************
		virtual protected void WorkFlowPropertySave()
		{
            if (this.WorkFlowToolbar1.IsNew)
            {
                string NumberString = RmsPM.BLL.SystemRule.GetProjectConfigValue(WorkFlowToolbar1.ProjectCode, "FlowNumber")
                    + RmsPM.BLL.WorkFlowRule.GetProcedureNumberByName(this.WorkFlowToolbar1.FlowName) + DateTime.Now.Year.ToString().Substring(2, 2);
                int FlowNumberLenth = (RmsPM.BLL.SystemRule.GetProjectConfigValue("FlowNumberLength") == "") ? 4 : int.Parse(RmsPM.BLL.SystemRule.GetProjectConfigValue("FlowNumberLength"));
                NumberString += RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode(NumberString).Substring(6 - FlowNumberLenth, FlowNumberLenth);
                this.WorkFlowToolbar1.SaveCasePropertyValue("流水号", NumberString);
            }

            if (this.WorkFlowToolbar1.GetModuleState("Hand") == ModuleState.Operable)
            {
                this.WorkFlowToolbar1.SaveCasePropertyValue("手送资料", this.WorkFlowToolbar1.HandMadeValue);
            }
			
		}
		virtual protected void SaveWorkFlowForMeet()
		{
			if ( WorkFlowToolbar1.GetModuleState("准备会签") == ModuleState.Operable)
			{
				WorkFlowToolbar1.SaveCasePropertyValue("会签部门",WorkFlowToolbar1.SendRoleItems);
				WorkFlowToolbar1.SaveCasePropertyValue("会签发起人",this.user.UserCode);
			}
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
			OpinionDataSubmit(dao);
			SaveWorkFlowForMeet();
			return true;
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
				string ud_sOpinionControlName = "WorkFlowOpinion";

				for (int i=1;i<=this.OpinionCount;i++ )
				{
					if(i==4)
					{
						continue;
					}
					RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoContraol;
					ud_wfoContraol = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)this.Page.FindControl(ud_sOpinionControlName + i.ToString());

					if(ud_wfoContraol.State == ModuleState.Operable)
					{
						ud_wfoContraol.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                        ud_wfoContraol.CaseCode = WorkFlowToolbar1.CaseCode;
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

		/// ****************************************************************************
		/// <summary>
		/// 流程意见控件数据保存
		/// 世茂特殊要求(2个项目总监会签)
		/// </summary>
		/// ****************************************************************************
		virtual protected void SaveMeetMessage(StandardEntityDAO dao,Repeater rptMeetSign)
		{

			try
			{
				foreach(RepeaterItem ud_rptItem in rptMeetSign.Items)
				{
						
					RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoControl;

					switch ( ud_rptItem.ItemType )
					{
						case ListItemType.Item:
							ud_wfoControl = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)ud_rptItem.FindControl("wfoItemOpinion");
							break;
						case ListItemType.AlternatingItem:
							ud_wfoControl = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)ud_rptItem.FindControl("wfoAlternatingItemOpinion");
							break;
						default:
							continue;
					}

					if(ud_wfoControl.State == ModuleState.Operable)
					{
						ud_wfoControl.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                        ud_wfoControl.CaseCode = WorkFlowToolbar1.CaseCode;
						ud_wfoControl.dao = dao;
						ud_wfoControl.SubmitData();
					}
				}
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "审批意见保存出错：" + ex.Message));
				throw ex;
			}
		}
		#endregion

		#region 动态生成会签信息
		/// ****************************************************************************
		/// <summary>
		/// 会签控件数据绑定
		/// 世茂特殊要求(2个项目总监会签)
		/// </summary>
		/// ****************************************************************************
		virtual protected void rptMeetSign_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			string ud_sRoleName,ud_sUserCode;
			string ud_sConfirmOpinionList = ViewState["_ConfirmOpinionList"] == null ? "" : ViewState["_ConfirmOpinionList"].ToString();

			switch ( e.Item.ItemType )
			{
				case ListItemType.AlternatingItem:
					ud_sRoleName = ((DataRowView)e.Item.DataItem).Row["RoleName"].ToString();
					ud_sUserCode = ((DataRowView)e.Item.DataItem).Row["UserCode"].ToString();
					OpinionControlInit(ud_sRoleName+"意见",ud_sRoleName,ud_sRoleName,(WorkFlowFormOpinion)e.Item.FindControl("wfoAlternatingItemOpinion"),ud_sUserCode,ud_sConfirmOpinionList,out ud_sConfirmOpinionList);
					break;
				case ListItemType.Item:
					ud_sRoleName = ((DataRowView)e.Item.DataItem).Row["RoleName"].ToString();
					ud_sUserCode = ((DataRowView)e.Item.DataItem).Row["UserCode"].ToString();
					OpinionControlInit(ud_sRoleName+"意见",ud_sRoleName,ud_sRoleName,(WorkFlowFormOpinion)e.Item.FindControl("wfoItemOpinion"),ud_sUserCode,ud_sConfirmOpinionList,out ud_sConfirmOpinionList);
					break;
				default:
					break;
			}

			ViewState["_ConfirmOpinionList"] = ud_sConfirmOpinionList;
		}
		#endregion
		
		#endregion


		#region 重写父类,邦定流程事件
		
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			//InitializeComponent();
            SetBaseControl();
			base.OnInit(e);
			this.WorkFlowToolbar1.ToolbarCommand += new System.EventHandler(WorkFlowToolbar1_ToolbarCommand);
			LoadEvent();
		}
        virtual protected void SetBaseControl()
        {
            WorkFlowToolbar1 = (WorkFlowToolbar)Page.FindControl("WorkFlowToolbar1");
            ucCheckControl = (RmsPM.Web.WorkFlowOperation.CheckControl)Page.FindControl("ucCheckControl");
            WorkFlowOpinion1 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion1");
            WorkFlowOpinion2 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion2");
            WorkFlowOpinion3 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion3");
            WorkFlowOpinion4 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion4");
            WorkFlowOpinion5 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion5");
            WorkFlowOpinion6 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion6");
            WorkFlowOpinion7 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion7");
            WorkFlowOpinion8 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion8");
            WorkFlowOpinion9 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion9");
            WorkFlowOpinion10 = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)Page.FindControl("WorkFlowOpinion10");
        }
		virtual protected void LoadEvent()
		{
		}
		virtual protected void WorkFlowToolbar1_ToolbarCommand(object sender , System.EventArgs e)
		{
		}
		#endregion
	}
}
