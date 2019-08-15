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

public partial class WorkFlowPage_ZD_BiddingConditionFile : WorkFlowPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            //this.EntityName = "Bidding";
            this.WorkFlowName = "招标技术条件评审";// System.Configuration.ConfigurationSettings.AppSettings["PaymentAuditingName"].ToString();
            this.OpinionCount = 1;

            InitPage();
        }

    }
    /*override protected void SetBaseControl()
    {
        base.wftToolbar = this.wftToolbar;
        //base.wfcCaseState = this.WorkFlowCaseState1;
        base.ucCheckControl = this.ucCheckControl;
        //base.ucOperationControl = this.ucOperationControl;
    }*/
    /// ****************************************************************************
    /// <summary>
    /// 初始化

    /// </summary>
    /// ****************************************************************************
    override protected void OperationControlInit()
    {
        base.OperationControlInit();

        /**************************************************************************************/

        this.ucOperationControl.ApplicationCode = this.wftToolbar.ApplicationCode;

        this.ucOperationControl.State = this.wftToolbar.GetModuleState("申请表");
        this.ucOperationControl.SetAttachList1 = this.wftToolbar.GetModuleState("附件1");
        this.ucOperationControl.SetAttachList2 = this.wftToolbar.GetModuleState("附件2");
        this.ucOperationControl.SetAttachList3 = this.wftToolbar.GetModuleState("附件3");
        this.ucOperationControl.SetAttachList4 = this.wftToolbar.GetModuleState("附件4");
        this.ucOperationControl.SetAttachList5 = this.wftToolbar.GetModuleState("附件5");
        this.ucOperationControl.SetAttachList6 = this.wftToolbar.GetModuleState("附件6");

        this.ucOperationControl.UserCode = user.UserCode;
        this.ucOperationControl.BiddingConditionFileCode = Request["BiddingConditionFileCode"] + ""; ;
        this.ucOperationControl.BiddingCode = Request["BiddingCode"] + "";
        this.ucOperationControl.ProjectCode = Request["ProjectCode"] + "";
        this.ucOperationControl.InitControl();

        //ModuleState Moneystate = this.wftToolbar.GetModuleState("金额设置");//添加金额
        //ModuleState state = ModuleState.Operable;
        //if (Moneystate == ModuleState.Operable)
        //{

        //    this.btnAddPrice.Visible = true;
        //    this.btnAddPrice.Attributes.Add("onclick", "javascript:OpenFullWindow('biddingmodify.aspx?ApplicationCode=" + this.ucOperationControl.BiddingCode + "','');");
        //    this.spMoney.Visible = true;
        //    this.spMoney.InnerHtml = this.ucOperationControl.Money.ToString();
        //}
        //else if (Moneystate == ModuleState.Eyeable)
        //{
        //    this.btnAddPrice.Visible = false;
        //    this.spMoney.Visible = true;
        //    this.spMoney.InnerHtml = this.ucOperationControl.Money.ToString();
        //}
        //else
        //{
        //    this.btnAddPrice.Visible = false;
        //    this.spMoney.Visible = false;
        //}




        //OpinionControlInit("意见", "TC_BP_公务所项目经理", "项目经理", this.wfoOpinion1);

        //OpinionControlInit("意见", "TC_BP_工务部工务主管", "工务主管", this.wfoOpinion2);

        //OpinionControlInit("意见", "TC_BP_工务部概预算组", "概预算组", this.wfoOpinion3);

        //OpinionControlInit("意见", "TC_BP_工务部设计组", "设计组", this.wfoOpinion4);

        //OpinionControlInit("意见", "TC_BP_总经理室副总经理", "副总经理", this.wfoOpinion5);

        //OpinionControlInit("意见", "TC_BP_总经理室总经理", "总经理", this.wfoOpinion6);



        //PageControlInit();

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
        OpinionControlInit("意见", "TC_BP_申请人意见", "申请表", this.wfoOpinion1);

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

    /// ****************************************************************************
    /// <summary>
    /// 保存流程属性数据

    /// </summary>
    /// ****************************************************************************
    override protected void WorkFlowPropertySave()
    {
        // base.WorkFlowPropertySave();
        if (wftToolbar.IsNew && this.ucOperationControl.State == ModuleState.Operable)
        {
            this.up_wftToolbar.SaveCasePropertyValue("主题", this.up_ucOperationControl.ApplicationTitle);
            this.up_wftToolbar.SaveCasePropertyValue("申请人", this.user.UserCode);
            this.up_wftToolbar.SaveCasePropertyValue("项目代码", this.up_ucOperationControl.ProjectCode);
            this.up_wftToolbar.SaveCasePropertyValue("项目部门", RmsPM.BLL.ProjectRule.GetUnitByProject(this.up_ucOperationControl.ProjectCode));
            this.up_wftToolbar.SaveCasePropertyValue("业务类别", this.up_ucOperationControl.ApplicationType);
            this.up_wftToolbar.SaveCasePropertyValue("业务部门", this.up_ucOperationControl.UnitCode);

        }
        if (wftToolbar.IsNew)
        {
            wftToolbar.SaveCasePropertyValue("表单部门", "");
        }

        if (wftToolbar.CommandType == ToolbarCommandType.Send)
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

        this.dgSignSheet.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgSignSheet_ItemDataBound);
    }



    #endregion

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
                OpinionControlInit(ud_sRoleName + "意见", "TC_BP_" + ud_sRoleName, ud_sRoleName, (WorkFlowFormOpinion)e.Item.FindControl("wfoSignSheet"));
                break;
            default:
                break;
        }

    }
}
