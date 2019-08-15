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

public partial class WorkFlowOperation_sm_SupplierGradeOpinion : WorkFlowOperationBase
{
    private string _GradeCode;
    private string _GradeMessageCode;
    private string _ConsiderDiathesisCode;
    private string _DepartmentDefineCode;
    private int _GradeValue;
    private string _SupplierCode;
    private ModuleState _StateProject;//工程是否显示



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
        else
        {
            LoadData(true);
        }
    }
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
            //集团合约部总分
            TotalAgreementPoint = RmsPM.BLL.GradeList.GetGradePoint("100001", this.GradeMessageCode);

            ////集团技术部总分
            TotalTechnic = RmsPM.BLL.GradeList.GetGradePoint("100002", this.GradeMessageCode);

            //项目总监总分
            TotalItemMajordomo = RmsPM.BLL.GradeList.GetGradePoint("100003", this.GradeMessageCode);

            //项目合约部总分
            TotalItemAgreement = RmsPM.BLL.GradeList.GetGradePoint("100004", this.GradeMessageCode);

            //项目工程部总分
            TotalItemEngineering = RmsPM.BLL.GradeList.GetGradePoint("100005", this.GradeMessageCode);

            //项目设计部总分
            TotalItemDesign = RmsPM.BLL.GradeList.GetGradePoint("100006", this.GradeMessageCode);

            //客服部总分
            TotalClientService = RmsPM.BLL.GradeList.GetGradePoint("100007", this.GradeMessageCode);

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
            //调整系数 权重 汇总


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
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100001'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100001'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100001'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100001'")[0]["Percentage"];
                            }
                            this.QZAgreement.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalAgreementPoint = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalAgreementPoint) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalAgreementPoint == "")
                            {
                                lastTotalAgreementPoint = "0";
                            }
                            this.lblTotalAgreement1.Text = lastTotalAgreementPoint;

                            break;
                        case "100002":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100002'")[0]["Percentage"];
                            }
                            this.QZTechnic.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalTechnic = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalTechnic) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalTechnic == "")
                            {
                                lastTotalTechnic = "0";
                            }
                            this.lblTotalTechnic1.Text = lastTotalTechnic;

                            break;
                        case "100003":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100003'")[0]["Percentage"];
                            }
                            this.QZItemMajordomo.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalItemMajordomo = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalItemMajordomo) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalItemMajordomo == "")
                            {
                                lastTotalItemMajordomo = "0";
                            }
                            this.lblTotalItemMajordomo1.Text = lastTotalItemMajordomo;

                            break;
                        case "100004":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100004'")[0]["Percentage"];
                            }
                            this.QZItemAgreement.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalItemAgreement = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalItemAgreement) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalItemAgreement == "")
                            {
                                lastTotalItemAgreement = "0";
                            }
                            this.lblTotalItemAgreement1.Text = lastTotalItemAgreement;

                            break;
                        case "100005":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100005'")[0]["Percentage"];
                            }
                            this.QZItemEngineering.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalItemEngineering = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalItemEngineering) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalItemEngineering == "")
                            {
                                lastTotalItemEngineering = "0";
                            }
                            this.lblTotalItemEngineering1.Text = lastTotalItemEngineering;

                            break;
                            break;
                        case "100006":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100006'")[0]["Percentage"];
                            }
                            this.QZItemDesign.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalItemDesign = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalItemDesign) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalItemDesign == "")
                            {
                                lastTotalItemDesign = "0";
                            }
                            this.lblTotalItemDesign1.Text = lastTotalItemDesign;

                            break;
                        case "100007":
                            if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'").Length != 0)
                            {
                                if (dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'")[0]["AdjustCoefficient"] != null && System.Convert.ToString(dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'")[0]["AdjustCoefficient"]) != "")
                                    drAgreementTZ["AdjustCoefficient"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'")[0]["AdjustCoefficient"];
                                drAgreementTZ["Percentage"] = (decimal)dtgradeDepartmentPercentage.Select("DepartmentDefineCode='100007'")[0]["Percentage"];
                            }
                            this.QZClientService.InnerHtml = "*" + RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(drAgreementTZ["Percentage"]) * 100) + "%";
                            lastTotalClientService = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(TotalClientService) * (decimal)drAgreementTZ["AdjustCoefficient"] * (decimal)drAgreementTZ["Percentage"]);
                            if (lastTotalClientService == "")
                            {
                                lastTotalClientService = "0";
                            }
                            this.lblTotalClientService1.Text = lastTotalClientService;

                            break;
                    }
                }
                lastTotalPoint = RmsPM.BLL.MathRule.GetDecimalNoPointShowString(System.Convert.ToDecimal(lastTotalAgreementPoint) + System.Convert.ToDecimal(lastTotalTechnic) + System.Convert.ToDecimal(lastTotalItemMajordomo) + System.Convert.ToDecimal(lastTotalItemAgreement) + System.Convert.ToDecimal(lastTotalItemEngineering) + System.Convert.ToDecimal(lastTotalItemDesign) + System.Convert.ToDecimal(lastTotalClientService));
                if (lastTotalPoint == "")
                {
                    lastTotalPoint = "0";
                }
                lblpoint.Text = lastTotalPoint;//综合得分

                GradeMessage gm = new GradeMessage();
                gm.GradeMessageCode = this.GradeMessageCode;
                gm.GetGradeMessages();
                this.ProjectCode = gm.ProjectCode;
                this.SupplierCode = gm.SupplierCode;

                this.SupplierManagerList.Text = gm.ProjectManage;

            }

            this.lblProjectNameModi.Text = ProjectRule.GetProjectName(this.ProjectCode);


            GradeConsiderDiathesis gcd = new GradeConsiderDiathesis();
            gcd.MainDefineCode = "100001";
            DataTable dt = gcd.GetGradeConsiderDiathesiss();
            DataTable returndt = gcd.GetGradeConsiderDiathesiss();
            //Grade gv = new Grade();
            //gv.GradeMessageCode = this.GradeMessageCode;
            //DataTable Gradedt = gv.GetGrades();

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


            RmsPM.BLL.ConvertRule.GetSHTreeDataSource(dt, returndt, GradeMessageCode, "ConsiderDiathesisCode", "ParentCode", "", "", "", 1, 0, "");
            //RmsPM.Web.temp.GetSHTreeDataSource(dt, returndt, GradeMessageCode, "ConsiderDiathesisCode", "ParentCode", "", "", "", 1, 0, "");
            if (Flag)
            {
                this.SupplierNameList.Text = ProjectRule.GetSupplierName(this.SupplierCode);
                //this.messagelist.Visible = 'false;
                // this.messageMoid.Visible = true;
                // this.GradeList.Visible = false;
                // this.GradeModify.Visible = true;

                this.Repeater2.DataSource = returndt;
                this.Repeater2.DataBind();
            }

        }
        switch (this.StateProject)//项目
        {
            case ModuleState.Operable:
                break;
            case ModuleState.Eyeable:
                break;
            case ModuleState.Begin:
                break;
            case ModuleState.End:
                break;
            default:
                this.messageMoid.Visible = false;
                break;
        }



    }


    
}
