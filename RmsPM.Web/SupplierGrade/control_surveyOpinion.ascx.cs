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

public partial class SupplierGrade_control_surveyOpinion : System.Web.UI.UserControl
{

    /// <summary>
    /// 模块状态

    /// </summary>
    private ModuleState _State = ModuleState.Unbeknown;

    /// <summary>
    /// 业务流程代码
    /// </summary>
    private string _ApplicationCode = "";

    /// <summary>
    /// 业务数据代码
    /// </summary>
    private string _OperationCode = "";



    private string _SupplierCode = "";

    /// <summary>
    /// 项目代码
    /// </summary>
    private string _ProjectCode = "";


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
    /// 业务流程代码
    /// </summary>
    public string SupplierCode
    {
        get
        {
            if (_SupplierCode == "")
            {
                if (this.ViewState["_SupplierCode"] != null)
                    return this.ViewState["_SupplierCode"].ToString();
                return "";
            }
            return _SupplierCode;
        }
        set
        {
            _SupplierCode = value;
            this.ViewState["_SupplierCode"] = value;
        }
    }
    /// ****************************************************************************
    /// <summary>
    /// 组件初始化


    /// </summary>
    /// ****************************************************************************
    public void InitControl()
    {
        if (this.State == ModuleState.Sightless)//不可见的
        {
            this.Visible = false;
        }
        else if (this.State == ModuleState.Operable)//可操作的
        {
            LoadData(true);
            EyeableDiv.Visible = false;
            OperableDiv.Visible = true;
        }
        else if (this.State == ModuleState.Eyeable)//可见的

        {
            LoadData(false);
            OperableDiv.Visible = false;
            EyeableDiv.Visible = true;
        }
        else if (this.State == ModuleState.Begin)//可见的

        {
            LoadData(false);
            OperableDiv.Visible = false;
            EyeableDiv.Visible = true;
        }
        else if (this.State == ModuleState.End)//可见的

        {
            LoadData(false);
            OperableDiv.Visible = false;
            EyeableDiv.Visible = true;
        }
        else
        {
            this.Visible = false;
        }



    }


    /// <summary>
    /// 装载控件数据
    /// </summary>
    public void LoadData(bool flag)
    {
        try
        {
            if (this.ApplicationCode != "")
            {
                this.OperationCode = this.ApplicationCode;
            }
            else if (this.OperationCode != "")
            {
                this.ApplicationCode = this.OperationCode;
            }


           
            if (flag)
            {
                if (this.ApplicationCode != "")
                {
                    RmsPM.BLL.SupplierSurveyOpinion cSupplierSurveyOpinion = new RmsPM.BLL.SupplierSurveyOpinion();
                    cSupplierSurveyOpinion.SupplierSurveyOpinionCode = this.ApplicationCode;
                    DataTable dSupplierSurveyOpinion = cSupplierSurveyOpinion.GetSupplierSurveyOpinions();
                    if (dSupplierSurveyOpinion != null)
                    {
                        foreach (DataRow dr in dSupplierSurveyOpinion.Select())
                        {
                            DataTable dttype = RmsPM.BLL.SupplierSurveyOpinion.GetSupplierGradeType();
                            if (dttype != null)
                            {
                                if (dttype.Rows.Count != 0)
                                {
                                    this.selectGrade.DataSource = dttype;
                                    this.selectGrade.DataTextField = "TypeName";
                                    this.selectGrade.DataValueField = "TypeName";
                                    this.selectGrade.DataBind();

                                    
                                    
                                    this.selectAdviceGrade.DataSource = dttype;
                                    this.selectAdviceGrade.DataTextField = "TypeName";
                                    this.selectAdviceGrade.DataValueField = "TypeName";
                                    this.selectAdviceGrade.DataBind();

                                    for (int i = 0; i < dttype.Rows.Count; i++)
                                    {
                                        if (this.selectGrade.Items[i].Value == dr["Grade"].ToString())
                                        {
                                            this.selectGrade.SelectedIndex = i;
                                        }
                                        if (this.selectAdviceGrade.Items[i].Value == dr["AdviceGrade"].ToString())
                                        {
                                            this.selectAdviceGrade.SelectedIndex = i;
                                        }
                                    }
                                   
                                }

                            }

                       
                            this.txtState.Text = RmsPM.BLL.SupplierSurveyOpinion.GetSupplierSurveyOpinionStatusName(dr["State"].ToString());

                            this.inputSystemGroup.Value = dr["WorkName"].ToString();

                            this.TxtPersonName.Value = dr["ZYName"].ToString();
                            this.TxtCompanyName.Text = RmsPM.BLL.SupplierRule.GetSupplierName(dr["SupplierCode"].ToString());
                            this.Txtdate.Text = dr["SurveyDate"].ToString();
                            this.TxtRemark.Value = dr["Remark"].ToString();
                            //this.TxtGrade.Value = dr["Grade"].ToString();
                            //this.TxtAdviceGrade.Value = dr["AdviceGrade"].ToString();
                        }
                    }
                }
                else
                {
                    DataTable dttype= RmsPM.BLL.SupplierSurveyOpinion.GetSupplierGradeType();
                    if (dttype != null)
                    {
                        if (dttype.Rows.Count != 0)
                        {
                            this.selectGrade.DataSource = dttype;
                            this.selectGrade.DataTextField = "TypeName";
                            this.selectGrade.DataValueField = "TypeName";
                            this.selectGrade.DataBind();

                            this.selectAdviceGrade.DataSource = dttype;
                            this.selectAdviceGrade.DataTextField = "TypeName";
                            this.selectAdviceGrade.DataValueField = "TypeName";
                            this.selectAdviceGrade.DataBind();
                        }
                       
                    }


                    this.inputSystemGroup.ClassCode = "1401";
                 
                    this.TxtCompanyName.Text = RmsPM.BLL.SupplierRule.GetSupplierName(this.SupplierCode);
                    this.Txtdate.ReadOnly = false;
                }
            }
            else
            {
                RmsPM.BLL.SupplierSurveyOpinion cSupplierSurveyOpinion = new RmsPM.BLL.SupplierSurveyOpinion();
                cSupplierSurveyOpinion.SupplierSurveyOpinionCode = this.ApplicationCode;
                DataTable dSupplierSurveyOpinion = cSupplierSurveyOpinion.GetSupplierSurveyOpinions();
                if (dSupplierSurveyOpinion != null)
                {
                    foreach (DataRow dr in dSupplierSurveyOpinion.Select())
                    {
                        this.lblstate.Text = RmsPM.BLL.SupplierSurveyOpinion.GetSupplierSurveyOpinionStatusName(dr["State"].ToString());
                        this.SpanlblTaskname.InnerHtml = RmsPM.BLL.ProjectRule.GetSupplierTypeName(dr["WorkName"].ToString());
                        
                        this.lblPersonName.Text = dr["ZYName"].ToString();
                        this.lblCompanyname.Text = RmsPM.BLL.SupplierRule.GetSupplierName(dr["SupplierCode"].ToString());
                        this.lblDate.Text = dr["SurveyDate"].ToString();
                        this.lblRemark.InnerHtml = System.Convert.ToString(dr["Remark"]).Replace("\n", "<br>");
                        this.lblGrade.Text = dr["Grade"].ToString();
                        this.lblAdviceGrade.Text = dr["AdviceGrade"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
         
        }
    }


    /// <summary>
    /// 保存控件数据
    /// </summary>
    public string SubmitData()
    {
        try
        {
            string ErrMsg = "";

            RmsPM.BLL.SupplierSurveyOpinion cSupplierSurveyOpinion = new RmsPM.BLL.SupplierSurveyOpinion();

            cSupplierSurveyOpinion.SupplierSurveyOpinionCode = this.ApplicationCode;


            if (this.inputSystemGroup.Value.Trim() == "")
            {
                ErrMsg += "请选择工种名称 ！";
                return ErrMsg;
            }
            if (this.TxtPersonName.Value.Trim() == "")
            {
                ErrMsg += "请填写专员姓名 ！";
                return ErrMsg;
            }

            cSupplierSurveyOpinion.WorkName = this.inputSystemGroup.Value.Trim();
            cSupplierSurveyOpinion.ZYName = this.TxtPersonName.Value.Trim();
            cSupplierSurveyOpinion.SupplierCode = this.SupplierCode;
            cSupplierSurveyOpinion.SurveyDate = this.Txtdate.Value;
            cSupplierSurveyOpinion.Remark = this.TxtRemark.Value.Trim();

            cSupplierSurveyOpinion.Grade = this.selectGrade.Items[this.selectGrade.SelectedIndex].Text;
            cSupplierSurveyOpinion.AdviceGrade = this.selectAdviceGrade.Items[this.selectAdviceGrade.SelectedIndex].Text;
            cSupplierSurveyOpinion.State = "1";
            cSupplierSurveyOpinion.SupplierSurveyOpinionAdd();
            this.ApplicationCode = cSupplierSurveyOpinion.SupplierSurveyOpinionCode;



            return ErrMsg;
        }
        catch (Exception ex)
        {

            Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
            throw ex;
        }
    }

  

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
}
