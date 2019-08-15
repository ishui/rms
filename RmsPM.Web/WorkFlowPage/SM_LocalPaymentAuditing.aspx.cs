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


public partial class WorkFlowPage_SM_LocalPaymentAuditing : WorkFlowPageBase
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面



        if (!IsPostBack)
        {
            this.EntityName = "Standard_Payment";
            this.WorkFlowName = "合同请款审核";// System.Configuration.ConfigurationSettings.AppSettings["PaymentAuditingName"].ToString();
            this.OpinionCount = 0;
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

        string ud_sPaymentCode = "";

        if (this.wftToolbar.ApplicationCode != "")
        {
            ud_sPaymentCode = this.wftToolbar.ApplicationCode;
        }
        else if (Request["PaymentCode"] + "" != "")
        {
            ud_sPaymentCode = Request["PaymentCode"] + "";
        }

        ModuleState ud_MoneyState = ModuleState.Unbeknown;

        ArrayList ar = user.GetResourceRight(ud_sPaymentCode, "Payment");
        if (ar.Contains("060110"))
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
        this.up_ucOperationControl.OperationCode = Request["PaymentCode"] + "";
        //        this.up_ucOperationControl.MoneyState = this.wftToolbar.GetModuleState("金额");
        this.up_ucOperationControl.MoneyState = ud_MoneyState;
        this.up_ucOperationControl.AttachmentState = this.wftToolbar.GetModuleState("附件");
        this.up_ucOperationControl.InitControl();
        /**************************************************************************************/
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



        /**************************************************************************************/


        /**************************************************************************************/
    }

    override protected void InitEventHandler()
    {
        base.InitEventHandler();

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
}



