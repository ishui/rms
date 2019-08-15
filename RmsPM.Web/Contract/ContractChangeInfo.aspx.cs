using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using System.Configuration;

namespace RmsPM.Web.Contract
{
    /// <summary>
    /// ContractChangeInfo 的摘要说明。
    /// </summary>
    public partial class ContractChangeInfo : PageBase
    {



        protected void Page_Load(object sender, System.EventArgs e)
        {
            // 在此处放置用户代码以初始化页面
            if (!IsPostBack)
            {
                string projectCode = Request["ProjectCode"] + "";
                string contractCode = Request["ContractCode"] + "";

                ArrayList ar = user.GetResourceRight(contractCode, "Contract");
                if (!ar.Contains("050101"))	//合同查看
                {
                    Response.Redirect("../RejectAccess.aspx");
                    Response.End();
                }

                IniPage();
                LoadData();

                if (!ar.Contains("050105")) //合同审核
                {
                    this.btnOldCheck.Visible = false;
                }

                if (!ar.Contains("050114")) //提交合同变更审核
                {
                    this.btnCheck.Visible = false;
                }

                if (!ar.Contains("050104")) //合同变更
                {
                    this.btnModify.Visible = false;
                    this.btnDelete.Visible = false;
                }

                if (!ar.Contains("050123"))
                {
                    this.btnCheckDelete.Visible = false;
                }

                if (BLL.WorkFlowRule.GetCaseCountByProcedureNameAndApplicationCode("合同变更审核", this.txtContractChangeCode.Value) > 0)
                {
                    this.btnCheck.Visible = false;
                }

            }
        }

        private void IniPage()
        {
            string contractChangeCode = Request["ContractChangeCode"] + "";
            this.myAttachMentList.AttachMentType = "ContractChangeAttachMent";
            this.myAttachMentList.MasterCode = Request["ContractChangeCode"] + ""; //contractChangeCode;


            ViewState["_AuditingURL"] = BLL.WorkFlowRule.GetProcedureURLByName("合同变更审核");
        }

        private void LoadData()
        {
            string projectCode = Request["ProjectCode"] + "";
            string contractCode = Request["ContractCode"] + "";
            string contractChangeCode = Request["ContractChangeCode"] + "";

            try
            {
                EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);
                int status = -2;

                if (entity.HasRecord())
                {
                    // 合同基本信息
                    entity.SetCurrentTable("Contract");

                    lblProjectName.Text = BLL.ProjectRule.GetProjectName(projectCode);
                    lblContractID.Text = entity.GetString("ContractID");
                    lblContractName.Text = entity.GetString("ContractName");
                    lblSupplierName.Text = BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"));

                    // 显示合同金额
                    ShowContractMoney(entity, contractChangeCode);

                    // 2007.1.25 表头金额处显示外币币种
                    string ForeignMoneyType = BLL.ContractRule.GetForeignMoneyType(entity);
                    if (ForeignMoneyType != "")
                    {
                        this.lblForeignMoneyType.Text = ForeignMoneyType;
                        this.lblForeignMoneyType2.Text = ForeignMoneyType;
                        this.lblForeignMoneyType3.Text = ForeignMoneyType;
                        this.lblForeignMoneyType4.Text = ForeignMoneyType;
                        this.lblForeignMoneyType5.Text = ForeignMoneyType;
                        this.lblForeignMoneyType6.Text = ForeignMoneyType;
                        this.lblForeignMoneyType7.Text = ForeignMoneyType;
                        this.lblForeignMoneyType8.Text = ForeignMoneyType;
                        this.lblForeignMoneyType9.Text = ForeignMoneyType;
                    }

                    //合同变更基本信息
                    entity.SetCurrentTable("ContractChange");



                    foreach (DataRow dr in entity.CurrentTable.Select(String.Format("ContractChangeCode='{0}'", contractChangeCode)))
                    {
                        if (dr["ChangeType"].ToString() == "结算")
                        {
                            Response.Redirect(string.Format("ContracStrikeInfo.aspx?ProjectCode={0}&ContractCode={1}&ContractChangeCode={2}", projectCode, contractCode, contractChangeCode), true);
                        }
                        lblVoucher.Text = dr["Voucher"].ToString();
                        lblChangeId.Text = dr["ContractChangeId"].ToString();
                        lblChangeReason.Text = dr["ChangeReason"].ToString();

                        txtSupplierChangeMoney.Value = dr["SupplierChangeMoney"].ToString();
                        txtConsultantAuditMoney.Value = dr["ConsultantAuditMoney"].ToString();
                        txtProjectAuditMoney.Value = dr["ProjectAuditMoney"].ToString();

                        lblChangeType.Text = dr["ChangeType"].ToString();
                    }

                    //款项明细
                    entity.SetCurrentTable("ContractCostChange");

                    dgCostListBind(entity.CurrentTable, contractChangeCode);

                    foreach (DataRow dr in entity.Tables["ContractChange"].Select(string.Format("ContractChangeCode={0}", contractChangeCode), "", System.Data.DataViewRowState.CurrentRows))
                    {
                        status = (int)dr["Status"];
                    }

                    this.btnCheck.Visible = false;
                    this.btnDelete.Visible = false;
                    this.btnModify.Visible = false;
                    this.btnOldCheck.Visible = false;
                    this.btnCheckDelete.Visible = false;

                    if (ConfigurationSettings.AppSettings["IsXinChangNin"] != null && ConfigurationSettings.AppSettings["IsXinChangNin"].ToString() == "1")
                    {
                        this.btnPrint.Visible = true;
                    }
                    else
                    {
                        this.btnPrint.Visible = false;
                    }

                    switch (status)
                    {
                        case 0:// 已审
                            this.btnCheckDelete.Visible = true;
                            break;
                        case 1:// 申请
                            this.btnCheck.Visible = true;
                            this.btnDelete.Visible = true;
                            this.btnModify.Visible = true;
                            this.btnOldCheck.Visible = true;
                            break;
                        case 2: // 申请流程中

                            break;
                        case -1:// 作废
                            break;
                    }

                    this.gvNexusBind(entity.Tables["ContractNexus"]);

                    entity.Dispose();
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "加载合同变更数据失败。");
                Response.Write(Rms.Web.JavaScript.Alert(true, "加载合同变更数据失败：" + ex.Message));
            }


        }

        protected override void InitEventHandler()
        {
            base.InitEventHandler();
            this.dgCostList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCostList_ItemDataBound);
            this.gvNexusList.RowDataBound += new GridViewRowEventHandler(this.gvNexusList_RowDataBound);
        }

        private void ShowContractMoney(EntityData entity, string contractChangeCode)
        {
            entity.SetCurrentTable("Contract");

            decimal TotalMoney, TotalChangeMoney, OriginalMoney, NewTotalMoney, ChangeMoney, BudgetMoney, AdjustMoney; ;

            OriginalMoney = entity.GetDecimal("OriginalMoney");
            BudgetMoney = entity.GetDecimal("BudgetMoney");
            AdjustMoney = entity.GetDecimal("AdjustMoney");

            TotalMoney = Decimal.Zero;
            TotalChangeMoney = Decimal.Zero;
            NewTotalMoney = Decimal.Zero;
            ChangeMoney = Decimal.Zero;

            foreach (DataRow dr in entity.Tables["ContractChange"].Select(string.Format("ContractChangeCode={0}", contractChangeCode), "", System.Data.DataViewRowState.CurrentRows))
            {
                TotalMoney = dr["Money"] != DBNull.Value ? (decimal)dr["Money"] : Decimal.Zero;
                TotalChangeMoney = dr["TotalChangeMoney"] != DBNull.Value ? (decimal)dr["TotalChangeMoney"] : Decimal.Zero;
                NewTotalMoney = dr["NewMoney"] != DBNull.Value ? (decimal)dr["NewMoney"] : Decimal.Zero;
                ChangeMoney = dr["ChangeMoney"] != DBNull.Value ? (decimal)dr["ChangeMoney"] : Decimal.Zero;
            }

            //合同原币金额
            decimal OriginalCash, NewTotalCash, ChangeCash, TotalChangeCash;

            string[] arrField = { "OriginalCash" };
            decimal[] arrValue = RmsPM.BLL.MathRule.SumColumn(entity.Tables["ContractCostCash"], arrField);
            OriginalCash = arrValue[0];

            string[] arrField2 = { "NewMoney", "NewCash", "TotalChangeCash", "ChangeCash" };
            decimal[] arrValue2 = RmsPM.BLL.MathRule.SumColumn(entity.Tables["ContractCostChange"].Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)", contractChangeCode)), arrField2);
//            NewTotalMoney = arrValue2[0];
            NewTotalCash = arrValue2[1];
            TotalChangeCash = arrValue2[2];
            ChangeCash = arrValue2[3];

            hidOriginalMoney.Value = OriginalMoney.ToString();
            hidTotalChangeMoney.Value = TotalChangeMoney.ToString();

            txtBudgetMoney.Value = BudgetMoney.ToString("N");
            txtAdjustMoney.Value = AdjustMoney.ToString("N");
            txtOriginalMoney.Value = OriginalCash.ToString("N");
            txtTotalChangeMoney.Value = TotalChangeCash.ToString("N");
            txtChangeMoney.Value = ChangeCash.ToString("N");
            txtNewTotalMoney.Value = NewTotalCash.ToString("N");
        }

        private void dgCostListBind(DataTable dt, string contractChangeCode)
        {
            DataView dv = new DataView(dt, String.Format("ContractChangeCode='{0}' and Status in (0,1)", contractChangeCode), "", DataViewRowState.CurrentRows);

            ViewState["SumCostOriginalMoney"] = BLL.MathRule.SumColumn(dt.Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)", contractChangeCode)), "OriginalMoney");
            ViewState["SumCostTotalChangeMoney"] = BLL.MathRule.SumColumn(dt.Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)", contractChangeCode)), "TotalChangeMoney");
            ViewState["SumCostChangeMoney"] = BLL.MathRule.SumColumn(dt.Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)", contractChangeCode)), "ChangeMoney");
            ViewState["SumCostNewMoney"] = BLL.MathRule.SumColumn(dt.Select(String.Format("ContractChangeCode='{0}' and Status in (0,1)", contractChangeCode)), "NewMoney");

            dgCostList.DataSource = dv;
            dgCostList.DataBind();

        }

        private void dgCostList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    break;
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    string ud_sProjectCode = Request["ProjectCode"] + "";

                    UserControls.ExchangeRateControl ud_ucExchangeRate = (UserControls.ExchangeRateControl)e.Item.FindControl("ucExchangeRate");
                    UserControls.InputCostBudgetDtl ud_ucCostBudgetDtl = (UserControls.InputCostBudgetDtl)e.Item.FindControl("ucCostBudgetDtl");

                    DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

                    ud_ucCostBudgetDtl.ProjectCode = ud_sProjectCode;
                    ud_ucCostBudgetDtl.Enable = false;

                    ud_ucExchangeRate.Cash = BLL.ConvertRule.ToDecimal(ud_drvItem["Cash"]);
                    ud_ucExchangeRate.ExchangeRate = BLL.ConvertRule.ToDecimal(ud_drvItem["ExchangeRate"]);
                    ud_ucExchangeRate.MoneyType = ud_drvItem["MoneyType"].ToString();
                    ud_ucExchangeRate.IsShowTitle = false;
                    ud_ucExchangeRate.EditMode = false;
                    ud_ucExchangeRate.BindControl();

                    break;
                case ListItemType.Footer:
                    //显示合计金额
                    ((Label)e.Item.FindControl("lblSumCostOriginalMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumCostOriginalMoney"], "", true);
                    ((Label)e.Item.FindControl("lblSumCostTotalChangeMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumCostTotalChangeMoney"], "", true);
                    ((Label)e.Item.FindControl("lblSumCostChangeMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumCostChangeMoney"], "", true);
                    ((Label)e.Item.FindControl("lblSumCostNewMoney")).Text = BLL.MathRule.GetDecimalShowString(ViewState["SumCostNewMoney"], "", true);
                    break;
                default:
                    break;

            }
        }

        /// <summary>
        /// 显示相关单据
        /// </summary>
        private void gvNexusBind(DataTable pm_dtNexusList)
        {
            string ud_sContractChangeCode = Request["ContractChangeCode"] + "";

            string ud_sFilter = string.Format("ContractChangeCode='{0}'", ud_sContractChangeCode);

            decimal ud_deSumMoney = BLL.MathRule.SumColumn(pm_dtNexusList, "Money", ud_sFilter);

            ViewState["_SumMoney"] = ud_deSumMoney;

            DataView ud_dvNexusList = new DataView(pm_dtNexusList, ud_sFilter, "", DataViewRowState.CurrentRows);

            gvNexusList.DataSource = ud_dvNexusList;
            gvNexusList.DataBind();

        }

        private void gvNexusList_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:

                    Label ud_lblNexusType = (Label)e.Row.FindControl("lblNexusType");
                    Label ud_lblPersonName = (Label)e.Row.FindControl("lblPersonName");

                    DataRowView ud_drvItem = (DataRowView)e.Row.DataItem;

                    switch (ud_drvItem["Type"].ToString())
                    {
                        case "Vise":
                            ud_lblNexusType.Text = "现场签证";
                            break;
                    }

                    ud_lblPersonName.Text = BLL.SystemRule.GetUserName(ud_drvItem["Person"].ToString());
                    break;

                case DataControlRowType.Footer:
                    Label ud_lblSumMoney = (Label)e.Row.FindControl("lblSumMoney");

                    ud_lblSumMoney.Text = decimal.Parse(ViewState["_SumMoney"].ToString()).ToString("N");

                    break;
            }
        }

        protected void btnOldCheck_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                string ud_sProjectCode = Request["ProjectCode"] + "";
                string ud_sContractCode = Request["ContractCode"] + "";
                string ud_sContractChangeCode = Request["ContractChangeCode"] + "";

                EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ud_sContractCode);

                BLL.ContractRule.ContractChangeAuditing(entity, ud_sContractChangeCode, true);


                //foreach( DataRow drCostChange in entity.Tables["ContractCostChange"].Select(String.Format("ContractChangeCode='{0}'",ud_sContractChangeCode),"",DataViewRowState.CurrentRows))
                //{
                //    drCostChange["Status"] = 0;

                //    if ( drCostChange["ContractCostCode"].ToString() != "" &&  drCostChange["ContractCostCashCode"].ToString() != "")
                //    {
                //        decimal	ud_deNewCostMoney = Decimal.Zero;

                //        foreach ( DataRow drCash in entity.Tables["ContractCostCash"].Select(String.Format("ContractCostCashCode='{0}'",drCostChange["ContractCostCashCode"].ToString()),"",DataViewRowState.CurrentRows))
                //        {
                //            decimal ud_deCash,ud_deExchangeRate,ud_deMoney;

                //            ud_deCash = BLL.ConvertRule.ToDecimal(drCostChange["NewCash"]);
                //            ud_deExchangeRate = BLL.ConvertRule.ToDecimal(drCostChange["ExchangeRate"]);
                //            ud_deMoney = ud_deCash * ud_deExchangeRate;

                //            drCash["Cash"] = ud_deCash;
                //            drCash["Money"] = ud_deMoney;

                //            ud_deNewCostMoney += ud_deMoney;

                //        }

                //        foreach ( DataRow drCost in entity.Tables["ContractCost"].Select(String.Format("ContractCostCode='{0}'",drCostChange["ContractCostCode"].ToString()),"",DataViewRowState.CurrentRows))
                //        {
                //            drCost["Money"] = ud_deNewCostMoney;
                //            drCost["Description"] = drCostChange["Description"];
                //        }


                //    }
                //    else
                //    {
                //        string ud_sContractCostCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostCode");
                //        string ud_sContractCostCashCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractCostCashCode");

                //        DataRow drCashNew = entity.GetNewRecord("ContractCostCash");

                //        drCashNew["ContractCostCashCode"] = ud_sContractCostCashCode;
                //        drCashNew["ContractCode"] = ud_sContractCode;
                //        drCashNew["ContractCostCode"] = ud_sContractCostCode;
                //        drCashNew["Money"] = drCostChange["NewMoney"];
                //        drCashNew["Cash"] =  drCostChange["NewCash"];
                //        drCashNew["OriginalCash"] = drCostChange["OriginalCash"];
                //        drCashNew["MoneyType"] = drCostChange["MoneyType"];
                //        drCashNew["ExchangeRate"] = drCostChange["ExchangeRate"];

                //        entity.Tables["ContractCostCash"].Rows.Add(drCashNew);

                //        DataRow drCostNew = entity.GetNewRecord("ContractCost");

                //        drCostNew["ContractCostCode"] = ud_sContractCostCode;
                //        drCostNew["ContractCode"] = ud_sContractCode;
                //        drCostNew["CostCode"] = drCostChange["CostCode"];
                //        drCostNew["Money"] = drCostChange["NewMoney"];
                //        drCostNew["CostBudgetDtlCode"] = drCostChange["CostBudgetDtlCode"];
                //        drCostNew["CostBudgetSetCode"] = drCostChange["CostBudgetSetCode"];
                //        drCostNew["PBSType"] = drCostChange["PBSType"];
                //        drCostNew["PBSCode"] = drCostChange["PBSCode"];
                //        drCostNew["Description"] = drCostChange["Description"];
                //        drCostNew["OriginalMoney"] = drCostChange["OriginalMoney"];

                //        entity.Tables["ContractCost"].Rows.Add(drCostNew);
                //    }
                //}

                //foreach (DataRow drChange in entity.Tables["ContractChange"].Select(String.Format("ContractChangeCode='{0}'",ud_sContractChangeCode),"",DataViewRowState.CurrentRows))
                //{
                //    drChange["Status"] = 0;

                //    foreach ( DataRow dr in entity.Tables["Contract"].Select(String.Format("ContractCode='{0}'",ud_sContractCode),"",DataViewRowState.CurrentRows))
                //    {
                //        dr["ChangePerson"] = drChange["ChangePerson"];
                //        dr["ChangeReason"] = drChange["ChangeReason"];
                //        dr["ChangeDate"] = drChange["ChangeDate"];
                //        dr["TotalMoney"] = drChange["NewMoney"];

                //        if ( entity.Tables["ContractChange"].Select(string.Format("ContractChangeCode<>'{0}' and Status in (1,2)",ud_sContractChangeCode),"",System.Data.DataViewRowState.CurrentRows).Length > 1 )
                //        {
                //            dr["Status"] = 4;
                //        }
                //        else
                //        {
                //            dr["Status"] = 0;
                //        }

                //    }
                //}
                //DAL.EntityDAO.ContractDAO.SubmitAllStandard_Contract(entity);
                //entity.Dispose();
                Response.Write(Rms.Web.JavaScript.Alert(true, "合同变更审核完毕"));
                Response.Write(Rms.Web.JavaScript.ScriptStart);

                Response.Write("window.location.href='../Contract/ContractChangeInfo.aspx?ProjectCode=" + ud_sProjectCode + "&ContractCode=" + ud_sContractCode + "&ContractChangeCode=" + ud_sContractChangeCode + "';");
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "审批合同变更出错：" + ex.Message));
            }
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            string ud_sProjectCode = Request["ProjectCode"] + "";
            string ud_sContractCode = Request["ContractCode"] + "";
            string ud_sContractChangeCode = Request["ContractChangeCode"] + "";

            EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ud_sContractCode);

            if (BLL.ContractRule.ContractChangeDetele(entity, ud_sContractChangeCode))
            {

                Response.Write(Rms.Web.JavaScript.Alert(true, "合同变更删除完毕"));
                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write("window.opener.location = window.opener.location;");
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "合同变更删除失败"));
            }
        }

        /// <summary>
        /// 审核后删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckDelete_ServerClick(object sender, EventArgs e)
        {
            string ud_sProjectCode = Request["ProjectCode"] + "";
            string ud_sContractCode = Request["ContractCode"] + "";
            string ud_sContractChangeCode = Request["ContractChangeCode"] + "";

            EntityData entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(ud_sContractCode);

            if (BLL.ContractRule.ContractChangeDetele(entity, ud_sContractChangeCode))
            {

                Response.Write(Rms.Web.JavaScript.Alert(true, "合同变更删除完毕"));
                Response.Write(Rms.Web.JavaScript.ScriptStart);
                Response.Write("window.opener.location = window.opener.location;");
                Response.Write(Rms.Web.JavaScript.WinClose(false));
                Response.Write(Rms.Web.JavaScript.ScriptEnd);
            }
            else
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "合同变更删除失败"));
            }
        }
    }
}
