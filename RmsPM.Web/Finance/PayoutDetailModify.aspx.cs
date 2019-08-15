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
using Infragistics.WebUI.WebDataInputT1;
using Rms.Web;

public partial class Finance_PayoutDetailModify : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IniPage();
            LoadData();
        }
    }

    private void IniPage()
    {
        try
        {
            this.inputSystemGroup.ClassCode = "0602";

            this.txtProjectCode.Value = Request.QueryString["ProjectCode"];
            this.txtPayoutCode.Value = Request.QueryString["PayoutCode"];
            this.txtPaymentCode.Value = Request.QueryString["PaymentCode"];

            this.ucInputSubject.ProjectCode = this.txtProjectCode.Value;
            RmsPM.BLL.PageFacade.LoadDictionarySelect(this.sltPaymentType, "付款类型", "");

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
        }
    }

    private void LoadData()
    {
        string ud_sPayoutCode = this.txtPayoutCode.Value;

        try
        {
            DataTable ud_dtDtl;
            DataTable ud_dtExchangeRate;

            //新增时必须传入项目代码
            if ((ud_sPayoutCode == "") && (this.txtProjectCode.Value == ""))
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, "无项目代码，不能新增"));
                return;
            }

            if (ud_sPayoutCode != "")
            {
                //修改
                this.txtIsNew.Value = "0";

                EntityData entity = RmsPM.DAL.EntityDAO.PaymentDAO.GetPayoutByCode(ud_sPayoutCode);

                if (entity.HasRecord())
                {
                    DataRow dr = entity.CurrentRow;

                    this.txtProjectCode.Value = entity.GetString("ProjectCode");
                    this.txtPaymentCode.Value = entity.GetString("PaymentCodes");
                    this.txtSubjectSetCode.Value = entity.GetString("SubjectSetCode");

                    this.ucInputSubject.SubjectSetCode = this.txtSubjectSetCode.Value;
                    this.ucInputSubject.Value = entity.GetString("SubjectCode");

                    this.dtPayoutDate.Value = entity.GetDateTimeOnlyDate("PayoutDate");

                    this.txtPayoutID.Value = entity.GetString("PayoutID");

                    this.lblMoney.Text = RmsPM.BLL.StringRule.BuildGeneralNumberString(entity.GetDecimal("Money"));

                    this.lblSupplyName.Text = entity.GetString("SupplyName");
                    this.txtPayer.Value = entity.GetString("Payer");
                    this.txtSupplyCode.Value = entity.GetString("SupplyCode");

                    this.sltPaymentType.Value = entity.GetString("PaymentType");
                    this.txtBillNo.Value = entity.GetString("BillNo");
                    this.txtInvoNo.Value = entity.GetString("InvoNo");
                    this.txtReceiptCount.Value = entity.GetInt("ReceiptCount").ToString();

                    this.txtStatus.Value = entity.GetInt("Status").ToString();

                    this.inputSystemGroup.Value = entity.GetString("GroupCode");

         

                    //财务系统凭证号
                    this.txtVoucherNo.Value = entity.GetString("VoucherNo");

                    //ud_dtDtl = RmsPM.BLL.PaymentRule.GeneratePayoutItemTable(ud_sPayoutCode);

                    ////修改时，明细缺省为选中
                    //foreach (DataRow drTemp in ud_dtDtl.Rows)
                    //{
                    //    drTemp["Checked"] = 1;
                    //}

                    //RmsPM.BLL.PaymentRule.GeneratePaymentItemTableValue(ud_dtDtl);

                    ud_dtDtl = GetDefaultPayoutItemTable();


                    switch (this.up_sPMNameLower)
                    {
                        case "shimaopm":
                            //世茂要求相应币种的初始汇率为合同中的汇率
                            if (ud_dtDtl.Rows.Count > 0)
                            {
                                ud_dtExchangeRate = this.ucExchangeRate.MoneyTypeDataSource;

                                foreach (DataRow ud_drExchangeRate in ud_dtExchangeRate.Select(string.Format("MoneyType='{0}'", ud_dtDtl.Rows[0]["MoneyType"].ToString())))
                                {
                                    ud_drExchangeRate["ExchangeRate"] = ud_dtDtl.Rows[0]["ExchangeRate"];
                                }
                                this.ucExchangeRate.MoneyTypeDataSource = ud_dtExchangeRate;
                            }
                            break;
                        default:
                            break;
                    }

                    this.ucExchangeRate.Cash = entity.GetDecimal("Cash");
                    this.ucExchangeRate.MoneyType = entity.GetString("MoneyType");
                    this.ucExchangeRate.ExchangeRate = entity.GetDecimal("ExchangeRate");
                    this.ucExchangeRate.DataBind();
                }
                else
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "付款单不存在"));
                    return;
                }
                entity.Dispose();


            }
            else
            {
                //新增
                this.txtIsNew.Value = "1";
                this.txtSubjectSetCode.Value = RmsPM.BLL.ProjectRule.GetSubjectSetCodeByProject(this.txtProjectCode.Value);
                string ud_sPaymentCodes = this.txtPaymentCode.Value;
                string ud_sSortID = Request["Type"] + "";
                string ud_sGroupCode = "";

                string ud_sMoneyType = "";
                decimal ud_deCash = decimal.Zero;
                decimal ud_deExchangeRate = decimal.Zero;


                if (ud_sPaymentCodes == "")
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "未传入请款单号"));
                    Response.Write(Rms.Web.JavaScript.WinClose(true));
                    return;
                }

                switch (this.up_sPMNameLower)
                {
                    case "shimaopm":
                        string ud_sProjectName = RmsPM.BLL.ProjectRule.GetProjectName(this.txtProjectCode.Value);
                        ud_sSortID = RmsPM.BLL.SystemGroupRule.GetSystemGroupSortIDByGroupNameAndClassCode(ud_sProjectName, "0602");
                        break;
                    default:
                        break;

                }

                if (ud_sSortID.Trim() != "")
                {
                    ud_sGroupCode = RmsPM.BLL.SystemGroupRule.GetSystemGroupCodeBySortID(ud_sSortID.Trim(), "0602");
                }

                //缺省值
                this.dtPayoutDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

                string ud_sPayer = "";
                string ud_sSupplyName = "";
                //ud_dtDtl = RmsPM.BLL.PaymentRule.GeneratePayoutItemTable(ud_sPayoutCode);

                bool ud_bIsFirstRecord = true;

                
                foreach ( string ud_sPaymentCode in ud_sPaymentCodes.Split(",".ToCharArray()) )
                {

                    //请款单信息
                    EntityData entityPayment = RmsPM.DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(ud_sPaymentCode);
                    if (!entityPayment.HasRecord())
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("请款单“{0}”不存在", ud_sPaymentCode)));
                        Response.Write(Rms.Web.JavaScript.WinClose(true));
                        return;
                    }

                    if ( ud_bIsFirstRecord )
                    {
                        //检查必须是同一受款人
                        ud_sSupplyName = entityPayment.GetString("SupplyName");
                        ud_sPayer = entityPayment.GetString("Payer");
                        this.txtSupplyCode.Value = entityPayment.GetString("SupplyCode");
                        this.lblSupplyName.Text = entityPayment.GetString("SupplyName");
                        this.txtPayer.Value = ud_sPayer;

                        ud_bIsFirstRecord = false;
                    }
                    else
                    {
                        //检查必须是同一受款人
                        if ((entityPayment.GetString("SupplyName") != ud_sSupplyName) || (entityPayment.GetString("Payer") != ud_sPayer))
                        {
                            Response.Write(Rms.Web.JavaScript.Alert(true, "只有同一受款单位、受款人才能一起付款"));
                            Response.Write(Rms.Web.JavaScript.WinClose(true));
                            return;
                        }

                    }

                    //string ud_sPaymentID = entityPayment.GetString("PaymentID");

                    ////缺省从请款单生成付款明细
                    //entityPayment.SetCurrentTable("PaymentItem");

                    //foreach (DataRow dr in entityPayment.CurrentTable.Rows)
                    //{
                    //    DataRow drNew = ud_dtDtl.NewRow();

                    //    int sno = RmsPM.BLL.ConvertRule.ToInt(this.txtDetailSno.Value.Trim()) + 1;
                    //    this.txtDetailSno.Value = sno.ToString();

                    //    drNew["PayoutItemCode"] = -sno;
                    //    drNew["PaymentItemCode"] = RmsPM.BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
                    //    drNew["PaymentID"] = ud_sPaymentID;
                    //    drNew["Summary"] = RmsPM.BLL.ConvertRule.ToString(dr["Summary"]);
                    //    drNew["CostCode"] = RmsPM.BLL.ConvertRule.ToString(dr["CostCode"]);
                    //    drNew["ItemMoney"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["ItemMoney"]);
                    //    drNew["SubjectCode"] = RmsPM.BLL.CBSRule.GetCBSSubjectCode( RmsPM.BLL.ConvertRule.ToString(dr["CostCode"]));
                    //    drNew["Checked"] = 0;


                    //    drNew["ItemCash"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["ItemCash"]);
                    //    drNew["PaymentMoneyType"] = RmsPM.BLL.ConvertRule.ToString(dr["MoneyType"]);
                    //    drNew["PaymentExchangeRate"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["ExchangeRate"]);


                    //    RmsPM.BLL.PaymentRule.GeneratePaymentItemTableValue(drNew);

                    //    //有剩余请款金额时，才生成到付款
                    //    if ( RmsPM.BLL.ConvertRule.ToDecimal(drNew["RemainItemCash"]) != 0)
                    //    {
                    //        ud_dtDtl.Rows.Add(drNew);
                    //    }
                    //}

                    entityPayment.Dispose();
                }

                ud_dtDtl = GetDefaultPayoutItemTable();

 
                //新增时立即生成付款单序号，PayoutID = PayoutCode
                ud_sPayoutCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");
                this.txtPayoutID.Value = ud_sPayoutCode;
                this.txtPayoutCode.Value = ud_sPayoutCode;
                this.lblMoney.Text = decimal.Zero.ToString("N");

                if (ud_sGroupCode != "")
                {
                    this.inputSystemGroup.Value = ud_sGroupCode;
                }

                bool ud_bBindExchangeRate = true;

                foreach (DataRow dr in ud_dtDtl.Rows)
                {
                    if (ud_sMoneyType == "")
                    {
                        ud_sMoneyType = dr["PaymentMoneyType"].ToString();
                        ud_deCash = RmsPM.BLL.ConvertRule.ToDecimal(dr["ItemCash"]);
                        ud_deExchangeRate = RmsPM.BLL.ConvertRule.ToDecimal(dr["PaymentExchangeRate"]);
                    }
                    else if (ud_sMoneyType == dr["PaymentMoneyType"].ToString())
                    {
                        ud_deCash += RmsPM.BLL.ConvertRule.ToDecimal(dr["ItemCash"]);
                    }
                    else
                    {
                        ud_bBindExchangeRate = false;
                        break;
                    }
                }



                //if (ud_bBindExchangeRate)
                //{
                //    this.ucExchangeRate.Cash = ud_deCash;
                //    this.ucExchangeRate.ExchangeRate = ud_deExchangeRate;
                //    this.ucExchangeRate.MoneyType = ud_sMoneyType;
                //    this.ucExchangeRate.DataBind();
                //}
                switch (this.up_sPMNameLower)
                {
                    case "shimaopm":
                        //世茂要求相应币种的初始汇率为合同中的汇率
                        ud_dtExchangeRate = this.ucExchangeRate.MoneyTypeDataSource;

                        foreach (DataRow dr in ud_dtExchangeRate.Select(string.Format("MoneyType='{0}'", ud_sMoneyType)))
                        {
                            dr["ExchangeRate"] = ud_deExchangeRate;
                        }
                        this.ucExchangeRate.MoneyTypeDataSource = ud_dtExchangeRate;
                        break;
                    default:
                        break;
                }

                //世茂要求初始为人民币，金额为0
                this.ucExchangeRate.Cash = decimal.Zero;
                this.ucExchangeRate.ExchangeRate = 1m;
                this.ucExchangeRate.MoneyType = "人民币 (RMB)";
                this.ucExchangeRate.DataBind();
            }

            RmsPM.BLL.PaymentRule.VoucherDetailAddColumnSubjectName(ud_dtDtl, this.txtSubjectSetCode.Value);
            ud_dtDtl.Columns.Add("SubjectHint", typeof(String));


            this.BindGridView(ud_dtDtl);

            //				BLL.PageFacade.LoadWBSTaskFullNameSelect(this.sltTask, "", this.txtProjectCode.Value);
            //				this.sltTask.Value = WBSCode;


            //申请时可以不录科目，审核后修改时必须录入科目 2018.6.12
            if (RmsPM.BLL.ConvertRule.ToInt(this.txtStatus.Value) == 0)
            {
                this.lblSubjectCodeHint.Visible = false;
            }

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "显示数据出错：" + ex.Message));
        }

    }

    private DataTable GetDefaultPayoutItemTable()
    {
        DataTable ud_dtDtl = new DataTable();

        switch (this.txtIsNew.Value)
        { 
            case "0":
                //修改

                string ud_sPayoutCode = txtPayoutCode.Value;

                ud_dtDtl = RmsPM.BLL.PaymentRule.GeneratePayoutItemTable(ud_sPayoutCode);

                //修改时，明细缺省为选中
                foreach (DataRow drTemp in ud_dtDtl.Rows)
                {
                    drTemp["Checked"] = 1;
                }

                RmsPM.BLL.PaymentRule.GeneratePaymentItemTableValue(ud_dtDtl);
                break;
            case "1":
                ud_dtDtl = RmsPM.BLL.PaymentRule.GeneratePayoutItemTable("");

                bool ud_bIsFirstRecord = true;

                string ud_sPaymentCodes = txtPaymentCode.Value;

                this.txtDetailSno.Value = "0";


                foreach (string ud_sPaymentCode in ud_sPaymentCodes.Split(",".ToCharArray()))
                {

                    //请款单信息
                    EntityData entityPayment = RmsPM.DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(ud_sPaymentCode);

                    string ud_sPaymentID = entityPayment.GetString("PaymentID");

                    //缺省从请款单生成付款明细
                    entityPayment.SetCurrentTable("PaymentItem");

                    foreach (DataRow dr in entityPayment.CurrentTable.Rows)
                    {
                        DataRow drNew = ud_dtDtl.NewRow();

                        int sno = RmsPM.BLL.ConvertRule.ToInt(this.txtDetailSno.Value.Trim()) + 1;
                        this.txtDetailSno.Value = sno.ToString();

                        drNew["PayoutItemCode"] = -sno;
                        drNew["PaymentItemCode"] = RmsPM.BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
                        drNew["PaymentID"] = ud_sPaymentID;
                        drNew["Summary"] = RmsPM.BLL.ConvertRule.ToString(dr["Summary"]);
                        drNew["CostCode"] = RmsPM.BLL.ConvertRule.ToString(dr["CostCode"]);
                        drNew["CostBudgetSetCode"] = RmsPM.BLL.ConvertRule.ToString(dr["CostBudgetSetCode"]);
                        drNew["ItemMoney"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["ItemMoney"]);
                        drNew["SubjectCode"] = RmsPM.BLL.CBSRule.GetCBSSubjectCode(RmsPM.BLL.ConvertRule.ToString(dr["CostCode"]));
                        drNew["Checked"] = 0;


                        drNew["ItemCash"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["ItemCash"]);
                        drNew["PaymentMoneyType"] = RmsPM.BLL.ConvertRule.ToString(dr["MoneyType"]);
                        drNew["PaymentExchangeRate"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["ExchangeRate"]);


                        RmsPM.BLL.PaymentRule.GeneratePaymentItemTableValue(drNew);

                        //有剩余请款金额时，才生成到付款
                        if (RmsPM.BLL.ConvertRule.ToDecimal(drNew["RemainItemCash"]) != 0)
                        {
                            ud_dtDtl.Rows.Add(drNew);
                        }
                    }
                    entityPayment.Dispose();
                }
                break;
        }

        return ud_dtDtl;

    }

    /// <summary>
    /// 显示付款单明细
    /// </summary>
    private void BindGridView(DataTable tb)
    {
        try
        {
            string[] arrField = { "ItemCash", "TotalPayoutCash", "RemainItemCash", "PayoutCash", "PayoutMoney" };
            decimal[] arrSum = RmsPM.BLL.MathRule.SumColumn(tb, arrField);
            this.gvPayoutItem.Columns[4].FooterText = arrSum[0].ToString("N");
            this.gvPayoutItem.Columns[5].FooterText = arrSum[1].ToString("N");
            this.gvPayoutItem.Columns[6].FooterText = arrSum[2].ToString("N");

            this.gvPayoutItem.DataSource = tb;
            this.gvPayoutItem.DataBind();

            ((Label)this.gvPayoutItem.FooterRow.FindControl("lblSumPayoutCash")).Text = arrSum[3].ToString("N");
//            ((Label)this.gvPayoutItem.FooterRow.FindControl("lblSumPayoutMoney")).Text = arrSum[4].ToString("N");
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
        }
    }

    protected void gvPayoutItem_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {

        switch (e.Row.RowType)
        { 
            case DataControlRowType.DataRow:
                DataRowView ud_drvItem = (DataRowView)e.Row.DataItem;

                WebNumericEdit ud_txtPayoutCash = (WebNumericEdit)e.Row.FindControl("txtPayoutCash");
                WebNumericEdit ud_txtPayoutExchangeRate = (WebNumericEdit)e.Row.FindControl("txtPayoutExchangeRate");
                Label ud_lblPayoutItemMoney = (Label)e.Row.FindControl("lblPayoutItemMoney");
                RmsPM.Web.UserControls.InputSubject ud_ucPayoutInputSubject = (RmsPM.Web.UserControls.InputSubject)e.Row.FindControl("ucPayoutInputSubject");

                string  ud_sPaymentMoneyType = ud_drvItem["PaymentMoneyType"] == DBNull.Value ? string.Empty : ud_drvItem["PaymentMoneyType"].ToString();

                bool ud_bChecked = false;

                if (ud_drvItem["Checked"] != DBNull.Value && ud_drvItem["Checked"].ToString() == "1")
                {
                    ud_bChecked = true;
                }

                if (ud_sPaymentMoneyType == ucExchangeRate.MoneyType)
                {
                    ud_txtPayoutExchangeRate.Enabled = false;
                }
                else 
                {
                    ud_txtPayoutExchangeRate.Enabled = true;
                }


                decimal ud_dePayoutItemMoney = ud_txtPayoutCash.ValueDecimal * ud_txtPayoutExchangeRate.ValueDecimal;

                ud_lblPayoutItemMoney.Text = ud_dePayoutItemMoney.ToString("N");

                break;
        }
    }

    protected void gvPayoutItem_RowCreated(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        { 
            case DataControlRowType.DataRow:
                RmsPM.Web.UserControls.InputSubject ud_ucPayoutInputSubject = (RmsPM.Web.UserControls.InputSubject)e.Row.FindControl("ucPayoutInputSubject");
                ud_ucPayoutInputSubject.SubjectSetCode = this.txtSubjectSetCode.Value;

                break;
        }
    }

    
    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        try
        {
            //新的明细表
            DataTable ud_dtDtl = ScreenToTable(true);
            if (ud_dtDtl == null) return;

            string Hint = "";
            if (!CheckValid(ref Hint, ud_dtDtl))
            {
                Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
                this.BindGridView(ud_dtDtl);
                return;
            }

            Save(ud_dtDtl);
        }
        catch (Exception ex)
        {
            Response.Write(JavaScript.Alert(true, "保存失败：" + ex.Message));
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            return;
        }

        GoBack();
    }


    /// <summary>
    /// 返回
    /// </summary>
    private void GoBack()
    {
        Response.Write(Rms.Web.JavaScript.ScriptStart);

        //保存后始终进入付款单信息页面（应付款 -> 付款）
        Response.Write(string.Format("window.opener.location = '../Finance/PayoutInfo.aspx?PayoutCode={0}';", this.txtPayoutCode.Value));
        //			Response.Write("window.opener.location = window.opener.location;");
        Response.Write(Rms.Web.JavaScript.WinClose(false));
        Response.Write(Rms.Web.JavaScript.ScriptEnd);
    }


    /// <summary>
    /// 屏幕数据保存到临时表
    /// </summary>
    /// <returns></returns>
    private DataTable ScreenToTable(bool isBindGrid)
    {
//        DataTable ud_dtDtl = RmsPM.BLL.PaymentRule.GeneratePayoutItemTable("");
        DataTable ud_dtDtl = GetDefaultPayoutItemTable();
        RmsPM.BLL.PaymentRule.VoucherDetailAddColumnSubjectName(ud_dtDtl, this.txtSubjectSetCode.Value);
        ud_dtDtl.Columns.Add("SubjectHint", typeof(String));

        decimal ud_dePayoutExchangeRate = ucExchangeRate.ExchangeRate;

        foreach (GridViewRow ud_gvrRow in this.gvPayoutItem.Rows)
        {
            HtmlInputHidden txtSelect = (HtmlInputHidden)ud_gvrRow.FindControl("txtSelect");
            HtmlInputHidden txtPayoutItemCode = (HtmlInputHidden)ud_gvrRow.FindControl("txtPayoutItemCode");
            HtmlInputHidden txtPaymentItemCode = (HtmlInputHidden)ud_gvrRow.FindControl("txtPaymentItemCode");
            HtmlInputHidden txtSummary = (HtmlInputHidden)ud_gvrRow.FindControl("txtSummary");
            HtmlInputHidden txtItemMoney = (HtmlInputHidden)ud_gvrRow.FindControl("txtItemMoney");
            HtmlInputHidden txtTotalPayoutCash = (HtmlInputHidden)ud_gvrRow.FindControl("txtTotalPayoutCash");
            HtmlInputHidden txtRemainItemCash = (HtmlInputHidden)ud_gvrRow.FindControl("txtRemainItemCash");
            HtmlInputHidden txtCostCode = (HtmlInputHidden)ud_gvrRow.FindControl("txtCostCode");
            HtmlInputHidden txtCostName = (HtmlInputHidden)ud_gvrRow.FindControl("txtCostName");
            HtmlInputHidden txtPaymentID = (HtmlInputHidden)ud_gvrRow.FindControl("txtPaymentID");


            WebNumericEdit txtPayoutCash = (WebNumericEdit)ud_gvrRow.FindControl("txtPayoutCash");
            RmsPM.Web.UserControls.InputSubject ucPayoutInputSubject = (RmsPM.Web.UserControls.InputSubject)ud_gvrRow.FindControl("ucPayoutInputSubject");
            WebNumericEdit txtPayoutExchangeRate = (WebNumericEdit)ud_gvrRow.FindControl("txtPayoutExchangeRate");


            string ud_sPayoutItemCode = txtPayoutItemCode.Value;
            string ud_sPayoutItemCodeFilter = string.Format("PayoutItemCode='{0}'",ud_sPayoutItemCode);

            foreach (DataRow dr in ud_dtDtl.Select(ud_sPayoutItemCodeFilter))
            {
                
                dr["Checked"] = RmsPM.BLL.ConvertRule.ToInt(txtSelect.Value);

                dr["PayoutCash"] = txtPayoutCash.ValueDecimal;
                dr["MoneyType"] = dr["PaymentMoneyType"];
                dr["ExchangeRate"] = txtPayoutExchangeRate.ValueDecimal;

                if (ucExchangeRate.MoneyType == "人民币 (RMB)")
                {
                    dr["PayoutMoney"] = txtPayoutCash.ValueDecimal * txtPayoutExchangeRate.ValueDecimal;
                }
                else
                {
                    dr["PayoutMoney"] = txtPayoutCash.ValueDecimal * (decimal)dr["ExchangeRate"];
                }

                dr["PayoutMoneyType"] = ucExchangeRate.MoneyType;
                dr["PayoutExchangeRate"] = ucExchangeRate.ExchangeRate;

                dr["SubjectCode"] = ucPayoutInputSubject.Value;
                dr["SubjectName"] = ucPayoutInputSubject.Text;
                dr["SubjectHint"] = ucPayoutInputSubject.Hint;


            }
        }

        if (isBindGrid)
        {
            this.BindGridView(ud_dtDtl);
        }

        return ud_dtDtl;
    }


    /// <summary>
    /// 有效性检查
    /// </summary>
    /// <param name="Hint"></param>
    /// <returns></returns>
    private bool CheckValid(ref string Hint, DataTable tbDtl)
    {
        Hint = "";

        if (this.dtPayoutDate.Value.Trim() == "")
        {
            Hint = "请输入付款日期 ！";
            return false;
        }

        if (this.inputSystemGroup.Value.Trim() == "")
        {
            Hint = "请输入系统类型 ！";
            return false;
        }
        //农房的不需要必填贷方
        if (this.up_sPMNameLower != "nonggongshangpm")
        {
            if (this.ucInputSubject.Value.Trim() == "")
            {
                Hint = "请输入贷方科目";
                return false;
            }
        }
        string SubjectCode = this.ucInputSubject.Value;

        //申请时可以不录科目，审核后修改时必须录入科目
        if (RmsPM.BLL.ConvertRule.ToInt(this.txtStatus.Value) > 0)
        {
            if ((SubjectCode == null) || (SubjectCode.Trim().Length == 0))
            {
                Hint = "请输入贷方科目 ！";
                return false;
            }
        }

        Hint = RmsPM.BLL.SubjectRule.CheckSubject(SubjectCode, txtSubjectSetCode.Value, string.Format("科目编号“{0}”", SubjectCode));
        if (Hint != "")
            return false;

        if (this.txtReceiptCount.Value != "")
        {
            if (!Rms.Check.StringCheck.IsInt(this.txtReceiptCount.Value))
            {
                Hint = "单据张数必须是整数 ！";
                return false;
            }
        }


        int status = RmsPM.BLL.ConvertRule.ToInt(this.txtStatus.Value);
        EntityData entityOld = null;

        if (this.txtPayoutCode.Value != "")  //修改时，要取修改前的单据金额
        {
            entityOld = RmsPM.DAL.EntityDAO.PaymentDAO.GetPayoutItemByPayoutCode(this.txtPayoutCode.Value);
        }

        foreach (DataRow dr in tbDtl.Rows)
        {
            string PayoutItemCode = RmsPM.BLL.ConvertRule.ToString(dr["PayoutItemCode"]);
            string PaymentItemCode = RmsPM.BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
            decimal ItemCash = RmsPM.BLL.ConvertRule.ToDecimal(dr["ItemCash"]);

            //已付金额（要包括未审的付款单 xyq 2018.11.21）
            decimal TotalPayoutCash = RmsPM.BLL.PaymentRule.GetPayoutCashByPaymentItemIncludeNotCheck(PaymentItemCode);

            decimal PayoutCash = RmsPM.BLL.ConvertRule.ToDecimal(dr["PayoutCash"]);
            decimal RemainItemCash = ItemCash - TotalPayoutCash;

            //修改时，剩余付款金额中要加上本次修改前的付款金额
            if (entityOld != null)
            {
                DataRow[] drs = entityOld.CurrentTable.Select("PayoutItemCode='" + PayoutItemCode + "'");
                if (drs.Length > 0)
                {
                    RemainItemCash = RemainItemCash + RmsPM.BLL.ConvertRule.ToDecimal(drs[0]["PayoutCash"]);
                }
            }

            if (PayoutCash > RemainItemCash)
            {
                Hint = string.Format("本次付款金额“{0}”不能超出剩余付款金额“{1}”", PayoutCash, RemainItemCash);
                return false;
            }

            //本次付款金额不为0时才检查
            if (PayoutCash != 0)
            {
                SubjectCode = RmsPM.BLL.ConvertRule.ToString(dr["SubjectCode"]);

                //申请时可以不录科目，审核后修改时必须录入科目
                if (RmsPM.BLL.ConvertRule.ToInt(this.txtStatus.Value) > 0)
                {
                    if ((SubjectCode == null) || (SubjectCode.Trim().Length == 0))
                    {
                        Hint = "请输入科目编号 ！";
                        return false;
                    }
                }

                Hint = RmsPM.BLL.SubjectRule.CheckSubject(SubjectCode, txtSubjectSetCode.Value, string.Format("科目编号“{0}”", SubjectCode));
                if (Hint != "")
                    return false;
            }
        }

        decimal ud_dePayoutMoney = this.ucExchangeRate.Money;

        decimal ud_dePayoutDtlMoney = RmsPM.BLL.MathRule.SumColumn(tbDtl, "PayoutMoney");
        decimal ud_deAbs = Math.Abs(ud_dePayoutMoney - ud_dePayoutDtlMoney);

        if ( ud_deAbs >= 0.01m)
        {
            Hint = "付款总额和明细不平，请检查 !";
            return false;
        }

        return true;
    }

    /// <summary>
    /// 保存
    /// </summary>
    private void Save(DataTable tbDtl)
    {
        string PayoutCode = this.txtPayoutCode.Value;
        string projectCode = this.txtProjectCode.Value;
        string ud_sPaymentCodes = this.txtPaymentCode.Value;// Request["PaymentCode"] + "";

        bool isNew = (this.txtIsNew.Value.Trim() == "1");
        int status = RmsPM.BLL.ConvertRule.ToInt(this.txtStatus.Value);

        try
        {
            //审核后修改时，要重新计算请款单的已付状态
            //修改前重算
            if (status > 0)
            {
                RmsPM.BLL.PaymentRule.UpdatePaymentStatusByPayout(PayoutCode, base.user.UserCode);
            }

            EntityData entity = null;
            DataRow dr = null;

            if (isNew)
            {
                entity = new EntityData("Standard_Payout");
                dr = entity.GetNewRecord();

                //					PayoutCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");
                //					this.txtPayoutCode.Value = PayoutCode;

                dr["PayoutCode"] = PayoutCode;
                dr["PayoutID"] = PayoutCode;
                dr["ProjectCode"] = projectCode;
                dr["SubjectSetCode"] = this.txtSubjectSetCode.Value;
                dr["InputPerson"] = base.user.UserCode;
                dr["InputDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                dr["Status"] = 0;
                entity.AddNewRecord(dr);
            }
            else
            {
                entity = RmsPM.DAL.EntityDAO.PaymentDAO.GetStandard_PayoutByCode(PayoutCode);
                dr = entity.CurrentRow;
            }

            //				dr["PayoutID"] = this.txtPayoutID.Value;

            dr["PaymentCodes"] = ud_sPaymentCodes;

            dr["Payer"] = this.txtPayer.Value;
            dr["SupplyCode"] = this.txtSupplyCode.Value;
            dr["SupplyName"] = this.lblSupplyName.Text;

            dr["IsApportioned"] = 0;

            dr["PayoutDate"] = RmsPM.BLL.ConvertRule.ToDate(this.dtPayoutDate.Value);

            dr["PaymentType"] = this.sltPaymentType.Value;
            dr["BillNo"] = this.txtBillNo.Value;
            dr["InvoNo"] = this.txtInvoNo.Value;
            dr["ReceiptCount"] = RmsPM.BLL.ConvertRule.ToIntObj(this.txtReceiptCount.Value);
            dr["SubjectCode"] = this.ucInputSubject.Value;

            dr["GroupCode"] = this.inputSystemGroup.Value;

            //财务系统凭证号
            dr["VoucherNo"] = this.txtVoucherNo.Value;

            //明细总金额
            dr["Cash"] = this.ucExchangeRate.Cash;
            dr["MoneyType"] = this.ucExchangeRate.MoneyType;
            dr["ExchangeRate"] = this.ucExchangeRate.ExchangeRate;

            if (this.ucExchangeRate.MoneyType == "人民币 (RMB)")
            {
                dr["Money"] = ucExchangeRate.Money;
            }
            else
            {
                dr["Money"] = RmsPM.BLL.MathRule.SumColumn(tbDtl, "PayoutMoney");
            }

            SaveDetail(entity, tbDtl);

            RmsPM.DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payout(entity);
            entity.Dispose();

            //审核后修改时，要重新计算请款单的已付状态
            //修改后重算
            if (status > 0)
            {
                RmsPM.BLL.PaymentRule.UpdatePaymentStatusByPayout(PayoutCode, base.user.UserCode);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 保存付款单明细
    /// </summary>
    private void SaveDetail(EntityData entity, DataTable tb)
    {
        try
        {
            entity.SetCurrentTable("Payout");
            string PayoutCode = entity.GetString("PayoutCode");
            string ProjectCode = entity.GetString("ProjectCode");

            //本次付款金额为0的删除
            for (int i = tb.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = tb.Rows[i];
                if ( RmsPM.BLL.ConvertRule.ToDecimal(dr["PayoutMoney"]) == 0)
                {
                    tb.Rows.Remove(dr);
                }
            }

            //旧的明细
            entity.SetCurrentTable("PayoutItem");

            //删除原先有现在没有的
            foreach (DataRow dr in entity.CurrentTable.Rows)
            {
                string PayoutItemCode = dr["PayoutItemCode"].ToString();
                if (tb.Select("PayoutItemCode='" + PayoutItemCode + "'").Length == 0)
                {
                    dr.Delete();

                    //删除付款单分摊到楼栋
                    DataRow[] drs = entity.Tables["PayoutItemBuilding"].Select("PayoutItemCode='" + PayoutItemCode + "'");
                    foreach (DataRow drI in drs)
                    {
                        drI.Delete();
                    }
                }
            }

            //新增或修改
            foreach (DataRow dr in tb.Rows)
            {
                string PayoutItemCode = dr["PayoutItemCode"].ToString();
                DataRow drNew;
                DataRow[] drs;

                //付款明细
                entity.SetCurrentTable("PayoutItem");
                drs = entity.CurrentTable.Select("PayoutItemCode='" + PayoutItemCode + "'");

                if (drs.Length == 0)
                {
                    drNew = entity.CurrentTable.NewRow();

                    PayoutItemCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemCode");
                    drNew["PayoutItemCode"] = PayoutItemCode;
                    drNew["PayoutCode"] = PayoutCode;
                    drNew["PaymentItemCode"] = RmsPM.BLL.ConvertRule.ToString(dr["PaymentItemCode"]);

                    entity.CurrentTable.Rows.Add(drNew);
                }
                else
                {
                    drNew = drs[0];
                }

                drNew["PayoutMoney"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["PayoutMoney"]);
                drNew["PayoutCash"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["PayoutCash"]);
                drNew["MoneyType"] = dr["MoneyType"].ToString();
                drNew["ExchangeRate"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["ExchangeRate"]);

                drNew["PayoutMoneyType"] = dr["PayoutMoneyType"].ToString();
                drNew["PayoutExchangeRate"] = RmsPM.BLL.ConvertRule.ToDecimalObj(dr["PayoutExchangeRate"]);

                drNew["SubjectCode"] = RmsPM.BLL.ConvertRule.ToString(dr["SubjectCode"]);

                //新增付款单明细时，取请款单明细的分摊方式，更新到付款单明细  begin---------------------
                if (drs.Length == 0)
                {
                    string PaymentItemCode = RmsPM.BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
                    EntityData entityPaymentItem = RmsPM.DAL.EntityDAO.PaymentDAO.GetPaymentItemByCode(PaymentItemCode);
                    EntityData entityPaymentItemBuilding = RmsPM.DAL.EntityDAO.PaymentDAO.GetPaymentItemBuildingByPaymentItemCode(PaymentItemCode);

                    drNew["AlloType"] = entityPaymentItem.GetString("AlloType");
                    drNew["IsManualAlloc"] = 0;

                    //从请款单复制分摊明细  begin-------------------------
                    entity.SetCurrentTable("PayoutItemBuilding");

                    //删除付款单分摊到楼栋
                    DataRow[] drsTemp = entity.Tables["PayoutItemBuilding"].Select("PayoutItemCode='" + PayoutItemCode + "'");
                    foreach (DataRow drI in drsTemp)
                    {
                        drI.Delete();
                    }

                    foreach (DataRow drSrc in entityPaymentItemBuilding.CurrentTable.Rows)
                    {
                        DataRow drDst = entity.GetNewRecord();

                        drDst["SystemID"] = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemBuildingCode");
                        drDst["PayoutItemCode"] = PayoutItemCode;
                        drDst["PayoutCode"] = PayoutCode;
                        drDst["BuildingCode"] = drSrc["BuildingCode"];
                        drDst["PBSUnitCode"] = drSrc["PBSUnitCode"];
                        drDst["ItemBuildingMoney"] = decimal.Zero;

                        entity.AddNewRecord(drDst);
                    }
                    //从请款单复制分摊明细  end-------------------------

                    entityPaymentItem.Dispose();
                    entityPaymentItemBuilding.Dispose();
                }
                //新增付款单明细时，取请款单明细的分摊方式，更新到付款单明细  end---------------------

            }
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            throw ex;
        }
    }



}
