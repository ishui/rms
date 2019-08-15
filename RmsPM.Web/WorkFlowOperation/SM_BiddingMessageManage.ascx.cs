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
using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;


public partial class WorkFlowOperation_SM_BiddingMessageManage : WorkFlowOperationBase
{
    /// <summary>
    /// 业务代码
    /// </summary>
    private string _BiddingCode = "";
    private ModuleState _SetAttachList1;
    private ModuleState _SetAttachList2;

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
    /// 
    /// </summary>
    public string MaxMoney
    {
        get
        {
            if (this.ViewState["MaxMoney"] == null)
                return "0";
            return this.ViewState["MaxMoney"].ToString();
        }
    }
    /// <summary>
    /// 业务代码
    /// </summary>
    private string _SupplierCode = "";

    /// <summary>
    /// 业务代码
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
    public string ContractName
    {
        get
        {
            return this.txtContractName.Value;
        }
        set
        {
            this.txtContractName.Value = value;
        }
    }
    public bool AllowContractName
    {
        get
        {
            return txtContractNember.Disabled;
        }
        set
        {
            txtContractName.Disabled = value;
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
    /// 设置合同编号
    /// </summary>
    public string ContractNember
    {
        get
        {
            return this.txtContractNember.Value;
        }
        set
        {
            this.txtContractNember.Value = value;
        }
    }

    /// <summary>
    /// 金额察看状态



    /// </summary>
    public ModuleState MoneyState
    {
        set
        {
            this.ViewState["MoneyState"] = value;
        }
        get
        {
            if (this.ViewState["MoneyState"] == null)
                return ModuleState.Unbeknown;
            return (ModuleState)this.ViewState["MoneyState"];

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
        AttachMentList1.AttachMentType = "BiddingMessageModify";
        AttachMentList1.MasterCode = this.BiddingCode + "5";
        AttachMentAdd1.AttachMentType = "BiddingMessageModify";
        AttachMentAdd1.MasterCode = this.BiddingCode + "5";

    }

    /// ****************************************************************************
    /// <summary>
    /// 组件初始化



    /// </summary>
    /// ****************************************************************************
    override public void InitControl()
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
        this.ViewState["BiddingReturnCodeStr"] = "";
        if (this.ApplicationCode != "")
        {
            RmsPM.BLL.BiddingMessage cBiddingMessage = new RmsPM.BLL.BiddingMessage();
            cBiddingMessage.BiddingMessageCode = this.ApplicationCode;
            this.BiddingCode = cBiddingMessage.BiddingCode;
            this.ProjectCode = cBiddingMessage.ProjectCode;
            this.SupplierCode = cBiddingMessage.Supplier;
            this.ProjectCode = cBiddingMessage.ProjectCode;

            RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
            bidding.BiddingCode = cBiddingMessage.BiddingCode;
            this.ViewState["Money"] = bidding.Money;
            this.ViewState["mostly"] = bidding.Accessory;
            this.ViewState["BiddingType"] = bidding.Type;




            if (Flag)
            {
                this.txtProjectCode.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ProjectRule.GetProjectName(cBiddingMessage.ProjectCode);
                this.txtContractNember.Value = cBiddingMessage.ContractNember;
                this.txtContractName.Value = cBiddingMessage.ContractName;
                this.txtContractType.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ContractRule.GetContractTypeName(cBiddingMessage.ContractType);

                this.txtContractDate.Value = cBiddingMessage.ContractDate;
                //this.TxtRemark.Value = cBiddingMessage.Remark;
                //this.SelectName.Value = cBiddingMessage.AttachUser;


                DataTable dt = bidding.GetBiddingReturnNoMessage();
                foreach (DataRow dr in dt.Select())
                {
                    ListItem li = new ListItem(RmsPM.BLL.ProjectRule.GetSupplierName(dr["SupplierCode"].ToString()), dr["SupplierCode"].ToString());
                    if (!DropSupplier.Items.Contains(li))
                        this.DropSupplier.Items.Add(li);
                }
                ListItem lis = new ListItem(RmsPM.BLL.ProjectRule.GetSupplierName(cBiddingMessage.Supplier), cBiddingMessage.Supplier);
                this.DropSupplier.Items.Add(lis);
                this.DropSupplier.SelectedIndex = this.DropSupplier.Items.IndexOf(this.DropSupplier.Items.FindByValue(cBiddingMessage.Supplier));
                BoundBiddingDtl(cBiddingMessage.BiddingReturnCode);
                this.ViewState["BiddingReturnCodeStr"] = cBiddingMessage.BiddingReturnCode;
            }
            else
            {
                this.tdProjectCode.InnerHtml = "&nbsp;&nbsp;" + RmsPM.BLL.ProjectRule.GetProjectName(cBiddingMessage.ProjectCode);
                this.tdContractNember.InnerHtml = "&nbsp;&nbsp;" + cBiddingMessage.ContractNember;
                this.tdContractName.InnerHtml = "&nbsp;&nbsp;" + cBiddingMessage.ContractName;
                this.tdContractType.InnerHtml = "&nbsp;&nbsp;" + RmsPM.BLL.ContractRule.GetContractTypeName(cBiddingMessage.ContractType);
                this.tdSupplier.InnerHtml = "&nbsp;&nbsp;" + RmsPM.BLL.ProjectRule.GetSupplierName(cBiddingMessage.Supplier);
                this.tdBiddingDtl.InnerHtml = this.GetBiddingDtlListStr(cBiddingMessage.BiddingReturnCode, cBiddingMessage.Supplier);
                this.tdContractDate.InnerHtml = "&nbsp;&nbsp;" + cBiddingMessage.ContractDate;
                //this.tdUserNames.InnerHtml = "&nbsp;&nbsp;" + cBiddingMessage.AttachUser;
                //this.tdRemark.InnerHtml = "&nbsp;&nbsp;" + cBiddingMessage.Remark.Replace("\n", "<br>");
            }
        }
        else
        {
            RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
            bidding.BiddingCode = this.BiddingCode;
            this.ViewState["Money"] = bidding.Money;
            this.ViewState["mostly"] = bidding.Accessory;
            this.ViewState["BiddingType"] = bidding.Type;
            //this.tdbiddingContent.InnerHtml = bidding.Content.Replace("\n", "<br>")+"&nbsp;";
            this.txtProjectCode.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ProjectRule.GetProjectName(bidding.ProjectCode);
            this.txtContractType.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ContractRule.GetContractTypeName(bidding.Type);
            this.ProjectCode = bidding.ProjectCode;
            DataTable dt = bidding.GetBiddingReturnNoMessage();
            foreach (DataRow dr in dt.Select())
            {
                ListItem li = new ListItem(RmsPM.BLL.ProjectRule.GetSupplierName(dr["SupplierCode"].ToString()), dr["SupplierCode"].ToString());
                if (!DropSupplier.Items.Contains(li))
                    this.DropSupplier.Items.Add(li);
            }
            BoundBiddingDtl("");

            this.txtContractName.Value = bidding.Title;
        }
        //构建编号
        RmsPM.BLL.BiddingMessage biddingMessage = new RmsPM.BLL.BiddingMessage();
        biddingMessage.BiddingCode = this.BiddingCode;
        System.Data.DataTable BiddingMessagedt = biddingMessage.GetBiddingMessages() as System.Data.DataTable;
        string CNnum = "0";
        if (BiddingMessagedt != null)
            CNnum = BiddingMessagedt.Rows.Count.ToString();

        RmsPM.BLL.BiddingPrejudication bp = new RmsPM.BLL.BiddingPrejudication();
        bp.BiddingCode = this.BiddingCode;
        DataTable dtp = bp.GetBiddingPrejudications();
        DataRow[] drw = dtp.Select("", "CreateDate desc");
        if (drw.Length > 0)
            this.ContractNember = drw[0]["Number"].ToString() + "-" + CNnum;

        /****************最后报价*****************/
        RmsPM.BLL.BiddingManage bm = new RmsPM.BLL.BiddingManage();
        RmsPM.BLL.BiddingReturn cBiddingReturn = new RmsPM.BLL.BiddingReturn();
        bm.BiddingCode = this.BiddingCode;
        string BiddingEmitCode = bm.GetLastBiddingEmitCode();
        cBiddingReturn.BiddingEmitCode = BiddingEmitCode;
        DataTable dtReturn = cBiddingReturn.GetBiddingReturns();
        decimal tempMoney = 0;
        foreach (DataRow drReturnSupplier in RmsPM.BLL.ConvertRule.GetDistinct(dtReturn, "BiddingDtlCode", "").Select())
        {
            decimal Money = 0;
            foreach (DataRow drReturnAll in dtReturn.Select())
            {
                if (drReturnSupplier["BiddingDtlCode"].ToString() == drReturnAll["BiddingDtlCode"].ToString())
                {
                    if (System.Convert.ToDecimal(drReturnAll["Money"]) > Money)
                    {
                        Money = System.Convert.ToDecimal(drReturnAll["Money"]);
                    }
                }
            }
            tempMoney += Money;
        }
        this.ViewState["MaxMoney"] = tempMoney;
        /***************************************/

    }
    private void BoundBiddingDtl(string BiddingReturnCodeStr)
    {
        this.CheckBoxList1.Items.Clear();
        RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
        bidding.BiddingCode = this.BiddingCode;
        DataTable dt = bidding.GetBiddingReturnNoMessage();
        foreach (DataRow dr in dt.Select("SupplierCode='" + DropSupplier.SelectedValue + "'"))
        {

            ListItem li = new ListItem(RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode(dr["BiddingDtlCode"].ToString()), dr["BiddingReturnCode"].ToString());
            if (this.MoneyState == ModuleState.Eyeable)
            {
                li.Text += " 报价：" + dr["Money"].ToString();
            }
            this.CheckBoxList1.Items.Add(li);
        }
        dt = bidding.GetBiddingReturn();
        foreach (DataRow dr in dt.Select("BiddingReturnCode in (" + BiddingReturnCodeStr + "'') and flag='1' and SupplierCode='" + DropSupplier.SelectedValue + "'"))
        {
            RmsPM.BLL.BiddingReturn br = new RmsPM.BLL.BiddingReturn();
            br.BiddingReturnCode = dr["BiddingReturnCode"].ToString();
            ListItem li = new ListItem(RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode(br.BiddingDtlCode), dr["BiddingReturnCode"].ToString());
            if (this.MoneyState == ModuleState.Eyeable)
            {
                li.Text += " 报价：" + dr["Money"].ToString();
            }
            li.Selected = true;
            this.CheckBoxList1.Items.Add(li);
        }
    }
    private string GetBiddingDtlListStr(string BiddingReturnCodeStr, string SupplierCode)
    {
        string returnstr = "";
        RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
        bidding.BiddingCode = this.BiddingCode;
        DataTable dt = bidding.GetBiddingReturn();
        foreach (DataRow dr in dt.Select("BiddingReturnCode in (" + BiddingReturnCodeStr + "'') and flag='1' and SupplierCode='" + SupplierCode + "'"))
        {
            RmsPM.BLL.BiddingReturn br = new RmsPM.BLL.BiddingReturn();
            br.BiddingReturnCode = dr["BiddingReturnCode"].ToString();
            if (this.MoneyState == ModuleState.Eyeable)
            {
                returnstr += "&nbsp;&nbsp;" + RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode(br.BiddingDtlCode) + " 报价：" + br.Money + "<br />";
            }
            else
            {
                returnstr += "&nbsp;&nbsp;" + RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode(br.BiddingDtlCode) + "<br />";
            }
        }
        if (returnstr == "")
            returnstr = "&nbsp;";
        return returnstr;
    }

    /// ****************************************************************************
    /// <summary>
    /// 提交数据
    /// </summary>
    /// ****************************************************************************
    override public string SubmitData()
    {

        try
        {

            string ErrMsg = "";

            decimal tempMoney = 0;
            string BiddingReturnCode = "";
            string BiddingDtlCode = "";
            ///* 取所选标段金额总和 */
            //foreach (ListItem li in this.CheckBoxList1.Items)
            //{
            //    if (li.Selected)
            //    {
            //        RmsPM.BLL.BiddingReturn br = new RmsPM.BLL.BiddingReturn();
            //        br.BiddingReturnCode = li.Value;
            //        BiddingReturnCode += "'" + br.BiddingReturnCode + "',";
            //        BiddingDtlCode += "'" + br.BiddingDtlCode + "',";
            //        tempMoney += System.Convert.ToDecimal(br.Money);
            //    }
            //}
            //this.ViewState["MaxMoney"] = tempMoney;

            RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
            bidding.dao = this.dao;
            bidding.BiddingCode = this.BiddingCode;

            RmsPM.BLL.BiddingMessage cBiddingMessage = new RmsPM.BLL.BiddingMessage();
            cBiddingMessage.BiddingMessageCode = this.ApplicationCode;
            cBiddingMessage.BiddingCode = this.BiddingCode;
            cBiddingMessage.ProjectCode = this.ProjectCode;
            cBiddingMessage.ContractNember = this.txtContractNember.Value;
            cBiddingMessage.ContractName = this.txtContractName.Value;
            cBiddingMessage.ContractType = bidding.Type;
            cBiddingMessage.Supplier = this.DropSupplier.SelectedValue;
            cBiddingMessage.ContractDate = this.txtContractDate.Value;
            //cBiddingMessage.Remark = this.TxtRemark.Value;
            cBiddingMessage.CreateDate = DateTime.Now.ToShortDateString();
            cBiddingMessage.CreateUser = "";
            //cBiddingMessage.AttachUser = this.SelectName.Value;
            cBiddingMessage.State = "1";
            cBiddingMessage.Flag = "0";
            cBiddingMessage.BiddingReturnCode = BiddingReturnCode;
            cBiddingMessage.BiddingDtlCode = BiddingDtlCode;
            cBiddingMessage.dao = this.dao;
            cBiddingMessage.BiddingMessageSubmit();

            this.ApplicationTitle = this.txtContractName.Value;

            if (this.ApplicationCode == "")
                this.ApplicationCode = cBiddingMessage.BiddingMessageCode;

            return ErrMsg;
        }
        catch (Exception ex)
        {
            
            Response.Write(Rms.Web.JavaScript.Alert(true, "保存出错：" + ex.Message));
            throw ex;
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
                        RmsPM.BLL.BiddingMessage cBiddingMessage = new RmsPM.BLL.BiddingMessage();
                        cBiddingMessage.BiddingMessageCode = this.ApplicationCode;
                        cBiddingMessage.State = "0";
                        cBiddingMessage.dao = this.dao;
                        cBiddingMessage.BiddingMessageSubmit();
                        RmsPM.BLL.BiddingSystem.Set_BiddingState("42", this.BiddingCode);
                        break;
                    case "Reject":
                        RmsPM.BLL.BiddingMessage cBiddingMessage1 = new RmsPM.BLL.BiddingMessage();
                        cBiddingMessage1.BiddingMessageCode = this.ApplicationCode;
                        cBiddingMessage1.State = "1";
                        cBiddingMessage1.dao = this.dao;
                        cBiddingMessage1.BiddingMessageSubmit();
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
            RmsPM.BLL.BiddingMessage cBiddingMessage = new RmsPM.BLL.BiddingMessage();
            cBiddingMessage.BiddingMessageCode = this.ApplicationCode;
            cBiddingMessage.State = "7";
            cBiddingMessage.dao = this.dao;
            cBiddingMessage.BiddingMessageSubmit();
            //RmsPM.BLL.BiddingSystem.Set_BiddingState("41", this.BiddingCode);


            return ErrMsg;
        }
        catch (Exception ex)
        {
          
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

            RmsPM.BLL.BiddingMessage cBiddingMessage = new RmsPM.BLL.BiddingMessage();
            cBiddingMessage.BiddingMessageCode = this.ApplicationCode;
            cBiddingMessage.State = "1";
            cBiddingMessage.dao = this.dao;
            cBiddingMessage.BiddingMessageSubmit();


            return ErrMsg;
        }
        catch (Exception ex)
        {
            Response.Write(Rms.Web.JavaScript.Alert(true, "恢复业务数据状态出错：" + ex.Message));
            throw ex;
        }
    }





    #region Web 窗体设计器生成的代码
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。



        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    ///		设计器支持所需的方法 - 不要使用代码编辑器



    ///		修改此方法的内容。



    /// </summary>
    private void InitializeComponent()
    {

    }
    #endregion
    protected void DropSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        BoundBiddingDtl(this.ViewState["BiddingReturnCodeStr"].ToString());
    }
}

