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
using RmsPM.Web;
using Rms.Web;



public partial class BiddingManage_BiddingMessageApproveModify : PageBase
{
    private string _BiddingCode = "";

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


    private string _ApplicationCode = "";

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
    /// 项目代码
    /// </summary>
    public string ProjectCode
    {
        get
        {
            if (this.ViewState["_ProjectCode"] == null)
                return "";
            return this.ViewState["_ProjectCode"].ToString();
        }
        set
        {
            this.ViewState["_ProjectCode"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.InitPage();
        }
        string BiddingMessageCode = Request["BiddingMessageCode"] + "";

        AttachMentAdd1.AttachMentType = "BiddingMessageModify";
        if (BiddingMessageCode != "")
        {
            AttachMentAdd1.MasterCode = BiddingMessageCode;
        }
    }

    /// ****************************************************************************
    /// /// <summary>
    /// 初始化

    /// </summary>
    /// ****************************************************************************
     protected void InitPage()
    {
        this.ViewState["BiddingReturnCodeStr"] = "";
        string BiddingMessageCode = Request["BiddingMessageCode"] + "";
        this.BiddingCode = Request["BiddingCode"] + "";

        this.ApplicationCode = BiddingMessageCode;

        if (BiddingMessageCode != "")
        {
            RmsPM.BLL.BiddingMessage cBiddingMessage = new RmsPM.BLL.BiddingMessage();
            cBiddingMessage.BiddingMessageCode = BiddingMessageCode;
           

            RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
            bidding.BiddingCode = cBiddingMessage.BiddingCode;

            this.txtProjectCode.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ProjectRule.GetProjectName(cBiddingMessage.ProjectCode);
            this.ProjectCode = cBiddingMessage.ProjectCode;
            this.txtContractNember.Value = cBiddingMessage.ContractNember;
            this.txtContractName.Value = cBiddingMessage.ContractName;
            this.txtContractType.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ContractRule.GetContractTypeName(cBiddingMessage.ContractType);
          
            this.txtContractDate.Value = cBiddingMessage.ContractDate;
            this.txtRemark.Value = cBiddingMessage.Remark;


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
        else if (BiddingCode != "")
        {
            RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
            bidding.BiddingCode = BiddingCode;
         
            this.txtProjectCode.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ProjectRule.GetProjectName(bidding.ProjectCode);
            this.ProjectCode = bidding.ProjectCode;
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
                this.txtContractNember.Value = drw[0]["Number"].ToString() + "-" + CNnum;
        }
        else
        {
            Response.Write(JavaScript.ScriptStart);
            Response.Write("window.alert('招标计划不存在！');");

            Response.Write("window.opener=null;window.close();");
            Response.Write(JavaScript.ScriptEnd);
          
            return;
        }


        
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
            li.Text += " 报价：" + dr["Money"].ToString();
            this.CheckBoxList1.Items.Add(li);
        }
        dt = bidding.GetBiddingReturn();
        foreach (DataRow dr in dt.Select("BiddingReturnCode in (" + BiddingReturnCodeStr + "'') and flag='1' and SupplierCode='" + DropSupplier.SelectedValue + "'"))
        {
            RmsPM.BLL.BiddingReturn br = new RmsPM.BLL.BiddingReturn();
            br.BiddingReturnCode = dr["BiddingReturnCode"].ToString();
            ListItem li = new ListItem(RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode(br.BiddingDtlCode), dr["BiddingReturnCode"].ToString());
            li.Text += " 报价：" + dr["Money"].ToString();
            li.Selected = true;
            this.CheckBoxList1.Items.Add(li);
        }
    }

    protected void DropSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        BoundBiddingDtl(this.ViewState["BiddingReturnCodeStr"].ToString());
    }

    protected void SaveToolsButton_ServerClick(object sender, EventArgs e)
    {
        string BiddingReturnCode = "";
        string BiddingDtlCode = "";
        decimal tempMoney = 0;
        if (this.DropSupplier.Items.Count <= 0)
        {
            Response.Write(JavaScript.ScriptStart);
            Response.Write("window.alert('不存在签约单位！');");
            Response.Write("window.opener=null;window.close();");
            Response.Write(JavaScript.ScriptEnd);
            return;
        }

        foreach (ListItem li in this.CheckBoxList1.Items)
        {
            if (li.Selected)
            {
                RmsPM.BLL.BiddingReturn br = new RmsPM.BLL.BiddingReturn();
                br.BiddingReturnCode = li.Value;
                BiddingReturnCode += "'" + br.BiddingReturnCode + "',";
                BiddingDtlCode += "'" + br.BiddingDtlCode + "',";

                if (System.Convert.ToDecimal(br.Money) > tempMoney)
                {
                    this.ViewState["MaxMoney"] = br.Money;
                    tempMoney = System.Convert.ToDecimal(br.Money);
                }
            }
        }

        RmsPM.BLL.Bidding bidding = new RmsPM.BLL.Bidding();
      
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
        cBiddingMessage.Remark = this.txtRemark.Value;
        cBiddingMessage.CreateDate = DateTime.Now.ToShortDateString();
        cBiddingMessage.CreateUser = "";
        cBiddingMessage.State = "1";
        cBiddingMessage.Flag = "0";
        cBiddingMessage.BiddingReturnCode = BiddingReturnCode;
        cBiddingMessage.BiddingDtlCode = BiddingDtlCode;
      
        cBiddingMessage.BiddingMessageSubmit();
        if (this.ApplicationCode == "")
            this.ApplicationCode = cBiddingMessage.BiddingMessageCode;
        //附件
        this.AttachMentAdd1.SaveAttachMent(this.ApplicationCode);



        Response.Write(JavaScript.ScriptStart);
        Response.Write("window.opener.location = window.opener.location;");
        Response.Write("window.close();");
        Response.Write(JavaScript.ScriptEnd);
    }
}
