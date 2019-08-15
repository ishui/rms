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
using RmsDM.BFL;
using RmsDM.MODEL;

using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;
using RmsPM.Web;

public partial class WorkFlowPage_DocumentFile : WorkFlowPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //
            string ud_sProcedureCode = Request["ProcedureCode"] + "";
            string FileTemplateCode = Request["FileTemplateCode"] + "";
            //this.EntityName = "WorkFlowCommon";           
            this.WorkFlowName = RmsPM.BLL.WorkFlowRule.GetProcedureNameByCode(ud_sProcedureCode);
            this.OpinionCount = 1;
            FileTemplateBFL FTBFL = new FileTemplateBFL();
            DocumentFileBFL DFBFL = new DocumentFileBFL();
            FileTemplateModel fileTemplate = null;
            if (string.IsNullOrEmpty(FileTemplateCode))
            {
                fileTemplate = FTBFL.GetFileTemplateByProcCode(ud_sProcedureCode);
            }
            else
            {
                fileTemplate = FTBFL.GetFileTemplate(int.Parse(FileTemplateCode));
            }
            if (fileTemplate != null)
            {
                this.lblWorkFlowName.Text = fileTemplate.FileTemplateName;
                FileTemplateCode = fileTemplate.Code.ToString();
            }

            if (!string.IsNullOrEmpty(Request["ApplicationCode"]))
            {
                this.lblWorkFlowName.Text = DFBFL.GetDocumentFile(int.Parse(Request["ApplicationCode"])).OperationType;
            }
            this.ucOperationControl.FileTemplateCode = fileTemplate.Code.ToString();


            this.OpinionPrefix = "WFC_";
            this.ucOperationControl.FileTemplateCode = FileTemplateCode;

            this.OpinionCount = 1;
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
        this.up_ucOperationControl.OperationCode = Request["DocumentFileCode"] + "";
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
        OpinionControlInit("意见", "DF_File_申请人意见", "申请表", this.wfoOpinion1);

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
    override protected Boolean OpinionDataSubmit(StandardEntityDAO dao)
    {
        Boolean ReturnValue;

        ReturnValue = base.OpinionDataSubmit(dao);

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
    /// 特殊
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
                OpinionControlInit(ud_sRoleName + "意见", "DF_File_" + ud_sRoleName, ud_sRoleName, (WorkFlowFormOpinion)e.Item.FindControl("wfoSignSheet"));
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
