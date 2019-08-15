//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\workflowcontrol\Stub_workflowformopinion_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���workflowcontrol\workflowformopinion.ascx.cs���������ࡰMigrated_WorkFlowFormOpinion���Ļ��ࡣ
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
    using Rms.ORMap;
    using RmsPM.Web.WorkFlowControl;

    abstract public class WorkFlowFormOpinion : RmsPM.Web.WorkFlowControl.WorkFlowControlClassBase
    {
        abstract public ModuleState StateConfirm
        {
            get;
            set;
        }
        abstract public string Title
        {
            get;
            set;
        }


        abstract public string ControlType
        {
            get;
            set;
        }
        abstract public string OpinionType
        {
            get;
            set;
        }
        abstract public string OpinionUserCode
        {
            get;
            set;
        }
        abstract public string OpinionConfirm
        {
            get;
            set;
        }
        abstract public string CaseCode
        {
            get;
            set;
        }
        virtual public bool IsUseTextArea
        {
            get { return false; }
            set { }
        }
        virtual public string TextOpinion
        {
            get { return ""; }
            set { }
        }
        virtual public string AuditValue
        {
            get { return ""; }
            set { }
        }
        virtual public string ProjectCode
        {
            get { return ""; }
            set { }
        }
        virtual public bool IsRdoCheck
        {
            get { return false; }
            set { }
        }
        virtual public bool IsUseTemplateOpinion
        {
            get { return false; }
            set { }
        }

        abstract public void InitControl();
        abstract public void SubmitData();
        abstract public void DeleteData();



    }

}