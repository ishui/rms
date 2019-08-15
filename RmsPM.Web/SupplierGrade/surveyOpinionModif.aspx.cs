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
using RmsPM.Web;
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;

public partial class SupplierGrade_surveyOpinionModif : PageBase
{

    /// <summary>
    /// 业务流程代码
    /// </summary>
    private string _ApplicationCode = "";

    /// <summary>
    /// 项目代码
    /// </summary>
    private string _ProjectCode = "";


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
    /// 中标单位评审页面
    /// </summary>
    public string SurveyUrl
    {
        get
        {
            return RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("厂商调查意见审核");
        }
    }


    public string ApplicationCodetemp
    {
        get
        {
            return this.ucOperationControl.ApplicationCode;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (!this.user.HasRight("141703"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }
            this.LoadData();
        }
     
    }


    public void LoadData()
    {

       
        string SupplierCode = Request["suppliercode"] + "";
       
           this.ApplicationCode = Request["ApplicationCode"] + "";
        RmsPM.BLL.SupplierSurveyOpinion cSupplierSurveyOpinion=new RmsPM.BLL.SupplierSurveyOpinion();
        EntityData entity = cSupplierSurveyOpinion.GetSupplierSurveyOpinionByCode(ApplicationCode);

           

        this.ucOperationControl.SupplierCode = SupplierCode;
        if (this.ApplicationCode == "")
        {
            this.btnModify.Visible = false;
            this.btnDelete.Visible = false;
            this.btnOldCheck.Visible = false;
            this.btnCheck.Visible = false;
            this.btnSave.Visible = this.user.HasRight("141704");
            this.ucOperationControl.State = ModuleState.Operable;
        }
        else
        {
            this.ucOperationControl.ApplicationCode = this.ApplicationCode;
            
            this.btnSave.Visible = false;
            this.btnModify.Visible = this.user.HasRight("141702");
            this.btnDelete.Visible = this.user.HasRight("141701");
            this.btnOldCheck.Visible = this.user.HasRight("141705");
            this.btnCheck.Visible = this.user.HasRight("141706");
            this.ucOperationControl.State = ModuleState.Eyeable;
        }
        this.ucOperationControl.InitControl();

        this.WorkFlowList1.ProcedureNameAndApplicationCodeList = GetWorkFlowListString(ApplicationCode);
        this.WorkFlowList1.DataBound();


        this.btnCheck.Attributes["OnClick"] = "javascript:opendoSurvey();return false;";
        if (entity.HasRecord())
        {

            switch (entity.GetString("State"))
            {
                case "0"://已审
                    this.btnModify.Style["display"] = "none";
                    this.btnDelete.Style["display"] = "none";
                    this.btnCheck.Style["display"] = "none";
                    this.btnOldCheck.Style["display"] = "none";



                    break;
                case "7"://审核中

                    this.btnModify.Style["display"] = "none";
                    this.btnDelete.Style["display"] = "none";
                    this.btnCheck.Style["display"] = "none";
                    this.btnOldCheck.Style["display"] = "none";

                    break;

                default:
                    break;
            }
        }


    }


    private string GetWorkFlowListString(string ApplicationCode)
    {
        string ListString = "''";


        ListString += ",'厂商调查意见审核" + ApplicationCode + "'";




        return ListString;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {


            RmsPM.BLL.SupplierSurveyOpinion cSupplierSurveyOpinion = new RmsPM.BLL.SupplierSurveyOpinion();
            cSupplierSurveyOpinion.SupplierSurveyOpinionCode = this.ucOperationControl.ApplicationCode;




            EntityData entity = cSupplierSurveyOpinion.GetSupplierSurveyOpinionByCode(this.ucOperationControl.ApplicationCode);
            if (entity.HasRecord())
            {
                entity.DeleteAllTableRow("SupplierSurveyOpinion");
                RmsPM.BLL.SupplierSurveyOpinion.DeleteStandard_Grade(entity);
            }


            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
            //Response.End();
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "删除评分错误");
            Response.Write(Rms.Web.JavaScript.Alert(true, "删除评分出错：" + ex.Message));
        }
    }
    protected void btnModify_ServerClick(object sender, EventArgs e)
    {
        this.btnModify.Visible = false;
        this.btnCheck.Visible = false;
        this.btnSave.Visible = this.user.HasRight("141704");

      
        this.ucOperationControl.State = ModuleState.Operable;
       

        this.ucOperationControl.InitControl();
    }
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        string Errmsg = "";
        Errmsg = this.ucOperationControl.SubmitData();

        if (Errmsg == "")
        {

            this.btnSave.Visible = false;

            this.btnModify.Visible = this.user.HasRight("141702");
            this.btnDelete.Visible = this.user.HasRight("141701");
            this.btnOldCheck.Visible = this.user.HasRight("141705");
            this.btnCheck.Visible = this.user.HasRight("141706");
            this.ucOperationControl.State = ModuleState.Eyeable;

            this.ucOperationControl.InitControl();
            this.ApplicationCode = this.ucOperationControl.ApplicationCode;

            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);
        }
        else
        {
          
            Response.Write(Rms.Web.JavaScript.Alert(true,Errmsg));

        }
    }
    protected void btnOldCheck_ServerClick(object sender, EventArgs e)
    {
        
        RmsPM.BLL.SupplierSurveyOpinion.SupplierSurveyOpinionStatusChange(this.ucOperationControl.ApplicationCode,0);
        this.btnModify.Style["display"] = "none";
        this.btnDelete.Style["display"] = "none";
        this.btnCheck.Style["display"] = "none";
        this.btnOldCheck.Style["display"] = "none";
        this.ucOperationControl.InitControl();
        Response.Write(Rms.Web.JavaScript.ScriptStart);
        Response.Write(Rms.Web.JavaScript.OpenerReload(false));
        
        Response.Write(Rms.Web.JavaScript.ScriptEnd);
    }
   
}
