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
public partial class BiddingManage_XCN_biddingprejudicationmanage : BiddingWorkFlowBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.InitPage();
        }
    }

    public void InitPage()
    {
        //if (Request["ApplicationCode"] != null)
            //this.BiddingPrejudicationModify1.ApplicationCode = Request["ApplicationCode"].ToString();
        //目前情况下只有新增


        if (Request["ApplicationCode"] != null && Request["ApplicationCode"].ToString() != "")
        {
            this.btnSave.Visible = false;
            this.btnUpdate.Visible = true; 
            if (Request["BiddingCode"] != null && Request["BiddingCode"].ToString() != "")
                this.BiddingPrejudicationModify1.BiddingCode = Request["BiddingCode"] + "";
            else
                this.BiddingPrejudicationModify1.BiddingCode = "";
            this.BiddingPrejudicationModify1.State = ModuleState.Eyeable;
            this.BiddingPrejudicationModify1.State1 = ModuleState.Eyeable;
            this.BiddingPrejudicationModify1.UserCode = user.UserCode;
            this.BiddingPrejudicationModify1.ApplicationCode = Request["ApplicationCode"].ToString();
        }
        else
        {
            this.btnSave.Visible = true;
            this.btnUpdate.Visible = false;
            if (Request["BiddingCode"] != null && Request["BiddingCode"].ToString() != "")
                this.BiddingPrejudicationModify1.BiddingCode = Request["BiddingCode"] + "";
            else
                this.BiddingPrejudicationModify1.BiddingCode = "";
            this.BiddingPrejudicationModify1.State = ModuleState.Operable;
            this.BiddingPrejudicationModify1.State1 = ModuleState.Operable; 
            this.BiddingPrejudicationModify1.UserCode = user.UserCode;
            this.BiddingPrejudicationModify1.ApplicationCode = "";
        }
        this.BiddingPrejudicationModify1.InitControl();

       

        //*** UCBiddingSupplierList(参加资格预审的单位名单) 控件初始化 **************************************************************************
        string BiddingPrejudicationCode = "";

        if (this.BiddingPrejudicationModify1.ApplicationCode == "")
            BiddingPrejudicationCode = this.BiddingPrejudicationModify1.tempCode;
        else
            BiddingPrejudicationCode = this.BiddingPrejudicationModify1.ApplicationCode;

        this.UCBiddingSupplierList1.BiddingPrejudicationCode = BiddingPrejudicationCode;
        this.UCBiddingSupplierList1.CanSelect = this.BiddingPrejudicationModify1.SelectState;
        this.UCBiddingSupplierList1.CanModify = this.BiddingPrejudicationModify1.EditState;
        //*****************************************************************************

        //*** UCBiddingSupplierModify 控件初始化 **************************************************************************
        this.UCBiddingSupplierModify1.BiddingPrejudicationCode = BiddingPrejudicationCode;
        this.UCBiddingSupplierModify1.BiddingSupplierCode = "";
        this.UCBiddingSupplierModify1.DoType = "SingleModify";
        this.UCBiddingSupplierModify1.IniControl();
        this.UCBiddingSupplierModify1.Visible = this.BiddingPrejudicationModify1.EditState;
    }

    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        using (StandardEntityDAO dao = new StandardEntityDAO("Leave"))
        {
            dao.BeginTrans();
            if (!this.UCBiddingSupplierList1.SelectedSupplierFlag && this.BiddingPrejudicationModify1.State1 == ModuleState.Operable)
            {
                this.RegisterStartupScript("", "<script>alert('请选择预审通过单位！');</script>");
                return;
            }
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
                if (ex.Message == "编号不能为空")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
                    return;
                }
               throw ex;
            }
        }
    }

    /// ****************************************************************************
    /// <summary>
    /// 业务数据操作
    /// </summary>
    /// ****************************************************************************
    protected Boolean DataSubmit(StandardEntityDAO dao)
    {
       

        if (this.BiddingPrejudicationModify1.State == ModuleState.Operable)
        {
            //this.BiddingPrejudicationModify1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
            this.BiddingPrejudicationModify1.dao = dao;
            this.BiddingPrejudicationModify1.SubmitData();
            //this.UCBiddingSupplierList1.InsertDepartMent();
           //WorkFlowToolbar1.ApplicationCode = this.BiddingPrejudicationModify1.ApplicationCode;
        }

        if (this.BiddingPrejudicationModify1.State1 == ModuleState.Operable)
        {
            //this.BiddingPrejudicationModify1.ApplicationCode = WorkFlowToolbar1.ApplicationCode;
            this.BiddingPrejudicationModify1.dao = dao;
            this.BiddingPrejudicationModify1.SubmitBiddingState();
            this.UCBiddingSupplierList1.BiddingPrejudicationCode = BiddingPrejudicationModify1.ApplicationCode;
        }
        if (this.UCBiddingSupplierList1.CanModify)
        {
            this.UCBiddingSupplierList1.dao = dao;
            this.UCBiddingSupplierList1.ModifyData();
        }
        if (this.UCBiddingSupplierList1.CanSelect)
        {
            this.UCBiddingSupplierList1.dao = dao;
            this.UCBiddingSupplierList1.SaveData();
        }
        //DataGridShowState();
        UCBiddingSupplierList1.UpdateDepartMentSelect();
        
        return true;
       // SaveMeetMessage(dao, this.rptMeetSign);
        //return base.DataSubmit(dao);
        //OpinionDataSubmit(dao);
    }
    protected void btnUpdate_ServerClick(object sender, EventArgs e)
    {
        this.btnSave.Visible = true;
        this.btnUpdate.Visible = false;

        this.BiddingPrejudicationModify1.BiddingCode = Request["BiddingCode"] + "";
        this.BiddingPrejudicationModify1.State = ModuleState.Operable;
        this.BiddingPrejudicationModify1.State1 = ModuleState.Operable;
        this.BiddingPrejudicationModify1.UserCode = user.UserCode;
        this.BiddingPrejudicationModify1.ApplicationCode = Request["ApplicationCode"].ToString();
        this.BiddingPrejudicationModify1.InitControl();

        //this.UCBiddingSupplierList1.BiddingPrejudicationCode = BiddingPrejudicationCode;
        this.UCBiddingSupplierList1.CanSelect = this.BiddingPrejudicationModify1.SelectState;
        this.UCBiddingSupplierList1.CanModify = this.BiddingPrejudicationModify1.EditState;
        //*****************************************************************************

        //*** UCBiddingSupplierModify 控件初始化 **************************************************************************
       // this.UCBiddingSupplierModify1.BiddingPrejudicationCode = BiddingPrejudicationCode;
        this.UCBiddingSupplierModify1.BiddingSupplierCode = "";
        this.UCBiddingSupplierModify1.DoType = "SingleModify";
        this.UCBiddingSupplierModify1.IniControl();
        this.UCBiddingSupplierModify1.Visible = this.BiddingPrejudicationModify1.EditState;

    }

    override protected void LoadEvent()
    {
        base.LoadEvent();
        //this.WorkFlowToolbar1.ToolbarCommand += new System.EventHandler(this.WorkFlowToolbar1_ToolbarCommand);
        this.UCBiddingSupplierModify1.SaveDataEvent += new System.EventHandler(this.UCBiddingSupplierModify1_SaveData);
        

    }

    private void UCBiddingSupplierModify1_SaveData(object sender, System.EventArgs e)
    {
        this.UCBiddingSupplierList1.LoadData();
    }

   
}
