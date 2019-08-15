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
using RmsPM.DAL.EntityDAO;
using Rms.WorkFlow;
using RmsPM.Web.WorkFlowControl;

namespace RmsPM.Web.OA.Bill
{
	/// <summary>
	/// EquipmentApply 的摘要说明。
	/// </summary>
	public class EquipmentApply_older : PageBase//System.Web.UI.Page
	{

		protected System.Web.UI.HtmlControls.HtmlInputText EquipmentName;
		protected System.Web.UI.HtmlControls.HtmlTextArea Configure;
		protected System.Web.UI.HtmlControls.HtmlTextArea Remark;
		protected System.Web.UI.HtmlControls.HtmlInputText Purpose;
		private string OAEquipmentApplyCode = "";
		protected AspWebControl.Calendar ReqTime;
		protected RmsPM.Web.UserControls.InputUser ApplyUser;
		protected WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1;
		protected System.Web.UI.WebControls.Literal lEquipmentName;
		protected System.Web.UI.WebControls.Literal lApplyUser;
		protected System.Web.UI.WebControls.Literal lReqTime;
		protected System.Web.UI.WebControls.Literal lConfigure;
		protected System.Web.UI.WebControls.Literal lRemark;
		public System.Web.UI.HtmlControls.HtmlTable tblEdit;
		public System.Web.UI.HtmlControls.HtmlTable tblView;
		protected RmsPM.Web.OA.Bill.EnquipmentUse EnquipmentUse1;
		protected System.Web.UI.WebControls.Literal lPurpose;
		protected RmsPM.Web.UserControls.UCDuty Unit;
		protected System.Web.UI.WebControls.Literal lUnit;
		public WorkFlowControl.WorkFlowControlClassBase mBase = new WorkFlowControlClassBase();
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
			try 
			{		
				this.InitPage();
				if(!this.IsPostBack)
					this.LoadData();
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}

		private void InitPage()
		{
			string actCode = Request["ActCode"]+"";
			WorkFlowToolbar1.ActCode = actCode;
			WorkFlowToolbar1.FlowName = "计算机设备申请";
			WorkFlowToolbar1.SystemUserCode = this.user.UserCode;
			WorkFlowToolbar1.SourceUrl = "../../WorkFlowControl/";
			WorkFlowToolbar1.ToolbarDataBind();

			this.OAEquipmentApplyCode = this.WorkFlowToolbar1.ApplicationCode;
			// 本页面状态控制 InitControl()
			//RmsPM.Web.WorkFlowControl.ModuleState mModuleState;
			this.mBase.State = this.WorkFlowToolbar1.GetModuleState("1");
			if(this.mBase.State == RmsPM.Web.WorkFlowControl.ModuleState.Sightless)//不可见的
			{
				this.Visible = false;
			}
			else if(this.mBase.State == RmsPM.Web.WorkFlowControl.ModuleState.Operable)//可操作的
			{
				this.tblView.Visible = false;
				this.tblEdit.Visible = true;
			}
			else if(this.mBase.State == RmsPM.Web.WorkFlowControl.ModuleState.Eyeable)//可见的
			{
				this.tblEdit.Visible = false;
				this.tblView.Visible = true;				
			}
			else 
			{
				this.Visible = false;
			}
			
			this.EnquipmentUse1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.EnquipmentUse1.State = this.WorkFlowToolbar1.GetModuleState("2");
			this.EnquipmentUse1.InitControl();

		}

		private void LoadData()
		{
			if(this.OAEquipmentApplyCode != "")
			{
				EntityData entity = DAL.EntityDAO.OADAO.GetOAEquipmentApplyByCode(OAEquipmentApplyCode);
				DataRow dr = entity.CurrentRow;
								
				EquipmentName.Value = entity.GetString("EquipmentName");
				Unit.Value = entity.GetString("Unit");
				Configure.Value = entity.GetString("Configure");
				ReqTime.Value = entity.GetDateTimeOnlyDate("ReqTime");
				ApplyUser.Value = entity.GetString("ApplyUser");
				Remark.Value = entity.GetString("Remark");
				Purpose.Value = entity.GetString("Purpose");
				
				lEquipmentName.Text = entity.GetString("EquipmentName");
				lUnit.Text = BLL.SystemRule.GetUnitName(entity.GetString("Unit"));
				lConfigure.Text = entity.GetString("Configure");
				lReqTime.Text = entity.GetDateTimeOnlyDate("ReqTime");
				lApplyUser.Text = BLL.SystemRule.GetUserName(entity.GetString("ApplyUser"));
				lRemark.Text = entity.GetString("Remark");
				lPurpose.Text = entity.GetString("Purpose");

				ViewState["Unit"] = entity.GetString("Unit");
				ViewState["User"] = entity.GetString("ApplyUser");

				entity.Dispose();	
				
			}
			else
				if(!this.IsPostBack)
					ApplyUser.Value = base.user.UserCode;
			
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
			this.Load += new System.EventHandler(this.Page_Load);
			this.WorkFlowToolbar1.ToolbarCommand += new System.EventHandler(this.WorkFlowToolbar1_ToolbarCommand);
		}
		#endregion
		//this.WorkFlowToolbar1.ToolbarCommand += new System.EventHandler(this.WorkFlowToolbar1_ToolbarCommand);

		private void WorkFlowToolbar1_ToolbarCommand(object sender , System.EventArgs e)
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
				if(!CheckData())
					return;
				EntityData entity = this.SaveData();
				RmsPM.DAL.EntityDAO.OADAO.SubmitAllOAEquipmentApply(entity);
				WorkFlowToolbar1.ApplicationCode = OAEquipmentApplyCode;
				this.EnquipmentUse1.ApplicationCode = OAEquipmentApplyCode;
				EntityData entity1 = this.EnquipmentUse1.SaveData((string)ViewState["Unit"],(string)ViewState["User"]);
				RmsPM.DAL.EntityDAO.OADAO.SubmitAllOAEquipmentUse(entity1);
				entity.Dispose();
				entity1.Dispose();

				WorkFlowToolbar1.Send();				
			}
			//保存
			if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Save)
			{
				if(!CheckData())
					return;
				EntityData entity = this.SaveData();
				RmsPM.DAL.EntityDAO.OADAO.SubmitAllOAEquipmentApply(entity);
				WorkFlowToolbar1.ApplicationCode = OAEquipmentApplyCode;
				EntityData entity1 = this.EnquipmentUse1.SaveData((string)ViewState["Unit"],(string)ViewState["User"]);
				RmsPM.DAL.EntityDAO.OADAO.SubmitAllOAEquipmentUse(entity1);
				entity.Dispose();
				entity1.Dispose();

				WorkFlowToolbar1.Save();				
			}
			//完成
			if(WorkFlowToolbar1.CommandType == ToolbarCommandType.TaskFinish)
			{
				WorkFlowToolbar1.TaskFinish();
			}
			//结束
			if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Finish)
			{
				WorkFlowToolbar1.ApplicationCode = OAEquipmentApplyCode;
				EntityData entity1 = this.EnquipmentUse1.SaveData((string)ViewState["Unit"],(string)ViewState["User"]);
				RmsPM.DAL.EntityDAO.OADAO.SubmitAllOAEquipmentUse(entity1);
				entity1.Dispose();

				WorkFlowToolbar1.Finish();				
			}
			//抄送
			if(WorkFlowToolbar1.CommandType == ToolbarCommandType.MakeCopy)
			{
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
		
		private EntityData SaveData()
		{
			EntityData entity = DAL.EntityDAO.OADAO.GetOAEquipmentApplyByCode(OAEquipmentApplyCode);
			if(this.tblEdit.Visible == true)
			{
				DataRow dr;
				if(this.WorkFlowToolbar1.ApplicationCode == "")
				{
					dr = entity.GetNewRecord();
					OAEquipmentApplyCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("OAEquipmentApply");
				}
				else
					dr = entity.CurrentRow;
				dr["OAEquipmentApplyCode"] = OAEquipmentApplyCode;
				dr["EquipmentName"] = EquipmentName.Value;
				dr["Unit"] = Unit.Value;
				dr["Configure"] = Configure.Value;
				dr["Purpose"] = Purpose.Value;
				if(ReqTime.Value.Length>0)
					dr["ReqTime"] = ReqTime.Value;
				dr["ApplyUser"] = ApplyUser.Value;
				dr["Remark"] = Remark.Value;
				if(this.WorkFlowToolbar1.ApplicationCode == "")
				{
					entity.AddNewRecord(dr);
				}
			}
			return entity;
		}

		private bool CheckData()
		{
			return true;
		}

		private void btnDelete_ServerClick(object sender, System.EventArgs e)
		{
			try 
			{		
				EntityData entity = DAL.EntityDAO.OADAO.GetOAEquipmentApplyByCode(OAEquipmentApplyCode);
				DAL.EntityDAO.OADAO.DeleteOAEquipmentApply(entity);

				Response.Write(Rms.Web.JavaScript.OpenerReload(true));
				Response.Write(Rms.Web.JavaScript.WinClose(true));
			}
			catch (Exception ex)
			{
				ApplicationLog.WriteLog(this.ToString(),ex,"");
				Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
			}
		}
	
	}
}
