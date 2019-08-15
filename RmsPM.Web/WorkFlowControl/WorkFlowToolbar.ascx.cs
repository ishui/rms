//===========================================================================
// ���ļ�����Ϊ ASP.NET 2.0 Web ��Ŀת����һ�����޸ĵġ�
// �����Ѹ��ģ��������޸�Ϊ���ļ���App_Code\Migrated\workflowcontrol\Stub_workflowtoolbar_ascx_cs.cs���ĳ������ 
// �̳С�
// ������ʱ�������������� Web Ӧ�ó����е�������ʹ�øó������󶨺ͷ��� 
// ��������ҳ��
// ����������ҳ��workflowcontrol\workflowtoolbar.ascx��Ҳ���޸ģ��������µ�������
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
    using System.Configuration;

    using Rms.ORMap;
    using Rms.WorkFlow;


    /// *******************************************************************************************
    /// <summary>
    ///		WorkFlowToolbar ��ժҪ˵����������Ӧ�ù�������
    /// </summary>
    /// *******************************************************************************************
    public partial class Migrated_WorkFlowToolbar : WorkFlowToolbar
    {
        #region -  ��Ԫ��  ------------------------------------------------------------

        /// <summary>
        /// ���̶������
        /// </summary>
        protected System.Web.UI.HtmlControls.HtmlInputHidden HiddenProcedureCode;
        /// <summary>
        /// ��¼ϵͳ�û�
        /// </summary>
        protected System.Web.UI.HtmlControls.HtmlInputHidden HiddenSystemUserCode;
        /// <summary>
        /// ���͵ȴ���ʶ
        /// </summary>
        #endregion ----------------------------------------------------------------------

        #region -  ˽�г�Ա  ------------------------------------------------------------

        /// <summary>
        /// ���̶�������
        /// </summary>
        private Procedure _Procedure = null;
        /// <summary>
        /// ���ͻ�ǩ��������
        /// </summary>
        private TaskActor _TaskActor = null;
        /// <summary>
        /// ����������
        /// </summary>
        private Task _Task = null;
        /// <summary>
        /// ����ʵ������
        /// </summary>
        private WorkCase _WorkCase = null;
        /// <summary>
        /// ����ʵ������
        /// </summary>
        private Act _Act = null;
        /// <summary>
        /// ����ʵ������
        /// </summary>
        private string _ActCode = null;
        /// <summary>
        /// ����������
        /// </summary>
        private string _FlowName = null;
        /// <summary>
        /// ҵ��ʵ������
        /// </summary>
        private string _ApplicationCode = null;
        /// <summary>
        /// ��Դ·��
        /// </summary>
        private string _SourceUrl = null;
        /// <summary>
        /// ϵͳ�û�
        /// </summary>
        private string _SystemUserCode = null;
        /// <summary>
        /// ϵͳ�û���λ�����ţ�
        /// </summary>
        private string _SystemUnitCode = null;
        /// <summary>
        /// ������
        /// </summary>
        private string _Transactor = null;
        /// <summary>
        /// ���쵥λ�����ţ�
        /// </summary>
        private string _TransactUnit = null;
        /// <summary>
        /// ���̼�ر�ʶ
        /// </summary>
        private bool _Scout = false;
        private string _ProcedureCode = null;
        /// <summary>
        /// ��������
        /// </summary>
        private ToolbarCommandType _CommandType = ToolbarCommandType.Unbeknown;
        /// <summary>
        /// ��ǩ��Ա�б�
        /// </summary>
        private Hashtable _MeetingUsers = new Hashtable();


        #endregion ----------------------------------------------------------------------

        #region -  ���Լ���  ------------------------------------------------------------

        /// <summary>
        /// ����ʵ������
        /// </summary>
        override public string ActCode
        {
            get
            {
                if (_ActCode == null)
                {
                    if (this.ViewState["_ActCode"] != null)
                        return this.ViewState["_ActCode"].ToString();
                    return "";
                }
                return _ActCode;
            }
            set
            {
                _ActCode = value;
                this.ViewState["_ActCode"] = value;
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        override public string FlowName
        {
            get
            {
                if (_FlowName == null)
                {
                    if (this.ViewState["_FlowName"] != null)
                        return this.ViewState["_FlowName"].ToString();
                    return "";
                }
                return _FlowName;
            }
            set
            {
                _FlowName = value;
                this.ViewState["_FlowName"] = value;
            }
        }
        /// <summary>
        /// ҵ��ʵ������
        /// </summary>
        override public string ApplicationCode
        {
            get
            {
                if (_ApplicationCode == null)
                {
                    if (this.ViewState["_ApplicationCode"] != null)
                        return this.ViewState["_ApplicationCode"].ToString();
                    return "";
                }
                return _ApplicationCode;
            }
            set
            {
                _ApplicationCode = value;
                this.ViewState["_ApplicationCode"] = value;
            }
        }
        /// <summary>
        /// ��Դ·��
        /// </summary>
        override public string SourceUrl
        {
            get
            {
                if (_SourceUrl == null)
                {
                    if (this.ViewState["_SourceUrl"] != null)
                        return this.ViewState["_SourceUrl"].ToString();
                    return "../../WorkFlowControl/";
                }
                return _SourceUrl;
            }
            set
            {
                _SourceUrl = value;
                this.ViewState["_SourceUrl"] = value;
            }
        }
        /// <summary>
        /// ���̼�ر�ʶ
        /// </summary>
        override public bool Scout
        {
            get
            {
                if (_Scout == false)
                {
                    if (this.ViewState["_Scout"] != null)
                        return (bool)this.ViewState["_Scout"];
                }
                return _Scout;
            }
            set
            {
                _Scout = value;
                this.ViewState["_Scout"] = value;
            }
        }
        /// <summary>
        /// ϵͳ�û�����
        /// </summary>
        override public string SystemUserCode
        {
            get
            {
                if (_SystemUserCode == null)
                {
                    if (this.ViewState["_SystemUserCode"] != null)
                        return this.ViewState["_SystemUserCode"].ToString();
                    throw new Exception("�Ҳ�����¼ϵͳ�û���");
                }
                return _SystemUserCode;
            }
            set
            {
                _SystemUserCode = value;
                this.ViewState["_SystemUserCode"] = value;
            }
        }
        /// <summary>
        /// ϵͳ�û���λ�����ţ�����
        /// </summary>
        override public string SystemUnitCode
        {
            get
            {
                if (_SystemUnitCode == null)
                {
                    if (this.ViewState["_SystemUnitCode"] != null)
                        return this.ViewState["_SystemUnitCode"].ToString();
                    return "";
                }
                return _SystemUnitCode;
            }
            set
            {
                _SystemUnitCode = value;
                this.ViewState["_SystemUnitCode"] = value;
            }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        override public string ProjectCode
        {
            get
            {
                if (this.ViewState["_ProjectCode"] == null)
                    return "";
                return this.ViewState["_ProjectCode"].ToString();
            }
            set
            {
                this.ViewState["_ProjectCode"] = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        override public string Transactor
        {
            get
            {
                if (_Transactor == null)
                {
                    if (this.ViewState["_Transactor"] != null)
                        return this.ViewState["_Transactor"].ToString();
                    return "";
                }
                return _Transactor;
            }
            set
            {
                _Transactor = value;
                this.ViewState["_Transactor"] = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        override public string ProcedureCode
        {
            get
            {
                if (_ProcedureCode == null)
                {
                    if (this.ViewState["_ProcedureCode"] != null)
                        return this.ViewState["_ProcedureCode"].ToString();
                    return "";
                }
                return _ProcedureCode;
            }
            set
            {
                _ProcedureCode = value;
                this.ViewState["_ProcedureCode"] = value;
            }
        }


        /// <summary>
        /// ���쵥λ�����ţ�
        /// </summary>
        override public string TransactUnit
        {
            get
            {
                if (_TransactUnit == null)
                {
                    if (this.ViewState["_TransactUnit"] != null)
                        return this.ViewState["_TransactUnit"].ToString();
                    return "";
                }
                return _TransactUnit;
            }
            set
            {
                _TransactUnit = value;
                this.ViewState["_TransactUnit"] = value;
            }

        }

        /// <summary>
        /// �Ƿ�����ͨ��Ȩ����ʾ��ӡ��ť
        /// </summary>
        override public bool UsePrint
        {
            get
            {
                if (this.ViewState["_UsePrint"] != null)
                {
                    if (this.ViewState["_UsePrint"].ToString().ToLower() == "true")
                    {
                        return true;
                    }
                }
                return false;
            }
            set
            {
                ViewState["_UsePrint"] = value.ToString();


            }
        }




        /// <summary>
        /// ��ǩ��Ա�б�
        /// </summary>
        private Hashtable MeetingUsers
        {
            get
            {
                return _MeetingUsers;
            }
            set
            {
                _MeetingUsers = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        override public ToolbarCommandType CommandType
        {
            get
            {
                return _CommandType;
            }
            set
            {
                _CommandType = value;
            }
        }
        /// <summary>
        /// �رհ�ť��ʾ״̬
        /// </summary>
        override public bool CloseButtonVisible
        {
            get
            {
                return this.btnClose.Visible;
            }
            set
            {
                this.btnClose.Visible = value;
            }
        }
        /// <summary>
        /// ���水ť��ʾ״̬
        /// </summary>
        override public bool SaveButtonVisible
        {
            get
            {
                return this.btnSave.Visible;
            }
            set
            {
                this.btnSave.Visible = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ϊ��������
        /// </summary>
        override public bool IsNew
        {
            get
            {
                if (this.HiddenCaseCode.Value == "IsNew")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                    this.HiddenCaseCode.Value = "IsNew";
                else
                    this.HiddenCaseCode.Value = "";
            }
        }
        /// <summary>
        /// �ͻ���У��ű������磺ScriptCheck = "javascript:if(BiddingCheckSubmit()) ";��
        /// </summary>
        override public string ScriptCheck
        {
            get
            {
                return this.btnSave.Attributes["OnClick"].ToString();
            }
            set
            {
                this.btnSave.Attributes["OnClick"] = value + "{" + this.btnSave.Attributes["OnClick"] + "}";
                this.btnSend.Attributes["OnClick"] = value + "{" + this.btnSend.Attributes["OnClick"] + "}";
            }
        }

        /// <summary>
        /// ɾ����ť�Ƿ���ʾ
        /// </summary>
        override public bool BtnDeleteVisible
        {
            get
            {
                return this.btnDelete.Visible;
            }
            set
            {
                if (this.ViewState["_CaseCode"] == null)
                    this.btnDelete.Visible = false;
                else
                    this.btnDelete.Visible = value;
            }
        }

        /// <summary>
        /// ���ϰ�ť�Ƿ���ʾ
        /// </summary>
        override public bool BtnBlankOutVisible
        {
            get
            {
                return this.btnBlankOut.Visible;
            }
            set
            {
                if (this.ViewState["_CaseCode"] == null)
                    this.btnBlankOut.Visible = false;
                else
                    this.btnBlankOut.Visible = value;
            }
        }

        /// <summary>
        /// ��ӡ��ť�Ƿ���ʾ
        /// </summary>
        override public bool BtnPrintVisible
        {
            get
            {
                return this.btnPrint.Visible;
            }
            set
            {
                this.btnPrint.Visible = value;
            }
        }
        /// <summary>
        /// ѡ����·�ɽ�ɫ��Ա�б�
        /// </summary>
        override public string SendRoleNames
        {
            get
            {
                string roleNames = "";
                string userCodes = this.HiddenSelectUserCodes.Value;

                foreach (string sss in userCodes.Split(new char[] { ';' }))
                {
                    if (sss != "")
                    {
                        string[] sus = sss.Split(new char[] { ',' });
                        if (sus[0] != "")
                        {
                            if (roleNames.Length > 0)
                                roleNames += "," + sus[3];
                            else
                                roleNames += sus[3];
                        }
                    }
                }

                return roleNames;
            }
        }
        /// <summary>
        /// ѡ����·�ɽ�ɫ��Ա�����б�(�û����룬��ɫ���룬�û����ƣ���ɫ���ƣ�...)
        /// </summary>
        override public string SendRoleItems
        {
            get
            {
                string RoleItemsStr = this.HiddenSelectUserCodes.Value + this.HiddenCopyUsers.Value;
                string ReturnStr = "";

                if (RoleItemsStr.IndexOf("taskActorName=") > 0)
                {

                    foreach (string tmpStr in RoleItemsStr.Split(';'))
                    {
                        string[] ud_saTmp = tmpStr.Split(',');
                        if (ud_saTmp.Length == 4)
                        {
                            ReturnStr += ud_saTmp[0] + ",";
                            ReturnStr += ud_saTmp[3] + ",";
                            ReturnStr += ud_saTmp[2] + ",";
                            ReturnStr += ud_saTmp[1].Replace("taskActorName=", "") + ";";
                        }
                    }
                }
                else
                {
                    ReturnStr = this.HiddenSelectUserCodes.Value + this.HiddenCopyUsers.Value;
                }

                return ReturnStr;
            }
        }
        private int _SendToTaskType = -1;
        /// <summary>
        /// ·����һ����ڵ�����(Ĭ��Ϊ -1;Task����: 0 "һ��ڵ�"; 1 "��ʼ"; 2 "����"; 3 "�������"; 4 "��������"; 5 "��ǩ�ڵ�";)
        /// </summary>
        override public int SendToTaskType
        {
            get
            {
                return _SendToTaskType;
            }
        }
        /// <summary>
        /// ����ʵ������
        /// </summary>
        override public string CaseCode
        {
            get
            {
                if (this.ViewState["_CaseCode"] == null)
                    return "";
                return this.ViewState["_CaseCode"].ToString();
            }
            set
            {
                this.ViewState["_CaseCode"] = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        override public string HandMadeValue
        {
            get
            {
                if (this.RadioHandmade.Visible == false)
                {
                    return "";
                }
                else
                {
                    return this.RadioHandmade.SelectedValue;
                }
            }
        }
        override public bool CheckValue
        {
            get
            {
                if (this.ViewState["_CheckValue"] == null)
                    return true;
                return (bool)this.ViewState["_CheckValue"];
            }
            set
            {
                this.ViewState["_CheckValue"] = value;
            }
        }
        override public string CaseStatus
        {
            get
            {
                if (this.ViewState["_StatusCase"] == null)
                    return "";
                return this.ViewState["_StatusCase"].ToString();
            }
            set
            {
                this.ViewState["_StatusCase"] = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        override public string FlowOpinion
        {
            get
            {
                return this.HiddenFlowOpinion.Value;
            }
            set
            {
                this.HiddenFlowOpinion.Value = value;
            }
        }
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        override public bool IsAudit
        {
            get
            {
                return (this.HiddenIsAudit.Value == "1");
            }
            set
            {
                if (value)
                    this.HiddenIsAudit.Value = "1";
                else
                    this.HiddenIsAudit.Value = "0";
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        override public string AuditValue
        {
            get
            {
                return this.HiddenAuditValue.Value;
            }
            set
            {
                this.HiddenAuditValue.Value = value;
            }
        }

        /// <summary>
        /// �����̶� trueΪ����  �����ˣ������� �������ڣ�2006-11-29 
        /// </summary>
        override public string RateValue
        {
            get
            {
                return this.HiddenRateValue.Value;
            }
            set
            {
                this.HiddenRateValue.Value = value;
            }
        }

        #endregion ----------------------------------------------------------------------

        #region -  �¼��ش�  ------------------------------------------------------------

        /// <summary>
        /// �����������¼��ش�
        /// </summary>
        override public event EventHandler ToolbarCommand;

        #endregion ----------------------------------------------------------------------

        #region -  Ԫ�ط������� ---------------------------------------------------------

        /// ****************************************************************************
        /// <summary>
        /// ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>ֻ��Ҫ�� actCode ���Ը�ֵ</remarks>
        /// ****************************************************************************
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadData();
                    this.btnSend.Attributes["ClientID"] = this.ClientID;
                    this.btnBack.Attributes["ClientID"] = this.ClientID;
                    this.btnBackTop.Attributes["ClientID"] = this.ClientID;
                    this.btnBackEx.Attributes["ClientID"] = this.ClientID;
                    this.btnOpinion.Attributes["ClientID"] = this.ClientID;
                    this.btnMakeCopy.Attributes["ClientID"] = this.ClientID;
                    this.btnFinish.Attributes["ClientID"] = this.ClientID;

                    //this.btnSend.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectRouterControl('Send','ѡ��·��');";
                    //this.btnBack.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectBackControl('back','ѡ���˻���һ��');";

                    //this.btnBackTop.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectBackControl('backTop','ѡ���˻ؾ�����');";
                    //this.btnBackEx.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectBackControl('backEx','ѡ���˻�');";
                    //this.btnOpinion.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectOpinionControl('Opinion','��д���');";
                }
                //InitUserControl();

                //OutputScript();
                this.scriptspan.EnableViewState = false;
                this.btnDelete.Attributes["OnClick"] = "javascript:if(confirm('ȷʵҪɾ����ǰ������')) ";
                //this.btnBackTop.Attributes["OnClick"] = "javascript:if(confirm('ȷʵҪ�˻ظ���������')) ";
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ǩ�հ�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnSignIn_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.SignIn;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// �ջذ�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnReturn_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.Return;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// ���水ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.Save;
                    this.ToolbarCommand(this, EventArgs.Empty);
                    if (this.HiddenNewFlow.Value == "IsNew")
                    {
                        this.scriptspan.InnerHtml = "<script language=\"javascript\">selectRouterControl('Send','ѡ��·��')</script>";
                        ToolbarDataBind();
                        this.btnSend.Attributes["OnClick"] = "GetObjectInControl('" + this.ClientID + "','btnHiddenForwardOpinion').onclick();selectRouterControl('Send','ѡ��·��');";
                        this.HiddenCaseCode.Value = "";
                        this.HiddenNewFlow.Value = "";
                    }
                    else
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "����ɹ�!"));
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// ������ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnFinish_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.Finish;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

        }
        /// ****************************************************************************
        /// <summary>
        /// ���ϰ�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnBlankOut_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.BlankOut;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

        }

        /// ****************************************************************************
        /// <summary>
        /// �����ύ��ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnHiddenSend_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                this.SaveOpinion();
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.Send;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

        }
        /// ****************************************************************************
        /// <summary>
        /// �˻��ύ��ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnHiddenBack_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                this.SaveOpinion();
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.Back;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

        }

        /// ****************************************************************************
        /// <summary>
        /// �;����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        private void btnBackTop_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                this.SaveOpinion();
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.BackTop;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }


        /// ****************************************************************************
        /// <summary>
        /// �����ύ��ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnHiddenMakeCopy_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                this.SaveOpinion();
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.MakeCopy;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

        }
        /// ****************************************************************************
        /// <summary>
        /// ��ɰ�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnTaskFinish_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                //////////////////////���ӵĻ�ǩ���״̬�����߼�///////////////////////////////////////
                //�޸����ڣ�2006-09-28
                //�޸��ˣ�clm
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Act currentAct = workCase.GetAct(this.ActCode);
                if (StatusEndCount(this.ViewState["_CaseCode"].ToString(), currentAct.ToTaskCode) == 1)
                {
                    this.btnTaskFinish.Visible = false;         //��ɰ�ť

                    DataSet ds5 = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                    DataView dv5 = new DataView(ds5.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", currentAct.ToTaskCode), "SortID", DataViewRowState.CurrentRows);

                    int FinishTaskCount = 0;
                    int OtherTaskCount = 0;
                    for (int z = 0; z < dv5.Count; z++)
                    {
                        string TaskCode = (String)dv5[z].Row["ToTaskCode"];
                        Task endtask = procedure.GetTask(TaskCode);

                        if (endtask.TaskType == 2)
                        {
                            FinishTaskCount++;
                        }
                        else
                        {
                            OtherTaskCount++;
                        }
                    }

                    this.btnFinish.Visible = (FinishTaskCount != 0);
                    this.btnSend.Visible = (OtherTaskCount != 0);
                    Page.RegisterClientScriptBlock("finishtask", "<script>alert('��Ǹ����������������������Ѿ���ɴ��������²�����');</script>");
                }///////////////////////////////////////////////////////////////////////////////////////
                else
                {
                    ToolbarCheck();
                    if (this.CheckValue)
                    {
                        this.CommandType = ToolbarCommandType.TaskFinish;
                        this.ToolbarCommand(this, EventArgs.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

        }
        /// ****************************************************************************
        /// <summary>
        /// ���������ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnHiddenOpinion_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.Opinion;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }


        /// ****************************************************************************
        /// <summary>
        /// ���͵Ȱ�ť����֮ǰ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnHiddenForwardOpinion_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.OpinionForward;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }


        /// ****************************************************************************
        /// <summary>
        /// ɾ����ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnDelete_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                ToolbarCheck();
                if (this.CheckValue)
                {
                    this.CommandType = ToolbarCommandType.Delete;
                    this.ToolbarCommand(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }

        #endregion ----------------------------------------------------------------------

        #region -  ���غ���  ------------------------------------------------------------

        /// ****************************************************************************
        /// <summary>
        /// ���ݼ���
        /// </summary>
        /// ****************************************************************************
        private void LoadData()
        {
        }

        /// <summary>
        /// �жϸ����������Ƿ��������ͬ��������Ŀ
        /// </summary>
        private void InitAudit()
        {
            int ud_iOpinionStateCount = 2;
            ModuleState[] ud_wfmaWorkFlowModuleState = new ModuleState[ud_iOpinionStateCount];
            Task currentTask = null;
            if (this.CaseStatus == "")
            {
                Procedure procedure = DefinitionManager.GetProcedureDifinition(this.ViewState["_ProcedureCode"].ToString(), true);
                if (procedure == null)
                {
                    LogHelper.WriteLog("ȡ������Ӧ����");
                    throw new Exception("������������ȡ������Ӧ����");
                }
                currentTask = Rms.WorkFlow.DefinitionManager.GetFirstTask(procedure);
                if (currentTask == null)
                {
                    LogHelper.WriteLog("����ȡ������ʼ�ڵ�");
                    throw new Exception("���������������鿪ʼ�ڵ������");
                }

                for (int i = 0; i < ud_iOpinionStateCount; i++)
                {
                    ud_wfmaWorkFlowModuleState[i] = this.GetModuleState(currentTask.OpinionType, i);
                }
            }
            else
            {


                string AppActCode = this.ActCode;

                string OpinionType = RmsPM.BLL.WorkFlowRule.GetTaskOpinionTypeByActCode(AppActCode);

                for (int i = 0; i < ud_iOpinionStateCount; i++)
                {
                    ud_wfmaWorkFlowModuleState[i] = this.GetModuleState(OpinionType, i);
                }
            }


            if (ud_wfmaWorkFlowModuleState[1] != null && ud_wfmaWorkFlowModuleState[1] == ModuleState.Operable)//��ǰ��������������Ӧ�ĸ�������Ϊ*|3
                this.IsAudit = true;

        }
        /// ****************************************************************************
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// ****************************************************************************
        private void InitUserControl()
        {
            if (this.ActCode == "")
            {
                if (this.CaseCode == "")//δ��ʼ����
                {
                    LoadNewFlow();
                }
                else //�Ѿ���ɵ�����
                {
                    DAL.QueryStrategy.WorkFlowCaseStrategyBuilder sb = new DAL.QueryStrategy.WorkFlowCaseStrategyBuilder();
                    sb.AddStrategy(new Strategy(DAL.QueryStrategy.WorkFlowCaseStrategyName.CaseCode, this.CaseCode));
                    string sql = sb.BuildMainQueryString();
                    string StatusTemp = "";
                    QueryAgent QA = new QueryAgent();
                    EntityData entity = QA.FillEntityData("WorkFlowCase", sql);
                    if (entity.HasRecord())
                    {
                        StatusTemp = entity.CurrentRow["Status"].ToString();
                        CaseStatus = StatusTemp;
                        this.ViewState["_ProcedureCode"] = entity.CurrentRow["ProcedureCode"].ToString();
                    }
                    else //δ��ʼ������
                    {
                        LoadNewFlow();
                    }
                    QA.Dispose();
                    entity.Dispose();

                    if (StatusTemp == "End")//�Ѿ���ɵ�����
                    {
                        LoadEndFlow();
                    }
                    else if (StatusTemp == "Begin")//;�е�����
                    {
                        string tempactcode = "";
                        WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                        Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                        System.Collections.IDictionaryEnumerator ieact = workCase.GetActEnumerator();
                        while (ieact.MoveNext())
                        {
                            Act act = (Act)ieact.Value;
                            Task currentTask = procedure.GetTask(act.ToTaskCode);
                            if (act.Status == ActStatus.Begin)
                            {
                                System.Collections.IDictionaryEnumerator ieactor = act.GetActUserEnumerator();
                                while (ieactor.MoveNext())
                                {
                                    ActUser au = (ActUser)ieactor.Value;
                                    if (au.ActUserCode == this.SystemUserCode)
                                    {
                                        if (act.Copy != 1)
                                        {
                                            if (currentTask.TaskType != 5)
                                                this.ActCode = act.ActCode;
                                            else if (currentTask.TaskType == 5 && currentTask.TaskMeetType != "1")
                                                this.ActCode = act.ActCode;
                                            else if (currentTask.TaskType == 5 && currentTask.TaskMeetType == "1" && act.ActUnitCode.Substring(0, 2) != "no")
                                                this.ActCode = act.ActCode;
                                        }
                                    }
                                }
                            }
                            else if (act.Status == ActStatus.DealWith)
                            {
                                if (act.ActUserCode == this.SystemUserCode)
                                {
                                    if (act.Copy != 1)
                                    {
                                        this.ActCode = act.ActCode;
                                    }
                                }
                            }
                            tempactcode = act.ActCode;
                        }

                        if (this.ActCode == "")
                        {
                            this.ActCode = tempactcode;
                            this._Scout = true;
                        }

                        LoadBeginFlow();
                    }

                }
            }
            else//;�е�����
            {
                LoadBeginFlow();
            }
            if (GetModuleState("FormPrint") == ModuleState.Operable)
            {
                this.btnPrintForm.Visible = true;
            }
            else
            {
                this.btnPrintForm.Visible = false;
            }

            User user = null;
            if ((Session["User"] == null) && (Request["DebugUser"] + "" != ""))
            {
                Session["User"] = new User(Request["DebugUser"] + "");
            }

            if ((Session["User"] == null) && (ConfigurationSettings.AppSettings["IsDebug"] == "1") && (ConfigurationSettings.AppSettings["DebugUser"] != ""))
            {
                Session["User"] = new User(ConfigurationSettings.AppSettings["DebugUser"]);
            }



            if (Session["User"] != null)
            {
                user = (User)Session["User"];


                if (UsePrint)
                {
                    if (user.HasRight("1109") && this.Scout)
                    {
                        this.BtnPrintVisible = true;
                    }
                }
            }
            //InitHandMade();
        }
        /// <summary>
        /// δ��ʼ������
        /// </summary>
        private void LoadNewFlow()
        {
            this.btnSignIn.Visible = false;            //ǩ�հ�ť
            this.btnSave.Visible = true;               //���水ť
            this.btnSend.Visible = true;               //���Ͱ�ť
            this.btnTaskFinish.Visible = false;        //��ɰ�ť
            this.btnFinish.Visible = false;            //������ť
            this.btnBack.Visible = false;              //�˻ذ�ť
            this.btnBackTop.Visible = false;           //�;����˰�ť
            this.btnOpinion.Visible = false;           //������水ť
            this.btnBackEx.Visible = false;            //�����˻�
            this.msgbutton.Visible = false;            //��Ϣ�鿴
            this.btnReturn.Visible = false;            //�ջ�
            this.btnBlankOut.Visible = false;          //���ϰ�ť

            this.HiddenCaseCode.Value = "IsNew";

            this.ViewState["_ProcedureCode"] = BLL.WorkFlowRule.GetProcedureCodeByName(this.FlowName, this.ProjectCode);
            this.btnMakeCopy.Visible = false;             //���Ͱ�ť
            this.btnSend.Attributes["OnClick"] = "document.all(\"" + this.ID + "_HiddenCaseCode\").value=\"IsNew\";document.all(\"" + this.ID + "_HiddenNewFlow\").value=\"IsNew\";document.all(\"" + this.ID + "_btnSave\").click();";
        }
        /// <summary>
        /// �Ѿ�����������
        /// </summary>
        private void LoadEndFlow()
        {
            this.btnSignIn.Visible = false;            //ǩ�հ�ť
            this.btnSave.Visible = false;              //���水ť
            this.btnSend.Visible = false;              //���Ͱ�ť
            this.btnTaskFinish.Visible = false;        //��ɰ�ť
            this.btnFinish.Visible = false;            //������ť
            this.btnMakeCopy.Visible = false;          //���Ͱ�ť
            this.btnBack.Visible = false;              //�˻ذ�ť
            this.btnBackTop.Visible = false;           //�;����˰�ť
            this.btnOpinion.Visible = false;           //������水ť
            this.btnBackEx.Visible = false;            //�����˻�
            this.msgbutton.Visible = false;            //��Ϣ�鿴
            this.btnReturn.Visible = false;            //�ջ�
            this.btnBlankOut.Visible = false;          //���ϰ�ť



            this.FormNoSpan.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;<b><font color=\"red\">��ˮ�ţ�" + BLL.WorkFlowRule.GetWorkFlowNumber(this.ViewState["_CaseCode"].ToString()) + "</font></b>";
            this.ActCode = BLL.WorkFlowRule.GetLoginUserLastActCode(this.SystemUserCode, this.CaseCode);
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
            this.ViewState["_ProcedureCode"] = workCase.ProcedureCode;
            this.CaseStatus = workCase.Status.ToString();
        }
        /// <summary>
        /// ;�е�����
        /// </summary>
        private void LoadBeginFlow()
        {
            this.HiddenCaseCode.Value = "";
            //////���ݶ�������������̴���//////
            DAL.QueryStrategy.WorkFlowActStrategyBuilder sb = new DAL.QueryStrategy.WorkFlowActStrategyBuilder();
            sb.AddStrategy(new Strategy(DAL.QueryStrategy.WorkFlowActStrategyName.ActCode, this.ActCode));

            string sql = sb.BuildMainQueryString();

            QueryAgent QA = new QueryAgent();
            EntityData entity = QA.FillEntityData("WorkFlowAct", sql);
            this.CaseCode = entity.CurrentRow["CaseCode"].ToString();
            this.ApplicationCode = entity.CurrentRow["ApplicationCode"].ToString();
            QA.Dispose();
            entity.Dispose();

            ////////////////////////////////////

            this.FormNoSpan.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;<b><font color=\"red\">��ˮ�ţ�" + BLL.WorkFlowRule.GetWorkFlowNumber(this.ViewState["_CaseCode"].ToString()) + "</font></b>";

            if (this._Scout == true) //���̼��
            {
                this.btnSignIn.Visible = false;            //ǩ�հ�ť
                this.btnSave.Visible = false;              //���水ť
                this.btnSend.Visible = false;              //���Ͱ�ť
                this.btnTaskFinish.Visible = false;        //��ɰ�ť
                this.btnFinish.Visible = false;            //������ť
                this.btnMakeCopy.Visible = false;          //���Ͱ�ť
                this.btnBack.Visible = false;              //�˻ذ�ť
                this.btnBackTop.Visible = false;           //�;����˰�ť
                this.btnOpinion.Visible = false;           //������水ť
                this.btnBackEx.Visible = false;            //�����˻�
                this.msgbutton.Visible = false;            //��Ϣ�鿴
                this.btnBlankOut.Visible = false;          //���ϰ�ť

                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                this.CaseStatus = workCase.Status.ToString();
                this.ViewState["_ProcedureCode"] = workCase.ProcedureCode;
                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Act currentAct = workCase.GetAct(this.ActCode);                

                if (currentAct.FromUserCode == this.SystemUserCode && currentAct.Status == Rms.WorkFlow.ActStatus.Begin)
                {
                    if (procedure.GetTask(currentAct.FromTaskCode).TaskType == 0 && procedure.GetTask(currentAct.ToTaskCode).TaskType == 0)
                        this.btnReturn.Visible = true;            //�ջ�
                    else
                        this.btnReturn.Visible = false;           //�ջ�
                }
                else
                {
                    this.btnReturn.Visible = false;            //�ջ�
                }

                if (currentAct.ActUserCode != this.SystemUserCode)
                {
                    this.ActCode = BLL.WorkFlowRule.GetLoginUserLastActCode(this.SystemUserCode, this.CaseCode);

                }
            }
            else
            {
                this.btnReturn.Visible = false;            //�ջ�

                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                this.CaseStatus = workCase.Status.ToString();


                //////////////////////////////////////
                //�����������
                System.Collections.IDictionaryEnumerator ieo = workCase.GetOpinionEnumerator();
                while (ieo.MoveNext())
                {
                    Opinion Flowopinion = (Opinion)ieo.Value;
                    if (Flowopinion.ApplicationCode == this.ActCode)
                        this.HiddenFlowOpinion.Value = Flowopinion.OpinionText;
                }
                //////////////////////////////////////

                ///////////////�жϵȴ�///////////////
                bool IsWait = false;
                System.Collections.IDictionaryEnumerator ie = workCase.GetActEnumerator();
                while (ie.MoveNext())
                {
                    Act act = (Act)ie.Value;
                    if (act.CopyFromActCode == this.ActCode && act.Status != Rms.WorkFlow.ActStatus.End)
                    {
                        this.btnSend.Attributes["OnClick"] = "alert('�ȴ��������');return false;";
                        this.btnTaskFinish.Attributes["OnClick"] = "alert('�ȴ��������');return false;";
                        this.btnFinish.Attributes["OnClick"] = "alert('�ȴ��������');return false;";
                        IsWait = true;
                    }
                }
                //////////////////////////////////////
                Act currentAct = workCase.GetAct(this.ActCode);
                if (currentAct.CopyFromActCode != "")
                    this.CancelCopyReturn(workCase, currentAct);
                //this.Label1.Text ="<script language='javascript'>document.title = '<FONT color=\"gray\">"+ currentAct.FromTaskName+" --> </FONT><b>"+currentAct.ToTaskName+"</b>';</script>";
                this.Label1.Text = "<script language='javascript'>document.title = '  " + currentAct.FromTaskName + " --> " + currentAct.ToTaskName + "';</script>";

                this.ViewState["_ProcedureCode"] = workCase.ProcedureCode;

                if (currentAct.ProjectCode != null)
                {
                    this.msgbutton.Visible = true;
                    string msg = currentAct.ProjectCode.ToString();
                    this.msgdiv.InnerHtml = msg.Replace("\n", "<br>");

                    //��֤��ǰ��Ϣ�Ƿ���д
                    if (msg.IndexOf("��") >= msg.Trim().Length - 1)
                    {
                        this.msgdiv.Style["display"] = "none";
                    }
                    else
                    {
                        this.msgdiv.Style["display"] = "";
                    }
                }
                else
                {
                    this.msgbutton.Visible = false;
                }
                if (currentAct.Status == ActStatus.Begin)//δǩ��״̬
                {
                    this.btnSignIn.Visible = true;             //ǩ�հ�ť
                    this.btnSave.Visible = false;              //���水ť
                    this.btnSend.Visible = false;              //���Ͱ�ť
                    this.btnMakeCopy.Visible = false;          //���Ͱ�ť
                    this.btnTaskFinish.Visible = false;        //��ɰ�ť
                    this.btnFinish.Visible = false;            //������ť
                    this.btnBack.Visible = false;              //�˻ذ�ť
                    this.btnBackTop.Visible = false;           //�;����˰�ť
                    this.btnOpinion.Visible = false;           //������水ť
                    this.btnBackEx.Visible = false;            //�����˻�
                    //this.msgbutton.Visible = true;            //��Ϣ�鿴
                    this.btnBlankOut.Visible = false;          //���ϰ�ť
                    if (System.Configuration.ConfigurationSettings.AppSettings["WorkFlowSignIn"] == "0")
                    {
                        try
                        {
                            this.CommandType = ToolbarCommandType.SignIn;
                            this.ToolbarCommand(this, EventArgs.Empty);
                        }
                        catch (Exception ex)
                        {
                            ApplicationLog.WriteLog(this.ToString(), ex, "");
                        }
                    }
                }
                else if (currentAct.Status == ActStatus.DealWith)//�Ѿ�ǩ��״̬
                {
                    //���ͻظ����
                    if (currentAct.CopyFromActCode != "")
                        this.CancelCopyReturn(workCase, currentAct);

                    if (currentAct.Copy == 1)
                    {
                        Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                        Task currentTask = procedure.GetTask(currentAct.ToTaskCode);

                        this.btnSignIn.Visible = false;             //ǩ�հ�ť
                        this.btnSend.Visible = false;              //���Ͱ�ť
                        if (currentTask.GetTaskActor(currentAct.TaskActorID).TaskActorName == "1")
                        {
                            this.btnMakeCopy.Visible = true;          //���Ͱ�ť
                            this.btnMakeCopy.Value = currentTask.TaskActorType;
                        }
                        else
                        {
                            this.btnMakeCopy.Visible = false;          //���Ͱ�ť
                        }

                        this.btnTaskFinish.Visible = false;        //��ɰ�ť
                        this.btnFinish.Visible = true;            //������ť
                        this.btnFinish.Value = " �� �� ";
                        this.btnBack.Visible = false;              //�˻ذ�ť
                        this.btnBackTop.Visible = false;           //�;����˰�ť
                        this.btnOpinion.Visible = true;           //������水ť
                        this.btnBackEx.Visible = false;            //�����˻�
                        this.btnBlankOut.Visible = false;          //���ϰ�ť
                    }
                    else
                    {

                        Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);

                        this.ViewState["_ProcedureCode"] = procedure.ProcedureCode;

                        Task currentTask = procedure.GetTask(currentAct.ToTaskCode);

                        this.btnSignIn.Visible = false;            //ǩ�հ�ť
                        this.btnSave.Visible = true;

                        if (currentTask.Copy == 1)                  //���Ͱ�ť
                        {
                            this.btnMakeCopy.Visible = true;
                            this.btnMakeCopy.Value = currentTask.TaskActorType;
                            //�ж��Ƿ�Ϊ���͵ȴ�״̬����Ϊshimaopm���� ���Ի�
                            if (!IsWait && System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower() == "shimaopm")
                            {
                                this.btnFinish.Attributes["onclick"] = "GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectRouterControl('Finish','��������');return false;";
                            }
                        }
                        else
                        {
                            this.btnMakeCopy.Visible = false;
                        }
                        /////////////////////////////////////////
                        DataSet dsx = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                        //DataView dvx = new DataView(dsx.Tables["WorkFlowAct"], String.Format(" ToTaskCode='{0}' and ActType <> 1 and Copy <> 1", currentTask.TaskCode), "", DataViewRowState.CurrentRows);
                        // clm �޸� 2006-11-2��
                        DataView dvx = new DataView(dsx.Tables["WorkFlowAct"], String.Format(" ToTaskCode='{0}' and Copy <> 1", currentTask.TaskCode), "", DataViewRowState.CurrentRows);

                        BLL.ConvertRule.GetDistinct(dvx.Table, "FromTaskCode", "");

                        string tempTaskCode = "";
                        for (int i = 0; i < dvx.Count; i++)
                        {
                            tempTaskCode = (String)dvx[i].Row["FromTaskCode"];
                        }

                        if (procedure.GetTask(tempTaskCode).TaskType == 1 && dvx.Count == 1)
                        {
                            this.btnBack.Visible = false;              //�˻ذ�ť
                            this.btnBackEx.Visible = false;            //�����˻�
                        }
                        else
                        {
                            this.btnBack.Visible = true;              //�˻ذ�ť
                            this.btnBackEx.Visible = true;            //�����˻�
                        }
                        /////////////////////////////////////////
                        this.btnOpinion.Visible = true;           //������水ť

                        DataSet dsp = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                        DataView dvt = new DataView(dsp.Tables["WorkFlowTask"], String.Format(" TaskType=1 "), "", DataViewRowState.CurrentRows);
                        DataView dvr = new DataView(dsp.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", dvt[0].Row["TaskCode"].ToString()), "", DataViewRowState.CurrentRows);
                        if (currentAct.ToTaskCode == dvr[0].Row["ToTaskCode"].ToString())
                            this.btnBackTop.Visible = false;           //�;����˰�ť
                        else
                            this.btnBackTop.Visible = true;           //�;����˰�ť

                        if (currentTask.TaskType == 5)//��ǩ�ڵ�״̬�ж�
                        {
                            this.btnBack.Visible = false;              //�˻ذ�ť
                            this.btnBackTop.Visible = false;           //�;����˰�ť
                            this.btnBackEx.Visible = false;            //�����˻�
                            this.btnBlankOut.Visible = false;          //���ϰ�ť

                            if (StatusEndCount(this.ViewState["_CaseCode"].ToString(), currentTask.TaskCode) == 1)
                            {
                                this.btnTaskFinish.Visible = false;         //��ɰ�ť

                                DataSet ds5 = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                                DataView dv5 = new DataView(ds5.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", currentTask.TaskCode), "SortID", DataViewRowState.CurrentRows);

                                int FinishTaskCount = 0;
                                int OtherTaskCount = 0;
                                for (int z = 0; z < dv5.Count; z++)
                                {
                                    string TaskCode = (String)dv5[z].Row["ToTaskCode"];
                                    Task endtask = procedure.GetTask(TaskCode);

                                    if (endtask.TaskType == 2)
                                    {
                                        FinishTaskCount++;
                                    }
                                    else
                                    {
                                        OtherTaskCount++;
                                    }
                                }

                                this.btnFinish.Visible = (FinishTaskCount != 0);
                                this.btnSend.Visible = (OtherTaskCount != 0);
                            }
                            else
                            {
                                this.btnTaskFinish.Visible = true;
                                this.btnSend.Visible = false;
                                this.btnFinish.Visible = false;
                            }
                            //////  ��ǩ�����������͵� /////////////
                            if (this.GetCurrentActSendButtonState(this.SystemUserCode))
                                this.btnSend.Visible = true;
                            ///////////////////////////////////////////

                        }
                        else//�ǻ�ǩ�ڵ�״̬�ж�
                        {
                            this.btnSend.Visible = true;               //���Ͱ�ť
                            this.btnTaskFinish.Visible = false;         //��ɰ�ť
                            /////////////////////////////////////////////////////////
                            this.btnBlankOut.Visible = false;          //���ϰ�ť

                            DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                            DataView dv = new DataView(ds.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", currentTask.TaskCode), "SortID", DataViewRowState.CurrentRows);


                            /////////////////�������Ա�///////////////////
                            DataTable PropertyTable = BLL.WorkFlowRule.GetPropertyTable(workCase, procedure);

                            for (int i = 0; i < dv.Table.Rows.Count; i++)
                            {
                                System.Collections.IDictionaryEnumerator iecondition = procedure.GetRouter((string)dv.Table.Rows[i]["RouterCode"]).GetConditionEnumerator();
                                if (iecondition.MoveNext())
                                {
                                    Condition condition = (Condition)iecondition.Value;
                                    if (PropertyTable.Select(condition.Description).Length == 0)
                                    {
                                        dv.Table.Rows.Remove(dv.Table.Rows[i]);
                                        i--;
                                    }
                                }
                            }
                            //////////////////////////////////////////////

                            if (dv.Count == 1)
                            {
                                string TaskCode = (String)dv[0].Row["ToTaskCode"];
                                Task endtask = procedure.GetTask(TaskCode);

                                if (endtask.TaskType == 2)
                                {
                                    this.btnSend.Visible = false;   //���Ͱ�ť
                                    this.btnFinish.Visible = true;  //������ť
                                    this.btnSave.Visible = false;
                                }
                                else
                                {
                                    this.btnSend.Visible = true;
                                    this.btnFinish.Visible = false;
                                }
                            }
                            else
                            {
                                bool taskIsFinishFlag = false;
                                for (int i = 0; i < dv.Count; i++)
                                {
                                    Task taskCase = procedure.GetTask(procedure.GetRouter((String)dv[i].Row["RouterCode"]).ToTaskCode);
                                    if (taskCase.TaskType == 2)
                                        taskIsFinishFlag = true;
                                }
                                if (taskIsFinishFlag)
                                    this.btnFinish.Visible = true;             //������ť
                                else
                                    this.btnFinish.Visible = false;

                                this.btnSend.Visible = true;
                            }
                        }
                    }
                }
            }
            if (this.btnSend.Visible == true)
            {
                // ���·��rbl
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                Act currentAct = workCase.GetAct(this.ActCode);
                DataView dv = new DataView(ds.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", currentAct.ToTaskCode), "SortID", DataViewRowState.CurrentRows);

                /////////////////�������Ա�///////////////////
                DataTable PropertyTable = BLL.WorkFlowRule.GetPropertyTable(workCase, procedure);

                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    System.Collections.IDictionaryEnumerator iecondition = procedure.GetRouter((string)dv.Table.Rows[i]["RouterCode"]).GetConditionEnumerator();
                    if (iecondition.MoveNext())
                    {
                        Condition condition = (Condition)iecondition.Value;
                        if (PropertyTable.Select(condition.Description).Length == 0)
                        {
                            dv.Table.Rows.Remove(dv.Table.Rows[i]);
                            i--;
                        }
                    }
                }
                ///////////////////////////////////

                int iCount = dv.Count;
                for (int i = 0; i < iCount; i++)
                {
                    string routerCode = (String)dv[i].Row["RouterCode"];
                    if (procedure.GetTask(procedure.GetRouter(routerCode).ToTaskCode).TaskType == 2)
                    {
                        iCount--;
                        i--;
                    }
                }

                if (iCount < 1)
                    this.btnSend.Visible = false;


            }
        }
        /// <summary>
        /// ����ű�����
        /// </summary>
        private void OutputScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<script>");
            sb.Append(" function returnSelectRouterControl( routerCode , routerName  , userCodes , userNames ,copyUsers ,workName,WaitForFlag,FlowOpinion,ChkShow , RouterMessage,ChkMail,AuditValue,ChkRate)");
            sb.Append(" {");
            sb.Append("     document.all(\"" + this.ID + "_HiddenSelectRouterCode\").value = routerCode;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenSelectUserCodes\").value = userCodes;");
            //sb.Append("     document.all(\"" + this.ID + "_HiddenSelectUserCodes\").value += copyUsers;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenCopyUsers\").value = copyUsers;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenWaitForFlag\").value = WaitForFlag;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenFlowOpinion\").value = FlowOpinion;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenChkShow\").value = ChkShow;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenRouterMessage\").value = RouterMessage;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenChkMail\").value = ChkMail;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenAuditValue\").value = AuditValue;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenRateValue\").value = ChkRate;");
            sb.Append("     if(workName == 'Send')");
            sb.Append("         WorkFlowSend();");
            sb.Append("     else if(workName == 'Back')");
            sb.Append("         WorkFlowBack();");
            sb.Append("     else if(workName == 'BackEx')");
            sb.Append("         WorkFlowBackEx();");
            sb.Append("     else if(workName == 'Finish')");
            sb.Append("     {");
            sb.Append("         WorkFlowFinish();");
            sb.Append("     }");
            sb.Append("     else");
            sb.Append("         WorkFlowMakeCopy();");
            sb.Append(" }");

            sb.Append("	function WorkFlowSend()");
            sb.Append(" {");
            sb.Append("     if ( (Form1." + this.ID + "_HiddenSelectRouterCode.value == '' )  || ( Form1." + this.ID + "_HiddenSelectUserCodes.value == '' ))");
            sb.Append("     {");
            sb.Append("         alert( '��Ҫ���ͣ���ѡ������ ��' );");
            sb.Append("         return;");
            sb.Append("     }");
            sb.Append("     Form1." + this.ID + "_btnHiddenSend.onclick();");
            sb.Append("	}");

            sb.Append("	function WorkFlowBack()");
            sb.Append(" {");
            sb.Append("     if ( (Form1." + this.ID + "_HiddenSelectRouterCode.value == '' )  || ( Form1." + this.ID + "_HiddenSelectUserCodes.value == '' ))");
            sb.Append("     {");
            sb.Append("         alert( '��Ҫ�˻أ���ѡ���˻����� ��' );");
            sb.Append("         return;");
            sb.Append("     }");
            sb.Append("     Form1." + this.ID + "_btnHiddenBack.onclick();");
            sb.Append("	}");

            sb.Append("	function WorkFlowMakeCopy()");
            sb.Append(" {");
            sb.Append("     if(document.all(\"" + this.ID + "_HiddenCopyUsers\").value == '')");
            sb.Append("     {");
            sb.Append("         alert( '��Ҫ���ͣ���ѡ������Ա ��' );");
            sb.Append("         return;");
            sb.Append("     }");
            sb.Append("     Form1." + this.ID + "_btnHiddenMakeCopy.onclick();");
            sb.Append("	}");

            sb.Append("	function WorkFlowFinish()");
            sb.Append(" {");
            sb.Append("     Form1." + this.ID + "_btnHiddenMakeCopy.onclick();");
            sb.Append("     if(document.all(\"" + this.ID + "_HiddenWaitForFlag\").value == 'true')");
            sb.Append("     {");
            sb.Append("         return;");
            sb.Append("     }");
            sb.Append("     Form1." + this.ID + "_btnHiddenFinish.onclick();");
            sb.Append("	}");

            string tmpCaseCode = "";
            if (this.ViewState["_CaseCode"] != null)
                tmpCaseCode = this.ViewState["_CaseCode"].ToString();

            sb.Append(" function selectRouterControl(WorkName,TitleText)");
            sb.Append(" {");
            sb.Append("     var doothers=new workflowtoolbar_DoOthers(WorkName);if(doothers.DoSomething()==false){return false;}");
            //sb.Append("     ////if(!doothers.DoSomething())return false;");
            sb.Append("     OpenMiddleWindow('" + this.SourceUrl + "WorkFlowRouterCopy.aspx?ProcedureCode=" + this.ViewState["_ProcedureCode"].ToString() + "&CaseCode=" + tmpCaseCode + "&ApplicationCode=" + this.ApplicationCode + "&UserCode=" + this.SystemUserCode + "&ActCode=" + this.ActCode + "&ProjectCode=" + this.ViewState["_ProjectCode"].ToString() + "&Work='+WorkName+'&IsAudit='+document.all(\"" + this.ID + "_HiddenIsAudit\").value,TitleText);");
            sb.Append(" }");
            sb.Append(" function selectOpinionControl(WorkName,TitleText)");
            sb.Append(" {");
            sb.Append("     OpenMiddleWindow('" + this.SourceUrl + "WorkFlowOpinionWrite.aspx?ProcedureCode=" + this.ViewState["_ProcedureCode"].ToString() + "&CaseCode=" + tmpCaseCode + "&ApplicationCode=" + this.ApplicationCode + "&UserCode=" + this.SystemUserCode + "&ActCode=" + this.ActCode + "&Work='+WorkName,TitleText);");
            sb.Append(" }");
            sb.Append("	function returnSaveOpinion(FlowOpinion,ChkShow)");
            sb.Append(" {");
            sb.Append("     document.all(\"" + this.ID + "_HiddenFlowOpinion\").value = FlowOpinion;");
            sb.Append("     document.all(\"" + this.ID + "_HiddenChkShow\").value = ChkShow;");
            sb.Append("     Form1." + this.ID + "_btnHiddenOpinion.onclick();");
            sb.Append("	}");

            sb.Append(" function selectBackControl(WorkName,TitleText)");
            sb.Append(" {");
            sb.Append(" if(WorkName=='back')");
            sb.Append("     OpenMiddleWindow('" + this.SourceUrl + "WorkFlowBack.aspx?ProcedureCode=" + this.ViewState["_ProcedureCode"].ToString() + "&CaseCode=" + tmpCaseCode + "&ApplicationCode=" + this.ApplicationCode + "&UserCode=" + this.SystemUserCode + "&ActCode=" + this.ActCode + "&ProjectCode=" + this.ViewState["_ProjectCode"].ToString() + "&Work='+WorkName,TitleText);");
            sb.Append(" else if(WorkName=='backEx')");
            sb.Append("     OpenMiddleWindow('" + this.SourceUrl + "WorkFlowBackEx.aspx?ProcedureCode=" + this.ViewState["_ProcedureCode"].ToString() + "&CaseCode=" + tmpCaseCode + "&ApplicationCode=" + this.ApplicationCode + "&UserCode=" + this.SystemUserCode + "&ActCode=" + this.ActCode + "&ProjectCode=" + this.ViewState["_ProjectCode"].ToString() + "&Work='+WorkName,TitleText);");
            sb.Append(" else if(WorkName=='backTop')");
            sb.Append("     OpenMiddleWindow('" + this.SourceUrl + "WorkFlowBackTop.aspx?ProcedureCode=" + this.ViewState["_ProcedureCode"].ToString() + "&CaseCode=" + tmpCaseCode + "&ApplicationCode=" + this.ApplicationCode + "&UserCode=" + this.SystemUserCode + "&ActCode=" + this.ActCode + "&ProjectCode=" + this.ViewState["_ProjectCode"].ToString() + "&Work='+WorkName,TitleText);");
            sb.Append(" }");

            sb.Append(" function workflowmsgfunction()");
            sb.Append(" {");
            sb.Append("     if(document.all(\"" + this.ID + "_msgdiv\").style.display == \"none\")");
            sb.Append("			document.all(\"" + this.ID + "_msgdiv\").style.display = \"\";");
            sb.Append("		else");
            sb.Append("			document.all(\"" + this.ID + "_msgdiv\").style.display = \"none\";");
            sb.Append(" }");

            sb.Append(" function DoPrintForm()");
            sb.Append(" {");
            sb.Append("     OpenFullWindow('" + BLL.WorkFlowRule.GetProcedureSourceURLByCode(this.ViewState["_ProcedureCode"].ToString()) + "?CaseCode=" + this.CaseCode + "&ApplicationCode=" + this.ApplicationCode + "&frameType=List','" + this.CaseCode + "');");
            sb.Append(" }");
            sb.Append("function workflowtoolbar_DoOthers(workName){this.WorkName=workName;}workflowtoolbar_DoOthers.prototype.DoSomething=function(){return true;};");
            sb.Append("</script>");

            this.spanScript.InnerHtml = sb.ToString();



        }
        /// ****************************************************************************
        /// <summary>
        /// ��ȡ��ǩ�л�δ��ɵ�����
        /// </summary>
        /// <param name="CaseCode">����ʵ�����</param>
        /// <param name="ToTaskCode">Ŀ�������������</param>
        /// <returns>��ǩ�л�δ��ɵ�����</returns>
        /// ****************************************************************************
        private int StatusEndCount(string CaseCode, string ToTaskCode)
        {
            DAL.QueryStrategy.TaskStatusStrategyBuilder sbq = new DAL.QueryStrategy.TaskStatusStrategyBuilder();

            sbq.AddStrategy(new Strategy(DAL.QueryStrategy.TaskActorStrategyName.CaseCode, CaseCode));
            sbq.AddStrategy(new Strategy(DAL.QueryStrategy.TaskActorStrategyName.ToTaskCode, ToTaskCode));
            sbq.AddStrategy(new Strategy(DAL.QueryStrategy.TaskActorStrategyName.Status, "End"));
            sbq.AddStrategy(new Strategy(DAL.QueryStrategy.TaskActorStrategyName.Copy, "1"));

            string sql = sbq.BuildMainQueryString();

            QueryAgent qa = new QueryAgent();
            DataSet ds = qa.ExecSqlForDataSet(sql);

            int count = qa.FillEntityData("WorkFlowAct", sql).Tables[0].Rows.Count;

            qa.Dispose();

            if (this.MeetingUsers.Count != 0)
                count = count - MeetingUsers.Count;

            return count;
        }

        #endregion ----------------------------------------------------------------------

        #region -  ������������  --------------------------------------------------------

        /// <summary>
        /// ����������
        /// </summary>
        private void ToolbarCheck()
        {
            if (this.RadioHandmade.Visible == true && this.RadioHandmade.SelectedValue == "")
            {
                this.CheckValue = false;
                this.Page.RegisterClientScriptBlock("HandmadeCheckFalse", "<script>alert('��ѡ����������ѡ�');</script>");
            }
            else
            {
                this.CheckValue = true;
            }

        }
        /// <summary>
        /// ������������״̬
        /// </summary>
        private void InitHandMade()
        {
            switch (GetModuleState("Hand"))
            {
                case ModuleState.Operable:
                    this.HandmadeTable.Visible = true;
                    this.HandMadeLabel.Visible = false;
                    this.RadioHandmade.Visible = true;
                    this.RadioHandmade.SelectedIndex = this.RadioHandmade.Items.IndexOf(this.RadioHandmade.Items.FindByValue(this.GetCasePropertyValue("��������")));
                    break;
                case ModuleState.Sightless:
                    this.HandmadeTable.Visible = false;
                    this.HandMadeLabel.Visible = false;
                    this.RadioHandmade.Visible = false;
                    break;
                default:
                    this.HandMadeLabel.Visible = true;
                    this.RadioHandmade.Visible = false;
                    this.HandMadeLabel.Text = this.GetCasePropertyValue("��������");
                    this.HandmadeTable.Visible = (this.HandMadeLabel.Text != "");
                    break;
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ���г�ʼ����ʾ���̡���������״̬�����ݵ�ˢ�¡�
        /// </summary>
        /// ****************************************************************************
        override public void ToolbarDataBind()
        {
            try
            {
                InitUserControl();

                OutputScript();

                InitHandMade();

                InitAudit();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
                throw ex;
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// ��ȡģ��״̬����
        /// </summary>
        /// <param name="ModuleId">ģ�������̶����е� ID </param>
        /// <returns>ģ��״̬</returns>
        /// ****************************************************************************
        override public ModuleState GetModuleState(string ModuleId)
        {
            return GetModuleState1(ModuleId, 0);
        }
        /// ****************************************************************************
        /// <summary>
        /// ��ȡģ��check״̬����
        /// </summary>
        /// <param name="ModuleId">ģ�������̶����е� ID </param>
        /// <returns>ģ��״̬</returns>
        /// ****************************************************************************
        override public ModuleState GetModuleState(string ModuleId, int flag)
        {
            return GetModuleState1(ModuleId, flag);
        }

        private ModuleState GetDefaultModuleState(string ModuleId, int flag, bool defaultflag)
        {
            bool ActStatusIsBegin = false;
            ModuleState moduleState = ModuleState.Unbeknown;
            Task currentTask = null;

            if (this.CaseStatus == "")
            {
                if (_Procedure == null)
                    _Procedure = DefinitionManager.GetProcedureDifinition(this.ViewState["_ProcedureCode"].ToString(), true);
                currentTask = Rms.WorkFlow.DefinitionManager.GetFirstTask(_Procedure);
            }
            else if (this.CaseStatus == "Begin" || this.CaseStatus == "End")
            {
                if (_WorkCase == null)
                    _WorkCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());

                string AppActCode = this.ActCode;

                Act currentAct = _WorkCase.GetAct(AppActCode);

                /************************* ���ͷ���״̬ *************************/
                if (currentAct.Copy == 1)
                {
                    moduleState = GetActorModuleState(ModuleId, this.SystemUserCode, flag);
                    if (moduleState != ModuleState.Unbeknown)
                        return moduleState;
                }
                /****************************************************************/
                if (_Procedure == null)
                    _Procedure = DefinitionManager.GetProcedureDifinition(_WorkCase.ProcedureCode, true);
                currentTask = _Procedure.GetTask(currentAct.ToTaskCode);

                /************************* ���ͷ���״̬ *************************/

                


                if (currentTask.TaskType == 5)
                {
                    moduleState = GetActorModuleState(ModuleId, this.SystemUserCode, flag);
                    if (moduleState != ModuleState.Unbeknown)
                        return moduleState;
                }
                /****************************************************************/

                if (currentAct.Status == ActStatus.Begin)
                    ActStatusIsBegin = true;

            }

            //tempi���ڿ��Ƶ�ǰ��Ҫ��ѯ���ַ����Ƿ�����ڸ���������  �޸��� karen  �޸���2006-11-16
            int tempi = 0;
            foreach (string sss in currentTask.ModuleState.Split(new char[] { ';' }))
            {
                if (sss != "")
                {
                    int ud_iIndexOfComma = sss.IndexOf(",");
                    string ud_sModuleName = sss.Substring(0, ud_iIndexOfComma);
                    if (ud_sModuleName.Trim() != ModuleId)
                        continue;
                    tempi = 1;
                    string ssss = sss.Substring(ud_iIndexOfComma + 1, sss.Length - ud_iIndexOfComma - 1);
                    string[] sas = ssss.Split(new char[] { '|' });

                    if (sas.Length > flag && sas[flag].Trim() != "")
                    {
                        string[] sus = sas[flag].Split(new char[] { ',' });

                        switch (int.Parse(sus[0].ToString()))
                        {
                            case 1://δ֪��
                                moduleState = ModuleState.Eyeable;
                                break;
                            case 2://�ɼ���
                                moduleState = ModuleState.Eyeable;
                                break;
                            case 3://�ɲ�����
                                moduleState = ModuleState.Operable;
                                break;
                            case 4://���ɼ���
                                moduleState = ModuleState.Sightless;
                                break;
                            case 5://������
                                moduleState = ModuleState.Other;
                                break;
                            case 6://�����ж�
                                moduleState = ModuleState.Eyeable;
                                for (int i = 1; i < sus.Length; i++)
                                {
                                    if (GetConditionState(sus[i].ToString()) == ModuleState.Operable)
                                        moduleState = ModuleState.Operable;
                                }
                                break;
                            default:
                                break;
                        }

                    }
                }
            }

            if (tempi == 0)
            {
                return ModuleState.Eyeable;
            }
            if (!defaultflag)
            {
                if ((moduleState == ModuleState.Operable && ActStatusIsBegin) || (moduleState == ModuleState.Eyeable && ActStatusIsBegin))
                    moduleState = ModuleState.Begin;
                if (moduleState == ModuleState.Operable && this.Scout == true)
                    moduleState = ModuleState.Eyeable;
            }

            if (this.CaseStatus == "End" && moduleState == ModuleState.Operable)
                moduleState = ModuleState.Eyeable;

            return moduleState;

        }



        /// ****************************************************************************
        /// <summary>
        /// ��ȡģ��״̬����(�����ǵ�ǰ����״̬)
        /// </summary>
        /// <param name="ModuleId">ģ�������̶����е� ID </param>
        /// <returns>ģ��״̬</returns>
        /// ****************************************************************************
        public ModuleState GetModuleStateEx(string ModuleId, int flag)
        {
            return GetDefaultModuleState(ModuleId, flag, true);
        }
        /// ****************************************************************************
        /// <summary>
        /// ��ȡģ��״̬����
        /// </summary>
        /// <param name="ModuleId">ģ�������̶����е� ID </param>
        /// <returns>ģ��״̬</returns>
        /// ****************************************************************************
        override public ModuleState GetModuleState1(string ModuleId, int flag)
        {
            return GetDefaultModuleState(ModuleId, flag, false);
        }

        /// ****************************************************************************
        /// <summary>
        /// ����״̬�ж�
        /// </summary>
        /// <param name="RouterOrderCode">·���������</param>
        /// <returns></returns>
        /// ****************************************************************************
        override public ModuleState GetConditionState(string RouterOrderCode)
        {
            ModuleState ConditionState = ModuleState.Eyeable;
            if (this.CaseStatus == "")
            {
                return ModuleState.Eyeable;
            }
            else if (this.CaseStatus == "End")
            {
                return ModuleState.End;
            }
            else if (this.CaseStatus == "Begin")
            {
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);

                DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                DataView dv = new DataView(ds.Tables["WorkFlowRouter"], String.Format(" SortID='{0}' ", RouterOrderCode), "SortID", DataViewRowState.CurrentRows);

                ///���������жϱ�
                DataTable PropertyTable = BLL.WorkFlowRule.GetPropertyTable(workCase, procedure);

                for (int i = 0; i < dv.Table.Rows.Count; i++)
                {
                    if (dv.Table.Rows[i]["SortID"].ToString() == RouterOrderCode)
                    {
                        Router router = procedure.GetRouter((string)dv.Table.Rows[i]["RouterCode"]);
                        System.Collections.IDictionaryEnumerator iecondition = router.GetConditionEnumerator();
                        if (iecondition.MoveNext())
                        {
                            Condition condition = (Condition)iecondition.Value;
                            if (PropertyTable.Select(condition.Description).Length == 0)
                            {
                                ConditionState = ModuleState.Eyeable;
                            }
                            else
                            {
                                ConditionState = ModuleState.Operable;
                            }
                        }
                    }
                }
            }
            return ConditionState;
        }
        /// ****************************************************************************
        /// <summary>
        /// ��ǰ�еķ��Ͱ�ť״̬
        /// </summary>
        /// <param name="UserCode">�û�����</param>
        /// <returns></returns>
        /// ****************************************************************************
        override public bool GetCurrentActSendButtonState(string UserCode)
        {
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
            Act currentAct = workCase.GetAct(this.ActCode);
            Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
            Task currentTask = procedure.GetTask(currentAct.ToTaskCode);

            bool BtnState = false;
            if (currentTask.TaskType == 5)
            {
                System.Collections.IDictionaryEnumerator ieTaskActor = currentTask.GetTaskActorEnumerator();
                while (ieTaskActor.MoveNext())
                {
                    bool IsRoleComprise = false;
                    TaskActor taskActor = (TaskActor)ieTaskActor.Value;
                    if (taskActor.TaskActorID == "0")
                    {
                        Role role = procedure.GetRole(taskActor.ActorCode);

                        // �����޶�����--����ֵ
                        string PropertyValue = ""; // �����޶�ֵ
                        string ProcedurePropertyType = ""; // �����޶�����
                        System.Collections.IDictionaryEnumerator ieCaseProperty = workCase.GetCasePropertyEnumerator();

                        while (ieCaseProperty.MoveNext())
                        {
                            CaseProperty CasePropertyCase = (CaseProperty)ieCaseProperty.Value;

                            if (CasePropertyCase.ProcedurePropertyCode == taskActor.ActorProperty)
                                PropertyValue = CasePropertyCase.ProcedurePropertyValue;
                        }
                        if (taskActor.ActorProperty != "")
                        {
                            Property PropertyCase = procedure.GetProperty(taskActor.ActorProperty);
                            ProcedurePropertyType = PropertyCase.ProcedurePropertyType;
                        }

                        DataTable UserTable = BLL.WorkFlowRule.GetRoleCompriseUser(role, ProcedurePropertyType, PropertyValue);
                        for (int i = 0; i < UserTable.Rows.Count; i++)
                        {
                            if (UserTable.Rows[i]["UserCode"].ToString() == UserCode)
                                IsRoleComprise = true;

                        }
                        if (IsRoleComprise && taskActor.TaskActorName == "1")
                        {
                            BtnState = true;
                        }
                    }
                }
            }
            return BtnState;
        }
        override public ModuleState GetActorModuleState(string ModuleId, string UserCode)
        {

            return GetActorModuleState(ModuleId, UserCode, 0);

        }
        /// ****************************************************************************
        /// <summary>
        /// ��ȡ��ǩ��ģ��״̬
        /// </summary>
        /// <param name="ModuleId">ģ�������̶����еĻ�ǩ�� ID </param>
        /// <returns>ģ��״̬</returns>
        /// ****************************************************************************
        override public ModuleState GetActorModuleState(string ModuleId, string UserCode, int flag)
        {
            string ActorModuleState = "";
            bool ActStatusIsBegin = false;
            ModuleState moduleState = ModuleState.Unbeknown;
            Task currentTask;

            if (this.CaseStatus == "")
            {
                if (_Procedure == null)
                    _Procedure = DefinitionManager.GetProcedureDifinition(this.ViewState["_ProcedureCode"].ToString(), true);
                currentTask = Rms.WorkFlow.DefinitionManager.GetFirstTask(_Procedure);
            }
            else if (this.CaseStatus == "Begin" || this.CaseStatus == "End")
            {
                if (_WorkCase == null)
                    _WorkCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());

                string AppActCode = this.ActCode;
                ///////////////�ж����̼�ؽڵ�///////////////
                string ActFinishDate = "";
                if (this.Scout == true)
                {
                    System.Collections.IDictionaryEnumerator ie = _WorkCase.GetActEnumerator();
                    while (ie.MoveNext())
                    {
                        Act act = (Act)ie.Value;
                        if (act.ActUserCode == this.SystemUserCode)
                        {
                            if (ActFinishDate == "")
                            {
                                ActFinishDate = act.FinishDate;
                                AppActCode = act.ActCode;
                            }
                            else if (act.FinishDate != "")
                            {
                                if (DateTime.Parse(act.FinishDate) > DateTime.Parse(ActFinishDate))
                                {
                                    ActFinishDate = act.FinishDate;
                                    AppActCode = act.ActCode;
                                }
                            }
                        }
                    }
                }
                //////////////////////////////////////

                Act currentAct = _WorkCase.GetAct(AppActCode);
                if (_Procedure == null)
                    _Procedure = DefinitionManager.GetProcedureDifinition(_WorkCase.ProcedureCode, true);
                currentTask = _Procedure.GetTask(currentAct.ToTaskCode);

                if (currentTask.TaskType == 5)
                {
                    if (_TaskActor == null)
                    {
                        System.Collections.IDictionaryEnumerator ieTaskActor = currentTask.GetTaskActorEnumerator();
                        while (ieTaskActor.MoveNext())
                        {
                            bool IsRoleComprise = false;
                            TaskActor taskActor = (TaskActor)ieTaskActor.Value;
                            if (taskActor.TaskActorID == "0")
                            {
                                Role role = _Procedure.GetRole(taskActor.ActorCode);
                                // �����޶�����--����ֵ
                                string PropertyValue = ""; // �����޶�ֵ
                                string ProcedurePropertyType = ""; // �����޶�����
                                System.Collections.IDictionaryEnumerator ieCaseProperty = _WorkCase.GetCasePropertyEnumerator();

                                while (ieCaseProperty.MoveNext())
                                {
                                    CaseProperty CasePropertyCase = (CaseProperty)ieCaseProperty.Value;

                                    if (CasePropertyCase.ProcedurePropertyCode == taskActor.ActorProperty)
                                        PropertyValue = CasePropertyCase.ProcedurePropertyValue;
                                }

                                if (taskActor.ActorProperty != "")
                                {
                                    Property PropertyCase = _Procedure.GetProperty(taskActor.ActorProperty);
                                    ProcedurePropertyType = PropertyCase.ProcedurePropertyType;
                                }

                                DataTable UserTable = BLL.WorkFlowRule.GetRoleCompriseUser(role, ProcedurePropertyType, PropertyValue);
                                for (int i = 0; i < UserTable.Rows.Count; i++)
                                {
                                    if (UserTable.Rows[i]["UserCode"].ToString() == UserCode)
                                        IsRoleComprise = true;

                                }

                                if (IsRoleComprise)
                                {
                                    _TaskActor = (TaskActor)taskActor;
                                    ActorModuleState = taskActor.ActorModuleState;
                                }
                            }
                        }
                    }
                    else
                    {
                        ActorModuleState = _TaskActor.ActorModuleState;
                    }
                }

                if (currentAct.Copy == 1)
                {
                    if (_TaskActor == null)
                    {
                        System.Collections.IDictionaryEnumerator ieTaskActor = currentTask.GetTaskActorEnumerator();
                        while (ieTaskActor.MoveNext())
                        {
                            bool IsRoleComprise = false;
                            TaskActor taskActor = (TaskActor)ieTaskActor.Value;
                            if (taskActor.TaskActorID == "1")
                            {
                                Role role = _Procedure.GetRole(taskActor.ActorCode);
                                // �����޶�����--����ֵ
                                string PropertyValue = ""; // �����޶�ֵ
                                string ProcedurePropertyType = ""; // �����޶�����
                                System.Collections.IDictionaryEnumerator ieCaseProperty = _WorkCase.GetCasePropertyEnumerator();

                                while (ieCaseProperty.MoveNext())
                                {
                                    CaseProperty CasePropertyCase = (CaseProperty)ieCaseProperty.Value;

                                    if (CasePropertyCase.ProcedurePropertyCode == taskActor.ActorProperty)
                                        PropertyValue = CasePropertyCase.ProcedurePropertyValue;
                                }

                                if (taskActor.ActorProperty != "")
                                {
                                    Property PropertyCase = _Procedure.GetProperty(taskActor.ActorProperty);
                                    ProcedurePropertyType = PropertyCase.ProcedurePropertyType;
                                }

                                DataTable UserTable = BLL.WorkFlowRule.GetRoleCompriseUser(role, ProcedurePropertyType, PropertyValue);
                                for (int i = 0; i < UserTable.Rows.Count; i++)
                                {
                                    if (UserTable.Rows[i]["UserCode"].ToString() == UserCode)
                                        IsRoleComprise = true;

                                }
                                if (IsRoleComprise)
                                {
                                    _TaskActor = (TaskActor)taskActor;
                                    ActorModuleState = taskActor.ActorModuleState;
                                }
                            }
                        }
                    }
                    //�������·���ע�͵���λ�á����˸о��ӳ����߼���������Ӧ��Ҫ�ŵ���ߵġ�
                    else
                    {
                        ActorModuleState = _TaskActor.ActorModuleState;
                    }
                }
                //else
                //{
                //    ActorModuleState = _TaskActor.ActorModuleState;
                //}

                if (currentAct.Status == ActStatus.Begin)
                    ActStatusIsBegin = true;

            }


            ActorModuleState = ActorModuleState == null ? "" : ActorModuleState;
            //tempi���ڿ��Ƶ�ǰ��Ҫ��ѯ���ַ����Ƿ�����ڸ��������� �޸��� ������  �޸���2006-11-16
            int tempi = 0;
            foreach (string sss in ActorModuleState.Split(new char[] { ';' }))
            {
                if (sss != "")
                {
                    int ud_iIndexOfComma = sss.IndexOf(",");
                    string ud_sModuleName = sss.Substring(0, ud_iIndexOfComma);
                    if (ud_sModuleName.Trim() != ModuleId)
                        continue;
                    tempi = 1;
                    string ssss = sss.Substring(ud_iIndexOfComma + 1, sss.Length - ud_iIndexOfComma - 1);
                    string[] sas = ssss.Split(new char[] { '|' });

                    if (sas.Length > flag && sas[flag].Trim() != "")
                    {
                        string[] sus = sas[flag].Split(new char[] { ',' });

                        switch (int.Parse(sus[0].ToString()))
                        {
                            case 1://δ֪��
                                moduleState = ModuleState.Eyeable;
                                break;
                            case 2://�ɼ���
                                moduleState = ModuleState.Eyeable;
                                break;
                            case 3://�ɲ�����
                                moduleState = ModuleState.Operable;
                                break;
                            case 4://���ɼ���
                                moduleState = ModuleState.Sightless;
                                break;
                            case 5://������
                                moduleState = ModuleState.Other;
                                break;
                            case 6://�����ж�
                                moduleState = ModuleState.Eyeable;
                                for (int i = 1; i < sus.Length; i++)
                                {
                                    if (GetConditionState(sus[i].ToString()) == ModuleState.Operable)
                                        moduleState = ModuleState.Operable;
                                }
                                break;
                            default:
                                break;
                        }

                    }
                }

            }
            if (tempi == 0)
            {
                return ModuleState.Eyeable;
            }

            if (moduleState == ModuleState.Operable && ActStatusIsBegin)
                moduleState = ModuleState.Begin;
            if (moduleState == ModuleState.Operable && this.Scout == true)
                moduleState = ModuleState.Eyeable;

            return moduleState;

        }


        /// <summary>
        /// ��ȡ�û��Ƿ�Ϊ���̶����û������������󷽿�ʹ�á�
        /// </summary>
        /// <param name="UserCode">�û�����</param>
        /// <returns>�Ƿ�Ϊ�����û���Ϣ</returns>
        override public bool GetIsTaskUser(string UserCode)
        {
            bool IsTaskUser = false;
            if (this.ActCode != "")
            {
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                Act currentAct = workCase.GetAct(this.ActCode);
                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Task currentTask = procedure.GetTask(currentAct.ToTaskCode);
                EntityData entity = BLL.WorkFlowRule.GetTaskUser(workCase, currentTask, this.ActCode);
                if (entity.HasRecord())
                {
                    int iCount = entity.CurrentTable.Rows.Count;
                    for (int i = 0; i < iCount; i++)
                    {
                        entity.SetCurrentRow(i);
                        if (entity.GetString("UserCode") == UserCode)
                            IsTaskUser = true;
                    }
                }
            }
            return IsTaskUser;
        }
        /// <summary>
        /// ����������������ֵ
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        override public void SaveCasePropertyValue(string PropertyName, string PropertyValue)
        {
            try
            {
                string PropertyCode = "";
                string WorkFlowCasePropertyCode = "";
                Procedure procedure = DefinitionManager.GetProcedureDifinition(this.ViewState["_ProcedureCode"].ToString(), true);
                System.Collections.IDictionaryEnumerator ie = procedure.GetPropertyEnumerator();
                while (ie.MoveNext())
                {
                    Property PropertyCase = (Property)ie.Value;
                    if (PropertyCase.ProcedurePropertyName == PropertyName)
                    {
                        PropertyCode = PropertyCase.WorkFlowProcedurePropertyCode;
                    }
                }

                if (this.ViewState["_CaseCode"] + "" != "")
                {
                    WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());

                    ie = workCase.GetCasePropertyEnumerator();
                    while (ie.MoveNext())
                    {
                        CaseProperty CasePropertyCase = (CaseProperty)ie.Value;
                        if (CasePropertyCase.ProcedurePropertyCode == PropertyCode)
                        {
                            WorkFlowCasePropertyCode = CasePropertyCase.WorkFlowCasePropertyCode;
                        }
                    }
                }

                if (PropertyCode != "")
                {
                    CaseProperty CasePropertyCaseM = new CaseProperty();
                    CasePropertyCaseM.WorkFlowCasePropertyCode = WorkFlowCasePropertyCode;
                    CasePropertyCaseM.CaseCode = this.ViewState["_CaseCode"].ToString();
                    CasePropertyCaseM.ProcedurePropertyCode = PropertyCode;
                    CasePropertyCaseM.ProcedurePropertyValue = PropertyValue;

                    BLL.WorkFlowRule.SaveCaseProperty(CasePropertyCaseM);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                //                throw ex;
            }
        }

        /// <summary>
        /// ��ȡ������������ֵ
        /// </summary>
        /// <param name="PropertyName"></param>
        override public string GetCasePropertyValue(string PropertyName)
        {
            try
            {
                string PropertyCode = "";
                string WorkFlowCasePropertyValue = "";
                Procedure procedure = DefinitionManager.GetProcedureDifinition(this.ViewState["_ProcedureCode"].ToString(), true);
                System.Collections.IDictionaryEnumerator ie = procedure.GetPropertyEnumerator();
                while (ie.MoveNext())
                {
                    Property PropertyCase = (Property)ie.Value;
                    if (PropertyCase.ProcedurePropertyName == PropertyName)
                    {
                        PropertyCode = PropertyCase.WorkFlowProcedurePropertyCode;
                    }
                }

                if (this.ViewState["_CaseCode"] + "" != "")
                {
                    WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());

                    ie = workCase.GetCasePropertyEnumerator();
                    while (ie.MoveNext())
                    {
                        CaseProperty CasePropertyCase = (CaseProperty)ie.Value;
                        if (CasePropertyCase.ProcedurePropertyCode == PropertyCode)
                        {
                            WorkFlowCasePropertyValue = CasePropertyCase.ProcedurePropertyValue;
                        }
                    }
                }

                return WorkFlowCasePropertyValue;
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                return "";
            }
        }



        /// ****************************************************************************
        /// <summary>
        /// ���淽��
        /// </summary>
        /// ****************************************************************************
        override public void Save()
        {
            if (this.ActCode == "")
            {
                if (this.ApplicationCode == "")
                {
                    throw new Exception("�޷���λҵ�����ݣ����ṩ ApplicationCode ��");
                }
                else
                {
                    if (this.FlowName == "")
                    {
                        throw new Exception("�޷���λ������Ϣ�����ṩ FlowName ��");
                    }
                    else
                    {
                        string actCode = "";
                        string ProcedureCode = this.ViewState["_ProcedureCode"].ToString();
                        WorkCase workCase = Rms.WorkFlow.WorkCaseManager.StartNewWorkCase(this.ApplicationCode, ProcedureCode, this.SystemUserCode, this.SystemUnitCode, ref actCode, this.Transactor, this.TransactUnit);
                        System.Collections.IDictionaryEnumerator ie = workCase.GetActEnumerator();
                        while (ie.MoveNext())
                        {
                            Act act = (Act)ie.Value;
                            if (act.Copy != 1)
                                act.IsSleep = 1;
                        }
                        DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                        BLL.WorkFlowApplicationRule.SaveWorkCaseEx(ds, workCase.CaseCode);

                        // �������
                        /////////////////////////////////////////////////////////////////////
                        this.ViewState["_CaseCode"] = workCase.CaseCode;
                        /////////////////////////////////////////////////////////////////////

                        this.ActCode = actCode;

                        Response.Write(Rms.Web.JavaScript.ScriptStart);
                        Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                        Response.Write(Rms.Web.JavaScript.ScriptEnd);
                    }
                }
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// ���ͷ���
        /// </summary>
        /// ****************************************************************************
        override public void Send()
        {
            string routerCode = this.HiddenSelectRouterCode.Value;
            string userCodes = this.HiddenSelectUserCodes.Value;
            string copyUsers = this.HiddenCopyUsers.Value;
            string routerMessage = BLL.SystemRule.GetUserName(this.SystemUserCode) + " �� " + this.HiddenRouterMessage.Value;

            try
            {

                string ProcedureCode = this.ViewState["_ProcedureCode"].ToString();
                WorkCase workCase = null;
                if (this.ActCode == "")
                {
                    string actCode = "";
                    workCase = Rms.WorkFlow.WorkCaseManager.StartNewWorkCase(this.ApplicationCode, ProcedureCode, this.SystemUserCode, this.SystemUnitCode, ref actCode, this.Transactor, this.TransactUnit);
                    this.ActCode = actCode;
                    ///new
                }
                else
                {
                    workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                    ///up
                }
                Act currentAct = workCase.GetAct(this.ActCode);

                if (currentAct.Status == ActStatus.End)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��������Ѿ��������� ��"));
                }
                else
                {

                    Rms.WorkFlow.WorkCaseManager.ForwardWorkCase(workCase, this.ApplicationCode, this.ActCode, routerCode, userCodes, this.SystemUserCode, this.HiddenFlowOpinion.Value, routerMessage);
                    if (copyUsers.Length > 0)
                    {
                        string CopyFromActCode = "";
                        if (this.HiddenWaitForFlag.Value == "true")
                            CopyFromActCode = this.ActCode;
                        Rms.WorkFlow.WorkCaseManager.ForwardCopyWorkCase(workCase, workCase.ApplicationCode, this.ActCode, "", copyUsers, this.SystemUserCode, this.HiddenFlowOpinion.Value, CopyFromActCode, routerMessage);
                    }
                    //////////////////////////////
                    if (System.Configuration.ConfigurationSettings.AppSettings["PMName"] == "ShiMaoPM")
                    {
                        IDictionaryEnumerator ie = workCase.GetActEnumerator();
                        while (ie.MoveNext())
                        {
                            Act tmpact = (Act)ie.Value;
                            if (tmpact.FromUnitCode == this.ActCode && tmpact.Copy == 1)
                                tmpact.IsSleep = 1;
                        }
                    }
                    //////////////////////////////
                    if (currentAct.Copy != 1)
                        currentAct.IsSleep = 1;

                    Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                    Task currentTask = procedure.GetTask(currentAct.ToTaskCode);

                    if (currentTask.TaskType == 5)
                    {
                        System.Collections.IDictionaryEnumerator ie = workCase.GetActEnumerator();
                        while (ie.MoveNext())
                        {
                            Act act = (Act)ie.Value;
                            if (act.ToTaskCode == currentTask.TaskCode && act.FinishDate == "" && act.Copy != 1)
                            {
                                Rms.WorkFlow.WorkCaseManager.EndWorkCase(workCase, workCase.ApplicationCode, act.ActCode, routerCode, userCodes, this.SystemUserCode, this.HiddenFlowOpinion.Value);
                            }
                        }
                    }

                    DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                    BLL.WorkFlowApplicationRule.SaveWorkCaseEx(ds, workCase.CaseCode);

                    this._SendToTaskType = procedure.GetTask(procedure.GetRouter(routerCode).ToTaskCode).TaskType;
                    ds.Dispose();

                    if (this.HiddenChkMail.Value == "true")
                        SendMail(userCodes, "��һ��" + BLL.WorkFlowRule.GetProcedureNameByCode(this.ViewState["_ProcedureCode"].ToString()) + "������Ҫ����", this.HiddenRouterMessage.Value);

                    Response.Write(Rms.Web.JavaScript.ScriptStart);
                    Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                    Response.Write(Rms.Web.JavaScript.WinClose(false));
                    Response.Write(Rms.Web.JavaScript.ScriptEnd);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        private void CancelCopyReturn(WorkCase workcase, Act act)
        {
            workcase.GetAct(act.ActCode).CopyFromActCode = "";
            DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workcase);
            BLL.WorkFlowApplicationRule.SaveWorkCaseEx(ds, workcase.CaseCode);
            ds.Dispose();
        }

        /// ****************************************************************************
        /// <summary>
        /// �˻ط���
        /// </summary>
        /// ****************************************************************************
        override public void Back()
        {
            string FromTaskCode = this.HiddenSelectRouterCode.Value;
            string userCodes = this.HiddenSelectUserCodes.Value;
            string copyUsers = this.HiddenCopyUsers.Value;
            string routerMessage = BLL.SystemRule.GetUserName(this.SystemUserCode) + " �� " + this.HiddenRouterMessage.Value;

            try
            {
                string ProcedureCode = this.ViewState["_ProcedureCode"].ToString();
                WorkCase workCase = null;
                if (this.ActCode != "")
                {
                    workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                    ///up
                }
                Act currentAct = workCase.GetAct(this.ActCode);

                if (currentAct.Status == ActStatus.End)
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "��������Ѿ����˻��� ��"));
                }
                else
                {
                    Rms.WorkFlow.WorkCaseManager.RetrogradeWorkCase(workCase, this.ApplicationCode, this.ActCode, FromTaskCode, userCodes, this.SystemUserCode, this.HiddenFlowOpinion.Value, routerMessage);


                    if (currentAct.Copy != 1)
                        currentAct.IsSleep = 1;

                    Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                    Task currentTask = procedure.GetTask(currentAct.ToTaskCode);

                    if (currentTask.TaskType == 5)
                    {
                        System.Collections.IDictionaryEnumerator ie = workCase.GetActEnumerator();
                        while (ie.MoveNext())
                        {
                            Act act = (Act)ie.Value;
                            if (act.ToTaskCode == currentTask.TaskCode && act.FinishDate == "" && act.Copy != 1)
                            {
                                Rms.WorkFlow.WorkCaseManager.EndWorkCase(workCase, workCase.ApplicationCode, act.ActCode, FromTaskCode, userCodes, this.SystemUserCode, this.HiddenFlowOpinion.Value);
                            }
                        }
                    }

                    DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                    BLL.WorkFlowApplicationRule.SaveWorkCaseEx(ds, workCase.CaseCode);
                    ds.Dispose();

                    if (this.HiddenChkMail.Value == "true")
                        SendMail(userCodes, "��һ��" + BLL.WorkFlowRule.GetProcedureNameByCode(this.ViewState["_ProcedureCode"].ToString()) + "������Ҫ����", this.HiddenRouterMessage.Value);

                    Response.Write(Rms.Web.JavaScript.ScriptStart);
                    Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                    Response.Write(Rms.Web.JavaScript.WinClose(false));
                    Response.Write(Rms.Web.JavaScript.ScriptEnd);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

        }

        /// ****************************************************************************
        /// <summary>
        /// �ջط���
        /// </summary>
        /// ****************************************************************************
        override public void Return()
        {
            try
            {
                WorkCase workCase = null;
                if (this.ActCode != "")
                {
                    workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                }
                //Rms.WorkFlow.WorkCaseManager.ReturnActWorkCase(workCase, this.ApplicationCode, this.ActCode, "", "", this.SystemUserCode, this.HiddenFlowOpinion.Value, "");

                workCase.GetAct(ActCode).Status = ActStatus.DealWith;
                workCase.GetAct(ActCode).FinishDate = "";
                DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);

                System.Collections.IDictionaryEnumerator ie = workCase.GetActEnumerator();
                while (ie.MoveNext())
                {
                    Act ToAct = (Act)ie.Value;
                    if (ToAct.FromUnitCode == ActCode)
                    {
                        BLL.WorkFlowApplicationRule.SaveWorkCaseEx(ds, workCase.CaseCode, ToAct.ActCode);
                    }
                }
                ds.Dispose();

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

        }

        /// ****************************************************************************
        /// <summary>
        /// �����������
        /// </summary>
        /// ****************************************************************************
        override public void SaveOpinion()
        {
            try
            {
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Act currentAct = workCase.GetAct(this.ActCode);
                if (currentAct.Copy != 1)
                    currentAct.IsSleep = 1;
                currentAct.ApplicationSubject = this.HiddenAuditValue.Value;
                Task currentTask = procedure.GetTask(currentAct.ToTaskCode);
                Rms.WorkFlow.WorkCaseManager.SaveOpinion(currentAct, workCase, this.HiddenFlowOpinion.Value, this.SystemUserCode, currentTask);
                //Rms.WorkFlow.WorkCaseManager.SaveSignOpinionText(workCase,currentAct,currentTask,this.SystemUserCode,this.HiddenFlowOpinion.Value);
                BLL.WorkFlowRule.SaveWorkFlowCase(workCase);
            }

            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }

        /// ****************************************************************************
        /// <summary>
        /// �;����˷���
        /// </summary>
        /// ****************************************************************************
        override public void BackTop()
        {
            string FromTaskCode = "";
            string userCodes = "";
            string routerMessage = BLL.SystemRule.GetUserName(this.SystemUserCode) + " �� " + this.HiddenRouterMessage.Value;

            try
            {
                string ProcedureCode = this.ViewState["_ProcedureCode"].ToString();

                WorkCase workCase = null;
                if (this.ActCode != "")
                {
                    workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                    ///up
                }

                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);

                DataSet dsp = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                DataView dvt = new DataView(dsp.Tables["WorkFlowTask"], String.Format(" TaskType=1 "), "", DataViewRowState.CurrentRows);
                DataView dvr = new DataView(dsp.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", dvt[0].Row["TaskCode"].ToString()), "", DataViewRowState.CurrentRows);
                FromTaskCode = dvr[0].Row["ToTaskCode"].ToString();

                System.Collections.IDictionaryEnumerator ieact = workCase.GetActEnumerator();
                while (ieact.MoveNext())
                {
                    Act act = (Act)ieact.Value;
                    if (act.FromTaskCode == dvt[0].Row["TaskCode"].ToString())
                        userCodes = act.ActUserCode + ",taskActorName=,,undefined;";
                }

                Rms.WorkFlow.WorkCaseManager.RetrogradeWorkCase(workCase, this.ApplicationCode, this.ActCode, FromTaskCode, userCodes, this.SystemUserCode, this.HiddenFlowOpinion.Value, routerMessage);


                Act currentAct = workCase.GetAct(this.ActCode);
                if (currentAct.Copy != 1)
                    currentAct.IsSleep = 1;

                Task currentTask = procedure.GetTask(currentAct.ToTaskCode);

                if (currentTask.TaskType == 5)
                {
                    System.Collections.IDictionaryEnumerator ie = workCase.GetActEnumerator();
                    while (ie.MoveNext())
                    {
                        Act act = (Act)ie.Value;
                        if (act.ToTaskCode == currentTask.TaskCode && act.FinishDate == "" && act.Copy != 1)
                        {
                            Rms.WorkFlow.WorkCaseManager.EndWorkCase(workCase, workCase.ApplicationCode, act.ActCode, FromTaskCode, userCodes, this.SystemUserCode, this.HiddenFlowOpinion.Value);
                        }
                    }
                }

                DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                BLL.WorkFlowApplicationRule.SaveWorkCaseEx(ds, workCase.CaseCode);
                ds.Dispose();

                if (this.HiddenChkMail.Value == "true")
                    SendMail(userCodes, "��һ��" + BLL.WorkFlowRule.GetProcedureNameByCode(this.ViewState["_ProcedureCode"].ToString()) + "������Ҫ����", this.HiddenRouterMessage.Value);

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }

        }


        /// ****************************************************************************
        /// <summary>
        /// ǩ�շ���
        /// </summary>
        /// ****************************************************************************
        override public void SignIn(StandardEntityDAO dao)
        {
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
            Act act = workCase.GetAct(this.ActCode);
            if (act.Status != ActStatus.Begin)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "��������Ѿ���ǩ���� ��"));
            }
            else
            {
                Rms.WorkFlow.WorkCaseManager.SignWorkAct(act, this.SystemUserCode, this.SystemUnitCode);
                DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                BLL.WorkFlowRule.SaveWorkFlowCase(ds, this.ViewState["_CaseCode"].ToString());
                ds.Dispose();

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);

                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);

                Task currentTask = procedure.GetTask(act.ToTaskCode);

                this.InitUserControl();
                this.OutputScript();
            }

        }

        /// ****************************************************************************
        /// <summary>
        /// ��������
        /// </summary>
        /// ****************************************************************************
        override public void Finish()
        {
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
            Act act = workCase.GetAct(this.ActCode);

            if (act.Status == ActStatus.End)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "��������Ѿ��������� ��"));
            }
            else
            {
                Rms.WorkFlow.WorkCaseManager.FinishWorkCase(workCase, act);

                if (act.Copy != 1)
                {
                    act.IsSleep = 1;
                }
                else
                {
                    Act acttemp = workCase.GetAct(act.FromUnitCode);
                    if (acttemp.Status != ActStatus.End)
                        acttemp.CopyFromActCode = this.ActCode;
                }

                DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                BLL.WorkFlowRule.SaveWorkFlowCase(ds, this.ViewState["_CaseCode"].ToString());
                ds.Dispose();

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ���Ϸ���
        /// </summary>
        /// ****************************************************************************
        override public void BlankOut()
        {
            Finish();
        }

        /// ****************************************************************************
        /// <summary>
        /// ���ͷ���
        /// </summary>
        /// ****************************************************************************
        override public void MakeCopy()
        {


            string routerCode = this.HiddenSelectRouterCode.Value;
            string copyUsers = this.HiddenCopyUsers.Value;
            string routerMessage = BLL.SystemRule.GetUserName(this.SystemUserCode) + " �� " + this.HiddenRouterMessage.Value;

            try
            {
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                Act act = workCase.GetAct(this.ActCode);
                if (act.Copy != 1)
                    act.IsSleep = 1;
                if (copyUsers.Length > 0)
                {
                    string CopyFromActCode = "";
                    if (this.HiddenWaitForFlag.Value == "true")
                        CopyFromActCode = this.ActCode;
                    Rms.WorkFlow.WorkCaseManager.ForwardCopyWorkCase(workCase, workCase.ApplicationCode, this.ActCode, routerCode, copyUsers, this.SystemUserCode, this.HiddenFlowOpinion.Value, CopyFromActCode, routerMessage);
                }
                //////////////////////////////
                if (System.Configuration.ConfigurationSettings.AppSettings["PMName"] == "ShiMaoPM")
                {
                    IDictionaryEnumerator ie = workCase.GetActEnumerator();
                    while (ie.MoveNext())
                    {
                        Act tmpact = (Act)ie.Value;
                        if (tmpact.FromUnitCode == this.ActCode && tmpact.Copy == 1)
                            tmpact.IsSleep = 1;
                    }
                }
                //////////////////////////////
                DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                BLL.WorkFlowRule.SaveWorkFlowCase(ds, this.ViewState["_CaseCode"].ToString());
                ds.Dispose();

                if (this.HiddenChkMail.Value == "true")
                    SendMail(copyUsers, "��һ��" + BLL.WorkFlowRule.GetProcedureNameByCode(this.ViewState["_ProcedureCode"].ToString()) + "������Ҫ����", this.HiddenRouterMessage.Value);

                this.InitUserControl();
                this.OutputScript();
                this.Page.RegisterStartupScript("alertcopymessage", "<script>alert('���ͳɹ���');</script>");
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ��ɷ���
        /// </summary>
        /// ****************************************************************************
        override public void TaskFinish()
        {
            string routerCode = this.HiddenSelectRouterCode.Value;
            string userCodes = this.HiddenSelectUserCodes.Value;

            try
            {
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                Act act = workCase.GetAct(this.ActCode);
                if (act.Copy != 1)
                    act.IsSleep = 1;

                Rms.WorkFlow.WorkCaseManager.EndWorkCase(workCase, workCase.ApplicationCode, this.ActCode, routerCode, userCodes, this.SystemUserCode, this.HiddenFlowOpinion.Value);
                DataSet ds = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                BLL.WorkFlowRule.SaveWorkFlowCase(ds, this.ViewState["_CaseCode"].ToString());
                ds.Dispose();

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// ɾ������
        /// </summary>
        /// ****************************************************************************
        override public void Delete()
        {
            try
            {
                DataSet ds = Rms.WorkFlow.WorkCaseManager.WorkCaseDelete(this.ViewState["_CaseCode"].ToString());
                BLL.WorkFlowRule.SaveWorkFlowCase(ds, this.ViewState["_CaseCode"].ToString());
                ds.Dispose();

                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="UserCodes"></param>
        /// <param name="Title"></param>
        /// <param name="Body"></param>
        override public void SendMail(string UserCodes, string Title, string Body)
        {
            try
            {
                string UserMails = "";
                string UserNames = "";
                //����µ���һ����Act,��ǩʱ��ͬʱ���ֶ��act
                foreach (string sss in UserCodes.Split(new char[] { ';' }))
                {
                    if (sss != "")
                    {
                        string[] sus = sss.Split(new char[] { ',' });
                        if (sus[0] != "")
                        {
                            if (UserMails == "")
                            {
                                UserMails = BLL.SystemRule.GetUserMailByCode(sus[0]).Trim();
                            }
                            else
                            {
                                UserMails = ";" + BLL.SystemRule.GetUserMailByCode(sus[0]).Trim();
                            }
                            if (UserNames == "")
                            {
                                UserNames = BLL.SystemRule.GetUserName(sus[0]).Trim();
                            }
                            else
                            {
                                UserNames = "," + BLL.SystemRule.GetUserName(sus[0]).Trim();
                            }

                        }
                    }
                }

                BLL.MailRule mail = new BLL.MailRule();
                mail.Title = Title;
                string url = BLL.ConvertRule.ToString(System.Configuration.ConfigurationSettings.AppSettings["SystemUrl"].ToString());
                string MailBody = UserNames + ",���ã�<br/>"
                + Title + "<br/>"
                + Body + "<br/>"
                + "������¼��<a href=\"" + url + "\" target=\"_blank\">" + url + "</a>�����д���лл��";
                mail.Body = MailBody;
                mail.ToMail = UserMails;
                mail.sendMail();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, ex.Message);
            }
        }


        #endregion ----------------------------------------------------------------------

        #region Web ������������ɵĴ���
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: �õ����� ASP.NET Web ���������������ġ�
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		�����֧������ķ��� - ��Ҫʹ�ô���༭��
        ///		�޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

    }

}
