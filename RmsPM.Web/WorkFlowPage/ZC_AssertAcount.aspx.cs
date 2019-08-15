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

public partial class WorkFlowPage_ZC_AssertAcount : WorkFlowPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.WorkFlowName = "�̶��ʲ�̨��";

            this.OpinionCount = 0;
            this.lblWorkFlowName.Text = this.WorkFlowName;
            InitPage();
        }
    }


    /// ****************************************************************************
    /// <summary>
    /// ҵ����ؼ���ʼ��


    /// </summary>
    /// ****************************************************************************
    override protected void OperationControlInit()
    {
        base.OperationControlInit();

        //ҵ�����ʼ��


        /**************************************************************************************/
        this.up_ucOperationControl.ApplicationCode = this.wftToolbar.ApplicationCode;
        this.up_ucOperationControl.State = this.wftToolbar.GetModuleState("�����");
        this.up_ucOperationControl.UserCode = this.user.UserCode;
        this.up_ucOperationControl.OperationCode = Request.QueryString["FileChangeCode"];
        this.up_ucOperationControl.ucWorkFlowToolbar = this.wftToolbar;
        this.up_ucOperationControl.InitControl();
        /**************************************************************************************/
    }


    /// ****************************************************************************
    /// <summary>
    /// �������ؼ���ʼ��


    /// </summary>
    /// ****************************************************************************
    override protected void PageControlInit()
    {
        base.PageControlInit();

        //�������ʼ��


        /**************************************************************************************/

        //��ǩ���ű���ʼ��


        /**************************************************************************************/
    }

    override protected void InitEventHandler()
    {
        base.InitEventHandler();

    }

    /// ****************************************************************************
    /// <summary>
    /// ����������������


    /// </summary>
    /// ****************************************************************************
    override protected void WorkFlowPropertySave()
    {
        base.WorkFlowPropertySave();


    }

    /// <summary>
    /// ��дWORKFLOWTOOLBAR�ؼ�
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
