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

public partial class WorkFlowOperation_SM_LocalPaymenAuditingV2 : WorkFlowOperationBase
{
    #region --- 公共方法 ---

    /// <summary>
    /// 控件初始化




    /// </summary>
    public override void InitControl()
    {
        try
        {
            switch (this.MoneyState)
            {
                case ModuleState.Operable:
                    break;
                default:
                    this.MoneyState = ModuleState.Eyeable;
                    break;
            }

            base.InitControl();

            switch (this.AttachmentState)
            {
                case ModuleState.Sightless://不可见的
                    trOperAttachment.Visible = false;
                    trEyeAttachment.Visible = false;
                    break;

                case ModuleState.End://可见的




                case ModuleState.Begin://可见的




                case ModuleState.Operable://可见的




                case ModuleState.Eyeable://可见的




                    trOperAttachment.Visible = true;
                    trEyeAttachment.Visible = true;
                    break;

                default:
                    trOperAttachment.Visible = false;
                    trEyeAttachment.Visible = false;
                    break;
            }


        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }


    /// <summary>
    /// 装载控件数据
    /// </summary>
    public override void LoadData()
    {
        try
        {

            if (this.ApplicationCode != "")
            {
                this.OperationCode = this.ApplicationCode;
            }
            else if (this.OperationCode != "")
            {
                this.ApplicationCode = this.OperationCode;
            }
            else
            {
                return;
            }

            string ud_sPaymentHyperLinkFormat = "../Finance/PaymentInfo.aspx?PaymentCode={0}&ProjectCode={1}";
            string ud_sContractHyperLinkFormat = "../Contract/ContractInfo.aspx?ContractCode={0}&ProjectCode={1}";

            EntityData entity = RmsPM.DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(this.OperationCode, this.dao);

            decimal ud_deTotalMoney, ud_deTotalChangeMoney, ud_deOriginalMoney, ud_deBudgetMoney, ud_deAdjustMoney;
            decimal ud_deNegAHMoney, ud_deTotalItemMoney, ud_deTotalPayMoney;

            ud_deTotalMoney = decimal.Zero;
            ud_deTotalChangeMoney = decimal.Zero;
            ud_deOriginalMoney = decimal.Zero;
            ud_deBudgetMoney = decimal.Zero;
            ud_deAdjustMoney = decimal.Zero;

            ud_deNegAHMoney = decimal.Zero;
            ud_deTotalItemMoney = decimal.Zero;
            ud_deTotalPayMoney = decimal.Zero;


            if (entity.HasRecord())
            {
                this.ProjectCode = entity.GetString("ProjectCode");
                this.ApplicationType = RmsPM.BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(entity.GetString("GroupCode"));
                //this.ApplicationTitle = "请款单号：" + entity.GetString("PaymentCode");
                //                this.UnitCode = entity.GetString("Unit");

                this.lblPaymentName.Text = entity.GetString("PaymentName");
                this.ApplicationTitle = entity.GetString("PaymentName");
                this.lblOperProjectName.Text = RmsPM.BLL.ProjectRule.GetProjectName(this.ProjectCode);
                this.lblEyeProjectName.Text = this.lblOperProjectName.Text;




                //    //显示合同信息
                //    EntityData entityCon = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode);
                //    if (entityCon.HasRecord())
                //    {


                //        this.lblOperContractName.Text = ShowApplicationHyperLink(entityCon.GetString("ContractName"), string.Format(ud_sContractHyperLinkFormat, this.ContractCode, this.ProjectCode)); //entityCon.GetString("ContractName");
                //        this.lblEyeContractName.Text = this.lblOperContractName.Text;



                //        this.lblOperContractID.Text = entityCon.GetString("ContractID");
                //        this.lblEyeContractID.Text = this.lblOperContractID.Text;

                //        this.lblOperSupplier2Name.Text = RmsPM.BLL.ProjectRule.GetSupplierName(entityCon.GetString("Supplier2Code"));
                //        this.lblEyeSupplier2Name.Text = this.lblOperSupplier2Name.Text;

                //        this.lblOperBuilding.Text = entityCon.GetString("Building");
                //        this.lblEyeBuilding.Text = this.lblOperBuilding.Text;

                //        //显示合同金额

                //        ud_deTotalMoney = entityCon.GetDecimal("TotalMoney");
                //        ud_deOriginalMoney = entityCon.GetDecimal("OriginalMoney");
                //        ud_deBudgetMoney = entityCon.GetDecimal("BudgetMoney");
                //        ud_deAdjustMoney = entityCon.GetDecimal("AdjustMoney");

                //        ud_deTotalChangeMoney = ud_deTotalMoney - ud_deOriginalMoney;

                //        switch (this.MoneyState)
                //        {
                //            case ModuleState.Sightless://不可见的
                //            case ModuleState.Begin://不可见的
                //            case ModuleState.End://不可见的
                //                this.txtOperOriginalMoney.Value = "***************";
                //                this.txtOperTotalChangeMoney.Value = "***************";
                //                this.txtOperNewTotalMoney.Value = "***************";
                //                this.txtOperBudgetMoney.Value = "***************";
                //                this.txtOperAdjustMoney.Value = "***************";
                //                break;

                //            case ModuleState.Operable://可操作的
                //            case ModuleState.Eyeable://可见的




                //                this.txtOperOriginalMoney.Value = ud_deOriginalMoney.ToString("N");
                //                this.txtOperTotalChangeMoney.Value = ud_deTotalChangeMoney.ToString("N");
                //                this.txtOperNewTotalMoney.Value = ud_deTotalMoney.ToString("N");
                //                this.txtOperBudgetMoney.Value = ud_deBudgetMoney.ToString("N");
                //                this.txtOperAdjustMoney.Value = ud_deAdjustMoney.ToString("N");
                //                break;

                //            default:
                //                this.tabOperMoney.Visible = false;
                //                this.tabEyeMoney.Visible = false;
                //                break;
                //        }

                //        this.txtEyeOriginalMoney.Value = this.txtOperOriginalMoney.Value;
                //        this.txtEyeTotalChangeMoney.Value = this.txtOperTotalChangeMoney.Value;
                //        this.txtEyeNewTotalMoney.Value = this.txtOperNewTotalMoney.Value;
                //        this.txtEyeBudgetMoney.Value = this.txtOperBudgetMoney.Value;
                //        this.txtEyeAdjustMoney.Value = this.txtOperAdjustMoney.Value;
                //    }
                //    entityCon.Dispose();

                this.lblOperSupplierName.Text = entity.GetString("SupplyName");
                this.lblEyeSupplierName.Text = lblOperSupplierName.Text;

                this.txtOperCheckOpinion.Value = entity.GetString("CheckRemark");
                this.lblEyeCheckOpinion.Text = HttpUtility.HtmlEncode(txtOperCheckOpinion.Value).Replace("\n", "<br>");



                ud_deTotalItemMoney = entity.GetDecimal("Money");

                ud_deNegAHMoney = -entity.GetDecimal("AHMoney");
                ud_deTotalPayMoney = entity.GetDecimal("TotalPayMoney");

                ud_deTotalPayMoney = ud_deTotalItemMoney - ud_deNegAHMoney;

                this.txtOperTotalPayMoney.Value = ud_deTotalPayMoney.ToString("N");
                this.txtEyeTotalPayMoney.Value = this.txtOperTotalPayMoney.Value;

                this.txtOperNegAHMoney.Value = ud_deNegAHMoney.ToString("N");
                this.txtEyeNegAHMoney.Value = this.txtOperNegAHMoney.Value;

                this.txtEyeTotalItemMoney.Value = ud_deTotalItemMoney.ToString("N");
                this.txtOperTotalItemMoney.Value = this.txtEyeTotalItemMoney.Value;

                this.txtOperBankName.Value = entity.GetString("BankName");
                this.lblEyeBankName.Text = this.txtOperBankName.Value;

                this.txtOperBankAccount.Value = entity.GetString("BankAccount");
                this.lblEyeBankAccount.Text = this.txtOperBankAccount.Value;

                this.dtOperInputDate.Value = entity.GetDateTimeOnlyDate("ApplyDate");
                this.dtEyeInputDate.Value = this.dtOperInputDate.Value;

                this.dtOperPayDate.Value = entity.GetDateTimeOnlyDate("PayDate");
                this.dtEyePayDate.Value = this.dtOperPayDate.Value;

                this.txtOperOtherAttachMent.Text = entity.GetString("OtherAttachMent");
                this.txtEyeOtherAttachMent.Text = this.txtOperOtherAttachMent.Text;

                this.txtOperIssueMode.Value = entity.GetString("IssueMode").Trim() != "" ? entity.GetString("IssueMode") : RmsPM.BLL.PaymentRule.GetIssueByContractCode(this.ContractCode).ToString();
                this.lblEyeIssueMode.Text = this.txtOperIssueMode.Value;

                this.txtOperIssue.Value = entity.GetInt("Issue").ToString();
                this.lblEyeIssue.Text = this.txtOperIssue.Value;

                this.rdoOperPayType.SelectedIndex = this.rdoOperPayType.Items.IndexOf(this.rdoOperPayType.Items.FindByValue(entity.GetInt("PayType").ToString()));
                this.rdoEyePayType.SelectedIndex = this.rdoEyePayType.Items.IndexOf(this.rdoEyePayType.Items.FindByValue(entity.GetInt("PayType").ToString()));
                this.HyperLink1.NavigateUrl = string.Format(ud_sPaymentHyperLinkFormat, this.ApplicationCode, this.ProjectCode);// "..//Finance/PaymentInfo.aspx?ProjectCode=100003&PaymentCode=" + this.ApplicationCode;


            }
            entity.Dispose();

            //业务流程属性保存




            SaveOperationProperty("单一付款", ud_deTotalItemMoney.ToString());

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
        }
    }


    /// <summary>
    /// 保存控件数据
    /// </summary>
    public override string SubmitData()
    {
        try
        {
            string ErrMsg = "";

            EntityData entity = new EntityData("Standard_Payment");

            if (UserCode == "")
            {
                ErrMsg = "操作用户为空";
                return ErrMsg;
            }

            if (txtOperCheckOpinion.Value.Trim() == "")
            {
                ErrMsg = "请填写合同履约情况";
                return ErrMsg;
            }

            //if ( txtOperBankName.Value.Trim() == "" )
            //{
            //    ErrMsg = "请填写收款银行名称";
            //    return ErrMsg;
            //}

            //if ( txtOperBankAccount.Value.Trim() == "" )
            //{
            //    ErrMsg = "请填写收款单位银行帐号";
            //    return ErrMsg;
            //}

            if (rdoOperPayType.SelectedIndex == -1)
            {
                ErrMsg = "请选择付款方式";
                return ErrMsg;
            }

            if (dtOperPayDate.Value.Trim() == "")
            {
                ErrMsg = "请填写最晚付款日期";
                return ErrMsg;
            }


            if (txtOperIssue.Value != "")
            {
                if (!Rms.Check.StringCheck.IsInt(this.txtOperIssue.Value))
                {
                    ErrMsg = "付款期数必须是整数";
                    return ErrMsg;
                }
            }

            if (dao == null)
            {
                entity = RmsPM.DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(this.OperationCode);
            }
            else
            {
                entity = RmsPM.DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(this.OperationCode, dao);
            }


            //请款单基本信息




            entity.SetCurrentTable("Payment");

            foreach (DataRow dr in entity.CurrentTable.Select(String.Format("PaymentCode='{0}'", this.OperationCode), "", DataViewRowState.CurrentRows))
            {

                dr["CheckRemark"] = txtOperCheckOpinion.Value;
                dr["BankName"] = txtOperBankName.Value;
                dr["BankAccount"] = txtOperBankAccount.Value;
                dr["PayDate"] = dtOperPayDate.Value;
                dr["OtherAttachMent"] = txtOperOtherAttachMent.Text;
                dr["PayType"] = rdoOperPayType.SelectedValue;
                dr["Issue"] = RmsPM.BLL.ConvertRule.ToIntObj(txtOperIssue.Value.Trim());
                dr["IssueMode"] = txtOperIssueMode.Value.Trim();

                dr["ApplyDate"] = dtOperInputDate.Value;

                //                dr["status"] = 3;
            }

            dao.SubmitEntity(entity);
            entity.Dispose();

            return ErrMsg;
        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
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

                EntityData entity = RmsPM.DAL.EntityDAO.PaymentDAO.GetStandard_PaymentByCode(this.OperationCode, this.dao);

                entity.SetCurrentTable("Payment");

                if (entity.HasRecord())
                {
                    DataRow dr = entity.CurrentRow;

                    switch (pm_sOpinionConfirm)
                    {
                        case "Approve":
                            dr["Status"] = 1;
                            break;
                        case "Reject":
                            dr["Status"] = 0;
                            break;
                        case "Unknow":
                            ErrMsg = "请选择评审结果！";
                            break;
                        default:
                            ErrMsg = "请选择评审结果！";
                            break;
                    }
                    dr["CheckDate"] = DateTime.Now;
                    dr["CheckPerson"] = base.UserCode;

                    if (ErrMsg != "")
                    {
                        Response.Write(Rms.Web.JavaScript.Alert(true, ErrMsg));
                        return false;
                    }

                    RmsPM.DAL.EntityDAO.PaymentDAO.UpdateStandard_Payment(entity);

                }

                entity.Dispose();
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

            RmsPM.BLL.PaymentRule.PaymentStatusChange(dao, this.OperationCode, 3);

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

            RmsPM.BLL.PaymentRule.PaymentStatusChange(this.OperationCode, 0);

            return ErrMsg;
        }
        catch (Exception ex)
        {
            RmsPM.Web.ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "恢复业务数据状态出错：" + ex.Message));
            throw ex;
        }
    }


    #endregion --- 公共方法 ---

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面




        if (this.ApplicationCode != "")
        {
            //附件
            this.myOperAttachMentAdd1.AttachMentType = "PaymentCheckAttachMent1";
            this.myOperAttachMentAdd1.MasterCode = this.ApplicationCode;
            this.myOperAttachMentAdd2.AttachMentType = "PaymentCheckAttachMent2";
            this.myOperAttachMentAdd2.MasterCode = this.ApplicationCode;
            this.myOperAttachMentAdd3.AttachMentType = "PaymentCheckAttachMent3";
            this.myOperAttachMentAdd3.MasterCode = this.ApplicationCode;
            this.myOperAttachMentAdd4.AttachMentType = "PaymentCheckAttachMent4";
            this.myOperAttachMentAdd4.MasterCode = this.ApplicationCode;
            this.myOperAttachMentAdd5.AttachMentType = "PaymentCheckAttachMent5";
            this.myOperAttachMentAdd5.MasterCode = this.ApplicationCode;
            this.myOperAttachMentAdd6.AttachMentType = "PaymentCheckAttachMent6";
            this.myOperAttachMentAdd6.MasterCode = this.ApplicationCode;
            this.myOperAttachMentAdd7.AttachMentType = "PaymentCheckAttachMent7";
            this.myOperAttachMentAdd7.MasterCode = this.ApplicationCode;

            this.myEyeAttachMentList1.AttachMentType = "PaymentCheckAttachMent1";
            this.myEyeAttachMentList1.MasterCode = this.ApplicationCode;
            this.myEyeAttachMentList2.AttachMentType = "PaymentCheckAttachMent2";
            this.myEyeAttachMentList2.MasterCode = this.ApplicationCode;
            this.myEyeAttachMentList3.AttachMentType = "PaymentCheckAttachMent3";
            this.myEyeAttachMentList3.MasterCode = this.ApplicationCode;
            this.myEyeAttachMentList4.AttachMentType = "PaymentCheckAttachMent4";
            this.myEyeAttachMentList4.MasterCode = this.ApplicationCode;
            this.myEyeAttachMentList5.AttachMentType = "PaymentCheckAttachMent5";
            this.myEyeAttachMentList5.MasterCode = this.ApplicationCode;
            this.myEyeAttachMentList6.AttachMentType = "PaymentCheckAttachMent6";
            this.myEyeAttachMentList6.MasterCode = this.ApplicationCode;
            this.myEyeAttachMentList7.AttachMentType = "PaymentCheckAttachMent7";
            this.myEyeAttachMentList7.MasterCode = this.ApplicationCode;

            if (myEyeAttachMentList1.Count > 0)
            {
                chkOperAttachMent1.Checked = true;
                chkEyeAttachMent1.Checked = true;
            }
            if (myEyeAttachMentList2.Count > 0)
            {
                chkOperAttachMent2.Checked = true;
                chkEyeAttachMent2.Checked = true;
            }
            if (myEyeAttachMentList3.Count > 0)
            {
                chkOperAttachMent3.Checked = true;
                chkEyeAttachMent3.Checked = true;
            }
            if (myEyeAttachMentList4.Count > 0)
            {
                chkOperAttachMent4.Checked = true;
                chkEyeAttachMent4.Checked = true;
            }
            if (myEyeAttachMentList5.Count > 0)
            {
                chkOperAttachMent5.Checked = true;
                chkEyeAttachMent5.Checked = true;
            }
            if (myEyeAttachMentList6.Count > 0)
            {
                chkOperAttachMent6.Checked = true;
                chkEyeAttachMent6.Checked = true;
            }
            if (myEyeAttachMentList7.Count > 0)
            {
                chkOperAttachMent7.Checked = true;
                chkEyeAttachMent7.Checked = true;
            }

        }
    }
}
