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
using RmsPM.DAL.EntityDAO;
using RmsPM.DAL.QueryStrategy;
using Rms.Web;
using Infragistics.WebUI.WebDataInput;

namespace RmsPM.Web.Finance
{
    /// <summary>
    /// PayoutModify 的摘要说明。
    /// </summary>
    public partial class PayoutModify : PageBase
    {

        private void Page_Load(object sender, System.EventArgs e)
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
                //				this.txtIsContract.Value = Request.QueryString["IsContract"];

                this.ucInputSubject.ProjectCode = this.txtProjectCode.Value;
                BLL.PageFacade.LoadDictionarySelect(this.sltPaymentType, "付款类型", "");

                if (hidDDLRMBValue.Value == "")
                {
                    this.ucExchangeRate.BindControl();
                    this.hidDDLRMBValue.Value = this.ucExchangeRate.MoneyTypeValue;
                }

            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "初始化页面出错：" + ex.Message));
            }
        }

        private void LoadData()
        {
            string PayoutCode = this.txtPayoutCode.Value;
            string ud_sProjectCode = Request["ProjectCode"] + "";

            try
            {
                DataTable tbDtl;
                //				string WBSCode = "";

                //新增时必须传入项目代码
                if ((PayoutCode == "") && (this.txtProjectCode.Value == ""))
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, "无项目代码，不能新增"));
                    return;
                }

                if (PayoutCode != "")
                {
                    //修改
                    this.txtIsNew.Value = "0";

                    EntityData entity = DAL.EntityDAO.PaymentDAO.GetPayoutByCode(PayoutCode);
                    if (entity.HasRecord())
                    {
                        DataRow dr = entity.CurrentRow;

                        this.txtProjectCode.Value = entity.GetString("ProjectCode");
                        this.txtSubjectSetCode.Value = entity.GetString("SubjectSetCode");

                        this.dtPayoutDate.Value = entity.GetDateTimeOnlyDate("PayoutDate");
                        //						this.lblMoney.Text = BLL.MathRule.GetDecimalShowString(dr["Money"]);

                        this.txtPayoutID.Value = entity.GetString("PayoutID");


                        this.ucExchangeRate.Cash = entity.GetDecimal("Cash");
                        this.ucExchangeRate.MoneyType = entity.GetString("MoneyType");
                        this.ucExchangeRate.ExchangeRate = entity.GetDecimal("ExchangeRate");
                        this.ucExchangeRate.BindControl();



                        this.txtMoney.Value = BLL.StringRule.BuildGeneralNumberString(entity.GetDecimal("Money"));

                        //						this.txtMoney.Value = BLL.StringRule.BuildGeneralNumberString(entity.GetDecimal("Money"));

                        this.lblSupplyName.Text = entity.GetString("SupplyName");
                        this.txtPayer.Value = entity.GetString("Payer");
                        this.txtSupplyCode.Value = entity.GetString("SupplyCode");

                        this.sltPaymentType.Value = entity.GetString("PaymentType");
                        this.txtBillNo.Value = entity.GetString("BillNo");
                        this.txtInvoNo.Value = entity.GetString("InvoNo");
                        this.txtReceiptCount.Value = entity.GetInt("ReceiptCount").ToString();

                        this.ucInputSubject.ProjectCode = this.txtProjectCode.Value;
                        this.ucInputSubject.Value = entity.GetString("SubjectCode");

                        this.txtStatus.Value = entity.GetInt("Status").ToString();

                        this.inputSystemGroup.Value = entity.GetString("GroupCode");

                        tbDtl = BLL.PaymentRule.GeneratePayoutItemTable(PayoutCode);

                        //修改时，明细缺省为选中
                        foreach (DataRow drTemp in tbDtl.Rows)
                        {
                            drTemp["Checked"] = 1;
                        }
                    }
                    else
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "付款单不存在"));
                        return;
                    }
                    entity.Dispose();

                    BLL.PaymentRule.GeneratePaymentItemTableValue(tbDtl);
                }
                else
                {
                    //新增
                    this.txtIsNew.Value = "1";
                    this.txtSubjectSetCode.Value = BLL.ProjectRule.GetSubjectSetCodeByProject(this.txtProjectCode.Value);
                    string PaymentCodes = this.txtPaymentCode.Value;
                    string ud_sSortID = Request["Type"] + "";
                    string ud_sGroupCode = "";


                    if (PaymentCodes == "")
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "未传入请款单号"));
                        Response.Write(Rms.Web.JavaScript.WinClose(true));
                        return;
                    }

                    switch (this.up_sPMNameLower)
                    { 
                        case "shimaopm":
                            string ud_sProjectName = BLL.ProjectRule.GetProjectName(ud_sProjectCode);
                            ud_sSortID = BLL.SystemGroupRule.GetSystemGroupSortIDByGroupNameAndClassCode(ud_sProjectName, "0602");
                            break;
                        default:
                            break;

                    }

                    if (ud_sSortID.Trim() != "")
                    {
                        ud_sGroupCode = BLL.SystemGroupRule.GetSystemGroupCodeBySortID(ud_sSortID.Trim(), "0602");
                    }

                    //缺省值
                    this.dtPayoutDate.Value = DateTime.Today.ToString("yyyy-MM-dd");

                    string payer = "";
                    string SupplyName = "";
                    tbDtl = BLL.PaymentRule.GeneratePayoutItemTable(PayoutCode);

                    string[] arrPaymentCode = PaymentCodes.Split(",".ToCharArray());
                    int iCount = arrPaymentCode.Length;
                    for (int i = 0; i < iCount; i++)
                    {
                        string PaymentCode = arrPaymentCode[i];

                        //请款单信息
                        EntityData entityPayment = DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(PaymentCode);
                        if (!entityPayment.HasRecord())
                        {
                            Response.Write(Rms.Web.JavaScript.Alert(true, string.Format("请款单“{0}”不存在", PaymentCode)));
                            Response.Write(Rms.Web.JavaScript.WinClose(true));
                            return;
                        }

                        if (i > 0)
                        {
                            //检查必须是同一受款人
                            if ((entityPayment.GetString("SupplyName") != SupplyName) || (entityPayment.GetString("Payer") != payer))
                            {
                                Response.Write(Rms.Web.JavaScript.Alert(true, "只有同一受款单位、受款人才能一起付款"));
                                Response.Write(Rms.Web.JavaScript.WinClose(true));
                                return;
                            }
                        }
                        else
                        {
                            //检查必须是同一受款人
                            SupplyName = entityPayment.GetString("SupplyName");
                            payer = entityPayment.GetString("Payer");
                            this.txtSupplyCode.Value = entityPayment.GetString("SupplyCode");
                            this.lblSupplyName.Text = entityPayment.GetString("SupplyName");
                            this.txtPayer.Value = payer;
                        }

                        string PaymentID = entityPayment.GetString("PaymentID");

                        //缺省从请款单生成付款明细
                        entityPayment.SetCurrentTable("PaymentItem");
                        foreach (DataRow dr in entityPayment.CurrentTable.Rows)
                        {
                            DataRow drNew = tbDtl.NewRow();

                            int sno = BLL.ConvertRule.ToInt(this.txtDetailSno.Value.Trim()) + 1;
                            this.txtDetailSno.Value = sno.ToString();

                            drNew["PayoutItemCode"] = -sno;
                            drNew["PaymentItemCode"] = BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
                            drNew["PaymentID"] = PaymentID;
                            drNew["Summary"] = BLL.ConvertRule.ToString(dr["Summary"]);
                            drNew["CostCode"] = BLL.ConvertRule.ToString(dr["CostCode"]);
                            drNew["ItemMoney"] = BLL.ConvertRule.ToDecimalObj(dr["ItemMoney"]);
                            drNew["SubjectCode"] = BLL.CBSRule.GetCBSSubjectCode(BLL.ConvertRule.ToString(dr["CostCode"]));
                            drNew["Checked"] = 0;


                            drNew["ItemCash"] = BLL.ConvertRule.ToDecimalObj(dr["ItemCash"]);
                            drNew["PaymentMoneyType"] = BLL.ConvertRule.ToDecimalObj(dr["MoneyType"]);
                            drNew["PaymentExchangeRate"] = BLL.ConvertRule.ToDecimalObj(dr["ExchangeRate"]);


                            //						drNew["PayoutMoney"] = BLL.ConvertRule.ToDecimalObj(dr["ItemMoney"]);

                            BLL.PaymentRule.GeneratePaymentItemTableValue(drNew);

                            //有剩余请款金额时，才生成到付款
                            if (BLL.ConvertRule.ToDecimal(drNew["RemainItemMoney"]) != 0)
                            {
                                tbDtl.Rows.Add(drNew);
                            }
                        }
                        entityPayment.Dispose();
                    }

                    //本次明细的付款金额=请款单的剩余金额
                    //					foreach(DataRow dr in tbDtl.Rows) 
                    //					{
                    //						string PaymentItemCode = BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
                    //						decimal PayoutMoney = BLL.ConvertRule.ToDecimal(dr["RemainItemMoney"]);
                    //						dr["PayoutMoney"] = PayoutMoney;
                    //					}

                    //新增时，填上缺省付款总额=请款余额
                    string[] arrField = { "ItemMoney", "TotalPayoutMoney" };
                    decimal[] arrSum = BLL.MathRule.SumColumn(tbDtl, arrField);
                    decimal TotalPayoutMoney = arrSum[0] - arrSum[1];

                    this.txtMoney.Value = TotalPayoutMoney;
                    this.ucExchangeRate.Cash = TotalPayoutMoney;
                    this.ucExchangeRate.BindControl();

                   					this.txtMoney.Value = TotalPayoutMoney.ToString("N");

                    //新增时立即生成付款单序号，PayoutID = PayoutCode
                    PayoutCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");
                    this.txtPayoutID.Value = PayoutCode;
                    this.txtPayoutCode.Value = PayoutCode;

                    if (ud_sGroupCode != "")
                    {
                        this.inputSystemGroup.Value = ud_sGroupCode;
                    }
                }

                BLL.PaymentRule.VoucherDetailAddColumnSubjectName(tbDtl, this.txtSubjectSetCode.Value);
                tbDtl.Columns.Add("SubjectHint", typeof(String));
                BindDataGrid(tbDtl);

                //				BLL.PageFacade.LoadWBSTaskFullNameSelect(this.sltTask, "", this.txtProjectCode.Value);
                //				this.sltTask.Value = WBSCode;

                ((HtmlInputText)this.ucExchangeRate.FindControl("ExchangeRateControl_R")).Value = (this.ucExchangeRate.Cash * this.ucExchangeRate.ExchangeRate).ToString();

                //申请时可以不录科目，审核后修改时必须录入科目 2018.6.12
                if (BLL.ConvertRule.ToInt(this.txtStatus.Value) == 0)
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

        /// <summary>
        /// 显示付款单明细
        /// </summary>
        private void BindDataGrid(DataTable tb)
        {
            try
            {
                string[] arrField = { "ItemMoney", "TotalPayoutMoney", "RemainItemMoney", "PayoutMoney" };
                decimal[] arrSum = BLL.MathRule.SumColumn(tb, arrField);
                this.dgList.Columns[4].FooterText = arrSum[0].ToString("N");
                this.dgList.Columns[5].FooterText = arrSum[1].ToString("N");
                this.dgList.Columns[6].FooterText = arrSum[2].ToString("N");
                this.txtSumPayoutMoney.Value = arrSum[3].ToString("N");

                this.dgList.DataSource = tb;
                this.dgList.DataBind();
            }
            catch (Exception ex)
            {
                ApplicationLog.WriteLog(this.ToString(), ex, "");
                Response.Write(Rms.Web.JavaScript.Alert(true, "显示列表出错：" + ex.Message));
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
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dgList.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemCreated);
            this.dgList.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgList_DeleteCommand);
            this.dgList.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgList_ItemDataBound);
            this.btnSave.ServerClick += new System.EventHandler(this.btnSave_ServerClick);
        }
        #endregion

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

            string SubjectCode = this.ucInputSubject.Value;

            //申请时可以不录科目，审核后修改时必须录入科目
            if (BLL.ConvertRule.ToInt(this.txtStatus.Value) > 0)
            {
                if ((SubjectCode == null) || (SubjectCode.Trim().Length == 0))
                {
                    Hint = "请输入贷方科目 ！";
                    return false;
                }
            }

            Hint = BLL.SubjectRule.CheckSubject(SubjectCode, txtSubjectSetCode.Value, string.Format("科目编号“{0}”", SubjectCode));
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

            /*
                        if ( dtPay != "" )
                        {
                            if ( !Rms.Check.StringCheck.IsDateTime(dtPay))
                            {
                                Response.Write(Rms.Web.JavaScript.Alert(true,"付款时间填写错误 ！"));
                                return;
                            }
                        }

                        string money = this.txtMoney.Text.Trim();
                        if ( money != "" )
                        {
                            if ( !Rms.Check.StringCheck.IsNumber(money) )
                            {
                                Response.Write(Rms.Web.JavaScript.Alert(true,"总金额填写错误 ！"));
                                return;
                            }
                        }
            */

            int status = BLL.ConvertRule.ToInt(this.txtStatus.Value);
            EntityData entityOld = null;
            //			if (status > 0) 
            if (this.txtPayoutCode.Value != "")  //修改时，要取修改前的单据金额
            {
                entityOld = DAL.EntityDAO.PaymentDAO.GetPayoutItemByPayoutCode(this.txtPayoutCode.Value);
            }

            foreach (DataRow dr in tbDtl.Rows)
            {
                string PayoutItemCode = BLL.ConvertRule.ToString(dr["PayoutItemCode"]);
                decimal ItemMoney = BLL.ConvertRule.ToDecimal(dr["ItemMoney"]);
                decimal TotalPayoutMoney = BLL.ConvertRule.ToDecimal(dr["TotalPayoutMoney"]);
                decimal PayoutMoney = BLL.ConvertRule.ToDecimal(dr["PayoutMoney"]);
                decimal RemainItemMoney = ItemMoney - TotalPayoutMoney;

                //修改时，剩余付款金额中要加上本次修改前的付款金额
                if (entityOld != null)
                {
                    DataRow[] drs = entityOld.CurrentTable.Select("PayoutItemCode='" + PayoutItemCode + "'");
                    if (drs.Length > 0)
                    {
                        RemainItemMoney = RemainItemMoney + BLL.ConvertRule.ToDecimal(drs[0]["PayoutMoney"]);
                    }
                }

                if (PayoutMoney > RemainItemMoney)
                {
                    Hint = string.Format("本次付款金额“{0}”不能超出未付金额“{1}”", PayoutMoney, RemainItemMoney);
                    return false;
                }

                //本次付款金额不为0时才检查
                if (PayoutMoney != 0)
                {
                    SubjectCode = BLL.ConvertRule.ToString(dr["SubjectCode"]);

                    //申请时可以不录科目，审核后修改时必须录入科目
                    if (BLL.ConvertRule.ToInt(this.txtStatus.Value) > 0)
                    {
                        if ((SubjectCode == null) || (SubjectCode.Trim().Length == 0))
                        {
                            Hint = "请输入科目编号 ！";
                            return false;
                        }
                    }

                    Hint = BLL.SubjectRule.CheckSubject(SubjectCode, txtSubjectSetCode.Value, string.Format("科目编号“{0}”", SubjectCode));
                    if (Hint != "")
                        return false;
                }
            }

            /*
            if ( txtMoney.Value != "")
            {
                if ( !Rms.Check.StringCheck.IsNumber(txtMoney.Value))
                {
                    Hint = "付款总额数字格式不正确 !";
                    return false;
                }
            }
            */

            decimal Money = this.txtMoney.ValueDecimal;
            Money = this.ucExchangeRate.Cash * this.ucExchangeRate.ExchangeRate;

            decimal DtlMoney = BLL.MathRule.SumColumn(tbDtl, "PayoutMoney");
            if (Money != DtlMoney)
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
            string ud_sPaymentCodes = Request["PaymentCode"] + "";

            bool isNew = (this.txtIsNew.Value.Trim() == "1");
            int status = BLL.ConvertRule.ToInt(this.txtStatus.Value);

            try
            {
                //审核后修改时，要重新计算请款单的已付状态
                //修改前重算
                if (status > 0)
                {
                    BLL.PaymentRule.UpdatePaymentStatusByPayout(PayoutCode, base.user.UserCode);
                }

                EntityData entity = null;
                DataRow dr = null;

                if (isNew)
                {
                    entity = new EntityData("Standard_Payout");
                    dr = entity.GetNewRecord();

                    //					PayoutCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutCode");
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
                    entity = DAL.EntityDAO.PaymentDAO.GetStandard_PayoutByCode(PayoutCode);
                    dr = entity.CurrentRow;
                }

                //				dr["PayoutID"] = this.txtPayoutID.Value;

                dr["PaymentCodes"] = ud_sPaymentCodes;

                dr["Payer"] = this.txtPayer.Value;
                dr["SupplyCode"] = this.txtSupplyCode.Value;
                dr["SupplyName"] = this.lblSupplyName.Text;

                dr["IsApportioned"] = 0;

                dr["PayoutDate"] = BLL.ConvertRule.ToDate(this.dtPayoutDate.Value);

                dr["PaymentType"] = this.sltPaymentType.Value;
                dr["BillNo"] = this.txtBillNo.Value;
                dr["InvoNo"] = this.txtInvoNo.Value;
                dr["ReceiptCount"] = BLL.ConvertRule.ToIntObj(this.txtReceiptCount.Value);
                dr["SubjectCode"] = this.ucInputSubject.Value;

                dr["GroupCode"] = this.inputSystemGroup.Value;

                //明细总金额
                dr["Money"] = BLL.MathRule.SumColumn(tbDtl, "PayoutMoney");

                dr["Cash"] = this.ucExchangeRate.Cash;
                dr["MoneyType"] = this.ucExchangeRate.MoneyType;
                dr["ExchangeRate"] = this.ucExchangeRate.ExchangeRate;

                SaveDetail(entity, tbDtl);

                DAL.EntityDAO.PaymentDAO.SubmitAllStandard_Payout(entity);
                entity.Dispose();

                //审核后修改时，要重新计算请款单的已付状态
                //修改后重算
                if (status > 0)
                {
                    BLL.PaymentRule.UpdatePaymentStatusByPayout(PayoutCode, base.user.UserCode);
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
                    if (BLL.ConvertRule.ToDecimal(dr["PayoutMoney"]) == 0)
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

                        PayoutItemCode = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemCode");
                        drNew["PayoutItemCode"] = PayoutItemCode;
                        drNew["PayoutCode"] = PayoutCode;
                        drNew["PaymentItemCode"] = BLL.ConvertRule.ToString(dr["PaymentItemCode"]);

                        entity.CurrentTable.Rows.Add(drNew);
                    }
                    else
                    {
                        drNew = drs[0];
                    }

                    drNew["PayoutMoney"] = BLL.ConvertRule.ToDecimalObj(dr["PayoutMoney"]);
                    drNew["PayoutCash"] = BLL.ConvertRule.ToDecimalObj(dr["PayoutCash"]);
                    drNew["MoneyType"] = dr["MoneyType"].ToString();
                    drNew["ExchangeRate"] = BLL.ConvertRule.ToDecimalObj(dr["ExchangeRate"]);

                    drNew["SubjectCode"] = BLL.ConvertRule.ToString(dr["SubjectCode"]);

                    //新增付款单明细时，取请款单明细的分摊方式，更新到付款单明细  begin---------------------
                    if (drs.Length == 0)
                    {
                        string PaymentItemCode = BLL.ConvertRule.ToString(dr["PaymentItemCode"]);
                        EntityData entityPaymentItem = DAL.EntityDAO.PaymentDAO.GetPaymentItemByCode(PaymentItemCode);
                        EntityData entityPaymentItemBuilding = DAL.EntityDAO.PaymentDAO.GetPaymentItemBuildingByPaymentItemCode(PaymentItemCode);

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

                            drDst["SystemID"] = DAL.EntityDAO.SystemManageDAO.GetNewSysCode("PayoutItemBuildingCode");
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

        private bool IsArrayInclude(object[] arr, object val)
        {
            try
            {
                bool ret = false;

                foreach (object item in arr)
                {
                    if (BLL.ConvertRule.ToString(item) == BLL.ConvertRule.ToString(val))
                    {
                        ret = true;
                        break;
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_ServerClick(object sender, System.EventArgs e)
        {
            try
            {
                //新的明细表
                DataTable tbDtl = ScreenToTable(true);
                if (tbDtl == null) return;

                string Hint = "";
                if (!CheckValid(ref Hint, tbDtl))
                {
                    Response.Write(Rms.Web.JavaScript.Alert(true, Hint));
                    BindDataGrid(tbDtl);
                    return;
                }

                Save(tbDtl);
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
            DataTable tb = BLL.PaymentRule.GeneratePayoutItemTable("");
            BLL.PaymentRule.VoucherDetailAddColumnSubjectName(tb, this.txtSubjectSetCode.Value);
            tb.Columns.Add("SubjectHint", typeof(String));

            foreach (DataGridItem item in this.dgList.Items)
            {
                HtmlInputHidden txtSelect = (HtmlInputHidden)item.FindControl("txtSelect");
                HtmlInputHidden txtPayoutItemCode = (HtmlInputHidden)item.FindControl("txtPayoutItemCode");
                HtmlInputHidden txtSummary = (HtmlInputHidden)item.FindControl("txtSummary");
                HtmlInputHidden txtItemMoney = (HtmlInputHidden)item.FindControl("txtItemMoney");
                HtmlInputHidden txtTotalPayoutMoney = (HtmlInputHidden)item.FindControl("txtTotalPayoutMoney");
                HtmlInputHidden txtRemainItemMoney = (HtmlInputHidden)item.FindControl("txtRemainItemMoney");

                WebNumericEdit txtPayoutMoney = (WebNumericEdit)item.FindControl("txtPayoutMoney");
                //				HtmlInputText txtPayoutMoney = (HtmlInputText)item.FindControl("txtPayoutMoney");

                HtmlInputHidden txtCostCode = (HtmlInputHidden)item.FindControl("txtCostCode");
                HtmlInputHidden txtCostName = (HtmlInputHidden)item.FindControl("txtCostName");
                RmsPM.Web.UserControls.InputSubject ucInputSubject = (RmsPM.Web.UserControls.InputSubject)item.FindControl("ucInputSubject");

                HtmlInputHidden txtBuildingCodeAll = (HtmlInputHidden)item.FindControl("txtBuildingCodeAll");
                HtmlInputHidden txtBuildingNameAll = (HtmlInputHidden)item.FindControl("txtBuildingNameAll");

                HtmlInputHidden txtPaymentItemCode = (HtmlInputHidden)item.FindControl("txtPaymentItemCode");
                HtmlInputHidden txtPaymentID = (HtmlInputHidden)item.FindControl("txtPaymentID");

                RmsPM.Web.UserControls.ExchangeRateControl ucItemCash = (RmsPM.Web.UserControls.ExchangeRateControl)item.FindControl("ucItemCash");

                string PayoutItemCode = txtPayoutItemCode.Value;

                /*
                if ( txtPayoutMoney.Value != "")
                {
                    if ( !Rms.Check.StringCheck.IsNumber(txtPayoutMoney.Value))
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, "金额数字格式不正确 !"));
                        return null;
                    }
                }
                */

                DataRow dr = tb.NewRow();

                dr["Checked"] = BLL.ConvertRule.ToInt(txtSelect.Value);
                dr["PayoutItemCode"] = PayoutItemCode;
                dr["Summary"] = txtSummary.Value;
                dr["ItemMoney"] = BLL.ConvertRule.ToDecimalObj(txtItemMoney.Value);
                dr["TotalPayoutMoney"] = BLL.ConvertRule.ToDecimalObj(txtTotalPayoutMoney.Value);
                dr["RemainItemMoney"] = BLL.ConvertRule.ToDecimalObj(txtRemainItemMoney.Value);

                dr["PayoutCash"] = ucItemCash.Cash;
                dr["MoneyType"] = ucItemCash.MoneyType;
                dr["ExchangeRate"] = ucItemCash.ExchangeRate;

                dr["PayoutMoney"] = ucItemCash.Cash * ucItemCash.ExchangeRate;

//                dr["PayoutMoney"] = txtPayoutMoney.ValueDecimal;
//                dr["PayoutMoney"] = BLL.ConvertRule.ToDecimalObj(txtPayoutMoney.Value);

                dr["CostCode"] = txtCostCode.Value;
                dr["CostName"] = txtCostName.Value;
                dr["SubjectCode"] = ucInputSubject.Value;
                dr["SubjectName"] = ucInputSubject.Text;
                dr["SubjectHint"] = ucInputSubject.Hint;

                dr["BuildingCodeAll"] = txtBuildingCodeAll.Value;
                dr["BuildingNameAll"] = txtBuildingNameAll.Value;

                dr["PaymentItemCode"] = txtPaymentItemCode.Value;
                dr["PaymentID"] = txtPaymentID.Value;

                tb.Rows.Add(dr);
            }

            if (isBindGrid)
            {
                this.dgList.DataSource = tb;
                this.dgList.DataBind();
            }

            return tb;
        }

        private void dgList_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.AlternatingItem:
                case ListItemType.Item:
                    RmsPM.Web.UserControls.ExchangeRateControl ucItemCash = (RmsPM.Web.UserControls.ExchangeRateControl)e.Item.FindControl("ucItemCash");

                    DataRowView ud_drvItem = (DataRowView)e.Item.DataItem;

                    if (ud_drvItem["MoneyType"] != DBNull.Value)
                    {
                        ucItemCash.Cash = BLL.ConvertRule.ToDecimal(ud_drvItem["PayoutCash"]);
                        ucItemCash.MoneyType = ud_drvItem["MoneyType"].ToString();
                        ucItemCash.ExchangeRate = BLL.ConvertRule.ToDecimal(ud_drvItem["ExchangeRate"]);
                    }

                    if (ud_drvItem["Checked"] != DBNull.Value && ud_drvItem["Checked"].ToString() == "0")
                    {
                        ucItemCash.IsAllowModify = false;
                    }
                    else
                    {
                        ucItemCash.IsAllowModify = true;
                    }

                    ucItemCash.BindControl();

                    break;
                case ListItemType.Footer:
                    Label lblSumPayoutMoney = (Label)e.Item.FindControl("lblSumPayoutMoney");
                    lblSumPayoutMoney.Text = this.txtSumPayoutMoney.Value;
                    break;
            }
        }

        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void dgList_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            try
            {
                string code = this.dgList.DataKeys[e.Item.ItemIndex].ToString();

                DataTable tb = ScreenToTable(false);
                if (tb == null) return;

                DataRow[] drs = tb.Select("PayoutItemCode='" + code + "'");
                if (drs.Length > 0)
                {
                    tb.Rows.Remove(drs[0]);
                }

                BindDataGrid(tb);
            }
            catch (Exception ex)
            {
                Response.Write(JavaScript.Alert(true, "删除明细失败：" + ex.Message));
                ApplicationLog.WriteLog(this.ToString(), ex, "");
            }
        }

        private void dgList_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    RmsPM.Web.UserControls.InputSubject ucInputSubject = (RmsPM.Web.UserControls.InputSubject)e.Item.FindControl("ucInputSubject");
                    
                    ucInputSubject.ProjectCode = this.txtProjectCode.Value;

                    break;
            }
        }

    }
}
