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
using RmsPM.Web;

public partial class WorkFlowOperation_SM_BiddingPrejudicationManage : WorkFlowOperationBase
{
    /// <summary>
    /// 业务代码
    /// </summary>
    private string _BiddingCode = "";

    private bool _AuditReturn;

    /// <summary>
    /// 辅助状态


    /// </summary>
    private ModuleState _State1;

    private ModuleState _SetAttachList1;
    private ModuleState _SetAttachList2;
    private ModuleState _SetAttachList3;


    //返回控件
    public RmsPM.Web.BiddingManage.UCBiddingSupplierModify up_UCBiddingSupplierModify
    {
        get { return (RmsPM.Web.BiddingManage.UCBiddingSupplierModify)this.FindControl("UCBiddingSupplierModify1"); }
    }

    public RmsPM.Web.BiddingManage.UCBiddingSupplierList uc_UCBiddingSupplierList
    {
        get { return (RmsPM.Web.BiddingManage.UCBiddingSupplierList)this.FindControl("UCBiddingSupplierList1"); }
    }
    /// <summary>
    /// 辅助状态1
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
    /// 业务代码
    /// </summary>
    public string BiddingCode
    {
        get
        {
            if (_BiddingCode == "")
            {
                if (this.ViewState["_BiddingCode"] != null)
                    return this.ViewState["_BiddingCode"].ToString();
                return "";
            }
            return _BiddingCode;
        }
        set
        {
            _BiddingCode = value;
            this.ViewState["_BiddingCode"] = value;
        }
    }
    /// <summary>
    /// 拟定标段
    /// </summary>
    public string BiddingTitle
    {
        get
        {
            return this.ViewState["BiddingTitle"].ToString();
        }
        set
        {
            this.ViewState["BiddingTitle"] = value;
        }
    }
    public string tempCode
    {
        get
        {
            if (this.ViewState["tempCode"] != null)
                return this.ViewState["tempCode"].ToString();
            return "";
        }
        set
        {
            this.ViewState["tempCode"] = value;
        }
    }

    public bool AuditReturn
    {
        get
        {
            if (this.ViewState["_AuditReturn"] != null)
                return System.Convert.ToBoolean(this.ViewState["_AuditReturn"]);
            return false;
        }
        set
        {
            this.ViewState["_AuditReturn"] = value;
        }
    }
    /// <summary>
    /// DepartMentCode
    /// </summary>
    public string DepartMentCode
    {
        get
        {
            if (this.ViewState["DepartMentCode"] != null)
                return this.ViewState["DepartMentCode"].ToString();
            return "";
        }
        set
        {
            this.ViewState["DepartMentCode"] = value;
        }
    }
    public bool SelectState
    {
        get
        {
            if (this.ViewState["SelectState"] != null)
                return (bool)this.ViewState["SelectState"];
            return false;
        }
        set
        {
            this.ViewState["SelectState"] = value;
        }
    }
    public bool EditState
    {
        get
        {
            if (this.ViewState["EditState"] != null)
                return (bool)this.ViewState["EditState"];
            return false;
        }
        set
        {
            this.ViewState["EditState"] = value;
        }
    }
    public string Money
    {
        get
        {
            return this.ViewState["Money"].ToString();
        }
    }
    public string mostly
    {
        get
        {
            return this.ViewState["mostly"].ToString();
        }
    }
    public string BiddingType
    {
        get
        {
            return this.ViewState["BiddingType"].ToString();
        }
    }
    /// <summary>
    /// 附件1
    /// </summary>
    public ModuleState SetAttachList1
    {
        get
        {
            if (_SetAttachList1 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_SetAttachList1"] != null)
                    return (ModuleState)this.ViewState["_SetAttachList1"];
                return ModuleState.Unbeknown;
            }
            return _SetAttachList1;
        }
        set
        {
            if (value == ModuleState.Sightless)//不可见的
            {
                AttachMentAdd1.Visible = false;
                AttachMentList1.Visible = false;
            }
            else if (value == ModuleState.Operable)//可操作的
            {
                AttachMentAdd1.Visible = true;
                AttachMentList1.Visible = false;

            }
            else if (value == ModuleState.Eyeable)//可见的

            {
                AttachMentAdd1.Visible = false;
                AttachMentList1.Visible = true;

            }
            else if (value == ModuleState.Begin)//可见的

            {
                AttachMentAdd1.Visible = false;
                AttachMentList1.Visible = true;
            }
            else if (value == ModuleState.End)//可见的

            {
                AttachMentAdd1.Visible = false;
                AttachMentList1.Visible = true;
            }
            else
            {
                AttachMentAdd1.Visible = false;
                AttachMentList1.Visible = false;
            }
            this.ViewState["_SetAttachList1"] = value;
            this._SetAttachList1 = value;

        }
    }
    /// <summary>
    /// 附件2
    /// </summary>
    public ModuleState SetAttachList2
    {
        get
        {
            if (_SetAttachList2 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_SetAttachList2"] != null)
                    return (ModuleState)this.ViewState["_SetAttachList2"];
                return ModuleState.Unbeknown;
            }
            return _SetAttachList2;
        }
        set
        {
            if (value == ModuleState.Sightless)//不可见的
            {
                AttachMentAdd2.Visible = false;
                AttachMentList2.Visible = false;
            }
            else if (value == ModuleState.Operable)//可操作的
            {
                AttachMentAdd2.Visible = true;
                AttachMentList2.Visible = false;

            }
            else if (value == ModuleState.Eyeable)//可见的

            {
                AttachMentAdd2.Visible = false;
                AttachMentList2.Visible = true;

            }
            else if (value == ModuleState.Begin)//可见的

            {
                AttachMentAdd2.Visible = false;
                AttachMentList2.Visible = true;
            }
            else if (value == ModuleState.End)//可见的

            {
                AttachMentAdd2.Visible = false;
                AttachMentList2.Visible = true;
            }
            else
            {
                AttachMentAdd2.Visible = false;
                AttachMentList2.Visible = false;
            }
            this.ViewState["_SetAttachList2"] = value;
            this._SetAttachList2 = value;

        }
    }
    /// <summary>
    /// 附件3
    /// </summary>
    public ModuleState SetAttachList3
    {
        get
        {
            if (_SetAttachList3 == ModuleState.Unbeknown)
            {
                if (this.ViewState["_SetAttachList3"] != null)
                    return (ModuleState)this.ViewState["_SetAttachList3"];
                return ModuleState.Unbeknown;
            }
            return _SetAttachList3;
        }
        set
        {
            if (value == ModuleState.Sightless)//不可见的
            {
                AttachMentAdd3.Visible = false;
                AttachMentList3.Visible = false;
            }
            else if (value == ModuleState.Operable)//可操作的
            {
                AttachMentAdd3.Visible = true;
                AttachMentList3.Visible = false;

            }
            else if (value == ModuleState.Eyeable)//可见的

            {
                AttachMentAdd3.Visible = false;
                AttachMentList3.Visible = true;

            }
            else if (value == ModuleState.Begin)//可见的

            {
                AttachMentAdd3.Visible = false;
                AttachMentList3.Visible = true;
            }
            else if (value == ModuleState.End)//可见的

            {
                AttachMentAdd3.Visible = false;
                AttachMentList3.Visible = true;
            }
            else
            {
                AttachMentAdd3.Visible = false;
                AttachMentList3.Visible = false;
            }
            this.ViewState["_SetAttachList3"] = value;
            this._SetAttachList3 = value;

        }
    }



    /// ****************************************************************************
    /// <summary>
    /// 组件加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// ****************************************************************************
    protected void Page_Load(object sender, System.EventArgs e)
    {

        AttachMentAdd1.AttachMentType = "BiddingPrejudication1";
        AttachMentList1.AttachMentType = "BiddingPrejudication1";
        AttachMentAdd2.AttachMentType = "BiddingPrejudication2";
        AttachMentList2.AttachMentType = "BiddingPrejudication2";
        AttachMentAdd3.AttachMentType = "BiddingPrejudication3";
        AttachMentList3.AttachMentType = "BiddingPrejudication3";


        if (this.ApplicationCode != "")
        {
            AttachMentAdd1.MasterCode = this.ApplicationCode;
            AttachMentList1.MasterCode = this.ApplicationCode;
            AttachMentAdd2.MasterCode = this.ApplicationCode;
            AttachMentList2.MasterCode = this.ApplicationCode;
            AttachMentAdd3.MasterCode = this.ApplicationCode;
            AttachMentList3.MasterCode = this.ApplicationCode;

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
    /// ****************************************************************************
    /// <summary>
    /// 数据加载
    /// </summary>
    /// ****************************************************************************
    private void LoadData(bool Flag)
    {
        if (this.State1 == ModuleState.Operable)
            SelectState = true;

        if (this.ApplicationCode != "")
        {
            RmsPM.BLL.BiddingPrejudication cBiddingPrejudication = new RmsPM.BLL.BiddingPrejudication();
            cBiddingPrejudication.BiddingPrejudicationCode = this.ApplicationCode;
            RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
            cBidding.BiddingCode = cBiddingPrejudication.BiddingCode;

            if (Flag)
            {
                this.txtProjectName.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode) + "&nbsp; ";
                string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + cBidding.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "')>" + cBidding.Title + "</a>";
                this.txtBiddingTitle.InnerHtml = LinkUrl;

                string formartEmitDate = System.Convert.ToDateTime(cBidding.EmitDate).ToString("yyyy") + " 年 " + System.Convert.ToDateTime(cBidding.EmitDate).ToString("MM") + " 月 " + System.Convert.ToDateTime(cBidding.EmitDate).ToString("dd") + " 日";
            

                this.txtNumber.Value = cBiddingPrejudication.Number;
                this.BiddingCode = cBiddingPrejudication.BiddingCode;
             
                this.EditState = true;
                this.TxtEmitDate.Value = cBidding.EmitDate;
                this.ApplicationTitle = cBidding.Title;
     
                this.ViewState["BiddingTitle"] = cBidding.Title;
                this.DepartMentCode = cBidding.BiddingRemark1;

               

            }
            else
            {
                this.tdProjectName.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode) + "&nbsp; ";
                string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + cBidding.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "')>" + cBidding.Title + "</a>";
                this.tdBiddingTitle.InnerHtml = LinkUrl;
           
                string formartEmitDate = System.Convert.ToDateTime(cBidding.EmitDate).ToString("yyyy") + " 年 " + System.Convert.ToDateTime(cBidding.EmitDate).ToString("MM") + " 月 " + System.Convert.ToDateTime(cBidding.EmitDate).ToString("dd") + " 日";
               
                this.tdNumber.InnerHtml = cBiddingPrejudication.Number + "&nbsp; ";
                this.BiddingCode = cBiddingPrejudication.BiddingCode;
                this.EditState = false;
                this.ViewState["BiddingTitle"] = cBidding.Title;
                this.ApplicationTitle = cBidding.Title;
                this.tdEmitDate.InnerHtml = cBidding.ConfirmDate;
                this.DepartMentCode = cBidding.BiddingRemark1;
                
             
            }
        }
        else
        {
            if (Flag)
            {
                RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
                cBidding.BiddingCode = this.BiddingCode;
                this.txtProjectName.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode) + "&nbsp; ";

                string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + cBidding.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "')>" + cBidding.Title + "</a>";
                this.txtBiddingTitle.InnerHtml = LinkUrl;
                //this.txtEmitDate.InnerHtml = cBidding.EmitDate + "&nbsp; ";
                this.ViewState["BiddingTitle"] = cBidding.Title;

                this.TxtEmitDate.Value = cBidding.EmitDate;

              
                this.ApplicationTitle = cBidding.Title;
                this.EditState = true;
            
            }
            this.ViewState["tempCode"] = this.UserCode + DateTime.Now.ToString();
        }
        RmsPM.BLL.Bidding ccBidding = new RmsPM.BLL.Bidding();
        ccBidding.BiddingCode = this.BiddingCode;
        this.ViewState["Money"] = ccBidding.Money;
        this.ViewState["mostly"] = ccBidding.Accessory;
        this.ViewState["BiddingType"] = ccBidding.Type;
        this.ProjectCode = ccBidding.ProjectCode;
        this.ApplicationType = RmsPM.BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(this.BiddingType);



        string BiddingPrejudicationCode = "";

        if (this.ApplicationCode == "")
            BiddingPrejudicationCode = this.tempCode;
        else
            BiddingPrejudicationCode = this.ApplicationCode;
        //*** UCBiddingSupplierList 控件初始化 **************************************************************************
        this.UCBiddingSupplierList1.BiddingPrejudicationCode = BiddingPrejudicationCode;
        this.UCBiddingSupplierList1.CanSelect = this.SelectState;
        this.UCBiddingSupplierList1.CanModify = this.EditState;
       

        //*****************************************************************************

        //*** UCBiddingSupplierModify 控件初始化 **************************************************************************
        this.UCBiddingSupplierModify1.SupplierCode = "ucOperationControl_UCBiddingSupplierModify1_HideSupplierCode";
        this.UCBiddingSupplierModify1.SupplierName = "ucOperationControl_UCBiddingSupplierModify1_txtSupplierName";
        this.UCBiddingSupplierModify1.BiddingPrejudicationCode = BiddingPrejudicationCode;
        this.UCBiddingSupplierModify1.BiddingSupplierCode = "";
        this.UCBiddingSupplierModify1.DoType = "SingleModify";
        this.UCBiddingSupplierModify1.IniControl();
        this.UCBiddingSupplierModify1.Visible = this.EditState;
        this.UCBiddingSupplierList1.IniControl();
        this.UCBiddingSupplierList1.LoadData();

        //this.ViewSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ViewState["tempCode"].ToString()+"','view','"+SelectState+"');return false\">参加资格预审的单位名单</a>";
        //this.EditSupplier.InnerHtml = "<a href=\"#\" onclick=\"javascript:BiddingPrejudicationOpenSupplierPage('"+this.ViewState["tempCode"].ToString()+"','edit','"+SelectState+"');return false\">参加资格预审的单位名单</a>";
    }

    /// ****************************************************************************
    /// <summary>
    /// 提交数据
    /// </summary>
    /// ****************************************************************************
    public override string SubmitData()
    {
        try
        {

            string ErrMsg = "";

            if (txtNumber.Value == "")
            {
                //Response.Write(Rms.Web.JavaScript.Alert(true,"合同编号不能为空"));
                ErrMsg = "编号不能为空";
                return ErrMsg;
            }
            if (!this.UCBiddingSupplierList1.SelectedSupplierFlag && this.State1 == ModuleState.Operable)
            {
                ErrMsg = "请选择预审通过单位";

                return ErrMsg;
            }
            if (this.State == ModuleState.Operable)
            {
                RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
                cBidding.BiddingCode = this.BiddingCode;
                cBidding.ConfirmDate = this.TxtEmitDate.Text;
            
                cBidding.BiddingSubmit();


                RmsPM.BLL.BiddingPrejudication cBiddingPrejudication = new RmsPM.BLL.BiddingPrejudication();
                cBiddingPrejudication.BiddingPrejudicationCode = this.ApplicationCode;
                cBiddingPrejudication.BiddingCode = this.BiddingCode;
                cBiddingPrejudication.Number = this.txtNumber.Value;
                cBiddingPrejudication.UserCode = this.UserCode;
                cBiddingPrejudication.CreateDate = DateTime.Now.ToShortDateString();
                cBiddingPrejudication.State = "";
                cBiddingPrejudication.Flag = "";
                cBiddingPrejudication.dao = this.dao;
                cBiddingPrejudication.BiddingPrejudicationSubmit();


                if (this.ApplicationCode == "")
                {
                    RmsPM.BLL.BiddingSupplier cBiddingSupplier = new RmsPM.BLL.BiddingSupplier();
                    cBiddingSupplier.BiddingPrejudicationCode = this.ViewState["tempCode"].ToString();
                    cBiddingSupplier.dao = dao;
                    EntityData entity = cBiddingSupplier._GetBiddingSuppliers();
                    for (int i = 0; i < entity.CurrentTable.Rows.Count; i++)
                    {
                        entity.CurrentTable.Rows[i]["BiddingPrejudicationCode"] = cBiddingPrejudication.BiddingPrejudicationCode;
                    }
                    dao.SubmitEntity(entity);
                    this.ApplicationCode = cBiddingPrejudication.BiddingPrejudicationCode;

                    RmsPM.BLL.BiddingSystem.UpDataPrejudicationCode(cBiddingPrejudication.BiddingPrejudicationCode, this.ViewState["tempCode"].ToString());
                }
            }


            if (SetAttachList1 == ModuleState.Operable)
                this.AttachMentAdd1.SaveAttachMent(this.ApplicationCode);
            if (SetAttachList2 == ModuleState.Operable)
                this.AttachMentAdd2.SaveAttachMent(this.ApplicationCode);
            if (SetAttachList3 == ModuleState.Operable)
                this.AttachMentAdd3.SaveAttachMent(this.ApplicationCode);


            if (this.State1 == ModuleState.Operable)
            {
                //this.BiddingPrejudicationModify1.ApplicationCode = this.ApplicationCode;
                //this.BiddingPrejudicationModify1.dao = dao;
                //this.SubmitBiddingState();
                this.UCBiddingSupplierList1.BiddingPrejudicationCode = this.ApplicationCode;
            }
            if (this.UCBiddingSupplierList1.CanModify)
            {
                this.UCBiddingSupplierList1.dao = this.dao;
                this.UCBiddingSupplierList1.ModifyData();
            }
            if (this.UCBiddingSupplierList1.CanSelect)
            {
                this.UCBiddingSupplierList1.dao = this.dao;
                this.UCBiddingSupplierList1.SaveData();
            }
            //DataGridShowState();
            UCBiddingSupplierList1.UpdateDepartMentSelect();

            //ErrMsg += this.BiddingSupplierGrade1.SubmitGradeData();
            return ErrMsg;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
            throw ex;
        }
    }

    public void LoadData()
    {
        //this.BiddingSupplierGrade1.InitControl();
    }

    /// <summary>
    /// 业务审核
    /// </summary>
    public override bool Audit(string pm_sOpinionConfirm)
    {
        base.Audit(pm_sOpinionConfirm);

        try
        {

            string ErrMsg = "";


            if (!this.UCBiddingSupplierList1.SelectedSupplierFlag && this.State1 == ModuleState.Operable)
            {
                ErrMsg = "请选择预审通过单位";
                Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));
                return false;
            }
            if (this.State1 == ModuleState.Operable)
            {
                this.SubmitBiddingState();
            }

            if (this.UCBiddingSupplierList1.CanModify)
            {
                this.UCBiddingSupplierList1.dao = this.dao;
                this.UCBiddingSupplierList1.ModifyData();
            }
            if (this.UCBiddingSupplierList1.CanSelect)
            {
                this.UCBiddingSupplierList1.dao = this.dao;
                this.UCBiddingSupplierList1.SaveData();
            }
            if (this.ApplicationCode != "")
            {
                if (SetAttachList1 == ModuleState.Operable)
                    this.AttachMentAdd1.SaveAttachMent(this.ApplicationCode);
                if (SetAttachList2 == ModuleState.Operable)
                    this.AttachMentAdd2.SaveAttachMent(this.ApplicationCode);
                if (SetAttachList3 == ModuleState.Operable)
                    this.AttachMentAdd3.SaveAttachMent(this.ApplicationCode);
            }

            if (ErrMsg != "")
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));
                return false;
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

    public void SubmitBiddingState()
    {
        RmsPM.BLL.BiddingPrejudication cBiddingPrejudication = new RmsPM.BLL.BiddingPrejudication();
        cBiddingPrejudication.BiddingPrejudicationCode = this.ApplicationCode;
        cBiddingPrejudication.dao = this.dao;

        RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
        bidding.dao = this.dao;
        bidding.BiddingCode = cBiddingPrejudication.BiddingCode;
        bidding.State = "1";
        bidding.BiddingSubmit();
    }
}

