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
using RmsPM.Web;


public partial class SupplierGrade_SupplierGradeModif :PageBase
{

    private string _GradeMessageCode;
    private string _Projectcode;
    private string _SupplierCode;

    /// <summary>
    /// 厂商编号
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


 

    /// <summary>
    /// 项目编号
    /// </summary>
    public string Projectcode
    {
        get
        {
            if (_Projectcode == "")
            {
                if (this.ViewState["_Projectcode"] != null)
                    return this.ViewState["_Projectcode"].ToString();
                return "";
            }
            return _Projectcode;
        }
        set
        {
            _Projectcode = value;
            this.ViewState["_Projectcode"] = value;
        }
    }

    /// <summary>
    /// 业务代码
    /// </summary>
    public string GradeMessageCode
    {
        get
        {
            if (_GradeMessageCode == "")
            {
                if (this.ViewState["_GradeMessageCode"] != null)
                    return this.ViewState["_GradeMessageCode"].ToString();
                return "";
            }
            return _GradeMessageCode;
        }
        set
        {
            _GradeMessageCode = value;
            this.ViewState["_GradeMessageCode"] = value;
        }
    }

    /// <summary>
    /// /// 单位评分评审页面
    /// </summary>
    public string SupplierGradePage
    {
        get
        {
            return RmsPM.BLL.WorkFlowRule.GetProcedureURLByName("单位评分审核");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPage();
            if (!this.user.HasRight("2703") && !this.user.HasRight("2708") && !this.user.HasRight("2701"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }
            this.wftToolbar.Visible = false;
        }

        this.ProjectCode = this.ucOperationControl.ProjectCode;

    }


    protected void InitPage()
    {
        OperationControlInit();
    }

    protected void OperationControlInit()
    {
        GradeMessageCode = Request["gradeMessageCode"] + "";
        Projectcode = Request["projectcode"] + "";
        SupplierCode = Request["suppliercode"] + "";
        string ApplicationCode = Request["ApplicationCode"] + "";
        string viewstate = Request["view"] + "";
        
        
        GradeMessage gm = new GradeMessage();
        EntityData entity = gm.GetGradeMessageByCode(GradeMessageCode);
        if (entity.HasRecord())
        {

            int state = entity.GetInt("State");

            // 评分状态： 0: 正常； 1 待审核，当前评分； 
            //            3 申请不通过 ；  6 历史记录 ； 7 合同申请中

            //			  8 预审;9 预审中


            this.lblState.Text = RmsPM.BLL.GradeMessage.GetContractStatusName(state.ToString());
            switch (state)
            {
                case 0:
                    break;
                case 1:
                    

                    this.btnCheck.Visible = this.user.HasRight("2705");
                    this.btnModify.Visible = this.user.HasRight("2704");
                   
                    this.btnDelete.Visible = this.user.HasRight("2707");
                    break;


            }
            this.ucOperationControl.State = ModuleState.Eyeable;
            this.ucOperationControl.StateProject = ModuleState.Eyeable;
            this.ucOperationControl.StatePersentage = ModuleState.Eyeable;
            if (this.user.HasRight("2708"))
            {
                this.ucOperationControl.StateProject = ModuleState.Sightless;

                this.ucGradeOpinionControl.ApplicationCode = this.wftToolbar.ApplicationCode;
                this.ucGradeOpinionControl.SupplierCode = SupplierCode;
                this.ucGradeOpinionControl.GradeMessageCode = this.GradeMessageCode;
                this.ucGradeOpinionControl.ProjectCode = this.ProjectCode;
                this.ucGradeOpinionControl.State = ModuleState.Operable;
                this.ucGradeOpinionControl.StateProject = ModuleState.Operable;
                this.ucGradeOpinionControl.InitControl();
            }
            else
            {
                this.ucGradeOpinionControl.Visible = false;
            }
          
        }
        else
        { 
            this.ucGradeOpinionControl.Visible = false;

            this.btnSave.Visible = this.user.HasRight("2701");
            this.ucOperationControl.State = ModuleState.Operable;
            this.ucOperationControl.StateProject = ModuleState.Operable;
            this.ucOperationControl.StatePersentage = ModuleState.Operable;
        }
        this.ucOperationControl.ApplicationCode = this.wftToolbar.ApplicationCode;
        this.ucOperationControl.SupplierCode = SupplierCode;
        this.ucOperationControl.GradeMessageCode = GradeMessageCode;
        this.ucOperationControl.ProjectCode = Projectcode;

        if (this.user.HasRight("2703"))
        {
            this.ucOperationControl.InitControl();
        }
        else
        {
            if (viewstate == "add")   
                this.ucOperationControl.InitControl();
           
            else
                this.ucOperationControl.Visible = false;
        }
        

        this.WorkFlowList1.ProcedureNameAndApplicationCodeList = GetWorkFlowListString();
        this.WorkFlowList1.DataBound();

        

    }


    private string GetWorkFlowListString()
    {
        string ListString = "''";


        ListString += ",'单位评分审核" + this.GradeMessageCode + "'";




        return ListString;
    }

    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        try
        {
            Grade grade = new Grade();
            grade.GradeMessageCode = this.ucOperationControl.GradeMessageCode;
            EntityData entityGrade = grade.GetGrade();
            if (entityGrade.HasRecord())
            {
                entityGrade.DeleteAllTableRow("Grade");
                RmsPM.BLL.GradeMessage.DeleteStandard_Grade(entityGrade);
            }

            GradeMessage gm = new GradeMessage();
            EntityData entityGradeMessage = gm.GetGradeMessageByCode(this.ucOperationControl.GradeMessageCode);
            if (entityGradeMessage.HasRecord())
            {
                entityGradeMessage.DeleteAllTableRow("GradeMessage");
                RmsPM.BLL.GradeMessage.DeleteStandard_GradeMessage(entityGradeMessage);
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

    protected void btnSave_ServerClick(object sender, System.EventArgs e)
    {
      

        string Errmsg = "";
        Errmsg = this.ucOperationControl.SubmitGradeMessage();
        if (Errmsg == "")
        {

            this.btnModify.Visible = this.user.HasRight("2704");
            this.btnSave.Visible = false;
            this.btnCheck.Visible = this.user.HasRight("2705");
            this.btnDelete.Visible = this.user.HasRight("2707");
            this.GradeMessageCode = this.ucOperationControl.GradeMessageCode;
            this.SupplierCode = this.ucOperationControl.SupplierCode;

            Response.Write(Rms.Web.JavaScript.ScriptStart);
            Response.Write(Rms.Web.JavaScript.OpenerReload(false));
            Response.Write(Rms.Web.JavaScript.WinClose(false));
            Response.Write(Rms.Web.JavaScript.ScriptEnd);

            //this.ucOperationControl.State = ModuleState.Eyeable;
            //this.ucOperationControl.StateProject = ModuleState.Eyeable;
            //this.ucOperationControl.StatePersentage = ModuleState.Eyeable;


            //if (this.user.HasRight("2708"))
            //{
            //    this.ucOperationControl.StateProject = ModuleState.Sightless;
            //    if (this.ucGradeOpinionControl.GradeMessageCode != "")
            //        this.ucGradeOpinionControl.Visible = true;
            //}

            //this.ucOperationControl.InitControl();
        }
       
    }

    protected void btnModify_ServerClick(object sender, EventArgs e)
    {
        this.btnModify.Visible = false;
        this.btnCheck.Visible = false;
        this.btnSave.Visible = this.user.HasRight("2701");

        this.ucGradeOpinionControl.Visible = false;
        this.ucOperationControl.State = ModuleState.Operable;
        this.ucOperationControl.StateProject = ModuleState.Operable;
        this.ucOperationControl.StatePersentage = ModuleState.Operable;

        this.ucOperationControl.InitControl();
    }


}