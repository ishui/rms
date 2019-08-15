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
using System.Text;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;
using RmsPM.BLL;

namespace RmsPM.Web.BiddingManage
{
	/// <summary>
	/// BiddingAuditingManage ��ժҪ˵����
	/// </summary>
	public partial class BiddingAuditingManage : BiddingWorkFlowBase
	{
		/// <summary>
		/// ������
		/// </summary>
		/// <summary>
		/// ���̲���쿴
		/// </summary>
		/// <summary>
		/// �������
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
                string company =  this.up_sPMName.ToLower();
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
                            this.Response.Redirect("XCN_BiddingAuditingmanage.aspx?BiddingCode=" + Binddingcode);
                        else
                            this.Response.Redirect("XCN_BiddingAuditingmanage.aspx?BiddingCode=" + Binddingcode + "ApplicationCode=" + ApplicationCode);
                        break;
                }
                //
                InitPage();
            }
		}
		/// ****************************************************************************
		/// <summary>
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
			WorkFlowToolbar1.FlowName = "�б굥λ����";
			WorkFlowToolbar1.SystemUserCode = this.user.UserCode;
			WorkFlowToolbar1.SourceUrl = "../WorkFlowControl/";
            WorkFlowToolbar1.ProjectCode = Request["ProjectCode"] + "";
			WorkFlowToolbar1.ToolbarDataBind();
			bool tempDeleteFlag = false;
			if(this.WorkFlowToolbar1.GetModuleState("DeleteFlag") == ModuleState.Operable)
				tempDeleteFlag = true;
			WorkFlowToolbar1.BtnDeleteVisible = tempDeleteFlag;
			/**************************************************************************************/

			this.BiddingAuditing1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
            this.BiddingAuditing1.MainState = this.WorkFlowToolbar1.GetModuleState("BaseWrite");
            this.BiddingAuditing1.Attachstate = this.WorkFlowToolbar1.GetModuleState("����");
			this.BiddingAuditing1.State = this.WorkFlowToolbar1.GetModuleState("SupplierSelect");
			this.BiddingAuditing1.State1 = this.WorkFlowToolbar1.GetModuleState("Select1");
			this.BiddingAuditing1.State2 = this.WorkFlowToolbar1.GetModuleState("Select2");
			this.BiddingAuditing1.State3 = this.WorkFlowToolbar1.GetModuleState("Select3");
			this.BiddingAuditing1.State4 = this.WorkFlowToolbar1.GetModuleState("Select4");
			this.BiddingAuditing1.State5 = this.WorkFlowToolbar1.GetModuleState("Select5");
			this.BiddingAuditing1.BiddingCode = Request["BiddingCode"]+"";
			this.BiddingAuditing1.UserCode = user.UserCode;
			this.BiddingAuditing1.InitControl();

			this.BiddingTop1.BiddingCode = this.BiddingAuditing1.BiddingCode;
			this.BiddingTop1.ContractNember = BiddingSystem.GetContractNemberByBiddingCode(this.BiddingAuditing1.BiddingCode);
			this.BiddingTop1.InitControl();

			/**************************************************************************************
			this.LeavePass1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;//ͬ�ⰴť��
			this.LeavePass1.State = this.WorkFlowToolbar1.GetModuleState("PassBtn");
			this.LeavePass1.InitControl();
			/**************************************************************************************/
			BiddingAuditing1.SetAgreementMessage = this.WorkFlowToolbar1.GetModuleState("��Լ��");
			BiddingAuditing1.SetProjectMessage = this.WorkFlowToolbar1.GetModuleState("���̲�");
			BiddingAuditing1.SetAdviserMessage = this.WorkFlowToolbar1.GetModuleState("���ʹ�˾");
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

            OpinionControlInit("�Ƽ����", "SM_BA_������Ʋ�", "Opinion1", base.WorkFlowOpinion1, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BA_���̲�", "Opinion2", base.WorkFlowOpinion2, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BA_��Լ��", "Opinion3", base.WorkFlowOpinion3, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BA_��Ŀ�ܼ�", "Opinion4", base.WorkFlowOpinion4, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BA_�ܲ��ܼ�1", "Opinion5", base.WorkFlowOpinion5, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BA_�ܲ��ܼ�2", "Opinion6", base.WorkFlowOpinion6, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BA_���³�1", "Opinion7", base.WorkFlowOpinion7, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("�Ƽ����", "SM_BA_���³�2", "Opinion8", base.WorkFlowOpinion8, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);
            OpinionControlInit("�Ƽ����", "SM_BA_���³�3", "Opinion9", base.WorkFlowOpinion9, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);
            OpinionControlInit("�Ƽ����", "SM_BA_���³�4", "Opinion10", base.WorkFlowOpinion10, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            ViewState["_ConfirmOpinionList"] = ud_sConfirmOpinionList;

			
			//��ǩ���ű���ʼ��
			/**************************************************************************************/
			DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("��ǩ����");

            if (ud_dtSendItems.Rows.Count > 0)
            {
                rptMeetSign.DataSource = ud_dtSendItems;
                rptMeetSign.DataBind();
            }
            else
            {
                this.WorkFlow4.Visible = true;
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
			//WorkFlowToolbar1.SaveCasePropertyValue("���벿��",this.LeaveInfo1.LeaveUnit);
			if(this.WorkFlowToolbar1.GetModuleState("SaveProperty") == ModuleState.Operable)
			{
				WorkFlowToolbar1.SaveCasePropertyValue("������",user.UserCode);
				WorkFlowToolbar1.SaveCasePropertyValue("����",this.BiddingTop1.Title);
				WorkFlowToolbar1.SaveCasePropertyValue("���ƽ��",this.BiddingTop1.Money);
				WorkFlowToolbar1.SaveCasePropertyValue("ҵ�����",BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(this.BiddingTop1.BiddingType));
				WorkFlowToolbar1.SaveCasePropertyValue("��Ҫ���",this.BiddingTop1.mostly);
				WorkFlowToolbar1.SaveCasePropertyValue("�û����",user.GetOperationType());
				WorkFlowToolbar1.SaveCasePropertyValue("��Ŀ����",BLL.ProjectRule.GetUnitByProject(this.BiddingAuditing1.ProjectCode));
				WorkFlowToolbar1.SaveCasePropertyValue("��Ŀ����",this.BiddingAuditing1.ProjectCode);
                WorkFlowToolbar1.SaveCasePropertyValue("��󱨼�", this.BiddingAuditing1.MaxMoney);
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
                        if (this.WorkFlowToolbar1.GetModuleState("Opinion4") == ModuleState.Operable)
                            WorkFlowToolbar1.SaveCasePropertyValue("�û����", user.GetOperationType());
                        if (WorkFlowToolbar1.GetModuleState("BaseWrite") == ModuleState.Operable)
                        {
                            BiddingSystem.Set_BiddingState("3", this.BiddingAuditing1.BiddingCode);
                        }
						InitPage();
						

					}
					//����
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Send)
					{
						if(!this.BiddingAuditing1.SupplierSelectedFlag && this.BiddingAuditing1.State == ModuleState.Operable)
						{
							this.RegisterStartupScript("","<script>alert('��ѡ���б굥λ��');</script>");
							return;
						}
						DataSubmit(dao);
						///
						//Bll.Bidding bidding = new Bidding();
						//bidding..BiddingCode
						//����bidding Ϊ����״̬
						if(WorkFlowToolbar1.GetModuleState("BaseWrite")==ModuleState.Operable)
						{
							BiddingSystem.Set_BiddingState("5",this.BiddingAuditing1.BiddingCode);
						}
						WorkFlowToolbar1.Send();
					}
					//�˻�
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Back)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.Back();
						//if(WorkFlowToolbar1.GetModuleState("BaseWrite")==ModuleState.Operable)
						//{
						//	BiddingSystem.Set_BiddingState("3");
						//}
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
						if(!this.BiddingAuditing1.SupplierSelectedFlag && this.BiddingAuditing1.State == ModuleState.Operable)
						{
							this.RegisterStartupScript("","<script>alert('��ѡ���б굥λ��');</script>");
							return;
						}
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
						//����bidding Ϊ����״̬
						if(WorkFlowToolbar1.GetModuleState("BaseWrite")==ModuleState.Operable)
						{
							BiddingSystem.Set_BiddingState("3",this.BiddingAuditing1.BiddingCode);
						}
					}
					//���
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.TaskFinish)
					{
						if(!this.BiddingAuditing1.SupplierSelectedFlag && this.BiddingAuditing1.State == ModuleState.Operable)
						{
							this.RegisterStartupScript("","<script>alert('��ѡ���б굥λ��');</script>");
							return;
						}
						DataSubmit(dao);
						WorkFlowToolbar1.TaskFinish();

					}
					//����
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Finish)
					{
						if(!this.BiddingAuditing1.SupplierSelectedFlag && this.BiddingAuditing1.State == ModuleState.Operable)
						{
							this.RegisterStartupScript("","<script>alert('��ѡ���б굥λ��');</script>");
							return;
						}
						this.BiddingAuditing1.State = this.WorkFlowToolbar1.GetModuleState("SupplierSelect");
						DataSubmit(dao);
						WorkFlowToolbar1.Finish();

					}
					//����
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
		/// ҵ�����ݲ���
		/// </summary>
		/// ****************************************************************************
		override protected Boolean DataSubmit(StandardEntityDAO dao)
		{
			
			this.WorkFlowCaseState1.SubmitData();
			
			//if(this.BiddingAuditing1.State == ModuleState.Operable)
			//{
				//this.BiddingAuditing1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.BiddingAuditing1.dao = dao;
				this.BiddingAuditing1.SubmitData();
				WorkFlowToolbar1.ApplicationCode = this.BiddingAuditing1.ApplicationCode;
			//}
			SaveMeetMessage(dao,this.rptMeetSign);
			return base.DataSubmit(dao);
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
	}
}
