using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rms.ORMap;
using RmsPM.Web.WorkFlowControl;
using RmsPM.Web;

	/// <summary>
	///		SM_ContractAuditing 的摘要说明。
	/// </summary>
public partial class WorkFlowOperation_SM_ContractAuditing : WorkFlowOperationBase
{



    #region --- 公共方法 ---

    /// <summary>
    /// 控件初始化
    /// </summary>
    public override void InitControl()
    {
        try
        {
            this.inputOperSystemGroup.ClassCode = "0501";
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

            this.ContractCode = this.OperationCode;

            EntityData entity = RmsPM.DAL.EntityDAO.ContractDAO.GetStandard_ContractByCode(this.ContractCode);

            entity.SetCurrentTable("Contract");

            if (!entity.HasRecord())
            {
                return;
            }

            this.ProjectCode = entity.GetString("ProjectCode");
            this.ApplicationTitle = entity.GetString("ContractName");
            this.ApplicationType = RmsPM.BLL.SystemGroupRule.GetSystemGroupSortIDByGroupCode(entity.GetString("Type"));


            string ud_sHyperLinkFormat = "../Contract/ContractInfo.aspx?ContractCode={0}&ProjectCode={1}";

            //合同基本信息
            lblOperProjectName.Text = RmsPM.BLL.ProjectRule.GetProjectName(this.ProjectCode);
            lblEyeProjectName.Text = lblOperProjectName.Text;

            txtOperContractID.Value = entity.GetString("ContractID");
            lblEyeContractID.Text = txtOperContractID.Value;

            lblOperContractName.Text = ShowApplicationHyperLink(entity.GetString("ContractName"), string.Format(ud_sHyperLinkFormat, this.ContractCode, this.ProjectCode));
            lblEyeContractName.Text = lblOperContractName.Text;

            inputOperSystemGroup.Value = entity.GetString("Type");
            lblEyeSystemGroupName.Text = inputOperSystemGroup.Text;

            txtOperSupplierCode.Value = entity.GetString("SupplierCode");
            txtOperSupplierName.Value = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("SupplierCode"));
            lblEyeSupplierName.Text = txtOperSupplierName.Value;

            txtOperContractObject.Value = entity.GetString("ContractObject");
            lblEyeContractObject.Text = HttpUtility.HtmlEncode(txtOperContractObject.Value).Replace("\n", "<br>");

            OperContractDate.Value = entity.GetDateTimeOnlyDate("ContractDate");
            lblEyeContractDate.Text = OperContractDate.Value;

            txtOperSupplier2Code.Value = entity.GetString("Supplier2Code");
            txtOperSupplier2Name.Value = RmsPM.BLL.ProjectRule.GetSupplierName(entity.GetString("Supplier2Code"));
            lblEyeSupplier2Name.Text = txtOperSupplier2Name.Value;

            //txtOperBuilding.Value = entity.GetString("Building");
            //lblEyeBuilding.Text = txtOperBuilding.Value;

            //显示合同金额
            decimal TotalMoney, OriginalMoney, BudgetMoney, AdjustMoney;

            //合同原币金额
            decimal OriginalCash, TotalChangeCash, TotalCash;

            string contractLabel = entity.GetString("ContractLabel");

            TotalMoney = entity.GetDecimal("TotalMoney");
            OriginalMoney = entity.GetDecimal("OriginalMoney");
            BudgetMoney = entity.GetDecimal("BudgetMoney");
            AdjustMoney = entity.GetDecimal("AdjustMoney");

            string[] arrField = { "Cash", "OriginalCash"};
            decimal[] arrValue = RmsPM.BLL.MathRule.SumColumn(entity.Tables["ContractCostCash"], arrField);
            TotalCash = arrValue[0];
            OriginalCash = arrValue[1];
            TotalChangeCash = TotalCash - OriginalCash;

            //显示原币币种 xyq 2007.1.25
            if (entity.Tables["ContractCostCash"].Rows.Count > 0)
            {
                this.lblOperMoneyType.Text = RmsPM.BLL.ConvertRule.ToString(entity.Tables["ContractCostCash"].Rows[0]["MoneyType"]);
                this.lblEyeMoneyType.Text = this.lblOperMoneyType.Text;
            }

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
                    break;

                case ModuleState.Operable://可操作的
                case ModuleState.Eyeable://可见的

                    txtOperOriginalMoney.Value = OriginalCash.ToString("N");
                    txtOperTotalChangeMoney.Value = TotalChangeCash.ToString("N");
                    txtOperNewTotalMoney.Value = TotalCash.ToString("N");
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
            txtEyeNewTotalMoney.Value = txtOperNewTotalMoney.Value;
            txtEyeBudgetMoney.Value = txtOperBudgetMoney.Value;
            txtEyeAdjustMoney.Value = txtOperAdjustMoney.Value;


            //业务流程属性保存
            SaveOperationProperty("合同金额", entity.GetDecimal("TotalMoney").ToString());
            SaveOperationProperty("主要标段", entity.GetInt("Mostly").ToString());


            entity.Dispose();
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

         
            if (inputOperSystemGroup.Value.Trim() == "")
            {
                ErrMsg = "请填写合同类型";
                return ErrMsg;
            }

            if (txtOperSupplierCode.Value.Trim() == "")
            {
                ErrMsg = "请填写签约单位";
                return ErrMsg;
            }

            if (OperContractDate.Value.Trim() == "")
            {
                ErrMsg = "请填写预计签约日期";
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


            //合同基本信息保存
            entity.SetCurrentTable("Contract");

            foreach (DataRow dr in entity.CurrentTable.Select(String.Format("ContractCode='{0}'", this.ContractCode), "", DataViewRowState.CurrentRows))
            {
                dr["ContractID"] = txtOperContractID.Value;
            
                dr["Type"] = inputOperSystemGroup.Value;

                dr["SupplierCode"] = txtOperSupplierCode.Value;
                dr["Supplier2Code"] = txtOperSupplier2Code.Value;

                //dr["Building"] = txtOperBuilding.Value;

                dr["ContractObject"] = txtOperContractObject.Value;
                dr["ContractDate"] = OperContractDate.Value;

//                dr["Status"] = 7;
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

                            dr["Status"] = 0;
                            dr["CheckPerson"] = this.UserCode;
                            dr["CheckDate"] = DateTime.Now.ToString("yyyy-MM-dd");

                            break;
                        case "Reject":

                            dr["Status"] = 1;
                            dr["CheckPerson"] = this.UserCode;
                            dr["CheckDate"] = DateTime.Now.ToString("yyyy-MM-dd");

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

    public override string ChangeStatusWhenSend(StandardEntityDAO dao)
    {
        try
        {

            base.ChangeStatusWhenSend(dao);

            string ErrMsg = "";

            RmsPM.BLL.ContractRule.ContractStatusChange(dao, this.OperationCode, 7);

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

            RmsPM.BLL.ContractRule.ContractStatusChange(this.OperationCode, 1);

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
            this.myOperAttachMentAdd.AttachMentType = "ContractAttachMent";
            this.myOperAttachMentAdd.MasterCode = this.ApplicationCode;

            this.myEyeAttachMentList.AttachMentType = "ContractAttachMent";
            this.myEyeAttachMentList.MasterCode = this.ApplicationCode;
        }
    }

 }