//===========================================================================
// 此文件是作为 ASP.NET 2.0 Web 项目转换的一部分修改的。
// 类名已更改，且类已修改为从文件“App_Code\Migrated\workflowcontrol\Stub_workflowtoolbar_ascx_cs.cs”的抽象基类 
// 继承。
// 在运行时，此项允许您的 Web 应用程序中的其他类使用该抽象基类绑定和访问 
// 代码隐藏页。
// 关联的内容页“workflowcontrol\workflowtoolbar.ascx”也已修改，以引用新的类名。
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
    using System.Collections;
    using System.Text;
    using System.Configuration;

    using Rms.ORMap;
    using Rms.WorkFlow;


    /// *******************************************************************************************
    /// <summary>
    ///		WorkFlowToolbar 的摘要说明。工作流应用工具栏。
    /// </summary>
    /// *******************************************************************************************
    public partial class Migrated_WorkFlowToolbar : WorkFlowToolbar
    {
        #region -  表单元素  ------------------------------------------------------------

        /// <summary>
        /// 流程定义代码
        /// </summary>
        protected System.Web.UI.HtmlControls.HtmlInputHidden HiddenProcedureCode;
        /// <summary>
        /// 登录系统用户
        /// </summary>
        protected System.Web.UI.HtmlControls.HtmlInputHidden HiddenSystemUserCode;
        /// <summary>
        /// 抄送等待标识
        /// </summary>
        #endregion ----------------------------------------------------------------------

        #region -  私有成员  ------------------------------------------------------------

        /// <summary>
        /// 流程定义数据
        /// </summary>
        private Procedure _Procedure = null;
        /// <summary>
        /// 抄送会签定义数据
        /// </summary>
        private TaskActor _TaskActor = null;
        /// <summary>
        /// 任务定义数据
        /// </summary>
        private Task _Task = null;
        /// <summary>
        /// 流程实例数据
        /// </summary>
        private WorkCase _WorkCase = null;
        /// <summary>
        /// 动作实例数据
        /// </summary>
        private Act _Act = null;
        /// <summary>
        /// 动作实例代码
        /// </summary>
        private string _ActCode = null;
        /// <summary>
        /// 工作流名称
        /// </summary>
        private string _FlowName = null;
        /// <summary>
        /// 业务实例代码
        /// </summary>
        private string _ApplicationCode = null;
        /// <summary>
        /// 资源路径
        /// </summary>
        private string _SourceUrl = null;
        /// <summary>
        /// 系统用户
        /// </summary>
        private string _SystemUserCode = null;
        /// <summary>
        /// 系统用户单位（部门）
        /// </summary>
        private string _SystemUnitCode = null;
        /// <summary>
        /// 经办人
        /// </summary>
        private string _Transactor = null;
        /// <summary>
        /// 经办单位（部门）
        /// </summary>
        private string _TransactUnit = null;
        /// <summary>
        /// 流程监控标识
        /// </summary>
        private bool _Scout = false;
        private string _ProcedureCode = null;
        /// <summary>
        /// 操作类型
        /// </summary>
        private ToolbarCommandType _CommandType = ToolbarCommandType.Unbeknown;
        /// <summary>
        /// 会签人员列表
        /// </summary>
        private Hashtable _MeetingUsers = new Hashtable();


        #endregion ----------------------------------------------------------------------

        #region -  属性集合  ------------------------------------------------------------

        /// <summary>
        /// 动作实例代码
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
        /// 工作流名称
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
        /// 业务实例代码
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
        /// 资源路径
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
        /// 流程监控标识
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
        /// 系统用户代码
        /// </summary>
        override public string SystemUserCode
        {
            get
            {
                if (_SystemUserCode == null)
                {
                    if (this.ViewState["_SystemUserCode"] != null)
                        return this.ViewState["_SystemUserCode"].ToString();
                    throw new Exception("找不到登录系统用户！");
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
        /// 系统用户单位（部门）代码
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
        /// 项目代码
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
        /// 经办人
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
        /// 经办人
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
        /// 经办单位（部门）
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
        /// 是否允许通过权限显示打印按钮
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
        /// 会签人员列表
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
        /// 操作类型
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
        /// 关闭按钮显示状态
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
        /// 保存按钮显示状态
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
        /// 是否为新增单据
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
        /// 客户端校验脚本（例如：ScriptCheck = "javascript:if(BiddingCheckSubmit()) ";）
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
        /// 删除按钮是否显示
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
        /// 作废按钮是否显示
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
        /// 打印按钮是否显示
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
        /// 选定的路由角色成员列表
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
        /// 选定的路由角色成员完整列表(用户代码，角色代码，用户名称，角色名称；...)
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
        /// 路由下一任务节点类型(默认为 -1;Task类型: 0 "一般节点"; 1 "开始"; 2 "结束"; 3 "并流起点"; 4 "并流交点"; 5 "会签节点";)
        /// </summary>
        override public int SendToTaskType
        {
            get
            {
                return _SendToTaskType;
            }
        }
        /// <summary>
        /// 流程实例代码
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
        /// 手送资料
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
        /// 流程意见
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
        /// 是否可以审批
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
        /// 审批结果
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
        /// 紧急程度 true为紧急  增加人：阚少明 增加日期：2006-11-29 
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

        #region -  事件回传  ------------------------------------------------------------

        /// <summary>
        /// 工具栏操作事件回传
        /// </summary>
        override public event EventHandler ToolbarCommand;

        #endregion ----------------------------------------------------------------------

        #region -  元素方法集合 ---------------------------------------------------------

        /// ****************************************************************************
        /// <summary>
        /// 组件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>只需要给 actCode 属性付值</remarks>
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

                    //this.btnSend.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectRouterControl('Send','选择路由');";
                    //this.btnBack.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectBackControl('back','选择退回上一步');";

                    //this.btnBackTop.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectBackControl('backTop','选择退回经办人');";
                    //this.btnBackEx.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectBackControl('backEx','选择退回');";
                    //this.btnOpinion.Attributes["OnClick"] = "Form1." + this.ClientID + "_btnHiddenForwardOpinion.onclick();selectOpinionControl('Opinion','填写意见');";
                }
                //InitUserControl();

                //OutputScript();
                this.scriptspan.EnableViewState = false;
                this.btnDelete.Attributes["OnClick"] = "javascript:if(confirm('确实要删除当前单据吗？')) ";
                //this.btnBackTop.Attributes["OnClick"] = "javascript:if(confirm('确实要退回给经办人吗？')) ";
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 签收按钮事件
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
        /// 收回按钮事件
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
        /// 保存按钮事件
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
                        this.scriptspan.InnerHtml = "<script language=\"javascript\">selectRouterControl('Send','选择路由')</script>";
                        ToolbarDataBind();
                        this.btnSend.Attributes["OnClick"] = "GetObjectInControl('" + this.ClientID + "','btnHiddenForwardOpinion').onclick();selectRouterControl('Send','选择路由');";
                        this.HiddenCaseCode.Value = "";
                        this.HiddenNewFlow.Value = "";
                    }
                    else
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "保存成功!"));
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
        /// 结束按钮事件
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
        /// 作废按钮事件
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
        /// 发送提交按钮事件
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
        /// 退回提交按钮事件
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
        /// 送经办人按钮事件
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
        /// 抄送提交按钮事件
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
        /// 完成按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// ****************************************************************************
        protected void btnTaskFinish_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                //////////////////////增加的会签完成状态控制逻辑///////////////////////////////////////
                //修改日期：2006-09-28
                //修改人：clm
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Act currentAct = workCase.GetAct(this.ActCode);
                if (StatusEndCount(this.ViewState["_CaseCode"].ToString(), currentAct.ToTaskCode) == 1)
                {
                    this.btnTaskFinish.Visible = false;         //完成按钮

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
                    Page.RegisterClientScriptBlock("finishtask", "<script>alert('抱歉，在您处理过程中其他人已经完成处理，请重新操作。');</script>");
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
        /// 保存意见按钮事件
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
        /// 发送等按钮操作之前意见保存
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
        /// 删除按钮事件
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

        #region -  加载函数  ------------------------------------------------------------

        /// ****************************************************************************
        /// <summary>
        /// 数据加载
        /// </summary>
        /// ****************************************************************************
        private void LoadData()
        {
        }

        /// <summary>
        /// 判断辅助控制中是否存在允许同意否决的项目
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
                    LogHelper.WriteLog("取不到对应流程");
                    throw new Exception("流程配置有误，取不到对应流程");
                }
                currentTask = Rms.WorkFlow.DefinitionManager.GetFirstTask(procedure);
                if (currentTask == null)
                {
                    LogHelper.WriteLog("流程取不到开始节点");
                    throw new Exception("流程配置有误，请检查开始节点的属性");
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


            if (ud_wfmaWorkFlowModuleState[1] != null && ud_wfmaWorkFlowModuleState[1] == ModuleState.Operable)//当前辅助控制项所对应的辅助控制为*|3
                this.IsAudit = true;

        }
        /// ****************************************************************************
        /// <summary>
        /// 初始化
        /// </summary>
        /// ****************************************************************************
        private void InitUserControl()
        {
            if (this.ActCode == "")
            {
                if (this.CaseCode == "")//未开始流程
                {
                    LoadNewFlow();
                }
                else //已经完成的流程
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
                    else //未开始的流程
                    {
                        LoadNewFlow();
                    }
                    QA.Dispose();
                    entity.Dispose();

                    if (StatusTemp == "End")//已经完成的流程
                    {
                        LoadEndFlow();
                    }
                    else if (StatusTemp == "Begin")//途中的流程
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
            else//途中的流程
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
        /// 未开始的流程
        /// </summary>
        private void LoadNewFlow()
        {
            this.btnSignIn.Visible = false;            //签收按钮
            this.btnSave.Visible = true;               //保存按钮
            this.btnSend.Visible = true;               //发送按钮
            this.btnTaskFinish.Visible = false;        //完成按钮
            this.btnFinish.Visible = false;            //结束按钮
            this.btnBack.Visible = false;              //退回按钮
            this.btnBackTop.Visible = false;           //送经办人按钮
            this.btnOpinion.Visible = false;           //意见保存按钮
            this.btnBackEx.Visible = false;            //任意退回
            this.msgbutton.Visible = false;            //消息查看
            this.btnReturn.Visible = false;            //收回
            this.btnBlankOut.Visible = false;          //作废按钮

            this.HiddenCaseCode.Value = "IsNew";

            this.ViewState["_ProcedureCode"] = BLL.WorkFlowRule.GetProcedureCodeByName(this.FlowName, this.ProjectCode);
            this.btnMakeCopy.Visible = false;             //抄送按钮
            this.btnSend.Attributes["OnClick"] = "document.all(\"" + this.ID + "_HiddenCaseCode\").value=\"IsNew\";document.all(\"" + this.ID + "_HiddenNewFlow\").value=\"IsNew\";document.all(\"" + this.ID + "_btnSave\").click();";
        }
        /// <summary>
        /// 已经结束的流程
        /// </summary>
        private void LoadEndFlow()
        {
            this.btnSignIn.Visible = false;            //签收按钮
            this.btnSave.Visible = false;              //保存按钮
            this.btnSend.Visible = false;              //发送按钮
            this.btnTaskFinish.Visible = false;        //完成按钮
            this.btnFinish.Visible = false;            //结束按钮
            this.btnMakeCopy.Visible = false;          //抄送按钮
            this.btnBack.Visible = false;              //退回按钮
            this.btnBackTop.Visible = false;           //送经办人按钮
            this.btnOpinion.Visible = false;           //意见保存按钮
            this.btnBackEx.Visible = false;            //任意退回
            this.msgbutton.Visible = false;            //消息查看
            this.btnReturn.Visible = false;            //收回
            this.btnBlankOut.Visible = false;          //作废按钮



            this.FormNoSpan.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;<b><font color=\"red\">流水号：" + BLL.WorkFlowRule.GetWorkFlowNumber(this.ViewState["_CaseCode"].ToString()) + "</font></b>";
            this.ActCode = BLL.WorkFlowRule.GetLoginUserLastActCode(this.SystemUserCode, this.CaseCode);
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
            this.ViewState["_ProcedureCode"] = workCase.ProcedureCode;
            this.CaseStatus = workCase.Status.ToString();
        }
        /// <summary>
        /// 途中的流程
        /// </summary>
        private void LoadBeginFlow()
        {
            this.HiddenCaseCode.Value = "";
            //////根据动作代码查找流程代码//////
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

            this.FormNoSpan.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;<b><font color=\"red\">流水号：" + BLL.WorkFlowRule.GetWorkFlowNumber(this.ViewState["_CaseCode"].ToString()) + "</font></b>";

            if (this._Scout == true) //流程监控
            {
                this.btnSignIn.Visible = false;            //签收按钮
                this.btnSave.Visible = false;              //保存按钮
                this.btnSend.Visible = false;              //发送按钮
                this.btnTaskFinish.Visible = false;        //完成按钮
                this.btnFinish.Visible = false;            //结束按钮
                this.btnMakeCopy.Visible = false;          //抄送按钮
                this.btnBack.Visible = false;              //退回按钮
                this.btnBackTop.Visible = false;           //送经办人按钮
                this.btnOpinion.Visible = false;           //意见保存按钮
                this.btnBackEx.Visible = false;            //任意退回
                this.msgbutton.Visible = false;            //消息查看
                this.btnBlankOut.Visible = false;          //作废按钮

                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                this.CaseStatus = workCase.Status.ToString();
                this.ViewState["_ProcedureCode"] = workCase.ProcedureCode;
                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                Act currentAct = workCase.GetAct(this.ActCode);                

                if (currentAct.FromUserCode == this.SystemUserCode && currentAct.Status == Rms.WorkFlow.ActStatus.Begin)
                {
                    if (procedure.GetTask(currentAct.FromTaskCode).TaskType == 0 && procedure.GetTask(currentAct.ToTaskCode).TaskType == 0)
                        this.btnReturn.Visible = true;            //收回
                    else
                        this.btnReturn.Visible = false;           //收回
                }
                else
                {
                    this.btnReturn.Visible = false;            //收回
                }

                if (currentAct.ActUserCode != this.SystemUserCode)
                {
                    this.ActCode = BLL.WorkFlowRule.GetLoginUserLastActCode(this.SystemUserCode, this.CaseCode);

                }
            }
            else
            {
                this.btnReturn.Visible = false;            //收回

                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                this.CaseStatus = workCase.Status.ToString();


                //////////////////////////////////////
                //流程意见加载
                System.Collections.IDictionaryEnumerator ieo = workCase.GetOpinionEnumerator();
                while (ieo.MoveNext())
                {
                    Opinion Flowopinion = (Opinion)ieo.Value;
                    if (Flowopinion.ApplicationCode == this.ActCode)
                        this.HiddenFlowOpinion.Value = Flowopinion.OpinionText;
                }
                //////////////////////////////////////

                ///////////////判断等待///////////////
                bool IsWait = false;
                System.Collections.IDictionaryEnumerator ie = workCase.GetActEnumerator();
                while (ie.MoveNext())
                {
                    Act act = (Act)ie.Value;
                    if (act.CopyFromActCode == this.ActCode && act.Status != Rms.WorkFlow.ActStatus.End)
                    {
                        this.btnSend.Attributes["OnClick"] = "alert('等待抄送完成');return false;";
                        this.btnTaskFinish.Attributes["OnClick"] = "alert('等待抄送完成');return false;";
                        this.btnFinish.Attributes["OnClick"] = "alert('等待抄送完成');return false;";
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

                    //验证当前消息是否填写
                    if (msg.IndexOf("：") >= msg.Trim().Length - 1)
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
                if (currentAct.Status == ActStatus.Begin)//未签收状态
                {
                    this.btnSignIn.Visible = true;             //签收按钮
                    this.btnSave.Visible = false;              //保存按钮
                    this.btnSend.Visible = false;              //发送按钮
                    this.btnMakeCopy.Visible = false;          //抄送按钮
                    this.btnTaskFinish.Visible = false;        //完成按钮
                    this.btnFinish.Visible = false;            //结束按钮
                    this.btnBack.Visible = false;              //退回按钮
                    this.btnBackTop.Visible = false;           //送经办人按钮
                    this.btnOpinion.Visible = false;           //意见保存按钮
                    this.btnBackEx.Visible = false;            //任意退回
                    //this.msgbutton.Visible = true;            //消息查看
                    this.btnBlankOut.Visible = false;          //作废按钮
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
                else if (currentAct.Status == ActStatus.DealWith)//已经签收状态
                {
                    //抄送回复解除
                    if (currentAct.CopyFromActCode != "")
                        this.CancelCopyReturn(workCase, currentAct);

                    if (currentAct.Copy == 1)
                    {
                        Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                        Task currentTask = procedure.GetTask(currentAct.ToTaskCode);

                        this.btnSignIn.Visible = false;             //签收按钮
                        this.btnSend.Visible = false;              //发送按钮
                        if (currentTask.GetTaskActor(currentAct.TaskActorID).TaskActorName == "1")
                        {
                            this.btnMakeCopy.Visible = true;          //抄送按钮
                            this.btnMakeCopy.Value = currentTask.TaskActorType;
                        }
                        else
                        {
                            this.btnMakeCopy.Visible = false;          //抄送按钮
                        }

                        this.btnTaskFinish.Visible = false;        //完成按钮
                        this.btnFinish.Visible = true;            //结束按钮
                        this.btnFinish.Value = " 完 成 ";
                        this.btnBack.Visible = false;              //退回按钮
                        this.btnBackTop.Visible = false;           //送经办人按钮
                        this.btnOpinion.Visible = true;           //意见保存按钮
                        this.btnBackEx.Visible = false;            //任意退回
                        this.btnBlankOut.Visible = false;          //作废按钮
                    }
                    else
                    {

                        Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);

                        this.ViewState["_ProcedureCode"] = procedure.ProcedureCode;

                        Task currentTask = procedure.GetTask(currentAct.ToTaskCode);

                        this.btnSignIn.Visible = false;            //签收按钮
                        this.btnSave.Visible = true;

                        if (currentTask.Copy == 1)                  //抄送按钮
                        {
                            this.btnMakeCopy.Visible = true;
                            this.btnMakeCopy.Value = currentTask.TaskActorType;
                            //判断是否为抄送等待状态，并为shimaopm所有 个性化
                            if (!IsWait && System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower() == "shimaopm")
                            {
                                this.btnFinish.Attributes["onclick"] = "GetObjectInControl(this.ClientID,'btnHiddenForwardOpinion').onclick();selectRouterControl('Finish','结束抄送');return false;";
                            }
                        }
                        else
                        {
                            this.btnMakeCopy.Visible = false;
                        }
                        /////////////////////////////////////////
                        DataSet dsx = Rms.WorkFlow.WorkCaseManager.SaveWorkCaseData(workCase);
                        //DataView dvx = new DataView(dsx.Tables["WorkFlowAct"], String.Format(" ToTaskCode='{0}' and ActType <> 1 and Copy <> 1", currentTask.TaskCode), "", DataViewRowState.CurrentRows);
                        // clm 修改 2006-11-2日
                        DataView dvx = new DataView(dsx.Tables["WorkFlowAct"], String.Format(" ToTaskCode='{0}' and Copy <> 1", currentTask.TaskCode), "", DataViewRowState.CurrentRows);

                        BLL.ConvertRule.GetDistinct(dvx.Table, "FromTaskCode", "");

                        string tempTaskCode = "";
                        for (int i = 0; i < dvx.Count; i++)
                        {
                            tempTaskCode = (String)dvx[i].Row["FromTaskCode"];
                        }

                        if (procedure.GetTask(tempTaskCode).TaskType == 1 && dvx.Count == 1)
                        {
                            this.btnBack.Visible = false;              //退回按钮
                            this.btnBackEx.Visible = false;            //任意退回
                        }
                        else
                        {
                            this.btnBack.Visible = true;              //退回按钮
                            this.btnBackEx.Visible = true;            //任意退回
                        }
                        /////////////////////////////////////////
                        this.btnOpinion.Visible = true;           //意见保存按钮

                        DataSet dsp = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                        DataView dvt = new DataView(dsp.Tables["WorkFlowTask"], String.Format(" TaskType=1 "), "", DataViewRowState.CurrentRows);
                        DataView dvr = new DataView(dsp.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", dvt[0].Row["TaskCode"].ToString()), "", DataViewRowState.CurrentRows);
                        if (currentAct.ToTaskCode == dvr[0].Row["ToTaskCode"].ToString())
                            this.btnBackTop.Visible = false;           //送经办人按钮
                        else
                            this.btnBackTop.Visible = true;           //送经办人按钮

                        if (currentTask.TaskType == 5)//会签节点状态判断
                        {
                            this.btnBack.Visible = false;              //退回按钮
                            this.btnBackTop.Visible = false;           //送经办人按钮
                            this.btnBackEx.Visible = false;            //任意退回
                            this.btnBlankOut.Visible = false;          //作废按钮

                            if (StatusEndCount(this.ViewState["_CaseCode"].ToString(), currentTask.TaskCode) == 1)
                            {
                                this.btnTaskFinish.Visible = false;         //完成按钮

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
                            //////  会签过程中允许发送的 /////////////
                            if (this.GetCurrentActSendButtonState(this.SystemUserCode))
                                this.btnSend.Visible = true;
                            ///////////////////////////////////////////

                        }
                        else//非会签节点状态判断
                        {
                            this.btnSend.Visible = true;               //发送按钮
                            this.btnTaskFinish.Visible = false;         //完成按钮
                            /////////////////////////////////////////////////////////
                            this.btnBlankOut.Visible = false;          //作废按钮

                            DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                            DataView dv = new DataView(ds.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", currentTask.TaskCode), "SortID", DataViewRowState.CurrentRows);


                            /////////////////创建属性表///////////////////
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
                                    this.btnSend.Visible = false;   //发送按钮
                                    this.btnFinish.Visible = true;  //结束按钮
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
                                    this.btnFinish.Visible = true;             //结束按钮
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
                // 填充路由rbl
                WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
                Procedure procedure = DefinitionManager.GetProcedureDifinition(workCase.ProcedureCode, true);
                DataSet ds = Rms.WorkFlow.DefinitionManager.SaveProcedureDefinitionData(procedure);
                Act currentAct = workCase.GetAct(this.ActCode);
                DataView dv = new DataView(ds.Tables["WorkFlowRouter"], String.Format(" FromTaskCode='{0}' ", currentAct.ToTaskCode), "SortID", DataViewRowState.CurrentRows);

                /////////////////创建属性表///////////////////
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
        /// 输出脚本程序
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
            sb.Append("         alert( '如要发送，请选择流向 ！' );");
            sb.Append("         return;");
            sb.Append("     }");
            sb.Append("     Form1." + this.ID + "_btnHiddenSend.onclick();");
            sb.Append("	}");

            sb.Append("	function WorkFlowBack()");
            sb.Append(" {");
            sb.Append("     if ( (Form1." + this.ID + "_HiddenSelectRouterCode.value == '' )  || ( Form1." + this.ID + "_HiddenSelectUserCodes.value == '' ))");
            sb.Append("     {");
            sb.Append("         alert( '如要退回，请选择退回流向 ！' );");
            sb.Append("         return;");
            sb.Append("     }");
            sb.Append("     Form1." + this.ID + "_btnHiddenBack.onclick();");
            sb.Append("	}");

            sb.Append("	function WorkFlowMakeCopy()");
            sb.Append(" {");
            sb.Append("     if(document.all(\"" + this.ID + "_HiddenCopyUsers\").value == '')");
            sb.Append("     {");
            sb.Append("         alert( '如要抄送，请选择抄送人员 ！' );");
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
        /// 获取会签中还未完成的数量
        /// </summary>
        /// <param name="CaseCode">流程实体代码</param>
        /// <param name="ToTaskCode">目标流程任务代码</param>
        /// <returns>会签中还未完成的数量</returns>
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

        #region -  公共方法集合  --------------------------------------------------------

        /// <summary>
        /// 工具栏交验
        /// </summary>
        private void ToolbarCheck()
        {
            if (this.RadioHandmade.Visible == true && this.RadioHandmade.SelectedValue == "")
            {
                this.CheckValue = false;
                this.Page.RegisterClientScriptBlock("HandmadeCheckFalse", "<script>alert('请选择手送资料选项！');</script>");
            }
            else
            {
                this.CheckValue = true;
            }

        }
        /// <summary>
        /// 便利手送资料状态
        /// </summary>
        private void InitHandMade()
        {
            switch (GetModuleState("Hand"))
            {
                case ModuleState.Operable:
                    this.HandmadeTable.Visible = true;
                    this.HandMadeLabel.Visible = false;
                    this.RadioHandmade.Visible = true;
                    this.RadioHandmade.SelectedIndex = this.RadioHandmade.Items.IndexOf(this.RadioHandmade.Items.FindByValue(this.GetCasePropertyValue("手送资料")));
                    break;
                case ModuleState.Sightless:
                    this.HandmadeTable.Visible = false;
                    this.HandMadeLabel.Visible = false;
                    this.RadioHandmade.Visible = false;
                    break;
                default:
                    this.HandMadeLabel.Visible = true;
                    this.RadioHandmade.Visible = false;
                    this.HandMadeLabel.Text = this.GetCasePropertyValue("手送资料");
                    this.HandmadeTable.Visible = (this.HandMadeLabel.Text != "");
                    break;
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 进行初始化显示过程。完成组件的状态和数据的刷新。
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
        /// 获取模块状态方法
        /// </summary>
        /// <param name="ModuleId">模块在流程定义中的 ID </param>
        /// <returns>模块状态</returns>
        /// ****************************************************************************
        override public ModuleState GetModuleState(string ModuleId)
        {
            return GetModuleState1(ModuleId, 0);
        }
        /// ****************************************************************************
        /// <summary>
        /// 获取模块check状态方法
        /// </summary>
        /// <param name="ModuleId">模块在流程定义中的 ID </param>
        /// <returns>模块状态</returns>
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

                /************************* 抄送返回状态 *************************/
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

                /************************* 抄送返回状态 *************************/

                


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

            //tempi用于控制当前所要查询的字符串是否存在于辅助控制中  修改人 karen  修改于2006-11-16
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
                            case 1://未知的
                                moduleState = ModuleState.Eyeable;
                                break;
                            case 2://可见的
                                moduleState = ModuleState.Eyeable;
                                break;
                            case 3://可操作的
                                moduleState = ModuleState.Operable;
                                break;
                            case 4://不可见的
                                moduleState = ModuleState.Sightless;
                                break;
                            case 5://其它的
                                moduleState = ModuleState.Other;
                                break;
                            case 6://条件判断
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
        /// 获取模块状态方法(不考虑当前流程状态)
        /// </summary>
        /// <param name="ModuleId">模块在流程定义中的 ID </param>
        /// <returns>模块状态</returns>
        /// ****************************************************************************
        public ModuleState GetModuleStateEx(string ModuleId, int flag)
        {
            return GetDefaultModuleState(ModuleId, flag, true);
        }
        /// ****************************************************************************
        /// <summary>
        /// 获取模块状态方法
        /// </summary>
        /// <param name="ModuleId">模块在流程定义中的 ID </param>
        /// <returns>模块状态</returns>
        /// ****************************************************************************
        override public ModuleState GetModuleState1(string ModuleId, int flag)
        {
            return GetDefaultModuleState(ModuleId, flag, false);
        }

        /// ****************************************************************************
        /// <summary>
        /// 条件状态判断
        /// </summary>
        /// <param name="RouterOrderCode">路由排序代码</param>
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

                ///属性条件判断表
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
        /// 会前中的发送按钮状态
        /// </summary>
        /// <param name="UserCode">用户代码</param>
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

                        // 任务限定条件--属性值
                        string PropertyValue = ""; // 符合限定值
                        string ProcedurePropertyType = ""; // 符合限定类型
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
        /// 获取会签点模块状态
        /// </summary>
        /// <param name="ModuleId">模块在流程定义中的会签点 ID </param>
        /// <returns>模块状态</returns>
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
                ///////////////判断流程监控节点///////////////
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
                                // 任务限定条件--属性值
                                string PropertyValue = ""; // 符合限定值
                                string ProcedurePropertyType = ""; // 符合限定类型
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
                                // 任务限定条件--属性值
                                string PropertyValue = ""; // 符合限定值
                                string ProcedurePropertyType = ""; // 符合限定类型
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
                    //本来在下方被注释掉的位置。个人感觉从程序逻辑上来看，应是要放到这边的。
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
            //tempi用于控制当前所要查询的字符串是否存在于辅助控制中 修改人 阚少明  修改于2006-11-16
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
                            case 1://未知的
                                moduleState = ModuleState.Eyeable;
                                break;
                            case 2://可见的
                                moduleState = ModuleState.Eyeable;
                                break;
                            case 3://可操作的
                                moduleState = ModuleState.Operable;
                                break;
                            case 4://不可见的
                                moduleState = ModuleState.Sightless;
                                break;
                            case 5://其它的
                                moduleState = ModuleState.Other;
                                break;
                            case 6://条件判断
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
        /// 获取用户是否为流程定义用户，流程启动后方可使用。
        /// </summary>
        /// <param name="UserCode">用户代码</param>
        /// <returns>是否为流程用户信息</returns>
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
        /// 保存流程属性属性值
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
        /// 获取流程属性属性值
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
        /// 保存方法
        /// </summary>
        /// ****************************************************************************
        override public void Save()
        {
            if (this.ActCode == "")
            {
                if (this.ApplicationCode == "")
                {
                    throw new Exception("无法定位业务数据，请提供 ApplicationCode ！");
                }
                else
                {
                    if (this.FlowName == "")
                    {
                        throw new Exception("无法定位流程信息，请提供 FlowName ！");
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

                        // 属性添加
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
        /// 发送方法
        /// </summary>
        /// ****************************************************************************
        override public void Send()
        {
            string routerCode = this.HiddenSelectRouterCode.Value;
            string userCodes = this.HiddenSelectUserCodes.Value;
            string copyUsers = this.HiddenCopyUsers.Value;
            string routerMessage = BLL.SystemRule.GetUserName(this.SystemUserCode) + " ： " + this.HiddenRouterMessage.Value;

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
                    Response.Write(Rms.Web.JavaScript.Alert(true, "这个流程已经被发送了 ！"));
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
                        SendMail(userCodes, "有一份" + BLL.WorkFlowRule.GetProcedureNameByCode(this.ViewState["_ProcedureCode"].ToString()) + "工作需要处理！", this.HiddenRouterMessage.Value);

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
        /// 退回方法
        /// </summary>
        /// ****************************************************************************
        override public void Back()
        {
            string FromTaskCode = this.HiddenSelectRouterCode.Value;
            string userCodes = this.HiddenSelectUserCodes.Value;
            string copyUsers = this.HiddenCopyUsers.Value;
            string routerMessage = BLL.SystemRule.GetUserName(this.SystemUserCode) + " ： " + this.HiddenRouterMessage.Value;

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
                    Response.Write(Rms.Web.JavaScript.Alert(true, "这个流程已经被退回了 ！"));
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
                        SendMail(userCodes, "有一份" + BLL.WorkFlowRule.GetProcedureNameByCode(this.ViewState["_ProcedureCode"].ToString()) + "工作需要处理！", this.HiddenRouterMessage.Value);

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
        /// 收回方法
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
        /// 保存意见方法
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
        /// 送经办人方法
        /// </summary>
        /// ****************************************************************************
        override public void BackTop()
        {
            string FromTaskCode = "";
            string userCodes = "";
            string routerMessage = BLL.SystemRule.GetUserName(this.SystemUserCode) + " ： " + this.HiddenRouterMessage.Value;

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
                    SendMail(userCodes, "有一份" + BLL.WorkFlowRule.GetProcedureNameByCode(this.ViewState["_ProcedureCode"].ToString()) + "工作需要处理！", this.HiddenRouterMessage.Value);

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
        /// 签收方法
        /// </summary>
        /// ****************************************************************************
        override public void SignIn(StandardEntityDAO dao)
        {
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
            Act act = workCase.GetAct(this.ActCode);
            if (act.Status != ActStatus.Begin)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "这个流程已经被签收了 ！"));
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
        /// 结束方法
        /// </summary>
        /// ****************************************************************************
        override public void Finish()
        {
            WorkCase workCase = Rms.WorkFlow.WorkCaseManager.GetWorkCase(this.ViewState["_CaseCode"].ToString());
            Act act = workCase.GetAct(this.ActCode);

            if (act.Status == ActStatus.End)
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "这个流程已经被结束了 ！"));
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
        /// 作废方法
        /// </summary>
        /// ****************************************************************************
        override public void BlankOut()
        {
            Finish();
        }

        /// ****************************************************************************
        /// <summary>
        /// 抄送方法
        /// </summary>
        /// ****************************************************************************
        override public void MakeCopy()
        {


            string routerCode = this.HiddenSelectRouterCode.Value;
            string copyUsers = this.HiddenCopyUsers.Value;
            string routerMessage = BLL.SystemRule.GetUserName(this.SystemUserCode) + " ： " + this.HiddenRouterMessage.Value;

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
                    SendMail(copyUsers, "有一份" + BLL.WorkFlowRule.GetProcedureNameByCode(this.ViewState["_ProcedureCode"].ToString()) + "工作需要处理！", this.HiddenRouterMessage.Value);

                this.InitUserControl();
                this.OutputScript();
                this.Page.RegisterStartupScript("alertcopymessage", "<script>alert('抄送成功！');</script>");
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }
        /// ****************************************************************************
        /// <summary>
        /// 完成方法
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
        /// 删除方法
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
        /// 发送邮件
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
                //添加新的下一步的Act,会签时会同时出现多个act
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
                string MailBody = UserNames + ",您好！<br/>"
                + Title + "<br/>"
                + Body + "<br/>"
                + "请您登录“<a href=\"" + url + "\" target=\"_blank\">" + url + "</a>”进行处理，谢谢。";
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
        ///		设计器支持所需的方法 - 不要使用代码编辑器
        ///		修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

    }

}
