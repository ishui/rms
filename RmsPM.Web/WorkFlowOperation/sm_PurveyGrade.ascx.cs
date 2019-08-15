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
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;
using RmsPM.BLL;

public partial class WorkFlowOperation_sm_PurveyGrade : WorkFlowOperationBase
{

    private string _GradeCode;
    private string _GradeMessageCode;
    private string _ConsiderDiathesisCode;
    private string _DepartmentDefineCode;
    private int _GradeValue;
    private string _SupplierCode;
    private ModuleState _State1;//集团合约部


    private ModuleState _State2;//集团技术部
    private ModuleState _State3;//项目总监
    private ModuleState _State4;//项目合约部


    private ModuleState _State5;//项目工程部


    private ModuleState _State6;//项目设计部


    private ModuleState _State7;//客服部


    private ModuleState _DpAllState;//是否全部显示
    private ModuleState _StateProject;//工程是否显示
    private string _GradeType;//Grade为评分　GradeOpinion为审核



    private ModuleState _StatePersentage;//权重是否显示




    /// <summary>
    /// 是否全部显示
    /// </summary>
    public ModuleState StatePersentage
    {
        get
        {
            if (_StatePersentage == ModuleState.Unbeknown)
            {
                if (this.ViewState["_StatePersentage"] != null)
                    return (ModuleState)this.ViewState["_StatePersentage"];
                return ModuleState.Unbeknown;
            }
            return _StatePersentage;
        }
        set
        {
            _StatePersentage = value;
            this.ViewState["_StatePersentage"] = value;
        }
    }



    public string GradeType
    {
        get
        {
            if (_GradeType == null)
            {
                if (this.ViewState["_GradeType"] != null)
                    return this.ViewState["_GradeType"].ToString();
                return "";
            }
            return _GradeType;
        }
        set
        {

            this._GradeType = value;
            this.ViewState["_GradeType"] = value;
        }
    }
    /// <summary>
    /// 是否全部显示
    /// </summary>
    public ModuleState StateProject
    {
        get
        {
            if (_StateProject == ModuleState.Unbeknown)
            {
                if (this.ViewState["_StateProject"] != null)
                    return (ModuleState)this.ViewState["_StateProject"];
                return ModuleState.Unbeknown;
            }
            return _StateProject;
        }
        set
        {
            _StateProject = value;
            this.ViewState["_StateProject"] = value;
        }
    }
    /// <summary>
    /// 是否全部显示
    /// </summary>
    public ModuleState DpAllState
    {
        get
        {
            if (_DpAllState == ModuleState.Unbeknown)
            {
                if (this.ViewState["_DpAllState"] != null)
                    return (ModuleState)this.ViewState["_DpAllState"];
                return ModuleState.Unbeknown;
            }
            return _DpAllState;
        }
        set
        {
            _DpAllState = value;
            this.ViewState["_DpAllState"] = value;
        }
    }
    /// <summary>
    /// 集团合约部


    /// </summary>
    public ModuleState State1
    {
        get
        {
            if (_State1 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_State1"] != null)
                    return (ModuleState)this.ViewState["_State1"];
                return ModuleState.Unbeknown;
            }
            return _State1;
        }
        set
        {
            _State1 = value;
            this.ViewState["_State1"] = value;
        }
    }

    /// <summary>
    /// 集团技术部
    /// </summary>
    public ModuleState State2
    {
        get
        {
            if (_State2 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_State2"] != null)
                    return (ModuleState)this.ViewState["_State2"];
                return ModuleState.Unbeknown;
            }
            return _State2;
        }
        set
        {
            _State2 = value;
            this.ViewState["_State2"] = value;
        }
    }

    /// <summary>
    /// 项目总监
    /// </summary>
    public ModuleState State3
    {
        get
        {
            if (_State3 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_State3"] != null)
                    return (ModuleState)this.ViewState["_State3"];
                return ModuleState.Unbeknown;
            }
            return _State3;
        }
        set
        {
            _State3 = value;
            this.ViewState["_State3"] = value;
        }
    }

    /// <summary>
    /// 项目合约部


    /// </summary>
    public ModuleState State4
    {
        get
        {
            if (_State4 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_State4"] != null)
                    return (ModuleState)this.ViewState["_State4"];
                return ModuleState.Unbeknown;
            }
            return _State4;
        }
        set
        {
            _State4 = value;
            this.ViewState["_State4"] = value;
        }
    }

    /// <summary>
    /// 项目工程部


    /// </summary>
    public ModuleState State5
    {
        get
        {
            if (_State5 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_State5"] != null)
                    return (ModuleState)this.ViewState["_State5"];
                return ModuleState.Unbeknown;
            }
            return _State5;
        }
        set
        {
            _State5 = value;
            this.ViewState["_State5"] = value;
        }
    }

    /// <summary>
    /// 项目设计部


    /// </summary>
    public ModuleState State6
    {
        get
        {
            if (_State6 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_State6"] != null)
                    return (ModuleState)this.ViewState["_State6"];
                return ModuleState.Unbeknown;
            }
            return _State6;
        }
        set
        {
            _State6 = value;
            this.ViewState["_State6"] = value;
        }
    }
    /// <summary>
    /// 客服部


    /// </summary>

    public ModuleState State7
    {
        get
        {
            if (_State7 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_State7"] != null)
                    return (ModuleState)this.ViewState["_State7"];
                return ModuleState.Unbeknown;
            }
            return _State7;
        }
        set
        {
            _State7 = value;
            this.ViewState["_State7"] = value;
        }
    }

    public string GradeCode
    {
        get
        {
            if (_GradeCode == null)
            {
                if (this.ViewState["_GradeCode"] != null)
                    return this.ViewState["_GradeCode"].ToString();
                return "";
            }
            return _GradeCode;
        }
        set
        {

            this._GradeCode = value;
            this.ViewState["_GradeCode"] = value;
        }
    }
    public string SupplierCode
    {
        get
        {
            if (_SupplierCode == null)
            {
                if (this.ViewState["_SupplierCode"] != null)
                    return this.ViewState["_SupplierCode"].ToString();
                return "";
            }
            return _SupplierCode;
        }
        set
        {

            this._SupplierCode = value;
            this.ViewState["_SupplierCode"] = value;
        }
    }


    /// 评分信息Code(FK)
    public string GradeMessageCode
    {
        get
        {
            if (_GradeMessageCode == null)
            {
                if (this.ViewState["_GradeMessageCode"] != null)
                    return this.ViewState["_GradeMessageCode"].ToString();
                return "";
            }
            return _GradeMessageCode;
        }
        set
        {
            this._GradeMessageCode = value;
            this.ViewState["_GradeMessageCode"] = value;
        }
    }

    /// 考虑因素Code(FK)
    public string ConsiderDiathesisCode
    {
        get
        {
            if (_ConsiderDiathesisCode == null)
            {
                if (this.ViewState["_ConsiderDiathesisCode"] != null)
                    return this.ViewState["_ConsiderDiathesisCode"].ToString();
                return "";
            }
            return _ConsiderDiathesisCode;
        }
        set
        {
            this._ConsiderDiathesisCode = value;
            this.ViewState["_ConsiderDiathesisCode"] = value;

        }
    }

    /// 评分部门Code(FK)
    public string DepartmentDefineCode
    {
        get
        {
            if (_DepartmentDefineCode == null)
            {
                if (this.ViewState["_DepartmentDefineCode"] != null)
                    return this.ViewState["_DepartmentDefineCode"].ToString();
                return "";
            }
            return _DepartmentDefineCode;
        }
        set
        {
            this._DepartmentDefineCode = value;
            this.ViewState["_DepartmentDefineCode"] = value;

        }
    }

    /// 分数
    public int GradeValue
    {
        get
        {
            if (_GradeValue == null)
            {
                if (this.ViewState["_GradeValue"] != null)
                    return (int)this.ViewState["_GradeValue"];
                return 0;
            }
            return (int)_GradeValue;
        }
        set
        {
            this._GradeValue = value;
            this.ViewState["_GradeValue"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 组件初始化


    /// </summary>
    override public void InitControl()
    {
        //RmsPM.BLL.GradeMessage.GradeMessageStatusChange(this.GradeMessageCode, 2);
        if (this.ApplicationCode != "")
        {
            this.GradeMessageCode = this.ApplicationCode;
        }
        else if (this.GradeMessageCode != "")
        {
            this.ApplicationCode = this.GradeMessageCode;
        }

        if (GradeType.ToLower() == "gradeopinion")
        {
            this.GradeOpinionDiv.Visible = true;
            this.GradeDiv.Visible = false;
            this.ucGradeOpinionControl.ApplicationCode = this.ApplicationCode;
            this.ucGradeOpinionControl.SupplierCode = this.SupplierCode;
            this.ucGradeOpinionControl.GradeMessageCode = this.GradeMessageCode;
            this.ucGradeOpinionControl.ProjectCode = this.ProjectCode;
            this.ucGradeOpinionControl.State = this.State;
            this.ucGradeOpinionControl.StateProject = this.StateProject;
            this.ucGradeOpinionControl.InitControl();
            this.ApplicationTitle = this.ucGradeOpinionControl.ApplicationTitle;
        }
        else if (GradeType.ToLower() == "gradeopinionandgrade")
        {
            this.ucGradeOpinionControl.SupplierCode = this.SupplierCode;
            this.ucGradeOpinionControl.GradeMessageCode = this.GradeMessageCode;
            this.ucGradeOpinionControl.ProjectCode = this.ProjectCode;
            this.ucGradeOpinionControl.State = this.State;

            switch (this.StateProject)//项目
            {
                case ModuleState.Operable:
                    this.ucGradeControl.StateProject = this.StateProject;
                    break;

                default:
                    this.ucGradeOpinionControl.StateProject = this.StateProject;
                    break;
            }

            this.ucGradeOpinionControl.InitControl();


            this.ucGradeControl.ApplicationCode = this.ApplicationCode;
            this.ucGradeControl.SupplierCode = this.SupplierCode;
            this.ucGradeControl.GradeMessageCode = this.GradeMessageCode;
            this.ucGradeControl.ProjectCode = this.ProjectCode;
            this.ucGradeControl.State = this.State;

            this.ucGradeControl.State1 = this.State1;
            this.ucGradeControl.State2 = this.State2;
            this.ucGradeControl.State3 = this.State3;
            this.ucGradeControl.State4 = this.State4;
            this.ucGradeControl.State5 = this.State5;
            this.ucGradeControl.State6 = this.State6;
            this.ucGradeControl.State7 = this.State7;
            this.ucGradeControl.StatePersentage = this.StatePersentage;
            this.ucGradeControl.InitControl();

        }
        else
        {
            this.GradeOpinionDiv.Visible = false;
            this.GradeDiv.Visible = true;

            this.ucGradeControl.ApplicationCode = this.ApplicationCode;
            this.ucGradeControl.SupplierCode = this.SupplierCode;
            this.ucGradeControl.GradeMessageCode = this.GradeMessageCode;
            this.ucGradeControl.ProjectCode = this.ProjectCode;
            this.ucGradeControl.State = this.State;
            this.ucGradeControl.StateProject = this.StateProject;
            this.ucGradeControl.State1 = this.State1;
            this.ucGradeControl.State2 = this.State2;
            this.ucGradeControl.State3 = this.State3;
            this.ucGradeControl.State4 = this.State4;
            this.ucGradeControl.State5 = this.State5;
            this.ucGradeControl.State6 = this.State6;
            this.ucGradeControl.State7 = this.State7;
            this.ucGradeControl.StatePersentage = this.StatePersentage;
            this.ucGradeControl.InitControl();

        }
    }


    /// <summary>
    /// 保存控件数据
    /// </summary>
    public override string SubmitData()
    {
        if (GradeType.ToLower() == "gradeopinion")
        {

            return "";
        }
        else
        {
            string ErrMsg = this.ucGradeControl.SubmitGradeData();
            string str = this.ProjectCode;
            this.ProjectCode = this.ucGradeControl.ProjectCode;
            this.ApplicationTitle = this.ucGradeControl.ApplicationTitle;
            this.UnitCode = this.ucGradeControl.UnitCode;
            this.ApplicationType = this.ucGradeControl.ApplicationType;
            this.ApplicationCode = this.ucGradeControl.ApplicationCode;
            return ErrMsg;
        }
    }

    /// <summary>
    /// 业务审核
    /// </summary>
    public override bool Audit(string pm_sOpinionConfirm)
    {
        base.Audit(pm_sOpinionConfirm);

        try
        {
            GradeMessage gm = new GradeMessage();
            string ErrMsg = "";

            if (pm_sOpinionConfirm != "")
            {



                EntityData Entity = gm.GetGradeMessageByCode(this.GradeMessageCode);

                Entity.SetCurrentTable("GradeMessage");

                if (Entity.HasRecord())
                {
                    DataRow dr = Entity.CurrentRow;

                    switch (pm_sOpinionConfirm)
                    {
                        case "Approve":

                            dr["state"] = 0;


                            break;
                        case "Reject":

                            dr["state"] = 1;

                            break;
                        case "Unknow":
                            ErrMsg = "请选择评审结果！";
                            break;
                        default:
                            ErrMsg = "请选择评审结果！";
                            break;
                    }


                    if (ErrMsg != "")
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));
                        return false;
                    }


                    gm.SubmitAllGradeMessage(Entity);

                }

                Entity.Dispose();
            }

            return true;

        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "业务审核出错：" + ex.Message));
            throw ex;
        }
    }

    public override string ChangeStatusWhenSend(StandardEntityDAO dao)
    {
        try
        {

            base.ChangeStatusWhenSend(dao);

            string ErrMsg = "";
            RmsPM.BLL.GradeMessage.GradeMessageStatusChange(this.ApplicationCode, 7);



            return ErrMsg;
        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "改变业务数据状态出错：" + ex.Message));
            throw ex;
        }

    }

    public override string RestoreStatus()
    {
        try
        {

            base.RestoreStatus();

            string ErrMsg = "";

            RmsPM.BLL.GradeMessage.GradeMessageStatusChange(this.ApplicationCode, 1);
            return ErrMsg;
        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "恢复业务数据状态出错：" + ex.Message));
            throw ex;
        }
    }


}
