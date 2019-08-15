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
using RmsPM.Web;
using Rms.ORMap;
using RmsPM.DAL;
using RmsPM.DAL.QueryStrategy;

public partial class Contract_ContractPaymentList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ProjectCode"] = Request["ProjectCode"] + "";

            string contractCode = Request["ContractCode"] + "";
            ArrayList ar = user.GetResourceRight(contractCode, "Contract");

            if (!ar.Contains("050101"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }

            IniPage();
            LoadData();
        }
    }

    protected void IniPage()
    { 
    }

    protected void LoadData()
    {
        string ud_sProjectCode = Request["ProjectCode"] + "";
        string ud_sContractCode = Request["ContractCode"] + "";
        string ud_sIssue = Request["Issue"] + "";

        PaymentItemStrategyBuilder PISB = new PaymentItemStrategyBuilder();

        if (ud_sContractCode != "")
        {
            PISB.AddStrategy(new Strategy(PaymentItemStrategyName.ContractCode, ud_sContractCode));
        }
        else
        {
            return;
        }

        if (ud_sProjectCode != "")
        {
            PISB.AddStrategy(new Strategy(PaymentItemStrategyName.ProjectCode, ud_sProjectCode));
        }

        if (ud_sIssue != "")
        {
            PISB.AddStrategy(new Strategy(PaymentItemStrategyName.MaxIssue, ud_sIssue.ToString()));
        }

        PISB.AddOrder("Issue", true);

        string sql = PISB.BuildQueryViewString();

        QueryAgent qa = new QueryAgent();
        EntityData entity = qa.FillEntityData("PaymentItem", sql);
        qa.Dispose();

        EntityData entityCon = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ud_sContractCode);


        this.lblProjectName.Text = RmsPM.BLL.ProjectRule.GetProjectName(entityCon.GetString("ProjectCode"));
        this.lblContractDate.Text = entityCon.GetDateTimeOnlyDate("ContractDate") == "" ? entityCon.GetDateTimeOnlyDate("CreateDate") : entityCon.GetDateTimeOnlyDate("ContractDate");
        this.lblOriginalMoney.Text = entityCon.GetDecimal("OriginalMoney").ToString("N");

        this.lblContractName.Text = entityCon.GetString("ContractName");
        this.lblContractID.Text = entityCon.GetString("ContractID");
        this.lblSupplierName.Text = RmsPM.BLL.ProjectRule.GetSupplierName(entityCon.GetString("SupplierCode"));

        this.lblWorkStartDate.Text = entityCon.GetDateTimeOnlyDate("WorkStartDate");
        this.lblWorkEndDate.Text = entityCon.GetDateTimeOnlyDate("WorkEndDate");

        this.lblPerCash1.Text = (entityCon.GetDecimal("PerCash1") / 100).ToString("P");
        this.lblPerCash2.Text = (entityCon.GetDecimal("PerCash2") / 100).ToString("P");
        this.lblPerCash3.Text = (entityCon.GetDecimal("PerCash3") / 100).ToString("P");

        decimal[] ud_deChangeMoney = new decimal[2];
        decimal ud_deTotalMoney = entityCon.GetDecimal("TotalMoney");

        ViewState["TotalMoney"] = ud_deTotalMoney.ToString();
        
        ud_deChangeMoney[0] = decimal.Zero;
        ud_deChangeMoney[1] = decimal.Zero;

        int i = 0;

        foreach (DataRow ud_drChange in entityCon.Tables["ContractChange"].Select("Status=0", "ChangeDate", DataViewRowState.CurrentRows))
        {
            if (i <= 1)
            {
                ud_deChangeMoney[i] = (decimal)ud_drChange["ChangeMoney"];
                i++;
            }
            else 
            {
                break;
            }
        }

        if (ud_deChangeMoney[0] != decimal.Zero)
        {
            this.lblChangeMoney1.Text = ud_deChangeMoney[0].ToString("N");
        }

        if (ud_deChangeMoney[1] != decimal.Zero)
        {
            this.lblChangeMoney2.Text = ud_deChangeMoney[1].ToString("N");
        }

        this.lblTotalMoney.Text = entityCon.GetDecimal("TotalMoney").ToString("N");

        gvPaymentItemBind(entity.Tables["PaymentItem"]);


        
    }

    protected void gvPaymentItemBind(DataTable pm_dtPaymentItem)
    {
        string[] arrField = { "ItemCash0", "ItemCash1", "ItemCash2", "ItemCash3", "ItemCash9", "ItemCash" };
        decimal[] arrSum = RmsPM.BLL.MathRule.SumColumn(pm_dtPaymentItem, arrField);

        this.gvPaymentItem.Columns[3].FooterText = arrSum[0].ToString("N");
        this.gvPaymentItem.Columns[4].FooterText = arrSum[1].ToString("N");
        this.gvPaymentItem.Columns[5].FooterText = arrSum[2].ToString("N");
        this.gvPaymentItem.Columns[6].FooterText = arrSum[3].ToString("N");
        this.gvPaymentItem.Columns[7].FooterText = arrSum[4].ToString("N");
        this.gvPaymentItem.Columns[8].FooterText = arrSum[5].ToString("N");
        this.gvPaymentItem.Columns[9].FooterText = (arrSum[5] / decimal.Parse(ViewState["TotalMoney"].ToString())).ToString("P");


        this.gvPaymentItem.DataSource = pm_dtPaymentItem;
        this.gvPaymentItem.DataBind();
    }




}
