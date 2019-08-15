using System;
using System.Web;
using System.Web.UI;
using RmsPM.BLL;
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace RmsPM.Web.WorkFlowPage
{
	/// <summary>
	/// MeetingWorkFlowPageBase ��ժҪ˵����
	/// </summary>
	public class MeetingWorkFlowPageBase : BiddingManage.YX_WorkFlowPageBase
	{
		public MeetingWorkFlowPageBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//protected override void InitPage()
		//{
			
		//}


		#region ��ʼ��
		virtual protected void PageControlInit(Repeater rptMeetSign)
		{
			base.PageControlInit ();
			//��ǩ���ű���ʼ��
			/**************************************************************************************/
			DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("��ǩ����");

			if ( ud_dtSendItems.Rows.Count > 0 )
			{
				rptMeetSign.DataSource = ud_dtSendItems;
				rptMeetSign.DataBind();
			}
		}

		#endregion


		#region ������������
		override protected void WorkFlowPropertySave()
		{
			//base.WorkFlowPropertySave();
			if ( wftToolbar.GetModuleState("׼����ǩ") == ModuleState.Operable)
			{
				wftToolbar.SaveCasePropertyValue("��ǩ����",wftToolbar.SendRoleItems);
				wftToolbar.SaveCasePropertyValue("��ǩ������",this.user.UserCode);
			}
		}

		#endregion


		#region ����ҵ������
		virtual protected Boolean OpinionDataSubmit(StandardEntityDAO dao,Repeater rptMeetSign)
		{
			//Boolean ReturnValue;

			//ReturnValue = base.OpinionDataSubmit(dao);

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
						ud_wfoControl.ApplicationCode = wftToolbar.ApplicationCode;
						ud_wfoControl.dao = dao;
						ud_wfoControl.SubmitData();
					}
				}
				return true;
			}
			catch(Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "��������������" + ex.Message));
				throw ex;
			}
		}

		#endregion


		#region ��̬���������
		/// ****************************************************************************
		/// <summary>
		/// ��ǩ�ؼ����ݰ�
		/// ��ï����Ҫ��(2����Ŀ�ܼ��ǩ)
		/// </summary>
		/// ****************************************************************************
		virtual protected  void rptMeetSign_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			string ud_sRoleName,ud_sUserCode;
			string ud_sConfirmOpinionList = ViewState["_ConfirmOpinionList"] == null ? "" : ViewState["_ConfirmOpinionList"].ToString();

			switch ( e.Item.ItemType )
			{
				case ListItemType.AlternatingItem:
					ud_sRoleName = ((DataRowView)e.Item.DataItem).Row["RoleName"].ToString();
					ud_sUserCode = ((DataRowView)e.Item.DataItem).Row["UserCode"].ToString();
					OpinionControlInit(ud_sRoleName+"���",ud_sRoleName,ud_sRoleName,(WorkFlowFormOpinion)e.Item.FindControl("wfoAlternatingItemOpinion"),ud_sUserCode,ud_sConfirmOpinionList,out ud_sConfirmOpinionList);
					break;
				case ListItemType.Item:
					ud_sRoleName = ((DataRowView)e.Item.DataItem).Row["RoleName"].ToString();
					ud_sUserCode = ((DataRowView)e.Item.DataItem).Row["UserCode"].ToString();
					OpinionControlInit(ud_sRoleName+"���",ud_sRoleName,ud_sRoleName,(WorkFlowFormOpinion)e.Item.FindControl("wfoItemOpinion"),ud_sUserCode,ud_sConfirmOpinionList,out ud_sConfirmOpinionList);
					break;
				default:
					break;
			}

			ViewState["_ConfirmOpinionList"] = ud_sConfirmOpinionList;
		}

		#endregion
	}
}
