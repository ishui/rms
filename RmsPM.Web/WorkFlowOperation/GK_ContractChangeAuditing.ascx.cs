
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using RmsPM.Web.WorkFlowControl;
	using Rms.ORMap;
    using RmsPM.Web;

	/// <summary>
	///		GK_ContractChangeAuditing 的摘要说明。
	/// </summary>
public partial class WorkFlowOperation_GK_ContractChangeAuditing : WorkFlowOperationBase
{

 

    #region --- 公共方法 ---

    /// <summary>
    /// 控件初始化
    /// </summary>
    public override void InitControl()
    {
        try
        {
            base.InitControl();

            switch (this.AttachmentState)
            {
                case ModuleState.Sightless://不可见的
                    trOperAttachment.Visible = false;
                    trEyeAttachment.Visible = false;
                    break;

                case ModuleState.Begin://可见的
                case ModuleState.End://可见的
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

            this.ContractCode = RmsPM.BLL.ContractRule.GetContractCodeByChangeCode(this.OperationCode);

            EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode);

            entity.SetCurrentTable("Contract");

            if (!entity.HasRecord())
            {
                return;
            }

            this.ProjectCode = entity.GetString("ProjectCode");
            this.ApplicationType = RmsPM.BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(entity.GetString("Type"));
            this.ApplicationTitle = entity.GetString("ContractName");

            string ud_sHyperLinkFormat = "../Contract/ContractInfo.aspx?ContractCode={0}&ProjectCode={1}";

            decimal ud_deMoney, ud_deTotalChangeMoney, ud_deChangeMoney;

            ud_deMoney = decimal.Zero;
            ud_deTotalChangeMoney = decimal.Zero;
            ud_deChangeMoney = decimal.Zero;


            //合同基本信息
            lblProjectName.Text = RmsPM.BLL.ProjectRule.GetProjectName(this.ProjectCode);
            lblContractID.Text = entity.GetString("ContractID");

            lblOperContractName.Text = ShowApplicationHyperLink(entity.GetString("ContractName"), string.Format(ud_sHyperLinkFormat, this.ContractCode, this.ProjectCode)); 
            lblEyeContractName.Text = lblOperContractName.Text;

            lblOperSupplierName.Text = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"));
            lblEyeSupplierName.Text = lblOperSupplierName.Text;

            lblOperSupplier2Name.Text = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("Supplier2Code"));
            lblEyeSupplier2Name.Text = lblOperSupplier2Name.Text;

            lblOperBuilding.Text = entity.GetString("Building");
            lblEyeBuilding.Text = lblOperBuilding.Text;

            //显示合同金额
            decimal TotalMoney, OriginalMoney, NewTotalMoney, BudgetMoney, AdjustMoney;

            TotalMoney = entity.GetDecimal("TotalMoney");
            OriginalMoney = entity.GetDecimal("OriginalMoney");
            BudgetMoney = entity.GetDecimal("BudgetMoney");
            AdjustMoney = entity.GetDecimal("AdjustMoney");

            NewTotalMoney = TotalMoney;

            ud_deMoney = TotalMoney;

            foreach (DataRow dr in entity.Tables["ContractChange"].Select(string.Format("ContractChangeCode={0}", this.ApplicationCode), "", System.Data.DataViewRowState.CurrentRows))
            {
                NewTotalMoney = (decimal)dr["NewMoney"];

                ud_deTotalChangeMoney = (decimal)dr["TotalChangeMoney"];
                ud_deChangeMoney = (decimal)dr["ChangeMoney"];
            }


             

            switch (this.MoneyState)
            {
                case ModuleState.Sightless://不可见的
                case ModuleState.Begin://不可见的
                case ModuleState.End://不可见的

                    txtOperOriginalMoney.Value = "***************";
                    txtOperTotalChangeMoney.Value = "***************";
                    txtOperChangeMoney.Value = "***************";
                    txtOperNewTotalMoney.Value = "***************";
                    txtOperBudgetMoney.Value = "***************";
                    txtOperAdjustMoney.Value = "***************";
                    break;

                case ModuleState.Operable://可操作的
                case ModuleState.Eyeable://可见的

                    txtOperOriginalMoney.Value = OriginalMoney.ToString("N");
                    txtOperTotalChangeMoney.Value = ud_deTotalChangeMoney.ToString("N");
                    txtOperChangeMoney.Value = ud_deChangeMoney.ToString("N");
                    txtOperNewTotalMoney.Value = NewTotalMoney.ToString("N");
                    txtOperBudgetMoney.Value = BudgetMoney.ToString("N");
                    txtOperAdjustMoney.Value = AdjustMoney.ToString("N");
                    break;

                default:
                    tabOperMoney.Visible = false;
                    tabEyeMoney.Visible = false;
                    break;
            }



            txtEyeOriginalMoney.Value = txtOperOriginalMoney.Value;
            txtEyeTotalChangeMoney.Value = txtOperTotalChangeMoney.Value;
            txtEyeChangeMoney.Value = txtOperChangeMoney.Value;
            txtEyeNewTotalMoney.Value = txtOperNewTotalMoney.Value;
            txtEyeBudgetMoney.Value = txtOperBudgetMoney.Value;
            txtEyeAdjustMoney.Value = txtOperAdjustMoney.Value;


            //合同变更信息
            entity.SetCurrentTable("ContractChange");
            foreach (DataRow dr in entity.CurrentTable.Select(String.Format("ContractChangeCode='{0}'", this.ApplicationCode)))
            {
                txtOperVoucher.Value = dr["Voucher"].ToString();
                lblEyeVoucher.Text = txtOperVoucher.Value;

                txtOperChangeId.Value = dr["ContractChangeId"].ToString();
                lblEyeChangeId.Text = txtOperChangeId.Value;

                txtOperChangeReason.Value = dr["ChangeReason"].ToString();
                lblEyeChangeReason.Text = HttpUtility.HtmlEncode(txtOperChangeReason.Value).Replace("\n", "<br>");

                txtOperSupplierChangeMoney.Value = dr["SupplierChangeMoney"].ToString();
                lblEyeSupplierChangeMoney.Text = txtOperSupplierChangeMoney.Text;

                txtOperConsultantAuditMoney.Value = dr["ConsultantAuditMoney"].ToString();
                lblEyeConsultantAuditMoney.Text = txtOperConsultantAuditMoney.Text;

                txtOperProjectAuditMoney.Value = dr["ProjectAuditMoney"].ToString();
                lblEyeProjectAuditMoney.Text = txtOperProjectAuditMoney.Text;
            }

            entity.Dispose();

            //业务流程属性保存
            SaveOperationProperty("合同金额", ud_deMoney.ToString());
            SaveOperationProperty("单一变更", ud_deChangeMoney.ToString());
            SaveOperationProperty("累计变更", ud_deTotalChangeMoney.ToString()); 

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

            EntityData entity = new EntityData("Standard_Contract");

            if (UserCode == "")
            {
                ErrMsg = "操作用户为空";
                return ErrMsg;
            }

            if (dao == null)
            {
                entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode);
            }
            else
            {
                entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode, dao);
            }


            //合同变更基本信息
            entity.SetCurrentTable("ContractChange");

            foreach (DataRow dr in entity.Tables["ContractChange"].Select(String.Format("ContractChangeCode='{0}'", this.ApplicationCode), "", DataViewRowState.CurrentRows))
            {
                dr["Voucher"] = txtOperVoucher.Value;
                dr["ContractChangeId"] = txtOperChangeId.Value;
                dr["ChangeReason"] = txtOperChangeReason.Value;

                dr["SupplierChangeMoney"] = txtOperSupplierChangeMoney.ValueDecimal;
                dr["ConsultantAuditMoney"] = txtOperConsultantAuditMoney.ValueDecimal;
                dr["ProjectAuditMoney"] = txtOperProjectAuditMoney.ValueDecimal;

                // 记录变更人、变更时间
                dr["ChangePerson"] = this.UserCode;
                dr["ChangeDate"] = DateTime.Now.ToString("yyyy-MM-dd");

//                dr["Status"] = 2;
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

                EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode, this.dao);

                entity.SetCurrentTable("Contract");

                if (entity.HasRecord())
                {

                    switch (pm_sOpinionConfirm)
                    {
                        case "Approve":
                            RmsPM.BLL.ContractRule.ContractChangeAuditing(entity, this.OperationCode);
                            break;
                        case "Reject":
                            foreach (DataRow drCostChange in entity.Tables["ContractCostChange"].Select(String.Format("ContractChangeCode='{0}'", this.OperationCode), "", DataViewRowState.CurrentRows))
                            {
                                drCostChange["Status"] = 1;
                            }

                            foreach (DataRow drChange in entity.Tables["ContractChange"].Select(String.Format("ContractChangeCode='{0}'", this.OperationCode), "", DataViewRowState.CurrentRows))
                            {
                                drChange["Status"] = 1;

                                foreach (DataRow dr in entity.Tables["Contract"].Select(String.Format("ContractCode='{0}'", this.ContractCode), "", DataViewRowState.CurrentRows))
                                {
                                    dr["ChangeStatus"] = 1;
                                }
                            }
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

                    RmsPM.DAL.EntityDAO.ContractDAO.UpdateStandard_Contract(entity);

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

            RmsPM.BLL.ContractRule.ContractChangeStatusChange(dao, this.OperationCode, 2);

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

            RmsPM.BLL.ContractRule.ContractChangeStatusChange(dao, this.OperationCode, 1);

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
            this.myOperAttachMentAdd.AttachMentType = "ContractChangeAttachMent";
            this.myOperAttachMentAdd.MasterCode = this.ApplicationCode;

            this.myEyeAttachMentList.AttachMentType = "ContractChangeAttachMent";
            this.myEyeAttachMentList.MasterCode = this.ApplicationCode;
        }
    }

  
}