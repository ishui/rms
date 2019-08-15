using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RmsPM.Web;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;
using RmsPM.Web;

/// <summary>
/// WorkFlowPageBase 的摘要说明（新版）
/// </summary>
public class WorkFlowPageBase : PageBase
{
    public WorkFlowPageBase()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region 页面控件
    protected RmsPM.Web.WorkFlowControl.WorkFlowToolbar up_wftToolbar
    {
        get { return (RmsPM.Web.WorkFlowControl.WorkFlowToolbar)this.FindControl("wftToolbar"); }
    }

    protected RmsPM.Web.WorkFlowControl.WorkFlowCaseState up_wfcCaseState
    {
        get { return (RmsPM.Web.WorkFlowControl.WorkFlowCaseState)this.FindControl("wfcCaseState"); }
    }

    protected WorkFlowOperationBase up_ucOperationControl
    {
        get { return (WorkFlowOperationBase)this.FindControl("ucOperationControl"); }
    }

    protected WorkFlowFormOpinion up_wfoOpinion1
    {
        get { return (WorkFlowFormOpinion)this.FindControl("wfoOpinion1"); }
    }



    protected GridView up_gvSignSheet
    {
        get { return (GridView)this.FindControl("gvSignSheet"); }
    }

    protected HtmlTableRow up_trProposerSheet
    {
        get { return (HtmlTableRow)this.FindControl("trProposerSheet"); }
    }

    protected HtmlTableRow up_trSignSheet
    {
        get { return (HtmlTableRow)this.FindControl("trSignSheet"); }
    }

    #endregion 页面控件

    #region 绑定工具栏事件(重写父类)
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
        //
        base.OnInit(e);
    }

    override protected void InitEventHandler()
    {
        this.up_wftToolbar.ToolbarCommand += new System.EventHandler(WorkFlowToolbar_ToolbarCommand);

        if (this.up_gvSignSheet != null)
        {
            this.up_gvSignSheet.RowDataBound += new GridViewRowEventHandler(gvSignSheet_DataBound);
        }

    }
    #endregion

    #region --- 属性集合 ---
    /// <summary>
    /// 数据对象名
    /// </summary>
    public string EntityName
    {
        get
        {
            if (this.ViewState["_EntityName"] != null)
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
            if (this.ViewState["_WorkFlowName"] != null)
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
            if (this.ViewState["_OpinionCount"] != null)
                return (int)this.ViewState["_OpinionCount"];
            return 0;
        }
        set
        {
            ViewState["_OpinionCount"] = value;
        }
    }

    /// <summary>
    /// 流程意见控件确认列表
    /// </summary>
    public string ConfirmOpinionList
    {
        get
        {
            if (this.ViewState["_ConfirmOpinionList"] != null)
                return this.ViewState["_ConfirmOpinionList"].ToString();
            return "";
        }
        set
        {
            ViewState["_ConfirmOpinionList"] = value;
        }
    }

    /// <summary>
    /// 是否自动生成流程意见控件
    /// </summary>
    public bool AutoBuild
    {
        get
        {
            if (this.ViewState["_AutoBuild"] != null)
            {
                if (this.ViewState["_AutoBuild"].ToString().ToLower() == "true")
                {
                    return true;
                }
            }
            return false;
        }
        set
        {
            ViewState["_AutoBuild"] = value.ToString();

            //if (value)
            //{
            //    this.OpinionCount = 1;
            //}
        }
    }

    public string OperationStateName
    {
        get
        {
            if (this.ViewState["_OperationStateName"] != null)
            {
                return this.ViewState["_OperationStateName"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            ViewState["_OperationStateName"] = value.ToString();
        }
    }

    public string MoneyStateName
    {
        get
        {
            if (this.ViewState["_MoneyStateName"] != null)
            {
                return this.ViewState["_MoneyStateName"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            ViewState["_MoneyStateName"] = value.ToString();
        }
    }


    public string AttachmentStateName
    {
        get
        {
            if (this.ViewState["_AttachmentStateName"] != null)
            {
                return this.ViewState["_AttachmentStateName"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            ViewState["_AttachmentStateName"] = value.ToString();
        }
    }

    public string OperationCode
    {
        get
        {
            if (this.ViewState["_OperationCode"] != null)
            {
                return this.ViewState["_OperationCode"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            ViewState["_OperationCode"] = value.ToString();
        }
    }


    public string OpinionPrefix
    {
        get
        {
            if (this.ViewState["_OpinionPrefix"] != null)
            {
                return this.ViewState["_OpinionPrefix"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            ViewState["_OpinionPrefix"] = value.ToString();
        }
    }

    public bool ProposerSheet
    {
        get
        {
            return this.up_trProposerSheet.Visible;
        }
        set
        {
            this.up_trProposerSheet.Visible = value;
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
    virtual protected void WorkFlowToolbar_ToolbarCommand(object sender, System.EventArgs e)
    {
        using (StandardEntityDAO dao = new StandardEntityDAO(this.EntityName))
        {
            dao.BeginTrans();
            try
            {
                //签收
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.SignIn)
                {
                    ToolBarSignIn(dao);
                }
                //发送
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.Send)
                {
                    ToolBarSend(dao);
                }
                //退回
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.Back)
                {
                    ToolBarBack(dao);
                }
                //送经办人
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.BackTop)
                {
                    ToolBarBackTop(dao);
                }
                //收回
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.Return)
                {
                    ToolBarReturn(dao);
                }
                //保存意见
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.Opinion)
                {
                    ToolBarSaveOpinion(dao);
                }
                //保存
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.Save)
                {
                    ToolBarSave(dao);

                }
                //完成
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.TaskFinish)
                {
                    ToolBarTaskFinish(dao);
                }
                //结束
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.Finish)
                {
                    ToolBarFinish(dao);
                }
                //抄送
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.MakeCopy)
                {
                    ToolBarMakeCopy(dao);
                }
                //删除
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.Delete)
                {
                    ToolBarDelete(dao);
                }
                //作废
                if(this.up_wftToolbar.CommandType == ToolbarCommandType.BlankOut)
                {
                    ToolBarBlankOut(dao);
                }
                //表单意见保存
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.OpinionForward)
                {
                    ToolBarOpinionForward(dao);
                }
                dao.CommitTrans();
            }
            catch (Exception ex)
            {
                dao.RollBackTrans();
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
                throw ex;
            }
        }
    }

    /// <summary>
    /// 签收
    /// </summary>
    virtual protected void ToolBarSignIn(StandardEntityDAO dao)
    {
        this.up_wftToolbar.SignIn(dao);
        if (this.up_ucOperationControl.State == ModuleState.Operable)
        {
            this.up_ucOperationControl.RestoreStatus();
        }
        this.InitPage();
    }

    /// <summary>
    /// 发送
    /// </summary>
    virtual protected void ToolBarSend(StandardEntityDAO dao)
    {
        if (DataSubmit(dao, true) && OpinionDataSubmit(dao))
        {
            this.up_wftToolbar.Send();
            if (this.up_ucOperationControl.State == ModuleState.Operable)
            {
                this.up_ucOperationControl.ChangeStatusWhenSend(dao);
            }
            WorkFlowPropertySave();
            this.up_wfcCaseState.SubmitData();
        }
        else
        {
            dao.RollBackTrans();
            this.up_wftToolbar.IsNew = false;
        }
    }

    /// <summary>
    /// 退回
    /// </summary>
    virtual protected void ToolBarBack(StandardEntityDAO dao)
    {
        if (DataSubmit(dao) && OpinionDataSubmit(dao))
        {
            this.up_wftToolbar.Back();
        }
        else
        {
            dao.RollBackTrans();
            this.up_wftToolbar.IsNew = false;
        }
    }

    /// <summary>
    /// 退返经办人
    /// </summary>
    virtual protected void ToolBarBackTop(StandardEntityDAO dao)
    {
        if (DataSubmit(dao) && OpinionDataSubmit(dao))
        {
            this.up_wftToolbar.BackTop();
        }
        else
        {
            dao.RollBackTrans();
            this.up_wftToolbar.IsNew = false;
        }
    }

    /// <summary>
    /// 退返经办人
    /// </summary>
    virtual protected void ToolBarReturn(StandardEntityDAO dao)
    {
        this.up_wftToolbar.Return();
    }

    /// <summary>
    /// 保存意见
    /// </summary>
    virtual protected void ToolBarSaveOpinion(StandardEntityDAO dao)
    {

        this.up_wftToolbar.SaveOpinion();
        this.OpinionDataSubmit(dao);
        this.up_wfcCaseState.Toobar = this.up_wftToolbar;
        this.up_wfcCaseState.ControlDataBind();
    }

    /// <summary>
    /// 表单意见保存
    /// </summary>
    virtual protected void ToolBarOpinionForward(StandardEntityDAO dao)
    {
        this.OpinionDataSubmit(dao,true);
        this.up_wftToolbar.SaveOpinion();
        this.up_wfcCaseState.Toobar = this.up_wftToolbar;
        this.up_wfcCaseState.ControlDataBind();
    }

    
    /// <summary>
    /// 保存
    /// </summary>
    virtual protected void ToolBarSave(StandardEntityDAO dao)
    {

        if (DataSubmit(dao))
        {
            this.up_wftToolbar.Save();
            OpinionDataSubmit(dao,true);
            this.up_wftToolbar.SaveOpinion();
            WorkFlowPropertySave();
            this.up_wfcCaseState.SubmitData();
            if (!this.up_wftToolbar.IsNew)
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
            this.up_wftToolbar.IsNew = false;
        }
    }

    /// <summary>
    /// 完成
    /// </summary>
    virtual protected void ToolBarTaskFinish(StandardEntityDAO dao)
    {
        if (DataSubmit(dao) && OpinionDataSubmit(dao,true))
        {
            this.up_wftToolbar.TaskFinish();
            this.up_wftToolbar.SaveOpinion();
            this.up_wfcCaseState.SubmitData();
        }
        else
        {
            dao.RollBackTrans();
            this.up_wftToolbar.IsNew = false;
        }
    }

    /// <summary>
    /// 结束
    /// </summary>
    virtual protected void ToolBarFinish(StandardEntityDAO dao)
    {
        if (OpinionDataSubmit(dao, true) && DataSubmit(dao, true))
        {
            this.up_wftToolbar.Finish();
            this.up_wftToolbar.SaveOpinion();
            this.up_wfcCaseState.SubmitData();
        }
        else
        {
            dao.RollBackTrans();
            this.up_wftToolbar.IsNew = false;
        }
    }
    /// <summary>
    /// 作废
    /// </summary>
    virtual protected void ToolBarBlankOut(StandardEntityDAO dao)
    {
        if (OpinionDataSubmit(dao, true) && DataSubmit(dao, true) && this.up_ucOperationControl.BlankOut(dao))
        {
            this.up_wftToolbar.BlankOut();
            this.up_wftToolbar.SaveOpinion();
            this.up_wfcCaseState.SubmitData();
        }
        else
        {
            dao.RollBackTrans();
            this.up_wftToolbar.IsNew = false;
        }
    }

    /// <summary>
    /// 抄送
    /// </summary>
    virtual protected void ToolBarMakeCopy(StandardEntityDAO dao)
    {
        if (DataSubmit(dao) && OpinionDataSubmit(dao))
        {
            this.up_wftToolbar.MakeCopy();
            WorkFlowPropertySave();
        }
        else
        {
            dao.RollBackTrans();
            this.up_wftToolbar.IsNew = false;
        }
    }
    /// <summary>
    /// 流程删除
    /// </summary>
    /// <param name="dao">流程删除</param>
    virtual protected void ToolBarDelete(StandardEntityDAO dao)
    {
        if (this.up_ucOperationControl.Delete(dao))
        {
            this.up_wftToolbar.Delete();
        }
        else
        {
            dao.RollBackTrans();
            this.up_wftToolbar.IsNew = false;
        }
    }

    #endregion

    #region 流程控件初始化

    virtual protected void InitPage()
    {
        WorkFlowInit();
        OperationControlInit();
        PageControlInit();
    }

    /// <summary>
    /// 流程工具栏设置
    /// </summary>
    virtual protected void WorkFlowInit()
    {
        string actCode = Request["ActCode"] + "";
        string CaseCode = Request["CaseCode"] + "";

        if (Request["frameType"] != null)//判断是否为流程监控状态
        {
            if (Request["frameType"].ToString() == "List")
            {
                this.up_wftToolbar.Scout = true;
            }
        }

        if ( Request["ApplicationCode"] != null )
        {
            this.up_wftToolbar.ApplicationCode = Request["ApplicationCode"].ToString();
        }

        /**************************************************************************************/
        this.up_wftToolbar.ActCode = actCode;//工具栏设置
        this.up_wftToolbar.CaseCode = CaseCode;
        this.up_wftToolbar.FlowName = this.WorkFlowName;
        this.up_wftToolbar.SystemUserCode = this.user.UserCode;
        this.up_wftToolbar.SourceUrl = "../WorkFlowControl/";
        string tempProjectCode = Request["ProjectCode"] + "";
        if (tempProjectCode != "")
            this.up_wftToolbar.ProjectCode = tempProjectCode;
        else
            this.up_wftToolbar.ProjectCode = RmsPM.BLL.WorkFlowRule.GetWorkFlowPropertyValuebyName(CaseCode,"项目代码");
      

        this.up_wftToolbar.ToolbarDataBind();


        if (this.up_wftToolbar.GetModuleState("Delete") == ModuleState.Operable)
        {
            this.up_wftToolbar.BtnDeleteVisible = true;
        }
        else
        {
            this.up_wftToolbar.BtnDeleteVisible = false;
        }
        if (this.up_wftToolbar.GetModuleState("Print") == ModuleState.Operable)
        {
            this.up_wftToolbar.BtnPrintVisible = true;
        }
        else if (this.up_wftToolbar.UsePrint)
        {
            //增加判断节点
        }
        else
        {
            this.up_wftToolbar.BtnPrintVisible = false;
        }
        
        if (this.up_wftToolbar.GetModuleState("BlankOut") == ModuleState.Operable)
        {
            this.up_wftToolbar.BtnBlankOutVisible = true;
        }
        else
        {
            this.up_wftToolbar.BtnBlankOutVisible = false;
        }

        //流程状态查看
        this.up_wfcCaseState.ProjectCode = this.up_wftToolbar.ProjectCode;
        this.up_wfcCaseState.IsScoutPopedom = this.user.HasRight("090102");//是否拥有流程监控权限
        this.up_wfcCaseState.ActCode = this.up_wftToolbar.ActCode;
        this.up_wfcCaseState.Toobar = this.up_wftToolbar;
        this.up_wfcCaseState.CaseCode = this.up_wftToolbar.CaseCode;
        this.up_wfcCaseState.ApplicationCode = this.up_wftToolbar.ApplicationCode;
        this.up_wfcCaseState.FlowName = this.up_wftToolbar.FlowName;
        this.up_wfcCaseState.UserCode = this.user.UserCode;
        this.up_wfcCaseState.Scout = this.up_wftToolbar.Scout;
        this.up_wfcCaseState.ControlDataBind();
    }

    /// ****************************************************************************
    /// <summary>
    /// 保存流程属性数据
    /// </summary>
    /// ****************************************************************************
    virtual protected void WorkFlowPropertySave()
    {
        try
        {
            if (this.up_wftToolbar.IsNew || this.up_ucOperationControl.State == ModuleState.Operable)
            {
                this.up_wftToolbar.SaveCasePropertyValue("主题", this.up_ucOperationControl.ApplicationTitle);
                this.up_wftToolbar.SaveCasePropertyValue("申请人", this.user.UserCode);
                this.up_wftToolbar.SaveCasePropertyValue("项目代码", this.up_ucOperationControl.ProjectCode);
                this.up_wftToolbar.SaveCasePropertyValue("项目部门", RmsPM.BLL.ProjectRule.GetUnitByProject(this.up_ucOperationControl.ProjectCode));
                this.up_wftToolbar.SaveCasePropertyValue("业务类别", this.up_ucOperationControl.ApplicationType);
                this.up_wftToolbar.SaveCasePropertyValue("业务部门", this.up_ucOperationControl.UnitCode);

                if (this.AutoBuild)
                {
                    this.up_wftToolbar.SaveCasePropertyValue("表单部门", "");
                }
             
                WorkFlowOperationPropertySave();

            }
            if (this.up_ucOperationControl.State == ModuleState.Operable)
            {
                this.up_wftToolbar.SaveCasePropertyValue("工作缓急", this.up_wftToolbar.RateValue);
            }

            if (this.up_wftToolbar.IsNew)
            {
                string NumberString = RmsPM.BLL.SystemRule.GetProjectConfigValue(this.up_ucOperationControl.ProjectCode, "FlowNumber")
                    + RmsPM.BLL.WorkFlowRule.GetProcedureNumberByName(this.up_wftToolbar.FlowName) + DateTime.Now.Year.ToString().Substring(2, 2);
                int FlowNumberLenth = (RmsPM.BLL.SystemRule.GetProjectConfigValue("FlowNumberLength") == "") ? 4 : int.Parse(RmsPM.BLL.SystemRule.GetProjectConfigValue("FlowNumberLength"));
                NumberString += RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode(NumberString).Substring(6 - FlowNumberLenth, FlowNumberLenth);
                //string NumberString = "";
                //string name = RmsPM.BLL.WorkFlowRule.GetProcedureCodeByName(this.up_wftToolbar.FlowName, this.up_ucOperationControl.ProjectCode) + RmsPM.BLL.SystemRule.GetProjectConfigValue(this.up_ucOperationControl.ProjectCode, "FlowNumber");
                //NumberString = RmsPM.DAL.EntityDAO.SystemManageDAO.GetFormatSysCode(name, RmsPM.BLL.WorkFlowRule.GetProcedureNumberByName(this.up_wftToolbar.FlowName));

                this.up_wftToolbar.SaveCasePropertyValue("流水号", NumberString);
            }

            if (this.up_wftToolbar.GetModuleState("Hand") == ModuleState.Operable)
            {
                this.up_wftToolbar.SaveCasePropertyValue("手送资料", this.up_wftToolbar.HandMadeValue);
            }

            if (this.AutoBuild)
            {
                if (this.up_wftToolbar.CommandType == ToolbarCommandType.Send && this.up_wftToolbar.GetModuleState("NoRegister") != ModuleState.Operable )
                {
                    DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("表单部门");

                    DataTable ud_dtNewSendItems = GetSendItemsByString(this.up_wftToolbar.SendRoleItems);

                    foreach (DataRow ud_drNew in ud_dtNewSendItems.Rows)
                    {
                        bool ud_bAlreadyAdd = false;

                        foreach (DataRow dr in ud_dtSendItems.Select(string.Format("RoleName='{0}'", ud_drNew["RoleName"].ToString())))
                        {
                            dr["UserCode"] = dr["UserCode"].ToString() + "." + ud_drNew["UserCode"].ToString();
                            dr["UserName"] = dr["UserName"].ToString() + "." + ud_drNew["UserName"].ToString();

                            ud_bAlreadyAdd = true;
                        }

                        if (!ud_bAlreadyAdd)
                        {
                            ud_dtSendItems.ImportRow(ud_drNew);
                        }

                    }


                    string ud_sSendRoleItems = GetStringBySendItems(ud_dtSendItems);

                    this.up_wftToolbar.SaveCasePropertyValue("表单部门", ud_sSendRoleItems);
                }

            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            throw ex;
        }
    }

    /// ****************************************************************************
    /// <summary>
    /// 保存业务属性数据
    /// </summary>
    /// ****************************************************************************
    virtual protected void WorkFlowOperationPropertySave()
    {
        try
        {
            if (this.up_ucOperationControl.OperationProperty != null)
            {
                foreach (DataRow ud_drProperty in this.up_ucOperationControl.OperationProperty.Rows)
                {
                    string ud_sPropertyName = ud_drProperty["PropertyName"].ToString();
                    string ud_sPropertyValue = ud_drProperty["PropertyValue"].ToString();

                    this.up_wftToolbar.SaveCasePropertyValue(ud_sPropertyName, ud_sPropertyValue);
                }
            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            throw ex;
        }
    }

    /// ****************************************************************************
    /// <summary>
    /// 页面控件初始化
    /// </summary>
    /// ****************************************************************************
    virtual protected void OperationControlInit()
    {
        if (this.AutoBuild)
        {
            //业务表单初始化

            /**************************************************************************************/
            this.up_ucOperationControl.ApplicationCode = this.up_wftToolbar.ApplicationCode;
            this.up_ucOperationControl.State = this.up_wftToolbar.GetModuleState(this.OperationStateName);
            this.up_ucOperationControl.UserCode = this.user.UserCode;
            this.up_ucOperationControl.OperationCode = this.OperationCode;
            this.up_ucOperationControl.ucWorkFlowToolbar = this.up_wftToolbar;

            if (this.MoneyStateName != "")
            {
                this.up_ucOperationControl.MoneyState = GetModuleStateByName(this.MoneyStateName);
            }

            if (this.AttachmentStateName != "")
            {
                this.up_ucOperationControl.AttachmentState = GetModuleStateByName(this.AttachmentStateName);
            }

            this.up_ucOperationControl.InitControl();
            /**************************************************************************************/
        }
    }

    /// ****************************************************************************
    /// <summary>
    /// 通过字符串获取辅助控制状态（与状态同名的字符串，返回该状态）
    /// </summary>
    /// ****************************************************************************
    protected ModuleState GetModuleStateByName(string pm_sModuleStateName)
    {
        return GetModuleStateByName(pm_sModuleStateName, 0);
    }

    protected ModuleState GetModuleStateByName(string pm_sModuleStateName,int i)
    {
        ModuleState ud_ModuleState = ModuleState.Unbeknown;

        switch (pm_sModuleStateName)
        {
            case "Begin":
                ud_ModuleState = ModuleState.Begin;
                break;
            case "Condition":
                ud_ModuleState = ModuleState.Condition;
                break;
            case "End":
                ud_ModuleState = ModuleState.End;
                break;
            case "Eyeable":
                ud_ModuleState = ModuleState.Eyeable;
                break;
            case "Operable":
                ud_ModuleState = ModuleState.Operable;
                break;
            case "Other":
                ud_ModuleState = ModuleState.Other;
                break;
            case "Sightless":
                ud_ModuleState = ModuleState.Sightless;
                break;
            case "Unbeknown":
                ud_ModuleState = ModuleState.Unbeknown;
                break;
            default:
                ud_ModuleState = this.up_wftToolbar.GetModuleState(pm_sModuleStateName, i);
                break;
        }

        return ud_ModuleState;
    }
    /// ****************************************************************************
    /// <summary>
    /// 页面表单意见控件初始化
    /// </summary>
    /// ****************************************************************************
    virtual protected void PageControlInit()
    {
        if (this.AutoBuild)
        {
            //意见表单初始化

            /**************************************************************************************/
            OpinionControlInit("意见", this.OpinionPrefix + "申请人意见", this.OperationStateName, this.up_wfoOpinion1);

            //会签部门表单初始化

            /**************************************************************************************/
            DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("表单部门");

            if (ud_dtSendItems.Rows.Count > 0)
            {
                this.up_trSignSheet.Visible = true;
                this.up_gvSignSheet.DataSource = ud_dtSendItems;
                this.up_gvSignSheet.DataBind();
            }
            else
            {
                this.up_trSignSheet.Visible = false;
            }
            /**************************************************************************************/
        }

    }

    /// ****************************************************************************
    /// <summary>
    /// 流程意见控件初始化
    /// </summary>
    /// ****************************************************************************
    protected ModuleState[] OpinionControlInit(string pm_sOpinionTitle, string pm_sOpinionType, string pm_sModuleName,
        WorkFlowFormOpinion pm_wfoOpinion)
    {
        return OpinionControlInit(pm_sOpinionTitle, pm_sOpinionType, pm_sModuleName, pm_wfoOpinion, "", "");
    }

    protected ModuleState[] OpinionControlInit(string pm_sOpinionType, string pm_sModuleName,
        WorkFlowFormOpinion pm_wfoOpinion)
    {
        return OpinionControlInit("", pm_sOpinionType, pm_sModuleName, pm_wfoOpinion, "", "");
    }

    protected ModuleState[] OpinionControlInit(string pm_sOpinionTitle, string pm_sOpinionType, string pm_sModuleName,
        WorkFlowFormOpinion pm_wfoOpinion, string pm_sUserCode)
    {
        return OpinionControlInit(pm_sOpinionTitle, pm_sOpinionType, pm_sModuleName, pm_wfoOpinion, pm_sUserCode,"");
    }

    protected ModuleState[] OpinionControlInit(string pm_sOpinionType, string pm_sModuleName,
        WorkFlowFormOpinion pm_wfoOpinion, string pm_sUserCode)
    {
        return OpinionControlInit("", pm_sOpinionType, pm_sModuleName, pm_wfoOpinion, pm_sUserCode,"");
    }

    protected ModuleState[] OpinionControlInit(string pm_sOpinionTitle, string pm_sOpinionType, string pm_sModuleName,
        WorkFlowFormOpinion pm_wfoOpinion, object pm_oInputType)
    {
        return OpinionControlInit(pm_sOpinionTitle, pm_sOpinionType, pm_sModuleName, pm_wfoOpinion, "",  pm_oInputType);
    }

    protected ModuleState[] OpinionControlInit(string pm_sOpinionType, string pm_sModuleName,
        WorkFlowFormOpinion pm_wfoOpinion, object pm_oInputType)
    {
        return OpinionControlInit("", pm_sOpinionType, pm_sModuleName, pm_wfoOpinion, "", pm_oInputType);
    }

    protected ModuleState[] OpinionControlInit(string pm_sOpinionType, string pm_sModuleName,
        WorkFlowFormOpinion pm_wfoOpinion, string pm_sUserCode, object pm_oInputType)
    {
        return OpinionControlInit("", pm_sOpinionType, pm_sModuleName, pm_wfoOpinion, pm_sUserCode, pm_oInputType);
    }

    /// <param name="po_sConfirmOpinionList">控件名1,控件名2,控件名3,</param>
    /// <param name="pm_sInputType">Text,TextArea,TextAreaEsay,TextNum</param>
    protected ModuleState[] OpinionControlInit(string pm_sOpinionTitle, string pm_sOpinionType, string pm_sModuleName,
        WorkFlowFormOpinion pm_wfoOpinion, string pm_sUserCode, object pm_oInputType)
    {
        int ud_iOpinionStateCount = 2;
        ModuleState[] ud_wfmaWorkFlowModuleState = new ModuleState[ud_iOpinionStateCount];

        for (int i = 0; i < ud_iOpinionStateCount; i++)
        {
            ud_wfmaWorkFlowModuleState[i] = this.up_wftToolbar.GetModuleState(pm_sModuleName, i);
        }

        if (ud_wfmaWorkFlowModuleState[1] == ModuleState.Operable)
        {
            this.ConfirmOpinionList += pm_wfoOpinion.UniqueID + ",";
        }

        switch (pm_oInputType.ToString().ToLower())
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
        pm_wfoOpinion.ApplicationCode = this.up_wftToolbar.ApplicationCode;
        pm_wfoOpinion.CaseCode = this.up_wftToolbar.CaseCode;
        pm_wfoOpinion.State = ud_wfmaWorkFlowModuleState[0];
        pm_wfoOpinion.StateConfirm = ud_wfmaWorkFlowModuleState[1];
        pm_wfoOpinion.ProjectCode = this.up_wftToolbar.ProjectCode;

        if (pm_sUserCode.Trim() != "")
        {
            pm_wfoOpinion.OpinionUserCode = pm_sUserCode;
        }

        pm_wfoOpinion.InitControl();

        if (ud_wfmaWorkFlowModuleState[1] == ModuleState.Operable)
            this.up_wftToolbar.IsAudit = true;

        return ud_wfmaWorkFlowModuleState;
    }

    //图片签名控件初始化
    protected ModuleState ImageSignControlInit(string pm_sOpinionType, string pm_sModuleName,
    WorkFlowFormOpinion pm_wfsImageSign)
    {
        return ImageSignControlInit(pm_sOpinionType, pm_sModuleName, pm_wfsImageSign, "");
    }

    protected ModuleState ImageSignControlInit(string pm_sOpinionType, string pm_sModuleName,
        WorkFlowFormOpinion pm_wfsImageSign, string pm_sUserCode)
    {
        int ud_iOpinionStateCount = 2;
        ModuleState[] ud_wfmaWorkFlowModuleState = new ModuleState[ud_iOpinionStateCount];

        for (int i = 0; i < ud_iOpinionStateCount; i++)
        {
            ud_wfmaWorkFlowModuleState[i] = this.up_wftToolbar.GetModuleState(pm_sModuleName, i);
        }



        pm_wfsImageSign.OpinionType = pm_sOpinionType;
        pm_wfsImageSign.ApplicationCode = this.up_wftToolbar.ApplicationCode;
        pm_wfsImageSign.CaseCode = this.up_wftToolbar.CaseCode;
        pm_wfsImageSign.State = ud_wfmaWorkFlowModuleState[0];

        if (pm_sUserCode.Trim() != "")
        {
            pm_wfsImageSign.OpinionUserCode = pm_sUserCode;
        }

        pm_wfsImageSign.InitControl();

        return ud_wfmaWorkFlowModuleState[0];

    }


    protected string GetStringBySendItems(DataTable pm_dtSendItems)
    {
        string ud_sSendRoleItems = "";

        foreach (DataRow dr in pm_dtSendItems.Rows)
        {
            ud_sSendRoleItems += dr["UserCode"].ToString() + ",";
            ud_sSendRoleItems += dr["RoleCode"].ToString() + ",";
            ud_sSendRoleItems += dr["UserName"].ToString() + ",";
            ud_sSendRoleItems += dr["RoleName"].ToString() + ";";
        }

        return ud_sSendRoleItems;
    }

    protected DataTable GetSendItemsByCasePropertyValue(string pm_sCasePropertyValue)
    {
        string ud_sSendItems = this.up_wftToolbar.GetCasePropertyValue(pm_sCasePropertyValue) == null ? "" : this.up_wftToolbar.GetCasePropertyValue(pm_sCasePropertyValue);

        return GetSendItemsByString(ud_sSendItems);
    }

    protected DataTable GetSendItemsByString(string pm_sSendItems)
    {
        DataTable ud_dtSendItems = new DataTable();

        ud_dtSendItems.Columns.Add("UserCode");
        ud_dtSendItems.Columns.Add("RoleCode");
        ud_dtSendItems.Columns.Add("UserName");
        ud_dtSendItems.Columns.Add("RoleName");


        foreach (string tmpStr in pm_sSendItems.Split(';'))
        {
            string[] ud_saTmp = tmpStr.Split(',');
            if (ud_saTmp.Length == 4)
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

    //自动生成页面时，签名表单生成
    virtual protected void gvSignSheet_DataBound(object sender, GridViewRowEventArgs e)
    {
        string ud_sRoleName;

        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                ud_sRoleName = ((DataRowView)e.Row.DataItem).Row["RoleName"].ToString();
                OpinionControlInit(ud_sRoleName + "意见", this.OpinionPrefix + ud_sRoleName, ud_sRoleName, (WorkFlowFormOpinion)e.Row.FindControl("wfoSignSheet"));
                break;
            default:
                break;
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
        return DataSubmit(dao, false);
    }

    virtual protected Boolean DataSubmit(StandardEntityDAO dao, Boolean AuditFlag)
    {
        Boolean ReturnValue = true;

        ReturnValue = OperationDataSubmit(dao);

        //if (ReturnValue)
        //{
        //    ReturnValue = OpinionDataSubmit(dao);'
        //}

        if (ReturnValue && AuditFlag)
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
    virtual protected Boolean DataSubmit(StandardEntityDAO dao, Boolean AuditFlag, Boolean OperationFlag)
    {
        Boolean ReturnValue;

        if (OperationFlag)
        {
            ReturnValue = OperationDataSubmit(dao);
        }
        else
        {
            ReturnValue = true;
        }
        if (ReturnValue)
        {
            ReturnValue = OpinionDataSubmit(dao);
        }

        if (ReturnValue && AuditFlag)
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
            if (this.up_ucOperationControl.State == ModuleState.Operable)
            {
                this.up_ucOperationControl.dao = dao;
                string ErrMsg = this.up_ucOperationControl.SubmitData();

                if (ErrMsg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));
                    return false;
                }
                this.up_wftToolbar.ApplicationCode = this.up_ucOperationControl.ApplicationCode;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    virtual protected Boolean OpinionDataSubmit(StandardEntityDAO dao)
    {
        return OpinionDataSubmit(dao, false);
    }

    /// ****************************************************************************
    /// <summary>
    /// 流程意见控件数据保存
    /// </summary>
    /// ****************************************************************************
    virtual protected Boolean OpinionDataSubmit(StandardEntityDAO dao,bool flag)
    {
        try
        {
            string ud_sOpinionControlName = "wfoOpinion";

            for (int i = 1; i <= this.OpinionCount; i++)
            {
                RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoControl;
                ud_wfoControl = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)this.Page.FindControl(ud_sOpinionControlName + i.ToString());

                if (ud_wfoControl.State == ModuleState.Operable)
                {
                    ud_wfoControl.ApplicationCode = this.up_wftToolbar.ApplicationCode;
                    ud_wfoControl.CaseCode = this.up_wftToolbar.CaseCode;
                    if (!flag)
                    {
                        if (ud_wfoControl.ControlType == "TextArea")
                        {
                            ud_wfoControl.TextOpinion = this.up_wftToolbar.FlowOpinion;
                        }

                        ud_wfoControl.AuditValue = this.up_wftToolbar.AuditValue;
                    }
                    else
                    {
                        if (ud_wfoControl.ControlType == "TextArea")
                        {
                            this.up_wftToolbar.FlowOpinion = ud_wfoControl.TextOpinion;
                        }

                        this.up_wftToolbar.AuditValue = ud_wfoControl.OpinionConfirm;
                    }
                    ud_wfoControl.dao = dao;
                    ud_wfoControl.SubmitData();
                }
            }

            if (this.AutoBuild)
            {
                foreach (GridViewRow  ud_gvRow in this.up_gvSignSheet.Rows)
                {
                    RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoControl;
                    ud_wfoControl = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)ud_gvRow.FindControl("wfoSignSheet");

                    if (ud_wfoControl.State == ModuleState.Operable)
                    {
                        ud_wfoControl.ApplicationCode = this.up_wftToolbar.ApplicationCode;
                        ud_wfoControl.CaseCode = this.up_wftToolbar.CaseCode;
                        if (!flag)
                        {
                            if (ud_wfoControl.ControlType == "TextArea")
                            {
                                ud_wfoControl.TextOpinion = this.up_wftToolbar.FlowOpinion;
                            }

                            ud_wfoControl.AuditValue = this.up_wftToolbar.AuditValue;
                        }
                        else
                        {
                            if (ud_wfoControl.ControlType == "TextArea")
                            {
                                this.up_wftToolbar.FlowOpinion = ud_wfoControl.TextOpinion;
                            }

                            this.up_wftToolbar.AuditValue = ud_wfoControl.OpinionConfirm;
                        }
                        ud_wfoControl.dao = dao;
                        ud_wfoControl.AuditValue = this.up_wftToolbar.AuditValue;
                        ud_wfoControl.SubmitData();
                    }
                }

            }

            return true;
        }
        catch (Exception ex)
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
        /**************************判断当前辅助控制项所对应的辅助控制是否有同意/否决 ******************************/
        string opinionType = RmsPM.BLL.WorkFlowRule.GetTaskOpinionTypeByActCode(this.up_wftToolbar.ActCode);
        int ud_iOpinionStateCount = 2;
        string ud_sOpinionConfirm = "";
        ModuleState[] ud_wfmaWorkFlowModuleState = new ModuleState[ud_iOpinionStateCount];

        for (int i = 0; i < ud_iOpinionStateCount; i++)
        {
            ud_wfmaWorkFlowModuleState[i] = this.up_wftToolbar.GetModuleState(opinionType, i);
        }

        if (ud_wfmaWorkFlowModuleState[1] == ModuleState.Operable)  //当前如果有同意/否决选项 则做验证
        {
            ud_sOpinionConfirm = this.up_wftToolbar.AuditValue;
            if (ud_sOpinionConfirm == "")
            {
                ud_sOpinionConfirm = "Unknow";
            }
        }
        else
        {
            ud_sOpinionConfirm = this.up_wftToolbar.AuditValue;
        }
        /******************************************************************/
        //string ud_sOpinionConfirm = GetOpinionConfirm();
        

        return this.up_ucOperationControl.Audit(ud_sOpinionConfirm);
    }


    /// ****************************************************************************
    /// <summary>
    /// 获取页面上所有有效确认框的值
    /// </summary>
    /// <param name="pm_iConfirmType">确认框取值原则: 0 多数通过,1 一票否决, 2 一票赞成</param>
    /// ****************************************************************************
    
    protected string GetOpinionConfirm(string pm_sConfirmOpinionList, int pm_iConfirmType)
    {
        string ud_sOpinionConfirm = "";

        int ud_iApproveCount = 0;
        int ud_iRejectCount = 0;

        if (pm_sConfirmOpinionList.Trim() != "")
        {
            string[] ud_saConfirmOpinion = pm_sConfirmOpinionList.Split(',');

            for (int i = 0; i < ud_saConfirmOpinion.Length; i++)
            {
                if (ud_saConfirmOpinion[i].Trim() != "")
                {
                    WorkFlowFormOpinion ud_wfoOpinion = (WorkFlowFormOpinion)this.FindControl(ud_saConfirmOpinion[i].Trim());

                    switch (ud_wfoOpinion.OpinionConfirm)
                    {
                        case "Approve":
                            if (pm_iConfirmType == 2)
                            {
                                return "Approve";
                            }
                            else
                            {
                                ud_iApproveCount++;
                            }
                            break;
                        case "Reject":
                            if (pm_iConfirmType == 1)
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
                    if (ud_iApproveCount > 0 || ud_iRejectCount > 0)
                    {
                        if (ud_iApproveCount >= ud_iRejectCount)
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
                    if (ud_iApproveCount > 0)
                    {
                        ud_sOpinionConfirm = "Approve";
                    }
                    else
                    {
                        ud_sOpinionConfirm = "Unknow";
                    }
                    break;
                case 2:
                    if (ud_iRejectCount > 0)
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

    virtual protected string GetOpinionConfirm(string pm_sConfirmOpinionList)
    {
        return GetOpinionConfirm(pm_sConfirmOpinionList, 1);
    }

    virtual protected string GetOpinionConfirm()
    {
        return GetOpinionConfirm(this.ConfirmOpinionList);
    }
    #endregion



}
