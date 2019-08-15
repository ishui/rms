//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�������ɵġ�
// �˴����ļ���App_Code\Migrated\workflowcontrol\Stub_workflowtoolbar_ascx_cs.cs���Ѵ��������а���һ�������� 
//���������ļ���workflowcontrol\workflowtoolbar.ascx.cs���������ࡰMigrated_WorkFlowToolbar���Ļ��ࡣ
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
    using System.Collections;
    using System.Text;
    using Rms.ORMap;
    using Rms.WorkFlow;

    abstract public class WorkFlowToolbar : System.Web.UI.UserControl
    {
        abstract public string ActCode
        {
            get;
            set;
        }

        abstract public string FlowName
        {
            get;
            set;
        }
        abstract public string ApplicationCode
        {
            get;
            set;
        }
        abstract public string SourceUrl
        {
            get;
            set;
        }
        abstract public bool UsePrint
        {
            get;
            set;
        }
        
        abstract public bool Scout
        {
            get;
            set;
        }
        abstract public string SystemUserCode
        {
            get;
            set;
        }
        abstract public string SystemUnitCode
        {
            get;
            set;
        }
        abstract public string Transactor
        {
            get;
            set;
        }
        abstract public string TransactUnit
        {
            get;
            set;
        }
        //abstract public bool OpinionConfirm
        //{
        //    get;
        //    set;
        //}
        //abstract public string Opinion
        //{
        //    get;
        //    set;
        //}
        abstract public ToolbarCommandType CommandType
        {
            get;
            set;
        }
        abstract public bool CloseButtonVisible
        {
            get;
            set;
        }
        abstract public bool SaveButtonVisible
        {
            get;
            set;
        }
        abstract public bool IsNew
        {
            get;
            set;
        }
        abstract public string ScriptCheck
        {
            get;
            set;
        }
        abstract public string ProjectCode
        {
            get;
            set;
        }
        abstract public bool BtnDeleteVisible
        {
            get;
            set;
        }
        abstract public bool BtnBlankOutVisible
        {
            get;
            set;
        }
        abstract public bool BtnPrintVisible
        {
            get;
            set;
        }
        abstract public string SendRoleNames
        {
            get;
        }
        abstract public string SendRoleItems
        {
            get;
        }
        abstract public int SendToTaskType
        {
            get;
        }
        abstract public string CaseCode
        {
            get;
            set;
        }
        abstract public string HandMadeValue
        {
            get;
        }
        abstract public bool CheckValue
        {
            get;
            set;
        }
        abstract public string CaseStatus
        {
            get;
            set;
        }
        abstract public string FlowOpinion
        {
            get;
            set;
        }
        abstract public bool IsAudit
        {
            get;
            set;
        }
        abstract public string AuditValue
        {
            get;
            set;
        }
        abstract public string RateValue
        {
            get;
            set;
        }
        abstract public string ProcedureCode
        {
            get;
            set;
        }
       
        abstract public void ToolbarDataBind();
        abstract public ModuleState GetModuleState(string ModuleId);
        abstract public ModuleState GetModuleState(string ModuleId, int flag);
        abstract public ModuleState GetModuleState1(string ModuleId, int flag);
       
        abstract public ModuleState GetConditionState(string RouterOrderCode);
        abstract public bool GetCurrentActSendButtonState(string UserCode);
        abstract public ModuleState GetActorModuleState(string ModuleId, string UserCode);
        abstract public ModuleState GetActorModuleState(string ModuleId, string UserCode, int flag);
     
        abstract public bool GetIsTaskUser(string UserCode);
        abstract public void SaveCasePropertyValue(string PropertyName, string PropertyValue);
        abstract public string GetCasePropertyValue(string PropertyName);
        abstract public void Save();
        abstract public void Send();
        abstract public void Back();
        abstract public void Return();
        abstract public void SaveOpinion();
        abstract public void BackTop();
        abstract public void SignIn(StandardEntityDAO dao);
        abstract public void Finish();
        abstract public void BlankOut();
        abstract public void MakeCopy();
        abstract public void TaskFinish();
        abstract public void Delete();
        abstract public void SendMail(string UserCodes, string Title, string Body);
        abstract public event EventHandler ToolbarCommand;


    }
}
