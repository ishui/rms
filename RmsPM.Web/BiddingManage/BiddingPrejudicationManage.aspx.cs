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
	/// BiddingPrejudicationManage 的摘要说明。
	/// </summary>
	public partial class BiddingPrejudicationManage : BiddingWorkFlowBase
	{
		/// <summary>
		/// 工具栏
		/// </summary>
		//protected RmsPM.Web.WorkFlowControl.WorkFlowToolbar WorkFlowToolbar1;
		/// <summary>
		/// 流程步骤察看
		/// </summary>
		/// <summary>   
		/// 建筑设计部意见		/// </summary>
		
		/// <summary>
		/// 预审信息
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
                        if (ApplicationCode=="")
                            this.Response.Redirect("XCN_biddingprejudicationmanage.aspx?BiddingCode=" + Binddingcode);
                        else
                            this.Response.Redirect("XCN_biddingprejudicationmanage.aspx?BiddingCode=" + Binddingcode + "ApplicationCode=" + ApplicationCode);
                        break;
                }
                //
				InitPage();				
			}
		}
        /*override protected void SetBaseControl()
        {
            base.WorkFlowToolbar1 = this.WorkFlowToolbar1;
            //base.wfcCaseState = this.WorkFlowCaseState1;
            base.ucCheckControl = this.ucCheckControl;
            //base.ucOperationControl = this.ucOperationControl;
        }*/
		/// ****************************************************************************
		/// <summary>
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
			WorkFlowToolbar1.FlowName = "投标单位评审";
			WorkFlowToolbar1.SystemUserCode = this.user.UserCode;
			WorkFlowToolbar1.SourceUrl = "../WorkFlowControl/";
            WorkFlowToolbar1.ProjectCode = Request["ProjectCode"] + "";
			WorkFlowToolbar1.ToolbarDataBind();
			bool tempDeleteFlag = false;
			if(this.WorkFlowToolbar1.GetModuleState("DeleteFlag") == ModuleState.Operable)
				tempDeleteFlag = true;
            if (this.WorkFlowToolbar1.GetModuleState("开始") == ModuleState.Operable)
                tempDeleteFlag = true;
			WorkFlowToolbar1.BtnDeleteVisible = tempDeleteFlag;
			/**************************************************************************************/

			this.BiddingPrejudicationModify1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.BiddingPrejudicationModify1.State = this.WorkFlowToolbar1.GetModuleState("BaseWrite");
			this.BiddingPrejudicationModify1.State1 = this.WorkFlowToolbar1.GetModuleState("SupplierSelect");
			this.BiddingPrejudicationModify1.UserCode = user.UserCode;
            this.BiddingPrejudicationModify1.BiddingCode = Request["BiddingCode"]+"";
			this.BiddingPrejudicationModify1.InitControl();
            

            ModuleState state=this.WorkFlowToolbar1.GetModuleState("金额设置");//世茂功能　添加金额
            //ModuleState state = ModuleState.Operable;
            if (state == ModuleState.Operable)
            {

                this.btnAddPrice.Visible = true;
                this.btnAddPrice.Attributes.Add("onclick", "javascript:OpenFullWindow('biddingmodify.aspx?ApplicationCode=" + this.BiddingPrejudicationModify1.BiddingCode + "','');");
                this.spMoney.Visible = true;
                this.spMoney.InnerHtml = this.BiddingPrejudicationModify1.Money.ToString();

                WorkFlowToolbar1.SaveCasePropertyValue("估计金额", this.BiddingPrejudicationModify1.Money);

            }
            else if (state == ModuleState.Eyeable)
            {
                this.btnAddPrice.Visible = false;
                this.spMoney.Visible = true;
                this.spMoney.InnerHtml = this.BiddingPrejudicationModify1.Money.ToString();
            }
            else
            {
                this.btnAddPrice.Visible = false;
                this.spMoney.Visible = false;
            }

            

			if(this.BiddingPrejudicationModify1.State == ModuleState.Operable)
			{
				this.WorkFlowToolbar1.ScriptCheck = "javascript:if(BiddingPrejudicationCheckSubmit()) ";
			}

			//*** UCBiddingSupplierList(参加资格预审的单位名单) 控件初始化 **************************************************************************
			string BiddingPrejudicationCode = "";

			if(this.BiddingPrejudicationModify1.ApplicationCode == "")
				BiddingPrejudicationCode = this.BiddingPrejudicationModify1.tempCode;
			else
				BiddingPrejudicationCode = this.BiddingPrejudicationModify1.ApplicationCode;

			this.UCBiddingSupplierList1.BiddingPrejudicationCode = BiddingPrejudicationCode;
			this.UCBiddingSupplierList1.CanSelect = this.BiddingPrejudicationModify1.SelectState;
			this.UCBiddingSupplierList1.CanModify = this.BiddingPrejudicationModify1.EditState;
            
						//*****************************************************************************

			//*** UCBiddingSupplierModify 控件初始化 **************************************************************************
			this.UCBiddingSupplierModify1.BiddingPrejudicationCode = BiddingPrejudicationCode;
			this.UCBiddingSupplierModify1.BiddingSupplierCode = "";
			this.UCBiddingSupplierModify1.DoType = "SingleModify";
			this.UCBiddingSupplierModify1.IniControl();
			this.UCBiddingSupplierModify1.Visible = this.BiddingPrejudicationModify1.EditState;
			//*****************************************************************************

			
			/**************************************************************************************/

			/*********************************************************************************
			 * 
			 * 
			  ****************************************************************************
			 
			
			
			/**************************************************************************************
			this.LeavePass1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;//同意按钮组
			this.LeavePass1.State = this.WorkFlowToolbar1.GetModuleState("PassBtn");
			this.LeavePass1.InitControl();
			/**************************************************************************************/
			BiddingPrejudicationModify1.SetAttachList1=this.WorkFlowToolbar1.GetModuleState("附件1");
			BiddingPrejudicationModify1.SetAttachList2=this.WorkFlowToolbar1.GetModuleState("附件2");
			BiddingPrejudicationModify1.SetAttachList3=this.WorkFlowToolbar1.GetModuleState("附件3");



			/**************************************************************************************/
			//流程状态查看
			this.WorkFlowCaseState1.ActCode = this.WorkFlowToolbar1.ActCode;
            this.WorkFlowCaseState1.Toobar = this.WorkFlowToolbar1;
            this.WorkFlowCaseState1.CaseCode = this.WorkFlowToolbar1.CaseCode;
			this.WorkFlowCaseState1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
			this.WorkFlowCaseState1.FlowName = this.WorkFlowToolbar1.FlowName;
			this.WorkFlowCaseState1.UserCode = this.user.UserCode;
			this.WorkFlowCaseState1.Scout = this.WorkFlowToolbar1.Scout;
			this.WorkFlowCaseState1.ControlDataBind();			
			/**************************************************************************************/
			DataGridShowState();
			this.UCBiddingSupplierList1.IniControl();
			this.UCBiddingSupplierList1.LoadData();

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

            OpinionControlInit("推荐意见", "SM_BP_建筑设计部", "Opinion1", base.WorkFlowOpinion1, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BP_工程部", "Opinion2", base.WorkFlowOpinion2, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BP_合约部", "Opinion3", base.WorkFlowOpinion3, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BP_项目总监", "Opinion4", base.WorkFlowOpinion4, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BP_总部总监1", "Opinion5", base.WorkFlowOpinion5, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BP_总部总监2", "Opinion6", base.WorkFlowOpinion6, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BP_董事长1", "Opinion7", base.WorkFlowOpinion7, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            OpinionControlInit("推荐意见", "SM_BP_董事长2", "Opinion8", base.WorkFlowOpinion8, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);
            OpinionControlInit("推荐意见", "SM_BP_董事长3", "Opinion9", base.WorkFlowOpinion9, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);
            OpinionControlInit("推荐意见", "SM_BP_董事长4", "Opinion10", base.WorkFlowOpinion10, ud_sConfirmOpinionList, out ud_sConfirmOpinionList);

            ViewState["_ConfirmOpinionList"] = ud_sConfirmOpinionList;
			
			//会签部门表单初始化
			/**************************************************************************************/
			DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("会签部门");

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
		/// 保存流程属性数据
		/// </summary>
		/// ****************************************************************************
		override protected void WorkFlowPropertySave()
		{
            base.WorkFlowPropertySave();
			if(this.BiddingPrejudicationModify1.State == ModuleState.Operable)
			{
				WorkFlowToolbar1.SaveCasePropertyValue("估计金额",this.BiddingPrejudicationModify1.Money);
				WorkFlowToolbar1.SaveCasePropertyValue("申请人",user.UserCode);
				WorkFlowToolbar1.SaveCasePropertyValue("主题",this.BiddingPrejudicationModify1.BiddingTitle);
				WorkFlowToolbar1.SaveCasePropertyValue("业务类别",BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(this.BiddingPrejudicationModify1.BiddingType));
				WorkFlowToolbar1.SaveCasePropertyValue("主要标段",this.BiddingPrejudicationModify1.mostly);
				WorkFlowToolbar1.SaveCasePropertyValue("用户类别",user.GetOperationType());
				WorkFlowToolbar1.SaveCasePropertyValue("项目部门",BLL.ProjectRule.GetUnitByProject(this.BiddingPrejudicationModify1.ProjectCode));
				WorkFlowToolbar1.SaveCasePropertyValue("项目代码",this.BiddingPrejudicationModify1.ProjectCode);
			}
            
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
                        if (this.WorkFlowToolbar1.GetModuleState("Opinion4") == ModuleState.Operable)
                            WorkFlowToolbar1.SaveCasePropertyValue("用户类别", user.GetOperationType());
                       
						InitPage();
						
						this.DataGridShowState();
						UCBiddingSupplierList1.LoadEditData();
					}
					//发送
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Send)
					{
						
						//this.RegisterStartupScript("","<script>alert('请选择预审通过单位！');</script>");
						if(!this.UCBiddingSupplierList1.SelectedSupplierFlag && this.BiddingPrejudicationModify1.State1 == ModuleState.Operable)
						{
							this.RegisterStartupScript("","<script>alert('请选择预审通过单位！');</script>");
							return;
						}
						try
						{
							DataSubmit(dao);
							//UCBiddingSupplierList1.UpdateDepartMentSelect();
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
						//UCBiddingSupplierList1.UpdateDepartMentSelect();
					}
					//保存
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Save)
					{
						///if(!this.UCBiddingSupplierList1.SelectedSupplierFlag && this.BiddingPrejudicationModify1.State1 == ModuleState.Operable)
						//{
						//	this.RegisterStartupScript("","<script>alert('请选择预审通过单位！');</script>");
						//	return;
						//}
						try
						{
							DataSubmit(dao);
							//UCBiddingSupplierList1.UpdateDepartMentSelect();
						}
						catch(Exception ex)
						{
							Response.Write(Rms.Web.JavaScript.Alert(true,ex.Message));
						}
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
						 if(!this.UCBiddingSupplierList1.SelectedSupplierFlag && this.BiddingPrejudicationModify1.State1 == ModuleState.Operable)
						{
							this.RegisterStartupScript("","<script>alert('请选择预审通过单位！');</script>");
							return;
						}
						DataSubmit(dao);
						WorkFlowToolbar1.TaskFinish();

					}
					//结束
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.Finish)
					{
						if(!this.UCBiddingSupplierList1.SelectedSupplierFlag && this.BiddingPrejudicationModify1.State1 == ModuleState.Operable)
						{
							this.RegisterStartupScript("","<script>alert('请选择预审通过单位！');</script>");
							return;
						}
						DataSubmit(dao);
						WorkFlowToolbar1.Finish();

					}
					//抄送
					if(WorkFlowToolbar1.CommandType == ToolbarCommandType.MakeCopy)
					{
						DataSubmit(dao);
						WorkFlowToolbar1.MakeCopy();

					}
                    //删除
                    if (WorkFlowToolbar1.CommandType == ToolbarCommandType.Delete)
                    {
                        WorkFlowToolbar1.Delete();
                        UCBiddingSupplierList1.DeleteAll(dao);
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

			if(this.BiddingPrejudicationModify1.State == ModuleState.Operable)
			{
				this.BiddingPrejudicationModify1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.BiddingPrejudicationModify1.dao = dao;
				this.BiddingPrejudicationModify1.SubmitData();
				//this.UCBiddingSupplierList1.InsertDepartMent();
				WorkFlowToolbar1.ApplicationCode = this.BiddingPrejudicationModify1.ApplicationCode;
			}

			if(this.BiddingPrejudicationModify1.State1 == ModuleState.Operable)
			{
				this.BiddingPrejudicationModify1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
				this.BiddingPrejudicationModify1.dao = dao;
				this.BiddingPrejudicationModify1.SubmitBiddingState();
			}
			if(this.UCBiddingSupplierList1.CanModify)
			{
				this.UCBiddingSupplierList1.dao = dao;
				this.UCBiddingSupplierList1.ModifyData();
			}
			if(this.UCBiddingSupplierList1.CanSelect)
			{
				this.UCBiddingSupplierList1.dao = dao;				
				this.UCBiddingSupplierList1.SaveData();
			}
			//DataGridShowState();
			UCBiddingSupplierList1.UpdateDepartMentSelect();
			SaveMeetMessage(dao,this.rptMeetSign);
			return base.DataSubmit(dao);			
			//OpinionDataSubmit(dao);
		}
		private void UCBiddingSupplierModify1_SaveData(object sender , System.EventArgs e)
		{
			this.UCBiddingSupplierList1.LoadData();
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
			base.LoadEvent();
			//this.WorkFlowToolbar1.ToolbarCommand += new System.EventHandler(this.WorkFlowToolbar1_ToolbarCommand);
			this.UCBiddingSupplierModify1.SaveDataEvent +=new System.EventHandler(this.UCBiddingSupplierModify1_SaveData);
			this.rptMeetSign.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptMeetSign_ItemDataBound);

		}
		#endregion
		protected void DataGridShowState()
		{
            //this.UCBiddingSupplierList1.State2=this.WorkFlowToolbar1.GetModuleState("ShowAllSelect");
            ////if(WorkFlowToolbar1.)
            //if(this.WorkFlowOpinion1.State == ModuleState.Operable)
            //{
            //    UCBiddingSupplierList1.ShowDgListColumn(BLL.BiddingSystem.DepartMentName.建筑部);
            //    return;
            //}

            //if(this.WorkFlowOpinion2.State == ModuleState.Operable)
            //{
            //    UCBiddingSupplierList1.ShowDgListColumn(BLL.BiddingSystem.DepartMentName.工程部);
            //    return;
            //}

            //if(this.WorkFlowOpinion3.State == ModuleState.Operable)
            //{
            //    UCBiddingSupplierList1.ShowDgListColumn(BLL.BiddingSystem.DepartMentName.合约部);
            //    return;
            //}			
            //if(this.WorkFlowToolbar1.GetModuleState("Option4") == ModuleState.Operable)
            //{
            //    UCBiddingSupplierList1.ShowDgListColumn(BLL.BiddingSystem.DepartMentName.项目总监);
            //    return;
            //}
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
			base.SaveMeetMessage (dao, rptMeetSign);
		}

	}		
}
