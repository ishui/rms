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
public partial class SupplierGrade_SupplierGradeInfo : WorkFlowPageBase
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
    public string GradeMessageGrade
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
            this.EntityName = "GradeMessage";
            this.WorkFlowName = "单位评分审核";// System.Configuration.ConfigurationSettings.AppSettings["PaymentAuditingName"].ToString();
            this.OpinionCount = 6;
            GradeMessageGrade = Request["gradeMessageCode"] + "";
            if (this.wftToolbar.ApplicationCode != "")
            {
                GradeMessageGrade = this.wftToolbar.ApplicationCode;
            }
            else if (GradeMessageGrade != "")
            {
                this.wftToolbar.ApplicationCode = GradeMessageGrade;
            }

            
            InitPage();
            this.wftToolbar.Visible = false;
        }

       this.ProjectCode = this.ucOperationControl.ProjectCode;
        
    }

    override protected void OperationControlInit()
    {
       

        base.OperationControlInit();
        GradeMessageGrade = Request["gradeMessageCode"] + "";
        Projectcode = Request["projectcode"] + "";
        SupplierCode = Request["suppliercode"] + "";
        

        GradeMessage gm = new GradeMessage();
        EntityData entity = gm.GetGradeMessageByCode(GradeMessageGrade);
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
                    //this.btnModify.Visible = true;
                    //this.btnDelete.Visible = true;
                    //this.btnCheck.Visible = true;
                    break;
               

            }
        }
        this.ucOperationControl.ApplicationCode = this.wftToolbar.ApplicationCode;
        this.ucOperationControl.SupplierCode = SupplierCode;
        this.ucOperationControl.GradeMessageCode = GradeMessageGrade;
        this.ucOperationControl.ProjectCode = Projectcode;
        
        this.ucOperationControl.State = ModuleState.Operable;
        this.ucOperationControl.StateProject = ModuleState.Operable;
        
        this.ucOperationControl.InitControl();


    }

    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        try
        {
            Grade grade = new Grade();
            grade.GradeMessageCode = this.ucOperationControl.GradeMessageCode;
            EntityData entityGrade=grade.GetGrade();
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
        this.btnModify.Visible = true;
        this.btnSave.Visible = false;
        this.btnCheck.Visible = true;
        //this.ucOperationControl.SubmitGradeMessage();
    }

    protected void btnModify_ServerClick(object sender, EventArgs e)
    {
        this.btnModify.Visible = false;
        this.btnSave.Visible = true;
       

        this.ucOperationControl.State = ModuleState.Eyeable;
        this.ucOperationControl.StateProject = ModuleState.Eyeable;

        this.ucOperationControl.InitControl();
    }
    
    
}