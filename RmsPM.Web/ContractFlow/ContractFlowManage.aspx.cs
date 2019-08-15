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
	/// ContractFlowManage ��ժҪ˵������ͬ����
	/// </summary>
	/// *******************************************************************************************
	public partial class ContractFlowManage : PageBase
	{
		/// <summary>
		/// ������
		/// </summary>
		/// <summary>
		/// ��ͬ������Ϣ
		/// </summary>
		/// <summary>
		/// ��ͬ�޸�
		/// </summary>
		/// <summary>
		/// ������
		/// </summary>
		/// <summary>
		/// ���ž������
		/// </summary>
		/// <summary>
		/// ��Ʋ����
		/// </summary>
		/// <summary>
		/// ���ʲ����
		/// </summary>
		/// <summary>
		/// ���̲����
		/// </summary>
		/// <summary>
		/// ���ײ����
		/// </summary>
		/// <summary>
		/// Ԥ���㲿���
		/// </summary>
		/// <summary>
		/// �������
		/// </summary>
		/// <summary>
		/// ���ž����ܽ����
		/// </summary>
		/// <summary>
		/// ���ţ���Ŀ�����������
		/// </summary>
		/// <summary>
		/// ���³��ܾ������
		/// </summary>
		/// <summary>
		/// �칫�����
		/// </summary>
		/// <summary>
		/// ����״̬��ѯ
		/// </summary>


		/// ****************************************************************************
		/// <summary>
		/// ҳ�����
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
		/// ��ʼ��
		/// </summary>
		/// ****************************************************************************
		private void InitPage()
		{
			string actCode = Request["ActCode"]+"";

			/********************************* ���������� **********************************/
			if(Request["frameType"] != null)//�ж��Ƿ�Ϊ���̼��״̬
			{
				if(Request["frameType"].ToString() == "List")
				{
					WorkFlowToolbar1.Scout = true;
				}
			}
			
			if(Request["ApplicationCode"] != null)
				WorkFlowToolbar1.ApplicationCode = Request["ApplicationCode"].ToString();
			
			WorkFlowToolbar1.ActCode = actCode;//����������
			WorkFlowToolbar1.FlowName = "��ͬ�������";
			WorkFlowToolbar1.SystemUserCode = this.user.UserCode;
			WorkFlowToolbar1.SourceUrl = "../WorkFlowControl/";
			WorkFlowToolbar1.ToolbarDataBind();
			/******************************************************************************/

			/********************************* ��ͬ��Ϣ���� ********************************/
			this.ContractInfoControl1.ContractCode = this.WorkFlowToolbar1.ApplicationCode;
			//this.ContractInfoControl1.ProjectCode = this.project.ProjectCode;
			this.ContractInfoControl1.UserCode = this.user.UserCode;
			this.ContractInfoControl1.State = this.WorkFlowToolbar1.GetModuleState("Base");
			this.ContractInfoControl1.InitControl();
			/******************************************************************************/

			/********************************* ��ͬ�޸����� ********************************/
			this.ContractModifyButtonControl1.ContractCode = this.WorkFlowToolbar1.ApplicationCode;
			this.ContractModifyButtonControl1.ProjectCode = this.ContractInfoControl1.ProjectCode;
			this.ContractModifyButtonControl1.State = this.WorkFlowToolbar1.GetModuleState("Modify");
			this.ContractModifyButtonControl1.ActCode = this.WorkFlowToolbar1.ActCode;
			this.ContractModifyButtonControl1.InitControl();
			/******************************************************************************/

			/********************************* ��ͬ���������� ********************************/
			this.ContractAuditingControl1.ContractCode = this.WorkFlowToolbar1.ApplicationCode;
			this.ContractAuditingControl1.ProjectCode = this.ContractInfoControl1.ProjectCode;
			this.ContractAuditingControl1.State = this.WorkFlowToolbar1.GetModuleState("Money");
			this.ContractAuditingControl1.InitControl();
			/*******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion1.OpinionName = "���ž������";
			this.WorkFlowOpinion1.OpinionType = "ContractOpinion1";
			this.WorkFlowOpinion1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion1.State = this.WorkFlowToolbar1.GetModuleState("UM1");
			this.WorkFlowOpinion1.IsTextBox = false;
			this.WorkFlowOpinion1.DISPLAY = this.WorkFlowToolbar1.GetModuleState("UMD1");
			this.WorkFlowOpinion1.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion2.OpinionName = "��Ʋ����";
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
			this.WorkFlowOpinion3.OpinionName = "���ʲ����";
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
			this.WorkFlowOpinion4.OpinionName = "���̲����";
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
			this.WorkFlowOpinion5.OpinionName = "���ײ����";
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
			this.WorkFlowOpinion6.OpinionName = "Ԥ���㲿���";
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
			this.WorkFlowOpinion7.OpinionName = "�������";
			this.WorkFlowOpinion7.OpinionType = "ContractOpinion7";
			this.WorkFlowOpinion7.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion7.State = this.WorkFlowToolbar1.GetModuleState("CUM1");
			this.WorkFlowOpinion7.IsTextBox = false;
			this.WorkFlowOpinion7.DISPLAY = this.WorkFlowToolbar1.GetModuleState("CUMD1");
			this.WorkFlowOpinion7.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion8.OpinionName = "��Ŀ�������";
			this.WorkFlowOpinion8.OpinionType = "ContractOpinion8";
			this.WorkFlowOpinion8.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion8.State = this.WorkFlowToolbar1.GetModuleState("UM2");
			this.WorkFlowOpinion8.IsTextBox = false;
			this.WorkFlowOpinion8.DISPLAY = this.WorkFlowToolbar1.GetModuleState("UMD2");
			this.WorkFlowOpinion8.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion9.OpinionName = "��Ŀ�ܼ����";
			this.WorkFlowOpinion9.OpinionType = "ContractOpinion9";
			this.WorkFlowOpinion9.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion9.State = this.WorkFlowToolbar1.GetModuleState("PMUM1");
			this.WorkFlowOpinion9.IsTextBox = false;
			this.WorkFlowOpinion9.DISPLAY = this.WorkFlowToolbar1.GetModuleState("PMUMD1");
			this.WorkFlowOpinion9.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion10.OpinionName = "���³��ܾ������";
			this.WorkFlowOpinion10.OpinionType = "ContractOpinion10";
			this.WorkFlowOpinion10.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion10.State = this.WorkFlowToolbar1.GetModuleState("BUM1");
			this.WorkFlowOpinion10.IsTextBox = false;
			this.WorkFlowOpinion10.DISPLAY = this.WorkFlowToolbar1.GetModuleState("BUMD1");
			this.WorkFlowOpinion10.InitControl();
			/******************************************************************************/

			/******************************************************************************/
			this.WorkFlowOpinion11.OpinionName = "�칫�����";
			this.WorkFlowOpinion11.OpinionType = "ContractOpinion11";
			this.WorkFlowOpinion11.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowOpinion11.State = this.WorkFlowToolbar1.GetModuleState("OUM1");
			this.WorkFlowOpinion11.IsTextBox = false;
			this.WorkFlowOpinion11.DISPLAY = this.WorkFlowToolbar1.GetModuleState("OUMD1");
			this.WorkFlowOpinion11.InitControl();
			/******************************************************************************/

			string action = Request["action"]+"";//����״̬�鿴
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
		/// �������¼�
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
					//ǩ��
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.SignIn)
					{
						WorkFlowToolbar1.SignIn(dao);
						InitPage();

					}
					//����
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Send)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.Send();

					}
					//����
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
					//���
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.TaskFinish)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.TaskFinish();

					}
					//����
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Finish)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.Finish();
						ContractControlBase.ContractAuditing(WorkFlowToolbar1.ApplicationCode);

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
		/// ����������������
		/// </summary>
		/// ****************************************************************************
		private void WorkFlowPropertySave()
		{
			
		}
		/// ****************************************************************************
		/// <summary>
		/// ҵ�����ݲ���
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
		#endregion
		//this.WorkFlowToolbar1.ToolbarCommand += new System.EventHandler(this.WorkFlowToolbar1_ToolbarCommand);
	}
}
