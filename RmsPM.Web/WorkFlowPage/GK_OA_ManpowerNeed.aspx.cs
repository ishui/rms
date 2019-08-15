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


public partial class WorkFlowPage_GK_OA_ManpowerNeed : WorkFlowPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.WorkFlowName = "人力资源需求审批";

            this.OpinionCount = 1;
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
        this.up_ucOperationControl.OperationCode = Request.QueryString["ManpowerNeedCode"];
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
        OpinionControlInit("意见", "GK_OA_ManpowerNeed_申请人意见", "申请表", this.wfoOpinion1);

        //会签部门表单初始化


        /**************************************************************************************/
        DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("表单部门");

        if (ud_dtSendItems.Rows.Count > 0)
        {
            tr_SignSheet.Visible = true;
            dgSignSheet.DataSource = ud_dtSendItems;
            dgSignSheet.DataBind();
        }
        else
        {
            tr_SignSheet.Visible = false;
        }
        /**************************************************************************************/
    }

    override protected void InitEventHandler()
    {
        base.InitEventHandler();
        this.dgSignSheet.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgSignSheet_ItemDataBound);
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

            wftToolbar.SaveCasePropertyValue("表单部门", "");

        }

        if (wftToolbar.CommandType == ToolbarCommandType.Send && this.up_wftToolbar.GetModuleState("NoRegister") != ModuleState.Operable)
        {
            DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("表单部门");

            DataTable ud_dtNewSendItems = GetSendItemsByString(wftToolbar.SendRoleItems);


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

            wftToolbar.SaveCasePropertyValue("表单部门", ud_sSendRoleItems);

        }
    }


    /// ****************************************************************************
    /// <summary>
    /// 流程意见控件数据保存
    /// 特殊
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
                foreach (DataGridItem ud_dgItem in dgSignSheet.Items)
                {
                    RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoControl;
                    ud_wfoControl = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)ud_dgItem.FindControl("wfoSignSheet");

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
            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "审批意见保存出错：" + ex.Message));
            ReturnValue = false;
        }

        return ReturnValue;

    }


    /// ****************************************************************************
    /// <summary>
    /// 会签控件数据绑定
    /// </summary>
    /// ****************************************************************************
    private void dgSignSheet_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        string ud_sRoleName;

        switch (e.Item.ItemType)
        {
            case ListItemType.AlternatingItem:
            case ListItemType.Item:
                ud_sRoleName = ((DataRowView)e.Item.DataItem).Row["RoleName"].ToString();
                OpinionControlInit(ud_sRoleName + "意见", "GK_OA_ManpowerNeed_" + ud_sRoleName, ud_sRoleName, (WorkFlowFormOpinion)e.Item.FindControl("wfoSignSheet"));
                break;
            default:
                break;
        }
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
