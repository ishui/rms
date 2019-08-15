//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\workflowcontrol\Stub_workflowcasestate_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���workflowcontrol\workflowcasestate.ascx.cs���������ࡰMigrated_WorkFlowCaseState���Ļ��ࡣ
// ��������������Ŀ�е����д����ļ����øû��ࡣ
// �йش˴���ģʽ�ĸ�����Ϣ����ο� http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================




namespace RmsPM.Web.WorkFlowControl
{

    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using RmsPM.BLL;
    using RmsPM.DAL.EntityDAO;
    using RmsPM.DAL.QueryStrategy;
    using RmsPM.Web;
    using Rms.ORMap;
    using Rms.WorkFlow;

    abstract public class WorkFlowCaseState : System.Web.UI.UserControl
    {
        abstract public WorkFlowToolbar Toobar
        {
            get;
            set;
        }
        abstract public string ActCode
        {
            get;
            set;
        }
        abstract public string ApplicationCode
        {
            get;
            set;
        }
        abstract public string FlowName
        {
            get;
            set;
        }
        abstract public string UserCode
        {
            get;
            set;
        }
        abstract public bool Scout
        {
            get;
            set;
        }
        abstract public string CaseCode
        {
            get;
            set;
        }
        abstract public bool IsScoutPopedom
        {
            get;
            set;
        }
        abstract public bool IsUsePrint
        {
            get;
            set;
        }
        abstract public string ProjectCode
        {
            get;
            set;
        }
       
        abstract public void ControlDataBind();
        abstract public void SubmitData();


    }



}
