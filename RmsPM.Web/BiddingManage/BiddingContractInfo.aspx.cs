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
using Rms.Web;


public partial class BiddingManage_BiddingContractInfo : RmsPM.Web.PageBase
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
        string BiddingCode=Request["BiddingCode"]+"";
        string ProjectCode=Request["ProjectCode"]+"";
        string SupplierCode=Request["SupplierCode"]+"";
        string BiddingDtlCode = Request["BiddingDtlCode"] + "";

        RmsPM.BLL.BiddingManage bm = new RmsPM.BLL.BiddingManage();
        bm.BiddingCode = BiddingCode;

        this.lblBiddingName.Text = bm.Title;

        this.lblProjectName.Text = RmsPM.BLL.ProjectRule.GetProjectName(ProjectCode);
        this.lblReturnCompany.Text = RmsPM.BLL.ProjectRule.GetSupplierName(SupplierCode);
        this.lblYear.Text = System.DateTime.Now.Year.ToString();
        this.lblMonth.Text = System.DateTime.Now.Month.ToString();
        this.lblDay.Text = System.DateTime.Now.Day.ToString();

        string BiddingDtlName = "";

        int tempindex = 0;
        foreach (string BiddingTempDtlCode in BiddingDtlCode.Split(','))
        {
            tempindex++;
            if (BiddingTempDtlCode == "")
                continue;
            if (tempindex != BiddingDtlCode.Split(',').Length-1)
                BiddingDtlName += RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode(BiddingTempDtlCode) + ",";
            else
                BiddingDtlName += RmsPM.BLL.BiddingDtl.GetBiddingDtlNameByCode(BiddingTempDtlCode);
            
        }

        this.lblDtlBiddingName.Text = BiddingDtlName;

        string company = System.Configuration.ConfigurationManager.AppSettings["PMName"].ToLower();
        switch (company)
        {
            
            case "zhudingpm":
                this.lblCompanyName.Text = "筑鼎";


                break;
            default:
                this.lblCompanyName.Text = "";

                break;
        }


       
    }
}
