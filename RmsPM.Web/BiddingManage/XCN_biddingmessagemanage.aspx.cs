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

public partial class BiddingManage_XCN_biddingmessagemanage : BiddingWorkFlowBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.InitPage();
        }
        BiddingMessageModify1.LoadAttach();
    }

    /// ****************************************************************************
    /// /// <summary>
    /// 初始化

    /// </summary>
    /// ****************************************************************************		

    override protected void InitPage()
    {
        if ( Request["ApplicationCode"] != null && Request["ApplicationCode"].ToString() != "")
        {
            this.btnSave.Visible = false;
            this.btnUpdate.Visible = true;
            this.BiddingMessageModify1.ApplicationCode = Request["ApplicationCode"].ToString();
            this.BiddingMessageModify1.State = ModuleState.Eyeable;
            this.BiddingMessageModify1.MoneyState = ModuleState.Eyeable;
            if (Request["BiddingCode"] != null && Request["BiddingCode"].ToString() != "")
                this.BiddingMessageModify1.BiddingCode = Request["BiddingCode"] + "";
            else
                this.BiddingMessageModify1.BiddingCode = "";
            this.BiddingMessageModify1.UserCode = user.UserCode;
            BiddingMessageModify1.SetAttachList = ModuleState.Eyeable;

            Control_BiddingEmitMoney1.State = ModuleState.Eyeable;
            Control_BiddingEmitMoney1.BiddingCode = this.BiddingMessageModify1.BiddingCode;


        }
        else
        {
            this.btnSave.Visible = true;
            this.btnUpdate.Visible = false;
            this.BiddingMessageModify1.ApplicationCode = "";
            this.BiddingMessageModify1.State = ModuleState.Operable;
            this.BiddingMessageModify1.MoneyState = ModuleState.Eyeable;
            if (Request["BiddingCode"] != null && Request["BiddingCode"].ToString() != "")
                this.BiddingMessageModify1.BiddingCode = Request["BiddingCode"] + "";
            else
                this.BiddingMessageModify1.BiddingCode = "";
            this.BiddingMessageModify1.UserCode = user.UserCode;
            BiddingMessageModify1.SetAttachList = ModuleState.Operable;

            Control_BiddingEmitMoney1.State = ModuleState.Eyeable;
            Control_BiddingEmitMoney1.BiddingCode = this.BiddingMessageModify1.BiddingCode;

        }

        this.BiddingMessageModify1.InitControl();
       
        ///是否显示Money;
        
        //Control_BiddingEmitMoney1.InitPage();			
        /**************************************************************************************/
        /*************************************************************************************/
        



        /**************************************************************************************
        this.LeavePass1.ApplicationCode = this.WorkFlowToolbar1.ApplicationCode;//同意按钮组

        this.LeavePass1.State = this.WorkFlowToolbar1.GetModuleState("PassBtn");
        this.LeavePass1.InitControl();
        /**************************************************************************************/

        base.InitPage();

       
    }
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        using (StandardEntityDAO dao = new StandardEntityDAO("Leave"))
        {
            dao.BeginTrans();
            try
            {
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
                //Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
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
        

        if (this.BiddingMessageModify1.State == ModuleState.Operable)
        {
            //this.BiddingMessageModify1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
            this.BiddingMessageModify1.dao = dao;
            this.BiddingMessageModify1.SubmitData();
            RmsPM.BLL.BiddingSystem.Set_BiddingState("42", this.BiddingMessageModify1.BiddingCode);
        }

        return true;

    }

    protected void btnUpdate_ServerClick(object sender, EventArgs e)
    {
        this.btnSave.Visible = true;
        this.btnUpdate.Visible = false;
        this.BiddingMessageModify1.ApplicationCode = Request["ApplicationCode"].ToString();
        this.BiddingMessageModify1.State = ModuleState.Operable;
        this.BiddingMessageModify1.MoneyState = ModuleState.Eyeable;
        this.BiddingMessageModify1.BiddingCode = Request["BiddingCode"] + "";
        this.BiddingMessageModify1.UserCode = user.UserCode;
        BiddingMessageModify1.SetAttachList = ModuleState.Operable;

        Control_BiddingEmitMoney1.State = ModuleState.Eyeable;
        Control_BiddingEmitMoney1.BiddingCode = this.BiddingMessageModify1.BiddingCode;
    }
}
