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
using RmsPM.Web;


	/// <summary>
	/// SM_ContractAuditing 的摘要说明。
	/// </summary>
public partial class WorkFlowPage_SM_ContractAuditing : WorkFlowPageBase
{

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (!IsPostBack)
        {
            this.EntityName = "Standard_Contract";
            this.WorkFlowName = "合同审核";// System.Configuration.ConfigurationSettings.AppSettings["ContractAuditingName"].ToString();
            this.OpinionCount = 13;
            InitPage();
        }
    }

    /// ****************************************************************************
    /// <summary>
    /// 审核意见控件初始化
    /// </summary>
    /// ****************************************************************************
    override protected void PageControlInit()
    {


        base.PageControlInit();

        string ud_sContractCode = "";

        if (this.wftToolbar.ApplicationCode != "")
        {
            ud_sContractCode = this.wftToolbar.ApplicationCode;
        }
        else if (Request["ContractCode"] + "" != "")
        {
            ud_sContractCode = Request["ContractCode"] + "";
        }

        ModuleState ud_MoneyState = ModuleState.Unbeknown;

        ArrayList ar = user.GetResourceRight(ud_sContractCode, "Contract");
        if (ar.Contains("050122"))
        {
            ud_MoneyState = ModuleState.Eyeable;
        }
        else
        {
            ud_MoneyState = ModuleState.Sightless;
        }

        //业务表单初始化
        /**************************************************************************************/
        this.up_ucOperationControl.ApplicationCode = this.wftToolbar.ApplicationCode;
        this.up_ucOperationControl.State = this.wftToolbar.GetModuleState("申请表");
        this.up_ucOperationControl.UserCode = this.user.UserCode;
        this.up_ucOperationControl.OperationCode = Request["ContractCode"] + "";
        //        this.up_ucOperationControl.MoneyState = this.wftToolbar.GetModuleState("金额");
        this.up_ucOperationControl.MoneyState = ud_MoneyState;
        this.up_ucOperationControl.AttachmentState = this.wftToolbar.GetModuleState("附件");
        this.up_ucOperationControl.InitControl();
        /**************************************************************************************/


        //意见表单初始化
        /**************************************************************************************/
        //控制意见是否可以操作
        string ud_sOpinionControlName = "wfoOpinion";
        for (int i = 1; i <= this.OpinionCount; i++)
        {
            RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoControl;
            ud_wfoControl = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)this.Page.FindControl(ud_sOpinionControlName + i.ToString());
            ud_wfoControl.IsRdoCheck = false;
            ud_wfoControl.IsUseTemplateOpinion = true;
            ud_wfoControl.IsUseTextArea = true;
        }

        OpinionControlInit("意见", "SM_CA_建筑设计部", "建筑设计部", this.wfoOpinion1);

        OpinionControlInit("意见", "SM_CA_工程部", "工程部", this.wfoOpinion2);

        OpinionControlInit("意见", "SM_CA_合约部", "合约部", this.wfoOpinion3);

        OpinionControlInit("意见", "SM_CA_法务部", "法务部", this.wfoOpinion4);

        //总部总监

        OpinionControlInit("工程总监", "SM_CA_工程总监", "工程总监", this.wfoOpinion5);
        OpinionControlInit("合约总监", "SM_CA_合约总监", "合约总监", this.wfoOpinion6);

        OpinionControlInit("财务总监", "SM_CA_财务总监", "财务总监", this.wfoOpinion7);

        //董事会

        OpinionControlInit("工程执董", "SM_CA_工程执董", "工程董事", this.wfoOpinion8);

        OpinionControlInit("合约执董", "SM_CA_合约执董", "合约董事", this.wfoOpinion9);

        OpinionControlInit("CFO", "SM_CA_财务执董", "财务董事", this.wfoOpinion10);

        OpinionControlInit("财务董事", "SM_CA_董事长", "董事长", this.wfoOpinion11);



        //二个总监表单初始化
        if (wftToolbar.GetModuleStateEx("二个项目总监", 0) == ModuleState.Operable)
        {
            tdMajordomo2.Visible = true;

        }
        else
        {
            tdMajordomo2.Visible = false;
        }

        //会签部门表单初始化
        /**************************************************************************************/
        DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("会签部门");

        if (ud_dtSendItems.Rows.Count > 0)
        {
            rptMeetSign.DataSource = ud_dtSendItems;
            rptMeetSign.DataBind();

            trMajordomo.Visible = false;
            trMeetSign.Visible = true;

        }
        else
        {
            //项目总监直签

            OpinionControlInit("项目副总监", "SM_CA_项目副总监", "项目副总监", this.wfoOpinion12);

            OpinionControlInit("项目总监", "SM_CA_项目总监", "项目总监", this.wfoOpinion13);

            trMajordomo.Visible = true;
            trMeetSign.Visible = false;
        }
        /**************************************************************************************/
    }

    override protected void InitEventHandler()
    {
        base.InitEventHandler();
        this.rptMeetSign.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptMeetSign_ItemDataBound);
    }

    /// ****************************************************************************
    /// <summary>
    /// 保存流程属性数据
    /// </summary>
    /// ****************************************************************************
    override protected void WorkFlowPropertySave()
    {
        base.WorkFlowPropertySave();

        if (wftToolbar.IsNew)
        {

            wftToolbar.SaveCasePropertyValue("用户类别", user.GetOperationType());

            wftToolbar.SaveCasePropertyValue("会签部门", "");
            wftToolbar.SaveCasePropertyValue("会签发起人", "");
        }

        if (wftToolbar.GetModuleState("项目总监") == ModuleState.Operable)
        {
            wftToolbar.SaveCasePropertyValue("用户类别", user.GetOperationType());
        }

        //if (wftToolbar.GetModuleState("准备会签") == ModuleState.Operable)
        //{
        //    wftToolbar.SaveCasePropertyValue("会签部门", wftToolbar.SendRoleItems);
        //    wftToolbar.SaveCasePropertyValue("会签发起人", this.user.UserCode);
        //}
    }


    /// ****************************************************************************
    /// <summary>
    /// 流程意见控件数据保存
    /// 世茂特殊要求(2个项目总监会签)
    /// </summary>
    /// ****************************************************************************
    override protected Boolean OpinionDataSubmit(StandardEntityDAO dao, bool flag)
    {
        Boolean ReturnValue;

        ReturnValue = base.OpinionDataSubmit(dao, flag);

        try
        {
            if (ReturnValue)
            {
                foreach (RepeaterItem ud_rptItem in rptMeetSign.Items)
                {

                    RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoControl;

                    switch (ud_rptItem.ItemType)
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

                    if (ud_wfoControl.State == ModuleState.Operable)
                    {
                        ud_wfoControl.ApplicationCode = wftToolbar.ApplicationCode;
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
                        ud_wfoControl.CaseCode = wftToolbar.CaseCode;
                        ud_wfoControl.SubmitData();
                    }
                }
            }

            return ReturnValue;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "审批意见保存出错：" + ex.Message));
            throw ex;
        }
    }

    /// ****************************************************************************
    /// <summary>
    /// 会签控件数据绑定
    /// 世茂特殊要求(2个项目总监会签)
    /// </summary>
    /// ****************************************************************************
    private void rptMeetSign_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        string ud_sRoleName, ud_sUserCode;

        switch (e.Item.ItemType)
        {
            case ListItemType.AlternatingItem:
                ud_sRoleName = ((DataRowView)e.Item.DataItem).Row["RoleName"].ToString();
                ud_sUserCode = ((DataRowView)e.Item.DataItem).Row["UserCode"].ToString();

                WorkFlowFormOpinion ud_AltwfoControl = (WorkFlowFormOpinion)e.Item.FindControl("wfoAlternatingItemOpinion");
                ud_AltwfoControl.IsRdoCheck = false;
                ud_AltwfoControl.IsUseTemplateOpinion = true;
                ud_AltwfoControl.IsUseTextArea = true;
                OpinionControlInit(ud_sRoleName + "意见", "SM_CAA_" + ud_sRoleName, ud_sRoleName, ud_AltwfoControl);
                break;
            case ListItemType.Item:
                ud_sRoleName = ((DataRowView)e.Item.DataItem).Row["RoleName"].ToString();
                ud_sUserCode = ((DataRowView)e.Item.DataItem).Row["UserCode"].ToString();
                WorkFlowFormOpinion ud_wfoControl = (WorkFlowFormOpinion)e.Item.FindControl("wfoItemOpinion");
                ud_wfoControl.IsRdoCheck = false;
                ud_wfoControl.IsUseTemplateOpinion = true;
                ud_wfoControl.IsUseTextArea = true;
                OpinionControlInit(ud_sRoleName + "意见", "SM_CAA_" + ud_sRoleName, ud_sRoleName, ud_wfoControl);
                break;
            default:
                break;
        }
    }
}