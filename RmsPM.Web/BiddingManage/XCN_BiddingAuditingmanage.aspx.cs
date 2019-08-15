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
using RmsPM.Web.BiddingManage;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;
using RmsPM.BLL;

public partial class BiddingManage_XCN_BiddingAuditingmanage :BiddingWorkFlowBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            this.InitPage();
    }

    /// ****************************************************************************
    /// <summary>
    /// 初始化

    /// </summary>
    /// ****************************************************************************
    override protected void InitPage()
    {
        
       
        /**************************************************************************************/
        if (Request["ApplicationCode"] != null && Request["ApplicationCode"].ToString() != "")
        {
            this.btnSave.Visible = false;
            this.btnUpdate.Visible = true;
            this.BiddingAuditing1.ApplicationCode = Request["ApplicationCode"].ToString();
            this.BiddingAuditing1.State = ModuleState.Eyeable;
            this.BiddingAuditing1.State1 = ModuleState.Eyeable;
            this.BiddingAuditing1.State2 = ModuleState.Eyeable;
            this.BiddingAuditing1.State3 = ModuleState.Eyeable;
            this.BiddingAuditing1.State4 = ModuleState.Eyeable;
            this.BiddingAuditing1.State5 = ModuleState.Eyeable;
            if (Request["BiddingCode"] != null && Request["BiddingCode"].ToString() != "")
                this.BiddingAuditing1.BiddingCode = Request["BiddingCode"] + "";
            else
                this.BiddingAuditing1.BiddingCode = "";
            this.BiddingAuditing1.UserCode = user.UserCode;
            BiddingAuditing1.SetAgreementMessage = ModuleState.Sightless;
            BiddingAuditing1.SetProjectMessage = ModuleState.Sightless;
            BiddingAuditing1.SetAdviserMessage = ModuleState.Sightless;

        }
        else
        {
            this.btnSave.Visible = true;
            this.btnUpdate.Visible = false;
            this.BiddingAuditing1.ApplicationCode = "";
            this.BiddingAuditing1.State = ModuleState.Operable;
            this.BiddingAuditing1.State1 = ModuleState.Operable;
            this.BiddingAuditing1.State2 = ModuleState.Operable;
            this.BiddingAuditing1.State3 = ModuleState.Operable;
            this.BiddingAuditing1.State4 = ModuleState.Operable;
            this.BiddingAuditing1.State5 = ModuleState.Operable;
            if (Request["BiddingCode"] != null && Request["BiddingCode"].ToString() != "")
                this.BiddingAuditing1.BiddingCode = Request["BiddingCode"] + "";
            else
                this.BiddingAuditing1.BiddingCode = "";
            this.BiddingAuditing1.UserCode = user.UserCode;
            BiddingAuditing1.SetAgreementMessage = ModuleState.Operable;
            BiddingAuditing1.SetProjectMessage = ModuleState.Operable;
            BiddingAuditing1.SetAdviserMessage = ModuleState.Operable;
        }

        this.BiddingAuditing1.InitControl();

        this.BiddingTop1.BiddingCode = this.BiddingAuditing1.BiddingCode;
        this.BiddingTop1.ContractNember = BiddingSystem.GetContractNemberByBiddingCode(this.BiddingAuditing1.BiddingCode);
        this.BiddingTop1.InitControl();

        /**************************************************************************************
        this.LeavePass1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;//同意按钮组

        this.LeavePass1.State = this.WorkFlowToolbar1.GetModuleState("PassBtn");
        this.LeavePass1.InitControl();
        /**************************************************************************************/
     
        /**************************************************************************************/
        
        base.InitPage();
        
       

    }
    protected void btnUpdate_ServerClick(object sender, EventArgs e)
    {
        this.btnSave.Visible = true;
        this.btnUpdate.Visible = false;
        this.BiddingAuditing1.ApplicationCode = Request["ApplicationCode"].ToString();
        this.BiddingAuditing1.State = ModuleState.Operable;
        this.BiddingAuditing1.State1 = ModuleState.Operable;
        this.BiddingAuditing1.State2 = ModuleState.Operable;
        this.BiddingAuditing1.State3 = ModuleState.Operable;
        this.BiddingAuditing1.State4 = ModuleState.Operable;
        this.BiddingAuditing1.State5 = ModuleState.Operable;
        this.BiddingAuditing1.BiddingCode = Request["BiddingCode"] + "";
        this.BiddingAuditing1.UserCode = user.UserCode;
        BiddingAuditing1.SetAgreementMessage = ModuleState.Operable;
        BiddingAuditing1.SetProjectMessage = ModuleState.Operable;
        BiddingAuditing1.SetAdviserMessage = ModuleState.Operable;
        this.BiddingAuditing1.InitControl();

    }
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {

        using (StandardEntityDAO dao = new StandardEntityDAO("Leave"))
        {
            dao.BeginTrans();
            try
            {
                if (!this.BiddingAuditing1.SupplierSelectedFlag && this.BiddingAuditing1.State == ModuleState.Operable)
                {
                    this.RegisterStartupScript("", "<script>alert('请选择中标单位！');</script>");
                    return;
                }
                bool bl=DataSubmit(dao);
                dao.CommitTrans();
                if (bl)
                {
                    Response.Write(Rms.Web.JavaScript.ScriptStart);
                    Response.Write(Rms.Web.JavaScript.OpenerReload(false));
                    Response.Write(Rms.Web.JavaScript.WinClose(false));
                    Response.Write(Rms.Web.JavaScript.ScriptEnd);
                }
            }
            catch (Exception ex)
            {
                dao.RollBackTrans();
                throw ex;
            }
        }
        
    }

    /// ****************************************************************************
    /// <summary>
    /// 业务数据操作
    /// </summary>
    /// ****************************************************************************
    override protected Boolean DataSubmit(StandardEntityDAO dao)
    {

        

        //if(this.BiddingAuditing1.State == ModuleState.Operable)
        //{
        //this.BiddingAuditing1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
        this.BiddingAuditing1.dao = dao;
        this.BiddingAuditing1.SubmitData();
        
        //}
        return true;
    }	
}
