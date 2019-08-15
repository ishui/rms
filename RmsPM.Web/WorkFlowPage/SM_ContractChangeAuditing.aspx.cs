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
	/// SM_ContractChangeAuditing ��ժҪ˵����
	/// </summary>
public partial class WorkFlowPage_SM_ContractChangeAuditing : WorkFlowPageBase
{


    protected void Page_Load(object sender, System.EventArgs e)
    {
        // �ڴ˴������û������Գ�ʼ��ҳ��
        if (!IsPostBack)
        {
            this.EntityName = "Standard_Contract";
            this.WorkFlowName = "��ͬ������";// System.Configuration.ConfigurationSettings.AppSettings["ContractChangeAuditingName"].ToString();
            this.OpinionCount = 11;
            InitPage();
        }
    }

    /// ****************************************************************************
    /// <summary>
    /// �������ؼ���ʼ��
    /// </summary>
    /// ****************************************************************************
    override protected void PageControlInit()
    {
        base.PageControlInit();

        string ud_sContractCode = "";
        string ud_sContractChangeCode = "";

        if (this.wftToolbar.ApplicationCode != "")
        {
            ud_sContractChangeCode = this.wftToolbar.ApplicationCode;
        }
        else if (Request["ContractChangeCode"] + "" != "")
        {
            ud_sContractChangeCode = Request["ContractChangeCode"] + "";
        }

        ud_sContractCode = RmsPM.BLL.ContractRule.GetContractCodeByChangeCode(ud_sContractChangeCode);

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

        //ҵ�����ʼ��
        /**************************************************************************************/
        this.up_ucOperationControl.ApplicationCode = this.wftToolbar.ApplicationCode;
        this.up_ucOperationControl.State = this.wftToolbar.GetModuleState("�����");
        this.up_ucOperationControl.UserCode = this.user.UserCode;
        this.up_ucOperationControl.OperationCode = Request["ContractChangeCode"] + "";
//        this.up_ucOperationControl.MoneyState = this.wftToolbar.GetModuleState("���");
        this.up_ucOperationControl.MoneyState = ud_MoneyState;
        this.up_ucOperationControl.AttachmentState = this.wftToolbar.GetModuleState("����");
        this.up_ucOperationControl.InitControl();
        /**************************************************************************************/


        //�������ʼ��
        /**************************************************************************************/
        //��������Ƿ���Բ���
        string ud_sOpinionControlName = "wfoOpinion";
        for (int i = 1; i <= this.OpinionCount; i++)
        {
            RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion ud_wfoControl;
            ud_wfoControl = (RmsPM.Web.WorkFlowControl.WorkFlowFormOpinion)this.Page.FindControl(ud_sOpinionControlName + i.ToString());
            ud_wfoControl.IsRdoCheck = false;
            ud_wfoControl.IsUseTemplateOpinion = true;
            ud_wfoControl.IsUseTextArea = true;
        }

        OpinionControlInit("�Ƽ����", "SM_CCA_������Ʋ�", "������Ʋ�", this.wfoOpinion1);
        OpinionControlInit("�Ƽ����", "SM_CCA_��Լ��", "��Լ��", this.wfoOpinion2);

        //�ܲ��ܼ�
        OpinionControlInit("�����ܼ�", "SM_CCA_�����ܼ�", "�����ܼ�", this.wfoOpinion3);
        OpinionControlInit("��Լ�ܼ�", "SM_CCA_��Լ�ܼ�", "��Լ�ܼ�", this.wfoOpinion4);
        OpinionControlInit("�����ܼ�", "SM_CCA_�����ܼ�", "�����ܼ�", this.wfoOpinion5);

        //���»�
        OpinionControlInit("����ִ��", "SM_CCA_����ִ��", "����ִ��", this.wfoOpinion6);
        OpinionControlInit("��Լִ��", "SM_CCA_��Լִ��", "��Լִ��", this.wfoOpinion7);
        OpinionControlInit("����ִ��", "SM_CCA_����ִ��", "����ִ��", this.wfoOpinion8);
        OpinionControlInit("����ִ��", "SM_CCA_����ִ��", "����ִ��", this.wfoOpinion11);

        //�����ܼ����ʼ��
        if (wftToolbar.GetModuleState("������Ŀ�ܼ�") == ModuleState.Operable)
        {
            tdMajordomo2.Visible = true;
        }
        else
        {
            tdMajordomo2.Visible = false;
        }

        //��ǩ���ű���ʼ��
        /**************************************************************************************/
        DataTable ud_dtSendItems = GetSendItemsByCasePropertyValue("��ǩ����");

        if (ud_dtSendItems.Rows.Count > 0)
        {
            rptMeetSign.DataSource = ud_dtSendItems;
            rptMeetSign.DataBind();

            trMajordomo.Visible = false;
            trMeetSign.Visible = true;

        }
        else
        {
            //��Ŀ�ܼ�ֱǩ
            OpinionControlInit("��Ŀ���ܼ�", "SM_CCA_��Ŀ���ܼ�", "��Ŀ���ܼ�", this.wfoOpinion9);
            OpinionControlInit("��Ŀ�ܼ�", "SM_CCA_��Ŀ�ܼ�", "��Ŀ�ܼ�", this.wfoOpinion10);

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
    /// ����������������
    /// </summary>
    /// ****************************************************************************
    override protected void WorkFlowPropertySave()
    {
        base.WorkFlowPropertySave();

        if (wftToolbar.IsNew)
        {
            wftToolbar.SaveCasePropertyValue("�û����", user.GetOperationType());

            wftToolbar.SaveCasePropertyValue("��ǩ����", "");
            wftToolbar.SaveCasePropertyValue("��ǩ������", "");
        }

        if (wftToolbar.GetModuleState("��Ŀ�ܼ�") == ModuleState.Operable)
        {
            wftToolbar.SaveCasePropertyValue("�û����", user.GetOperationType());
        }


        //if ( wftToolbar.GetModuleState("׼����ǩ") == ModuleState.Operable)
        //{
        //    wftToolbar.SaveCasePropertyValue("��ǩ����",wftToolbar.SendRoleItems);
        //    wftToolbar.SaveCasePropertyValue("��ǩ������",this.user.UserCode);
        //}
    }



    /// ****************************************************************************
    /// <summary>
    /// ��������ؼ����ݱ���
    /// ��ï����Ҫ��(2����Ŀ�ܼ��ǩ)
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
            Response.Write(Rms.Web.JavaScript.Alert(true, "��������������" + ex.Message));
            throw ex;
        }
    }

    /// ****************************************************************************
    /// <summary>
    /// ��ǩ�ؼ����ݰ�
    /// ��ï����Ҫ��(2����Ŀ�ܼ��ǩ)
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
                OpinionControlInit(ud_sRoleName + "���", "SM_CAA_" + ud_sRoleName, ud_sRoleName, ud_AltwfoControl);
                break;
            case ListItemType.Item:
                ud_sRoleName = ((DataRowView)e.Item.DataItem).Row["RoleName"].ToString();
                ud_sUserCode = ((DataRowView)e.Item.DataItem).Row["UserCode"].ToString();
                WorkFlowFormOpinion ud_wfoControl = (WorkFlowFormOpinion)e.Item.FindControl("wfoItemOpinion");
                ud_wfoControl.IsRdoCheck = false;
                ud_wfoControl.IsUseTemplateOpinion = true;
                ud_wfoControl.IsUseTextArea = true;
                OpinionControlInit(ud_sRoleName + "���", "SM_CAA_" + ud_sRoleName, ud_sRoleName, ud_wfoControl);
                break;
            default:
                break;
        }
    }
}