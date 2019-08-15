using System;
using Rms.ORMap;

namespace RmsPM.Web.WorkFlowControl
{
	/// <summary>
	/// WorkFlowInit 的摘要说明。
	/// </summary>
	public class WorkFlowInit
	{
		public WorkFlowInit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static void Init_Toolbar( WorkFlowToolbar up_Toolbar,string up_sFlowName, RmsPM.Web.User up_User, object ApplicationCode, string actCode, bool BtnDeleteVisible)
		{
			try
			{
				if(ApplicationCode != null)
				{
					up_Toolbar.ApplicationCode = ApplicationCode.ToString();
				}
			
				/**************************************************************************************/
				up_Toolbar.ActCode = actCode;//工具栏设置
				up_Toolbar.FlowName = up_sFlowName;
				up_Toolbar.SystemUserCode = up_User.UserCode;
				up_Toolbar.SourceUrl = "../WorkFlowControl/";
				up_Toolbar.ToolbarDataBind();
				up_Toolbar.BtnDeleteVisible = BtnDeleteVisible;
				/**************************************************************************************/

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}


		public static void Init_CaseState( WorkFlowCaseState up_CaseState,WorkFlowToolbar up_Toolbar, RmsPM.Web.User up_User)
		{
			try
			{
				/**************************************************************************************/
				//流程状态查看
				up_CaseState.ActCode = up_Toolbar.ActCode;
				up_CaseState.ApplicationCode = up_Toolbar.ApplicationCode;
				up_CaseState.FlowName = up_Toolbar.FlowName;
				up_CaseState.UserCode = up_User.UserCode;
				up_CaseState.Scout = up_Toolbar.Scout;
				up_CaseState.ControlDataBind();

				/**************************************************************************************/

			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		public static void OpinionDataSubmit(StandardEntityDAO dao,WorkFlowToolbar up_Toolbar,System.Web.UI.Page up_Page ,int up_iProposerCount, int up_iDepartmentCount, int up_iDirectorCount)
		{
			try
			{
				string ud_sProposerControlName, ud_sDepartmentControlName, ud_sDirectorControlName;
				int i;

				ud_sProposerControlName = "wfoProposer";
				ud_sDepartmentControlName = "wfoDepartment";
				ud_sDirectorControlName = "wfoDirector";
		
				for ( i=1;i<=up_iProposerCount;i++ )
				{
					RmsPM.Web.WorkFlowControl.WorkFlowOpinion ud_wfoContraol;
					ud_wfoContraol = (RmsPM.Web.WorkFlowControl.WorkFlowOpinion)up_Page.FindControl(ud_sProposerControlName + i.ToString());

					if(ud_wfoContraol.State == ModuleState.Operable)
					{
						ud_wfoContraol.ApplicationCode = up_Toolbar.ApplicationCode;
						ud_wfoContraol.dao = dao;
						ud_wfoContraol.SubmitData();
					}			
				}

				for ( i=1;i<=up_iDepartmentCount;i++ )
				{
					RmsPM.Web.WorkFlowControl.WorkFlowOpinion ud_wfoContraol;
					ud_wfoContraol = (RmsPM.Web.WorkFlowControl.WorkFlowOpinion)up_Page.FindControl(ud_sDepartmentControlName + i.ToString());

					if(ud_wfoContraol.State == ModuleState.Operable)
					{
						ud_wfoContraol.ApplicationCode = up_Toolbar.ApplicationCode;
						ud_wfoContraol.dao = dao;
						ud_wfoContraol.SubmitData();
					}			
				}

				for ( i=1;i<=up_iDirectorCount;i++ )
				{
					RmsPM.Web.WorkFlowControl.WorkFlowOpinion ud_wfoContraol;
					ud_wfoContraol = (RmsPM.Web.WorkFlowControl.WorkFlowOpinion)up_Page.FindControl(ud_sDirectorControlName + i.ToString());

					if(ud_wfoContraol.State == ModuleState.Operable)
					{
						ud_wfoContraol.ApplicationCode = up_Toolbar.ApplicationCode;
						ud_wfoContraol.dao = dao;
						ud_wfoContraol.SubmitData();
					}			
				}

			}
			catch(Exception ex)
			{
				throw ex;
			}

		}


	}
}
