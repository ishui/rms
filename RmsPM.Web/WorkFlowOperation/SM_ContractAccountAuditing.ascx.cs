
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using RmsPM.Web.WorkFlowControl;
using Rms.ORMap;
using RmsPM.Web;
using RmsPM.BLL;


/// <summary>
///		SM_ContractAccountAuditing 的摘要说明。
/// </summary>
public partial class WorkFlowOperation_SM_ContractAccountAuditing : WorkFlowOperationBase
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

            this.trOperEstimateChangeMoney.Visible = false;
            this.trEyeEstimateChangeMoney.Visible = false;

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
            string ud_sAction = "";

            if (this.ApplicationCode != "")
            {
                this.OperationCode = RmsPM.BLL.ContractRule.GetContractCodeByAccountCode(this.ApplicationCode);

            }
            else if (this.OperationCode != "")
            {
                this.ApplicationCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractAccountCode");
                ud_sAction = "Add";
            }
            else
            {
                return;
            }

            this.ContractCode = this.OperationCode;

            EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode);

            if (ud_sAction == "Add")
            {
                //新增

                DataRow drAdd = entity.GetNewRecord("ContractAccount");
                drAdd["ContractAccountCode"] = this.ApplicationCode;
                drAdd["ContractCode"] = this.ContractCode;
                drAdd["Status"] = 0;
                drAdd["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                drAdd["CreatePerson"] = this.UserCode;

                entity.Tables["ContractAccount"].Rows.Add(drAdd);
            }

            entity.SetCurrentTable("Contract");

            if (!entity.HasRecord())
            {
                return;
            }

            this.ProjectCode = entity.GetString("ProjectCode");
            this.ApplicationType = RmsPM.BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(entity.GetString("Type"));
            this.ApplicationTitle = entity.GetString("ContractName");


            string ud_sHyperLinkFormat = "../Contract/ContractInfo.aspx?ContractCode={0}&ProjectCode={1}";

            //合同基本信息
            lblOperProjectName.Text = RmsPM.BLL.ProjectRule.GetProjectName(this.ProjectCode);
            lblEyeProjectName.Text = lblOperProjectName.Text;

            lblOperContractID.Text = entity.GetString("ContractID");
            lblEyeContractID.Text = lblOperContractID.Text;

            lblOperContractName.Text = ShowApplicationHyperLink(entity.GetString("ContractName"), string.Format(ud_sHyperLinkFormat, this.ContractCode, this.ProjectCode)); //entity.GetString("ContractName");
            lblEyeContractName.Text = lblOperContractName.Text;

            lblOperSupplierName.Text = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"));
            lblEyeSupplierName.Text = lblOperSupplierName.Text;

            lblOperSupplier2Name.Text = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("Supplier2Code"));
            lblEyeSupplier2Name.Text = lblOperSupplier2Name.Text;

            lblOperBuilding.Text = entity.GetString("Building");
            lblEyeBuilding.Text = lblOperBuilding.Text;

            //显示合同金额
            decimal TotalMoney, TotalChangeMoney, OriginalMoney, ChangeMoney, BudgetMoney, AdjustMoney, EstimateChangeMoney;

            TotalMoney = entity.GetDecimal("TotalMoney");
            OriginalMoney = entity.GetDecimal("OriginalMoney");
            BudgetMoney = entity.GetDecimal("BudgetMoney");
            AdjustMoney = entity.GetDecimal("AdjustMoney");

            TotalChangeMoney = TotalMoney - OriginalMoney;



            ChangeMoney = RmsPM.BLL.MathRule.SumColumn(entity.Tables["ContractChange"].Select("Status in (1,2)"), "ChangeMoney");
            EstimateChangeMoney = TotalChangeMoney + ChangeMoney;


            switch (this.MoneyState)
            {
                case ModuleState.Sightless://不可见的
                case ModuleState.Begin://不可见的
                case ModuleState.End://不可见的

                    txtOperOriginalMoney.Value = "***************";
                    txtOperTotalChangeMoney.Value = "***************";
                    txtOperNewTotalMoney.Value = "***************";
                    txtOperBudgetMoney.Value = "***************";
                    txtOperAdjustMoney.Value = "***************";
                    txtOperEstimateChangeMoney.Value = "***************";
                    break;

                case ModuleState.Operable://可操作的
                case ModuleState.Eyeable://可见的

                    txtOperOriginalMoney.Value = OriginalMoney.ToString("N");
                    txtOperTotalChangeMoney.Value = TotalChangeMoney.ToString("N");
                    txtOperNewTotalMoney.Value = TotalMoney.ToString("N");
                    txtOperBudgetMoney.Value = BudgetMoney.ToString("N");
                    txtOperAdjustMoney.Value = AdjustMoney.ToString("N");
                    txtOperEstimateChangeMoney.Value = EstimateChangeMoney.ToString("N");

                    break;

                default:
                    tabOperMoney.Visible = false;
                    tabEyeMoney.Visible = false;
                    break;
            }

            txtEyeOriginalMoney.Value = txtOperOriginalMoney.Value;
            txtEyeTotalChangeMoney.Value = txtOperTotalChangeMoney.Value;
            txtEyeNewTotalMoney.Value = txtOperNewTotalMoney.Value;
            txtEyeBudgetMoney.Value = txtOperBudgetMoney.Value;
            txtEyeAdjustMoney.Value = txtOperAdjustMoney.Value;
            txtEyeEstimateChangeMoney.Value = txtOperEstimateChangeMoney.Value;

            hidOriginalMoney.Value = OriginalMoney.ToString();

            //1.原合同金额
            txtOperSupplierOriginalMoney.Value = OriginalMoney.ToString("N");
            txtEyeSupplierOriginalMoney.Value = txtOperSupplierOriginalMoney.Value;

            txtOperConsultantOriginalMoney.Value = OriginalMoney.ToString("N");
            txtEyeConsultantOriginalMoney.Value = txtOperConsultantOriginalMoney.Value;

            txtOperProjectOriginalMoney.Value = OriginalMoney.ToString("N");
            txtEyeProjectOriginalMoney.Value = txtOperProjectOriginalMoney.Value;


            //合同结算信息
            entity.SetCurrentTable("ContractAccount");
            foreach (DataRow dr in entity.CurrentTable.Select(String.Format("ContractAccountCode='{0}'", this.ApplicationCode)))
            {
                txtOperContractAccountID.Value = dr["ContractAccountID"].ToString();
                lblEyeContractAccountID.Text = txtOperContractAccountID.Value;

                txtOperReason.Value = dr["Reason"].ToString();
                lblEyeReason.Text = HttpUtility.HtmlEncode(txtOperReason.Value).Replace("\n", "<br>");

                decimal SupplierTotalChangeMoney, ConsultantTotalAuditMoney, ProjectTotalAuditMoney;
                decimal SupplierTotalMoney, ConsultantTotalMoney, ProjectTotalMoney;

                SupplierTotalChangeMoney = RmsPM.BLL.MathRule.SumColumn(entity.Tables["ContractChange"].Select(String.Format("ContractCode='{0}' and Status=0", this.ContractCode)), "SupplierChangeMoney");
                ConsultantTotalAuditMoney = RmsPM.BLL.MathRule.SumColumn(entity.Tables["ContractChange"].Select(String.Format("ContractCode='{0}' and Status=0", this.ContractCode)), "ConsultantAuditMoney");
                ProjectTotalAuditMoney = RmsPM.BLL.MathRule.SumColumn(entity.Tables["ContractChange"].Select(String.Format("ContractCode='{0}' and Status=0", this.ContractCode)), "ProjectAuditMoney");

                SupplierTotalMoney = OriginalMoney + SupplierTotalChangeMoney;
                ConsultantTotalMoney = OriginalMoney + ConsultantTotalAuditMoney;
                ProjectTotalMoney = OriginalMoney + ProjectTotalAuditMoney;

                //2.变更总额
                txtOperSupplierTotalChangeMoney.Value = SupplierTotalChangeMoney.ToString("N");
                txtEyeSupplierTotalChangeMoney.Value = txtOperSupplierTotalChangeMoney.Value;

                txtOperConsultantTotalAuditMoney.Value = ConsultantTotalAuditMoney.ToString("N");
                txtEyeConsultantTotalAuditMoney.Value = txtOperConsultantTotalAuditMoney.Value;

                txtOperProjectTotalAuditMoney.Value = ProjectTotalAuditMoney.ToString("N");
                txtEyeProjectTotalAuditMoney.Value = txtOperProjectTotalAuditMoney.Value;

                //3. 其他(调整/扣款)
                txtOperSupplierTotalMoney.Value = SupplierTotalMoney.ToString("N");
                txtEyeSupplierTotalMoney.Value = txtOperSupplierTotalMoney.Value;

                txtOperConsultantTotalMoney.Value = ConsultantTotalMoney.ToString("N");
                txtEyeConsultantTotalMoney.Value = txtOperConsultantTotalMoney.Value;

                //string str = Request["ContractChangeCode"];
                //if (String.IsNullOrEmpty(str)) str = entity.Tables["ContractAccount"].Rows[0]["contractchangeid"].ToString();


                //foreach (DataRow dr in entity.Tables["contractchange"].Select(String.Format("ContractChangeCode='{0}'", str), "", DataViewRowState.CurrentRows))
                //{

                //}

                //txtOperProjectAdjustMoney.Value = "";



                txtOperProjectTotalMoney.Value = ProjectTotalMoney.ToString("N");
                txtEyeProjectTotalMoney.Value = txtOperProjectTotalMoney.Value;

            }
            //业务流程属性保存


            string ContractChangeCode = Request["ContractChangeCode"];
            if (String.IsNullOrEmpty(ContractChangeCode))
            {

                string temp = string.Empty;
                foreach (DataRow dr in entity.Tables["ContractAccount"].Select(String.Format("ContractAccountCode='{0}'", this.ApplicationCode), "", DataViewRowState.CurrentRows))
                {
                    temp = dr["contractchangeid"].ToString();
                }

                ContractChangeCode = temp;

            }

            decimal sum = 0;
            foreach (DataRow dr in entity.Tables["ContractCostChange"].Select(String.Format("ContractChangeCode='{0}'", ContractChangeCode), "", DataViewRowState.CurrentRows))
            {
                try
                {
                    sum += RmsPM.BLL.ConvertRule.ToDecimal(dr["changemoney"]);
                }
                catch { }
            }
            try
            {
                txtOperProjectAdjustMoney.Value = sum.ToString("N");
                txtEyeProjectAdjustMoney.Value = sum.ToString("N");
            }
            catch { }
            try
            {
                txtOperProjectTotalMoney.Value = (RmsPM.BLL.ConvertRule.ToDecimal(txtOperProjectTotalMoney.Value) + sum).ToString("N");
                txtEyeProjectTotalMoney.Value = (RmsPM.BLL.ConvertRule.ToDecimal(txtOperProjectTotalMoney.Value) + sum).ToString("N");
            }
            catch { }

            if (!String.IsNullOrEmpty(ContractChangeCode) &&ContractChangeCode !="0")
            {
                int changecount = 0;
                try
                {
                    changecount = entity.Tables["contractchange"].Rows.Count;
                    txtOperSupplierAdjustMoney.Value = RmsPM.BLL.ConvertRule.ToDecimal(entity.Tables["contractchange"].Rows[changecount - 1]["SupplierChangeMoney"].ToString()).ToString("N");
                }
                catch { }

                try
                {
                    txtOperConsultantAdjustMoney.Value = RmsPM.BLL.ConvertRule.ToDecimal(entity.Tables["contractchange"].Rows[changecount - 1]["ConsultantAuditMoney"].ToString()).ToString("N");
                }
                catch { }
                txtEyeSupplierAdjustMoney.Value = txtOperSupplierAdjustMoney.Value;
                txtEyeConsultantAdjustMoney.Value = txtOperConsultantAdjustMoney.Value;
            }


            SaveOperationProperty("合同金额", OriginalMoney.ToString());

            //xyq 2008.2.8  累计变更要包括本次结算变更金额
            SaveOperationProperty("累计变更", (TotalChangeMoney + sum).ToString());
            //            SaveOperationProperty("累计变更", TotalChangeMoney.ToString());

            SaveOperationProperty("结算金额", sum.ToString());

            entity.Dispose();

        }
        catch (Exception ex)
        {
            ApplicationLog.WriteLog(this.ToString(), ex, "");
            Response.Write(Rms.Web.JavaScript.Alert(true, "读取合同结算信息出错：" + ex.Message));
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

            if (this.UserCode == "")
            {
                ErrMsg = "操作用户为空";
                return ErrMsg;
            }

            if (txtOperContractAccountID.Value.Trim() == "")
            {
                ErrMsg = "请填写审批表编号";
                return ErrMsg;
            }

            if (dao == null)
            {
                entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.OperationCode);
            }
            else
            {
                entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.OperationCode, dao);
            }



            if (entity.Tables["ContractAccount"].Select(String.Format("ContractAccountCode='{0}'", this.ApplicationCode)).Length == 0)
            {
                //新增
                DataRow drAdd = entity.GetNewRecord("ContractAccount");
                drAdd["ContractAccountCode"] = this.ApplicationCode;
                drAdd["ContractCode"] = this.ContractCode;
                drAdd["Status"] = 0;
                drAdd["CreateDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                drAdd["CreatePerson"] = this.UserCode;

                try
                {
                    drAdd["contractchangeid"] = Request["ContractChangeCode"] + string.Empty;
                }
                catch
                {
                }
                entity.Tables["ContractAccount"].Rows.Add(drAdd);
            }


            string ContractChangeCode = Request["ContractChangeCode"];
            if (String.IsNullOrEmpty(ContractChangeCode))
            {

                string temp = string.Empty;
                foreach (DataRow dr in entity.Tables["ContractAccount"].Select(String.Format("ContractAccountCode='{0}'", this.ApplicationCode), "", DataViewRowState.CurrentRows))
                {
                    temp = dr["contractchangeid"].ToString();
                }

                ContractChangeCode = temp;


            }

            if (!String.IsNullOrEmpty(ContractChangeCode))
            {
                foreach (DataRow dr in entity.Tables["ContractChange"].Select(String.Format("ContractChangeCode={0}", ContractChangeCode), "", DataViewRowState.CurrentRows))
                {
                    dr["status"] = 2;
                }
            }

            foreach (DataRow dr in entity.Tables["ContractAccount"].Select(String.Format("ContractAccountCode='{0}'", this.ApplicationCode), "", DataViewRowState.CurrentRows))
            {
                dr["ContractAccountID"] = txtOperContractAccountID.Value;
                dr["Reason"] = txtOperReason.Value;
                try
                {
                    dr["contractchangeid"] = ContractChangeCode;
                }
                catch { }
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

                EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.OperationCode, this.dao);

                entity.SetCurrentTable("Contract");

                if (entity.HasRecord())
                {
                    DataRow dr = entity.CurrentRow;

                    switch (pm_sOpinionConfirm)
                    {
                        case "Approve":

                            dr["Status"] = 2;

                            //entity = DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(contractCode);

                            try
                            {
                                string contractchangecode = entity.Tables["ContractChange"].Rows[entity.Tables["ContractChange"].Rows.Count - 1]["contractchangecode"].ToString();

                                RmsPM.BLL.ContractRule.ContractChangeAuditing(entity, contractchangecode, true);
                            }
                            catch (Exception ex)
                            {
                                //Response.Write(Rms.Web.JavaScript.Alert(true, ex.ToString()));
                            }

                            break;
                        case "Reject":

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

                    RmsPM.DAL.EntityDAO.ContractDAO.SubmitAllStandard_Contract(entity);
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



    public override bool Delete(StandardEntityDAO dao)
    {
        if (this.ApplicationCode != "")
        {
            this.OperationCode = RmsPM.BLL.ContractRule.GetContractCodeByAccountCode(this.ApplicationCode);

        }
        else if (this.OperationCode != "")
        {
            this.ApplicationCode = RmsPM.DAL.EntityDAO.SystemManageDAO.GetNewSysCode("ContractAccountCode");

        }
        else
        {
            return false;
        }

        this.ContractCode = this.OperationCode;

        EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode);

        //RmsPM.BLL.ContractRule.ContractStatusChange(this.ContractCode, 2);

        foreach (DataRow dr in entity.Tables["ContractAccount"].Select(String.Format("ContractAccountCode='{0}'", this.ApplicationCode), "", DataViewRowState.CurrentRows))
        {
            dr["Status"] = 1;

            if (!String.IsNullOrEmpty(dr["contractchangeid"].ToString()))
            {
                DataRow[] drs = entity.Tables["Contractchange"].Select(String.Format("contractchangecode='{0}'", dr["contractchangeid"].ToString(), "", DataViewRowState.CurrentRows));
                if (drs.Length >= 1)
                    drs[0]["status"] = 1;
            }
        }

        dao.SubmitEntity(entity);


        return base.Delete(dao);

    }

    #endregion --- 公共方法 ---

    protected void Page_Load(object sender, System.EventArgs e)
    {
        // 在此处放置用户代码以初始化页面
        if (this.ApplicationCode != "")
        {
            this.myOperAttachMentAdd.AttachMentType = "ContractAccountAttachMent";
            this.myOperAttachMentAdd.MasterCode = this.ApplicationCode;

            this.myEyeAttachMentList.AttachMentType = "ContractAccountAttachMent";
            this.myEyeAttachMentList.MasterCode = this.ApplicationCode;
        }
    }

}