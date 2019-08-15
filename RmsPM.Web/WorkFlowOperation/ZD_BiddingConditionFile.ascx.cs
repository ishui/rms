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
using RmsPM.BLL;




public partial class WorkFlowOperation_ZD_BiddingConditionFile : WorkFlowOperationBase
{
    #region 私有变量


    private string _BiddingConditionFileCode = "";
    private string _BiddingCode = "";
    private string _Name = "";//技术条件名
    private string _BiddingConditionFileState = "";//状态

    private string _Zbfw = "";//招标范围
    private string _Jsxq = "";//技术要求及指标
    private string _Zlbz = "";//质量标准
    private string _Gq = "";//工期
    private string _Rctj = "";//入场条件及总包管理方式
    private string _Shfw = "";//保修及售后服务

    private string _Remark = "";//备注


    #endregion

    #region 属性


    public string BiddingConditionFileCode
    {
        get
        {
            if (_BiddingConditionFileCode == "")
            {
                if (this.ViewState["_BiddingConditionFileCode"] != null)
                    return this.ViewState["_BiddingConditionFileCode"].ToString();
                return "";
            }
            return _BiddingConditionFileCode;
        }
        set
        {
            _BiddingConditionFileCode = value;
            this.ViewState["_BiddingConditionFileCode"] = value;
        }
    }

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
    /// 技术条件名
    /// </summary>
    public string Name
    {
        get
        {
            if (_Name == "")
            {
                if (this.ViewState["_Name"] != null)
                    return this.ViewState["_Name"].ToString();
                return "";
            }
            return _Name;
        }
        set
        {
            _Name = value;
            this.ViewState["_Name"] = value;
        }
    }

    /// <summary>
    /// 状态

    /// </summary>
    public string BiddingConditionFileState
    {
        get
        {
            if (_BiddingConditionFileState == "")
            {
                if (this.ViewState["_BiddingConditionFileState"] != null)
                    return this.ViewState["_BiddingConditionFileState"].ToString();
                return "";
            }
            return _BiddingConditionFileState;
        }
        set
        {
            _BiddingConditionFileState = value;
            this.ViewState["_BiddingConditionFileState"] = value;
        }
    }

    /// <summary>
    /// 招标范围
    /// </summary>
    public string Zbfw
    {
        get
        {
            if (_Zbfw == "")
            {
                if (this.ViewState["_Zbfw"] != null)
                    return this.ViewState["_Zbfw"].ToString();
                return "";
            }
            return _Zbfw;
        }
        set
        {
            _Zbfw = value;
            this.ViewState["_Zbfw"] = value;
        }
    }


    /// <summary>
    /// 技术要求及指标
    /// </summary>
    public string Jsxq
    {
        get
        {
            if (_Jsxq == "")
            {
                if (this.ViewState["_Jsxq"] != null)
                    return this.ViewState["_Jsxq"].ToString();
                return "";
            }
            return _Jsxq;
        }
        set
        {
            _Jsxq = value;
            this.ViewState["_Jsxq"] = value;
        }
    }


    /// <summary>
    /// 质量标准
    /// </summary>
    public string Zlbz
    {
        get
        {
            if (_Zlbz == "")
            {
                if (this.ViewState["_Zlbz"] != null)
                    return this.ViewState["_Zlbz"].ToString();
                return "";
            }
            return _Zlbz;
        }
        set
        {
            _Zlbz = value;
            this.ViewState["_Zlbz"] = value;
        }
    }

    /// <summary>
    /// 工期
    /// </summary>
    public string Gq
    {
        get
        {
            if (_Gq == "")
            {
                if (this.ViewState["_Gq"] != null)
                    return this.ViewState["_Gq"].ToString();
                return "";
            }
            return _Gq;
        }
        set
        {
            _Gq = value;
            this.ViewState["_Gq"] = value;
        }
    }

    /// <summary>
    /// 入场条件及总包管理方式
    /// </summary>
    public string Rctj
    {
        get
        {
            if (_Rctj == "")
            {
                if (this.ViewState["_Rctj"] != null)
                    return this.ViewState["_Rctj"].ToString();
                return "";
            }
            return _Rctj;
        }
        set
        {
            _Rctj = value;
            this.ViewState["_Rctj"] = value;
        }
    }

    /// <summary>
    /// 保修及售后服务

    /// </summary>
    public string Shfw
    {
        get
        {
            if (_Shfw == "")
            {
                if (this.ViewState["_Shfw"] != null)
                    return this.ViewState["_Shfw"].ToString();
                return "";
            }
            return _Shfw;
        }
        set
        {
            _Shfw = value;
            this.ViewState["_Shfw"] = value;
        }
    }
    /// <summary>
    /// 附件1
    /// </summary>
    public ModuleState SetAttachList1
    {
        set
        {
            if (value == ModuleState.Sightless)
            {
                AttachMentList1.Visible = false;
            }
        }
    }
    /// <summary>
    /// 附件2
    /// </summary>
    public ModuleState SetAttachList2
    {
        set
        {
            if (value == ModuleState.Sightless)
            {
                AttachMentList2.Visible = false;
            }
        }
    }
    /// <summary>
    /// 附件3
    /// </summary>
    public ModuleState SetAttachList3
    {
        set
        {
            if (value == ModuleState.Sightless)
            {
                AttachMentList3.Visible = false;
            }
        }
    }

    /// <summary>
    /// 附件4
    /// </summary>
    public ModuleState SetAttachList4
    {
        set
        {
            if (value == ModuleState.Sightless)
            {
                AttachMentList4.Visible = false;
            }
        }
    }

    /// <summary>
    /// 附件5
    /// </summary>
    public ModuleState SetAttachList5
    {
        set
        {
            if (value == ModuleState.Sightless)
            {
                AttachMentList5.Visible = false;
            }
        }
    }

    public ModuleState SetAttachList6
    {
        set
        {
            if (value == ModuleState.Sightless)
            {
                AttachMentList6.Visible = false;
            }
        }
    }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark
    {
        get
        {
            if (_Remark == "")
            {
                if (this.ViewState["_Remark"] != null)
                    return this.ViewState["_Remark"].ToString();
                return "";
            }
            return _Remark;
        }
        set
        {
            _Remark = value;
            this.ViewState["_Remark"] = value;
        }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        AttachMentList1.AttachMentType = "BiddingConditionFile1";
        AttachMentAdd1.AttachMentType = "BiddingConditionFile1";
        AttachMentList2.AttachMentType = "BiddingConditionFile2";
        AttachMentAdd2.AttachMentType = "BiddingConditionFile2";
        AttachMentList3.AttachMentType = "BiddingConditionFile3";
        AttachMentAdd3.AttachMentType = "BiddingConditionFile3";
        AttachMentList4.AttachMentType = "BiddingConditionFile4";
        AttachMentAdd4.AttachMentType = "BiddingConditionFile4";
        AttachMentList5.AttachMentType = "BiddingConditionFile5";
        AttachMentAdd5.AttachMentType = "BiddingConditionFile5";
        AttachMentList6.AttachMentType = "BiddingConditionFile6";
        AttachMentAdd6.AttachMentType = "BiddingConditionFile6";
        if (this.BiddingConditionFileCode != "")
        {

            AttachMentList1.MasterCode = this.BiddingConditionFileCode;
            AttachMentAdd1.MasterCode = this.BiddingConditionFileCode;
            AttachMentList2.MasterCode = this.BiddingConditionFileCode;
            AttachMentAdd2.MasterCode = this.BiddingConditionFileCode;
            AttachMentList3.MasterCode = this.BiddingConditionFileCode;
            AttachMentAdd3.MasterCode = this.BiddingConditionFileCode;
            AttachMentList4.MasterCode = this.BiddingConditionFileCode;
            AttachMentAdd4.MasterCode = this.BiddingConditionFileCode;
            AttachMentList5.MasterCode = this.BiddingConditionFileCode;
            AttachMentAdd5.MasterCode = this.BiddingConditionFileCode;
            AttachMentList6.MasterCode = this.BiddingConditionFileCode;
            AttachMentAdd6.MasterCode = this.BiddingConditionFileCode;
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

          RmsPM.BLL.BiddingConditionFile cBiddingConditionFile = new RmsPM.BLL.BiddingConditionFile();
        if (this.ApplicationCode != "")
        {
            this.BiddingConditionFileCode = this.ApplicationCode;
        }
        else if (this.BiddingConditionFileCode != "")
        {
            this.ApplicationCode = this.BiddingConditionFileCode;
        }


        if (this.ApplicationCode != "")
        {
            cBiddingConditionFile.BiddingConditionFileCode = this.ApplicationCode;
            EntityData entitydata = RmsPM.BLL.BiddingConditionFile.GetBiddingConditionFileByCode(this.ApplicationCode);

            if (entitydata.HasRecord())
            {
                
                this.BiddingConditionFileState = entitydata.GetString("state");
                this.BiddingCode = entitydata.GetString("BiddingCode");
                if (Flag)
                {
                 
                    RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
                    cBidding.BiddingCode = this.BiddingCode;
                    string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + this.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "','ZD_BiddingConditionFile3')>" + cBidding.Title + "</a>";
                    //this.tdBiddingTitle.InnerHtml = cBidding.Title;
                    this.txtBiddingTitle.InnerHtml = LinkUrl;
                    this.txtProjectName.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode);


                    this.TxtBiddingConditionFileName.Value = entitydata.GetString("name");
                    this.TxtNumber.Value = entitydata.GetString("BiddingConditionFileNumber");
                    this.tdBiddingConditionFileState1.InnerHtml = RmsPM.BLL.BiddingConditionFile.GetBiddingConditionFileStatusName(BiddingConditionFileState);

                    this.TxtZBFW.Text = StringRule.FormartOutput(cBiddingConditionFile.Zbfw);
                    this.TxtJSYQ.Text = StringRule.FormartOutput(cBiddingConditionFile.Jsxq);
                    this.TxtZLBZ.Text = StringRule.FormartOutput(cBiddingConditionFile.Zlbz);
                    this.TxtGQ.Text = StringRule.FormartOutput(cBiddingConditionFile.Gq);
                    this.TxtRCTJ.Text = StringRule.FormartOutput(cBiddingConditionFile.Rctj);
                    this.TxtSHFW.Text = StringRule.FormartOutput(cBiddingConditionFile.Shfw);

                }
                else
                {
                    RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
                    cBidding.BiddingCode = this.BiddingCode;
                    string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + this.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "','ZD_BiddingConditionFile1')>" + cBidding.Title + "</a>";
                    //this.tdBiddingTitle.InnerHtml = cBidding.Title;
                    this.tdBiddingTitle.InnerHtml = LinkUrl;
                    this.tdProjectName.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode);


                    this.TdBiddingConditionFileName.InnerHtml = entitydata.GetString("name");
                    this.TdNumber.InnerHtml = entitydata.GetString("BiddingConditionFileNumber");
                    this.tdBiddingConditionFileState2.InnerHtml = RmsPM.BLL.BiddingConditionFile.GetBiddingConditionFileStatusName(BiddingConditionFileState);

                    this.lblZBFW.Text = StringRule.FormartOutput(cBiddingConditionFile.Zbfw).Replace("\n", "<br />");
                    this.lblJSYQ.Text = StringRule.FormartOutput(cBiddingConditionFile.Jsxq).Replace("\n", "<br />");
                    this.lblZLBZ.Text = StringRule.FormartOutput(cBiddingConditionFile.Zlbz).Replace("\n", "<br />");
                    this.lblGQ.Text = StringRule.FormartOutput(cBiddingConditionFile.Gq).Replace("\n", "<br />");
                    this.lblRCTJ.Text = StringRule.FormartOutput(cBiddingConditionFile.Rctj).Replace("\n", "<br />");
                    this.lblSHFW.Text = StringRule.FormartOutput(cBiddingConditionFile.Shfw).Replace("\n", "<br />");
                }
            }
            this.ApplicationTitle = cBiddingConditionFile.Name;
            entitydata.Dispose();
        }
        else
        {
            RmsPM.BLL.Bidding cBidding = new RmsPM.BLL.Bidding();
            cBidding.BiddingCode = this.BiddingCode;
            string LinkUrl = "<a onclick=OpenLargeWindow('../BiddingManage/biddingmodify.aspx?BiddingCode=" + this.BiddingCode + "&State=edit&ProjectCode=" + cBidding.ProjectCode + "','ZD_BiddingConditionFile2')>" + cBidding.Title + "</a>";
            //this.tdBiddingTitle.InnerHtml = cBidding.Title;
            this.txtBiddingTitle.InnerHtml = LinkUrl;
            this.txtProjectName.InnerHtml = RmsPM.BLL.ProjectRule.GetProjectName(cBidding.ProjectCode);
        }


    }

    /// ****************************************************************************
    /// <summary>
    /// 提交数据
    /// </summary>
    /// ****************************************************************************
    public override string SubmitData()
    {
        string Errmsg = "";
        try
        {

            if (this.TxtBiddingConditionFileName.Value == "")
            {
                Errmsg = "招标文件名称不允许为空";
                return Errmsg;
            }
            else if (this.TxtNumber.Value == "")
            {
                Errmsg = "招标文件编号不允许为空";
                return Errmsg;
            }

           

            RmsPM.BLL.BiddingConditionFile cBiddingConditionFile = new RmsPM.BLL.BiddingConditionFile();

            cBiddingConditionFile.BiddingConditionFileCode = this.BiddingConditionFileCode + "";

            cBiddingConditionFile.BiddingCode = this.BiddingCode + "";
            cBiddingConditionFile.BiddingConditionFileNumber = this.TxtNumber.Value;
            cBiddingConditionFile.Name = this.TxtBiddingConditionFileName.Value;
            this.ApplicationTitle = this.TxtBiddingConditionFileName.Value;

            cBiddingConditionFile.Zbfw = this.TxtZBFW.Text;
            cBiddingConditionFile.Jsxq = this.TxtJSYQ.Text;
            cBiddingConditionFile.Zlbz = this.TxtZLBZ.Text;
            cBiddingConditionFile.Gq = this.TxtGQ.Text;
            cBiddingConditionFile.Rctj = this.TxtRCTJ.Text;
            cBiddingConditionFile.Shfw = this.TxtSHFW.Text;
            //cBiddingConditionFile.State = "0";


            cBiddingConditionFile.BiddingConditionFileAdd();

            this.BiddingConditionFileCode = cBiddingConditionFile.BiddingConditionFileCode;
            if (this.ApplicationCode != "")
            {
                this.BiddingConditionFileCode = this.ApplicationCode;
            }
            else if (this.BiddingConditionFileCode != "")
            {
                this.ApplicationCode = this.BiddingConditionFileCode;
            }

            this.AttachMentAdd1.SaveAttachMent(this.BiddingConditionFileCode);
            this.AttachMentAdd2.SaveAttachMent(this.BiddingConditionFileCode);
            this.AttachMentAdd3.SaveAttachMent(this.BiddingConditionFileCode);
            this.AttachMentAdd4.SaveAttachMent(this.BiddingConditionFileCode);
            this.AttachMentAdd5.SaveAttachMent(this.BiddingConditionFileCode);
            this.AttachMentAdd6.SaveAttachMent(this.BiddingConditionFileCode);
            return Errmsg;

        }
        catch (System.Exception ec)
        {
            Errmsg = ec.Message;
            return Errmsg;
        }
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

            if (pm_sOpinionConfirm != "")
            {



                switch (pm_sOpinionConfirm)
                {
                    case "Approve":


                        RmsPM.BLL.BiddingConditionFile.BiddingConditionFileStatusChange(this.BiddingConditionFileCode, 0);

                        break;
                    case "Reject":


                        RmsPM.BLL.BiddingConditionFile.BiddingConditionFileStatusChange(this.BiddingConditionFileCode, 1);

                        break;
                    case "Unknow":
                        ErrMsg = "请选择评审结果！";
                        break;
                    default:
                        ErrMsg = "请选择评审结果！";
                        break;
                }


                if (ErrMsg != "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));
                    return false;
                }






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

            RmsPM.BLL.BiddingConditionFile.BiddingConditionFileStatusChange(dao, this.BiddingConditionFileCode, 7);

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
            RmsPM.BLL.BiddingConditionFile.BiddingConditionFileStatusChange(this.BiddingConditionFileCode, 1);

            return ErrMsg;
        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "恢复业务数据状态出错：" + ex.Message));
            throw ex;
        }
    }
    

    

 
}
