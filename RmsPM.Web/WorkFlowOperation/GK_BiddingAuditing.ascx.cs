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
using RmsPM.Web.WorkFlowControl;
using RmsPM.BLL;
using RmsPM.Web;


public partial class WorkFlowOperation_GK_BiddingAuditing : WorkFlowOperationBase
{
    protected System.Web.UI.HtmlControls.HtmlGenericControl OperableDiv;
    protected System.Web.UI.HtmlControls.HtmlTableCell tdProjectName;
    protected System.Web.UI.HtmlControls.HtmlTableCell tdContractNember;
    protected System.Web.UI.HtmlControls.HtmlTableCell tdBiddingTitle;

    /// <summary>
    /// 业务代码
    /// </summary>
    private string _BiddingCode = "";
    
    private ModuleState _State2 = ModuleState.Unbeknown;
    private ModuleState _State3 = ModuleState.Unbeknown;
    private ModuleState _State4 = ModuleState.Unbeknown;
    private ModuleState _State5 = ModuleState.Unbeknown;
    private ModuleState _SupplierState = ModuleState.Unbeknown;//辅助状态

    private ModuleState _State1;

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

    public ModuleState SupplierState
    {
        get
        {
            if (_SupplierState == ModuleState.Unbeknown)
            {
                if (this.ViewState["_SupplierState"] != null)
                    return (ModuleState)this.ViewState["_SupplierState"];
                return ModuleState.Unbeknown;
            }
            return _SupplierState;
        }
        set
        {
            _SupplierState = value;
            this.ViewState["_SupplierState"] = value;
        }
    }
    
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
    /// 合约部意见设置

    /// </summary>
    public ModuleState SetAgreementMessage
    {
        get
        {
            if (this.ViewState["_SetAgreementMessage"] != null)
                return (ModuleState)this.ViewState["_SetAgreementMessage"];
            else
                return ModuleState.Unbeknown;
        }
        set
        {
            this.ViewState["_SetAgreementMessage"] = value;
        }
    }
    /// <summary>
    /// 工程部意见设置

    /// </summary>
    public ModuleState SetProjectMessage
    {
        get
        {
            if (this.ViewState["_SetProjectMessage"] != null)
                return (ModuleState)this.ViewState["_SetProjectMessage"];
            else
                return ModuleState.Unbeknown;
        }
        set
        {
            this.ViewState["_SetProjectMessage"] = value;
        }
    }
    /// <summary>
    /// 工程部意见设置

    /// </summary>
    public ModuleState SetAdviserMessage
    {
        get
        {
            if (this.ViewState["_SetProjectMessage"] != null)
                return (ModuleState)this.ViewState["_SetProjectMessage"];
            else
                return ModuleState.Unbeknown;
        }
        set
        {
            this.ViewState["_SetProjectMessage"] = value;
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
    public bool SupplierSelectedFlag
    {
        get
        {
            bool Flag = false;
            for (int i = 0; i < this.Repeater1.Items.Count; i++)
            {
                if (((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Checked)
                {
                    Flag = true;
                }
            }
            return Flag;
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
    /// 合同编号
    /// </summary>
    public string ContractNember
    {
        get
        {
            return this.tdContractNember.InnerHtml;
        }
        set
        {
            this.tdContractNember.InnerHtml = value;
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
        // 在此处放置用户代码以初始化页面

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
        }
        else if (this.State == ModuleState.Eyeable)//可见的

        {
            LoadData(false);
        }
        else if (this.State == ModuleState.Sightless)//其它,隐藏商务标及金额
        {
            LoadData(false);
        }
        else if (this.State == ModuleState.Begin)//可见的

        {
            LoadData(false);
        }
        else if (this.State == ModuleState.End)//可见的

        {
            LoadData(false);
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
        RmsPM.BLL.BiddingManage bm = new RmsPM.BLL.BiddingManage();
        RmsPM.BLL.BiddingReturn cBiddingReturn = new RmsPM.BLL.BiddingReturn();
        if (this.ApplicationCode != "")
        {
            cBiddingReturn.BiddingEmitCode = this.ApplicationCode;
            RmsPM.BLL.BiddingEmit cBiddingEmit = new RmsPM.BLL.BiddingEmit();
            cBiddingEmit.BiddingEmitCode = cBiddingReturn.BiddingEmitCode;
            this.BiddingCode = cBiddingEmit.BiddingCode;
        }
        else
        {
            bm.BiddingCode = this.BiddingCode;
            this.ApplicationCode = bm.GetLastBiddingEmitCode();
            cBiddingReturn.BiddingEmitCode = this.ApplicationCode;
        }
        //主体信息
        RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
        cBidding.BiddingCode = this.BiddingCode;
        string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + cBidding.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "')>" + cBidding.Title + "</a>";
        this.tdBiddingTitleTop.InnerHtml = LinkUrl;
        this.ApplicationTitle = cBidding.Title;
        this.tdProjectNameTop.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode);
        if (this.State == ModuleState.Operable)
        {
            this.trOperple.Visible = true;
            this.txtMeetingContent.Text = cBidding.ContentMeeting.Replace("\n", "<br>").Replace("\r", "&nbsp;");
        }
        else
        {
            this.trEyeable.Visible = true;
            this.tdMeetingContent.InnerHtml = cBidding.ContentMeeting.Replace("\n", "<br>").Replace("\r", "&nbsp;");
        }
        this.ViewState["Money"] = cBidding.Money;
        this.ViewState["mostly"] = cBidding.Accessory;
        this.ViewState["BiddingType"] = cBidding.Type;

        //显示业务部门
        this.ucUnit.Value = this.UnitCode;
        this.lblUnit.Text = RmsPM.BLL.SystemRule.GetUnitFullName(this.UnitCode);

        //业务部门是否可改
        if (this.State == ModuleState.Operable)
        {
            this.tdUnitModify.Visible = true;
            this.tdUnitView.Visible = false ;
        }
        else
        {
            this.tdUnitModify.Visible = false;
            this.tdUnitView.Visible = true;
        }

        this.ProjectCode = bm.ProjectCode;

        DataTable dt = cBiddingReturn.GetBiddingReturns();
        //获取最后的压价信息
        dt = RmsPM.BLL.BiddingSystem.GetAuditingMessage(dt, this.BiddingCode, this.ApplicationCode);
        DataView dv1 = new DataView(dt);
        //按价格排
        DataView dv2 = new DataView(dt);
        dv2.Sort = "BiddingDtlCode,Money";
        int le = dv1.Table.Rows.Count;
        //DataRow dr
        dv1.Table.Columns.Add("myState", System.Type.GetType("System.String"));
        int j = 0;
        int k = 0;
        foreach (DataRowView drv2 in dv2)
        {
            j++;
            foreach (DataRowView drv1 in dv1)
            {
                if (drv2["BiddingReturnCode"] == drv1["BiddingReturnCode"] && drv2["BiddingDtlCode"] == drv1["BiddingDtlCode"])
                {
                    drv1["myState"] = drv2["State"];
                    break;
                }
            }
        }
        this.Repeater1.DataSource = dv1;
        this.Repeater1.DataBind();
        for (int i = 0; i < this.Repeater1.Items.Count; i++)
        {

            if (this.SupplierState == ModuleState.Operable)
            {
                ((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Visible = true;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanAuditing")).Visible = false;
                ((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Checked = (dt.Rows[i]["Flag"].ToString() == "1");
            }
            else
            {
                ((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Visible = false;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanAuditing")).Visible = true;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanAuditing")).InnerHtml = this.SpanText("1", dt.Rows[i]["Flag"].ToString());
            }
            if (this.State1 == ModuleState.Operable)
            {
                for (k = 0; k < ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Items.Count; k++)
                {
                    ListItem ud_Item = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Items[k];
                    if (ud_Item.Value == dt.Rows[i]["Design"].ToString())
                    {
                        ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Items[k].Selected = true;
                        break;
                    }
                }
                //					((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).SelectedValue = dt.Rows[i]["Design"].ToString();
                ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Visible = true;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanDesign")).Visible = false;
            }
            else
            {
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanDesign")).InnerHtml = this.SpanText("2", dt.Rows[i]["Design"].ToString());
                ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).Visible = false;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanDesign")).Visible = true;
            }
            if (this.State2 == ModuleState.Operable)
            {
                for (k = 0; k < ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Items.Count; k++)
                {
                    ListItem ud_Item = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Items[k];
                    if (ud_Item.Value == dt.Rows[i]["Design"].ToString())
                    {
                        ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Items[k].Selected = true;
                        break;
                    }
                }

                ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Visible = true;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanProject")).Visible = false;
            }
            else
            {
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanProject")).InnerHtml = this.SpanText("2", dt.Rows[i]["Project"].ToString());
                ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).Visible = false;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanProject")).Visible = true;
            }
            if (this.State3 == ModuleState.Operable)
            {
                for (k = 0; k < ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Items.Count; k++)
                {
                    ListItem ud_Item = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Items[k];
                    if (ud_Item.Value == dt.Rows[i]["Design"].ToString())
                    {
                        ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Items[k].Selected = true;
                        break;
                    }
                }
                ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Visible = true;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanConsultant")).Visible = false;
            }
            else
            {
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanConsultant")).InnerHtml = this.SpanText("2", dt.Rows[i]["Consultant"].ToString());
                ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).Visible = false;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanConsultant")).Visible = true;
            }
            if (this.State4 == ModuleState.Operable)
            {
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("txtState")).Visible = true;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanState")).Visible = false;
            }
            else
            {
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("txtState")).Visible = false;
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanState")).Visible = true;
            }
            //商务标报价是否显示

            if (this.State5 == ModuleState.Sightless)
            {
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("txtState")).InnerHtml = "&nbsp;";
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanState")).InnerHtml = "&nbsp;";
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spMoney")).InnerHtml = "&nbsp;";
            }
            //显示评选结果

            if (this.SetAgreementMessage == ModuleState.Sightless)
            {
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanDesign")).Visible = false;
            }
            if (this.SetProjectMessage == ModuleState.Sightless)
            {
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanProject")).Visible = false;
            }
            if (this.SetAdviserMessage == ModuleState.Sightless)
            {
                ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("spanConsultant")).Visible = false;
            }
        }
        dt.Dispose();
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

            if (this.tdUnitModify.Visible)
            {
                if (this.ucUnit.Value == "")
                {
                    ErrMsg = "请选择业务部门";
                    return ErrMsg;
                }

                base.UnitCode = this.ucUnit.Value;
            }

            if (trOperple.Visible)
            {
                RmsPM.BLL.BiddingManage bm = new RmsPM.BLL.BiddingManage();
                bm.BiddingCode = this.BiddingCode;
                bm.ContentMeeting = this.txtMeetingContent.Text;
                //bm.ObligateMoney = this.TeamMoney;

                bm.dao = this.dao;
                bm.BiddingSubmit();

            }

            //RmsPM.DAL.QueryStrategy.BiddingReturnStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.BiddingReturnStrategyBuilder();
            //sb.AddStrategy(new Strategy(RmsPM.DAL.QueryStrategy.BiddingReturnStrategyName.BiddingEmitCode, this.ApplicationCode));

            //string sql = sb.BuildMainQueryString();

            //EntityData entity = new EntityData("BiddingReturn");
            //dao.FillEntity(sql, entity);

            //for (int i = 0; i < this.Repeater1.Items.Count; i++)
            //{
            //    string _BiddingReturnCode = ((HtmlInputText)this.Repeater1.Items[i].FindControl("txtBiddingReturnCode")).Value.Trim();
            //    string _Design = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).SelectedValue;
            //    string _Project = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).SelectedValue;
            //    string _Consultant = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).SelectedValue;
            //    string _State = ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("txtState")).InnerText.Trim();
            //    string _Flag = "0";

            //    if (((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Checked)
            //    {
            //        _Flag = "1";
            //        //string flag="1";
            //        try
            //        {
            //            string supplier = ((HtmlInputText)this.Repeater1.Items[i].FindControl("hiddenSupplierCode")).Value.Trim();
            //            RmsPM.BLL.BiddingReturn cbiddingReturn = new RmsPM.BLL.BiddingReturn();
            //            cbiddingReturn.dao = this.dao;
            //            cbiddingReturn.BiddingReturnCode = _BiddingReturnCode;
            //            cbiddingReturn.Flag = "1";
            //            cbiddingReturn.BiddingReturnSubmit();
            //        }
            //        catch (Exception ex)
            //        {
            //            Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
            //        }
            //    }

            //    DataRow[] dr = entity.CurrentTable.Select("BiddingReturnCode=" + _BiddingReturnCode);
            //    if (_Design != "" && this.State1 == ModuleState.Operable)
            //    {
            //        dr[0]["Design"] = _Design;
            //    }
            //    if (_Project != "" && this.State2 == ModuleState.Operable)
            //    {
            //        dr[0]["Project"] = _Project;
            //    }
            //    if (_Consultant != "" && this.State3 == ModuleState.Operable)
            //    {
            //        dr[0]["Consultant"] = _Consultant;
            //    }
            //    if (_State != "" && this.State4 == ModuleState.Operable)
            //    {
            //        dr[0]["State"] = _State;
            //    }
            //    if (this.SupplierState == ModuleState.Operable)
            //    {
            //        dr[0]["Flag"] = _Flag;
            //    }
            //}

            //dao.SubmitEntity(entity);
            return ErrMsg;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
            throw ex;
        }
        //if (this.SupplierState == ModuleState.Operable)
        //{
        //    RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
        //    bidding.BiddingCode = this.BiddingCode;
        //    bidding.dao = this.dao;
        //    bidding.State = "4";
        //    bidding.BiddingSubmit();
        //}
    }

    public string SpanText(string Type, string Value)
    {
        string returnText = "&nbsp;";
        if (Type == "1")
        {
            if (Value == "1")
                returnText = "√";
        }
        else
        {
            if (Value == "0")
                returnText = "不符合";
            if (Value == "1")
                returnText = "符合";
        }
        return returnText;
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

            
            RmsPM.DAL.QueryStrategy.BiddingReturnStrategyBuilder sb = new RmsPM.DAL.QueryStrategy.BiddingReturnStrategyBuilder();
            sb.AddStrategy(new Strategy(RmsPM.DAL.QueryStrategy.BiddingReturnStrategyName.BiddingEmitCode, this.ApplicationCode));

            string sql = sb.BuildMainQueryString();

            EntityData entity = new EntityData("BiddingReturn");
            if (this.dao == null)
            {
                this.dao = new StandardEntityDAO("BiddingReturn");
            }
            dao.FillEntity(sql, entity);

            for (int i = 0; i < this.Repeater1.Items.Count; i++)
            {
                string _BiddingReturnCode = ((HtmlInputText)this.Repeater1.Items[i].FindControl("txtBiddingReturnCode")).Value.Trim();
                string _Design = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioDesign")).SelectedValue;
                string _Project = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioProject")).SelectedValue;
                string _Consultant = ((RadioButtonList)this.Repeater1.Items[i].FindControl("RadioConsultant")).SelectedValue;
                string _State = ((HtmlGenericControl)this.Repeater1.Items[i].FindControl("txtState")).InnerText.Trim();
                string _Flag = "0";

                if (((HtmlInputCheckBox)this.Repeater1.Items[i].FindControl("chkAuditing")).Checked)
                {
                    _Flag = "1";
                    //string flag="1";
                    try
                    {
                        string supplier = ((HtmlInputText)this.Repeater1.Items[i].FindControl("hiddenSupplierCode")).Value.Trim();
                        RmsPM.BLL.BiddingReturn cbiddingReturn = new RmsPM.BLL.BiddingReturn();
                        cbiddingReturn.dao = this.dao;
                        cbiddingReturn.BiddingReturnCode = _BiddingReturnCode;
                        cbiddingReturn.Flag = "1";
                        cbiddingReturn.BiddingReturnSubmit();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, ex.Message));
                    }
                }

                DataRow[] dr = entity.CurrentTable.Select("BiddingReturnCode=" + _BiddingReturnCode);
                if (_Design != "" && this.State1 == ModuleState.Operable)
                {
                    dr[0]["Design"] = _Design;
                }
                if (_Project != "" && this.State2 == ModuleState.Operable)
                {
                    dr[0]["Project"] = _Project;
                }
                if (_Consultant != "" && this.State3 == ModuleState.Operable)
                {
                    dr[0]["Consultant"] = _Consultant;
                }
                if (_State != "" && this.State4 == ModuleState.Operable)
                {
                    dr[0]["State"] = _State;
                }
                if (this.SupplierState == ModuleState.Operable)
                {
                    dr[0]["Flag"] = _Flag;
                }
            }

            dao.SubmitEntity(entity);

            if (this.SupplierState == ModuleState.Operable)
            {
                RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
                bidding.BiddingCode = this.BiddingCode;
                bidding.dao = this.dao;
                bidding.State = "4";
                bidding.BiddingSubmit();
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

    public override string ChangeStatusWhenSend(StandardEntityDAO dao)
    {
        try
        {

            base.ChangeStatusWhenSend(dao);

            string ErrMsg = "";

            if (this.State == ModuleState.Operable)
            {
                BiddingSystem.Set_BiddingState("5", this.BiddingCode,this.dao);
            }

            return ErrMsg;
        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "改变业务数据状态出错：" + ex.Message));
            throw ex;
        }

    }

    public override string RestoreStatus()
    {
        try
        {

            base.RestoreStatus();

            string ErrMsg = "";

            BiddingSystem.Set_BiddingState("3", this.BiddingCode);

            return ErrMsg;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "恢复业务数据状态出错：" + ex.Message));
            throw ex;
        }
    }

}
