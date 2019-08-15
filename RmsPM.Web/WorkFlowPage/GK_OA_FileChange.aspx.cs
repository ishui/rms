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

public partial class WorkFlowPage_GK_OA_FileChange : WorkFlowPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.WorkFlowName = "文件更改审批";

            this.OpinionCount = 0;
            this.lblWorkFlowName.Text = this.WorkFlowName;
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

        //业务表单初始化


        /**************************************************************************************/
        this.up_ucOperationControl.ApplicationCode = this.wftToolbar.ApplicationCode;
        this.up_ucOperationControl.State = this.wftToolbar.GetModuleState("申请表");
        this.up_ucOperationControl.UserCode = this.user.UserCode;
        this.up_ucOperationControl.OperationCode = Request.QueryString["FileChangeCode"];
        this.up_ucOperationControl.ucWorkFlowToolbar = this.wftToolbar;
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
       
        //会签部门表单初始化


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

        
    }

    /// <summary>
    /// 重写WORKFLOWTOOLBAR控件
    /// </summary>
    /// <param name="dao"></param>
    /// <param name="AuditFlag"></param>
    /// <returns></returns>
    override protected Boolean DataSubmit(StandardEntityDAO dao, Boolean AuditFlag)
    {
        this.up_ucOperationControl.ucWorkFlowToolbar = this.wftToolbar;
        return base.DataSubmit(dao, AuditFlag);
    }
}
