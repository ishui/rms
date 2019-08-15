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


using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;
using RmsPM.BLL;
using RmsPM.Web;
using Rms.Web;

public partial class BiddingManage_BiddingMessageInfo : PageBase
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
    /// 中标通知书评审页面

    /// </summary>
    public string BiddingMessageManageUrl
    {
        get
        {
            return RmsPM.BLL.WorkFlowRule.GetProcedureURLByCode(RmsPM.BLL.WorkFlowRule.GetProcedureCodeByName("中标通知书评审", this.ProjectCode));

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.InitPage();
        }
        string BiddingMessageCode = Request["BiddingMessageCode"] + "";

        AttachMentList1.AttachMentType = "BiddingMessageModify";
        if (BiddingMessageCode != "")
        {
            AttachMentList1.MasterCode = BiddingMessageCode;
        }
    }

    /// ****************************************************************************
    /// /// <summary>
    /// 初始化

    /// </summary>
    /// ****************************************************************************
    protected void InitPage()
    {
        if (!this.user.HasRight("210502"))
        {
            Response.Redirect("../RejectAccess.aspx");
            Response.End();
        }

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
            this.BiddingCode = cBiddingMessage.BiddingCode;

            this.tdProjectCode.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ProjectRule.GetProjectName(cBiddingMessage.ProjectCode);
            this.tdContractNember.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + cBiddingMessage.ContractNember;
            this.tdContractName.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + cBiddingMessage.ContractName;
            this.tdContractType.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ContractRule.GetContractTypeName(cBiddingMessage.ContractType);
            this.tdSupplier.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.ProjectRule.GetSupplierName(cBiddingMessage.Supplier);
            this.tdBiddingDtl.InnerHtml = this.GetBiddingDtlListStr(cBiddingMessage.BiddingReturnCode, cBiddingMessage.Supplier);
            this.tdContractDate.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + cBiddingMessage.ContractDate;
            this.tdRemark.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + cBiddingMessage.Remark.Replace("\n", "<br>");

            string StateName=cBiddingMessage.State=="0"?"已审":"未审";
            this.tdState.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;" + StateName;
            switch (cBiddingMessage.State)
            {
                case "0":
                    this.btnMessage.Visible = false;
                    break;
            }
        }
        else
        {
            Response.Write(JavaScript.ScriptStart);
            Response.Write("window.alert('中标通知书不存在！');");
            Response.Write("window.opener=null;window.close();");
            Response.Write(JavaScript.ScriptEnd);

            return;
        }

        this.WorkFlowList1.ProcedureNameAndApplicationCodeList = GetWorkFlowListString();
        this.WorkFlowList1.DataBound();


        this.btnMessage.Attributes["onclick"] = "javascript:BiddingMessage('" + this.ApplicationCode + "');return false;";

    }
    private string GetWorkFlowListString()
    {
        string ListString = "";

        RmsPM.BLL.BiddingMessage bm = new RmsPM.BLL.BiddingMessage();
        bm.BiddingMessageCode = this.ApplicationCode;
        DataTable dtm = bm.GetBiddingMessages();
        if(dtm.Rows.Count!=0)
            ListString += "'中标通知书评审" + this.ApplicationCode + "'";
       
        return ListString;
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

            returnstr += "&nbsp;&nbsp;&nbsp;&nbsp;" + RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode(br.BiddingDtlCode) + " 报价：" + br.Money + "<br />";

        }
        if (returnstr == "")
            returnstr = "&nbsp;";
        return returnstr;
    }

  

   
}

