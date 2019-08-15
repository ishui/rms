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
using RmsPM.BLL;
using Rms.ORMap;
using RmsPM.DAL.QueryStrategy;

public partial class SupplierGrade_Control_SupplierGrade : WorkFlowOperationBase
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
    private ModuleState _StatePersentage;//权重是否显示
    protected RmsPM.Web.User user = null;

    //调试状态下用admin 



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

        if (this.State == ModuleState.Sightless)//不可见的
        {
            this.Visible = false;
        }
        else if (this.State == ModuleState.Operable)//可操作的
        {

            LoadData(true);
            this.EyeableDiv.Visible = false;
            this.OperableDiv.Visible = true;
        }
        else if (this.State == ModuleState.Eyeable)//可见的

        {

            LoadData(false);
            this.EyeableDiv.Visible = true;
            this.OperableDiv.Visible = false;
        }
        else if (this.State == ModuleState.Begin)//可见的

        {

            LoadData(false);
            this.EyeableDiv.Visible = true;
            this.OperableDiv.Visible = false;
        }
        else if (this.State == ModuleState.End)//可见的

        {

            LoadData(false);
            this.EyeableDiv.Visible = true;
            this.OperableDiv.Visible = false;
        }
        else
        {
            this.Visible = false;
        }
    }

    /// ****************************************************************************
    /// <summary>
    /// 数据加载
    /// </summary>
    /// ****************************************************************************
    private void LoadData(bool Flag)
    {
        if (this.ApplicationCode != "")
        {
            this.GradeMessageCode = this.ApplicationCode;
        }
        else if (this.GradeMessageCode != "")
        {
            this.ApplicationCode = this.GradeMessageCode;
        }

        if ((Session["User"] == null) && (ConfigurationSettings.AppSettings["IsDebug"] == "1") && (ConfigurationSettings.AppSettings["DebugUser"] != ""))
        {
            Session["User"] = new RmsPM.Web.User(ConfigurationSettings.AppSettings["DebugUser"]);
        }

        if (Session["User"] != null)
        {
            this.user = (RmsPM.Web.User)Session["User"];
        }

        string TotalAgreementPoint = "0";  //集团合约部总分
        string TotalTechnic = "0";//集团技术部总分
        string TotalItemMajordomo = "0";//项目总监总分
        string TotalItemAgreement = "0"; //项目合约部总分
        string TotalItemEngineering = "0";//项目工程部总分
        string TotalItemDesign = "0";//项目设计部总分
        string TotalClientService = "0"; //客服部总分

        string lastTotalAgreementPoint = "0";  //集团合约部最后总分
        string lastTotalTechnic = "0";//集团技术部最后总分
        string lastTotalItemMajordomo = "0";//项目总监最后总分
        string lastTotalItemAgreement = "0"; //项目合约部最后总分
        string lastTotalItemEngineering = "0";//项目工程部最后总分
        string lastTotalItemDesign = "0";//项目设计部最后总分
        string lastTotalClientService = "0"; //客服部最后总分

        string lastTotalPoint = "0";//综合得分
        if (this.ApplicationCode != "")
        {

            //总权重


            this.lblTotalPercentage.Text = "100%";
            this.OPlblTotalPercentage.Text = "100%";
            //集团合约部总分
            TotalAgreementPoint = RmsPM.BLL.GradeList.GetGradePoint("100001", this.GradeMessageCode);
            this.lblTotalAgreement.Text = TotalAgreementPoint;
            this.OPlblTotalAgreement.Text = TotalAgreementPoint;
            ////集团技术部总分
            TotalTechnic = RmsPM.BLL.GradeList.GetGradePoint("100002", this.GradeMessageCode);
            this.lblTotalTechnic.Text = TotalTechnic;
            this.OPlblTotalTechnic.Text = TotalTechnic;
            //项目总监总分
            TotalItemMajordomo = RmsPM.BLL.GradeList.GetGradePoint("100003", this.GradeMessageCode);
            this.lblTotalItemMajordomo.Text = TotalItemMajordomo;
            this.OPlblTotalItemMajordomo.Text = TotalItemMajordomo;
            //项目合约部总分
            TotalItemAgreement = RmsPM.BLL.GradeList.GetGradePoint("100004", this.GradeMessageCode);
            this.lblTotalItemAgreement.Text = TotalItemAgreement;
            this.OPlblTotalItemAgreement.Text = TotalItemAgreement;
            //项目工程部总分
            TotalItemEngineering = RmsPM.BLL.GradeList.GetGradePoint("100005", this.GradeMessageCode);
            this.lblTotalItemEngineering.Text = TotalItemEngineering;
            this.OPlblTotalItemEngineering.Text = TotalItemEngineering;
            //项目设计部总分
            TotalItemDesign = RmsPM.BLL.GradeList.GetGradePoint("100006", this.GradeMessageCode);
            this.lblTotalItemDesign.Text = TotalItemDesign;
            this.OPlblTotalItemDesign.Text = TotalItemDesign;
            //客服部总分
            TotalClientService = RmsPM.BLL.GradeList.GetGradePoint("100007", this.GradeMessageCode);
            this.lblTotalClientService.Text = TotalClientService;
            this.OPlblTotalClientService.Text = TotalClientService;


            RmsPM.BLL.GradeDepartment gradeDepartment = new GradeDepartment();
            DataTable Dt = gradeDepartment.GetGradeDepartments();
            RmsPM.BLL.GradeDepartmentPercentage cgradeDepartmentPercentage = new GradeDepartmentPercentage();
            DataTable dtgradeDepartmentPercentage = cgradeDepartmentPercentage.GetLastDepartmentPercentage(this.GradeMessageCode, "100001");
            //调整系数 权重 汇总

            decimal DepartmentSumPercentage = 0;
            foreach (DataRow drDepartmentSp in dtgradeDepartmentPercentage.Select())
            {
                DepartmentSumPercentage += (decimal)drDepartmentSp["Percentage"];
            }

            if (Dt != null)
            {

                //DataRow[] drAgreementTZ = Dt.Select("DepartmentDefineCode='100001'");
                foreach (DataRow drAgreementTZ in Dt.Select("MainDefineCode='100001'"))
                {
                    switch (drAgreementTZ["DepartmentDefineCode"].ToString())
                    {
                        //集团合约部


                        case "100001":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100001'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100001'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100001'")[0]["AdjustCoefficient"])!="")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100001'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100001'")[0]["Percentage"];
                            }
                            this.TZAgreement.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.QZAgreement.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalAgreementPoint = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalAgreementPoint) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalAgreementPoint == "")
                            {
                                lastTotalAgreementPoint = "0";
                            }
                            this.lblTotalAgreement1.Text = lastTotalAgreementPoint;
                            this.OPTZAgreement.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.OPQZAgreement.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            this.OPlblTotalAgreement1.Text = lastTotalAgreementPoint;
                            break;
                        case "100002":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'")[0]["Percentage"];
                            }
                            this.TZTechnic.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.QZTechnic.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalTechnic = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalTechnic) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalTechnic == "")
                            {
                                lastTotalTechnic = "0";
                            }
                            this.lblTotalTechnic1.Text = lastTotalTechnic;
                            this.OPTZTechnic.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.OPQZTechnic.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            this.OPlblTotalTechnic1.Text = lastTotalTechnic;
                            break;
                        case "100003":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'")[0]["Percentage"];
                            }
                            this.TZItemMajordomo.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.QZItemMajordomo.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalItemMajordomo = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalItemMajordomo) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalItemMajordomo == "")
                            {
                                lastTotalItemMajordomo = "0";
                            }
                            this.lblTotalItemMajordomo1.Text = lastTotalItemMajordomo;
                            this.OPTZItemMajordomo.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.OPQZItemMajordomo.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            this.OPlblTotalItemMajordomo1.Text = lastTotalItemMajordomo;
                            break;
                        case "100004":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'")[0]["Percentage"];
                            }
                            this.TZItemAgreement.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.QZItemAgreement.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalItemAgreement = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalItemAgreement) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalItemAgreement == "")
                            {
                                lastTotalItemAgreement = "0";
                            }
                            this.lblTotalItemAgreement1.Text = lastTotalItemAgreement;
                            this.OPTZItemAgreement.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.OPQZItemAgreement.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            this.OPlblTotalItemAgreement1.Text = lastTotalItemAgreement;
                            break;
                        case "100005":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'")[0]["Percentage"];
                            }
                            this.TZItemEngineering.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.QZItemEngineering.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalItemEngineering = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalItemEngineering) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalItemEngineering == "")
                            {
                                lastTotalItemEngineering = "0";
                            }
                            this.lblTotalItemEngineering1.Text = lastTotalItemEngineering;
                            this.OPTZItemEngineering.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.OPQZItemEngineering.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            this.OPlblTotalItemEngineering1.Text = lastTotalItemEngineering;
                            break;

                        case "100006":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'")[0]["Percentage"];
                            }
                            this.TZItemDesign.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.QZItemDesign.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalItemDesign = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalItemDesign) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalItemDesign == "")
                            {
                                lastTotalItemDesign = "0";
                            }
                            this.lblTotalItemDesign1.Text = lastTotalItemDesign;
                            this.OPTZItemDesign.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.OPQZItemDesign.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            this.OPlblTotalItemDesign1.Text = lastTotalItemDesign;
                            break;
                        case "100007":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'")[0]["Percentage"];
                            }
                            this.TZClientService.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.QZClientService.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalClientService = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalClientService) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalClientService == "")
                            {
                                lastTotalClientService = "0";
                            }
                            this.lblTotalClientService1.Text = lastTotalClientService;
                            this.OPTZClientService.InnerHtml = "*" + drAgreementTZ["AdjustCoefficient"].ToString();
                            this.OPQZClientService.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            this.OPlblTotalClientService1.Text = lastTotalClientService;
                            break;
                    }
                }

            }



            lastTotalPoint = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(lastTotalAgreementPoint) + System.Convert.ToDecimal(lastTotalTechnic) + System.Convert.ToDecimal(lastTotalItemMajordomo) + System.Convert.ToDecimal(lastTotalItemAgreement) + System.Convert.ToDecimal(lastTotalItemEngineering) + System.Convert.ToDecimal(lastTotalItemDesign) + System.Convert.ToDecimal(lastTotalClientService));
            OPlblpoint.Text = lastTotalPoint;
            lblpoint.Text = lastTotalPoint;//综合得分


            GradeMessage gm = new GradeMessage();
            gm.GradeMessageCode = this.GradeMessageCode;
            gm.GetGradeMessages();
            this.ProjectCode = gm.ProjectCode;
            this.SupplierCode = gm.SupplierCode;
            this.SupplierManagerModi.Value = gm.ProjectManage;
            this.SupplierManagerList.Text = gm.ProjectManage;

        }

        //如果没有项目编号，将让用户自行选择
        if (this.ProjectCode == "")
        {
            this.lblProjectNameList.Visible = false;
            this.DropDownProject.Visible = true;
            this.DropDownProject.DataSource = new DataView(user.m_EntityDataAccessProject.CurrentTable, "", "ProjectName", DataViewRowState.CurrentRows);
            this.DropDownProject.DataTextField = "ProjectShortName";
            this.DropDownProject.DataValueField = "ProjectCode";
            this.DropDownProject.DataBind();
            this.ProjectCode = this.DropDownProject.SelectedValue;
        }
        else
        {
            this.DropDownProject.Visible = false;
            this.lblProjectNameList.Visible = true;
            this.lblProjectNameList.Text = ProjectRule.GetProjectName(this.ProjectCode);
            this.lblProjectNameModi.Text = this.lblProjectNameList.Text;
        }



        GradeConsiderDiathesis gcd = new GradeConsiderDiathesis();
        gcd.MainDefineCode = "100001";
        DataTable dt = gcd.GetGradeConsiderDiathesiss();
        DataTable returndt = gcd.GetGradeConsiderDiathesiss();


        //获取当前子项权重
        RmsPM.BLL.GradeConsiderPercentage cgradeConsiderPercentage = new GradeConsiderPercentage();

        string ConsiderDiathesisCodeFilter = "";
        int ConsiderDiathesisIndex = 0;
        if (this.GradeMessageCode != "")
        {
            DataTable dtgradeConsiderPercentage = cgradeConsiderPercentage.GetLastConsiderPercentage(this.GradeMessageCode, "100001");

            if (dtgradeConsiderPercentage != null && dtgradeConsiderPercentage.Rows.Count != 0)
            {
                foreach (DataRow drgradeConsiderPercentage in dtgradeConsiderPercentage.Select())
                {
                    if (dt.Select("ConsiderDiathesisCode='" + drgradeConsiderPercentage["ConsiderDiathesisCode"].ToString() + "'").Length != 0)
                        dt.Select("ConsiderDiathesisCode='" + drgradeConsiderPercentage["ConsiderDiathesisCode"].ToString() + "'")[0]["Percentage"] = (decimal)drgradeConsiderPercentage["Percentage"];


                    if (ConsiderDiathesisIndex != dtgradeConsiderPercentage.Select().Length - 1)
                    {
                        ConsiderDiathesisCodeFilter = ConsiderDiathesisCodeFilter + "'" + drgradeConsiderPercentage["ConsiderDiathesisCode"].ToString() + "',";
                    }
                    else
                    {
                        ConsiderDiathesisCodeFilter = ConsiderDiathesisCodeFilter + "'" + drgradeConsiderPercentage["ConsiderDiathesisCode"].ToString() + "'";
                    }
                    ConsiderDiathesisIndex++;
                }

                foreach (DataRow tempConsiderDiathesisdt in dt.Select("ConsiderDiathesisCode not in(" + ConsiderDiathesisCodeFilter + ")"))
                {
                    if (dtgradeConsiderPercentage.Select("ConsiderDiathesisCode='" + tempConsiderDiathesisdt["ParentCode"] + "'").Length != 0)
                        tempConsiderDiathesisdt["Percentage"] = (decimal)dtgradeConsiderPercentage.Select("ConsiderDiathesisCode='" + tempConsiderDiathesisdt["ParentCode"] + "'")[0]["Percentage"];
                }

            }
        }

        //组织结构
        Grade gv = new Grade();
        gv.GradeMessageCode = this.GradeMessageCode;
        DataTable Gradedt = gv.GetGrades();
        RmsPM.BLL.ConvertRule.GetAllTreeDataSource(dt, returndt, Gradedt, GradeMessageCode, "ConsiderDiathesisCode", "ParentCode", "", "", "", 1, 0, "");
        //
        switch (this.StateProject)//项目
        {
            case ModuleState.Operable:
                this.messagelist.Visible = false;
                this.messageMoid.Visible = true;
                this.txtSupplierName.Disabled = false;
                this.txtSupplierCode.Disabled = false;

                this.supplierChange.Visible = true;
                this.SupplierManagerModi.Disabled = false;
                break;
            case ModuleState.Eyeable:
                this.messagelist.Visible = true;
                this.messageMoid.Visible = false;
                this.txtSupplierName.Disabled = true;
                this.txtSupplierCode.Disabled = true;

                this.supplierChange.Visible = false;
                this.SupplierManagerModi.Disabled = true;

                break;
            case ModuleState.Begin:
                this.messagelist.Visible = true;
                this.messageMoid.Visible = false;
                this.txtSupplierName.Disabled = true;
                this.txtSupplierCode.Disabled = true;

                this.supplierChange.Visible = false;
                this.SupplierManagerModi.Disabled = true;

                break;
            case ModuleState.End:
                this.messagelist.Visible = true;
                this.messageMoid.Visible = false;
                this.txtSupplierName.Disabled = true;
                this.txtSupplierCode.Disabled = true;

                this.supplierChange.Visible = false;
                this.SupplierManagerModi.Disabled = true;

                break;
            default:
                this.messagelist.Visible = false;
                this.messageMoid.Visible = false;
                break;
        }

        if (!Flag)
        {
            this.SupplierNameList.Text = ProjectRule.GetSupplierName(this.SupplierCode);
            //this.messagelist.Visible = false;
            // this.messageMoid.Visible = true;
            // this.GradeList.Visible = false;
            // this.GradeModify.Visible = true;

            this.Repeater2.DataSource = returndt;
            this.Repeater2.DataBind();
        }
        else
        {
            this.txtSupplierName.Value = ProjectRule.GetSupplierName(this.SupplierCode);
            this.txtSupplierCode.Value = SupplierCode;
            this.ApplicationTitle = this.txtSupplierName.Value;
            //this.messagelist.Visible = false;
            // this.messageMoid.Visible = true;
            // this.GradeList.Visible = false;
            // this.GradeModify.Visible = true;
            this.Repeater1.DataSource = returndt;
            this.Repeater1.DataBind();



            for (int i = 0; i < Repeater1.Items.Count; i++)
            {

                if (((Label)Repeater1.Items[i].FindControl("LabelFlag")).Text == "1")
                {


                    //项目权重
                    TextBox TxtPercentage = ((TextBox)Repeater1.Items[i].FindControl("TxtPercentage"));
                    switch (this.StatePersentage)
                    {
                        case ModuleState.Operable:
                            // lblAgreement.Visible = true;
                            TxtPercentage.Visible = true;
                            // lblAgreement.Enabled = true;
                            if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text != "1")
                            {
                                TxtPercentage.Enabled = true;
                            }
                            break;
                        case ModuleState.Eyeable:
                            // lblAgreement.Visible = true;
                            TxtPercentage.Visible = true;
                            // lblAgreement.Enabled = false;
                            TxtPercentage.Enabled = false;
                            break;
                        default:
                            // lblAgreement.Visible = true;
                            TxtPercentage.Visible = false;
                            // lblAgreement.Enabled = false;
                            //txtAgreement.Enabled = false;

                            break;
                    }


                    //集团合约部


                    // Label lblAgreement = ((Label)Repeater1.Items[i].FindControl("lblAgreement"));
                    TextBox txtAgreement = ((TextBox)Repeater1.Items[i].FindControl("txtAgreement"));
                    Label lblAgreement = (Label)Repeater1.Items[i].FindControl("lblAgreement1");
                    if (lblAgreement.Text.Trim() != "0")
                    {
                        switch (this.State1)
                        {
                            case ModuleState.Operable:
                                // lblAgreement.Visible = true;
                                txtAgreement.Visible = true;
                                // lblAgreement.Enabled = true;
                                if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text != "1")
                                {
                                    txtAgreement.Enabled = true;
                                }
                                break;
                            case ModuleState.Eyeable:
                                // lblAgreement.Visible = true;
                                txtAgreement.Visible = true;
                                // lblAgreement.Enabled = false;
                                txtAgreement.Enabled = false;
                                break;
                            default:
                                // lblAgreement.Visible = true;
                                txtAgreement.Visible = false;
                                // lblAgreement.Enabled = false;
                                //txtAgreement.Enabled = false;
                                OPlblTotalAgreement.Visible = false;
                                OPTZAgreement.InnerHtml = "&nbsp;";
                                OPQZAgreement.InnerHtml = "&nbsp;";
                                OPlblTotalAgreement1.Visible = false;
                                break;
                        }
                    }


                    //集团技术部
                    //Label lblTechnic = ((Label)Repeater1.Items[i].FindControl("lblTechnic"));
                    TextBox txtTechnic = ((TextBox)Repeater1.Items[i].FindControl("txtTechnic"));
                    Label lblTechnic = (Label)Repeater1.Items[i].FindControl("lblTechnic1");
                    if (lblTechnic.Text.Trim() != "0")
                    {
                        switch (this.State2)
                        {
                            case ModuleState.Operable:
                                //lblTechnic.Visible = true;
                                txtTechnic.Visible = true;
                                //lblTechnic.Enabled = true;
                                if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text != "1")
                                {
                                    txtTechnic.Enabled = true;
                                }

                                break;
                            case ModuleState.Eyeable:
                                //lblTechnic.Visible = true;
                                txtTechnic.Visible = true;
                                //lblTechnic.Enabled = false;
                                txtTechnic.Enabled = false;
                                break;
                            default:
                                //lblTechnic.Visible = true;
                                txtTechnic.Visible = false;
                                OPlblTotalTechnic.Visible = false;
                                OPTZTechnic.InnerHtml = "&nbsp;";
                                OPQZTechnic.InnerHtml = "&nbsp;";
                                OPlblTotalTechnic1.Visible = false;
                                break;
                        }
                    }

                    //项目总监

                    //Label lblItemMajordomo = ((Label)Repeater1.Items[i].FindControl("lblItemMajordomo"));
                    TextBox txtItemMajordomo = ((TextBox)Repeater1.Items[i].FindControl("txtItemMajordomo"));
                    Label lblItemMajordomo = (Label)Repeater1.Items[i].FindControl("lblItemMajordomo1");
                    if (lblItemMajordomo.Text.Trim() != "0")
                    {
                        switch (this.State3)
                        {
                            case ModuleState.Operable:
                                //lblItemMajordomo.Visible = true;
                                txtItemMajordomo.Visible = true;
                                //lblItemMajordomo.Enabled = true;
                                if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text != "1")
                                {
                                    txtItemMajordomo.Enabled = true;
                                }

                                break;
                            case ModuleState.Eyeable:
                                //lblItemMajordomo.Visible = true;
                                txtItemMajordomo.Visible = true;
                                //lblItemMajordomo.Enabled = false;
                                txtItemMajordomo.Enabled = false;
                                break;
                            default:
                                //lblItemMajordomo.Visible = true;
                                txtItemMajordomo.Visible = false;
                                OPlblTotalItemMajordomo.Visible = false;
                                OPTZItemMajordomo.InnerHtml = "&nbsp;";
                                OPQZItemMajordomo.InnerHtml = "&nbsp;";
                                OPlblTotalItemMajordomo1.Visible = false;
                                break;
                        }
                    }

                    //项目合约部




                    //Label lblItemAgreement = ((Label)Repeater1.Items[i].FindControl("lblItemAgreement"));
                    TextBox txtItemAgreement = ((TextBox)Repeater1.Items[i].FindControl("txtItemAgreement"));
                    Label lblItemAgreement = (Label)Repeater1.Items[i].FindControl("lblItemAgreement1");
                    if (lblItemAgreement.Text.Trim() != "0")
                    {
                        switch (this.State4)
                        {
                            case ModuleState.Operable:
                                //lblItemAgreement.Visible = true;
                                txtItemAgreement.Visible = true;
                                //lblItemAgreement.Enabled = true;
                                if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text != "1")
                                {
                                    txtItemAgreement.Enabled = true;
                                }

                                break;
                            case ModuleState.Eyeable:
                                //lblItemAgreement.Visible = true;
                                txtItemAgreement.Visible = true;
                                //lblItemAgreement.Enabled = false;
                                txtItemAgreement.Enabled = false;
                                break;
                            default:
                                //lblItemAgreement.Visible = true;
                                txtItemAgreement.Visible = false;
                                OPlblTotalItemAgreement.Visible = false;
                                OPTZItemAgreement.InnerHtml = "&nbsp;";
                                OPQZItemAgreement.InnerHtml = "&nbsp;";
                                OPlblTotalItemAgreement1.Visible = false;
                                break;
                        }
                    }
                    //项目工程部




                    // Label lblItemEngineering = ((Label)Repeater1.Items[i].FindControl("lblItemEngineering"));
                    TextBox txtItemEngineering = ((TextBox)Repeater1.Items[i].FindControl("txtItemEngineering"));
                    Label lblItemEngineering = (Label)Repeater1.Items[i].FindControl("lblItemEngineering1");
                    if (lblItemEngineering.Text.Trim() != "0")
                    {
                        switch (this.State5)
                        {
                            case ModuleState.Operable:
                                // lblItemEngineering.Visible = true;
                                txtItemEngineering.Visible = true;
                                //lblItemEngineering.Enabled = true;
                                if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text != "1")
                                {
                                    txtItemEngineering.Enabled = true;
                                }

                                break;
                            case ModuleState.Eyeable:
                                //lblItemEngineering.Visible = true;
                                txtItemEngineering.Visible = true;
                                //lblItemEngineering.Enabled = false;
                                txtItemEngineering.Enabled = false;
                                break;
                            default:
                                //lblItemEngineering.Visible = true;
                                txtItemEngineering.Visible = false;
                                OPlblTotalItemEngineering.Visible = false;
                                OPTZItemEngineering.InnerHtml = "&nbsp;";
                                OPQZItemEngineering.InnerHtml = "&nbsp;";
                                OPlblTotalItemEngineering1.Visible = false;
                                break;
                        }
                    }

                    //项目设计部




                    //Label lblItemDesign = ((Label)Repeater1.Items[i].FindControl("lblItemDesign"));
                    TextBox txtItemDesign = ((TextBox)Repeater1.Items[i].FindControl("txtItemDesign"));
                    Label lblItemDesign = (Label)Repeater1.Items[i].FindControl("lblItemDesign1");
                    if (lblItemDesign.Text.Trim() != "0")
                    {
                        switch (this.State6)
                        {
                            case ModuleState.Operable:
                                //lblItemDesign.Visible = true;
                                txtItemDesign.Visible = true;
                                //lblItemDesign.Enabled = true;
                                if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text != "1")
                                {
                                    txtItemDesign.Enabled = true;
                                }

                                break;
                            case ModuleState.Eyeable:
                                //lblItemDesign.Visible = true;
                                txtItemDesign.Visible = true;
                                //lblItemDesign.Enabled = false;
                                txtItemDesign.Enabled = false;
                                break;
                            default:
                                //lblItemDesign.Visible = true;
                                txtItemDesign.Visible = false;
                                OPlblTotalItemDesign.Visible = false;
                                OPTZItemDesign.InnerHtml = "&nbsp;";
                                OPQZItemDesign.InnerHtml = "&nbsp;";
                                OPlblTotalItemDesign1.Visible = false;
                                break;
                        }
                    }

                    //客服部


                    //Label lblClientService = ((Label)Repeater1.Items[i].FindControl("lblClientService"));
                    TextBox txtClientService = ((TextBox)Repeater1.Items[i].FindControl("txtClientService"));
                    Label lblClientService = (Label)Repeater1.Items[i].FindControl("lblClientService1");
                    if (lblClientService.Text.Trim() != "0")
                    {
                        switch (this.State7)
                        {
                            case ModuleState.Operable:
                                //lblClientService.Visible = true;
                                txtClientService.Visible = true;
                                //lblClientService.Enabled = true;
                                if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text != "1")
                                {
                                    txtClientService.Enabled = true;
                                }

                                break;
                            case ModuleState.Eyeable:
                                //lblClientService.Visible = true;
                                txtClientService.Visible = true;
                                //lblClientService.Enabled = false;
                                txtClientService.Enabled = false;
                                break;
                            default:
                                //lblClientService.Visible = true;
                                txtClientService.Visible = false;
                                OPlblTotalClientService.Visible = false;
                                OPTZClientService.InnerHtml = "&nbsp;";
                                OPQZClientService.InnerHtml = "&nbsp;";
                                OPlblTotalClientService1.Visible = false;
                                break;
                        }
                    }
                }

            }
        }
        //保存属性

        SaveOperationProperty("部门总分", lastTotalPoint);
    }
    public string SubmitGradeMessage()//提交主信息

    {
        using (StandardEntityDAO dao = new StandardEntityDAO("GradeMessage"))
        {
            try
            {
                if (this.ApplicationCode != "")
                {
                    this.GradeMessageCode = this.ApplicationCode;
                }
                else if (this.GradeMessageCode != "")
                {
                    this.ApplicationCode = this.GradeMessageCode;
                }
                string msg = "";
                if (this.txtSupplierCode.Value == "")
                {
                    msg += "请填写供应商 ！";
                }

                if (this.SupplierManagerModi.Value == "")
                {
                    msg += "请填写承包商项目经理！";
                }
               


                int TempRow = 0;
                decimal PersentageValidate = 0;
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text == "1")
                    {
                        TempRow++;
                        continue;
                    }
                    if (((Label)Repeater1.Items[i].FindControl("LabelFlag")).Text == "1")
                    {
                        TempRow++;

                        PersentageValidate += System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim());

                    }
                }

                    if (System.Convert.ToInt32(PersentageValidate) != 100)
                    {
                        msg += "您的权重相加为 " + (PersentageValidate) + "% 不是100%";
                        
                    }
               

                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                    return msg;
                }


                dao.BeginTrans();

                GradeMessage gm = new GradeMessage();
                if (this.State == ModuleState.Operable)
                {
                    if (this.GradeMessageCode != "")
                    {
                        gm.dao = dao;
                        gm.GradeMessageCode = this.GradeMessageCode;
                        gm.SupplierCode = this.txtSupplierCode.Value;
                        gm.ProjectManage = this.SupplierManagerModi.Value; 
                        gm.GradeMessageUpdate();
                    }
                    else
                    {
                        gm.dao = dao;
                        gm.GradeMessageCode = "";
                        gm.MainDefineCode = "100001";
                        gm.ProjectCode = this.ProjectCode;
                        gm.SupplierCode = this.txtSupplierCode.Value;
                        gm.ProjectManage = this.SupplierManagerModi.Value;
                        gm.CreateDate = System.DateTime.Now.ToString();
                        gm.State = "1";
                        gm.GradeMessageSubmit();
                        this.GradeMessageCode = gm.GradeMessageCode;
                        this.SupplierCode = gm.SupplierCode;

                    }

                    



                    Grade gv = new Grade();
                    gv.GradeMessageCode = this.GradeMessageCode;
                    DataTable Gradedt = gv.GetGrades();

                    RmsPM.BLL.GradeConsiderPercentage cgradeConsiderPercentage = new GradeConsiderPercentage();
                    cgradeConsiderPercentage.dao = dao;
                    DataTable dtgradeConsiderPercentage = cgradeConsiderPercentage.GetLastConsiderPercentage(this.GradeMessageCode, "100001");
                    decimal Persentage = 0;
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text == "1")
                        {
                            //增加小计子项权重
                            if (dtgradeConsiderPercentage != null && dtgradeConsiderPercentage.Rows.Count != 0)
                            {
                                if (dtgradeConsiderPercentage.Select("MainDefineCode='100001' and ConsiderDiathesisCode='" + ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text + "'").Length != 0)
                                    cgradeConsiderPercentage.ConsiderPercentageCode = dtgradeConsiderPercentage.Select("MainDefineCode='100001' and ConsiderDiathesisCode='" + ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text + "'")[0]["ConsiderPercentageCode"].ToString();
                                if (this.StatePersentage == ModuleState.Operable)
                                {
                                    cgradeConsiderPercentage.dao = dao;
                                    cgradeConsiderPercentage.Percentage = Persentage / 100;
                                    cgradeConsiderPercentage.GradeConsiderPercentageAdd();
                                }
                            }
                            else
                            {
                                if (this.StatePersentage == ModuleState.Operable)
                                {
                                    cgradeConsiderPercentage.dao = dao;
                                    cgradeConsiderPercentage.ConsiderPercentageCode = "";
                                    cgradeConsiderPercentage.GradeMessageCode = this.GradeMessageCode;
                                    cgradeConsiderPercentage.MainDefineCode = "100001";
                                    cgradeConsiderPercentage.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                                    cgradeConsiderPercentage.Percentage = Persentage / 100;
                                    cgradeConsiderPercentage.GradeConsiderPercentageAdd();
                                }
                            }
                            Persentage = 0;
                            continue;
                        }
                        if (((Label)Repeater1.Items[i].FindControl("LabelFlag")).Text == "1")
                        {
                            //增加子项权重
                            if (dtgradeConsiderPercentage != null && dtgradeConsiderPercentage.Rows.Count != 0)
                            {
                                cgradeConsiderPercentage.ConsiderPercentageCode = dtgradeConsiderPercentage.Select("MainDefineCode='100001' and ConsiderDiathesisCode='" + ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text + "'")[0]["ConsiderPercentageCode"].ToString();
                                if (this.StatePersentage == ModuleState.Operable)
                                {
                                    cgradeConsiderPercentage.dao = dao;
                                    cgradeConsiderPercentage.Percentage = System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim()) / 100;
                                    cgradeConsiderPercentage.GradeConsiderPercentageAdd();
                                    Persentage += System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim());
                                }

                            }
                            else
                            {
                                if (this.StatePersentage == ModuleState.Operable)
                                {
                                    cgradeConsiderPercentage.dao = dao;
                                    cgradeConsiderPercentage.ConsiderPercentageCode = "";
                                    cgradeConsiderPercentage.GradeMessageCode = this.GradeMessageCode;
                                    cgradeConsiderPercentage.MainDefineCode = "100001";
                                    cgradeConsiderPercentage.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                                    cgradeConsiderPercentage.Percentage = System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim()) / 100;
                                    cgradeConsiderPercentage.GradeConsiderPercentageAdd();
                                    Persentage += System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim());
                                }
                            }
                        }
                    }

                    //增加部门权重

                    RmsPM.BLL.GradeDepartment gradeDepartment = new GradeDepartment();
                    gradeDepartment.dao = dao;
                    DataTable Dtdepartment = gradeDepartment.GetGradeDepartments();

                    RmsPM.BLL.GradeDepartmentPercentage cgradeDepartmentPercentage = new GradeDepartmentPercentage();
                    cgradeDepartmentPercentage.dao = dao;
                    DataTable dtgradeDepartmentPercentage = cgradeDepartmentPercentage.GetLastDepartmentPercentage(this.GradeMessageCode, "100001");

                    RmsPM.BLL.GradeConsiderDepartment cgradeConsiderDepartment = new GradeConsiderDepartment();
                    cgradeConsiderDepartment.dao = dao;

                    if (Dtdepartment != null)
                    {
                        foreach (DataRow drAgreementTZ in Dtdepartment.Select("MainDefineCode='100001'"))
                        {
                            if (dtgradeDepartmentPercentage != null && dtgradeDepartmentPercentage.Rows.Count != 0)
                            {
                             
                                if (dtgradeDepartmentPercentage.Select("MainDefineCode='100001' and DepartmentDefineCode='" + System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]) + "'").Length != 0)
                                    cgradeDepartmentPercentage.DepartmentPercentageCode = dtgradeDepartmentPercentage.Select("MainDefineCode='100001' and DepartmentDefineCode='" + System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]) + "'")[0]["DepartmentPercentageCode"].ToString();


                                if (this.StatePersentage == ModuleState.Operable)
                                {
                                    cgradeDepartmentPercentage.dao = dao;
                                    cgradeDepartmentPercentage.AdjustCoefficient = System.Convert.ToDecimal(cgradeConsiderDepartment.GetAdjustCoefficient(System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]), this.GradeMessageCode));
                                    cgradeDepartmentPercentage.Percentage = System.Convert.ToDecimal(drAgreementTZ["Percentage"]);
                                    cgradeDepartmentPercentage.GradeDepartmentPercentageAdd();
                                }
                            }
                            else
                            {
                               
                                cgradeDepartmentPercentage.dao = dao;
                                cgradeDepartmentPercentage.DepartmentPercentageCode = "";
                                cgradeDepartmentPercentage.GradeMessageCode = this.GradeMessageCode;
                                cgradeDepartmentPercentage.MainDefineCode = "100001";
                                cgradeDepartmentPercentage.AdjustCoefficient = System.Convert.ToDecimal(cgradeConsiderDepartment.GetAdjustCoefficient(System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]), this.GradeMessageCode));
                                cgradeDepartmentPercentage.Percentage = System.Convert.ToDecimal(drAgreementTZ["Percentage"]);
                                cgradeDepartmentPercentage.DepartmentDefineCode = System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]);
                                if (this.StatePersentage == ModuleState.Operable)
                                {
                                    cgradeDepartmentPercentage.GradeDepartmentPercentageAdd();
                                }
                            }
                        }
                    }
                }
                dao.CommitTrans();
               
                return "";
            }
            catch (Exception ex)
            {
                dao.RollBackTrans();
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
                throw ex;
                return ex.Message;
            }
        }
    }

    public string SubmitGradeData()
    {
        using (StandardEntityDAO dao = new StandardEntityDAO("GradeMessage"))
        {
            try
            {
                if (this.ProjectCode == "")
                {
                    this.ProjectCode = this.DropDownProject.SelectedValue;
                }
                if (this.ApplicationCode != "")
                {
                    this.GradeMessageCode = this.ApplicationCode;
                }
                else if (this.GradeMessageCode != "")
                {
                    this.ApplicationCode = this.GradeMessageCode;
                }
                //string GradeMessageCode = Request["GradeMessageCode"] + "";
                //string act = Request["act"] + "";
                string msg = "";
                if (this.txtSupplierCode.Value == "")
                {
                    msg += "请填写供应商 ！";
                }

                if (this.SupplierManagerModi.Value == "")
                {
                    msg += "请填写承包商项目经理！";
                }
                if (msg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, msg));
                    return msg;
                }
                dao.BeginTrans();

                try
                {
                    int TempRow = 0;
                    decimal PersentageValidate = 0;
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text == "1")
                        {
                            TempRow++;
                            continue;
                        }
                        if (((Label)Repeater1.Items[i].FindControl("LabelFlag")).Text == "1")
                        {
                            TempRow++;

                            PersentageValidate += System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim());

                            if (this.State1 == ModuleState.Operable)
                            {

                                int temprowvalue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtAgreement")).Text.Trim());
                                if (temprowvalue < 0 || temprowvalue > 10)
                                {
                                    msg += "第" + System.Convert.ToString(TempRow) + "行 数据不符合要求，请修改!";
                                    return msg;
                                }

                            }
                        }
                        if (this.State2 == ModuleState.Operable)
                        {

                            int temprowvalue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtTechnic")).Text.Trim());
                            if (temprowvalue < 0 || temprowvalue > 10)
                            {
                                msg += "第" + System.Convert.ToString(TempRow) + "行 数据不符合要求，请修改!";
                                return msg;
                            }

                        }
                        if (this.State3 == ModuleState.Operable)
                        {

                            int temprowvalue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemMajordomo")).Text.Trim());
                            if (temprowvalue < 0 || temprowvalue > 10)
                            {
                                msg += "第" + System.Convert.ToString(TempRow) + "行 数据不符合要求，请修改!";
                                return msg;
                            }

                        }
                        if (this.State4 == ModuleState.Operable)
                        {
                            int temprowvalue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemAgreement")).Text.Trim());
                            if (temprowvalue < 0 || temprowvalue > 10)
                            {
                                msg += "第" + System.Convert.ToString(TempRow) + "行 数据不符合要求，请修改!";
                                return msg;
                            }
                        }
                        if (this.State5 == ModuleState.Operable)
                        {
                            int temprowvalue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemEngineering")).Text.Trim());
                            if (temprowvalue < 0 || temprowvalue > 10)
                            {
                                msg += "第" + System.Convert.ToString(TempRow) + "行 数据不符合要求，请修改!";
                                return msg;
                            }
                        }
                        if (this.State6 == ModuleState.Operable)
                        {
                            int temprowvalue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemDesign")).Text.Trim());
                            if (temprowvalue < 0 || temprowvalue > 10)
                            {
                                msg += "第" + System.Convert.ToString(TempRow) + "行 数据不符合要求，请修改!";
                                return msg;
                            }
                        }
                        if (this.State7 == ModuleState.Operable)
                        {
                            int temprowvalue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtClientService")).Text.Trim());
                            if (temprowvalue < 0 || temprowvalue > 10)
                            {
                                msg += "第" + System.Convert.ToString(TempRow) + "行 数据不符合要求，请修改!";
                                return msg;
                            }
                        }
                    }

                    if (System.Convert.ToInt32(PersentageValidate) != 100)
                    {
                        msg += "您的权重相加为" + (PersentageValidate) + "% 不是100%";
                        return msg;
                    }
                }
                catch (System.Exception ece)
                {
                    System.Exception tempec = new Exception("输入格式不正确");
                    throw tempec;
                }




                GradeMessage gm = new GradeMessage();
                if (this.State == ModuleState.Operable)
                {
                    if (this.GradeMessageCode != "")
                    {
                        gm.GradeMessageCode = this.GradeMessageCode;
                        gm.SupplierCode = this.txtSupplierCode.Value;
                        gm.ProjectManage = this.SupplierManagerModi.Value;
                        gm.GradeMessageUpdate();
                    }
                    else
                    {
                        gm.GradeMessageCode = "";
                        gm.MainDefineCode = "100001";
                        gm.ProjectCode = this.ProjectCode;
                        gm.SupplierCode = this.txtSupplierCode.Value;
                        gm.ProjectManage = this.SupplierManagerModi.Value;
                        gm.State = "1";
                        gm.GradeMessageSubmit();
                        this.GradeMessageCode = gm.GradeMessageCode;
                        this.ApplicationTitle = txtSupplierName.Value + "评分表";
                        this.UnitCode = "";
                        this.ApplicationType = "";
                        this.ApplicationCode = this.GradeMessageCode;
                    }
                }





                




                Grade gv = new Grade();
                gv.GradeMessageCode = this.GradeMessageCode;
                DataTable Gradedt = gv.GetGrades();

                RmsPM.BLL.GradeConsiderPercentage cgradeConsiderPercentage = new GradeConsiderPercentage();
                DataTable dtgradeConsiderPercentage = this.GetLastConsiderPercentage(this.GradeMessageCode, "100001");
                decimal Persentage = 0;
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    if (((Label)Repeater1.Items[i].FindControl("Labelsubtotal")).Text == "1")
                    {
                        //增加小计子项权重
                        if (dtgradeConsiderPercentage != null && dtgradeConsiderPercentage.Rows.Count != 0)
                        {
                            if (dtgradeConsiderPercentage.Select("MainDefineCode='100001' and ConsiderDiathesisCode='" + ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text + "'").Length != 0)
                                cgradeConsiderPercentage.ConsiderPercentageCode = dtgradeConsiderPercentage.Select("MainDefineCode='100001' and ConsiderDiathesisCode='" + ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text + "'")[0]["ConsiderPercentageCode"].ToString();
                            if (this.StatePersentage == ModuleState.Operable)
                            {
                                cgradeConsiderPercentage.Percentage = Persentage / 100;
                                cgradeConsiderPercentage.GradeConsiderPercentageAdd();
                            }
                        }
                        else
                        {
                            if (this.StatePersentage == ModuleState.Operable)
                            {
                                cgradeConsiderPercentage.ConsiderPercentageCode = "";
                                cgradeConsiderPercentage.GradeMessageCode = this.GradeMessageCode;
                                cgradeConsiderPercentage.MainDefineCode = "100001";
                                cgradeConsiderPercentage.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                                cgradeConsiderPercentage.Percentage = Persentage / 100;
                                cgradeConsiderPercentage.GradeConsiderPercentageAdd();
                            }
                        }
                        Persentage = 0;
                        continue;
                    }
                    if (((Label)Repeater1.Items[i].FindControl("LabelFlag")).Text == "1")
                    {
                        //增加子项权重
                        if (dtgradeConsiderPercentage != null && dtgradeConsiderPercentage.Rows.Count != 0)
                        {
                            cgradeConsiderPercentage.ConsiderPercentageCode = dtgradeConsiderPercentage.Select("MainDefineCode='100001' and ConsiderDiathesisCode='" + ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text + "'")[0]["ConsiderPercentageCode"].ToString();
                            if (this.StatePersentage == ModuleState.Operable)
                            {
                                cgradeConsiderPercentage.Percentage = System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim()) / 100;
                                cgradeConsiderPercentage.GradeConsiderPercentageAdd();
                                Persentage += System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim());
                            }

                        }
                        else
                        {
                            if (this.StatePersentage == ModuleState.Operable)
                            {
                                cgradeConsiderPercentage.ConsiderPercentageCode = "";
                                cgradeConsiderPercentage.GradeMessageCode = this.GradeMessageCode;
                                cgradeConsiderPercentage.MainDefineCode = "100001";
                                cgradeConsiderPercentage.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                                cgradeConsiderPercentage.Percentage = System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim()) / 100;
                                cgradeConsiderPercentage.GradeConsiderPercentageAdd();
                                Persentage += System.Convert.ToDecimal(((TextBox)Repeater1.Items[i].FindControl("TxtPercentage")).Text.Trim());
                            }
                        }
                        if (Gradedt != null && Gradedt.Rows.Count != 0)
                        {
                            if (this.State1 == ModuleState.Operable)
                            {
                                gv.GradeCode = ((Label)Repeater1.Items[i].FindControl("lblAgreement")).Text.Trim();
                                gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtAgreement")).Text.Trim());
                                if (gv.GradeCode != "0")
                                    gv.GradeUpdate();
                            }
                            if (this.State2 == ModuleState.Operable)
                            {
                                gv.GradeCode = ((Label)Repeater1.Items[i].FindControl("lblTechnic")).Text.Trim();
                                gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtTechnic")).Text.Trim());
                                if (gv.GradeCode != "0")
                                    gv.GradeUpdate();
                            }
                            if (this.State3 == ModuleState.Operable)
                            {
                                gv.GradeCode = ((Label)Repeater1.Items[i].FindControl("lblItemMajordomo")).Text.Trim();
                                gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemMajordomo")).Text.Trim());
                                if (gv.GradeCode != "0")
                                    gv.GradeUpdate();
                            }
                            if (this.State4 == ModuleState.Operable)
                            {
                                gv.GradeCode = ((Label)Repeater1.Items[i].FindControl("lblItemAgreement")).Text.Trim();
                                gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemAgreement")).Text.Trim());
                                if (gv.GradeCode != "0")
                                    gv.GradeUpdate();
                            }
                            if (this.State5 == ModuleState.Operable)
                            {
                                gv.GradeCode = ((Label)Repeater1.Items[i].FindControl("lblItemEngineering")).Text.Trim();
                                gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemEngineering")).Text.Trim());
                                if (gv.GradeCode != "0")
                                    gv.GradeUpdate();
                            }
                            if (this.State6 == ModuleState.Operable)
                            {
                                gv.GradeCode = ((Label)Repeater1.Items[i].FindControl("lblItemDesign")).Text.Trim();
                                gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemDesign")).Text.Trim());
                                if (gv.GradeCode != "0")
                                    gv.GradeUpdate();
                            }
                            if (this.State7 == ModuleState.Operable)
                            {
                                gv.GradeCode = ((Label)Repeater1.Items[i].FindControl("lblClientService")).Text.Trim();
                                gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtClientService")).Text.Trim());
                                if (gv.GradeCode != "0")
                                    gv.GradeUpdate();
                            }
                        }
                        else
                        {

                            //gv.GradeMessageCode = gm.GradeMessageCode;
                            gv.GradeCode = "";
                            gv.DepartmentDefineCode = "100001";
                            gv.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                            gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtAgreement")).Text.Trim());
                            gv.GradeSubmit();

                            //gv.GradeMessageCode = gm.GradeMessageCode;
                            gv.GradeCode = "";
                            gv.DepartmentDefineCode = "100002";
                            gv.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                            gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtTechnic")).Text.Trim());
                            gv.GradeSubmit();

                            //gv.GradeMessageCode = gm.GradeMessageCode;
                            gv.GradeCode = "";
                            gv.DepartmentDefineCode = "100003";
                            gv.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                            gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemMajordomo")).Text.Trim());
                            gv.GradeSubmit();

                            //gv.GradeMessageCode = gm.GradeMessageCode;
                            gv.GradeCode = "";
                            gv.DepartmentDefineCode = "100004";
                            gv.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                            gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemAgreement")).Text.Trim());
                            gv.GradeSubmit();

                            //gv.GradeMessageCode = gm.GradeMessageCode;
                            gv.GradeCode = "";
                            gv.DepartmentDefineCode = "100005";
                            gv.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                            gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemEngineering")).Text.Trim());
                            gv.GradeSubmit();

                            //gv.GradeMessageCode = gm.GradeMessageCode;
                            gv.GradeCode = "";
                            gv.DepartmentDefineCode = "100006";
                            gv.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                            gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtItemDesign")).Text.Trim());
                            gv.GradeSubmit();

                            //gv.GradeMessageCode = gm.GradeMessageCode;
                            gv.GradeCode = "";
                            gv.DepartmentDefineCode = "100007";
                            gv.ConsiderDiathesisCode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;
                            gv.GradeValue = System.Convert.ToInt32(((TextBox)Repeater1.Items[i].FindControl("txtClientService")).Text.Trim());
                            gv.GradeSubmit();

                        }

                    }
                }

                //增加部门权重

                RmsPM.BLL.GradeDepartment gradeDepartment = new GradeDepartment();
                DataTable Dtdepartment = gradeDepartment.GetGradeDepartments();
                DataTable dtgradeDepartmentPercentage = this.GetLastDepartmentPercentage(this.GradeMessageCode, "100001");

                RmsPM.BLL.GradeConsiderDepartment cgradeConsiderDepartment = new GradeConsiderDepartment();


                if (Dtdepartment != null)
                {
                    foreach (DataRow drAgreementTZ in Dtdepartment.Select("MainDefineCode='100001'"))
                    {
                        if (dtgradeDepartmentPercentage != null && dtgradeDepartmentPercentage.Rows.Count != 0)
                        {
                            RmsPM.BLL.GradeDepartmentPercentage cgradeDepartmentPercentage = new GradeDepartmentPercentage();
                            if (dtgradeDepartmentPercentage.Select("MainDefineCode='100001' and DepartmentDefineCode='" + System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]) + "'").Length != 0)
                                cgradeDepartmentPercentage.DepartmentPercentageCode = dtgradeDepartmentPercentage.Select("MainDefineCode='100001' and DepartmentDefineCode='" + System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]) + "'")[0]["DepartmentPercentageCode"].ToString();


                            if (this.StatePersentage == ModuleState.Operable)
                            {
                                cgradeDepartmentPercentage.AdjustCoefficient = System.Convert.ToDecimal(cgradeConsiderDepartment.GetAdjustCoefficient(System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]),this.GradeMessageCode));
                                cgradeDepartmentPercentage.Percentage = System.Convert.ToDecimal(drAgreementTZ["Percentage"]);
                                cgradeDepartmentPercentage.GradeDepartmentPercentageAdd();
                            }
                        }
                        else
                        {
                            RmsPM.BLL.GradeDepartmentPercentage cgradeDepartmentPercentage = new GradeDepartmentPercentage();
                            cgradeDepartmentPercentage.DepartmentPercentageCode = "";
                            cgradeDepartmentPercentage.GradeMessageCode = this.GradeMessageCode;
                            cgradeDepartmentPercentage.MainDefineCode = "100001";
                            cgradeDepartmentPercentage.AdjustCoefficient = System.Convert.ToDecimal(cgradeConsiderDepartment.GetAdjustCoefficient(System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]), this.GradeMessageCode));
                            cgradeDepartmentPercentage.Percentage = System.Convert.ToDecimal(drAgreementTZ["Percentage"]);
                            cgradeDepartmentPercentage.DepartmentDefineCode = System.Convert.ToString(drAgreementTZ["DepartmentDefineCode"]);
                            if (this.StatePersentage == ModuleState.Operable)
                            {
                                cgradeDepartmentPercentage.GradeDepartmentPercentageAdd();
                            }
                        }
                    }
                }
                dao.CommitTrans();
                return "";
            }
            catch (Exception ex)
            {
                dao.RollBackTrans();
                Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));

                return ex.Message;
            }
        }
    }

    protected void DropDownProject_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        this.ProjectCode = this.DropDownProject.SelectedValue;
    }

    /// <summary>
    /// 返回考虑因素权重
    /// </summary>
    /// <param name="GradeMessageCode"></param>
    /// <returns></returns>
    public DataTable GetLastConsiderPercentage(string GradeMessageCode, string MainDefineCode)
    {
        return GetLastConsiderPercentage("", GradeMessageCode, MainDefineCode);
    }

    public DataTable GetLastConsiderPercentage(string supplierCode, string GradeMessageCode, string MainDefineCode)
    {
        DataTable dt = new DataTable();

        if (GradeMessageCode != "")
        {
            RmsPM.BLL.GradeConsiderPercentage cgradeConsiderPercentage = new GradeConsiderPercentage();
            cgradeConsiderPercentage.GradeMessageCode = "'" + GradeMessageCode + "'";
            cgradeConsiderPercentage.MainDefineCode = MainDefineCode;
            dt = cgradeConsiderPercentage.GetGradeConsiderPercentages();
        }
        else
        {
            RmsPM.BLL.GradeMessage cgradeMessage = new GradeMessage();
            cgradeMessage.SupplierCode = supplierCode;
            cgradeMessage.MainDefineCode = MainDefineCode;
            DataTable dtgradeMessage = new DataTable();
            dtgradeMessage = cgradeMessage.GetGradeMessages();
            string GradeMessageFilter = "";
            int GradeMessageLenth = 0;
            if (dtgradeMessage != null)
            {
                foreach (DataRow tempGradedr in dtgradeMessage.Select())
                {
                    if (GradeMessageLenth != dtgradeMessage.Rows.Count - 1)
                    {
                        GradeMessageFilter = GradeMessageFilter + "'" + tempGradedr["GradeMessageCode"].ToString() + "',";
                    }
                    else
                    {
                        GradeMessageFilter = GradeMessageFilter + "'" + tempGradedr["GradeMessageCode"].ToString() + "'";
                    }
                    GradeMessageLenth++;

                }
            }

            RmsPM.BLL.GradeConsiderPercentage cgradeConsiderPercentage = new GradeConsiderPercentage();
            cgradeConsiderPercentage.GradeMessageCode = GradeMessageFilter;
            cgradeConsiderPercentage.MainDefineCode = MainDefineCode;
            dt = cgradeConsiderPercentage.GetGradeConsiderPercentages();

        }

        return dt;
    }


    /// <summary>
    /// 返回部门权重
    /// </summary>
    /// <param name="GradeMessageCode"></param>
    /// <param name="MainDefineCode"></param>
    /// <returns></returns>
    public DataTable GetLastDepartmentPercentage(string GradeMessageCode, string MainDefineCode)
    {
        return GetLastDepartmentPercentage("", GradeMessageCode, MainDefineCode);
    }

    public DataTable GetLastDepartmentPercentage(string supplierCode, string GradeMessageCode, string MainDefineCode)
    {
        DataTable dt = new DataTable();

        if (GradeMessageCode != "")
        {
            RmsPM.BLL.GradeDepartmentPercentage cgradeDepartmentPercentage = new GradeDepartmentPercentage();
            cgradeDepartmentPercentage.GradeMessageCode = "'" + GradeMessageCode + "'";
            cgradeDepartmentPercentage.MainDefineCode = MainDefineCode;
            dt = cgradeDepartmentPercentage.GetGradeDepartmentPercentages();
        }
        else
        {
            RmsPM.BLL.GradeMessage cgradeMessage = new GradeMessage();
            cgradeMessage.SupplierCode = supplierCode;
            cgradeMessage.MainDefineCode = MainDefineCode;
            DataTable dtgradeMessage = new DataTable();
            dtgradeMessage = cgradeMessage.GetGradeMessages();
            string GradeMessageFilter = "";
            int GradeMessageLenth = 0;
            if (dtgradeMessage != null)
            {
                foreach (DataRow tempGradedr in dtgradeMessage.Select())
                {
                    if (GradeMessageLenth != dtgradeMessage.Rows.Count - 1)
                    {
                        GradeMessageFilter = GradeMessageFilter + "'" + tempGradedr["GradeMessageCode"].ToString() + "',";
                    }
                    else
                    {
                        GradeMessageFilter = GradeMessageFilter + "'" + tempGradedr["GradeMessageCode"].ToString() + "'";
                    }
                    GradeMessageLenth++;

                }
            }

            RmsPM.BLL.GradeDepartmentPercentage cgradeDepartmentPercentage = new GradeDepartmentPercentage();
            cgradeDepartmentPercentage.GradeMessageCode = GradeMessageFilter;
            cgradeDepartmentPercentage.MainDefineCode = MainDefineCode;
            dt = cgradeDepartmentPercentage.GetGradeDepartmentPercentages();

        }

        return dt;
    }
}
