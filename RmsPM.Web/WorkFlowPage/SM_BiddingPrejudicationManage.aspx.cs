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
using RmsPM.Web;

public partial class WorkFlowPage_SM_BiddingPrejudicationManage : WorkFlowPageBase
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面

        if (!IsPostBack)
        {
            this.WorkFlowName = "投标单位评审";
            this.OpinionCount = 10;
            InitPage();
        }
    }


    /// ****************************************************************************
    /// <summary>
    /// 业务表单控件初始化

    /// </summary>
    /// ****************************************************************************
    override protected void OperationControlInit()
    {
        base.OperationControlInit();

        /**************************************************************************************/

        this.ucOperationControl.ApplicationCode = this.wftToolbar.ApplicationCode;
        this.ucOperationControl.State = this.wftToolbar.GetModuleState("申请表");
        this.ucOperationControl.State1 = this.wftToolbar.GetModuleState("辅助状态");
        this.ucOperationControl.SetAttachList1 = this.wftToolbar.GetModuleState("工作范围附件");
        this.ucOperationControl.SetAttachList2 = this.wftToolbar.GetModuleState("资格要求附件");
        this.ucOperationControl.SetAttachList3 = this.wftToolbar.GetModuleState("单位名单附件");

        this.ucOperationControl.UserCode = user.UserCode;
        this.ucOperationControl.BiddingCode = Request["BiddingCode"] + "";
        this.ucOperationControl.ProjectCode = Request["ProjectCode"] + "";

        this.ucOperationControl.InitControl();
    }


    /// ****************************************************************************
    /// <summary>
    /// 审核意见控件初始化




    /// </summary>
    /// ****************************************************************************
    override protected void PageControlInit()
    {
        base.PageControlInit();

        //意见表单初始化

        string ud_sConfirmOpinionList = "";

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

        OpinionControlInit("推荐意见", "SM_BA_建筑设计部", "建筑设计部", this.wfoOpinion1);

        OpinionControlInit("推荐意见", "SM_BA_工程部", "工程部", this.wfoOpinion2);

        OpinionControlInit("推荐意见", "SM_BA_合约部", "合约部", this.wfoOpinion3);

        OpinionControlInit("推荐意见", "SM_BA_项目总监", "项目总监", this.wfoOpinion4);

        OpinionControlInit("推荐意见", "SM_BA_总部总监1", "总部总监1", this.wfoOpinion5);

        OpinionControlInit("推荐意见", "SM_BA_总部总监2", "总部总监2", this.wfoOpinion6);

        OpinionControlInit("推荐意见", "SM_BA_董事长1", "工程执董", this.wfoOpinion7);

        OpinionControlInit("推荐意见", "SM_BA_董事长2", "合约执董", this.wfoOpinion8);
        OpinionControlInit("推荐意见", "SM_BA_董事长3", "董事长3", this.wfoOpinion9);
        OpinionControlInit("推荐意见", "SM_BA_董事长4", "董事长4", this.wfoOpinion10);

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

    }


    /// ****************************************************************************
    /// <summary>
    /// 保存流程属性数据



    /// </summary>
    /// ****************************************************************************
    override protected void WorkFlowPropertySave()
    {
        base.WorkFlowPropertySave();
        if (wftToolbar.IsNew && this.ucOperationControl.State == ModuleState.Operable)
        {
            wftToolbar.SaveCasePropertyValue("用户类别", "");
            wftToolbar.SaveCasePropertyValue("会签部门", "");
            wftToolbar.SaveCasePropertyValue("会签发起人", "");


        }

        if (wftToolbar.GetModuleState("项目总监") == ModuleState.Operable)
        {
            wftToolbar.SaveCasePropertyValue("用户类别", this.user.GetOperationType());
        }
        if (wftToolbar.GetModuleState("准备会签") == ModuleState.Operable)
        {
            wftToolbar.SaveCasePropertyValue("会签部门", wftToolbar.SendRoleItems);
            wftToolbar.SaveCasePropertyValue("会签发起人", this.user.UserCode);
        }
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

    override protected void InitEventHandler()
    {
        base.InitEventHandler();
        this.ucOperationControl.up_UCBiddingSupplierModify.SaveDataEvent += new System.EventHandler(this.ucOperationControl_SaveData);
        this.rptMeetSign.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.rptMeetSign_ItemDataBound);
    }


    #endregion

    private void ucOperationControl_SaveData(object sender, System.EventArgs e)
    {
        this.ucOperationControl.uc_UCBiddingSupplierList.LoadData();
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

