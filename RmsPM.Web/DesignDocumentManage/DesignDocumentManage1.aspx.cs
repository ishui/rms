using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;

namespace RmsPM.Web.LeaveManage
{
    public partial class DesignDocumentManage_DesignDocumentManage1 : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                InitPage();
        }
        /// ****************************************************************************
        /// <summary>
        /// 初始化

        /// </summary>
        /// ****************************************************************************
        private void InitPage()
        {
            string actCode = Request["ActCode"] + "";
            string CaseCode = Request["CaseCode"] + "";

            if (Request["frameType"] != null)//判断是否为流程监控状态

            {
                if (Request["frameType"].ToString() == "List")
                {
                    /*if(!user.HasOperationRight("130105"))
                    {
                        Response.Redirect( "../RejectAccess.aspx" );
                        Response.End();
                    }*/
                    WorkFlowToolbar1.Scout = true;

                }
            }

            if (Request["ApplicationCode"] != null)
                WorkFlowToolbar1.ApplicationCode = Request["ApplicationCode"].ToString();

            /**************************************************************************************/
            WorkFlowToolbar1.ActCode = actCode;//工具栏设置

            WorkFlowToolbar1.CaseCode = CaseCode;

            WorkFlowToolbar1.FlowName = "方案设计评审";
            WorkFlowToolbar1.SystemUserCode = this.user.UserCode;
            WorkFlowToolbar1.SourceUrl = "../WorkFlowControl/";
            WorkFlowToolbar1.ToolbarDataBind();
            if (this.WorkFlowToolbar1.GetModuleState("Delete") == ModuleState.Operable)
            {
                this.WorkFlowToolbar1.BtnDeleteVisible = true;
            }
            else
            {
                this.WorkFlowToolbar1.BtnDeleteVisible = false;
            }
            /**************************************************************************************/

            /**************************************************************************************/
            this.DesignDocument1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
            this.DesignDocument1.State = this.WorkFlowToolbar1.GetModuleState("业务填写");
            this.DesignDocument1.ProjectCode = this.project.ProjectCode;
            this.DesignDocument1.UserCode = user.UserCode;
            if(this.WorkFlowToolbar1.ApplicationCode == "")
                this.DesignDocument1.ApplicationCode = Request["DesignDocumentCode"] + "";
            this.DesignDocument1.InitControl();
            /**************************************************************************************/

            /**************************************************************************************/
            this.LoadOpinionControl(this.Workflowformopinion1, "投资发展部经理意见：", "YR_FASJ_投资发展部经理", this.WorkFlowToolbar1.ApplicationCode, "投资发展部经理");
            this.LoadOpinionControl(this.Workflowformopinion2, "工程技术部会签意见：", "YR_FASJ_工程技术部", this.WorkFlowToolbar1.ApplicationCode, "工程技术部");
            this.LoadOpinionControl(this.Workflowformopinion3, "物资采购部会签意见：", "YR_FASJ_物资采购部", this.WorkFlowToolbar1.ApplicationCode, "物资采购部");
            this.LoadOpinionControl(this.Workflowformopinion4, "财务部会签意见：", "YR_FASJ_财务部", this.WorkFlowToolbar1.ApplicationCode, "财务部");
            this.LoadOpinionControl(this.Workflowformopinion5, "销售部会签意见：", "YR_FASJ_销售部", this.WorkFlowToolbar1.ApplicationCode, "销售部");
            this.LoadOpinionControl(this.Workflowformopinion6, "总工程师意见：", "YR_FASJ_总工程师", this.WorkFlowToolbar1.ApplicationCode, "总工程师");
            this.LoadOpinionControl(this.Workflowformopinion7, "常务副总意见：", "YR_FASJ_常务副总", this.WorkFlowToolbar1.ApplicationCode, "常务副总");
            this.LoadOpinionControl(this.Workflowformopinion8, "总经理意见：", "YR_FASJ_总经理", this.WorkFlowToolbar1.ApplicationCode, "总经理");
            /**************************************************************************************/

            /**************************************************************************************/
            this.WorkFlowCaseState1.ActCode = this.WorkFlowToolbar1.ActCode;
            this.WorkFlowCaseState1.CaseCode = this.WorkFlowToolbar1.CaseCode;
            this.WorkFlowCaseState1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;
            this.WorkFlowCaseState1.FlowName = this.WorkFlowToolbar1.FlowName;
            this.WorkFlowCaseState1.UserCode = this.user.UserCode;
            this.WorkFlowCaseState1.Scout = this.WorkFlowToolbar1.Scout;
            this.WorkFlowCaseState1.ControlDataBind();
            /**************************************************************************************/
        }
        /// ****************************************************************************
        /// <summary>
        /// 意见输入框加载

        /// </summary>
        /// <param name="OpinionCase">输入框</param>
        /// <param name="Title">标题</param>
        /// <param name="Type">类型</param>
        /// <param name="ApplicationCode">业务代码</param>
        /// <param name="StateName">状态配置名称</param>
        /// ****************************************************************************
        private void LoadOpinionControl(WorkFlowFormOpinion OpinionCase,string Title,string Type,string ApplicationCode,string StateName)
        {
            OpinionCase.Title = Title;
            OpinionCase.OpinionType = Type;
            OpinionCase.ApplicationCode = ApplicationCode;
            OpinionCase.CaseCode = this.WorkFlowToolbar1.CaseCode;
            OpinionCase.State = this.WorkFlowToolbar1.GetModuleState(StateName);
            if (this.WorkFlowToolbar1.GetModuleState(StateName, 1) == ModuleState.Operable)
            {
                OpinionCase.StateConfirm = ModuleState.Operable;
                this.ViewState["ConfirmControl"] = OpinionCase.ID;
            }
            OpinionCase.InitControl();
        }
        /// ****************************************************************************
        /// <summary>
        /// 保存流程属性数据

        /// </summary>
        /// ****************************************************************************
        private void WorkFlowPropertySave()
        {
            if (this.DesignDocument1.State == ModuleState.Operable)
            {
                WorkFlowToolbar1.SaveCasePropertyValue("申请部门", this.DesignDocument1.Unit);
                WorkFlowToolbar1.SaveCasePropertyValue("申请人", user.UserCode);
                WorkFlowToolbar1.SaveCasePropertyValue("主题", this.DesignDocument1.Title);
                WorkFlowToolbar1.SaveCasePropertyValue("项目代码", this.DesignDocument1.ProjectCode);
                WorkFlowToolbar1.SaveCasePropertyValue("项目部门", RmsPM.BLL.ProjectRule.GetUnitByProject(this.DesignDocument1.ProjectCode));
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 工具栏事件

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void WorkFlowToolbar1_ToolbarCommand(object sender, System.EventArgs e)
        {
            /****************************************************************************/
            using (StandardEntityDAO dao = new StandardEntityDAO("DesignDocument"))
            {
                dao.BeginTrans();
                try
                {
                    /***********************************************************/
                    //签收
                    if (WorkFlowToolbar1.CommandType == ToolbarCommandType.SignIn)
                    {
                        WorkFlowToolbar1.SignIn(dao);
                        InitPage();

                    }
                    //发送

                    if (WorkFlowToolbar1.CommandType == ToolbarCommandType.Send)
                    {
                        DataSubmit(dao);
                        WorkFlowToolbar1.Send();
                    }
                    //保存
                    if (WorkFlowToolbar1.CommandType == ToolbarCommandType.Save)
                    {
                        DataSubmit(dao);
                        WorkFlowToolbar1.Save();
                        this.WorkFlowCaseState1.SubmitData();
                        WorkFlowPropertySave();
                        if (!this.WorkFlowToolbar1.IsNew)
                        {
                            Response.Write(Rms.Web.JavaScript.ScriptStart);
                            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                            Response.Write(Rms.Web.JavaScript.WinClose(false));
                            Response.Write(Rms.Web.JavaScript.ScriptEnd);
                        }
                    }
                    //完成
                    if (WorkFlowToolbar1.CommandType == ToolbarCommandType.TaskFinish)
                    {
                        DataSubmit(dao);
                        WorkFlowToolbar1.TaskFinish();
                    }
                    //结束
                    if (WorkFlowToolbar1.CommandType == ToolbarCommandType.Finish)
                    {
                        DataSubmit(dao);
                        WorkFlowToolbar1.Finish();

                    }
                    //抄送

                    if (WorkFlowToolbar1.CommandType == ToolbarCommandType.MakeCopy)
                    {
                        DataSubmit(dao);
                        WorkFlowToolbar1.MakeCopy();

                    }
                    /*******************************************************/

                    //退回

                    if (this.WorkFlowToolbar1.CommandType == ToolbarCommandType.Back)
                    {
                        DataSubmit(dao);
                        this.WorkFlowToolbar1.Back();
                    }
                    //送经办人
                    if (this.WorkFlowToolbar1.CommandType == ToolbarCommandType.BackTop)
                    {
                        DataSubmit(dao);
                        this.WorkFlowToolbar1.BackTop();
                    }
                    //收回
                    if (this.WorkFlowToolbar1.CommandType == ToolbarCommandType.Return)
                    {
                        DataSubmit(dao);
                        this.WorkFlowToolbar1.Return();
                    }
                    //保存意见
                    if (this.WorkFlowToolbar1.CommandType == ToolbarCommandType.Opinion)
                    {
                        DataSubmit(dao);
                        this.WorkFlowToolbar1.SaveOpinion();
                        this.WorkFlowCaseState1.ControlDataBind();
                    }
                    dao.CommitTrans();
                }
                catch (Exception ex)
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
        private void DataSubmit(StandardEntityDAO dao)
        {
            this.WorkFlowCaseState1.SubmitData();

            if (this.DesignDocument1.State == ModuleState.Operable)
            {
                //this.DesignDocument1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                this.DesignDocument1.dao = dao;
                this.DesignDocument1.DocumentState = "f1";
                this.DesignDocument1.SubmitData();
                WorkFlowToolbar1.ApplicationCode = this.DesignDocument1.ApplicationCode;
            }
            if (this.ViewState["ConfirmControl"] != null)
            {
                string OpinionConfirm = ((WorkFlowFormOpinion)this.Page.FindControl(this.ViewState["ConfirmControl"].ToString())).OpinionConfirm;
                if ( OpinionConfirm == "Approve")
                {
                    this.DesignDocument1.dao = dao;
                    this.DesignDocument1.ConfirmData(true,"f");
                 }
                else if (OpinionConfirm == "Reject")
                {
                    this.DesignDocument1.dao = dao;
                    this.DesignDocument1.ConfirmData(false, "f");
                 }
            }

            if (this.Workflowformopinion1.State == ModuleState.Operable)
            {
                this.Workflowformopinion1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                this.Workflowformopinion1.CaseCode = WorkFlowToolbar1.CaseCode;
                this.Workflowformopinion1.dao = dao;
                this.Workflowformopinion1.SubmitData();
            }

            if (this.Workflowformopinion2.State == ModuleState.Operable)
            {
                this.Workflowformopinion2.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                this.Workflowformopinion2.CaseCode = WorkFlowToolbar1.CaseCode;
                this.Workflowformopinion2.dao = dao;
                this.Workflowformopinion2.SubmitData();
            }

            if (this.Workflowformopinion3.State == ModuleState.Operable)
            {
                this.Workflowformopinion3.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                this.Workflowformopinion3.CaseCode = WorkFlowToolbar1.CaseCode;
                this.Workflowformopinion3.dao = dao;
                this.Workflowformopinion3.SubmitData();
            }

            if (this.Workflowformopinion4.State == ModuleState.Operable)
            {
                this.Workflowformopinion4.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                this.Workflowformopinion4.CaseCode = WorkFlowToolbar1.CaseCode;
                this.Workflowformopinion4.dao = dao;
                this.Workflowformopinion4.SubmitData();
            }
            if (this.Workflowformopinion5.State == ModuleState.Operable)
            {
                this.Workflowformopinion5.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                this.Workflowformopinion5.CaseCode = WorkFlowToolbar1.CaseCode;
                this.Workflowformopinion5.dao = dao;
                this.Workflowformopinion5.SubmitData();
            }
            if (this.Workflowformopinion6.State == ModuleState.Operable)
            {
                this.Workflowformopinion6.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                this.Workflowformopinion6.CaseCode = WorkFlowToolbar1.CaseCode;
                this.Workflowformopinion6.dao = dao;
                this.Workflowformopinion6.SubmitData();
            }
            if (this.Workflowformopinion7.State == ModuleState.Operable)
            {
                this.Workflowformopinion7.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                this.Workflowformopinion7.CaseCode = WorkFlowToolbar1.CaseCode;
                this.Workflowformopinion7.dao = dao;
                this.Workflowformopinion7.SubmitData();
            }
            if (this.Workflowformopinion8.State == ModuleState.Operable)
            {
                this.Workflowformopinion8.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
                this.Workflowformopinion8.CaseCode = WorkFlowToolbar1.CaseCode;
                this.Workflowformopinion8.dao = dao;
                this.Workflowformopinion8.SubmitData();
            }
        }
    }
}

