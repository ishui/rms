using System;
using System.Data;
using RmsPM.Web;

using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;

/// <summary>
/// WorkFlowOperationBase 的摘要说明
/// </summary>
public class WorkFlowOperationBase : System.Web.UI.UserControl
{
    protected System.Web.UI.HtmlControls.HtmlGenericControl up_OperableDiv
    {
        get
        {
            return (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("OperableDiv");
        }
    }
    protected System.Web.UI.HtmlControls.HtmlGenericControl up_EyeableDiv
    {
        get
        {
            return (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("EyeableDiv");
        }
    }

    #region --- 私有成员集合 ---
    /// <summary>
    /// 业务流程代码
    /// </summary>
    private string _ApplicationCode = "";
    /// <summary>
    /// 业务数据代码
    /// </summary>
    private string _OperationCode = "";
    /// <summary>
    /// 业务主题
    /// </summary>
    private string _ApplicationTitle = "";
    /// <summary>
    /// 业务类型
    /// </summary>
    private string _ApplicationType = "";
    /// <summary>
    /// 模块状态
    /// </summary>
    private ModuleState _State = ModuleState.Unbeknown;
    /// <summary>
    /// 附件模块状态
    /// </summary>
    private ModuleState _AttachmentState = ModuleState.Unbeknown;
    /// <summary>
    /// 金额模块状态
    /// </summary>
    private ModuleState _MoneyState = ModuleState.Unbeknown;
    /// <summary>
    /// 项目代码
    /// </summary>
    private string _ProjectCode = "";
    /// <summary>
    /// 合同代码
    /// </summary>
    private string _ContractCode = "";
    /// <summary>
    /// 操作用户
    /// </summary>
    private string _UserCode = "";
    /// <summary>
    /// 业务部门
    /// </summary>
    private string _UnitCode = "";
    /// <summary>
    /// 事务对象
    /// </summary>
    private StandardEntityDAO _dao;
    /// <summary>
    /// 流程属性表
    /// </summary>
    private DataTable _OperationProperty;
    /// <summary>
    /// 流程属性名列表
    /// </summary>
    private string _OperationPropertyNameList = "";
    /// <summary>
    /// 流程属性名列表
    /// </summary>
    private WorkFlowToolbar _ucWorkFlowToolbar = null;

    #endregion --- 私有成员集合 ---

    #region --- 属性集合 ---

    /// <summary>
    /// 业务流程代码
    /// </summary>
    public string ApplicationCode
    {
        get
        {
            if (_ApplicationCode == "")
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
    /// 业务数据代码
    /// </summary>
    public string OperationCode
    {
        get
        {
            if (_OperationCode == "")
            {
                if (this.ViewState["_OperationCode"] != null)
                    return this.ViewState["_OperationCode"].ToString();
                return "";
            }
            return _OperationCode;
        }
        set
        {
            _OperationCode = value;
            this.ViewState["_OperationCode"] = value;
        }
    }

    /// <summary>
    /// 业务主题
    /// </summary>
    public string ApplicationTitle
    {
        get
        {
            if (_ApplicationTitle == "")
            {
                if (this.ViewState["_ApplicationTitle"] != null)
                    return this.ViewState["_ApplicationTitle"].ToString();
                return "";
            }
            return _ApplicationTitle;
        }
        set
        {
            _ApplicationTitle = value;
            this.ViewState["_ApplicationTitle"] = value;
        }
    }

    /// <summary>
    /// 业务类别
    /// </summary>
    public string ApplicationType
    {
        get
        {
            if (_ApplicationType == "")
            {
                if (this.ViewState["_ApplicationType"] != null)
                    return this.ViewState["_ApplicationType"].ToString();
                return "";
            }
            return _ApplicationType;
        }
        set
        {
            _ApplicationType = value;
            this.ViewState["_ApplicationType"] = value;
        }
    }

    /// <summary>
    /// 项目代码
    /// </summary>
    public string ProjectCode
    {
        get
        {
            if (_ProjectCode == "")
            {
                if (this.ViewState["_ProjectCode"] != null)
                    return this.ViewState["_ProjectCode"].ToString();
                return "";
            }
            return _ProjectCode;
        }
        set
        {
            _ProjectCode = value;
            this.ViewState["_ProjectCode"] = value;
        }
    }

    /// <summary>
    /// 合同代码（非合同业务时，值为空）
    /// </summary>
    public string ContractCode
    {
        get
        {
            if (_ContractCode == "")
            {
                if (this.ViewState["_ContractCode"] != null)
                    return this.ViewState["_ContractCode"].ToString();
                return "";
            }
            return _ContractCode;
        }
        set
        {
            _ContractCode = value;
            this.ViewState["_ContractCode"] = value;
        }
    }

    /// <summary>
    /// 操作用户
    /// </summary>
    public string UserCode
    {
        get
        {
            if (_UserCode == "")
            {
                if (this.ViewState["_UserCode"] != null)
                    return this.ViewState["_UserCode"].ToString();
                return "";
            }
            return _UserCode;
        }
        set
        {
            _UserCode = value;
            this.ViewState["_UserCode"] = value;
        }
    }

    /// <summary>
    /// 业务部门
    /// </summary>
    public string UnitCode
    {
        get
        {
            if (_UnitCode == "")
            {
                if (this.ViewState["_UnitCode"] != null)
                    return this.ViewState["_UnitCode"].ToString();
                return "";
            }
            return _UnitCode;
        }
        set
        {
            _UnitCode = value;
            this.ViewState["_UnitCode"] = value;
        }
    }

    /// <summary>
    /// 是否存在手送资料
    /// </summary>
    public bool IsHandmade
    {
        get 
        {
            if (this.ViewState["_IsHandmade"] == null)
                return false;
            return (bool)this.ViewState["_IsHandmade"];
        }
        set
        {
            this.ViewState["_IsHandmade"] = value;
        }
    }

    /// <summary>
    /// 模块状态
    /// </summary>
    public ModuleState State
    {
        get
        {
            if (_State == ModuleState.Unbeknown)
            {
                if (this.ViewState["_State"] != null)
                    return (ModuleState)this.ViewState["_State"];
                return ModuleState.Unbeknown;
            }
            return _State;
        }
        set
        {
            _State = value;
            this.ViewState["_State"] = value;
        }
    }

    /// <summary>
    /// 附件模块状态
    /// </summary>
    public ModuleState AttachmentState
    {
        get
        {
            if (_AttachmentState == ModuleState.Unbeknown)
            {
                if (this.ViewState["_AttachmentState"] != null)
                    return (ModuleState)this.ViewState["_AttachmentState"];
                return ModuleState.Unbeknown;
            }
            return _AttachmentState;
        }
        set
        {
            _AttachmentState = value;
            this.ViewState["_AttachmentState"] = value;
        }
    }

    /// <summary>
    /// 金额模块状态
    /// </summary>
    public ModuleState MoneyState
    {
        get
        {
            if (_MoneyState == ModuleState.Unbeknown)
            {
                if (this.ViewState["_MoneyState"] != null)
                    return (ModuleState)this.ViewState["_MoneyState"];
                return ModuleState.Unbeknown;
            }
            return _MoneyState;
        }
        set
        {
            _MoneyState = value;
            this.ViewState["_MoneyState"] = value;
        }
    }

    /// <summary>
    /// 事务对象
    /// </summary>
    public StandardEntityDAO dao
    {
        get
        {
            return this._dao;
        }
        set
        {
            _dao = value;
        }
    }

    /// <summary>
    /// 流程属性表
    /// </summary>
    public DataTable OperationProperty
    {
        get
        {

            
            if (this._OperationProperty == null  )
            {
                initOperationProperty();

                if ( this.OperationPropertyNameList.Trim() != "" )
                {
                    LoadOperationProperty();
                }
            }

            return this._OperationProperty;
        }
        set
        {
            _OperationProperty = value;
        }
    }

    /// <summary>
    /// 流程属性名列表
    /// </summary>
    public string OperationPropertyNameList
    {
        get
        {
            if (_OperationPropertyNameList == "")
            {
                if (this.ViewState["_OperationPropertyNameList"] != null)
                {
                    return this.ViewState["_OperationPropertyNameList"].ToString();
                }
                return "";
            }
            return _OperationPropertyNameList;
        }
        set
        {
            _OperationPropertyNameList = value;
            this.ViewState["_OperationPropertyNameList"] = value;
        }
    }

    /// <summary>
    /// 流程属性名列表
    /// </summary>
    public WorkFlowToolbar ucWorkFlowToolbar
    {
        get
        {
            return this._ucWorkFlowToolbar;
        }
        set
        {
            _ucWorkFlowToolbar = value;
        }
    }

    #endregion --- 属性集合 ---

    #region --- 公共方法 ---

    /// <summary>
    /// 控件初始化
    /// </summary>
    public virtual void InitControl()
    {
        try
        {
            this.Visible = true;

            switch (this.State)
            {
                case ModuleState.Sightless://不可见的
                    this.Visible = false;
                    break;

                case ModuleState.Operable://可操作的
                    this.LoadData();
                    this.RestoreStatus();
                    this.up_EyeableDiv.Visible = false;
                    this.up_OperableDiv.Visible = true;
                    break;

                case ModuleState.Eyeable://可见的
                    this.LoadData();
                    this.up_OperableDiv.Visible = false;
                    this.up_EyeableDiv.Visible = true;
                    break;

                case ModuleState.Begin://可见的
                    this.LoadData();
                    this.up_OperableDiv.Visible = false;
                    this.up_EyeableDiv.Visible = true;
                    break;

                case ModuleState.End://可见的
                    this.LoadData();
                    this.up_OperableDiv.Visible = false;
                    this.up_EyeableDiv.Visible = true;
                    break;

                default:
                    this.Visible = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }

    /// <summary>
    /// 装载业务控件数据
    /// </summary>
    virtual public void LoadData()
    {
    }

    virtual public void UpdataData()
    {
    }

    virtual public void UpdateDateReturn()
    {
    }
    /// <summary>
    /// 发送时改变业务状态
    /// </summary>
    virtual public string ChangeStatusWhenSend(StandardEntityDAO dao)
    {
        return "";
    }

    /// <summary>
    /// 还原业务数据状态
    /// </summary>
    virtual public string RestoreStatus()
    {
        return "";
    }

   
    /// <summary>
    /// 保存业务控件数据
    /// </summary>
    virtual public string SubmitData()
    {
        return "";
    }

    /// <summary>
    /// 审核业务
    /// </summary>
    virtual public Boolean Audit(string pm_sOpinionConfirm)
    {
        return true;
    }



    /// <summary>
    /// 作废业务
    /// </summary>
    virtual public Boolean BlankOut(StandardEntityDAO dao)
    {
        return true;
    }

    /// <summary>
    /// 删除业务数据
    /// </summary>
    virtual public Boolean Delete(StandardEntityDAO dao)
    {
        return true;
    }


    /// <summary>
    /// 显示业务链结
    /// </summary>
    virtual protected string ShowApplicationHyperLink(string pm_sShowName, string pm_sLinkURL)
    {
        return ShowApplicationHyperLink(pm_sShowName, pm_sLinkURL, "");
    }

    virtual protected string ShowApplicationHyperLink(string pm_sShowName,string pm_sLinkURL,string pm_sStyle)
    {
        string ud_sHyperLink;

        ud_sHyperLink  = "<a href=\"##\" onclick=\"javascript:OpenFullWindow('" + pm_sLinkURL + "','');\"";
        if ( pm_sStyle.Trim() != "" )
        {
            ud_sHyperLink += " style=\"" + pm_sStyle + "\"";
        }

        ud_sHyperLink += ">";
        ud_sHyperLink += pm_sShowName;
        ud_sHyperLink += "</a>";

        return ud_sHyperLink;


    }

    virtual protected void SaveOperationProperty(string pm_sPropertyName, string pm_sPropertyValue)
    {
        try
        {
            string ud_sFilter = string.Format("PropertyName='{0}'", pm_sPropertyName);

            
            if (this.OperationProperty.Select(ud_sFilter).Length > 0)
            {
                foreach (DataRow ud_drProperty in this.OperationProperty.Select(ud_sFilter, "", DataViewRowState.CurrentRows))
                {
                    ud_drProperty["PropertyValue"] = pm_sPropertyValue;
                }
            }
            else
            {
                DataRow ud_drProperty = this.OperationProperty.NewRow();
                ud_drProperty["PropertyName"] = pm_sPropertyName;
                ud_drProperty["PropertyValue"] = pm_sPropertyValue;

                this.OperationProperty.Rows.Add(ud_drProperty);

            }

            if (this.OperationPropertyNameList.IndexOf(pm_sPropertyName) < 0)
            {
                this.OperationPropertyNameList += pm_sPropertyName + ";";
            }

            ViewState[pm_sPropertyName] = pm_sPropertyValue;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            throw ex;
        }

    }

    protected void initOperationProperty()
    {
        try
        {
            this.OperationProperty = new DataTable();

            this.OperationProperty.Columns.Add("PropertyName", typeof(string));
            this.OperationProperty.Columns.Add("PropertyValue", typeof(string));
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            throw ex;
        }
    }

    virtual protected void LoadOperationProperty()
    {
        try
        {
            string[] ud_saOperationPropertyName = this.OperationPropertyNameList.Split( new Char [] { ';'});

            for (int i = 0; i < ud_saOperationPropertyName.Length; i++)
            {
                if ( ud_saOperationPropertyName[i].Trim() != "" && ViewState[ud_saOperationPropertyName[i]] != null)
                {
                    SaveOperationProperty(ud_saOperationPropertyName[i], ViewState[ud_saOperationPropertyName[i]].ToString());
                }
            }

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            throw ex;
        }
    }
    #endregion

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        this.InitEventHandler();
    }

    protected virtual void InitEventHandler()
    {
    }

    public WorkFlowOperationBase()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
}
