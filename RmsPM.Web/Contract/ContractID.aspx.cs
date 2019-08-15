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

public partial class Contract_ContractID : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["ProjectCode"] = Request["ProjectCode"] + "";

            string ud_sContractCode = Request["ContractCode"] + "";
            ArrayList ar = user.GetResourceRight(ud_sContractCode, "Contract");
            if (!ar.Contains("050101"))
            {
                Response.Redirect("../RejectAccess.aspx");
                Response.End();
            }

            LoadData();
        }
    }

    private void LoadData()
    {
        string ud_sContractCode = Request["ContractCode"] + "";
        string ud_sProjectCode = Request["ProjectCode"] + "";

        try
        {

            EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ud_sContractCode);

            if (entity.HasRecord())
            {
                ViewState["ProjectCode"] = entity.GetString("ProjectCode");

                this.lblContractName.Text = entity.GetString("ContractName");
                this.lblContractStatus.Text = RmsPM.BLL.ContractRule.GetContractStatusName(entity.GetInt("status").ToString());

                this.txtContractID.Value = entity.GetString("ContractID");


                ViewState["_TotalMoney"] = entity.GetDecimal("TotalMoney");

                BindCostDataGrid(entity);
            }
        }
        catch(Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "加载合同数据失败。");
            Response.Write(Rms.Web.JavaScript.Alert(true, "加载合同数据失败：" + ex.Message));

        }
    }


    /// <summary>
    /// 读取合同款项明细
    /// </summary>
    private void BindCostDataGrid(EntityData entity)
    {
        ViewState["_SumCostMoney"] = Decimal.Zero;
        ViewState["_SumAHMoney"] = Decimal.Zero;
        ViewState["_SumAPMoney"] = Decimal.Zero;
        ViewState["_SumUPMoney"] = Decimal.Zero;

        DataTable ud_dtCostList = entity.Tables["ContractCostCash"].Clone();

        foreach (DataColumn dc in entity.Tables["ContractCost"].Columns)
        {
            switch (dc.ColumnName)
            {
                case "CostCode":
                case "CostBudgetDtlCode":
                case "CostBudgetSetCode":
                case "PBSType":
                case "PBSCode":
                    ud_dtCostList.Columns.Add(dc.ColumnName, dc.DataType);
                    break;
            }
        }

        foreach (DataRow drCost in entity.Tables["ContractCost"].Rows)
        {
            string ud_sCashFilter = string.Format("ContractCostCode='{0}'", drCost["ContractCostCode"].ToString());

            foreach (DataRow drCash in entity.Tables["ContractCostCash"].Select(ud_sCashFilter))
            {
                DataRow drNew = ud_dtCostList.NewRow();

                foreach (DataColumn dc in entity.Tables["ContractCost"].Columns)
                {
                    switch (dc.ColumnName)
                    {
                        case "CostCode":
                        case "CostBudgetDtlCode":
                        case "CostBudgetSetCode":
                        case "PBSType":
                        case "PBSCode":
                            drNew[dc.ColumnName] = drCost[dc.ColumnName];
                            break;
                    }
                }

                foreach (DataColumn dc in entity.Tables["ContractCostCash"].Columns)
                {
                    drNew[dc.ColumnName] = drCash[dc.ColumnName];
                }

                ud_dtCostList.Rows.Add(drNew);
            }
        }


        dgCostList.DataSource = ud_dtCostList;
        dgCostList.DataBind();
    }


    private void dgCostList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        decimal ud_deSumCostMoney = ViewState["_SumCostMoney"] == null ? Decimal.Zero : (decimal)ViewState["_SumCostMoney"];
        decimal ud_deSumAHMoney = ViewState["_SumAHMoney"] == null ? Decimal.Zero : (decimal)ViewState["_SumAHMoney"];
        decimal ud_deSumAPMoney = ViewState["_SumAPMoney"] == null ? Decimal.Zero : (decimal)ViewState["_SumAPMoney"];
        decimal ud_deSumUPMoney = ViewState["_SumUPMoney"] == null ? Decimal.Zero : (decimal)ViewState["_SumUPMoney"];
        decimal ud_deTotalMoney = RmsPM.BLL.ConvertRule.ToDecimal(ViewState["_TotalMoney"].ToString());

        switch (e.Item.ItemType)
        {
            case ListItemType.AlternatingItem:
            case ListItemType.Item:
                RmsPM.Web.UserControls.InputCostBudgetDtl ud_ucCostBudgetDtl = (RmsPM.Web.UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl");
                RmsPM.Web.UserControls.ExchangeRateControl ud_ucExchangeRate = (RmsPM.Web.UserControls.ExchangeRateControl)e.Item.FindControl("ucExchangeRate");
                Label ud_lblPBSName = (Label)e.Item.FindControl("lblPBSName");
                Label ud_lblCostName = (Label)e.Item.FindControl("lblCostName");
                Label ud_lblAHMoney = (Label)e.Item.FindControl("lblAHMoney");
                Label ud_lblAHMoneyPer = (Label)e.Item.FindControl("lblAHMoneyPer");
                Label ud_lblAPMoney = (Label)e.Item.FindControl("lblAPMoney");
                Label ud_lblUPMoney = (Label)e.Item.FindControl("lblUPMoney");

                DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

                string ud_sProjectCode = Request["ProjectCode"] + "";
                string ud_sContractCode = Request["ContractCode"] + "";
                string ud_sContractCostCode = ud_drvItem["ContractCostCode"].ToString();
                string ud_sContractCostCashCode = ud_drvItem["ContractCostCashCode"].ToString();

                ud_ucCostBudgetDtl.CostBudgetSetCode = ud_drvItem["CostBudgetSetCode"].ToString();
                ud_ucCostBudgetDtl.CostCode = ud_drvItem["CostCode"].ToString();

                ud_lblCostName.Text = ud_ucCostBudgetDtl.CostName;
                ud_lblPBSName.Text = ud_ucCostBudgetDtl.PBSName;

                // 已付和未付款
                decimal ud_deCash = RmsPM.BLL.ConvertRule.ToDecimal(ud_drvItem["Cash"]);
                decimal ud_deAHCash = RmsPM.BLL.CBSRule.GetAHCash("", "", "", ud_sContractCode, "1", ud_sContractCostCashCode);
                decimal ud_deExchangeRate =RmsPM.BLL.ConvertRule.ToDecimal(ud_drvItem["ExchangeRate"]);
                decimal ud_deAHMoney = ud_deAHCash * ud_deExchangeRate;
                decimal ud_deMoney = ud_deCash * ud_deExchangeRate;
                float ud_fAHMoneyPey = ud_deTotalMoney == Decimal.Zero ? 0 : (float)(ud_deAHMoney / ud_deTotalMoney);
                decimal ud_deAPMoney =RmsPM.BLL.CBSRule.GetAPCash(ud_sContractCode, ud_sContractCostCashCode);
                decimal ud_deUPMoney = ud_deMoney - ud_deAPMoney;

                ud_ucExchangeRate.Cash = ud_deCash;
                ud_ucExchangeRate.ExchangeRate = ud_deExchangeRate;
                ud_ucExchangeRate.MoneyType = ud_drvItem["MoneyType"].ToString();
                ud_ucExchangeRate.EditMode = false;
                ud_ucExchangeRate.BindControl();


                ud_lblAHMoney.Text = ud_deAHMoney.ToString("N");
                ud_lblAHMoneyPer.Text = ud_fAHMoneyPey.ToString("#0.00%");

                ud_lblAPMoney.Text = ud_deAPMoney.ToString("N");
                ud_lblUPMoney.Text = ud_deUPMoney.ToString("N");

                ud_deSumAHMoney += ud_deAHMoney;
                ud_deSumAPMoney += ud_deAPMoney;
                ud_deSumCostMoney += ud_deMoney;
                ud_deSumUPMoney += ud_deUPMoney;

                ViewState["_SumAHMoney"] = ud_deSumAHMoney;
                ViewState["_SumAPMoney"] = ud_deSumAPMoney;
                ViewState["_SumCostMoney"] = ud_deSumCostMoney;
                ViewState["_SumUPMoney"] = ud_deSumUPMoney;

                break;
            case ListItemType.Footer:
                Label ud_lblSumAHMoney = (Label)e.Item.FindControl("lblSumAHMoney");
                Label ud_lblSumAHMoneyPer = (Label)e.Item.FindControl("lblSumAHMoneyPer");
                Label ud_lblSumAPMoney = (Label)e.Item.FindControl("lblSumAPMoney");
                Label ud_lblSumUPMoney = (Label)e.Item.FindControl("lblSumUPMoney");
                Label ud_lblSumCostMoney = (Label)e.Item.FindControl("lblSumCostMoney");

                float ud_fSumAHMoneyPey = ud_deTotalMoney == Decimal.Zero ? 0 : (float)(ud_deSumAHMoney / ud_deTotalMoney);

                ud_lblSumAHMoney.Text = ud_deSumAHMoney.ToString("N");
                ud_lblSumAHMoneyPer.Text = ud_fSumAHMoneyPey.ToString("#0.00%");
                ud_lblSumAPMoney.Text = ud_deSumAPMoney.ToString("N");
                ud_lblSumUPMoney.Text = ud_deSumUPMoney.ToString("N");
                ud_lblSumCostMoney.Text = ud_deSumCostMoney.ToString("N");
                break;


        }
    }

    override protected void InitEventHandler()
    {
        this.dgCostList.ItemDataBound += new DataGridItemEventHandler(this.dgCostList_ItemDataBound);
    }

    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        string ud_sContractCode = Request["ContractCode"] + "";
        EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ud_sContractCode);

        foreach (DataRow dr in entity.Tables["Contract"].Select(string.Format("ContractCode = '{0}'", ud_sContractCode), "", DataViewRowState.CurrentRows))
        {
            dr["ContractID"] = txtContractID.Value.Trim();
        }

        RmsPM.DAL.EntityDAO.ContractDAO.UpdateContract(entity);

        GoBack();
    }



    /// <summary>
    /// 返回
    /// </summary>
    private void GoBack()
    {


        Response.Write(Rms.Web.JavaScript.ScriptStart);

        Response.Write("window.opener.location = window.opener.location;");
        Response.Write(Rms.Web.JavaScript.WinClose(false));
        Response.Write(Rms.Web.JavaScript.ScriptEnd);
    }

}
