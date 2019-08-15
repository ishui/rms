//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分生成的。
// 此代码文件“App_Code\Migrated\workflowcontrol\Stub_workflowformopinion_ascx_cs.cs”已创建，其中包含一个抽象类 
//，该类在文件“workflowcontrol\workflowformopinion.ascx.cs”中用作类“Migrated_WorkFlowFormOpinion”的基类。
// 此项允许您的项目中的所有代码文件引用该基类。
// 有关此代码模式的更多信息，请参考 http://go.microsoft.com/fwlink/?LinkId=46995 
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