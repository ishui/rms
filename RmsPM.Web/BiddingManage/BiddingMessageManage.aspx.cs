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
	/// BiddingMessageManage ��ժҪ˵����
	/// </summary>
	public partial class BiddingMessageManage : BiddingWorkFlowBase
	{
		/// <summary>
		/// ������
		/// </summary>
		//protected RmsPM.Web.WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1;
		/// <summary>
		/// ���̲���쿴
		/// </summary>
		protected System.Web.UI.WebControls.DataGrid dgMeetSign;

		/// ****************************************************************************
		/// <summary>
		/// ҳ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// ****************************************************************************
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!IsPostBack)
            {
                // //--//֮��Ϊ�³�������������
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
		/// ��Ŀ����
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
		/// ��ʼ��
		/// </summary>
		/// ****************************************************************************		
		
		override protected void InitPage()
		{
			string actCode = Request["ActCode"]+"";
            string caseCode = Request["CaseCode"] + "";
			if(Request["frameType"] != null)//�ж��Ƿ�Ϊ���̼��״̬
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
			WorkFlowToolbar1.ActCode = actCode;//����������
            WorkFlowToolbar1.CaseCode = caseCode;
			WorkFlowToolbar1.FlowName = "�б�֪ͨ������";
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
			///�Ƿ���ʾMoney;
			Control_BiddingEmitMoney1.State = this.WorkFlowToolbar1.GetModuleState("ShowMoney");
			Control_BiddingEmitMoney1.BiddingCode = this.BiddingMessageModify1.BiddingCode;
			//Control_BiddingEmitMoney1.InitPage();			
			/**************************************************************************************/
			/*************************************************************************************/
			BiddingMessageModify1.SetAttachList=this.WorkFlowToolbar1.GetModuleState("����1");
			
			
			

			/**************************************************************************************
			this.LeavePass1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;//ͬ�ⰴť��
			this.LeavePass1.State = this.WorkFlowToolbar1.GetModuleState("PassBtn");
			this.LeavePass1.InitControl();
			/**************************************************************************************/

			/**************************************************************************************/
			//����״̬�鿴
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

            //��������Ƿ���Բ���
            string ud_sOpinionControlName = "WorkFlowOpinion";
            for (int i = 1; i <= 10; i++)
            {
                RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoControl;
                ud_wfoControl = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)this.Page.FindControl(ud_sOpinionControlName + i.ToString());
                ud_wfoControl.IsRdoCheck = false;
                ud_wfoControl.IsUseTemplateOpinion = true;
                ud_wfoControl.IsUseTextArea = true;
            }

            OpinionControlInit("�Ƽ����", "SM_BM_������Ʋ�", "Opinion1", base.WorkFlowOpinion1, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BM_���̲�", "Opinion2", base.WorkFlowOpinion2, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BM_��Լ��", "Opinion3", base.WorkFlowOpinion3, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BM_��Ŀ�ܼ�", "Opinion4", base.WorkFlowOpinion4, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BM_�ܲ��ܼ�1", "Opinion5", base.WorkFlowOpinion5, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BM_�ܲ��ܼ�2", "Opinion6", base.WorkFlowOpinion6, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BM_���³�1", "Opinion7", base.WorkFlowOpinion7, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BM_���³�2", "Opinion8", base.WorkFlowOpinion8, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);
            OpinionControlInit("�Ƽ����", "SM_BM_���³�3", "Opinion9", base.WorkFlowOpinion9, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);
            OpinionControlInit("�Ƽ����", "SM_BM_���³�4", "Opinion10", base.WorkFlowOpinion10, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            ViewState["_ConfirmOpinionList"] = ud_sConfirmOpinionList;
			//��ǩ���ű���ʼ��
			/**************************************************************************************/
			DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("��ǩ����");

			if ( ud_dtSendItems.Rows.Count > 0 )
			{
				rptMeetSign.DataSource = ud_dtSendItems;
				rptMeetSign.DataBind();
			}
			PageControlInit();
		}
		/// ****************************************************************************
		/// <summary>
		/// ����������������
		/// </summary>
		/// ****************************************************************************
		override protected void WorkFlowPropertySave()
		{
			if(this.BiddingMessageModify1.State == ModuleState.Operable)
			{
				WorkFlowToolbar1.SaveCasePropertyValue("����",this.BiddingMessageModify1.ContractName);
				WorkFlowToolbar1.SaveCasePropertyValue("������",user.UserCode);
				WorkFlowToolbar1.SaveCasePropertyValue("���ƽ��",this.BiddingMessageModify1.Money);
				WorkFlowToolbar1.SaveCasePropertyValue("ҵ�����",BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(this.BiddingMessageModify1.BiddingType));
				WorkFlowToolbar1.SaveCasePropertyValue("��Ҫ���",this.BiddingMessageModify1.mostly);
				WorkFlowToolbar1.SaveCasePropertyValue("�û����",user.GetOperationType());
				WorkFlowToolbar1.SaveCasePropertyValue("��Ŀ����",BLL.ProjectRule.GetUnitByProject(this.BiddingMessageModify1.ProjectCode));
				WorkFlowToolbar1.SaveCasePropertyValue("��Ŀ����",this.BiddingMessageModify1.ProjectCode);
                WorkFlowToolbar1.SaveCasePropertyValue("��󱨼�", this.BiddingMessageModify1.MaxMoney);
			}
			base.WorkFlowPropertySave();
		}
		/// ****************************************************************************
		/// <summary>
		/// �������¼�
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
					//ǩ��
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.SignIn)
					{
						WorkFlowToolbar1.SignIn(dao);
						InitPage();
						if(WorkFlowToolbar1.GetModuleState("Opinion4") == ModuleState.Operable)
							WorkFlowToolbar1.SaveCasePropertyValue("�û����",user.GetOperationType());

					}
					//����
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Send)
					{
						//Response.Write(Rms.Web.JavaScript.WriteJS("ggggg"));
						try
						{
							DataSubmit(dao);
							//���Ϊ����,��״̬��Ϊ"�б굥λ������"
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
					//�˻�
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Back)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.Back();
					}
					//�ջ�
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Return)
					{
						WorkFlowToolbar1.Return();
					}
					//�;�����
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.BackTop)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.BackTop();
					}
					//�������
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Opinion)
					{
						WorkFlowToolbar1.SaveOpinion();
						this.WorkFlowCaseState1.ControlDataBind();
					}
					//����
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
					//���
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.TaskFinish)
					{
						DataSubmit(dao);
						if(WorkFlowToolbar1.GetModuleState("End")==ModuleState.Eyeable)
						{
							BLL.BiddingSystem.Set_BiddingState("42",this.BiddingMessageModify1.BiddingCode);
						}
						WorkFlowToolbar1.TaskFinish();

					}
					//����
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Finish)
					{
						DataSubmit(dao);
						if(WorkFlowToolbar1.GetModuleState("End")==ModuleState.Eyeable)
						{
							BLL.BiddingSystem.Set_BiddingState("42",this.BiddingMessageModify1.BiddingCode);
						}
						WorkFlowToolbar1.Finish();

					}
					//����
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.MakeCopy)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.MakeCopy();

					}
                    //ɾ��
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
		/// ҵ�����ݲ���
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
		/// ��ǩ�ؼ����ݰ�
		/// ��ï����Ҫ��(2����Ŀ�ܼ��ǩ)
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


		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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


